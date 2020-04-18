using GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees
{
    /// <summary>
    /// This class represents a leaf node in a binary tree used for efficient
    /// computations on sparse multivectors and sparse linear maps on
    /// multivectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaBtrLeafNode<T> : IGaBtrNode<T>
    {
        public T Value { get; set; }

        public bool IsInternalNode => false;

        public bool IsLeafNode => true;

        public IGaBtrNode<T> ChildNode0 => null;

        public IGaBtrNode<T> ChildNode1 => null;

        public GaBtrLeafNode<T> LeafChildNode0 => null;

        public GaBtrLeafNode<T> LeafChildNode1 => null;

        public bool HasChildNode0 => false;

        public bool HasChildNode1 => false;

        public bool HasNoChildNodes => true;


        internal GaBtrLeafNode(T value)
        {
            Value = value;
        }


        public int GetTreeDepth()
        {
            return 0;
        }

        public GaBinaryTreeNodeInfo1<T> GetNodeInfo(int treeDepth, ulong id)
        {
            return new GaBinaryTreeNodeInfo1<T>(0, id, this);
        }
    }
}