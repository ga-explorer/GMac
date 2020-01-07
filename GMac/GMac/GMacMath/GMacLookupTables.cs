using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GeometricAlgebraNumericsLib.Frames;

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
                "GMacLookupTables" + GMacMathUtils.MaxVSpaceDimension.ToString().Trim() + ".dat"
                );

        /// <summary>
        /// (ID1, ID2) to Euclidean Geometric Product sign lookup table for unit basis of Euclidean GA
        /// </summary>
        internal static BitArray IsNegativeEGpTable { get; private set; }


        static GMacLookupTables()
        {
            //FillChooseList();

            FillGaLookupTables();
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

            IsNegativeEGpTable = new BitArray(d << (2 *GMacMathUtils.MaxVSpaceDimension));

            //var stopWatch = new Stopwatch();
            //long ticks = 0;

            for (var id1 = 0; id1 <= maxId; id1++)
            {
                //stopWatch.Start();
                
                //Calculate the sign of the geometric product of basis blades
                for (var id2 = 0; id2 <= maxId; id2++)
                    if (GaNumFrameUtils.IsNegativeEGp(id1, id2))
                        IsNegativeEGpTable.Set(GMacMathUtils.JoinIDs(id1, id2), true);

                //stopWatch.Stop();
                //ticks += stopWatch.ElapsedTicks;
            }

            //MessageBox.Show(ticks.ToString("###,###,###,###,###"));
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
                    //formatter.Serialize(st, IsNegativeReverseTable);
                    //formatter.Serialize(st, IsNegativeGradeInvTable);
                    //formatter.Serialize(st, IsNegativeClifConjTable);
                    //formatter.Serialize(st, IdToGradeTable);
                    //formatter.Serialize(st, IdToIndexTable);
                    //formatter.Serialize(st, GradeIndexToIdTable);
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
                    //IsNegativeReverseTable = formatter.Deserialize(st) as BitArray;
                    //IsNegativeGradeInvTable = formatter.Deserialize(st) as BitArray;
                    //IsNegativeClifConjTable = formatter.Deserialize(st) as BitArray;
                    //IdToGradeTable = formatter.Deserialize(st) as int[];
                    //IdToIndexTable = formatter.Deserialize(st) as int[];
                    //GradeIndexToIdTable = formatter.Deserialize(st) as List<int[]>;
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
