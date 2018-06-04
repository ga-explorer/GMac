namespace GeometricAlgebraNumericsLib.Structures
{
    public sealed class GaQuadTreeLeafNode<T> : IGaQuadTreeNode<T>
    {
        public bool IsParentNode => false;

        public bool IsLeafNode => true;

        public IGaQuadTreeNode<T> ChildNode00 => null;

        public IGaQuadTreeNode<T> ChildNode01 => null;

        public IGaQuadTreeNode<T> ChildNode10 => null;

        public IGaQuadTreeNode<T> ChildNode11 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode00 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode01 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode10 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode11 => null;

        public T Value { get; set; }

        public int TreeDepth => 0;

        public bool HasChildNode00 => false;

        public bool HasChildNode01 => false;

        public bool HasChildNode10 => false;

        public bool HasChildNode11 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GaQuadTreeLeafNode(T value)
        {
            Value = value;
        }
    }
}