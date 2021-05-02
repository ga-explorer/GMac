using System;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class InvolutionMethodsFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal InvolutionMethodsFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateNegativeFunction(ulong kvSpaceDim)
        {
            var caseTemplate = Templates["negative_case"];

            var casesText = new ListTextComposer("," + Environment.NewLine);

            for (var i = 0UL; i < kvSpaceDim; i++)
                casesText.Add(caseTemplate, "num", i);

            TextComposer.AppendAtNewLine(
                Templates["negative"],
                "num", kvSpaceDim,
                "double", GMacLanguage.ScalarTypeName,
                "cases", casesText
                );
        }

        private void GenerateMainInvolutionFunction(string macroName, Func<int, bool> useNegative)
        {
            var caseTemplate1 = Templates["main_negative_case"];
            var caseTemplate2 = Templates["main_negative_case2"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in CurrentFrame.Grades())
                if (useNegative(grade))
                    casesText.Add(caseTemplate1,
                        "frame", CurrentFrameName,
                        "grade", grade,
                        "num", CurrentFrame.KvSpaceDimension(grade)
                        );
                else
                    casesText.Add(caseTemplate2,
                        "grade", grade
                        );

            TextComposer.AppendAtNewLine(
                Templates["main_involution"],
                "frame", CurrentFrameName,
                "name", macroName,
                "cases", casesText
                );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var kvSpaceDimList =
                Enumerable
                .Range(0, CurrentFrame.VSpaceDimension)
                .Select(grade => CurrentFrame.KvSpaceDimension(grade))
                .Distinct();

            foreach (var kvSpaceDim in kvSpaceDimList)
                GenerateNegativeFunction(kvSpaceDim);

            GenerateMainInvolutionFunction(DefaultMacro.EuclideanUnary.Negative, grade => true);

            GenerateMainInvolutionFunction(DefaultMacro.EuclideanUnary.Reverse, GaFrameUtils.GradeHasNegativeReverse);

            GenerateMainInvolutionFunction(DefaultMacro.EuclideanUnary.GradeInvolution, GaFrameUtils.GradeHasNegativeGradeInv);

            GenerateMainInvolutionFunction(DefaultMacro.EuclideanUnary.CliffordConjugate, GaFrameUtils.GradeHasNegativeCliffConj);

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
