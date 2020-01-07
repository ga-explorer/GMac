using System;
using GeometricAlgebraNumericsLib.Frames;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.OutermorphismClass
{
    internal class ClassFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal ClassFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        private string GenerateOutermorphismTranposeCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " + "Scalars".ScalarItem(j, i) + ";"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismDeterminantCode(string opName)
        {
            var macroGenerator = CreateMacroCodeGenerator(opName);

            macroGenerator.ActionSetMacroParametersBindings =
                macroBinding =>
                {
                    macroBinding.BindToVariables("result");

                    for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                    {
                        var id = CurrentFrame.BasisVectorId(i);

                        for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                        {
                            var valueAccessName = "om.ImageV" + (j + 1) + ".#E" + id + "#";

                            macroBinding.BindToVariables(valueAccessName);
                        }
                    }
                };

            macroGenerator.ActionSetTargetVariablesNames =
                targetNaming =>
                {
                    targetNaming.SetScalarParameter("result", "det");

                    for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                    {
                        var id = CurrentFrame.BasisVectorId(i);

                        for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                        {
                            var varName = "Scalars".ScalarItem(i, j);

                            var valueAccessName = "om.ImageV" + (j + 1) + ".#E" + id + "#";

                            targetNaming.SetScalarParameter(valueAccessName, varName);
                        }
                    }

                    BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
                };

            return macroGenerator.Generate();
        }

        private string GenerateOutermorphismPlusCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om1.Scalars".ScalarItem(i, j) + " + " +
                        "om2.Scalars".ScalarItem(i, j) + ";"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismSubtCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om1.Scalars".ScalarItem(i, j) + " - " +
                        "om2.Scalars".ScalarItem(i, j) + ";"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismComposeCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            var sumText = new ListTextComposer(" + ");

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                {
                    sumText.Clear();

                    for (var k = 0; k < CurrentFrame.VSpaceDimension; k++)
                        sumText.Add(
                            "om1.Scalars".ScalarItem(i, k) + " * " + "om2.Scalars".ScalarItem(k, j)
                            );

                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " + sumText + ";"
                        );
                }

            return codeText.ToString();
        }

        private string GenerateOutermorphismTimesCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om.Scalars".ScalarItem(i, j) + " * scalar;"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismDivideCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om.Scalars".ScalarItem(i, j) + " / scalar;"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismNegativesCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < CurrentFrame.VSpaceDimension; i++)
                for (var j = 0; j < CurrentFrame.VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "-om.Scalars".ScalarItem(i, j) + ";"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismApplyCasesCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var inGrade = 1; inGrade <= CurrentFrame.VSpaceDimension; inGrade++)
                codeText.Add(
                    Templates["om_apply_code_case"],
                    "grade", inGrade,
                    "frame", CurrentFrameName
                    );

            return codeText.ToString();
        }

        public override void Generate()
        {
            GenerateOutermorphismFileStartCode();

            TextComposer.Append(
                Templates["outermorphism"],
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName,
                "transpose_code", GenerateOutermorphismTranposeCode(),
                "metric_det_code", GenerateOutermorphismDeterminantCode(DefaultMacro.Outermorphism.MetricDeterminant),
                "euclidean_det_code", GenerateOutermorphismDeterminantCode(DefaultMacro.Outermorphism.EuclideanDeterminant),
                "plus_code", GenerateOutermorphismPlusCode(),
                "subt_code", GenerateOutermorphismSubtCode(),
                "compose_code", GenerateOutermorphismComposeCode(),
                "times_code", GenerateOutermorphismTimesCode(),
                "divide_code", GenerateOutermorphismDivideCode(),
                "negative_code", GenerateOutermorphismNegativesCode(),
                "apply_cases_code", GenerateOutermorphismApplyCasesCode()
                );

            GenerateOutermorphismFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
