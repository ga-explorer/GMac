using System;
using DataStructuresLib;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Maps;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using TextComposerLib.Text.Markdown;
using TextComposerLib.Text.Markdown.Tables;

namespace GMacBenchmarks.Samples.Computations.Symbolic
{
    /// <summary>
    /// Construct several kinds of multiplication tables for some frames and display their 
    /// sparsity and sizes
    /// </summary>
    public sealed class Sample2 : IGMacSample
    {
        private const string LongFmt = "###,###,###,###,###";


        public string Title 
            => "Lookup Tables of Main Products";

        public string Description 
            => "Lookup Tables of Main Products";


        public int MinVSpaceDimension { get; set; } = 3;

        public int MaxVSpaceDimension { get; set; } = 10;

        private GaSymFrame Frame { get; set; }

        private MarkdownTable MdSparsityTable { get; set; }

        private MarkdownTable MdSizeTable { get; set; }


        private Tuple<long, long, long> GpHashTableSize()
        {
            var table = Frame.Gp.ToHashMap();
            var tableSize = table.SizeInBytes();
            var mvCount = table.TargetMultivectorsCount;
            var termsCount = table.TargetMultivectorTermsCount;

            return new Tuple<long, long, long>(
                tableSize,
                mvCount,
                termsCount
            );
        }

        private long GpTreeTableSize()
        {
            return Frame.Gp.ToTreeMap().SizeInBytes();
        }

        private long GpArrayTableSize()
        {
            return Frame.Gp.ToArrayMap().SizeInBytes();
        }

        private long GpCoefSumsTableSize()
        {
            return Frame.Gp.ToCoefSumsMap().SizeInBytes();
        }


        private Tuple<long, long, long> SpSparseTableSize()
        {
            var table = Frame.Sp.ToHashMap(); 
            var tableSize = table.SizeInBytes();
            var mvCount = table.TargetMultivectorsCount;
            var termsCount = table.TargetMultivectorTermsCount;

            return new Tuple<long, long, long>(
                tableSize,
                mvCount,
                termsCount
            );
        }

        private long SpTreeTableSize()
        {
            return Frame.Sp.ToTreeMap().SizeInBytes();
        }

        private long SpArrayTableSize()
        {
            return Frame.Sp.ToArrayMap().SizeInBytes();
        }

        private long SpCoefSumsTableSize()
        {
            return Frame.Sp.ToCoefSumsMap().SizeInBytes();
        }


        private Tuple<long, long, long> LcpHashTableSize()
        {
            var table = Frame.Lcp.ToHashMap();
            var tableSize = table.SizeInBytes();
            var mvCount = table.TargetMultivectorsCount;
            var termsCount = table.TargetMultivectorTermsCount;

            return new Tuple<long, long, long>(
                tableSize,
                mvCount,
                termsCount
            );
        }

        private long LcpTreeTableSize()
        {
            return Frame.Lcp.ToTreeMap().SizeInBytes();
        }

        private long LcpArrayTableSize()
        {
            return Frame.Lcp.ToArrayMap().SizeInBytes();
        }

        private long LcpCoefSumsTableSize()
        {
            return Frame.Lcp.ToCoefSumsMap().SizeInBytes();
        }


        private Tuple<long, long, long> OpSparseTableSize()
        {
            var table = GaSymMapBilinearHash.CreateFromOuterProduct(Frame);
            var tableSize = table.SizeInBytes();
            var mvCount = table.TargetMultivectorsCount;
            var termsCount = table.TargetMultivectorTermsCount;

            return new Tuple<long, long, long>(
                tableSize,
                mvCount,
                termsCount
            );
        }

        private long OpTreeTableSize()
        {
            return GaSymMapBilinearTree.CreateFromOuterProduct(Frame).SizeInBytes();
        }

        private long OpArrayTableSize()
        {
            return GaSymMapBilinearArray.CreateFromOuterProduct(Frame).SizeInBytes();
        }

        private long OpCombinationsTableSize()
        {
            return GaSymMapBilinearCoefSums.CreateFromOuterProduct(Frame).SizeInBytes();
        }


