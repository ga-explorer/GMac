using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Symbol;
using FrameUtils = GMac.GMacUtils.EuclideanUtils;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstFrameSubspace : AstSymbol
    {
        #region Static members
        #endregion


        internal GMacFrameSubspace AssociatedSubspace { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedSubspace;

        public override bool IsValidSubspace => AssociatedSubspace != null;


        /// <summary>
        /// The multivector type of this subspace
        /// </summary>
        public AstFrameMultivector FrameMultivector => new AstFrameMultivector(AssociatedSubspace.ParentFrame.MultivectorType);

        /// <summary>
        /// The basis blade IDs in this subspace
        /// </summary>
        public IEnumerable<int> BasisBladeIDs => AssociatedSubspace
            .SubspaceSignaturePattern
            .TrueIndexes;

        /// <summary>
        /// The basis blade grades in this subspace
        /// </summary>
        public IEnumerable<int> BasisBladeGrades
        {
            get
            {
                return
                    AssociatedSubspace
                    .SubspaceSignaturePattern
                    .TrueIndexes
                    .Select(GMacUtils.FrameUtils.BasisBladeGrade)
                    .Distinct()
                    .OrderBy(grade => grade);
            }
        }

        /// <summary>
        /// The basis blades in this subspace
        /// </summary>
        public IEnumerable<AstFrameBasisBlade> BasisBlades
        { 
            get 
            {
                var subspace = AssociatedSubspace;

                return 
                    AssociatedSubspace
                    .SubspaceSignaturePattern
                    .TrueIndexes
                    .Select(id => new AstFrameBasisBlade(subspace.ParentFrame, id));
            } 
        }

        /// <summary>
        /// True if this subspace has a single grade for all its basis blades
        /// </summary>
        public bool IsSingleGradeSubspace => BasisBladeGrades.Count() == 1;


        internal AstFrameSubspace(GMacFrameSubspace subspace)
        {
            AssociatedSubspace = subspace;
        }
    }
}
