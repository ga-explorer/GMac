using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Products;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;

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
                value1.SymbolicMultivector.EMagnitude2()
                );
        }
    }
}