using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.API.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateMetricUnaryMacros(AstFrame frameInfo)
        {
            GenerateUnaryScalarMacro(frameInfo, DefaultMacro.MetricUnary.Magnitude, GMacOpInfo.UnaryMagnitude);

            GenerateUnaryScalarMacro(frameInfo, DefaultMacro.MetricUnary.MagnitudeSquared, GMacOpInfo.UnaryMagnitude2);

            GenerateUnaryScalarMacro(frameInfo, DefaultMacro.MetricUnary.NormSquared, GMacOpInfo.UnaryNorm2);

            GenerateUnaryMacro(frameInfo, DefaultMacro.MetricVersor.Inverse, "reverse(mv) / norm2(mv)");

            GenerateUnaryMacro(frameInfo, DefaultMacro.MetricUnary.Dual, "mv lcp " + DefaultMacro.MetricVersor.Inverse + "(I)");

            GenerateUnaryMacro(frameInfo, DefaultMacro.MetricUnary.SelfGeometricProduct, "mv gp mv");

            GenerateUnaryMacro(frameInfo, DefaultMacro.MetricUnary.SelfGeometricProductReverse, "mv gp reverse(mv)");
        }
    }
}
