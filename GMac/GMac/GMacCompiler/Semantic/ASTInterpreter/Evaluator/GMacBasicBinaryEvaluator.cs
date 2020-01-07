using System;
using CodeComposerLib.Irony.DSLInterpreter;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using GMac.GMacCompiler.Semantic.ASTConstants;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator
{
    /// <summary>
    /// The base class for all basic binary expressions evaluators
    /// </summary>
    public abstract class GMacBasicBinaryEvaluator : LanguageBasicBinaryDynamicEvaluator
    {
        public abstract GMacOpInfo OperatorInfo { get; }

        public string OperatorSymbol => OperatorInfo.OpSymbol;

        public string OperatorName => OperatorInfo.OpName;


        public override bool UseExceptions => false;


        public override ILanguageValue Fallback(ILanguageValue value1, ILanguageValue value2, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }
    }
}
