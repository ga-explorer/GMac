using System;
using GeometricAlgebraStructuresLib.Frames;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class ApplyVersorMainMethodFileGenerator : CodeLibraryCodeFileGenerator 
    {
        internal string OperatorName { get; }


        internal ApplyVersorMainMethodFileGenerator(CodeLibraryComposer libGen, string opName)
            : base(libGen)
        {
            OperatorName = opName;
        }


        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var t2 = Templates["applyversor_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in CurrentFrame.Grades())
                foreach (var inGrade2 in CurrentFrame.Grades())
                {
                    var outGrade = inGrade2;

                    var id = inGrade1 + inGrade2 * CurrentFrame.GradesCount;

                    var name = BladesLibraryGenerator.GetBinaryFunctionName(OperatorName, inGrade1, inGrade2, outGrade);

                    casesText.Add(t2,
                        "name", name,
                        "id", id,
                        "g1", inGrade1,
                        "g2", inGrade2,
                        "grade", outGrade,
                        "frame", CurrentFrameName
                        );
                }

            TextComposer.AppendAtNewLine(
                Templates["applyversor_main"],
                "name", OperatorName,
                "frame", CurrentFrameName,
                "cases", casesText
                );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
