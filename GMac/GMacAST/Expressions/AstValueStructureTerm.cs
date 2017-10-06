using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Expression.Value;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstValueStructureTerm : AstValue
    {
        #region Static members
        #endregion


        private ValueStructureSparse _associatedStructureValue;


        internal ValueStructureSparse AssociatedStructureValue
        {
            get
            {
                if (ReferenceEquals(_associatedStructureValue, null))
                {
                    _associatedStructureValue = ValueStructureSparse.Create(AssociatedStructure);

                    _associatedStructureValue.Add(DataMemberName, TermDataMemberValue);
                    
                }

                return _associatedStructureValue;
            }
        }

        internal GMacStructure AssociatedStructure { get; }

        internal ILanguageValue TermDataMemberValue { get; }

        internal override ILanguageValue AssociatedValue => AssociatedStructureValue;


        public override bool IsValidStructureTermValue => AssociatedStructure != null;

        public override bool IsValidCompositeValue => AssociatedStructure != null;

        /// <summary>
        /// The name of the data member of this term
        /// </summary>
        public string DataMemberName { get; }

        //public SimpleTreeNode<Expr> GetAstValueSimpleTree
        //{
        //    get
        //    {
        //        var dataMember = AssociatedStructure.GetDataMember(DataMemberName);

        //        return new SimpleTreeBranchDictionaryByIndex<Expr>
        //        {
        //            {
        //                dataMember.DefinitionIndex, 
        //                DataMemberName, 
        //                dataMember.SymbolTypeSignature,
        //                TermDataMemberValue.ToSimpleExprTree()
        //            }
        //        };
        //    }
        //}

        /// <summary>
        /// The structure type of this value
        /// </summary>
        public AstStructure Structure => new AstStructure(AssociatedStructure);

        /// <summary>
        /// The data member of this term
        /// </summary>
        public AstStructureDataMember DataMember => new AstStructureDataMember(AssociatedStructure.GetDataMember(DataMemberName));

        /// <summary>
        /// The value of the data member in this term
        /// </summary>
        public AstValue DataMemberValue => TermDataMemberValue.ToAstValue();

        /// <summary>
        /// Convert this term into a structure value
        /// </summary>
        public AstValueStructure ToStructureValue
        {
            get
            {
                var value = ValueStructureSparse.Create(AssociatedStructure);

                value.Add(DataMemberName, TermDataMemberValue);

                return new AstValueStructure(value);
            }
        }


        internal AstValueStructureTerm(GMacStructure structure, string dataMemberName, ILanguageValue dataMemberValue)
        {
            AssociatedStructure = structure;
            DataMemberName = dataMemberName;
            TermDataMemberValue = dataMemberValue;
        }
    }
}
