using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GMacBenchmarks2.Samples.Computations;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks2.Samples.Memory.Numeric
{
    public sealed class Sample3 : IGMacSample
    {
        public string Title { get; }
            = "Memory requirements of multivectors";

        public string Description { get; }
            = "Memory requirements of multivectors";

        public string Execute1()
        {
            var randGen = new GaRandomGenerator(10);
            var composer = new LinearTextComposer();

            composer.AppendLine("Term Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, gaSpaceDim).Select(
                        id =>
                        {
                            var mv = randGen.GetNumTerm(gaSpaceDim, id);
                            var termSize = mv.ToGradedMultivector().SizeInBytes();

                            return termSize;
                        }
                    ).Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("Vector Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mv = randGen.GetNumKVector(gaSpaceDim, 1);
                var kVectorSize = mv.ToGradedMultivector().SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("2-Vector Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mv = randGen.GetNumKVector(gaSpaceDim, 2);
                var kVectorSize = mv.ToGradedMultivector().SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("k-Vector Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mvSize =
                    Enumerable.Range(0, i + 1).Select(
                        grade =>
                        {
                            var mv = randGen.GetNumKVector(gaSpaceDim, grade);
                            var kVectorSize = mv.ToGradedMultivector().SizeInBytes();

                            return kVectorSize;// / (double)mv.TermsCount;
                        }
                    ).Max();

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("Full Multivector Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();
                var mv = randGen.GetNumMultivectorFull(gaSpaceDim);
                var mvSize = mv.ToGradedMultivector().SizeInBytes();

                composer.AppendLine(mvSize);// / (double)mv.TermsCount);
            }
            
            return composer.ToString();
        }

        public string Execute2()
        {
            var randGen = new GaRandomGenerator(10);
            var composer = new LinearTextComposer();

            composer.AppendLine("Term Sizes:");

            for (var i = 3; i <= 16; i++)
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
                .AppendLine("Vector Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mv = randGen.GetNumKVector(gaSpaceDim, 1);
                var kVectorSize = mv.GetInternalTermsTree().SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("2-Vector Sizes:");

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();

                var mv = randGen.GetNumKVector(gaSpaceDim, 2);
                var kVectorSize = mv.GetInternalTermsTree().SizeInBytes();
                var mvSize = kVectorSize;// / (double)mv.TermsCount;

                composer.AppendLine(mvSize);
            }

            composer
                .AppendLine()
                .AppendLine("k-Vector Sizes:");

            for (var i = 3; i <= 16; i++)
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

            for (var i = 3; i <= 16; i++)
            {
                var gaSpaceDim = i.ToGaSpaceDimension();
                var mv = randGen.GetNumMultivectorFull(gaSpaceDim);
                var mvSize = mv.GetInternalTermsTree().SizeInBytes();

                composer.AppendLine(mvSize);// / (double)mv.TermsCount);
            }
            
            return composer.ToString();
        }

        public string Execute()
        {
            return Execute2();
        }
    }
}
