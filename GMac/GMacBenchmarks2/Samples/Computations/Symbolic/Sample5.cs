using GeometricAlgebraSymbolicsLib.Frames;
using TextComposerLib.Text.Markdown;

namespace GMacBenchmarks2.Samples.Computations.Symbolic
{
    /// <summary>
    /// Compare operations on old sparse multivector vs new tree multivector representetions
    /// </summary>
    public sealed class Sample5 : IGMacSample
    {
        public string Title 
            => "Multivector Representations Comparisons";

        public string Description
            => "Multivector Representations Comparisons";


        public MarkdownComposer LogComposer { get; }
            = new MarkdownComposer();

        public GaSymFrame Frame { get; set; }


        public Sample5()
        {
            Frame = GaSymFrame.CreateEuclidean(3);
        }


        public string Execute()
        {
            LogComposer.Clear();

            //for (var vSpaceDim = 3; vSpaceDim <= 3; vSpaceDim++)
            //{
            //    Frame = GaSymFrame.CreateEuclidean(vSpaceDim);

            //    //var randGen = new GMacRandomGenerator(10);
            //    //var mvA = randGen.GetSymbolicMultivector(Frame.GaSpaceDimension, "A");
            //    //var mvB = randGen.GetSymbolicMultivector(Frame.GaSpaceDimension, "B");
            //    var mvA = GaSymMultivector.CreateSymbolic(Frame.GaSpaceDimension, "A");
            //    var mvB = GaSymMultivector.CreateSymbolic(Frame.GaSpaceDimension, "B");

            //    var op1 = Frame.Rcp[mvA, mvB];
            //    var mv1 = mvA.ToMultivector();
            //    var mv2 = mvB.ToMultivector();

            //    var op2 = Frame.Rcp[mv1, mv2];
            //    //var diff = op1 - op2.ToHashMultivector();

            //    LogComposer
            //        .AppendHeader("Euclidean " + vSpaceDim, 2);

            //    LogComposer
            //        .AppendAtNewLine("Sparse A = ")
            //        .AppendLine(mvA.ToString());

            //    LogComposer
            //        .AppendAtNewLine("Sparse B = ")
            //        .AppendLine(mvB.ToString())
            //        .AppendLine();

            //    LogComposer
            //        .AppendAtNewLine("Tree A = ")
            //        .AppendLine(mv1.ToString());

            //    LogComposer
            //        .AppendAtNewLine("Tree B = ")
            //        .AppendLine(mv2.ToString())
            //        .AppendLine();

            //    LogComposer
            //        .AppendAtNewLine("Sparse A op B = ")
            //        .AppendLine(op1.ToString())
            //        .AppendLine();

            //    LogComposer
            //        .AppendAtNewLine("Tree A op B = ")
            //        .AppendLine(op2.ToString())
            //        .AppendLine();

            //    //LogComposer
            //    //    .AppendAtNewLine("Diff = ")
            //    //    .AppendLine(diff.ToString())
            //    //    .AppendLine();
            //}

            return LogComposer.ToString();
        }
    }
}
