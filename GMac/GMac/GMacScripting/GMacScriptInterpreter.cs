using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Symbol;
using DataStructuresLib;
using GeometricAlgebraSymbolicsLib;
using GMac.GMacAPI.Binding;
using GMac.GMacAST;
using GMac.GMacAST.Commands;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator;
using TextComposerLib;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;
using Wolfram.NETLink;

namespace GMac.GMacScripting
{
    /// <summary>
    /// The interpreter class for GMac Scripting
    /// </summary>
    public sealed class GMacScriptInterpreter : IProgressReportSource
    {
        /// <summary>
        /// The rolenames of the main symbols of the AST
        /// </summary>
        private static readonly string[] AllowedSymbolRoleNames =
        {
            RoleNames.Namespace,
            RoleNames.Frame,
            RoleNames.FrameBasisVector,
            RoleNames.FrameMultivector,
            RoleNames.FrameSubspace,
            RoleNames.Constant,
            RoleNames.Structure,
            //RoleNames.StructureDataMember,
            RoleNames.Macro,
            //RoleNames.MacroParameter,
            RoleNames.LocalVariable
        };


        private readonly ParametricTextComposer _substituteComposer;

        private CommandBlock _mainCommandBlock;

        private readonly Dictionary<string, LanguageSymbol> _symbolsCache;

        private readonly GMacDynamicCompiler _expressionCompiler;

        private readonly GMacTempSymbolCompiler _tempSymbolsCompiler;

        private GMacExpressionEvaluator _expressionEvaluator;


        internal GMacScopeResolutionContext RefResContext => _expressionCompiler.RefResContext;

        internal IEnumerable<LanguageCommand> GMacCommands => _mainCommandBlock.Commands;

        internal GMacScriptShortcuts Shortcuts { get; }


        /// <summary>
        /// The root of the GMacAST this interpreter uses
        /// </summary>
        public AstRoot Root { get; private set; }

        public string ProgressSourceId => "GMac Script Interpreter";

        public ProgressComposer Progress => GMacSystemUtils.Progress;

        public GMacScriptOutput Output { get; }

        /// <summary>
        /// A list of all local variables used in the main command block
        /// </summary>
        public IEnumerable<AstLocalVariable> LocalVariables
        {
            get
            {
                return
                    _mainCommandBlock
                    .LocalVariables
                    .Select(v => new AstLocalVariable(v));
            }
        }

        /// <summary>
        /// A list of all child commands used inside the main command block (non-recursive)
        /// </summary>
        public IEnumerable<AstCommand> Commands
        {
            get { return _mainCommandBlock.Commands.Select(c => c.ToAstCommand()); }
        }


        internal GMacScriptInterpreter(AstRoot rootAst)
        {
            Root = rootAst;
            Shortcuts = new GMacScriptShortcuts();
            Output = new GMacScriptOutput();

            _substituteComposer = new ParametricTextComposer("|{", "}|");
            _symbolsCache = new Dictionary<string, LanguageSymbol>();
            _expressionCompiler = new GMacDynamicCompiler();
            _tempSymbolsCompiler = new GMacTempSymbolCompiler();
        }


        private void AddToSymbolsCache(string symbolName, LanguageSymbol symbol)
        {
            if (_symbolsCache.ContainsKey(symbolName))
                _symbolsCache[symbolName] = symbol;

            else
                _symbolsCache.Add(symbolName, symbol);
        }

        /// <summary>
        /// Compile commands text into GMacDSL commands to be executed
        /// </summary>
        /// <param name="commandsText"></param>
        /// <returns></returns>
        private List<LanguageCommand> AddCommands(string commandsText)
        {
            return _expressionCompiler.CompileCommands(commandsText);
        }


