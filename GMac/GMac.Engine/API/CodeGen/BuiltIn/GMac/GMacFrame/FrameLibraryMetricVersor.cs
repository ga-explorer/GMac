using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.ASTConstants;

namespace GMac.Engine.API.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateMetricVersorToOutermorphismMacro(AstFrame frameInfo)
        {
            var commandsList = GMacLanguage.SyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine(),
                GMacDslSyntaxFactory.AssignToLocalVariable("vi", DefaultMacro.MetricVersor.Inverse + "(v)"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
            {
                //var id = GaUtils.BasisVector_Index_To_ID(index - 1);

                commandsList.AddRange(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "v gp " + frameInfo.BasisVectorFromIndex((ulong)index - 1).AccessName + " gp vi"
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
                DefaultMacro.MetricVersor.ToOutermorphism,
                ComposeMacroInputs("v", frameInfo.FrameMultivector.Name),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateMetricVersorToLinearTransformMacro(AstFrame frameInfo)
        {
            GenerateMacro(
                frameInfo,
                DefaultMacro.MetricVersor.ToLinearTransform,
                ComposeMacroInputs("v", frameInfo.FrameMultivector.Name),
                DefaultStructure.LinearTransform,
                "",
                DefaultMacro.Outermorphism.ToLinearTransform + "(" + DefaultMacro.MetricVersor.ToOutermorphism + "(v))"
                );
        }

        private void GenerateMetricVersorMacros(AstFrame frameInfo)
        {
            GenerateMetricVersorToOutermorphismMacro(frameInfo);

            GenerateMetricVersorToLinearTransformMacro(frameInfo);

            GenerateApplyVersorMacro(frameInfo, DefaultMacro.MetricVersor.Apply, "(v gp mv gp reverse(v)) / norm2(v)");

            GenerateApplyVersorMacro(frameInfo, DefaultMacro.MetricVersor.ApplyRotor, "v gp mv gp reverse(v)");

            GenerateApplyVersorMacro(frameInfo, DefaultMacro.MetricVersor.ApplyReflector, "(-1 * v) gp mv gp reverse(v)");
        }
    }
}
