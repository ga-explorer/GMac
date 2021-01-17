using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Samples.Structures
{
    public static class Sample1
    {
        public static void Execute()
        {
            var treeDepth = 8;

            var dict = new Dictionary<int, int>
            {
                //{0, 0},
                {1, 1},
                {2, 2},
                //{3, 3},
                {4, 4},
                {5, 5},
                //{6, 6},
                {7, 7}
            };

            var indexedList = new GaBinaryTree<int>(
                treeDepth, 
                (IDictionary<int, int>)dict
            );

            Console.WriteLine(indexedList.ToString());
        }
    }
}