        #region Initialization of the GMacDSL Code’s Computational Context
        /// <summary>
        /// The main Reset method called by all others. The given symbol must be a frame or a namespace
        /// </summary>
        /// <param name="symbol"></param>
        public void Reset(AstSymbol symbol)
        {
            if (symbol.IsNullOrInvalid() || (symbol.IsValidFrame == false && symbol.IsValidNamespace == false))
            {
                this.ReportNormal("Reset Interpreter", ProgressEventArgsResult.Failure);

                return;
            }

            Output.Clear();

            Root = symbol.Root;

            _mainCommandBlock = CommandBlock.Create(symbol.AssociatedSymbol as SymbolWithScope);

            RefResContext.Clear(_mainCommandBlock);

            _expressionEvaluator = GMacExpressionEvaluator.CreateForDynamicEvaluation(_mainCommandBlock);

            _symbolsCache.Clear();

            this.ReportNormal("Reset Interpreter", symbol.AccessName, ProgressEventArgsResult.Success);
        }

        /// <summary>
        /// Initialize this interpreter by:
        /// 1- Set the root AST of this interpreter to the given frame's AST
        /// 2- Create a temp command block for this interpreter inside the given frame
        /// 3- Initialize the scope information by setting the main scope to the command block with no
        /// opened scopes
        /// 4- Initialize the internal expression evaluator to dynamic evaluation mode on the command block
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public void Reset(AstFrame symbol)
        {
            Reset((AstSymbol)symbol);
        }

        /// <summary>
        /// Initialize this interpreter by:
        /// 1- Set the root AST of this interpreter to the given namespace's AST
        /// 2- Create a temp command block for this interpreter inside the given namespace
        /// 3- Initialize the scope information by setting the main scope to the command block with no
        /// opened scopes
        /// 4- Initialize the internal expression evaluator to dynamic evaluation mode on the command block
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public void Reset(AstNamespace symbol)
        {
            Reset((AstSymbol)symbol);
        }

        /// <summary>
        /// Initialize this interpreter by:
        /// 1- Set the root AST of this interpreter to the given symbol's AST
        /// 2- Create a temp command block for this interpreter inside the given symbol
        /// 3- Initialize the scope information by setting the main scope to the command block with no
        /// opened scopes
        /// 4- Initialize the internal expression evaluator to dynamic evaluation mode on the command block
        /// </summary>
        /// <param name="symbolName">The name of a frame or a namespace, no other symbols are allowed</param>
        public void Reset(string symbolName)
        {
            if (RefResContext.MainScope == null)
                RefResContext.SetMainScope(Root.AssociatedAst.RootScope);

            var symbol = Symbol(symbolName);

            Reset(symbol);
        }


        /// <summary>
        /// Adds the child scope of the given namespace or frame symbol to the opened scopes
        /// </summary>
        /// <param name="symbol"></param>
        public void OpenScope(AstSymbol symbol)
        {
            if (symbol.IsValidNamespace)
            {
                OpenScope((AstNamespace)symbol);
                return;
            }

            if (symbol.IsValidFrame)
            {
                OpenScope((AstFrame)symbol);
                return;
            }

            this.ReportNormal("Open Scope", ProgressEventArgsResult.Failure);
        }

        /// <summary>
        /// Adds the child scope of the given namespace to the opened scopes
        /// </summary>
        /// <param name="symbol"></param>
        public void OpenScope(AstNamespace symbol)
        {
            if (symbol.IsNullOrInvalid())
            {
                this.ReportNormal("Open Scope", ProgressEventArgsResult.Failure);

                return;
            }

            RefResContext.OpenScope(symbol);

            this.ReportNormal("Open Scope", symbol.AccessName, ProgressEventArgsResult.Success);
        }

        /// <summary>
        /// Adds the child scope of the given frame to the opened scopes
        /// </summary>
        /// <param name="symbol"></param>
        public void OpenScope(AstFrame symbol)
        {
            if (symbol.IsNullOrInvalid())
            {
                this.ReportNormal("Open Scope", ProgressEventArgsResult.Failure);

                return;
            }

            RefResContext.OpenScope(symbol);

            this.ReportNormal("Open Scope", symbol.AccessName, ProgressEventArgsResult.Success);
        }

        /// <summary>
        /// Adds the child scope of the given namespace or frame to the opened scopes
        /// </summary>
        /// <param name="symbolName"></param>
        public void OpenScope(string symbolName)
        {
            OpenScope(Symbol(symbolName));
        }

        /// <summary>
        /// Try to open several scopes
        /// </summary>
        /// <param name="symbols"></param>
        public void OpenScope(params string[] symbols)
        {
            foreach (var symbolName in symbols)
                OpenScope(symbolName);
        }

