using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.API.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateEuclideanBinaryMacros(AstFrame frameInfo)
        {
            var inputsText = ComposeMacroInputs(
                "mv", frameInfo.FrameMultivector.Name,
                "s", GMacLanguage.ScalarTypeName
                );

            GenerateMacro(frameInfo, DefaultMacro.EuclideanBinary.TimesWithScalar, inputsText, "mv * s");

            GenerateMacro(frameInfo, DefaultMacro.EuclideanBinary.DivideByScalar, inputsText, "mv / s");

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.OuterProduct, GMacOpInfo.BinaryOp);

            GenerateBinaryScalarMacro(frameInfo, DefaultMacro.EuclideanBinary.ScalarProduct, GMacOpInfo.BinaryESp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.GeometricProduct, GMacOpInfo.BinaryEGp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.LeftContractionProduct, GMacOpInfo.BinaryELcp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.RightContractionProduct, GMacOpInfo.BinaryERcp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.FatDotProduct, GMacOpInfo.BinaryEFdp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.HestenesInnerProduct, GMacOpInfo.BinaryEHip);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.CommutatorProduct, GMacOpInfo.BinaryECp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.AntiCommutatorProduct, GMacOpInfo.BinaryEAcp);

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.GeometricProductDual, DefaultMacro.EuclideanUnary.Dual + "(mv1 egp mv2)");

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.DirectSandwitchProduct, "mv1 egp mv2 egp reverse(mv1)");

            GenerateBinaryMacro(frameInfo, DefaultMacro.EuclideanBinary.GradeInvolutionSandwitchProduct, "mv1 egp grade_inv(mv2) egp reverse(mv1)");
        }
    }
}
