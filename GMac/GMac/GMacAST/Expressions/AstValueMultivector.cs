using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstValueMultivector : AstValue
    {
        #region Static members
        #endregion


        internal GMacValueMultivector AssociatedMultivectorValue { get; }

        internal override ILanguageValue AssociatedValue => AssociatedMultivectorValue;


        public override bool IsValidMultivectorValue => AssociatedMultivectorValue != null;

        public override bool IsValidCompositeValue => AssociatedMultivectorValue != null;

        /// <summary>
        /// The multivector type of this value
        /// </summary>
        public AstFrameMultivector FrameMultivector => new AstFrameMultivector(AssociatedMultivectorValue.ValueMultivectorType);

        /// <summary>
        /// The frame of the multivector type of this value
        /// </summary>
        public AstFrame Frame => new AstFrame(AssociatedMultivectorValue.MultivectorFrame);

        /// <summary>
        /// True if this multivector value contains no terms
        /// </summary>
        public bool IsZero 
            => AssociatedMultivectorValue.SymbolicMultivector.IsZero();

        /// <summary>
        /// The multivector terms of this multivector value
        /// </summary>
        public IEnumerable<AstValueMultivectorTerm> Terms
        {
            get
            {
                var value = AssociatedMultivectorValue;
                var mvClass = AssociatedMultivectorValue.ValueMultivectorType;
                var scalarType = value.CoefficientType;

                return AssociatedMultivectorValue
                    .SymbolicMultivector
                    .NonZeroTerms
                    .Select(
                        pair =>
                            new AstValueMultivectorTerm(
                                mvClass,
                                pair.Key,
                                ValuePrimitive<MathematicaScalar>.Create(scalarType, pair.Value)
                                )
                        );
            }
        }

        /// <summary>
        /// The scalar coefficient value of abasis blade in this multivector value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AstValueScalar this[int id] => new AstValueScalar(AssociatedMultivectorValue[id]);

        /// <summary>
        /// The basis blade IDs used in this value
        /// </summary>
        public IEnumerable<int> ActiveIDs 
            => AssociatedMultivectorValue
                .SymbolicMultivector
                .BasisBladeIds;

        /// <summary>
        /// The basis blade grades used in this value
        /// </summary>
        public IEnumerable<int> ActiveGrades
            => AssociatedMultivectorValue
                .SymbolicMultivector
                .BasisBladeIds
                .Select(id => id.BasisBladeGrade())
                .Distinct();


        internal AstValueMultivector(GMacValueMultivector value)
        {
            AssociatedMultivectorValue = value;
        }
    }
}
