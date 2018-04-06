using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GMac.GMacMath
{
    internal static class GMacLookupTables
    {
        /// <summary>
        /// The file path of the lookup tables data file
        /// </summary>
        private static readonly string LookupDataFilePath 
            = Path.Combine(
                Environment.CurrentDirectory, 
                "GATables" + GMacMathUtils.MaxVSpaceDimension.ToString().Trim() + ".dat"
                );


        /// <summary>
        /// This is a lookup table for C(n, k) where n is any number between 0 and 16
        /// </summary>
        internal static List<int[]> ChooseList;

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
        internal static BitArray IsNegativeClifConjTable;

        /// <summary>
        /// (ID1, ID2) to Euclidean Geometric Product sign lookup table for unit basis of Euclidean GA
        /// </summary>
        internal static BitArray IsNegativeEGpTable;

        /// <summary>
        /// (grade, index) to ID lookup table
        /// </summary>
        internal static List<int[]> GradeIndexToIdTable;



        static GMacLookupTables()
        {
            FillChooseList();

            FillGaLookupTables();
        }


        /// <summary>
        /// Get the combination C(n, r) where n is between 0 and 16 and r is between 0 and n using a lookup table
        /// </summary>
        /// <param name="n"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int Choose(int n, int r)
        {
            if (ReferenceEquals(ChooseList, null))
                ChooseList = FillChooseList();

            return ChooseList[n][r];
        }

        /// <summary>
        /// Compute and fill the combinations lookup table
        /// </summary>
        private static List<int[]> FillChooseList()
        {
            ChooseList = new List<int[]>(17)
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
                new[] {1, 16, 120, 560, 1820, 4368, 8008, 11440, 12870, 11440, 8008, 4368, 1820, 560, 120, 16, 1}
            };

            return ChooseList;
        }

        /// <summary>
        /// Fill GA lookup tables by loading them from disk or computing them
        /// </summary>
        private static void FillGaLookupTables()
        {
            if (LoadGaLookupTables(LookupDataFilePath))
                return;

            ComputeGaLookupTables();

            SaveGaLookupTables(LookupDataFilePath);
        }

        /// <summary>
        /// Compute and fill all GA lookup tables
        /// </summary>
        private static void ComputeGaLookupTables()
        {
            const int c = 1;
            const int d = 1;
            var gaSpaceDim = (c << GMacMathUtils.MaxVSpaceDimension);
            var maxId = gaSpaceDim - 1;
            var gradeCount = new int[GMacMathUtils.MaxVSpaceDimension + 1];

            //Initialize all tables
            IdToGradeTable = new int[gaSpaceDim];
            IdToIndexTable = new int[gaSpaceDim];
            IsNegativeReverseTable = new BitArray(gaSpaceDim);
            IsNegativeGradeInvTable = new BitArray(gaSpaceDim);
            IsNegativeClifConjTable = new BitArray(gaSpaceDim);
            IsNegativeEGpTable = new BitArray(d << (2 *GMacMathUtils.MaxVSpaceDimension));
            GradeIndexToIdTable = new List<int[]>(GMacMathUtils.MaxVSpaceDimension);

            //var stopWatch = new Stopwatch();
            //long ticks = 0;

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
                if (grade.GradeHasNegativeClifConj())
                    IsNegativeClifConjTable.Set(id, true);

                //Calculate index of basis blade ID
                IdToIndexTable[id] = gradeCount[grade];

                gradeCount[grade] += 1;

                
                //stopWatch.Start();
                
                //Calculate the sign of the geometric product of basis blades
                var id1 = id;

                for (var id2 = 0; id2 <= maxId; id2++)
                    if (GMacMathUtils.ComputeIsNegativeEGp(id1, id2))
                        IsNegativeEGpTable.Set(GMacMathUtils.JoinIDs(id1, id2), true);

                //stopWatch.Stop();
                //ticks += stopWatch.ElapsedTicks;
            }

            //MessageBox.Show(ticks.ToString("###,###,###,###,###"));

            //Calculate inverse index table: (grade, index) to ID table
            gradeCount = new int[gaSpaceDim];

            for (var id = 0; id <= maxId; id++)
            {
                var grade = IdToGradeTable[id];
                var index = gradeCount[grade];

                gradeCount[grade] += 1;

                if (gradeCount[grade] == 1)
                    GradeIndexToIdTable.Add(new int[Choose(GMacMathUtils.MaxVSpaceDimension, grade)]);

                GradeIndexToIdTable[grade][index] = id;
            }
        }

        /// <summary>
        /// Save GA tables to disk
        /// </summary>
        private static bool SaveGaLookupTables(string fileName)
        {
            try
            {
                var formatter = new BinaryFormatter();

                using (var st = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //Formatter.Serialize(St, _ChooseList);
                    formatter.Serialize(st, IsNegativeEGpTable);
                    formatter.Serialize(st, IsNegativeReverseTable);
                    formatter.Serialize(st, IsNegativeGradeInvTable);
                    formatter.Serialize(st, IsNegativeClifConjTable);
                    formatter.Serialize(st, IdToGradeTable);
                    formatter.Serialize(st, IdToIndexTable);
                    formatter.Serialize(st, GradeIndexToIdTable);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Load GA tables from disk. If file does not exist or error during data load, compute 
        /// the tables and save to file
        /// </summary>
        private static bool LoadGaLookupTables(string fileName)
        {
            try
            {
                if (File.Exists(fileName) == false)
                    return false;

                var formatter = new BinaryFormatter();

                using (var st = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    //_ChooseList = Formatter.Deserialize(St) as List<int[]>;
                    IsNegativeEGpTable = formatter.Deserialize(st) as BitArray;
                    IsNegativeReverseTable = formatter.Deserialize(st) as BitArray;
                    IsNegativeGradeInvTable = formatter.Deserialize(st) as BitArray;
                    IsNegativeClifConjTable = formatter.Deserialize(st) as BitArray;
                    IdToGradeTable = formatter.Deserialize(st) as int[];
                    IdToIndexTable = formatter.Deserialize(st) as int[];
                    GradeIndexToIdTable = formatter.Deserialize(st) as List<int[]>;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
