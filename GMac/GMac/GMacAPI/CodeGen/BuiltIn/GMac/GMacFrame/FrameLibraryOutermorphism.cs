using System;
using GeometricAlgebraStructuresLib.Frames;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Structured;

namespace GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateOutermorphismToLinearTransformMacro(AstFrame frameInfo)
        {
            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                GMacDslSyntaxFactory.EmptyLine(),
                GMacDslSyntaxFactory.FixedCode("let newTr.ImageE0 = Multivector(#E0# = '1')"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
            {
                var id = GaFrameUtils.BasisVectorId((ulong)index - 1);

                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newTr.ImageE" + id,
                        "om.ImageV" + index + ".@G1@"
                        )
                    );
            }

            commandsList.AddEmptyLine();

            for (var grade = 2; grade <= frameInfo.VSpaceDimension; grade++)
            {
                var kvSpaceDim = frameInfo.KvSpaceDimension(grade);

                for (var index = 0UL; index < kvSpaceDim; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);

                    id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                    commandsList.Add(
                        GMacDslSyntaxFactory.AssignToLocalVariable(
                            "newTr.ImageE" + id,
                            " newTr.ImageE" + id1 + " op newTr.ImageE" + id2
                            )
                        );
                }

                commandsList.AddEmptyLine();
            }

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.ToLinearTransform,
                ComposeMacroInputs("om", DefaultStructure.Outermorphism),
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateApplyOutermorphismMacro(AstFrame frameInfo)
        {
            var inputsText = ComposeMacroInputs(
                "om", DefaultStructure.Outermorphism,
                "mv", frameInfo.FrameMultivector.Name
                );

            const string exprText = 
                DefaultMacro.LinearTransform.Apply + 
                "(" + 
                DefaultMacro.Outermorphism.ToLinearTransform + 
                "(om), mv)";

            GenerateMacro(
                frameInfo, 
                DefaultMacro.Outermorphism.Apply, 
                inputsText, 
                "",
                exprText
                );
        }

        private void GenerateApplyOutermorphismToVectorMacro(AstFrame frameInfo)
        {
            var inputsText = ComposeMacroInputs(
                "om", DefaultStructure.Outermorphism,
                "mv", frameInfo.FrameMultivector.Name
                );

            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.AssignToLocalVariable("imageMv", "mv.#E1# * om.ImageV1.@G1@")
                );

            for (var index = 2; index <= frameInfo.VSpaceDimension; index++)
            {
                var id = GaFrameUtils.BasisVectorId((ulong)index - 1);

                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "imageMv",
                        "imageMv + mv.#E" + id + "# * om.ImageV" + index + ".@G1@"
                        )
                    );
            }

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.ApplyToVector,
                inputsText,
                commandsText,
                "imageMv"
                );
        }

        private void GenerateTransposeOutermorphismMacro(AstFrame frameInfo)
        {
            var inputsText = ComposeMacroInputs(
                "om", DefaultStructure.Outermorphism
                );

            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            var componentsText = new ListTextComposer("," + Environment.NewLine);

            for (var index1 = 1; index1 <= frameInfo.VSpaceDimension; index1++)
            {
                componentsText.Clear();

                var id1 = GaFrameUtils.BasisBladeId(1, (ulong)index1 - 1);

                commandsList.AddFixedCode("let newOm.ImageV" + index1 + " = Multivector(");

                for (var index2 = 1; index2 <= frameInfo.VSpaceDimension; index2++)
                {
                    var id2 = GaFrameUtils.BasisBladeId(1, (ulong)index2 - 1);

                    componentsText.Add("    #E" + id2 + "# = om.ImageV" + index2 + ".#E" + id1 + "#");
                }

                commandsList.AddFixedCode(componentsText.ToString());

                commandsList.AddFixedCode(")");
                commandsList.AddEmptyLine();
            }

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.Transpose,
                inputsText,
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateOutermorphismDeterminantMacros(AstFrame frameInfo)
        {
            GenerateComment("Compute matric determinant of an outer-morphism");

            var pseudoScalar = frameInfo.Name + ".E" + frameInfo.MaxBasisBladeId;

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.MetricDeterminant,
                ComposeMacroInputs("om", DefaultStructure.Outermorphism),
                GMacLanguage.ScalarTypeName,
                "",
                string.Format("ApplyOM(om, {0}) sp reverse({0}) / norm2({0})", pseudoScalar)
                );

            GenerateComment("Compute Euclidean determinant of an outer-morphism");

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.EuclideanDeterminant,
                ComposeMacroInputs("om", DefaultStructure.Outermorphism),
                GMacLanguage.ScalarTypeName,
                "",
                string.Format("ApplyOM(om, {0}) esp reverse({0}) / emag2({0})", pseudoScalar)
                );
        }

        private void GenerateComposeOutermorphismsMacro(AstFrame frameInfo)
        {
            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        DefaultMacro.Outermorphism.ApplyToVector + "(om1, om2.ImageV" + index + ")"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.Compose,
                ComposeMacroInputs(
                    "om1", DefaultStructure.Outermorphism,
                    "om2", DefaultStructure.Outermorphism
                    ),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateAddOutermorphismsMacro(AstFrame frameInfo)
        {
            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "om1.ImageV" + index + ".@G1@ + om2.ImageV" + index + ".@G1@"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.Add,
                ComposeMacroInputs(
                    "om1", DefaultStructure.Outermorphism,
                    "om2", DefaultStructure.Outermorphism
                    ),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateSubtractOutermorphismsMacro(AstFrame frameInfo)
        {
            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "om1.ImageV" + index + ".@G1@ - om2.ImageV" + index + ".@G1@"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.Subtract,
                ComposeMacroInputs(
                    "om1", DefaultStructure.Outermorphism,
                    "om2", DefaultStructure.Outermorphism
                    ),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateTimesWithScalarOutermorphismsMacro(AstFrame frameInfo)
        {
            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "om.ImageV" + index + ".@G1@ * s"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.TimesWithScalar,
                ComposeMacroInputs(
                    "om", DefaultStructure.Outermorphism,
                    "s", GMacLanguage.ScalarTypeName
                    ),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateDivideByScalarOutermorphismsMacro(AstFrame frameInfo)
        {
            var commandsList = GMacDslSyntaxFactory.SyntaxElementsList(
                GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.Outermorphism, "newOm"),
                GMacDslSyntaxFactory.EmptyLine()
                );

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newOm.ImageV" + index,
                        "om.ImageV" + index + ".@G1@ / s"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.Outermorphism.DivideByScalar,
                ComposeMacroInputs(
                    "om", DefaultStructure.Outermorphism,
                    "s", GMacLanguage.ScalarTypeName
                    ),
                DefaultStructure.Outermorphism,
                commandsText,
                "newOm"
                );
        }

        private void GenerateOutermorphismMacros(AstFrame frameInfo)
        {
            GenerateOutermorphismToLinearTransformMacro(frameInfo);

            GenerateApplyOutermorphismMacro(frameInfo);

            GenerateApplyOutermorphismToVectorMacro(frameInfo);

            GenerateTransposeOutermorphismMacro(frameInfo);

            GenerateOutermorphismDeterminantMacros(frameInfo);

            GenerateComposeOutermorphismsMacro(frameInfo);

            GenerateAddOutermorphismsMacro(frameInfo);

            GenerateSubtractOutermorphismsMacro(frameInfo);

            GenerateTimesWithScalarOutermorphismsMacro(frameInfo);

            GenerateDivideByScalarOutermorphismsMacro(frameInfo);
        }
    }
}
