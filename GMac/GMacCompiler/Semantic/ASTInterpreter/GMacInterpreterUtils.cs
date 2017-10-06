using System.Collections.Generic;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Unary;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter
{
    internal static class GMacInterpreterUtils
    {
        /// <summary>
        /// A dictionary of basic unary expressions evaluators
        /// </summary>
        public static Dictionary<string, GMacBasicUnaryEvaluator> UnaryEvaluators = new Dictionary<string, GMacBasicUnaryEvaluator>();

        /// <summary>
        /// A dictionary of basic binary expressions evaluators
        /// </summary>
        public static Dictionary<string, GMacBasicBinaryEvaluator> BinaryEvaluators = new Dictionary<string, GMacBasicBinaryEvaluator>();


        static GMacInterpreterUtils()
        {
            UnaryEvaluators.Add(GMacOpInfo.UnaryPlus.OpName, new UnaryPlusEval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryMinus.OpName, new NegativeEval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryReverse.OpName, new ReverseEval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryGradeInvolution.OpName, new GradeInversionEval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryCliffordConjugate.OpName, new CliffordConjugateEval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryNorm2.OpName, new Norm2Eval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryMagnitude2.OpName, new Magnitude2Eval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryMagnitude.OpName, new MagnitudeEval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryEuclideanMagnitude2.OpName, new EMagnitude2Eval());
            UnaryEvaluators.Add(GMacOpInfo.UnaryEuclideanMagnitude.OpName, new EMagnitudeEval());

            BinaryEvaluators.Add(GMacOpInfo.BinaryPlus.OpName, new BinaryPlusEval());
            BinaryEvaluators.Add(GMacOpInfo.BinarySubtract.OpName, new SubtractEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryTimesWithScalar.OpName, new TimesEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryDivideByScalar.OpName, new DivideEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryOp.OpName, new OpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryGp.OpName, new GpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinarySp.OpName, new SpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryLcp.OpName, new LcpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryRcp.OpName, new RcpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryHip.OpName, new HipEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryFdp.OpName, new FdpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryCp.OpName, new CpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryAcp.OpName, new AcpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryEGp.OpName, new EGpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryESp.OpName, new ESpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryELcp.OpName, new ELcpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryERcp.OpName, new ERcpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryEHip.OpName, new EHipEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryEFdp.OpName, new EFdpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryECp.OpName, new ECpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryEAcp.OpName, new EAcpEval());
            BinaryEvaluators.Add(GMacOpInfo.BinaryDiff.OpName, new DiffEval());
        }

    }
}
