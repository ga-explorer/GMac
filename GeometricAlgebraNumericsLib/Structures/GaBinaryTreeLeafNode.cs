namespace GeometricAlgebraNumericsLib.Structures
{
    public sealed class GaBinaryTreeLeafNode<T> : IGaBinaryTreeNode<T>
    {
        public bool IsParentNode => false;

        public bool IsLeafNode => true;

        public IGaBinaryTreeNode<T> ChildNode0 => null;

        public IGaBinaryTreeNode<T> ChildNode1 => null;

        public GaBinaryTreeLeafNode<T> LeafChildNode0 => null;

        public GaBinaryTreeLeafNode<T> LeafChildNode1 => null;

        public T Value { get; set; }

        public int TreeDepth => 0;

        public bool HasChildNode0 => false;

        public bool HasChildNode1 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GaBinaryTreeLeafNode(T value)
        {
            Value = value;
        }
    }
}