using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.Engine.AST.Expressions;
using GMac.Engine.Compiler.Semantic.AST;

namespace GMac.Engine.AST.Symbols
{
    public sealed class AstMacroParameter : AstSymbol, IAstObjectWithDatastoreValueAccess
    {
        #region Static members
        #endregion


        internal SymbolProcedureParameter AssociatedParameter { get; }

        public override LanguageSymbol AssociatedSymbol => AssociatedParameter;

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
