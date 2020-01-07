using System;
using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.GMacAST;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Semantic.ASTGenerator;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator;
using GMac.GMacCompiler.Syntax;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text.Parametric;

namespace GMac.GMacCompiler
{
    /// <summary>
    /// This class can provide compilation services for compiling GMacDSL temp expressions
    /// of all kinds including composite expressions, value access objects, and language values in addition
    /// to reference resolution of AST symbols.
    /// </summary>
    public sealed class GMacDynamicCompiler : GMacTempCodeCompiler
    {
        private static readonly ParametricTextComposer ProgressTitleTemplate = 
            new ParametricTextComposer("#", "#", "#action##object#");

        private static string InitProgressTitle(string targetObject)
        {
            return ProgressTitleTemplate.GenerateUsing("Initializing Context for Compiling ", targetObject);
        }

        private static string CompileProgressTitle(string targetObject)
        {
            return ProgressTitleTemplate.GenerateUsing("Compiling ", targetObject);
        }


        public override string ProgressSourceId => "GMac Dynamic Compiler";

        public GMacScopeResolutionContext RefResContext { get; }


        public GMacDynamicCompiler()
        {
            RefResContext = new GMacScopeResolutionContext();
        }


        private bool InitializeCompiler(string targetObject, string codeText)
        {
            var progressTitle = InitProgressTitle(targetObject);

            var result = InitializeCompiler(codeText, GMacSourceParser.ParseQualifiedItem, RefResContext);

            this.ReportNormal(progressTitle, codeText, result);

            return result;
        }

        private bool InitializeCompiler(LanguageScope scope, string targetObject, string codeText)
        {
            var progressTitle = InitProgressTitle(targetObject);

            var result = InitializeCompiler(codeText, GMacSourceParser.ParseQualifiedItem, scope);

            this.ReportNormal(progressTitle, codeText, result);

            return result;
        }

        public AstNamespace GetNamespace(string codeText)
        {
            if (InitializeCompiler("Namespace", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Namespace");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.Namespace);

                this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstNamespace(symbol as GMacNamespace);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e); 

                return null;
            }
        }

        public AstFrame GetFrame(string codeText)
        {
            if (InitializeCompiler("Frame", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Frame");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.Frame);

                this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstFrame(symbol as GMacFrame);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e); 

                return null;
            }
        }

