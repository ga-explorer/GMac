using System;
using DataStructuresLib.Combinations;

namespace DataStructuresLib.Samples.Combinations
{
    public static class Sample1
    {
        public static void Execute()
        {
            var pascalTriangle16 = new PascalTriangleUInt16(18);
            var pascalTriangle32 = new PascalTriangleUInt32(34);
            var pascalTriangle64 = new PascalTriangleUInt64(67);

            Console.WriteLine(pascalTriangle16);
            Console.WriteLine(pascalTriangle32);
            Console.WriteLine(pascalTriangle64);
        }
    }
}
