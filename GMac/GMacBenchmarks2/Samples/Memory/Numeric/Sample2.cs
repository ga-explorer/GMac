using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GMacBenchmarks2.Samples.Computations;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks2.Samples.Memory.Numeric
{
    public sealed class Sample2 : IGMacSample
    {
        public string Title { get; }
            = "Memory requirements of multivectors";

        public string Description { get; }
            = "Memory requirements of multivectors";

        public int VSpaceDimension { get; } 
            = 20;

        public string Execute1()
        {
            var randGen = new GaRandomGenerator(10);
            var composer = new LinearTextComposer();

            composer.AppendLine("Term Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, gaSpaceDim).Select(
                        id => randGen.GetNumTerm(id).CreateDgrMultivector(vSpaceDim).SizeInBytes()
                    ).Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("Vector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var mv = randGen.GetNumFullKVectorTerms(vSpaceDim, 1);
                var kVectorSize = mv.CreateDgrMultivector(vSpaceDim).SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("2-Vector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

                var mv = randGen.GetNumFullKVectorTerms(vSpaceDim, 2);
                var kVectorSize = mv.CreateDgrMultivector(vSpaceDim).SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("k-Vector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, vSpaceDim + 1).Select(
                        grade => randGen
                            .GetNumFullKVectorTerms(vSpaceDim, grade)
                            .CreateDgrMultivector(vSpaceDim)
                            .SizeInBytes()
                    )
                    .Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("Full Multivector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();
                var mv = randGen.GetNumFullMultivectorTerms(vSpaceDim).CreateSarMultivector(vSpaceDim);
                var mvSize = mv.GetDgrMultivector().SizeInBytes();

                composer.AppendLine(mvSize);// / (double)mv.TermsCount);
            }
            
            return composer.ToString();
        }

        public string Execute2()
        {
            var randGen = new GaRandomGenerator(10);
            var composer = new LinearTextComposer();

            //composer.AppendLine("Term Sizes:");

            //for (var i = 3; i <= VSpaceDimension; i++)
            //{
            //    var gaSpaceDim = i.ToGaSpaceDimension();

            //    var mvSize =
            //        Enumerable.Range(0, gaSpaceDim).Select(
            //            id =>
            //            {
            //                var mv = randGen.GetNumTerm(gaSpaceDim, id);
            //                var termSize = mv.GetInternalTermsTree().SizeInBytes();

            //                return termSize;
            //            }
            //        ).Max();

            //    composer.AppendLine(mvSize);
            //}

            composer
                .AppendLine()
                .AppendLine("Vector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

                var mv = randGen.GetNumFullKVectorTerms(vSpaceDim, 1).CreateSarMultivector(vSpaceDim);
                var kVectorSize = mv.GetBtrRootNode().SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("2-Vector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

                var mv = randGen.GetNumFullKVectorTerms(vSpaceDim, 2).CreateSarMultivector(vSpaceDim);
                var kVectorSize = mv.GetBtrRootNode().SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("k-Vector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, vSpaceDim + 1).Select(
                        grade => randGen
                            .GetNumFullKVectorTerms(vSpaceDim, grade)
                            .CreateBtr(vSpaceDim)
                            .SizeInBytes()
                    )
                    .Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("Full Multivector Sizes:");

            for (var vSpaceDim = 3; vSpaceDim <= VSpaceDimension; vSpaceDim++)
            {
                var mvSize = 
                    randGen
                        .GetNumFullMultivectorTerms(vSpaceDim)
                        .CreateSarMultivector(vSpaceDim)
                        .GetBtrRootNode()
                        .SizeInBytes();

                composer.AppendLine(mvSize);// / (double)mv.TermsCount);
            }
            
            return composer.ToString();
        }

        public string Execute()
        {
            return Execute1();
        }
    }
}
