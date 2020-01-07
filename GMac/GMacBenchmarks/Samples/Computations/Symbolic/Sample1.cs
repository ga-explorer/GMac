using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Hash;
using TextComposerLib.Text.Markdown;
using TextComposerLib.Text.Markdown.Tables;

namespace GMacBenchmarks.Samples.Computations.Symbolic
{
    /// <summary>
    /// Construct multivectors of several kinds and display their sizes
    /// </summary>
    public sealed class Sample1 : IGMacSample
    {
        private const string LongFmt = "###,###,###,###,###.00000";
        //private const string LongFmt = "G";

        internal class MultivectorSizeInfo
        {
            public long TermsCount { get; }

            public long ScalarsSize { get; }

            public long NewTreeSize { get; }

            public long HashSize { get; }

            public long TreeSize { get; }

            public long ArraySize { get; }

            public long NewTreeOverhead => NewTreeSize - ScalarsSize;

            public long HashOverhead => HashSize - ScalarsSize;

            public long TreeOverhead => TreeSize - ScalarsSize;

            public long ArrayOverhead => ArraySize - ScalarsSize;

            public double NewTreeOverheadPerTerm => (NewTreeSize - ScalarsSize) / (double)TermsCount;

            public double HashOverheadPerTerm => (HashSize - ScalarsSize) / (double)TermsCount;

            public double TreeOverheadPerTerm => (TreeSize - ScalarsSize) / (double)TermsCount;

            public double ArrayOverheadPerTerm => (ArraySize - ScalarsSize) / (double)TermsCount;


            public MultivectorSizeInfo(int gaSpaceDim, IEnumerable<int> idsList)
            {
                //Construct a multivector and set all its selected terms to the same symbolic scalar
                var mv = GaSymMultivectorHash.CreateZero(gaSpaceDim);
                foreach (var id in idsList)
                    mv.Add(id, GaSymbolicsUtils.Constants.One);

                TermsCount = mv.Count;

                //The overhead size is equal to the total size of the multivector minus the single symbolic scalar size
                ScalarsSize = GaSymbolicsUtils.Constants.One.SizeInBytes();

                HashSize = mv.SizeInBytes();

                NewTreeSize = mv.ToMultivector().SizeInBytes();

                TreeSize = mv.ToTreeMultivector().SizeInBytes();

                ArraySize = mv.TermsToArray().SizeInBytes();
            }
        }


        public string Title 
            => "Multivector Sizes";

        public string Description 
            => "Multivector Sizes";


        public int MinVSpaceDimension { get; set; } = 2;

        public int MaxVSpaceDimension { get; set; } = 11;


        private static MarkdownTable CreateTable(int n)
        {
            var gaSpaceDim = n.ToGaSpaceDimension();
            var idsList = GaNumFrameUtils.BasisBladeIDs(n).ToArray();

            var mdOverheadTable = new MarkdownTable();
            var firstColumn = mdOverheadTable.AddColumn("op", MarkdownTableColumnAlignment.Left);
            firstColumn.AddRange(
                idsList
                .Skip(1)
                .Select(termsCount => termsCount + " Terms")
                );

            var newTreeOverheadColumn = mdOverheadTable.AddColumn(
                "New Tree",
                MarkdownTableColumnAlignment.Right,
                "New Binary Tree"
            );

            var hashOverheadColumn = mdOverheadTable.AddColumn(
                "Hash",
                MarkdownTableColumnAlignment.Right,
                "Hash Table"
            );

            var treeOverheadColumn = mdOverheadTable.AddColumn(
                "Tree",
                MarkdownTableColumnAlignment.Right,
                "Binary Tree"
            );

            var arrayOverheadColumn = mdOverheadTable.AddColumn(
                "Array",
                MarkdownTableColumnAlignment.Right,
                "Array"
            );

            for (var termsCount = 1; termsCount < gaSpaceDim; termsCount++)
            {
                var sizeInfo = new MultivectorSizeInfo(
                    gaSpaceDim,
                    idsList.Take(termsCount)
                );

                newTreeOverheadColumn.Add(sizeInfo.NewTreeOverheadPerTerm.ToString(LongFmt)); 
                hashOverheadColumn.Add(sizeInfo.HashOverheadPerTerm.ToString(LongFmt));
                treeOverheadColumn.Add(sizeInfo.TreeOverheadPerTerm.ToString(LongFmt));
                arrayOverheadColumn.Add(sizeInfo.ArrayOverheadPerTerm.ToString(LongFmt));
            }

            return mdOverheadTable;
        }


        public string Execute()
        {
            var mdComposer = new MarkdownComposer();

            mdComposer
                .AppendHeader(Title);

            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                mdComposer
                    .AppendHeader("Space Dimension: " + n, 2)
                    .AppendLineAtNewLine(CreateTable(n));
            }

            return mdComposer.ToString();
        }
    }
}
