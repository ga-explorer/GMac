using CodeComposerLib.Irony.Semantic.Expression.Value;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Products;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator.Binary
{
    public sealed class EHipEval : GMacBasicBinaryEvaluator
    {
        public override GMacOpInfo OperatorInfo => GMacOpInfo.BinaryEHip;


        public ILanguageValue Evaluate(ValuePrimitive<MathematicaScalar> value1, ValuePrimitive<MathematicaScalar> value2)
        {
            return ValuePrimitive<MathematicaScalar>.Create(
                value1.ValuePrimitiveType,
                GaSymbolicsUtils.Constants.Zero
                );
        }

        public ILanguageValue Evaluate(GMacValueMultivector value1, GMacValueMultivector value2)
        {
            return GMacValueMultivector.Create(
                value1.ValueMultivectorType,
                value1.SymbolicMultivector.EHip(value2.SymbolicMultivector)
                );
        }

        //All other allowed combinations are handled using casting
    }
}