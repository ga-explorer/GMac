using System;
using GeometricAlgebraStructuresLib.Frames;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class BilinearProductMainMethodFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal string OperatorName { get; }

        internal string ZeroCondition { get; }

        internal Func<int, int, int> GetFinalGrade { get; }

        internal Func<int, int, bool> IsLegalGrade { get; }


        internal BilinearProductMainMethodFileGenerator(CodeLibraryComposer libGen, string opName, string zeroCondition, Func<int, int, int> getFinalGrade, Func<int, int, bool> isLegalGrade)
            : base(libGen)
        {
            OperatorName = opName;

            ZeroCondition = zeroCondition;

            GetFinalGrade = getFinalGrade;

            IsLegalGrade = isLegalGrade;
        }


        public string GetCasesText()
        {
            var t2 = Templates["bilinearproduct_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade1 in CurrentFrame.Grades())
                foreach (var grade2 in CurrentFrame.Grades())
                {
                    if (IsLegalGrade(grade1, grade2) == false)
                        continue;

                    var grade = GetFinalGrade(grade1, grade2);

                    var id = grade1 + grade2 * CurrentFrame.GradesCount;

                    var name = BladesLibraryGenerator.GetBinaryFunctionName(OperatorName, grade1, grade2, grade);

                    casesText.Add(t2,
                        "name", name,
                        "id", id,
                        "g1", grade1,
                        "g2", grade2,
                        "grade", grade,
                        "frame", CurrentFrameName
                        );
                }

            return casesText.ToString();
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var casesText = GetCasesText();

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct_main"],
                "name", OperatorName,
                "frame", CurrentFrameName,
                "zerocond", ZeroCondition,
                "cases", casesText
                );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
