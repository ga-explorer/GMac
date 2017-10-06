using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstMacroParameter : AstSymbol, IAstObjectWithDatastoreValueAccess
    {
        #region Static members
        #endregion


        internal SymbolProcedureParameter AssociatedParameter { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedParameter;

        public override bool IsValidMacroParameter => AssociatedParameter != null;

        public override bool IsValidDatastore => AssociatedParameter != null;

        public override bool IsValidVariableDatastore => AssociatedParameter != null;

        public AstType GMacType => new AstType(AssociatedParameter.SymbolType);

        public string GMacTypeSignature => AssociatedParameter.SymbolTypeSignature;

        public AstExpression Expression => LanguageValueAccess.Create(AssociatedParameter).ToAstExpression();

        public AstDatastoreValueAccess DatastoreValueAccess => new AstDatastoreValueAccess(LanguageValueAccess.Create(AssociatedParameter));

        /// <summary>
        /// The parent macro of this parameter
        /// </summary>
        public AstMacro ParentMacro => new AstMacro((GMacMacro)AssociatedParameter.ParentLanguageSymbol);

        /// <summary>
        /// True if this is an input parameter
        /// </summary>
        public bool IsInput => AssociatedParameter.DirectionIn;

        /// <summary>
        /// True if this is an output parameter
        /// </summary>
        public bool IsOutput => AssociatedParameter.DirectionOut;


        internal AstMacroParameter(SymbolProcedureParameter parameter)
        {
            AssociatedParameter = parameter;
        }
    }
}
