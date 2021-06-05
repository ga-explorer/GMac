using System;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class EqualsMethodsFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal EqualsMethodsFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateEqualsFunction(ulong kvSpaceDim)
        {
            var caseTemplate = Templates["equals_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            for (var i = 0UL; i < kvSpaceDim; i++)
                casesText.Add(caseTemplate, "num", i);

            TextComposer.AppendAtNewLine(
                Templates["equals"],
                "num", kvSpaceDim,
                "double", GMacLanguage.ScalarTypeName,
                "cases", casesText
                );
        }

        private void GenerateMainEqualsFunction()
        {
            var caseTemplate = Templates["main_equals_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in CurrentFrame.Grades())
                casesText.Add(caseTemplate,
                    "grade", grade,
                    "num", CurrentFrame.KvSpaceDimension(grade)
                    );

            TextComposer.AppendAtNewLine(
                Templates["main_equals"],
                "frame", CurrentFrameName,
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
                GenerateEqualsFunction(kvSpaceDim);

            GenerateMainEqualsFunction();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }

    }
}
