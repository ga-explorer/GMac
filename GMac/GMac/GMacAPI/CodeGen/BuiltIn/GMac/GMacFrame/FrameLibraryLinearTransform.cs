using System;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Structured;

namespace GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        private void GenerateApplyLinearTransformMacro(AstFrame frameInfo)
        {
            var inputsText = ComposeMacroInputs(
                "tr", DefaultStructure.LinearTransform,
                "mv", frameInfo.FrameMultivector.Name
                );

            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.AssignToLocalVariable("imageMv", "mv.#E0# * tr.ImageE0")
                    );

            for (var id = 1; id <= frameInfo.MaxBasisBladeId; id++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable("imageMv", "imageMv + mv.#E" + id + "# * tr.ImageE" + id)
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo, 
                DefaultMacro.LinearTransform.Apply, 
                inputsText, 
                commandsText, 
                "imageMv"
                );
        }

        private void GenerateTransposeLinearTransformMacro(AstFrame frameInfo)
        {
            var inputsText = ComposeMacroInputs(
                "tr", DefaultStructure.LinearTransform
                );

            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            var componentsText = new ListTextComposer("," + Environment.NewLine);

            for (var id1 = 0; id1 <= frameInfo.MaxBasisBladeId; id1++)
            {
                componentsText.Clear();

                commandsList.AddFixedCode("let newTr.ImageE" + id1 + " = Multivector(");

                for (var id2 = 0; id2 <= frameInfo.MaxBasisBladeId; id2++)
                    componentsText.Add("    #E" + id2 + "# = tr.ImageE" + id2 + ".#E" + id1 + "#");

                commandsList.AddFixedCode(componentsText.ToString());

                commandsList.AddFixedCode(")");
                commandsList.AddEmptyLine();
            }

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.LinearTransform.Transpose,
                inputsText,
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateComposeLinearTransformsMacro(AstFrame frameInfo)
        {
            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newTr.ImageE" + id,
                        DefaultMacro.LinearTransform.Apply + "(tr1, tr2.ImageE" + id + ")"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.LinearTransform.Compose,
                ComposeMacroInputs(
                    "tr1", DefaultStructure.LinearTransform,
                    "tr2", DefaultStructure.LinearTransform
                    ),
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateAddLinearTransformsMacro(AstFrame frameInfo)
        {
            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newTr.ImageE" + id,
                        "tr1.ImageE" + id + " + tr2.ImageE" + id
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.LinearTransform.Add,
                ComposeMacroInputs(
                    "tr1", DefaultStructure.LinearTransform,
                    "tr2", DefaultStructure.LinearTransform
                    ),
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateSubtractLinearTransformsMacro(AstFrame frameInfo)
        {
            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newTr.ImageE" + id,
                        "tr1.ImageE" + id + " - tr2.ImageE" + id
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.LinearTransform.Subtract,
                ComposeMacroInputs(
                    "tr1", DefaultStructure.LinearTransform,
                    "tr2", DefaultStructure.LinearTransform
                    ),
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateTimesWithScalarLinearTransformsMacro(AstFrame frameInfo)
        {
            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newTr.ImageE" + id,
                        "tr.ImageE" + id + " * s"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.LinearTransform.TimesWithScalar,
                ComposeMacroInputs(
                    "tr", DefaultStructure.LinearTransform,
                    "s", GMacLanguage.ScalarTypeName
                    ),
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateDivideByScalarLinearTransformsMacro(AstFrame frameInfo)
        {
            var commandsList = 
                GMacDslSyntaxFactory.SyntaxElementsList(
                    GMacDslSyntaxFactory.DeclareLocalVariable(DefaultStructure.LinearTransform, "newTr"),
                    GMacDslSyntaxFactory.EmptyLine()
                    );

            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                commandsList.Add(
                    GMacDslSyntaxFactory.AssignToLocalVariable(
                        "newTr.ImageE" + id,
                        "tr.ImageE" + id + " / s"
                        )
                    );

            commandsList.AddEmptyLine();

            var commandsText = GMacLanguage.CodeGenerator.GenerateCode(commandsList);

            GenerateMacro(
                frameInfo,
                DefaultMacro.LinearTransform.DivideByScalar,
                ComposeMacroInputs(
                    "tr", DefaultStructure.LinearTransform,
                    "s", GMacLanguage.ScalarTypeName
                    ),
                DefaultStructure.LinearTransform,
                commandsText,
                "newTr"
                );
        }

        private void GenerateLinearTransformMacros(AstFrame frameInfo)
        {
            GenerateApplyLinearTransformMacro(frameInfo);

            GenerateTransposeLinearTransformMacro(frameInfo);

            GenerateComposeLinearTransformsMacro(frameInfo);

            GenerateAddLinearTransformsMacro(frameInfo);

            GenerateSubtractLinearTransformsMacro(frameInfo);

            GenerateTimesWithScalarLinearTransformsMacro(frameInfo);

            GenerateDivideByScalarLinearTransformsMacro(frameInfo);
        }
    }
}
