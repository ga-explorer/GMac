using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using IronyGrammars.Semantic.Expression.Value;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class DivideEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryDivideByScalar;


        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1, ValuePrimitive<MathematicaScalar> value2)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                value1.Value / value2.Value
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1, ValuePrimitive<MathematicaScalar> value2)
        {
            return GMacValueMultivector.Create(
                value1.ValueMultivectorType,
                value1.MultivectorCoefficients / value2.Value
                );
        }

        //All other allowed combinations are handled using casting
    }
}
