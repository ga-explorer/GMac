using System;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors;
using JetBrains.Profiler.Windows.Api;
using TextComposerLib.Text.Markdown;
using TextComposerLib.Text.Markdown.Tables;
using UtilLib;

namespace GMacTests.Numeric
{
    /// <summary>
    /// Construct several kinds of multiplication tables for some frames and display their 
    /// sparsity and sizes
    /// </summary>
    public sealed class GMacNumericTest2 : IGMacTest
    {
        private const string LongFmt = "###,###,###,###,###";


        public string Title { get; } = "Lookup Tables of Main Products";

        public int MinVSpaceDimension { get; set; } = 9;

        public int MaxVSpaceDimension { get; set; } = 9;

        private GaNumFrame Frame { get; set; }

        private MarkdownTable MdSparsityTable { get; set; }

        private MarkdownTable MdSizeTable { get; set; }


        private Tuple<long, long, long> GpHashTableSize()
        {
            // Enable collecting memory allocation data.
            // Check IsActive only once. Do nothing
            // (do not throw exceptions, etc.) if IsActive is false
            if (MemoryProfiler.IsActive && MemoryProfiler.CanControlAllocations)
                MemoryProfiler.EnableAllocations();

            // Here goes your code to profile
            var table = Frame.Gp.ToHashMap();

            MemoryProfiler.Dump();

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
            var table = Frame.Gp.ToTreeMap();

            return table.SizeInBytes();
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
            var table = Frame.Lcp.ToTreeMap();

            Console.Out.WriteLine("Zero Multivectors Count: " + table.Multivectors.Count(mv => mv.IsNullOrZero()));

            return table.SizeInBytes();
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
            var table = GaNumMapBilinearHash.CreateFromOuterProduct(Frame);
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
            return GaNumMapBilinearTree.CreateFromOuterProduct(Frame).SizeInBytes();
        }

        private long OpArrayTableSize()
        {
            return GaNumMapBilinearArray.CreateFromOuterProduct(Frame).SizeInBytes();
        }

        private long OpCombinationsTableSize()
        {
            return GaNumMapBilinearCoefSums.CreateFromOuterProduct(Frame).SizeInBytes();
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
            firstColumn.Add("Geometric Product - CoefSums");

            firstColumn.Add("Scalar Product - Hash");
            firstColumn.Add("Scalar Product - Tree");
            firstColumn.Add("Scalar Product - Array");
            firstColumn.Add("Scalar Product - CoefSums");

            firstColumn.Add("Left Contraction Product - Hash");
            firstColumn.Add("Left Contraction Product - Tree");
            firstColumn.Add("Left Contraction Product - Array");
            firstColumn.Add("Left Contraction Product - CoefSums");

            firstColumn.Add("Outer Product - Hash");
            firstColumn.Add("Outer Product - Tree");
            firstColumn.Add("Outer Product - Array");
            firstColumn.Add("Outer Product - CoefSums");


            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                Console.Out.WriteLine("Euclidean Frame " + n);
                Frame = GaNumFrame.CreateEuclidean(n);

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
            firstColumn.Add("Geometric Product - CoefSums");

            firstColumn.Add("Scalar Product - Hash");
            firstColumn.Add("Scalar Product - Tree");
            firstColumn.Add("Scalar Product - Array");
            firstColumn.Add("Scalar Product - CoefSums");

            firstColumn.Add("Left Contraction Product - Hash");
            firstColumn.Add("Left Contraction Product - Tree");
            firstColumn.Add("Left Contraction Product - Array");
            firstColumn.Add("Left Contraction Product - CoefSums");


            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                Console.Out.WriteLine("Conformal Frame " + n);
                Frame = GaNumFrame.CreateConformal(n);

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
            firstColumn.Add("Geometric Product - CoefSums");

            firstColumn.Add("Scalar Product - Hash");
            firstColumn.Add("Scalar Product - Tree");
            firstColumn.Add("Scalar Product - Array");
            firstColumn.Add("Scalar Product - CoefSums");

            firstColumn.Add("Left Contraction Product - Hash");
            firstColumn.Add("Left Contraction Product - Tree");
            firstColumn.Add("Left Contraction Product - Array");
            firstColumn.Add("Left Contraction Product - CoefSums");

            for (var n = MinVSpaceDimension; n <= MaxVSpaceDimension; n++)
            {
                Console.Out.WriteLine("Projective Frame " + n);
                Frame = GaNumFrame.CreateProjective(n);

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
