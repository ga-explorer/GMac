using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacUtils;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Tabular;
using UtilLib.SampleTasks;

namespace GMacSamples.GMacBase
{
    internal static class GMacBaseSampleTasks
    {
        private static readonly string PageSeparator = "".PadLeft(64, '-');

        private static readonly LinearComposer Composer = new LinearComposer();

        private static void AddPatternsListToComposer(IEnumerable<int> patternSeq)
        {
            var patternList = patternSeq.ToArray();

            var size = patternList.Max().LastOneBitPosition() + 1;

            Composer.IncreaseIndentation()
                .AppendLine(
                    patternList
                    .Select(i => $"{i.ToString().PadRight(3)} ({i.PatternToString(size)})")
                    .Concatenate(Environment.NewLine)
                )
                .DecreaseIndentation();
        }

        private static void AddPatternsListToComposer(IEnumerable<int> patternSeq, Func<int, string> itemToString)
        {
            var patternList = patternSeq.ToArray();

            var size = patternList.Max().LastOneBitPosition() + 1;

            Composer.IncreaseIndentation()
                .AppendLine(
                    patternList
                    .Select(i => $"{i.ToString().PadLeft(3)} ({i.PatternToString(size)}) -> {itemToString(i)}")
                    .Concatenate(Environment.NewLine)
                )
                .DecreaseIndentation();
        }


        private static string PatternsRangeSample()
        {
            Composer.Clear();

            Composer.AppendLine("PatternsBetween(3, 12):");
            AddPatternsListToComposer(BitUtils.PatternsBetween(3, 12));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternsBetween(12, 3):");
            AddPatternsListToComposer(BitUtils.PatternsBetween(12, 3));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("OnesPermutations(5, 2):");
            AddPatternsListToComposer(BitUtils.OnesPermutations(5, 2));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("OnesPermutations(5, 3):");
            AddPatternsListToComposer(BitUtils.OnesPermutations(5, 3));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("ZerosPermutations(5, 2):");
            AddPatternsListToComposer(BitUtils.ZerosPermutations(5, 2));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("ZerosPermutations(5, 3):");
            AddPatternsListToComposer(BitUtils.ZerosPermutations(5, 3));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            return Composer.ToString();
        }

