using System;
using System.Collections.Generic;
using System.Text;
using DataStructuresLib.Combinations;

namespace DataStructuresLib.Samples.Combinations
{
    public static class Sample2
    {
        private static string ListToString(IEnumerable<int> itemsList)
        {
            var composer = new StringBuilder();

            foreach (var item in itemsList)
                composer.Append(item).Append(", ");

            composer.Length -= 2;

            return composer.ToString();
        }

        public static void Execute1()
        {
            var digitsCount = 8;
            var index = 1000000UL;

            var combinadicArray = 
                index.IndexToCombinadic(digitsCount);

            Console.WriteLine(ListToString(combinadicArray));
        }

        public static void Execute2()
        {
            var setSize = 6;
            var digitsCount = 3;
            var indexCount = 
                setSize.GetBinomialCoefficient(digitsCount);

            for (var index = 0UL; index < indexCount; index++)
            {
                var combinadicArray =
                    index.IndexToCombinadic(digitsCount);

                var combinadicPattern =
                    index.IndexToCombinadicPattern(digitsCount);

                var index2 = 
                    combinadicPattern.CombinadicPatternToIndex();

                var combinadicArrayText = 
                    ListToString(combinadicArray);

                var combinadicPatternText = 
                    Convert.ToString((long) combinadicPattern, 2).PadLeft(setSize, '0');

                Console.WriteLine(index2 + ": " + combinadicPatternText + " = " + combinadicArrayText);
            }
        }
    }
}