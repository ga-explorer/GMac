using System;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Operator;

namespace GMac.Engine.Compiler.Semantic.AST
{
    public sealed class GMacFrameMultivectorConstructor : ILanguageOperator
    {
        public GMacFrameMultivector MultivectorType { get; }

        public ILanguageExpressionAtomic DefaultValueSource { get; private set; }

        public bool HasDefaultValueSource => DefaultValueSource != null;

        public string OperatorName => "construct<" + MultivectorType.SymbolAccessName + ">";


        private GMacFrameMultivectorConstructor(GMacFrameMultivector mvType)
        {
            MultivectorType = mvType;
        }


        public ILanguageOperator DuplicateOperator()
        {
            return 
                new GMacFrameMultivectorConstructor(MultivectorType) 
                { 
                    DefaultValueSource = DefaultValueSource 
                };
        }


        internal void SetDefaultValueSource(ILanguageExpressionAtomic defaultValueSource)
        {
            if (!defaultValueSource.ExpressionType.IsSameType(MultivectorType))
                throw new InvalidOperationException("Default value source must be of type " + MultivectorType.TypeSignature);

            DefaultValueSource = defaultValueSource;
        }


        internal static GMacFrameMultivectorConstructor Create(GMacFrameMultivector mvType)
        {
            return new GMacFrameMultivectorConstructor(mvType);
        }

        internal static GMacFrameMultivectorConstructor Create(GMacFrameMultivector mvType, ILanguageExpressionAtomic defaultValueSource)
        {
            return new GMacFrameMultivectorConstructor(mvType) { DefaultValueSource = defaultValueSource };
        }
    }
}
