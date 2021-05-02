using System;
using DataStructuresLib.Combinations;

namespace DataStructuresLib.Samples.Combinations
{
    public static class Sample3
    {
        public static void Execute()
        {
            var n = 8;
            var k = 2;
            var count = n.ComputeBinomialCoefficient(k);

            for (var index = 0UL; index < count; index++)
            {
                //var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
                //var n1 = index - n2 * (n2 - 1) / 2;

                var (n1, n2) = BinaryCombinationsUtilsUInt64.IndexToCombinadic(index);

                var pattern = index.IndexToCombinadicPattern(k);
                var patternText =
                    Convert.ToString((long) pattern, 2).PadLeft(n, '0');

                //var index2 = n1.Combinadic2DigitsToIndex(n2);
                var index2 = BinaryCombinationsUtilsUInt64.CombinadicPatternToIndex(pattern);

                //var nn2 = (ulong) Math.Log(pattern, 2);
                //var nn1 = (ulong) Math.Log(pattern - (1UL << (int)nn2), 2);
                
                //var index2 = nn1 + ((nn2 * (nn2 - 1UL)) >> 1);

                Console.WriteLine($"index: {index}, pattern: {patternText}, n1: {n1}, n2: {n2}, index2 : {index2}");
            }
        }
    }
}