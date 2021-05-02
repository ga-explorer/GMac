using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstValueScalar : AstValue
    {
        #region Static members
        #endregion


        internal ValuePrimitive<MathematicaScalar> AssociatedScalarValue { get; }

        internal override ILanguageValue AssociatedValue => AssociatedScalarValue;


        public override bool IsValidScalarValue => AssociatedScalarValue != null;

        public override bool IsValidPrimitiveValue => AssociatedScalarValue != null;

        /// <summary>
        /// The symbolic scalar of this value
        /// </summary>
        public MathematicaScalar ScalarValue => AssociatedScalarValue.Value;

        /// <summary>
        /// The Mathematica expression of this value
        /// </summary>
        public Expr ScalarValueExpr => AssociatedScalarValue.Value.Expression;

        /// <summary>
        /// True if the value of this scalar is the integer zero
        /// </summary>
        public bool IsZero => ScalarValue.IsZero();

        /// <summary>
        /// True if the value of this scalar is the integer 1
        /// </summary>
        public bool IsOne => ScalarValue.IsOne();

        /// <summary>
        /// True if the value of this scalar is the integer -1
        /// </summary>
        public bool IsMinusOne => ScalarValue.IsMinusOne();


        internal AstValueScalar(ValuePrimitive<MathematicaScalar> value)
        {
            AssociatedScalarValue = value;
        }
    }
}
