namespace GeometricAlgebraNumericsLib.Structures
{
    public struct GaCompactBinaryTreeEmptyNode<T> : IGaCompactBinaryTreeNode<T>
    {
        public bool IsInternalNode => false;

        public bool IsLeafNode => false;

        public bool IsEmptyNode => true;

        public int ChildNodeIndex0 => -1;

        public int ChildNodeIndex1 => -1;

        public ulong Id => 0ul;

        public T Value => default(T);

        public int TreeDepth => -1;

        public bool HasChildNode0 => false;

        public bool HasChildNode1 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;
    }
}