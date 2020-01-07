using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstStructureDataMember : AstSymbol, IAstObjectWithType
    {
        #region Static members
        #endregion


        internal SymbolStructureDataMember AssociatedDataMember { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedDataMember;

        public override bool IsValidStructureDataMember => AssociatedDataMember != null;

        public AstType GMacType => new AstType(AssociatedDataMember.SymbolType);

        public string GMacTypeSignature => AssociatedDataMember.SymbolTypeSignature;

        /// <summary>
        /// The parent structure of this data member
        /// </summary>
        public AstStructure ParentStructure => new AstStructure((GMacStructure)AssociatedDataMember.ParentLanguageSymbol);


        internal AstStructureDataMember(SymbolStructureDataMember structMember)
        {
            AssociatedDataMember = structMember;
        }
    }
}
