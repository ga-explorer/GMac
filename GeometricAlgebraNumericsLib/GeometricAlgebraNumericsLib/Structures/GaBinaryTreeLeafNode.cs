namespace GeometricAlgebraNumericsLib.Structures
{
    /// <summary>
    /// This class represents a leaf node in a binary tree used for efficient
    /// computations on sparse multivectors and sparse linear maps on
    /// multivectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaBinaryTreeLeafNode<T> : IGaBinaryTreeNode<T>
    {
        public ulong Id { get; }

        public ulong ChildId0 => 0ul;

        public ulong ChildId1 => 0ul;

        public T Value { get; set; }

        public bool IsInternalNode => false;

        public bool IsLeafNode => true;

        public IGaBinaryTreeNode<T> ChildNode0 => null;

        public IGaBinaryTreeNode<T> ChildNode1 => null;

        public GaBinaryTreeLeafNode<T> LeafChildNode0 => null;

        public GaBinaryTreeLeafNode<T> LeafChildNode1 => null;

        public int TreeDepth => 0;

        public bool HasChildNode0 => false;

        public bool HasChildNode1 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GaBinaryTreeLeafNode(ulong id, T value)
        {
            Id = id;
            Value = value;
        }
    }
}