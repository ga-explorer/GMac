using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Commands;
using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstMacro : AstSymbol
    {
        #region Static members
        #endregion


        internal GMacMacro AssociatedMacro { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedMacro;


        public override bool IsValidMacro => AssociatedMacro != null;

        /// <summary>
        /// The parameters of this macro
        /// </summary>
        public IEnumerable<AstMacroParameter> Parameters
        {
            get
            {
                return AssociatedMacro.Parameters.Select(item => new AstMacroParameter(item));
            }
        }

        /// <summary>
        /// The input parameters of this macro as a list of data-store value access objects
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> ParametersValueAccess
        {
            get
            {
                return
                    AssociatedMacro
                    .Parameters
                    .Select(item => new AstDatastoreValueAccess(LanguageValueAccess.Create(item)));
            }
        }

        /// <summary>
        /// The input parameters of this macro as a list of fully-expanded primitive data-store value access objects
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> ParametersPrimitiveValueAccess
        {
            get
            {
                return
                    AssociatedMacro
                    .Parameters
                    .SelectMany(
                        item =>
                            new AstDatastoreValueAccess(LanguageValueAccess.Create(item))
                            .ExpandAll()
                        );
            }
        }

        /// <summary>
        /// The names of the parameters of this macro
        /// </summary>
        public IEnumerable<string> ParametersNames
        {
            get
            {
                return AssociatedMacro.Parameters.Select(item => item.ObjectName);
            }
        }

        /// <summary>
        /// The input parameters of this macro
        /// </summary>
        public IEnumerable<AstMacroParameter> InputParameters
        {
            get
            {
                return 
                    AssociatedMacro
                    .Parameters
                    .Where(item => item.DirectionIn)
                    .Select(item => new AstMacroParameter(item));
            }
        }

        /// <summary>
        /// The input parameters of this macro as a list of data-store value access objects
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> InputParametersValueAccess
        {
            get
            {
                return
                    AssociatedMacro
                    .Parameters
                    .Where(item => item.DirectionIn)
                    .Select(item => new AstDatastoreValueAccess(LanguageValueAccess.Create(item)));
            }
        }

        /// <summary>
        /// The input parameters of this macro as a list of fully-expanded primitive data-store value access objects
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> InputParametersPrimitiveValueAccess
        {
            get
            {
                return
                    AssociatedMacro
                    .Parameters
                    .Where(item => item.DirectionIn)
                    .SelectMany(
                        item => 
                            new AstDatastoreValueAccess(LanguageValueAccess.Create(item))
                            .ExpandAll()
                        );
            }
        }

        /// <summary>
        /// The names of the input parameters of this macro
        /// </summary>
        public IEnumerable<string> InputParametersNames
        {
            get
            {
                return AssociatedMacro.Parameters.Where(item => item.DirectionIn).Select(item => item.ObjectName);
            }
        }

        /// <summary>
        /// The macros called inside the body of this macro
        /// </summary>
        public IEnumerable<AstMacro> CalledMacros
        {
            get
            {
                var dict = new Dictionary<string, GMacMacro>();

                var stack = new Stack<CommandBlock>();
                
                stack.Push(AssociatedMacro.SymbolBody);

                while (stack.Count > 0)
                {
                    var commandBlock = stack.Pop();

                    foreach (var command in commandBlock.CommandsNoDeclare)
                    {
                        var childBlock = command as CommandBlock;

                        if (ReferenceEquals(childBlock, null) == false)
                        {
                            stack.Push(childBlock);

                            continue;
                        }

                        var letCommand = command as CommandAssign;

                        if (ReferenceEquals(letCommand, null) == false)
                        {
                            var compositeExpr = letCommand.RhsExpression as CompositeExpression;

                            if (ReferenceEquals(compositeExpr, null) == false)
                            {
                                stack.Push(compositeExpr);

                                continue;
                            }

                            var polyadicExpr = letCommand.RhsExpression as BasicPolyadic;

                            if (ReferenceEquals(polyadicExpr, null) == false)
                            {
                                var calledMacro = polyadicExpr.Operator as GMacMacro;

                                if (ReferenceEquals(calledMacro, null) == false && dict.ContainsKey(calledMacro.SymbolAccessName) == false)
                                    dict.Add(calledMacro.SymbolAccessName, calledMacro);
                            }
                        }

                    }
                }

                return dict.Select(pair => pair.Value.ToAstMacro());
            }
        }

        /// <summary>
        /// The output type of this macro
        /// </summary>
        public AstType OutputType => new AstType(AssociatedMacro.OutputParameterType);

        /// <summary>
        /// The output parameter of this macro
        /// </summary>
        public AstMacroParameter OutputParameter => new AstMacroParameter(AssociatedMacro.OutputParameter);

        /// <summary>
        /// The output parameter name of this macro
        /// </summary>
        public string OutputParameterName => AssociatedMacro.OutputParameter.ObjectName;

        /// <summary>
        /// The output parameter of this macro as a data-store value access object
        /// </summary>
        public AstDatastoreValueAccess OutputParameterValueAccess => new AstDatastoreValueAccess(
            LanguageValueAccess.Create(AssociatedMacro.OutputParameter)
            );

        /// <summary>
        /// The output parameter of this macro as a list of fully-expanded prmitive data-store value access objects
        /// </summary>
        public IEnumerable<AstDatastoreValueAccess> OutputParameterPrimitiveValueAccess => new AstDatastoreValueAccess(
            LanguageValueAccess.Create(AssociatedMacro.OutputParameter)
            ).ExpandAll();

        /// <summary>
        /// The parameter types of this macro
        /// </summary>
        public IEnumerable<AstType> ParametersTypes
        {
            get
            {
                var dict = new Dictionary<string, AstType>();

                foreach (var parameter in AssociatedMacro.Parameters)
                {
                    var typeSignature = parameter.SymbolTypeSignature;

                    if (dict.ContainsKey(typeSignature) == false)
                        dict.Add(typeSignature, new AstType(parameter.SymbolType));
                }

                return dict.Values;
            }
        }

        /// <summary>
        /// The parsed command block body of this macro
        /// </summary>
        public AstCommandBlock CommandBlock => new AstCommandBlock(AssociatedMacro.SymbolBody);

        /// <summary>
        /// The compiled command block body of this macro
        /// </summary>
        public AstCommandBlock CompiledCommandBlock => new AstCommandBlock(AssociatedMacro.CompiledBody);

        /// <summary>
        /// The optimized command block body of this macro
        /// </summary>
        public AstCommandBlock OptimizedCommandBlock => new AstCommandBlock(AssociatedMacro.OptimizedCompiledBody);


        internal AstMacro(GMacMacro macro)
        {
            AssociatedMacro = macro;
        }



        /// <summary>
        /// The data members of this structure grouped by type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<AstType, List<AstMacroParameter>>> GroupParametersByType()
        {
            var dict =
                new Dictionary<string, List<AstMacroParameter>>();

            foreach (var parameter in AssociatedMacro.Parameters)
            {
                List<AstMacroParameter> list;

                var typeSignature = parameter.SymbolTypeSignature;

                if (dict.TryGetValue(typeSignature, out list) == false)
                {
                    list = new List<AstMacroParameter>();

                    dict.Add(typeSignature, list);
                }

                list.Add(new AstMacroParameter(parameter));
            }

            return
                dict.Select(
                    pair => new KeyValuePair<AstType, List<AstMacroParameter>>(
                        pair.Value[0].GMacType,
                        pair.Value
                        )
                    );
        }

        /// <summary>
        /// Finds a macro parameter component by its value access name
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstDatastoreValueAccess Parameter(string accessName)
        {
            return Root.DynamicCompiler.GetMacroParameterValueAccess(this, accessName);

            //SymbolProcedureParameter symbol;

            //AssociatedMacro.LookupParameter(accessName, out symbol);

            //return new AstMacroParameter(symbol);
        }

        /// <summary>
        /// Finds a macro parameter component type by its value access name
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstType ParameterType(string accessName)
        {
            var valueAccess = Root.DynamicCompiler.GetMacroParameterValueAccess(this, accessName);

            return valueAccess.IsNullOrInvalid() ? null : valueAccess.GMacType;

            //SymbolProcedureParameter symbol;

            //return 
            //    AssociatedMacro.LookupParameter(accessName, out symbol) 
            //    ? new AstType(symbol.SymbolType) 
            //    : null;
        }

        /// <summary>
        /// Return the parsed, compiled, or optimized command block body of this macro. The default
        /// is the optimized command block body.
        /// </summary>
        /// <param name="macroBodyKind"></param>
        /// <returns></returns>
        public AstCommandBlock GetBodyCommandBlock(AstMacroBodyKind macroBodyKind = AstMacroBodyKind.ParsedBody)
        {
            if (macroBodyKind == AstMacroBodyKind.RawBody)
                return CommandBlock;

            if (macroBodyKind == AstMacroBodyKind.ParsedBody)
                return CommandBlock;

            if (macroBodyKind == AstMacroBodyKind.CompiledBody)
                return CompiledCommandBlock;

            return OptimizedCommandBlock;
        }
    }
}
