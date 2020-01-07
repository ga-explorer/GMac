using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstValueStructure : AstValue
    {
        #region Static members
        #endregion


        internal ValueStructureSparse AssociatedStructureValue { get; }

        internal override ILanguageValue AssociatedValue => AssociatedStructureValue;


        public override bool IsValidStructureValue => AssociatedStructureValue != null;

        public override bool IsValidCompositeValue => AssociatedStructureValue != null;

        /// <summary>
        /// The structure type of this value
        /// </summary>
        public AstStructure Structure => new AstStructure((GMacStructure) AssociatedStructureValue.ValueStructureType);

        /// <summary>
        /// The terms of this value
        /// </summary>
        public IEnumerable<AstValueStructureTerm> Terms
        {
            get
            {
                var structure = (GMacStructure) AssociatedStructureValue.ValueStructureType;

                return
                    AssociatedStructureValue.Select(
                        pair =>
                            new AstValueStructureTerm(
                                structure, 
                                pair.Key, 
                                pair.Value
                                )
                        );
            }
        }

        /// <summary>
        /// The sub-value associated with a given data member name
        /// </summary>
        /// <param name="dataMemberName"></param>
        /// <returns></returns>
        public AstValue this[string dataMemberName] => AssociatedStructureValue[dataMemberName].ToAstValue();


        internal AstValueStructure(ValueStructureSparse value)
        {
            AssociatedStructureValue = value;
        }
    }
}
