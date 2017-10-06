using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Operator;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstTypeCast : AstExpression
    {
        internal BasicUnary AssociatedUnaryExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedUnaryExpression;

        internal ILanguageOperator AssociatedOperator => AssociatedUnaryExpression.Operator;

        internal ILanguageType AssociatedLanguageType
        {
            get
            {
                var op1 = AssociatedUnaryExpression.Operator as GMacFrameMultivector;
                if (ReferenceEquals(op1, null) == false) return op1;

                var op2 = AssociatedUnaryExpression.Operator as TypePrimitive;
                if (ReferenceEquals(op2, null) == false) return op2;

                return null;
            }
        }


        public override bool IsValidTypeCast => AssociatedUnaryExpression != null &&
                                                AssociatedLanguageType != null;

        /// <summary>
        /// The operand expression of this type cast
        /// </summary>
        public AstExpression Operand => AssociatedUnaryExpression.Operand.ToAstExpression();


        internal AstTypeCast(BasicUnary expr)
        {
            AssociatedUnaryExpression = expr;
        }
    }
}
