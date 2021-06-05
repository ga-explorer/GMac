using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.API.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateMetricBinaryMacros(AstFrame frameInfo)
        {
            GenerateBinaryScalarMacro(frameInfo, DefaultMacro.MetricBinary.ScalarProduct, GMacOpInfo.BinarySp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.GeometricProduct, GMacOpInfo.BinaryGp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.LeftContractionProduct, GMacOpInfo.BinaryLcp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.RightContractionProduct, GMacOpInfo.BinaryRcp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.FatDotProduct, GMacOpInfo.BinaryFdp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.HestenesInnerProduct, GMacOpInfo.BinaryHip);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.CommutatorProduct, GMacOpInfo.BinaryCp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.AntiCommutatorProduct, GMacOpInfo.BinaryAcp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.GeometricProductDual, DefaultMacro.MetricUnary.Dual + "(mv1 gp mv2)");

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.DirectSandwitchProduct, "mv1 gp mv2 gp reverse(mv1)");

            GenerateBinaryMacro(frameInfo, DefaultMacro.MetricBinary.GradeInvolutionSandwitchProduct, "mv1 gp grade_inv(mv2) gp reverse(mv1)");
        }
    }
}
