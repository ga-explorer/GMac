using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacUtils;
using IronyGrammars.Semantic.Expression.Value;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class ESpEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryESp;


        public ILanguageValue Evaluate(ValuePrimitive<int> value1, ValuePrimitive<int> value2)
        {
            return ValuePrimitive<int>.Create(
                value1.ValuePrimitiveType,
                value1.Value * value2.Value
                );
        }

        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1, ValuePrimitive<MathematicaScalar> value2)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                value1.Value * value2.Value
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1, GMacValueMultivector value2)
        {
            var scalar = value1.MultivectorCoefficients.EuclideanSp(value2.MultivectorCoefficients);

            return ValuePrimitive<MathematicaScalar>.Create(value1.CoefficientType, scalar[0]);
        }

        //All other allowed combinations are handled using casting
    }
}