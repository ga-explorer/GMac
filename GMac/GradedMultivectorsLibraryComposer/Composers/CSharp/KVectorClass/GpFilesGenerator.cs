using System;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using TextComposerLib.Text.Structured;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class GpFilesGenerator : CodeLibraryCodePartGenerator
    {
        internal string OperatorName { get; }

        internal bool DualFlag { get; }

        private GpMethodsFileGenerator _mainFileGenerator;


        internal GpFilesGenerator(CodeLibraryComposer libGen, string opName, bool dualFlag)
            : base(libGen)
        {
            OperatorName = opName;
            DualFlag = dualFlag;
        }


        private void GenerateMethods(int inGrade1, int inGrade2)
        {
            var gpCaseText = new ListTextComposer("," + Environment.NewLine);

            var gradesList =
                DualFlag
                ? Frame.GradesOfEGp(inGrade1, inGrade2)
                    .Select(grade => Frame.VSpaceDimension - grade)

                : Frame.GradesOfEGp(inGrade1, inGrade2);

            foreach (var outGrade in gradesList)
            {
                var funcName = BladesLibraryGenerator.GetBinaryFunctionName(OperatorName, inGrade1, inGrade2, outGrade);

                BladesLibraryGenerator.GenerateBilinearProductMethodFile(
                    OperatorName,
                    funcName,
                    inGrade1,
                    inGrade2,
                    outGrade
                    );

                gpCaseText.Add(Templates["gp_case"],
                    "frame", FrameTargetName,
                    "grade", outGrade,
                    "name", funcName
                    );
            }

            var name = BladesLibraryGenerator.GetBinaryFunctionName(OperatorName, inGrade1, inGrade2);

            _mainFileGenerator.GenerateIntermediateMethod(gpCaseText.ToString(), name);
        }

        internal void Generate()
        {
            BladesLibraryGenerator.CodeFilesComposer.InitalizeFile(OperatorName + ".cs");

            _mainFileGenerator = new GpMethodsFileGenerator(BladesLibraryGenerator, OperatorName);

            _mainFileGenerator.GenerateKVectorFileStartCode();

            BladesLibraryGenerator.CodeFilesComposer.UnselectActiveFile();

            foreach (var grade1 in Frame.Grades())
                foreach (var grade2 in Frame.Grades())
                    GenerateMethods(grade1, grade2);

            _mainFileGenerator.GenerateMainMethod();

            _mainFileGenerator.GenerateKVectorFileFinishCode();

            BladesLibraryGenerator.CodeFilesComposer.UnselectActiveFile();
        }
    }
}
