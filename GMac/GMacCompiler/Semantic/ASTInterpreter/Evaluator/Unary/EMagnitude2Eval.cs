using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacUtils;
using IronyGrammars.Semantic.Expression.Value;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Unary
{
    public sealed class EMagnitude2Eval : GMacBasicUnaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.UnaryEuclideanMagnitude2;


        public ILanguageValue Evaluate(ValuePrimitive<int> value1)
        {
            return ValuePrimitive<int>.Create(
                value1.ValuePrimitiveType,
                value1.Value * value1.Value
                );
        }

        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                value1.Value * value1.Value
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.GMacRootAst.ScalarType,
                value1.MultivectorCoefficients.EuclideanMagnitude2()
                );
        }
    }
}