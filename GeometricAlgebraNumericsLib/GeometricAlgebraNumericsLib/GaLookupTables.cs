using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib
{
    public static class GaLookupTables
    {
        /// <summary>
        /// This is a lookup table for C(n, k) where n is any number between 0 and 25
        /// </summary>
        internal static int[][] ChooseList;

        /// <summary>
        /// ID to grade lookup table
        /// </summary>
        internal static int[] IdToGradeTable;

        /// <summary>
        /// ID to index lookup table
        /// </summary>
        internal static int[] IdToIndexTable;

        /// <summary>
        /// ID to 'is reverse sign = -1' lookup table
        /// </summary>
        internal static BitArray IsNegativeReverseTable;

        /// <summary>
        /// ID to 'is grade inverse sign = -1' lookup table
        /// </summary>
        internal static BitArray IsNegativeGradeInvTable;

        /// <summary>
        /// ID to 'is clifford conjugate sign = -1' lookup table
        /// </summary>
        internal static BitArray IsNegativeCliffConjTable;

        /// <summary>
        /// (grade, index) to ID lookup table
        /// </summary>
        internal static List<int[]> GradeIndexToIdTable;

        /// <summary>
        /// Is basis blades EGP Negative lookup tables
        /// </summary>
        internal static BitArray[] IsNegativeEgpLookupTables;

        /// <summary>
        /// Is (basis blade by basis blade) EGP Negative lookup tables
        /// </summary>
        internal static BitArray[] IsNegativeVectorEgpLookupTables;

        /// <summary>
        /// An index lookup table used to compute the outer product of a vector with a k-vector
        /// </summary>
        internal static int[][][][] VectorKVectorOpIndexLookupTables;


        static GaLookupTables()
        {
            FillChooseList();

            ComputeIsNegativeVectorEgpLookupTables();

            ComputeIsNegativeEgpLookupTables();

            ComputeGaLookupTables();

            ComputeVectorKVectorOpIndexLookupTables();
        }


        private static void FillChooseList()
        {
            ChooseList = new[]
            {
                new[] {1},
                new[] {1, 1},
                new[] {1, 2, 1},
                new[] {1, 3, 3, 1},
                new[] {1, 4, 6, 4, 1},
                new[] {1, 5, 10, 10, 5, 1},
                new[] {1, 6, 15, 20, 15, 6, 1},
                new[] {1, 7, 21, 35, 35, 21, 7, 1},
                new[] {1, 8, 28, 56, 70, 56, 28, 8, 1},
                new[] {1, 9, 36, 84, 126, 126, 84, 36, 9, 1},
                new[] {1, 10, 45, 120, 210, 252, 210, 120, 45, 10, 1},
                new[] {1, 11, 55, 165, 330, 462, 462, 330, 165, 55, 11, 1},
                new[] {1, 12, 66, 220, 495, 792, 924, 792, 495, 220, 66, 12, 1},
                new[] {1, 13, 78, 286, 715, 1287, 1716, 1716, 1287, 715, 286, 78, 13, 1},
                new[] {1, 14, 91, 364, 1001, 2002, 3003, 3432, 3003, 2002, 1001, 364, 91, 14, 1},
                new[] {1, 15, 105, 455, 1365, 3003, 5005, 6435, 6435, 5005, 3003, 1365, 455, 105, 15, 1},
                new[] {1, 16, 120, 560, 1820, 4368, 8008, 11440, 12870, 11440, 8008, 4368, 1820, 560, 120, 16, 1},
                new[] {1, 17, 136, 680, 2380, 6188, 12376, 19448, 24310, 24310, 19448, 12376, 6188, 2380, 680, 136, 17, 1},
                new[] {1, 18, 153, 816, 3060, 8568, 18564, 31824, 43758, 48620, 43758, 31824, 18564, 8568, 3060, 816, 153, 18, 1},
                new[] {1, 19, 171, 969, 3876, 11628, 27132, 50388, 75582, 92378, 92378, 75582, 50388, 27132, 11628, 3876, 969, 171, 19, 1},
                new[] {1, 20, 190, 1140, 4845, 15504, 38760, 77520, 125970, 167960, 184756, 167960, 125970, 77520, 38760, 15504, 4845, 1140, 190, 20, 1},
                new[] {1, 21, 210, 1330, 5985, 20349, 54264, 116280, 203490, 293930, 352716, 352716, 293930, 203490, 116280, 54264, 20349, 5985, 1330, 210, 21, 1},
                new[] {1, 22, 231, 1540, 7315, 26334, 74613, 170544, 319770, 497420, 646646, 705432, 646646, 497420, 319770, 170544, 74613, 26334, 7315, 1540, 231, 22, 1},
                new[] {1, 23, 253, 1771, 8855, 33649, 100947, 245157, 490314, 817190, 1144066, 1352078, 1352078, 1144066, 817190, 490314, 245157, 100947, 33649, 8855, 1771, 253, 23, 1},
                new[] {1, 24, 276, 2024, 10626, 42504, 134596, 346104, 735471, 1307504, 1961256, 2496144, 2704156, 2496144, 1961256, 1307504, 735471, 346104, 134596, 42504, 10626, 2024, 276, 24, 1},
                new[] {1, 25, 300, 2300, 12650, 53130, 177100, 480700, 1081575, 2042975, 3268760, 4457400, 5200300, 5200300, 4457400, 3268760, 2042975, 1081575, 480700, 177100, 53130, 12650, 2300, 300, 25, 1}
            };
        }

        private static void ComputeIsNegativeEgpLookupTables()
        {
            const int c = 1;
            const int maxDim = 13;

            var maxVSpaceDim = 
                GaNumFrameUtils.MaxVSpaceDimension < maxDim
                    ? GaNumFrameUtils.MaxVSpaceDimension 
                    : maxDim;

            var maxGaSpaceDim = (c << maxVSpaceDim);
            
            IsNegativeEgpLookupTables = new BitArray[maxGaSpaceDim];

            for (var id1 = 0; id1 < maxGaSpaceDim; id1++)
            {
                var bitArray = new BitArray(maxGaSpaceDim);

                var n = id1;
                Parallel.For(
                    0, 
                    maxGaSpaceDim, 
                    id2 => { bitArray[id2] = GaNumFrameUtils.ComputeIsNegativeEGp(n, id2); }
                );

                IsNegativeEgpLookupTables[id1] = bitArray;
            }
        }

        private static void ComputeIsNegativeVectorEgpLookupTables()
        {
            const int c = 1;
            const int maxDim = 20;

            var maxVSpaceDim =
                GaNumFrameUtils.MaxVSpaceDimension < maxDim
                    ? GaNumFrameUtils.MaxVSpaceDimension
                    : maxDim;

            var gaSpaceDim = (c << maxVSpaceDim);

            IsNegativeVectorEgpLookupTables = new BitArray[gaSpaceDim];

            for (var index1 = 0; index1 < maxVSpaceDim; index1++)
            {
                var bitArray = new BitArray(gaSpaceDim);

                var id1 = c << index1;
                Parallel.For(
                    0,
                    gaSpaceDim,
                    id2 => { bitArray[id2] = GaNumFrameUtils.ComputeIsNegativeEGp(id1, id2); }
                );

                IsNegativeVectorEgpLookupTables[index1] = bitArray;
            }
        }

        private static void ComputeGaLookupTables()
        {
            const int c = 1;

            var gaSpaceDim = (c << GaNumFrameUtils.MaxVSpaceDimension);
            var maxId = gaSpaceDim - 1;
            var gradeCount = new int[GaNumFrameUtils.MaxVSpaceDimension + 1];

            //Initialize all tables
            IdToGradeTable = new int[gaSpaceDim];
            IdToIndexTable = new int[gaSpaceDim];
            IsNegativeReverseTable = new BitArray(gaSpaceDim);
            IsNegativeGradeInvTable = new BitArray(gaSpaceDim);
            IsNegativeCliffConjTable = new BitArray(gaSpaceDim);
            GradeIndexToIdTable = new List<int[]>(GaNumFrameUtils.MaxVSpaceDimension);
            
            for (var id = 0; id <= maxId; id++)
            {
                var grade = id.CountOnes();

                IdToGradeTable[id] = grade;

                //Calculate grade inversion sign
                if (grade.GradeHasNegativeGradeInv())
                    IsNegativeGradeInvTable.Set(id, true);

                //Calculate reversion sign
                if (grade.GradeHasNegativeReverse())
                    IsNegativeReverseTable.Set(id, true);

                //Calculate Clifford conjugate sign
                if (grade.GradeHasNegativeCliffConj())
                    IsNegativeCliffConjTable.Set(id, true);

                //Calculate index of basis blade ID
                IdToIndexTable[id] = gradeCount[grade];

                gradeCount[grade] += 1;
            }

            //Calculate inverse index table: (grade, index) to ID table
            gradeCount = new int[GaNumFrameUtils.MaxVSpaceDimension + 1];

            for (var id = 0; id <= maxId; id++)
            {
                var grade = IdToGradeTable[id];
                var index = gradeCount[grade];

                gradeCount[grade] += 1;

                if (gradeCount[grade] == 1)
                    GradeIndexToIdTable.Add(new int[Choose(GaNumFrameUtils.MaxVSpaceDimension, grade)]);

                GradeIndexToIdTable[grade][index] = id;
            }
        }

        private static void ComputeVectorKVectorOpIndexLookupTables()
        {
            const int maxDim = 15;
            const int minVSpaceDim = 12;

            var maxVSpaceDim =
                GaNumFrameUtils.MaxVSpaceDimension < maxDim
                    ? GaNumFrameUtils.MaxVSpaceDimension
                    : maxDim;

            //var maxGaSpaceDim = (1 << maxVSpaceDim);

            VectorKVectorOpIndexLookupTables = new int[maxVSpaceDim - minVSpaceDim + 1][][][];

            for (var vSpaceDim = minVSpaceDim; vSpaceDim <= maxVSpaceDim; vSpaceDim++)
            {
                var vectorKVectorOpIndexLookupTable = new int[vSpaceDim - 1][][];

                for (var grade = 1; grade < vSpaceDim; grade++)
                {
                    var lookupTable = new int[Choose(vSpaceDim, grade + 1)][];

                    var resultIdsList =
                        GaNumFrameUtils.BasisBladeIDsOfGrade(vSpaceDim, grade + 1);

                    var lookupTableIndex = 0;
                    foreach (var id in resultIdsList)
                    {
                        var indexList1 = id.PatternToPositions().ToArray();
                        var lookupTableItems = new List<int>(2 * indexList1.Length);

                        foreach (var index1 in indexList1)
                        {
                            var id1 = 1 << index1;
                            var id2 = id ^ id1;
                            var index2 = id2.BasisBladeIndex();

                            lookupTableItems.Add(index1);
                            lookupTableItems.Add(index2);
                        }

                        lookupTable[lookupTableIndex] = lookupTableItems.ToArray();

                        lookupTableIndex++;
                    }

                    vectorKVectorOpIndexLookupTable[grade - 1] = lookupTable;
                }

                VectorKVectorOpIndexLookupTables[vSpaceDim - minVSpaceDim] = vectorKVectorOpIndexLookupTable;
            }
        }


        /// <summary>
        /// Get the combination C(n, r) where n is between 0 and 16 and r is between 0 and n using a lookup table
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int Choose(int n, int r)
        {
            return ChooseList[n][r];
        }
    }
}
