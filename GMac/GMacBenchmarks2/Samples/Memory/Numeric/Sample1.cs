using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GMacBenchmarks2.Samples.Computations;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks2.Samples.Memory.Numeric
{
    public sealed class Sample1 : IGMacSample
    {
        public string Title { get; }
            = "Memory requirements of multivectors and linear maps";

        public string Description { get; }
            = "Memory requirements of multivectors and linear maps";


        public string Execute()
        {
            var randGen = new GaRandomGenerator(10);
            var composer = new LinearTextComposer();

            composer.AppendLine("Term Sizes:");

            for (var i = 3; i <= 15; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, gaSpaceDim).Select(
                        id =>
                        {
                            var mv = randGen.GetNumTerm(gaSpaceDim, id);
                            var termSize = mv.GetInternalTermsTree().SizeInBytes();

                            return termSize;
                        }
                    ).Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("k-Vector Sizes:");

            for (var i = 3; i <= 15; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, i + 1).Select(
                        grade =>
                        {
                            var mv = randGen.GetNumKVector(gaSpaceDim, grade);
                            var kVectorSize = mv.GetInternalTermsTree().SizeInBytes();

                            return kVectorSize;// / (double)mv.TermsCount;
                        }
                    ).Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("Full Multivector Sizes:");

            for (var i = 3; i <= 15; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();
                var mv = randGen.GetNumMultivectorFull(gaSpaceDim);
                var mvSize = mv.GetInternalTermsTree().SizeInBytes();

                composer.AppendLine(mvSize);// / (double)mv.TermsCount);
            }

            //composer.AppendLine("Outermorphism Sizes:");
            //for (var i = 3; i <= 15; i++)
            //{
            //    var matrix = DenseMatrix.Create(i, i, 0);
            //    for (var row = 0; row < i; row++)
            //    for (var col = 0; col < i; col++)
            //        matrix[row, col] = randGen.GetScalar(-10, 10);

            //    //var om = GaNumMixedOutermorphism.Create(matrix);
            //    //var om = GaNumMapUnilinearKVectorsArray.CreateOutermorphism(matrix);
            //    var om = GaNumOutermorphism.Create(matrix);

            //    composer.AppendLine(om.SizeInBytes());
            //}

            return composer.ToString();
        }
    }
}
