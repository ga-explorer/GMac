using System.Collections.Generic;

namespace GeometricAlgebraStructuresLib.Trees
{
    public static class GaTreesUtils
    {
        public static GaBinaryTree<T> GetBinaryIndexedSparseList<T>(this IReadOnlyDictionary<ulong, T> sourceDictionary, int treeDepth)
        {
            return new GaBinaryTree<T>(
                treeDepth,
                sourceDictionary
            );
        }

        public static GaBinaryTree<T> GetBinaryIndexedSparseList<T>(this IReadOnlyList<ulong> idsList, int treeDepth)
        {
            return new GaBinaryTree<T>(treeDepth, idsList);
        }
    }
}