        private static string PatternsTestingSample()
        {
            Composer.Clear();

            var patterns = BitUtils.PatternsBetween(0, 15).ToArray();

            Composer.AppendLine("IsEven(): ");
            AddPatternsListToComposer(patterns, p => p.IsEven().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("IsOdd(): ");
            AddPatternsListToComposer(patterns, p => p.IsOdd().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("IsBasicPattern(): ");
            AddPatternsListToComposer(patterns, p => p.IsBasicPattern().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("IsZeroOrBasicPattern(): ");
            AddPatternsListToComposer(patterns, p => p.IsZeroOrBasicPattern().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("IsOneAt(i); i=0,1,2,3: ");
            AddPatternsListToComposer(
                patterns, 
                p =>
                    Enumerable.Range(0, 4)
                    .Select(i => $"{p.IsOneAt(i),5}")
                    .Concatenate(", ", "{ ", " }")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("IsZeroAt(i); i=0,1,2,3: ");
            AddPatternsListToComposer(
                patterns,
                p =>
                    Enumerable.Range(0, 4)
                    .Select(i => $"{p.IsZeroAt(i),5}")
                    .Concatenate(", ", "{ ", " }")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            return Composer.ToString();
        }

        private static string PatternsInformationSample()
        {
            Composer.Clear();

            var patterns = BitUtils.PatternsBetween(0, 15).ToArray();

            Composer.AppendLine("CountOnes(): ");
            AddPatternsListToComposer(patterns, p => p.CountOnes().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("FirstOneBitPosition(): ");
            AddPatternsListToComposer(patterns, p => p.FirstOneBitPosition().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("LastOneBitPosition(): ");
            AddPatternsListToComposer(patterns, p => p.LastOneBitPosition().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("LowerPower2Limit(): ");
            AddPatternsListToComposer(patterns, p => p.LowerPower2Limit().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("UpperPower2Limit(): ");
            AddPatternsListToComposer(patterns, p => p.UpperPower2Limit().ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToFullMask(): ");
            AddPatternsListToComposer(patterns, p => p.PatternToFullMask().PatternToString(4));
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            return Composer.ToString();
        }

        private static string PatternsManipulationSample()
        {
            Composer.Clear();

            var patterns = BitUtils.PatternsBetween(0, 15).ToArray();

            Composer.AppendLine("SetBitToZeroAt(i); i=0,1,2,3,4,5: ");
            AddPatternsListToComposer(
                patterns,
                p =>
                    Enumerable.Range(0, 6)
                    .Select(i => p.SetBitToZeroAt(i).PatternToString(6))
                    .Concatenate(", ", "{ ", " }")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("SetBitToOneAt(i); i=0,1,2,3,4,5: ");
            AddPatternsListToComposer(
                patterns,
                p =>
                    Enumerable.Range(0, 6)
                    .Select(i => p.SetBitToOneAt(i).PatternToString(6))
                    .Concatenate(", ", "{ ", " }")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("InvertBitAt(i); i=0,1,2,3,4,5: ");
            AddPatternsListToComposer(
                patterns,
                p =>
                    Enumerable.Range(0, 6)
                    .Select(i => p.InvertBitAt(i).PatternToString(6))
                    .Concatenate(", ", "{ ", " }")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("SetBitsToZeroAt(0, 2): ");
            AddPatternsListToComposer(patterns, p => p.SetBitsToZeroAt(0, 2).ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("SetBitsToOneAt(0, 2): ");
            AddPatternsListToComposer(patterns, p => p.SetBitsToOneAt(0, 2).ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("InvertBitsAt(0, 2): ");
            AddPatternsListToComposer(patterns, p => p.InvertBitsAt(0, 2).ToString());
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            return Composer.ToString();
        }

        private static string ConversionFromPatternSample()
        {
            Composer.Clear();

            var patterns = BitUtils.PatternsBetween(0, 15).ToArray();

            Composer.AppendLine("PatternToBooleans(): ");
            AddPatternsListToComposer(
                patterns,
                p => 
                    p.PatternToBooleans()
                    .Select(b => $"{b,5}")
                    .Concatenate(",", "{", "}")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToPositions(): ");
            AddPatternsListToComposer(
                patterns,
                p => 
                    p.PatternToPositions()
                    .Select(b => $"{b,2}")
                    .Concatenate(",", "{", "}")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToSequence(-1, 1): ");
            AddPatternsListToComposer(
                patterns,
                p => 
                    p.PatternToSequence(-1, 1)
                    .Select(b => $"{b,2}")
                    .Concatenate(",", "{", "}")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToSequence(4, -1, 1): ");
            AddPatternsListToComposer(
                patterns,
                p =>
                    p.PatternToSequence(4, -1, 1)
                    .Select(b => $"{b,2}")
                    .Concatenate(",", "{", "}")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToFullMask(): ");
            AddPatternsListToComposer(
                patterns,
                p => $"{p.PatternToFullMask(),2} ({p.PatternToFullMask().PatternToString(4)})"
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToString(): ");
            AddPatternsListToComposer(
                patterns,
                p => $"{p.PatternToString()}"
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            Composer.AppendLine("PatternToString(4): ");
            AddPatternsListToComposer(
                patterns,
                p => $"{p.PatternToString(4)}"
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            var sList = new[] {"a", "b", "c", "d"};

            Composer.AppendLine("PickUsingPattern(): ");
            AddPatternsListToComposer(
                patterns,
                p =>
                    sList.PickUsingPattern(p)
                    .Select(b => $"{b}")
                    .Concatenate(",", "{", "}")
                );
            Composer.AppendLineAtNewLine(PageSeparator).AppendLine();

            return Composer.ToString();
        }

        private static string ConversionToPatternSample()
        {
            Composer.Clear();

            var bitPattern = new[] {true, false, true, true}.BooleansToPattern();
            Composer
                .Append("BooleansToPattern(true, false, true, true): ")
                .AppendLine($"{bitPattern} ({bitPattern.PatternToString()})")
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            bitPattern = new[] {7, 2, 3}.PositionsToPattern();
            Composer
                .Append("PositionsToPattern(7, 2, 3): ")
                .AppendLine($"{bitPattern} ({bitPattern.PatternToString()})")
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            bitPattern = "10010".StringToPattern();
            Composer
                .Append("StringToPattern(\"10010\"): ")
                .AppendLine($"{bitPattern} ({bitPattern.PatternToString()})")
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            bitPattern = "+*%+0".StringToPattern('+');
            Composer
                .Append("StringToPattern(\"+*%+0\", '+'): ")
                .AppendLine($"{bitPattern} ({bitPattern.PatternToString()})")
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            return Composer.ToString();
        }

        private static string PatternPartsSample()
        {
            Composer.Clear();



            return Composer.ToString();
        }

        private static string StringPatternSample()
        {
            Composer.Clear();

            return Composer.ToString();
        }


        private static string EuclideanBilinearProductTable(int[] basisBladeIds, Func<int, int, bool> isZeroFunc)
        {
            var composer = new TableComposer(basisBladeIds.Length, basisBladeIds.Length);

            for (var r = 0; r < basisBladeIds.Length; r++)
            {
                var id1 = basisBladeIds[r];
                composer.ColumnsInfo[r].Header = id1.BasisBladeName();
                composer.RowsInfo[r].Header = id1.BasisBladeName();

                for (var c = 0; c < basisBladeIds.Length; c++)
                {
                    var id2 = basisBladeIds[c];

                    //var isNegative = MultivectorUtils.ComputeIsNegativeEGp(id1, id2);
                    var isNegative = FrameUtils.IsNegativeEGp(id1, id2);

                    var name = isZeroFunc(id1, id2) 
                        ? "0" 
                        : ((isNegative ? "-" : "") + (id1 ^ id2).BasisBladeName());

                    composer.Items[r, c] = name;
                }
            }

            return composer.ToString();
        }

        private static string InvolutionTables(int[] basisBladeIds)
        {
            var composer = new TableComposer(basisBladeIds.Length, 3);

            composer.ColumnsInfo[0].Header = "Reverse";
            composer.ColumnsInfo[1].Header = "Grade Involution";
            composer.ColumnsInfo[2].Header = "Clifford Conjugate";

            for (var r = 0; r < basisBladeIds.Length; r++)
            {
                var id = basisBladeIds[r];

                composer.RowsInfo[r].Header = id.BasisBladeName();

                composer.Items[r, 0] = id.BasisBladeIdHasNegativeReverse() ? "-1" : "+1";
                composer.Items[r, 1] = id.BasisBladeIdHasNegativeGradeInv() ? "-1" : "+1";
                composer.Items[r, 2] = id.BasisBladeIdHasNegativeClifConj() ? "-1" : "+1";
            }

            return composer.ToString();
        }

        private static string EuclideanSample(int vSpaceDim)
        {
            Composer.Clear();

            var basisBladeIds = FrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim).ToArray();

            Composer
                .AppendLine("Involution Tables, " + vSpaceDim + "D Space:")
                .IncreaseIndentation()
                .AppendLine(InvolutionTables(basisBladeIds))
                .DecreaseIndentation()
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            Composer
                .AppendLine("Geometric Product Table, " + vSpaceDim + "D Space:")
                .IncreaseIndentation()
                .AppendLine(EuclideanBilinearProductTable(basisBladeIds, EuclideanUtils.IsZeroEuclideanGp))
                .DecreaseIndentation()
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            Composer
                .AppendLine("Outer Product Table, " + vSpaceDim + "D Space:")
                .IncreaseIndentation()
                .AppendLine(EuclideanBilinearProductTable(basisBladeIds, EuclideanUtils.IsZeroOp))
                .DecreaseIndentation()
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            Composer
                .AppendLine("Scalar Product Table, " + vSpaceDim + "D Space:")
                .IncreaseIndentation()
                .AppendLine(EuclideanBilinearProductTable(basisBladeIds, EuclideanUtils.IsZeroEuclideanSp))
                .DecreaseIndentation()
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            Composer
                .AppendLine("Left Contraction Product Table, " + vSpaceDim + "D Space:")
                .IncreaseIndentation()
                .AppendLine(EuclideanBilinearProductTable(basisBladeIds, EuclideanUtils.IsZeroEuclideanLcp))
                .DecreaseIndentation()
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            Composer
                .AppendLine("Right Contraction Product Table, " + vSpaceDim + "D Space:")
                .IncreaseIndentation()
                .AppendLine(EuclideanBilinearProductTable(basisBladeIds, EuclideanUtils.IsZeroEuclideanRcp))
                .DecreaseIndentation()
                .AppendLineAtNewLine(PageSeparator)
                .AppendLine();

            return Composer.ToString();
        }


        public static List<SampleTask> MultivectorUtilsSampleTasks()
        {
            var tasksList = new List<SampleTask>
            {
                new SampleTask(
                    "Euclidean Operations Tables for 2D Space Sample",
                    "",
                    () => EuclideanSample(2)
                    ),

                new SampleTask(
                    "Euclidean Operations Tables for 3D Space Sample",
                    "",
                    () => EuclideanSample(3)
                    ),

                new SampleTask(
                    "Euclidean Operations Tables for 4D Space Sample",
                    "",
                    () => EuclideanSample(4)
                    )

            };

            return tasksList;
        }

        public static List<SampleTask> BitUtilsSampleTasks()
        {
            var tasksList = new List<SampleTask>
            {
                new SampleTask(
                    "Patterns Range Sample",
                    "",
                    PatternsRangeSample
                    ),

                new SampleTask(
                    "Patterns Testing Sample",
                    "",
                    PatternsTestingSample
                    ),

                new SampleTask(
                    "Patterns Information Sample",
                    "",
                    PatternsInformationSample
                    ),

                new SampleTask(
                    "Pattern Manipulation Sample",
                    "",
                    PatternsManipulationSample
                    ),

                new SampleTask(
                    "Conversion from Pattern to Other Forms Sample",
                    "",
                    ConversionFromPatternSample
                    ),

                new SampleTask(
                    "Conversion to Pattern Sample",
                    "",
                    ConversionToPatternSample
                    ),

                new SampleTask(
                    "Parts of Pattern Sample",
                    "",
                    PatternPartsSample
                    ),

                new SampleTask(
                    "Patterns and Strings Sample",
                    "",
                    StringPatternSample
                    )
            };

            return tasksList;
        }


    }
}
