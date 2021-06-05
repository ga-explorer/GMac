using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class CpEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryCp;


        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1, ValuePrimitive<MathematicaScalar> value2)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                value1.Value * value2.Value
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1, GMacValueMultivector value2)
        {
            return GMacValueMultivector.Create(
                value1.ValueMultivectorType,
                value1.SymbolicFrame.Cp[value1.SymbolicMultivector, value2.SymbolicMultivector]
                );
        }

        //All other allowed combinations are handled using casting
    }
}
