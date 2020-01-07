using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraNumericsLib.Structures
{
    public static class GaStructuresUtils
    {
        public static GaSparseTable1D<TKey, TValue> ToSparseTable<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return new GaSparseTable1D<TKey, TValue>(pairs);
        }

        public static GaSparseTable1D<TKey, TValue> ToSparseTable<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> pairs, TValue defaultValue)
        {
            return new GaSparseTable1D<TKey, TValue>(defaultValue, pairs);
        }

        public static GaSparseTable2D<TKey1, TKey2, TValue> ToSparseTable<TKey1, TKey2, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2>, TValue>> pairs)
        {
            return new GaSparseTable2D<TKey1, TKey2, TValue>(pairs);
        }

        public static GaSparseTable2D<TKey1, TKey2, TValue> ToSparseTable<TKey1, TKey2, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2>, TValue>> pairs, TValue defaultValue)
        {
            return new GaSparseTable2D<TKey1, TKey2, TValue>(defaultValue, pairs);
        }

        public static GaSparseTable3D<TKey1, TKey2, TKey3, TValue> ToSparseTable<TKey1, TKey2, TKey3, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> pairs)
        {
            return new GaSparseTable3D<TKey1, TKey2, TKey3, TValue>(pairs);
        }

        public static GaSparseTable3D<TKey1, TKey2, TKey3, TValue> ToSparseTable<TKey1, TKey2, TKey3, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> pairs, TValue defaultValue)
        {
            return new GaSparseTable3D<TKey1, TKey2, TKey3, TValue>(defaultValue, pairs);
        }


        public static bool IsActiveBinaryTreeNodeId(this ulong nodeId, ulong bitMask, IEnumerable<ulong> activeLeafNodeIDs)
        {
            return activeLeafNodeIDs.Any(leafNodeId => (leafNodeId ^ nodeId) < bitMask);
        }

        public static bool IsActiveQuadTreeNodeId(this Tuple<ulong, ulong> nodeId, ulong bitMask, IEnumerable<Tuple<ulong, ulong>> activeLeafNodeIDs)
        {
            return activeLeafNodeIDs.Any(
                leafNodeId => 
                    (leafNodeId.Item1 ^ nodeId.Item1) < bitMask &&
                    (leafNodeId.Item2 ^ nodeId.Item2) < bitMask
            );
        }
    }
}
