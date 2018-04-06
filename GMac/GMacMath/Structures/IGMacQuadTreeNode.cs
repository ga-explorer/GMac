namespace GMac.GMacMath.Structures
{
    public interface IGMacQuadTreeNode<T>
    {
        bool IsParentNode { get; }

        bool IsLeafNode { get; }

        IGMacQuadTreeNode<T> ChildNode00 { get; }

        IGMacQuadTreeNode<T> ChildNode01 { get; }

        IGMacQuadTreeNode<T> ChildNode10 { get; }

        IGMacQuadTreeNode<T> ChildNode11 { get; }

        GMacQuadTreeLeafNode<T> LeafChildNode00 { get; }

        GMacQuadTreeLeafNode<T> LeafChildNode01 { get; }

        GMacQuadTreeLeafNode<T> LeafChildNode10 { get; }

        GMacQuadTreeLeafNode<T> LeafChildNode11 { get; }

        T Value { get; }

        /// <summary>
        /// The number of child node levels under this node. For a leaf node this is zero, and
        /// for a root node this is equal to the vector space dimension of the space
        /// </summary>
        int TreeDepth { get; }

        bool HasChildNode00 { get; }

        bool HasChildNode01 { get; }

        bool HasChildNode10 { get; }

        bool HasChildNode11 { get; }

        bool HasNoChildNodes { get; }

        ulong BitMask { get; }
    }
}