        /// <summary>
        /// Try to open several scopes
        /// </summary>
        /// <param name="symbols"></param>
        public void OpenScope(params AstSymbol[] symbols)
        {
            foreach (var symbol in symbols)
                OpenScope(symbol);
        }


        /// <summary>
        /// Close all opened scopes
        /// </summary>
        public void CloseScope()
        {
            _symbolsCache.Clear();

            RefResContext.CloseScopes();

            this.ReportNormal("Close All Scopes");
        }

        /// <summary>
        /// Close the child scope of the given namespace
        /// </summary>
        /// <param name="symbol"></param>
        public void CloseScope(AstNamespace symbol)
        {
            if (symbol.IsNullOrInvalid())
            {
                this.ReportNormal("Close Scope", ProgressEventArgsResult.Failure);

                return;
            }

            _symbolsCache.Clear();

            RefResContext.CloseScope(symbol);

            this.ReportNormal("Close Scope", symbol.AccessName, ProgressEventArgsResult.Success);
        }

        /// <summary>
        /// Close the child scope of the given frame
        /// </summary>
        /// <param name="symbol"></param>
        public void CloseScope(AstFrame symbol)
        {
            if (symbol.IsNullOrInvalid())
            {
                this.ReportNormal("Close Scope", ProgressEventArgsResult.Failure);

                return;
            }

            _symbolsCache.Clear();

            RefResContext.CloseScope(symbol);

            this.ReportNormal("Close Scope", symbol.AccessName, ProgressEventArgsResult.Success);
        }

        /// <summary>
        /// Close the child scope of the given namespace or frame
        /// </summary>
        /// <param name="symbol"></param>
        public void CloseScope(AstSymbol symbol)
        {

            if (symbol.IsValidNamespace)
            {
                CloseScope((AstNamespace)symbol);
                return;
            }

            if (symbol.IsValidFrame)
            {
                CloseScope((AstFrame)symbol);
                return;
            }

            this.ReportNormal("Close Scope", ProgressEventArgsResult.Failure);
        }

        /// <summary>
        /// Close the given scope
        /// </summary>
        /// <param name="symbolName"></param>
        public void CloseScope(string symbolName)
        {
            CloseScope(Symbol(symbolName));
        }

        /// <summary>
        /// Close the given scopes
        /// </summary>
        /// <param name="symbolNames"></param>
        public void CloseScope(params string[] symbolNames)
        {
            foreach(var symbolName in symbolNames)
                CloseScope(symbolName);
        }

        /// <summary>
        /// Close the given scopes
        /// </summary>
        /// <param name="symbols"></param>
        public void CloseScope(params AstSymbol[] symbols)
        {
            foreach (var symbol in symbols)
                CloseScope(symbol);
        }
        #endregion

        #region Accessing GMacAST Information
        /// <summary>
        /// Find a namespace given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstNamespace Namespace(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstNamespace symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstNamespace(symbol as GMacNamespace);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetNamespace(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedNamespace);
            }

