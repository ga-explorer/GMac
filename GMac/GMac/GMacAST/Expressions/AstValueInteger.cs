using CodeComposerLib.Irony.Semantic.Expression.Value;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstValueInteger : AstValue
    {
        #region Static members
        #endregion


        internal ValuePrimitive<int> AssociatedIntegerValue { get; }

        internal override ILanguageValue AssociatedValue => AssociatedIntegerValue;

        public override bool IsValidIntegerValue => AssociatedIntegerValue != null;

        public override bool IsValidPrimitiveValue => AssociatedIntegerValue != null;

        public int IntegerValue => AssociatedIntegerValue.Value;


        internal AstValueInteger(ValuePrimitive<int> value)
        {
            AssociatedIntegerValue = value;
        }
    }
}
