using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class DiffEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryDiff;


        //public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1, ValuePrimitive<MathematicaScalar> value2)
        //{
        //    return ValuePrimitive<MathematicaScalar>.Create(
        //        value1.ValuePrimitiveType,
        //        value1.Value * value2.Value
        //        );
        //}

        //public ILanguageValue Evaluate(GMacValueMultivector value1, GMacValueMultivector value2)
        //{
        //    return GMacValueMultivector.Create(
        //        value1.ValueMultivectorType,
        //        value1.SymbolicFrame.ACP(
        //            value1.MultivectorCoefficients, value2.MultivectorCoefficients
        //            )
        //        );
        //}

        //All other allowed combinations are handled using casting
    }
}
