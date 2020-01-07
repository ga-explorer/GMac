using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Unary
{
    public sealed class NegativeEval : GMacBasicUnaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.UnaryMinus;


        public ILanguageValue Evaluate(ValuePrimitive<int> value1)
        {
            return ValuePrimitive<int>.Create(
                value1.ValuePrimitiveType,
                -value1.Value
                );
        }

        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                -value1.Value
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1)
        {
            return GMacValueMultivector.Create(
                value1.ValueMultivectorType,
                -value1.SymbolicMultivector
                );
        }
    }
}
