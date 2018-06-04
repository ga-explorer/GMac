using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler;

namespace GMac.GMacMath
{
    public static class GMacMathUtils
    {
        /// <summary>
        /// The maximum allowed GA vector space dimension
        /// </summary>
        public static int MaxVSpaceDimension { get; private set; }

        /// <summary>
        /// The maximum possible basis blade ID in the maximum allowed GA vector space dimension
        /// </summary>
        public static int MaxVSpaceBasisBladeId { get; }

        public static string[] DefaultBasisVectorsNames { get; }


        private static void SetMaxVSpaceDimension()
        {
            const int defaultMaxVSpaceDimension = 10;

            if (GMacCompilerFeatures.MaxFrameDimension < defaultMaxVSpaceDimension)
            {
                MaxVSpaceDimension = GMacCompilerFeatures.MaxFrameDimension;
                return;
            }

            try
            {
                var value = GMacSystemUtils.Settings["maxVSpaceDimension"];

                int i;
                MaxVSpaceDimension = 
                    Int32.TryParse(value, out i) 
                    ? Math.Max(defaultMaxVSpaceDimension, Math.Min(i, GMacCompilerFeatures.MaxFrameDimension)) 
                    : defaultMaxVSpaceDimension;
            }
            catch
            {
                MaxVSpaceDimension = defaultMaxVSpaceDimension;
            }
        }

        static GMacMathUtils()
        {
            SetMaxVSpaceDimension();
            MaxVSpaceBasisBladeId = (1 << MaxVSpaceDimension) - 1;

            DefaultBasisVectorsNames = 
                Enumerable
                .Range(1, MaxVSpaceDimension)
                .Select(i => "e" + i)
                .ToArray();
        }


        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by MaxVSpaceDimension bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static int JoinIDs(int id1, int id2)
        {
            return id1 | (id2 << MaxVSpaceDimension);
        }

        /// <summary>
        /// Given two basis blades IDs id1 and id2 in a Euclidean GA, this finds if their Geometric Product 
        /// is +1 (returns false) or -1 (returns true). The result is not computed but found in a lookup table
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNegativeEGp(int id1, int id2)
        {
            //This is only double the speed compared to the computed version, for symbolic computations
            //it's a highly insignificant optimization with a 128 MBytes cost in memory
            //var id = JoinIDs(id1, id2);
            //return GMacLookupTables.IsNegativeEGpTable[id];

            if (id1 == 0 || id2 == 0) return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0) flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id = id ^ mask2;

                mask2 <<= 1;
            }

            return flag;
        }


        public static IEnumerable<AstFrameBasisBlade> SortBasisBladesByGrade(this IEnumerable<AstFrameBasisBlade> idsSeq)
        {
            return idsSeq.OrderBy(basisBlade => basisBlade.Grade).ThenBy(basisBlade => basisBlade.Index);
        }

        public static IEnumerable<IGrouping<int, AstFrameBasisBlade>> GroupBasisBladesByGrade(this IEnumerable<AstFrameBasisBlade> idsSeq)
        {
            return idsSeq.GroupBy(basisBlade => basisBlade.Grade);
        }
    }
}