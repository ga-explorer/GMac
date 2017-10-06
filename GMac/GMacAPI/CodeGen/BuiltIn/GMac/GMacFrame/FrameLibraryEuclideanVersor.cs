using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateEuclideanVersorToOutermorphismMacro(AstFrame frameInfo)
        {
            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                    GMacDslSyntaxFactory.EmptyLine(),
                    GMacDslSyntaxFactory.AssignToLocalVariable("vi", DefaultMacro.EuclideanVersor.Inverse + "(v)"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
            {
                //var id = GaUtils.BasisVector_Index_To_ID(index - 1);

                commandsList.AddRange(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "v egp " + frameInfo.BasisVectorFromIndex(index - 1).AccessName + " egp vi"
                        ),

                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "newOm.ImageV" + index + ".@G1@"
                        ),

                    GMacDslSyntaxFactory.EmptyLine()
                    );
            }

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.EuclideanVersor.ToOutermorphism,
                ComposeMacroInputs("v", frameInfo.FrameMultivector.Name),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateEuclideanVersorToLinearTransformMacro(AstFrame frameInfo)
        {
            GenerateMacro(
                frameInfo,
                DefaultMacro.EuclideanVersor.ToLinearTransform,
                ComposeMacroInputs("v", frameInfo.FrameMultivector.Name),
                DefaultStructure.LinearTransform,
                "",
                DefaultMacro.Outermorphism.ToLinearTransform + "(" + DefaultMacro.EuclideanVersor.ToOutermorphism + "(v))"
                );
        }

        private void GenerateEuclideanVersorMacros(AstFrame frameInfo)
        {
            GenerateEuclideanVersorToOutermorphismMacro(frameInfo);

            GenerateEuclideanVersorToLinearTransformMacro(frameInfo);

            GenerateApplyVersorMacro(frameInfo, DefaultMacro.EuclideanVersor.Apply, "(v egp mv egp reverse(v)) / emag2(v)");

            GenerateApplyVersorMacro(frameInfo, DefaultMacro.EuclideanVersor.ApplyRotor, "v egp mv egp reverse(v)");

            GenerateApplyVersorMacro(frameInfo, DefaultMacro.EuclideanVersor.ApplyReflector, "-v egp mv egp reverse(v)");
        }
    }
}
