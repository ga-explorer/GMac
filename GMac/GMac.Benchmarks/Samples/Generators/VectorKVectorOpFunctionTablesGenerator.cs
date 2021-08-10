using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraStructuresLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;

namespace GMac.Benchmarks.Samples.Generators
{
    public sealed class VectorKVectorOpFunctionTablesGenerator : IGMacSample
    {
        private const string TemplateText = @"
using System;

namespace GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp
{
    public static partial class GaNumVectorKVectorOpUtils
    {
        private static Func<double[], double[], double[]>[][] VectorKVectorOpFunctionTablesArray { get; }


        static GaNumVectorKVectorOpUtils()
        {
            VectorKVectorOpFunctionTablesArray = new[]
            {
                #FunctionTablesArrayItems#
            };
        }

        
        #Functions#
    }
}";


        public string Title { get; } 
            = "Generate Function Tables C# code for the Outer Product of a vector and a k-vector";

        public string Description { get; }
            = "Generate Function Tables C# code for the Outer Product of a vector and a k-vector";

        public int MinVSpaceDimension { get; set; } = 2;

        public int MaxVSpaceDimension { get; set; } = 16;


        private string GenerateFunctionComputationsText(int vSpaceDim, int grade)
        {
            var composer = new LinearTextComposer();

            var outputIdsList = GaFrameUtils.BasisBladeIDsOfGrade(vSpaceDim, grade + 1);

            var firstIdFlag = true;
            foreach (var id in outputIdsList)
            {
                if (firstIdFlag)
                    firstIdFlag = false;
                else
                    composer.AppendLine(",");

                var index = id.BasisBladeIndex();

                var indexList1 = id.PatternToPositions();

                var firstIndexFlag = true;
                foreach (var index1 in indexList1)
                {
                    var id1 = 1UL << index1;
                    var id2 = id ^ id1;
                    var index2 = id2.BasisBladeIndex();

                    var sign = GaFrameUtils.IsNegativeEGp(id1, id2);

                    if (firstIndexFlag)
                    {
                        firstIndexFlag = false;
                        composer.Append(sign ? "-" : "");
                    }
                    else
                        composer.Append(sign ? " - " : " + ");

                    composer
                        .Append($"a[{index1}] * b[{index2}]");
                }
            }

            return composer.ToString();
        }

        private string GenerateFunctionTablesArrayItemsText()
        {
            var composer = new LinearTextComposer();

            var firstItemFlag = true;
            for (var vSpaceDim = MinVSpaceDimension; vSpaceDim <= MaxVSpaceDimension; vSpaceDim++)
            {
                if (firstItemFlag)
                    firstItemFlag = false;
                else
                    composer.AppendLine(",");

                composer.AppendAtNewLine("new Func<double[], double[], double[]>[]{ ");

                var firstGradeFlag = true;
                for (var grade = 1; grade < vSpaceDim; grade++)
                {
                    if (firstGradeFlag)
                        firstGradeFlag = false;
                    else
                        composer.Append(", ");

                    composer.Append($"Op_{vSpaceDim}_{grade}");
                }

                composer.Append(" }");
            }

            return composer.ToString();
        }

        private string GenerateFunctionsText()
        {
            var composer = new LinearTextComposer();

            var firstFunctionFlag = true;
            for (var vSpaceDim = MinVSpaceDimension; vSpaceDim <= MaxVSpaceDimension; vSpaceDim++)
            {
                for (var grade = 1; grade < vSpaceDim; grade++)
                {
                    if (firstFunctionFlag)
                        firstFunctionFlag = false;
                    else
                        composer.AppendLineAtNewLine();

                    composer
                        .AppendAtNewLine($"private static double[] Op_{vSpaceDim}_{grade}(double[] a, double[] b)")
                        .IncreaseIndentation()
                        .AppendAtNewLine("=> new[]")
                        .AppendAtNewLine("{")
                        .IncreaseIndentation()
                        .AppendLine()
                        .Append(GenerateFunctionComputationsText(vSpaceDim, grade))
                        .DecreaseIndentation()
                        .AppendAtNewLine("};")
                        .DecreaseIndentation();
                }
            }

            return composer.ToString();
        }

        public string Execute()
        {
            var template = new ParametricTextComposer("#", "#", TemplateText);

            var functionTablesArrayItemsText = 
                GenerateFunctionTablesArrayItemsText();

            var functionsText =
                GenerateFunctionsText();

            return template.GenerateUsing(
                functionTablesArrayItemsText,
                functionsText
            );
        }
    }
}