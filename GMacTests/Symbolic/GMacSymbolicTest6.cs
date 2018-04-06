using System.Collections.Generic;
using GMac.GMacMath;
using GMac.GMacMath.Symbolic;
using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Symbolic.Multivectors;
using TextComposerLib.Text.Markdown;

namespace GMacTests.Symbolic
{
    /// <summary>
    /// Measure the number of symbolic computations and time used for multiplying some multivectors
    /// </summary>
    public sealed class GMacSymbolicTest6 : IGMacTest
    {
        public string Title { get; } = "";

        public MarkdownComposer LogComposer { get; }
            = new MarkdownComposer();

        public GaSymFrame Frame { get; set; }


        public GMacSymbolicTest6()
        {
            Frame = GaSymFrame.CreateConformal(5);
        }


        public string Execute()
        {
            LogComposer.Clear();

            var randGen = new GMacRandomGenerator(10);
            var mvList = new List<GaSymMultivector>(10);
            

            for (var i = 0; i < 10; i++)
                mvList.Add(randGen.GetSymMultivector(Frame.GaSpaceDimension));

            //var mvA = GaMultivector.CreateSymbolic(Frame.GaSpaceDimension, "A");
            //var mvB = GaMultivector.CreateSymbolic(Frame.GaSpaceDimension, "B");

            var cacheMisses1 = SymbolicUtils.Cas.Connection.CacheMisses;
            var elapsedTime1 = SymbolicUtils.Cas.Connection.Stopwatch.Elapsed;

            var resultList = new List<GaSymMultivector>(mvList.Count * mvList.Count);

            foreach (var mv1 in mvList)
                foreach (var mv2 in mvList)
                    resultList.Add(Frame.Gp[mv1, mv2]);

            var cacheMisses2 = SymbolicUtils.Cas.Connection.CacheMisses;
            var elapsedTime2 = SymbolicUtils.Cas.Connection.Stopwatch.Elapsed;

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
