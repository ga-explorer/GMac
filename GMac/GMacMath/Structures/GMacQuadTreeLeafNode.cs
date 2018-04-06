namespace GMac.GMacMath.Structures
{
    public sealed class GMacQuadTreeLeafNode<T> : IGMacQuadTreeNode<T>
    {
        public bool IsParentNode => false;

        public bool IsLeafNode => true;

        public IGMacQuadTreeNode<T> ChildNode00 => null;

        public IGMacQuadTreeNode<T> ChildNode01 => null;

        public IGMacQuadTreeNode<T> ChildNode10 => null;

        public IGMacQuadTreeNode<T> ChildNode11 => null;

        public GMacQuadTreeLeafNode<T> LeafChildNode00 => null;

        public GMacQuadTreeLeafNode<T> LeafChildNode01 => null;

        public GMacQuadTreeLeafNode<T> LeafChildNode10 => null;

        public GMacQuadTreeLeafNode<T> LeafChildNode11 => null;

        public T Value { get; set; }

        public int TreeDepth => 0;

        public bool HasChildNode00 => false;

        public bool HasChildNode01 => false;

        public bool HasChildNode10 => false;

        public bool HasChildNode11 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GMacQuadTreeLeafNode(T value)
        {
            Value = value;
        }
    }
}