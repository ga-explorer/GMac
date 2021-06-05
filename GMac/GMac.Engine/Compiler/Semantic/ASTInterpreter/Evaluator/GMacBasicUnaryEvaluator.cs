using System;
using CodeComposerLib.Irony.DSLInterpreter;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using GMac.Engine.Compiler.Semantic.ASTConstants;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.Evaluator
{
    /// <summary>
    /// The base class for all basic unary expressions evaluators
    /// </summary>
    public abstract class GMacBasicUnaryEvaluator : LanguageBasicUnaryDynamicEvaluator
    {
        public abstract GMacOpInfo OperatorInfo { get; }

        public string OperatorSymbol => OperatorInfo.OpSymbol;

        public string OperatorName => OperatorInfo.OpName;


        public override bool UseExceptions => false;


        public override ILanguageValue Fallback(ILanguageValue value1, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }
    }
}
