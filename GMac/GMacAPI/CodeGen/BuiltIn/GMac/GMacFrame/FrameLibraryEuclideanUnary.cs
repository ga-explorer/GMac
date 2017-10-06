using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateEuclideanUnaryMacros(AstFrame frameInfo)
        {
            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.Negative, "-mv");

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.Reverse, GMacOpInfo.UnaryReverse);

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.GradeInvolution, GMacOpInfo.UnaryGradeInvolution);

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.CliffordConjugate, GMacOpInfo.UnaryCliffordConjugate);

            GenerateUnaryScalarMacro(frameInfo, DefaultMacro.EuclideanUnary.Magnitude, GMacOpInfo.UnaryEuclideanMagnitude);

            GenerateUnaryScalarMacro(frameInfo, DefaultMacro.EuclideanUnary.MagnitudeSquared, GMacOpInfo.UnaryEuclideanMagnitude2);

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanVersor.Inverse, "reverse(mv) / emag2(mv)");

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.Dual, "mv elcp " + DefaultMacro.EuclideanVersor.Inverse + "(I)");

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.SelfGeometricProduct, "mv egp mv");

            GenerateUnaryMacro(frameInfo, DefaultMacro.EuclideanUnary.SelfGeometricProductReverse, "mv egp reverse(mv)");
        }
    }
}
