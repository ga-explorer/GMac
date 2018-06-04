namespace GeometricAlgebraNumericsLib.Structures
{
    public interface IGaQuadTreeNode<T>
    {
        bool IsParentNode { get; }

        bool IsLeafNode { get; }

        IGaQuadTreeNode<T> ChildNode00 { get; }

        IGaQuadTreeNode<T> ChildNode01 { get; }

        IGaQuadTreeNode<T> ChildNode10 { get; }

        IGaQuadTreeNode<T> ChildNode11 { get; }

        GaQuadTreeLeafNode<T> LeafChildNode00 { get; }

        GaQuadTreeLeafNode<T> LeafChildNode01 { get; }

        GaQuadTreeLeafNode<T> LeafChildNode10 { get; }

        GaQuadTreeLeafNode<T> LeafChildNode11 { get; }

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