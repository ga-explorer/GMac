using CodeComposerLib.SyntaxTree.Expressions;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.API.Target;

namespace GMac.Engine.API.CodeGen
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