            this.ReportNormal("Request Namespace " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a frame given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstFrame Frame(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstFrame symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstFrame(symbol as GMacFrame);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetFrame(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedFrame);
            }

            this.ReportNormal("Request Frame " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a frame basis vector given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstFrameBasisVector BasisVector(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstFrameBasisVector symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstFrameBasisVector(symbol as GMacFrameBasisVector);
            }
            else
            {
                symbolInfo = _expressionCompiler.GatBasisVector(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedBasisVector);
            }

            this.ReportNormal("Request Frame Basis Vector " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a frame multivector given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstFrameMultivector FrameMultivector(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstFrameMultivector symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstFrameMultivector(symbol as GMacFrameMultivector);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetFrameMultivector(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedFrameMultivector);
            }

            this.ReportNormal("Request Multivector Type " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a frame subspace given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstFrameSubspace Subspace(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstFrameSubspace symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstFrameSubspace(symbol as GMacFrameSubspace);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetSubspace(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedSubspace);
            }

            this.ReportNormal("Request Frame Subspace " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a constant given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstConstant Constant(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstConstant symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstConstant(symbol as GMacConstant);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetConstant(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedConstant);
            }

            this.ReportNormal("Request Constant " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a structure given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstStructure Structure(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstStructure symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstStructure(symbol as GMacStructure);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetStructure(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedStructure);
            }

            this.ReportNormal("Request Structure " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        ///// <summary>
        ///// Find a structure data member given a text reference starting at the current scope information
        ///// </summary>
        ///// <param name="symbolName"></param>
        ///// <returns></returns>
        //public GMacInfoStructureDataMember DataMember(string symbolName)
        //{
        //    GMacInfoStructureDataMember symbolInfo;

        //    LanguageSymbol symbol;
        //    if (_symbolsCache.TryGetValue(symbolName, out symbol))
        //    {
        //        symbolInfo = new GMacInfoStructureDataMember(symbol as SymbolStructureDataMember);
        //    }
        //    else
        //    {
        //        symbolInfo = _expressionCompiler.DataMember(symbolName);

        //        AddToSymbolsCache(symbolName, symbolInfo.AssociatedDataMember);
        //    }

        //    _requests.Add(
        //        new InterpreterRequest(InterpreterRequestType.GetSymbol, symbolName) 
        //        { IsSuccess = symbolInfo.IsReady }
        //        );

        //    return symbolInfo;
        //}

        /// <summary>
        /// Find a macro given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstMacro Macro(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstMacro symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = new AstMacro(symbol as GMacMacro);
            }
            else
            {
                symbolInfo = _expressionCompiler.GetMacro(symbolName);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedMacro);
            }

            this.ReportNormal("Request Macro " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        ///// <summary>
        ///// Find a macro parameter given a text reference starting at the current scope information
        ///// </summary>
        ///// <param name="symbolName"></param>
        ///// <returns></returns>
        //public GMacInfoMacroParameter Parameter(string symbolName)
        //{
        //    GMacInfoMacroParameter symbolInfo;

        //    LanguageSymbol symbol;
        //    if (_symbolsCache.TryGetValue(symbolName, out symbol))
        //    {
        //        symbolInfo = new GMacInfoMacroParameter(symbol as SymbolProcedureParameter);
        //    }
        //    else
        //    {
        //        symbolInfo = _expressionCompiler.Parameter(symbolName);

        //        AddToSymbolsCache(symbolName, symbolInfo.AssociatedParameter);
        //    }

        //    _requests.Add(
        //        new InterpreterRequest(InterpreterRequestType.GetSymbol, symbolName) 
        //        { IsSuccess = symbolInfo.IsReady }
        //        );

        //    return symbolInfo;
        //}

        /// <summary>
        /// Find a local variable given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstLocalVariable LocalVariable(string symbolName)
        {
            symbolName = symbolName.Trim();

            var symbolInfo = _expressionCompiler.GetLocalVariable(symbolName);

            this.ReportNormal("Request Local Variable " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a symbol given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstSymbol Symbol(string symbolName)
        {
            symbolName = symbolName.Trim();

            AstSymbol symbolInfo;

            if (_symbolsCache.TryGetValue(symbolName, out var symbol))
            {
                symbolInfo = symbol.ToAstSymbol();
            }
            else
            {
                symbolInfo = _expressionCompiler.GetSymbol(symbolName, AllowedSymbolRoleNames);

                AddToSymbolsCache(symbolName, symbolInfo.AssociatedSymbol);
            }

            this.ReportNormal("Request Symbol " + symbolName.DoubleQuote(), symbolInfo.IsNotNullAndValid());

            return symbolInfo;
        }

        /// <summary>
        /// Find a type given a text reference starting at the current scope information
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public AstType GMacType(string symbolName)
        {
            symbolName = symbolName.Trim();

            var result = _expressionCompiler.GetGMacType(symbolName);

            this.ReportNormal("Request GMac Type " + symbolName.DoubleQuote(), result.IsNotNullAndValid());

            return result;
        }
        #endregion

        #region Compilation, Execution, and Evaluation of GMacDSL Commands and Expressions
        /// <summary>
        /// Compile a string into a LanguageValueAccess object with a datastore root symbol.
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess ValueAccess(string valueAccessName)
        {
            valueAccessName = valueAccessName.Trim();

            var result = _expressionCompiler.GetDataStoreValueAccess(valueAccessName);

            this.ReportNormal("Compile Value Access " + valueAccessName.DoubleQuote(), result.IsNotNullAndValid());

            return result;
        }

        /// <summary>
        /// Compile a string into a LanguageValueAccess object with a datastore root symbol and make sure
        /// it's of a given type
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="requiredType"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess ValueAccess(string valueAccessName, AstType requiredType)
        {
            var valueAccess = ValueAccess(valueAccessName);

            if (valueAccess.GMacType.IsSameType(requiredType))
            {
                this.ReportNormal("Compile Value Access " + valueAccessName.DoubleQuote() + " of Type " + requiredType.GMacTypeSignature, ProgressEventArgsResult.Success);

                return valueAccess;
            }

            this.ReportNormal(
                "Compile Value Access " + valueAccessName.DoubleQuote() + " of Type " + requiredType.GMacTypeSignature,
                "Value Access has Type " + valueAccess.GMacTypeSignature,
                ProgressEventArgsResult.Failure);

            return null;
            //throw new InvalidOperationException(
            //    String.Format(
            //        "Specified variable {0} is not of required type {1}",
            //        valueAccessName,
            //        requiredType.GMacTypeSignature
            //        )
            //    );
        }

        /// <summary>
        /// Return the type of the given value access
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        public AstType GMacTypeOf(string valueAccessName)
        {
            var valueAccess = ValueAccess(valueAccessName);

            if (valueAccess.IsNullOrInvalid())
            {
                this.ReportNormal("Request GMac Type of Value Access " + valueAccessName.DoubleQuote(), ProgressEventArgsResult.Failure);

                return null;
            }

            var result = valueAccess.GMacType;

            this.ReportNormal("Request GMac Type of Value Access " + valueAccessName.DoubleQuote(), result.GMacTypeSignature, ProgressEventArgsResult.Success);

            return result;
        }

        /// <summary>
        /// True if the given variable exists
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        public bool ValueAccessExists(string valueAccessName)
        {
            var result = ValueAccess(valueAccessName).IsNotNullAndValid();

            this.ReportNormal("Request Value Access Exists " + valueAccessName.DoubleQuote() + (result ? " : True" : " : False"));

            return result;
        }

        /// <summary>
        /// Compile a string into an expression object for later evaluation
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        public AstExpression Expression(string exprText)
        {
            this.ReportNormal("Request Compile Expression", exprText);

            var oldCount = _mainCommandBlock.CommandsCount;

            var finalExpr = _expressionCompiler.GetExpression(exprText);

            if (_mainCommandBlock.CommandsCount == oldCount)
                return finalExpr;

            foreach (var command in _mainCommandBlock.Commands.Skip(oldCount))
                command.AcceptVisitor(_expressionEvaluator);

            return finalExpr;
        }

        public void Declare(string varName, string varTypeName)
        {
            Execute("declare " + varName.Trim() + " : " + varTypeName.Trim());
        }

        public void Declare(string varName, IAstObjectWithType varType)
        {
            Execute("declare " + varName.Trim() + " : " + varType);
        }

        public void Assign(string lhsName, string rhsExprText)
        {
            Execute("let " + lhsName.Trim() + " = " + rhsExprText);
        }

        public void Assign(string lhsName, IAstObjectWithExpression rhsExpr)
        {
            Execute("let " + lhsName.Trim() + " = " + rhsExpr);
        }

        public void Assign(string lhsName, string lhsTypeName, string rhsExprText)
        {
            Execute("let " + lhsName.Trim() + " : " + lhsTypeName.Trim() + " = " + rhsExprText);
        }

        public void Assign(string lhsName, IAstObjectWithType lhsType, string rhsExprText)
        {
            Execute("let " + lhsName.Trim() + " : " + lhsType + " = " + rhsExprText);
        }

        public void Assign(string lhsName, string lhsTypeName, IAstObjectWithExpression rhsExpr)
        {
            Execute("let " + lhsName.Trim() + " : " + lhsTypeName.Trim() + " = " + rhsExpr);
        }

        public void Assign(string lhsName, IAstObjectWithType lhsType, IAstObjectWithExpression rhsExpr)
        {
            Execute("let " + lhsName.Trim() + " : " + lhsType + " = " + rhsExpr);
        }

        public void Assign(AstDatastoreValueAccess lhsValueAccess, string rhsExprText)
        {
            Execute("let " + lhsValueAccess + " = " + rhsExprText);
        }

        public void Assign(AstDatastoreValueAccess lhsValueAccess, IAstObjectWithExpression rhsExpr)
        {
            Execute("let " + lhsValueAccess + " = " + rhsExpr);
        }

        /// <summary>
        /// Execute the given command using the internal dynamic evaluator
        /// </summary>
        /// <param name="commandText"></param>
        public void Execute(string commandText)
        {
            if (_mainCommandBlock == null)
            {
                this.ReportNormal("Execute GMac Command not Possible; no Command Block Present", commandText, ProgressEventArgsResult.Failure);

                return;
            }

            var commandsList = AddCommands(commandText);

            if (commandsList == null)
            {
                this.ReportNormal("Execute GMac Command", commandText, ProgressEventArgsResult.Failure);

                return;
            }

            foreach (var command in commandsList)
                command.AcceptVisitor(_expressionEvaluator);

            this.ReportNormal("Execute GMac Command", commandText, ProgressEventArgsResult.Success);
        }

        /// <summary>
        /// Get the value of the given value access using the internal dynamic evaluator
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <returns></returns>
        public AstValue ValueOf(string valueAccessName)
        {
            var valueAccess = ValueAccess(valueAccessName);

            if (valueAccess.IsNullOrInvalid())
            {
                this.ReportNormal("Request Value of Value Access " + valueAccessName.DoubleQuote(), ProgressEventArgsResult.Failure);

                return null;
            }

            var result =
                valueAccess
                .AssociatedValueAccess
                .AcceptVisitor(_expressionEvaluator)
                .ToAstValue();

            if (result.IsNullOrInvalid())
            {
                this.ReportNormal("Request Value of Value Access " + valueAccessName.DoubleQuote(), ProgressEventArgsResult.Failure);

                return null;
            }

            this.ReportNormal("Request Value of Value Access " + valueAccessName.DoubleQuote(), result.ToString(), ProgressEventArgsResult.Success);

            return result;
        }

        /// <summary>
        /// Evaluate the given expression into a value
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public AstValue Evaluate(AstExpression expr)
        {
            if (expr.IsNullOrInvalid()) return null;

            var value = expr.AssociatedExpression.AcceptVisitor(_expressionEvaluator);

            if (GMacCompilerOptions.SimplifyLowLevelRhsValues)
                value.Simplify();

            return value.ToAstValue();
        }

        /// <summary>
        /// Evaluate the given expression into a value
        /// </summary>
        /// <param name="exprText"></param>
        /// <returns></returns>
        public AstValue Evaluate(string exprText)
        {
            var expr = Expression(exprText);

            if (expr.IsNullOrInvalid()) return null;

            var value = expr.AssociatedExpression.AcceptVisitor(_expressionEvaluator);

            if (GMacCompilerOptions.SimplifyLowLevelRhsValues)
                value.Simplify();

            return value.ToAstValue();
        }
        #endregion

        #region Initialize Multivector Values from Patterns and Subspaces
        /// <summary>
        /// Create a symbolic multivector value with all variable coeffiecints from a given subspace using
        /// a sequence naming template
        /// </summary>
        /// <param name="subspaceName"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        public AstValueMultivector SubspaceToMultivector(string subspaceName, string templateText)
        {
            var subspaceInfo = Subspace(subspaceName);

            if (subspaceInfo.IsNullOrInvalid()) return null;

            var pattern = GMacMultivectorBinding.Create(subspaceInfo);

            return pattern.ToValue(new StringSequenceTemplate(templateText));
        }

        public AstValueMultivector SubspaceToMultivector(string subspaceName, StringSequenceTemplate varNameTemplate)
        {
            var subspaceInfo = Subspace(subspaceName);

            if (subspaceInfo.IsNullOrInvalid()) return null;

            var pattern = GMacMultivectorBinding.Create(subspaceInfo);

            return pattern.ToValue(varNameTemplate);
        }

        public AstValueMultivector SubspaceToMultivector(string subspaceName, Func<int, string> basisBladeToVarName)
        {
            var subspaceInfo = Subspace(subspaceName);

            if (subspaceInfo.IsNullOrInvalid()) return null;

            var pattern = GMacMultivectorBinding.Create(subspaceInfo);

            return pattern.ToValue(basisBladeToVarName);
        }

        public AstValueMultivector SubspaceToMultivector(string subspaceName, Func<AstFrame, int, string> basisBladeToVarName)
        {
            var subspaceInfo = Subspace(subspaceName);

            if (subspaceInfo.IsNullOrInvalid()) return null;

            var pattern = GMacMultivectorBinding.Create(subspaceInfo);

            return pattern.ToValue(basisBladeToVarName);
        }

        public AstValueMultivector SubspaceToMultivector(string subspaceName, Func<AstFrameBasisBlade, string> basisBladeToVarName)
        {
            var subspaceInfo = Subspace(subspaceName);

            if (subspaceInfo.IsNullOrInvalid()) return null;

            var pattern = GMacMultivectorBinding.Create(subspaceInfo);

            return pattern.ToValue(basisBladeToVarName);
        }

        //TODO: Add more pattern services here
        #endregion

        #region Low-Level Communication with the Symbolic Engine
        /// <summary>
        /// Use Mathematica to compute the given symbolic code into a symbolic expression
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public Expr ComputeToExpr(string codeText)
        {
            var expr = GaSymbolicsUtils.Cas.Connection.EvaluateToExpr(codeText);

            if (GaSymbolicsUtils.Cas.Connection.HasError)
            {
                this.ReportError(GaSymbolicsUtils.Cas.Connection.LastException);

                this.ReportNormal("Compute Symbolic Code To Expr Object", codeText, ProgressEventArgsResult.Failure);
            }
            else
                this.ReportNormal("Compute Symbolic Code To Expr Object", codeText, ProgressEventArgsResult.Success);

            return expr;
        }

        /// <summary>
        /// Use Mathematica to compute the given symbolic code into a symbolic expression in raw text form
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public string ComputeToString(string codeText)
        {
            var text = GaSymbolicsUtils.Cas.Connection.EvaluateToString(codeText);

            if (GaSymbolicsUtils.Cas.Connection.HasError)
            {
                this.ReportError(GaSymbolicsUtils.Cas.Connection.LastException);

                this.ReportNormal("Compute Symbolic Code To Text", codeText, ProgressEventArgsResult.Failure);
            }
            else
                this.ReportNormal("Compute Symbolic Code To Text", codeText, ProgressEventArgsResult.Success);

            return text;
        }

        /// <summary>
        /// Use Mathematica to compute the given symbolic code into a string in input form
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public string ComputeToInputForm(string codeText)
        {
            var text = GaSymbolicsUtils.Cas.Connection.EvaluateToInputForm(codeText);

            if (GaSymbolicsUtils.Cas.Connection.HasError)
            {
                this.ReportError(GaSymbolicsUtils.Cas.Connection.LastException);

                this.ReportNormal("Compute Symbolic Code To Input Form Text", codeText, ProgressEventArgsResult.Failure);
            }
            else
                this.ReportNormal("Compute Symbolic Code To Input Form Text", codeText, ProgressEventArgsResult.Success);

            return text;
        }

        /// <summary>
        /// Use Mathematica to compute the given symbolic code into a string in input form
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public string ComputeToOutputForm(string codeText)
        {
            var text = GaSymbolicsUtils.Cas.Connection.EvaluateToOutputForm(codeText);

            if (GaSymbolicsUtils.Cas.Connection.HasError)
            {
                this.ReportError(GaSymbolicsUtils.Cas.Connection.LastException);

                this.ReportNormal("Compute Symbolic Code To Output Form Text", codeText, ProgressEventArgsResult.Failure);
            }
            else
                this.ReportNormal("Compute Symbolic Code To Output Form Text", codeText, ProgressEventArgsResult.Success);

            return text;
        }

        /// <summary>
        /// Use Mathematica to compute the given symbolic code into an image of the typeset expression
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public Image ComputeToTypeset(string codeText)
        {
            var img = GaSymbolicsUtils.Cas.Connection.EvaluateToTypeset(codeText);

            if (GaSymbolicsUtils.Cas.Connection.HasError)
            {
                this.ReportError(GaSymbolicsUtils.Cas.Connection.LastException);

                this.ReportNormal("Compute Symbolic Code To Typeset Image", codeText, ProgressEventArgsResult.Failure);
            }
            else
                this.ReportNormal("Compute Symbolic Code To Typeset Image", codeText, ProgressEventArgsResult.Success);

            return img;
        }

        /// <summary>
        /// Use Mathematica to compute the given symbolic code into an image
        /// </summary>
        /// <param name="codeText"></param>
        /// <returns></returns>
        public Image ComputeToImage(string codeText)
        {
            var img = GaSymbolicsUtils.Cas.Connection.EvaluateToImage(codeText);

            if (GaSymbolicsUtils.Cas.Connection.HasError)
            {
                this.ReportError(GaSymbolicsUtils.Cas.Connection.LastException);

                this.ReportNormal("Compute Symbolic Code To Image", codeText, ProgressEventArgsResult.Failure);
            }
            else
                this.ReportNormal("Compute Symbolic Code To Image", codeText, ProgressEventArgsResult.Success);

            return img;
        }
        #endregion

        #region Report Values Generated During Script Execution
        /// <summary>
        /// Return a simple string to describe the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string AsString(AstValueMultivector value)
        {
            if (value.IsNullOrInvalid()) 
                return "<Invalid Value>";

            if (value.IsZero) 
                return "0";

            var composer = new ListTextComposer(" + ");

            var terms = value.Terms.OrderBy(t => t.TermBasisBladeGrade).ThenBy(t => t.TermBasisBladeIndex);

            foreach (var term in terms)
            {
                if (term.TermBasisBladeId == 0)
                    composer.Add(term.CoefValue);

                else if (term.CoefValue.IsOne)
                    composer.Add(term.BasisBlade.Name);

                else if (term.CoefValue.IsMinusOne)
                    composer.Add("-" + term.BasisBlade.Name);

                else
                    composer.Add(term.CoefValue + " " + term.BasisBlade.Name);
            }

            return composer.ToString();
        }

        /// <summary>
        /// Return a simple string to describe the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string AsString(AstValue value)
        {
            if (value.IsValidMultivectorValue)
                return AsString((AstValueMultivector)value);

            //TODO: Change this for structures
            if (value.IsNullOrInvalid()) return "<Invalid Value>";

            return value.ToString();
        }

        /// <summary>
        /// Return a simple string to describe the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string AsLaTeXString(AstValueMultivector value)
        {
            if (value.IsNullOrInvalid()) 
                return "<Invalid Value>";

            if (value.IsZero) 
                return "0";

            var composer = new ListTextComposer("+");

            var terms = value.Terms.OrderBy(t => t.TermBasisBladeGrade).ThenBy(t => t.TermBasisBladeIndex);

            foreach (var term in terms)
            {
                if (term.TermBasisBladeId == 0)
                    composer.Add(term.CoefValue);

                else if (term.CoefValue.IsOne)
                    composer.Add(term.BasisBlade.Name);

                else if (term.CoefValue.IsMinusOne)
                    composer.Add("-" + term.BasisBlade.Name);

                else
                    composer.Add(term.CoefValue + " " + term.BasisBlade.Name);
            }

            return composer.ToString();
        }



        /// <summary>
        /// Substitutes the given items into the text replacing any text block in the form |{n}| where
        /// n is the order of the given item. Functionally similar to String.Format().
        /// </summary>
        /// <param name="text"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public string Substitute(string text, params object[] items)
        {
            _substituteComposer.SetTemplateText(text);
            
            return _substituteComposer.GenerateUsing(items);
        }
        #endregion

        #region Set Shortcut Names for Methods
        public void ResetShortcuts()
        {
            Shortcuts.ResetShortcuts();
        }

        public void SetShortcuts(string methodName, params string[] shortcuts)
        {
            Shortcuts.SetShortcuts(methodName, shortcuts);
        }
        #endregion
    }
}
