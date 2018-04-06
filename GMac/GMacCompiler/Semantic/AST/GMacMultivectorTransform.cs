using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacMath.Symbolic.Maps.Unilinear;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Operator;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// A GMac multivector linear transform
    /// </summary>
    public sealed class GMacMultivectorTransform : SymbolWithScope, ILanguageOperator
    {
        /// <summary>
        /// The source GMac frame (the domain)
        /// </summary>
        internal GMacFrame SourceFrame { get; private set; }

        /// <summary>
        /// The taeget GMac frame (the co-domain)
        /// </summary>
        internal GMacFrame TargetFrame { get; }

        /// <summary>
        /// The associated symbolic linear transform
        /// </summary>
        internal IGaSymMapUnilinear AssociatedSymbolicTransform { get; private set; }

        internal GMacAst GMacRootAst => (GMacAst)RootAst;


        internal GMacMultivectorTransform(string transformName, LanguageScope parentScope, GMacFrame sourceFrame, GMacFrame targetFrame, IGaSymMapUnilinear symbolicTransform)
            : base(transformName, parentScope, RoleNames.Transform)
        {
            SourceFrame = sourceFrame;
            TargetFrame = targetFrame;
            AssociatedSymbolicTransform = symbolicTransform;
        }


        public ILanguageOperator DuplicateOperator()
        {
            return this;
        }


        public string OperatorName => SymbolAccessName;


        internal BasicUnary CreateTransformExpression(ILanguageExpressionAtomic operand)
        {
            return BasicUnary.Create(TargetFrame.MultivectorType, this, operand);
        }
    }
}
