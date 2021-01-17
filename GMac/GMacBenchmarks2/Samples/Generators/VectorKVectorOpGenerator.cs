using DataStructuresLib;
using GeometricAlgebraStructuresLib.Frames;
using GMacBenchmarks2.Samples.Computations;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks2.Samples.Generators
{
    public sealed class VectorKVectorOpGenerator : IGMacSample
    {
        private readonly LinearTextComposer _codeComposer 
            = new LinearTextComposer();

        public string Title { get; } 
            = "Generate Outer Product C# code of vector and k-vector";

        public string Description { get; }
            = "Generate Outer Product C# code of vector and k-vector";

        public int MinVSpaceDimension { get; set; } = 2;

        public int MaxVSpaceDimension { get; set; } = 12;


        private void GenerateForDimension(int vSpaceDim)
        {
            _codeComposer
                .AppendLine()
                .AppendLine($"if (vSpaceDim == {vSpaceDim})")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("if (grade == 0)")
                .IncreaseIndentation()
                .AppendLine("return vector;")
                .DecreaseIndentation();

            for (var grade = 1; grade < vSpaceDim; grade++)
            {
                _codeComposer
                    .AppendLine()
                    .AppendLine($"if (grade == {grade})")
                    .IncreaseIndentation()
                    .AppendLine($"return VectorKVectorOp_{vSpaceDim}_{grade}(vector, kVector);")
                    .DecreaseIndentation();
            }

            _codeComposer
                .DecreaseIndentation()
                .AppendLine("}");
        }

        private void GenerateForGrade(int vSpaceDim, int grade)
        {
            var resultArrayLength = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade + 1);
            
            _codeComposer
                .AppendAtNewLine("private static GaNumKVector VectorKVectorOp_")
                .Append(vSpaceDim)
                .Append("_")
                .Append(grade)
                .AppendLine("(GaNumKVector vector, GaNumKVector kVector)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("var vectorArray = vector.ScalarValuesArray;")
                .AppendLine("var kVectorArray = kVector.ScalarValuesArray;")
                .AppendLine($"var resultArray = new double[{resultArrayLength}];")
                .AppendLine()
                .Append("var ");

            var resultIdsList = GaFrameUtils.BasisBladeIDsOfGrade(vSpaceDim, grade + 1);
            foreach (var id in resultIdsList)
            {
                var index = id.BasisBladeIndex();
                _codeComposer.AppendLine("value = 0.0d;");

                var indexList1 = id.PatternToPositions();
                foreach (var index1 in indexList1)
                {
                    var id1 = 1 << index1;
                    var id2 = id ^ id1;
                    var index2 = id2.BasisBladeIndex();

                    var sign = GaFrameUtils.IsNegativeEGp(id1, id2) 
                        ? "-" : "+";

                    _codeComposer
                        .AppendLineAtNewLine($"value {sign}= vectorArray[{index1}] * kVectorArray[{index2}];");
                }

                _codeComposer
                    .AppendLineAtNewLine($"resultArray[{index}] = value;")
                    .AppendLine();
            }

            _codeComposer
                .AppendLine($"return GaNumKVector.Create({vSpaceDim.ToGaSpaceDimension()}, {grade + 1}, resultArray);")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();
        }

        public string Execute()
        {
            _codeComposer.Clear();

            _codeComposer
                .AppendLine("public static GaNumKVector VectorKVectorOp(this GaNumKVector vector, GaNumKVector kVector)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine("if (vector.GaSpaceDimension != kVector.GaSpaceDimension)")
                .IncreaseIndentation()
                .AppendLine("throw new GaNumericsException(\"Multivector size mismatch\");")
                .DecreaseIndentation()
                .AppendLine()
                .AppendLine("if (vector.Grade != 1)")
                .IncreaseIndentation()
                .AppendLine("throw new InvalidOperationException();")
                .DecreaseIndentation()
                .AppendLine()
                .AppendLine("var vSpaceDim = kVector.VSpaceDimension;")
                .AppendLine("var grade = kVector.Grade;");
            
            for (var vSpaceDim = MinVSpaceDimension; vSpaceDim <= MaxVSpaceDimension; vSpaceDim++)
                GenerateForDimension(vSpaceDim);

            _codeComposer
                .AppendLine()
                .AppendLine("return VectorKVectorOp_All(vector, kVector);")
                .DecreaseIndentation()
                .AppendLine("}")
                .AppendLine();

            for (var vSpaceDim = MinVSpaceDimension; vSpaceDim <= MaxVSpaceDimension; vSpaceDim++)
            for (var grade = 1; grade < vSpaceDim; grade++)
                GenerateForGrade(vSpaceDim, grade);

            return _codeComposer.ToString();
        }
    }
}
