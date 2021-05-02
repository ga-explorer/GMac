using System.Collections.Generic;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using TextComposerLib.Text.Markdown;

namespace GMacBenchmarks2.Samples.Computations.Symbolic
{
    /// <summary>
    /// Measure the number of symbolic computations and time used for multiplying some multivectors
    /// </summary>
    public sealed class Sample6 : IGMacSample
    {
        public string Title 
            => "";
        
        public string Description 
            => "";


        public MarkdownComposer LogComposer { get; }
            = new MarkdownComposer();

        public GaSymFrame Frame { get; set; }


        public Sample6()
        {
            Frame = GaSymFrame.CreateConformal(5);
        }


        public string Execute()
        {
            LogComposer.Clear();

            var randGen = new GaRandomGenerator(10);
            var mvList = new List<GaSymMultivector>(10);
            

            for (var i = 0; i < 10; i++)
                mvList.Add(randGen.GetSymMultivector(Frame.VSpaceDimension));

            //var mvA = GaMultivector.CreateSymbolic(Frame.GaSpaceDimension, "A");
            //var mvB = GaMultivector.CreateSymbolic(Frame.GaSpaceDimension, "B");

            var cacheMisses1 = GaSymbolicsUtils.Cas.Connection.CacheMisses;
            var elapsedTime1 = GaSymbolicsUtils.Cas.Connection.Stopwatch.Elapsed;

            var resultList = new List<GaSymMultivector>(mvList.Count * mvList.Count);

            foreach (var mv1 in mvList)
                foreach (var mv2 in mvList)
                    resultList.Add(Frame.Gp[mv1, mv2]);

            var cacheMisses2 = GaSymbolicsUtils.Cas.Connection.CacheMisses;
            var elapsedTime2 = GaSymbolicsUtils.Cas.Connection.Stopwatch.Elapsed;

            //LogComposer
            //    .AppendAtNewLine("A = ")
            //    .AppendLine(mvA.ToString());

            //LogComposer
            //    .AppendAtNewLine("B = ")
            //    .AppendLine(mvB.ToString())
            //    .AppendLine();

            //LogComposer
            //    .AppendAtNewLine("A gp B = ")
            //    .AppendLine(gp.ToString())
            //    .AppendLine();

            LogComposer
                .AppendAtNewLine("Symbolic Computations Count: ")
                .AppendLine(cacheMisses2 - cacheMisses1)
                .AppendAtNewLine("Symbolic Computations Time: ")
                .AppendLine(elapsedTime2 - elapsedTime1);

            return LogComposer.ToString();
        }
    }
}
