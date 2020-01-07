using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Operator;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstBinaryExpression : AstExpression
    {
        internal BasicBinary AssociatedBinaryExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedBinaryExpression;

        internal OperatorPrimitive AssociatedOperator => AssociatedBinaryExpression.Operator as OperatorPrimitive;


        public override bool IsValidBinary => AssociatedBinaryExpression != null;

        /// <summary>
        /// The binary operator name
        /// </summary>
        public string OperatorName => AssociatedBinaryExpression.Operator.OperatorName;

        /// <summary>
        /// The binary operator symbol
        /// </summary>
        public string OperatorSymbol => AssociatedOperator.OperatorSymbolString;

        /// <summary>
        /// The first operand of this binary expression
        /// </summary>
        public AstExpression FirstOperand => AssociatedBinaryExpression.Operand1.ToAstExpression();

        /// <summary>
        /// The second operand of this binary expression
        /// </summary>
        public AstExpression SecondOperand => AssociatedBinaryExpression.Operand2.ToAstExpression();


        internal AstBinaryExpression(BasicBinary expr)
        {
            AssociatedBinaryExpression = expr;
        }
    }
}
