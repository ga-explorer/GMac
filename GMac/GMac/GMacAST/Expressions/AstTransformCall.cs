using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Operator;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Expressions
{
    public sealed class AstTransformCall : AstExpression 
    {
        internal BasicUnary AssociatedUnaryExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedUnaryExpression;

        internal ILanguageOperator AssociatedOperator => AssociatedUnaryExpression.Operator;

        internal GMacMultivectorTransform AssociatedTransform => AssociatedUnaryExpression.Operator as GMacMultivectorTransform;


        public override bool IsValidTransformCall => AssociatedUnaryExpression != null &&
                                                     AssociatedTransform != null;

        /// <summary>
        /// The qualified access name of the called transform
        /// </summary>
        public string CalledTransformAccessName => AssociatedTransform.SymbolAccessName;

        /// <summary>
        /// The called transform
        /// </summary>
        public AstTransform CalledTransform => IsValidTransformCall ? new AstTransform(AssociatedTransform) : null;

        /// <summary>
        /// The operand of the called transform
        /// </summary>
        public AstExpression Operand => AssociatedUnaryExpression.Operand.ToAstExpression();


        internal AstTransformCall(BasicUnary expr)
        {
            AssociatedUnaryExpression = expr;
        }
    }
}