        public AstFrameMultivector GetFrameMultivector(string codeText)
        {
            if (InitializeCompiler("Multivector Type", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Multivector Type");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.FrameMultivector);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstFrameMultivector(symbol as GMacFrameMultivector);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstFrameBasisVector GatBasisVector(string codeText)
        {
            if (InitializeCompiler("Basis Vector", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Basis Vector");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.FrameBasisVector);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstFrameBasisVector(symbol as GMacFrameBasisVector);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstFrameSubspace GetSubspace(string codeText)
        {
            if (InitializeCompiler("Frame Subspace", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Frame Subspace");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.FrameSubspace);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstFrameSubspace(symbol as GMacFrameSubspace);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstConstant GetConstant(string codeText)
        {
            if (InitializeCompiler("Constant", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Constant");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.Constant);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstConstant(symbol as GMacConstant);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstStructure GetStructure(string codeText)
        {
            if (InitializeCompiler("Structure", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Structure");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.Structure);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstStructure(symbol as GMacStructure);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstStructureDataMember GetDataMember(string codeText)
        {
            if (InitializeCompiler("Structure Data Member", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Structure Data Member");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.StructureDataMember);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstStructureDataMember(symbol as SymbolStructureDataMember);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstMacro GetMacro(string codeText)
        {
            if (InitializeCompiler("Macro", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Macro");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.Macro);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstMacro(symbol as GMacMacro);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstMacroParameter GetParameter(string codeText)
        {
            if (InitializeCompiler("Macro Parameter", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Macro Parameter");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.MacroParameter);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstMacroParameter(symbol as SymbolProcedureParameter);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstLocalVariable GetLocalVariable(string codeText)
        {
            if (InitializeCompiler("Local Variable", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Local Variable");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, RoleNames.LocalVariable);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return new AstLocalVariable(symbol as SymbolLocalVariable);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstType GetGMacType(string codeText)
        {
            if (InitializeCompiler("GMac Type", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("GMac Type");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct_LanguageType(Context, RootParseNode);

                
                    this.ReportNormal(progressTitle, symbol.TypeSignature, ProgressEventArgsResult.Success);

                return new AstType(symbol);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstSymbol GetSymbol(string codeText, IEnumerable<string> allowedRoleNames)
        {
            if (InitializeCompiler("Symbol", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Symbol");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var symbol =
                    GMacValueAccessGenerator.Translate_Direct(Context, RootParseNode, allowedRoleNames);

                
                    this.ReportNormal(progressTitle, symbol.SymbolAccessName, ProgressEventArgsResult.Success);

                return symbol.ToAstSymbol();
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }


        public AstDatastoreValueAccess GetDataStoreValueAccess(string codeText)
        {
            if (InitializeCompiler("Data-store Value Access", codeText) == false) return null;

            var progressTitle = CompileProgressTitle("Data-store Value Access");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var valueAccess =
                    GMacValueAccessGenerator.Translate(Context, RootParseNode, false);

                if (valueAccess.RootSymbolAsDataStore == null)
                {
                    
                        this.ReportNormal(progressTitle, codeText, ProgressEventArgsResult.Failure);

                    return null;
                }

                
                    this.ReportNormal(progressTitle, valueAccess.GetName(), ProgressEventArgsResult.Success);

                return new AstDatastoreValueAccess(valueAccess);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstDatastoreValueAccess GetMacroParameterValueAccess(AstMacro macroInfo, string codeText)
        {
            if (InitializeCompiler(macroInfo.AssociatedMacro.ChildScope, "Macro Parameter Value Access", codeText) == false)
                return null;

            var progressTitle = CompileProgressTitle("Macro Parameter Value Access");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var valueAccess =
                    GMacValueAccessGenerator.Translate_LValue_MacroParameter(Context, RootParseNode, macroInfo.AssociatedMacro);

                
                    this.ReportNormal(progressTitle, valueAccess.GetName(), ProgressEventArgsResult.Success);

                return new AstDatastoreValueAccess(valueAccess);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstDatastoreValueAccess GetMacroParameterValueAccess(string codeText)
        {
            var progressTitle = InitProgressTitle("Macro Parameter Value Access");

            var mainScope = RefResContext.MainScope as ScopeSymbolChild;

            if (mainScope == null)
            {
                this.ReportNormal(progressTitle, codeText, false);

                return null;
            }

            var macro = mainScope.ParentLanguageSymbolWithScope as GMacMacro;

            if (macro == null)
            {
                this.ReportNormal(progressTitle, codeText, false);

                return null;
            }

            if (InitializeCompiler(codeText, GMacSourceParser.ParseQualifiedItem, mainScope) == false)
            {
                this.ReportNormal(progressTitle, codeText, false);

                return null;
            }

            progressTitle = CompileProgressTitle("Macro Parameter Value Access");

            try
            {
                RootParseNode = RootParseNode.ChildNodes[0];

                var valueAccess =
                    GMacValueAccessGenerator.Translate_LValue_MacroParameter(Context, RootParseNode, macro);

                
                    this.ReportNormal(progressTitle, valueAccess.GetName(), ProgressEventArgsResult.Success);

                return new AstDatastoreValueAccess(valueAccess);
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstExpression GetExpression(string codeText)
        {
            var progressTitle = InitProgressTitle("Expression");

            if (InitializeCompiler(codeText, GMacSourceParser.ParseExpression, RefResContext) == false)
            {
                this.ReportNormal(progressTitle, codeText, false);

                return null;
            }

            progressTitle = CompileProgressTitle("Expression");

            try
            {
                var expr = GMacExpressionGenerator.Translate(Context, RootParseNode).ToAstExpression();

                this.ReportNormal(progressTitle, expr.ToString(), ProgressEventArgsResult.Success);

                return expr;
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }

        public AstValue Evaluate(string codeText)
        {
            var expr = GetExpression(codeText);

            if (expr.IsNullOrInvalid()) return null;

            var progressTitle = CompileProgressTitle("Expression");

            try
            {
                var value =
                    GMacExpressionEvaluator.EvaluateExpression(
                        RefResContext.MainScope,
                        expr.AssociatedExpression
                        );

                
                    this.ReportNormal(progressTitle, value.ToString(), ProgressEventArgsResult.Success);

                return value.ToAstValue();
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, e);

                return null;
            }
        }


        internal List<LanguageCommand> CompileCommands(string codeText)
        {
            var progressTitle = InitProgressTitle("Commands");

            var compiledCommands = new List<LanguageCommand>();

            if (RefResContext.MainScope is ScopeCommandBlockChild == false)
            {
                this.ReportNormal(progressTitle, codeText, false);

                return compiledCommands;
            }

            if (InitializeCompiler(codeText, GMacSourceParser.ParseCommands, RefResContext) == false)
            {
                this.ReportNormal(progressTitle, codeText, false);

                return compiledCommands;
            }

            progressTitle = CompileProgressTitle("Commands");

            try
            {
                foreach (var commandNode in RootParseNode.ChildNodes)
                    compiledCommands.AddRange(
                        GMacCommandGenerator.Translate(Context, commandNode)
                        );

                
                    this.ReportNormal(progressTitle, codeText, ProgressEventArgsResult.Success);

                return compiledCommands;
            }
            catch (Exception e)
            {
                this.ReportError(progressTitle, codeText, e);

                return null;
            }
        }
    }
}
