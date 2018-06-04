using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Maps;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Tree;
using TextComposerLib.Text.Markdown;

namespace GMacTests.Symbolic
{
    /// <summary>
    /// Construct the main multiplication tables for a given frame
    /// </summary>
    public sealed class GMacSymbolicTest4 : IGMacTest
    {
        public string Title { get; } = "";

        public MarkdownComposer LogComposer { get; }
            = new MarkdownComposer();

        public GaSymFrame Frame { get; set; }


        public GMacSymbolicTest4()
        {
            Frame = GaSymFrame.CreateEuclidean(3);
        }


        public string Execute()
        {
            LogComposer.Clear();

            //var randGen = new GMacRandomGenerator(10);
            //var mvA = randGen.GetSymbolicMultivector(Frame.GaSpaceDimension, "A");
            //var mvB = randGen.GetSymbolicMultivector(Frame.GaSpaceDimension, "B");
            var mvA = GaSymMultivector.CreateSymbolic(Frame.GaSpaceDimension, "A");
            var mvB = GaSymMultivector.CreateSymbolic(Frame.GaSpaceDimension, "B");

            LogComposer
                .AppendAtNewLine("A = ")
                .AppendLine(mvA.ToString());

            LogComposer
                .AppendAtNewLine("B = ")
                .AppendLine(mvB.ToString())
                .AppendLine();

            var coefSumsTable = GaSymMapBilinearCoefSums.CreateFromOuterProduct(Frame);
            LogComposer
                .AppendAtNewLine("A op B = ")
                .AppendLine(coefSumsTable[mvA, mvB].ToString())
                .AppendLine()
                .AppendLineAtNewLine("A op B Coef Sums:")
                .AppendLine(coefSumsTable.ToMarkdownTable());

            var mv1 = mvA.ToTreeMultivector();
            var mv2 = mvB.ToTreeMultivector();
            var opMv = mv1.Op(mv2);
            LogComposer
                .AppendAtNewLine("Tree: A = ")
                .AppendLine(mv1.ToTreeString())
                .AppendAtNewLine("Tree: B = ")
                .AppendLine(mv2.ToTreeString())
                .AppendAtNewLine("Tree: A op B = ")
                .AppendLine(opMv.ToTreeString())
                .AppendLine();

            coefSumsTable = Frame.Gp.ToCoefSumsMap();
            LogComposer
                .AppendAtNewLine("A gp B = ")
                .AppendLine(coefSumsTable[mvA, mvB].ToString())
                .AppendLine()
                .AppendLineAtNewLine("A gp B Coef Sums:")
                .AppendLine(coefSumsTable.ToMarkdownTable());

            var gpMv = mv1.EGp(mv2);
            LogComposer
                .AppendAtNewLine("Tree: A = ")
                .AppendLine(mv1.ToTreeString())
                .AppendAtNewLine("Tree: B = ")
                .AppendLine(mv2.ToTreeString())
                .AppendAtNewLine("Tree: A gp B = ")
                .AppendLine(gpMv.ToTreeString())
                .AppendLine();

            coefSumsTable = Frame.Sp.ToCoefSumsMap();
            LogComposer
                .AppendAtNewLine("A sp B = ")
                .AppendLine(coefSumsTable[mvA, mvB].ToString())
                .AppendLine()
                .AppendLineAtNewLine("A sp B Coef Sums:")
                .AppendLine(coefSumsTable.ToMarkdownTable());

            coefSumsTable = Frame.Lcp.ToCoefSumsMap();
            LogComposer
                .AppendAtNewLine("A lcp B = ")
                .AppendLine(coefSumsTable[mvA, mvB].ToString())
                .AppendLine()
                .AppendLineAtNewLine("A lcp B Coef Sums:")
                .AppendLine(coefSumsTable.ToMarkdownTable());

            return LogComposer.ToString();
        }
    }
}
