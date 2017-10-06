using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Operator;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstUnaryExpression : AstExpression
    {
        internal BasicUnary AssociatedUnaryExpression { get; }

        internal ILanguageOperator AssociatedOperator => AssociatedUnaryExpression.Operator;

        internal OperatorPrimitive AssociatedPrimitiveOperator => AssociatedUnaryExpression.Operator as OperatorPrimitive;

        internal override ILanguageExpression AssociatedExpression => AssociatedUnaryExpression;


        public override bool IsValidUnary => AssociatedUnaryExpression != null;

        /// <summary>
        /// The unary operator name
        /// </summary>
        public string OperatorName => AssociatedUnaryExpression.Operator.OperatorName;

        /// <summary>
        /// The unary operator symbol
        /// </summary>
        public string OperatorSymbol => AssociatedPrimitiveOperator.OperatorSymbolString;

        /// <summary>
        /// The operand of this unary expression
        /// </summary>
        public AstExpression Operand => AssociatedUnaryExpression.Operand.ToAstExpression();


        internal AstUnaryExpression(BasicUnary expr)
        {
            AssociatedUnaryExpression = expr;
        }
    }
}
