﻿using System;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Operator;

namespace GMac.Engine.Compiler.Semantic.AST
{
    public sealed class GMacStructureConstructor : ILanguageOperator
    {
        public GMacStructure Structure { get; }

        public ILanguageExpressionAtomic DefaultValueSource { get; private set; }

        public bool HasDefaultValueSource => DefaultValueSource != null;

        public string OperatorName => "construct<" + Structure.SymbolAccessName + ">";


        private GMacStructureConstructor(GMacStructure structure)
        {
            Structure = structure;
        }


        public ILanguageOperator DuplicateOperator()
        {
            return 
                new GMacStructureConstructor(Structure) 
                { 
                    DefaultValueSource = DefaultValueSource 
                };
        }


        internal void SetDefaultValueSource(ILanguageExpressionAtomic defaultValueSource)
        {
            if (!defaultValueSource.ExpressionType.IsSameType(Structure))
                throw new InvalidOperationException("Default value source must be of type " + Structure.TypeSignature);

            DefaultValueSource = defaultValueSource;
        }


        internal static GMacStructureConstructor Create(GMacStructure structure)
        {
            return new GMacStructureConstructor(structure);
        }

        internal static GMacStructureConstructor Create(GMacStructure structure, ILanguageExpressionAtomic defaultValueSource)
        {
            return 
                new GMacStructureConstructor(structure)
                {
                    DefaultValueSource = defaultValueSource
                };
        }
    }
}
