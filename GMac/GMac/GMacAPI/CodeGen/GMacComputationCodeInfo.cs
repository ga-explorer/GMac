﻿using CodeComposerLib.SyntaxTree.Expressions;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacAPI.Target;

namespace GMac.GMacAPI.CodeGen
{
    public sealed class GMacComputationCodeInfo
    {
        public GMacCbComputedVariable ComputedVariable { get; internal set; }

        public string TargetVariableName 
            => ComputedVariable.TargetVariableName;

        public SteExpression RhsExpressionCode { get; internal set; }

        public GMacLanguageServer GMacLanguage { get; internal set; }

        public bool EnableCodeGeneration { get; set; }
    }
}