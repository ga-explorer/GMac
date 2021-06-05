using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class SpEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinarySp;


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
            var scalar = value1.SymbolicFrame.Sp[value1.SymbolicMultivector, value2.SymbolicMultivector];

            return ValuePrimitive<MathematicaScalar>.Create(
                value1.CoefficientType, 
                scalar[0].ToMathematicaScalar()
                );
        }

        //All other allowed combinations are handled using casting
    }
}
