using DataStructuresLib;
using GeometricAlgebraStructuresLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;

namespace GMac.Benchmarks.Samples.Generators
{
    public sealed class VectorKVectorOpIndexTablesGenerator : IGMacSample
    {
        private const string TemplateText = @"
namespace GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp
{
    public static partial class GaNumVectorKVectorOpUtils
    {
        internal static readonly int[][][][] VectorKVectorOpIndexTablesArray =
        {
            #Arrays#
        };
    }
}";


        public string Title { get; } 
            = "Generate Index Lookup Tables C# code for the Outer Product of a vector and a k-vector";

        public string Description { get; }
            = "Generate Index Lookup Tables C# code for the Outer Product of a vector and a k-vector";

        public int MinVSpaceDimension { get; set; } = 2;

        public int MaxVSpaceDimension { get; set; } = 16;

        
        private string GenerateArraysText(int vSpaceDim, int grade)
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLineAtNewLine($"new[] //n = {vSpaceDim}, k = {grade}")
                .AppendLine("{")
                .IncreaseIndentation();

            var resultIdsList = 
                GaFrameUtils.BasisBladeIDsOfGrade(vSpaceDim, grade + 1);

            var firstIdFlag = true;
            foreach (var id in resultIdsList)
            {
                if (firstIdFlag)
                    firstIdFlag = false;
                else
                    composer.Append(",");

                composer.AppendAtNewLine("new[] {");

                var firstIndexFlag = true;
                var indexList1 = id.PatternToPositions();
                foreach (var index1 in indexList1)
                {
                    var id1 = 1UL << index1;
                    var id2 = id ^ id1;
                    var index2 = id2.BasisBladeIndex();

                    if (firstIndexFlag)
                        firstIndexFlag = false;
                    else
                        composer.Append(", ");

                    composer
                        .Append($"{index1}, {index2}");
                }

                composer.Append("}");
            }

            composer
                .DecreaseIndentation()
                .AppendAtNewLine("}");

            return composer.ToString();
        }

        private string GenerateArraysText()
        {
            var composer = new LinearTextComposer();

            var firstSpaceFlag = true;
            for (var vSpaceDim = MinVSpaceDimension; vSpaceDim <= MaxVSpaceDimension; vSpaceDim++)
            {
                if (firstSpaceFlag)
                    firstSpaceFlag = false;
                else
                    composer.AppendLine(",");

                composer
                    .AppendLineAtNewLine($"new[] //n = {vSpaceDim}")
                    .AppendLine("{")
                    .IncreaseIndentation();

                var firstGradeFlag = true;
                for (var grade = 1; grade < vSpaceDim; grade++)
                {
                    if (firstGradeFlag)
                        firstGradeFlag = false;
                    else
                        composer.AppendLine(",");

                    composer.Append(GenerateArraysText(vSpaceDim, grade));
                }

                composer
                    .DecreaseIndentation()
                    .AppendAtNewLine("}");
            }

            return composer.ToString();
        }

        public string Execute()
        {
            var template = 
                new ParametricTextComposer("#", "#", TemplateText);

            var arraysText = GenerateArraysText();

            return template.GenerateUsing(arraysText);
        }
    }
}