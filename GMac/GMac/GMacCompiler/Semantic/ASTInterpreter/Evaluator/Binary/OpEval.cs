using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Products;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class OpEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryOp;


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
                value1.SymbolicMultivector.Op(value2.SymbolicMultivector)
                );
        }

        //All other allowed combinations are handled using casting
    }
}
