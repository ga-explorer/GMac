namespace GMac.GMacMath.Structures
{
    public sealed class GMacBinaryTreeLeafNode<T> : IGMacBinaryTreeNode<T>
    {
        public bool IsParentNode => false;

        public bool IsLeafNode => true;

        public IGMacBinaryTreeNode<T> ChildNode0 => null;

        public IGMacBinaryTreeNode<T> ChildNode1 => null;

        public GMacBinaryTreeLeafNode<T> LeafChildNode0 => null;

        public GMacBinaryTreeLeafNode<T> LeafChildNode1 => null;

        public T Value { get; set; }

        public int TreeDepth => 0;

        public bool HasChildNode0 => false;

        public bool HasChildNode1 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GMacBinaryTreeLeafNode(T value)
        {
            Value = value;
        }
    }
}