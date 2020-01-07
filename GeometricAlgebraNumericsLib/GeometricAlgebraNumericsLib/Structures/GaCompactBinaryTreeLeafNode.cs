namespace GeometricAlgebraNumericsLib.Structures
{
    public struct GaCompactBinaryTreeLeafNode<T> : IGaCompactBinaryTreeNode<T>
    {
        public bool IsInternalNode => false;

        public bool IsLeafNode => true;

        public bool IsEmptyNode => false;

        public int ChildNodeIndex0 => -1;

        public int ChildNodeIndex1 => -1;

        public ulong Id { get; }

        public T Value { get; }

        public int TreeDepth => 0;

        public bool HasChildNode0 => false;

        public bool HasChildNode1 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GaCompactBinaryTreeLeafNode(int id, T value)
        {
            Id = (ulong)id;
            Value = value;
        }

        internal GaCompactBinaryTreeLeafNode(ulong id, T value)
        {
            Id = id;
            Value = value;
        }
    }
}