        private void EuclideanTables()
        {
            MdSparsityTable = new MarkdownTable();
            var firstColumn = MdSparsityTable.AddColumn("op", MarkdownTableColumnAlignment.Left);

            firstColumn.Add("Geometric Product - Multivectors");
            firstColumn.Add("Geometric Product - Terms");

            firstColumn.Add("Scalar Product - Multivectors");
            firstColumn.Add("Scalar Product - Terms");

            firstColumn.Add("Left Contraction Product - Multivectors");
            firstColumn.Add("Left Contraction Product - Terms");

            firstColumn.Add("Outer Product - Multivectors");
            firstColumn.Add("Outer Product - Terms");


            MdSizeTable = new MarkdownTable();
            firstColumn = MdSizeTable.AddColumn("op", MarkdownTableColumnAlignment.Left);

            firstColumn.Add("Geometric Product - Hash");
            firstColumn.Add("Geometric Product - Tree");
            firstColumn.Add("Geometric Product - Array");
            firstColumn.Add("Geometric Product - Combinations");

            firstColumn.Add("Scalar Product - Hash");
            firstColumn.Add("Scalar Product - Tree");
            firstColumn.Add("Scalar Product - Array");
            firstColumn.Add("Scalar Product - Combinations");

            firstColumn.Add("Left Contraction Product - Hash");
            firstColumn.Add("Left Contraction Product - Tree");
            firstColumn.Add("Left Contraction Product - Array");
            firstColumn.Add("Left Contraction Product - Combinations");

            firstColumn.Add("Outer Product - Hash");
            firstColumn.Add("Outer Product - Tree");
            firstColumn.Add("Outer Product - Array");
            firstColumn.Add("Outer Product - Combinations");


            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                Console.Out.WriteLine("Euclidean Frame " + n);
                Frame = GaSymFrame.CreateEuclidean(n);

                var sparsityColumn = MdSparsityTable.AddColumn(
                    "n" + n,
                    MarkdownTableColumnAlignment.Right,
                    n.ToString()
                );

                var sizeColumn = MdSizeTable.AddColumn(
                    "n" + n,
                    MarkdownTableColumnAlignment.Right,
                    n.ToString()
                );

                var t = GpHashTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(GpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(GpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(GpCoefSumsTableSize().ToString(LongFmt));

                t = SpSparseTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(SpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(SpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(SpCoefSumsTableSize().ToString(LongFmt));

                t = LcpHashTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(LcpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(LcpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(LcpCoefSumsTableSize().ToString(LongFmt));

                t = OpSparseTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(OpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(OpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(OpCombinationsTableSize().ToString(LongFmt));
            }
        }

        private void ConformalTables()
        {
            MdSparsityTable = new MarkdownTable();
            var firstColumn = MdSparsityTable.AddColumn("op", MarkdownTableColumnAlignment.Left);
            firstColumn.Add("Geometric Product - Multivectors");
            firstColumn.Add("Geometric Product - Terms");

            firstColumn.Add("Scalar Product - Multivectors");
            firstColumn.Add("Scalar Product - Terms");

            firstColumn.Add("Left Contraction Product - Multivectors");
            firstColumn.Add("Left Contraction Product - Terms");


            MdSizeTable = new MarkdownTable();
            firstColumn = MdSizeTable.AddColumn("op", MarkdownTableColumnAlignment.Left);
            firstColumn.Add("Geometric Product - Hash");
            firstColumn.Add("Geometric Product - Tree");
            firstColumn.Add("Geometric Product - Array");
            firstColumn.Add("Geometric Product - Combinations");

            firstColumn.Add("Scalar Product - Hash");
            firstColumn.Add("Scalar Product - Tree");
            firstColumn.Add("Scalar Product - Array");
            firstColumn.Add("Scalar Product - Combinations");

            firstColumn.Add("Left Contraction Product - Hash");
            firstColumn.Add("Left Contraction Product - Tree");
            firstColumn.Add("Left Contraction Product - Array");
            firstColumn.Add("Left Contraction Product - Combinations");


            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                Console.Out.WriteLine("Conformal Frame " + n);
                Frame = GaSymFrame.CreateConformal(n);

                var sparsityColumn = MdSparsityTable.AddColumn(
                    "n" + n,
                    MarkdownTableColumnAlignment.Right,
                    n.ToString()
                );

                var sizeColumn = MdSizeTable.AddColumn(
                    "n" + n,
                    MarkdownTableColumnAlignment.Right,
                    n.ToString()
                );

                var t = GpHashTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(GpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(GpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(GpCoefSumsTableSize().ToString(LongFmt));

                t = SpSparseTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(SpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(SpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(SpCoefSumsTableSize().ToString(LongFmt));

                t = LcpHashTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(LcpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(LcpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(LcpCoefSumsTableSize().ToString(LongFmt));
            }
        }

        private void ProjectiveTables()
        {
            MdSparsityTable = new MarkdownTable();
            var firstColumn = MdSparsityTable.AddColumn("op", MarkdownTableColumnAlignment.Left);
            firstColumn.Add("Geometric Product - Multivectors");
            firstColumn.Add("Geometric Product - Terms");

            firstColumn.Add("Scalar Product - Multivectors");
            firstColumn.Add("Scalar Product - Terms");

            firstColumn.Add("Left Contraction Product - Multivectors");
            firstColumn.Add("Left Contraction Product - Terms");


            MdSizeTable = new MarkdownTable();
            firstColumn = MdSizeTable.AddColumn("op", MarkdownTableColumnAlignment.Left);
            firstColumn.Add("Geometric Product - Hash");
            firstColumn.Add("Geometric Product - Tree");
            firstColumn.Add("Geometric Product - Array");
            firstColumn.Add("Geometric Product - Combinations");

            firstColumn.Add("Scalar Product - Hash");
            firstColumn.Add("Scalar Product - Tree");
            firstColumn.Add("Scalar Product - Array");
            firstColumn.Add("Scalar Product - Combinations");

            firstColumn.Add("Left Contraction Product - Hash");
            firstColumn.Add("Left Contraction Product - Tree");
            firstColumn.Add("Left Contraction Product - Array");
            firstColumn.Add("Left Contraction Product - Combinations");

            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                Console.Out.WriteLine("Projective Frame " + n);
                Frame = GaSymFrame.CreateProjective(n);

                var sparsityColumn = MdSparsityTable.AddColumn(
                    "n" + n,
                    MarkdownTableColumnAlignment.Right,
                    n.ToString()
                );

                var sizeColumn = MdSizeTable.AddColumn(
                    "n" + n,
                    MarkdownTableColumnAlignment.Right,
                    n.ToString()
                );

                var t = GpHashTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(GpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(GpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(GpCoefSumsTableSize().ToString(LongFmt));

                t = SpSparseTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(SpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(SpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(SpCoefSumsTableSize().ToString(LongFmt));

                t = LcpHashTableSize();
                sparsityColumn.Add(t.Item2.ToString(LongFmt));
                sparsityColumn.Add(t.Item3.ToString(LongFmt));
                sizeColumn.Add(t.Item1.ToString(LongFmt));
                sizeColumn.Add(LcpTreeTableSize().ToString(LongFmt));
                sizeColumn.Add(LcpArrayTableSize().ToString(LongFmt));
                sizeColumn.Add(LcpCoefSumsTableSize().ToString(LongFmt));
            }
        }

        public string Execute()
        {
            var mdComposer = new MarkdownComposer();

            mdComposer
                .AppendHeader(Title);


            EuclideanTables();

            mdComposer
                .AppendHeader("1. Euclidean Geometric Algebras", 2);

            mdComposer
                .AppendHeader("1.1 Sparsity of Tables:", 3)
                .Append(MdSparsityTable.ToString())
                .AppendLine();

            mdComposer
                .AppendHeader("1.2 Size of Tables:", 3)
                .Append(MdSizeTable.ToString())
                .AppendLine();


            ConformalTables();

            mdComposer
                .AppendHeader("2. Conformal Geometric Algebras", 2);

            mdComposer
                .AppendHeader("2.1 Sparsity of Tables:", 3)
                .Append(MdSparsityTable.ToString())
                .AppendLine();

            mdComposer
                .AppendHeader("2.2 Size of Tables:", 3)
                .Append(MdSizeTable.ToString())
                .AppendLine();


            ProjectiveTables();

            mdComposer
                .AppendHeader("3. Projective Geometric Algebras", 2);

            mdComposer
                .AppendHeader("3.1 Sparsity of Tables:", 3)
                .Append(MdSparsityTable.ToString())
                .AppendLine();

            mdComposer
                .AppendHeader("3.2 Size of Tables:", 3)
                .Append(MdSizeTable.ToString())
                .AppendLine();

            return mdComposer.ToString();
        }
    }
}