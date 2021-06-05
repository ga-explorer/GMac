using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.Evaluator.Unary
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
