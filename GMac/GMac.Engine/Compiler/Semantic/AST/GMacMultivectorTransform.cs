using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using GeometricAlgebraSymbolicsLib.Maps.Unilinear;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.Compiler.Semantic.AST
{
    /// <summary>
    /// A GMac multivector linear transform
    /// </summary>
    public sealed class GMacMultivectorTransform : SymbolWithScope, ILanguageOperator
    {
        /// <summary>
        /// The source GMac frame (the domain)
        /// </summary>
        public GMacFrame SourceFrame { get; private set; }

        /// <summary>
        /// The taeget GMac frame (the co-domain)
        /// </summary>
        public GMacFrame TargetFrame { get; }

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
