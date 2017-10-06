using System;
using GMac.GMacCompiler.Semantic.ASTConstants;
using IronyGrammars.DSLInterpreter;
using IronyGrammars.Semantic.Expression.Value;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator
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
