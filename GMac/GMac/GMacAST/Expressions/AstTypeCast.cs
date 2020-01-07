using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Type;
using GMac.GMacCompiler.Semantic.AST;

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
