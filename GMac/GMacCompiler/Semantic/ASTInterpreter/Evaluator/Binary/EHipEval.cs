using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Symbolic;
using GMac.GMacUtils;
using IronyGrammars.Semantic.Expression.Value;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class EHipEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryEHip;


        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1, ValuePrimitive<MathematicaScalar> value2)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                SymbolicUtils.Constants.Zero
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1, GMacValueMultivector value2)
        {
            return GMacValueMultivector.Create(
                value1.ValueMultivectorType,
                value1.MultivectorCoefficients.EuclideanHip(value2.MultivectorCoefficients)
                );
        }

        //All other allowed combinations are handled using casting
    }
}