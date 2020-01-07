using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Unary
{
    public sealed class UnaryPlusEval : GMacBasicUnaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.UnaryPlus;


        public ILanguageValue Evaluate(ValuePrimitive<int> value1)
        {
            return value1;
        }

        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1)
        {
            return value1;
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1)
        {
            return value1;
        }
    }
}
