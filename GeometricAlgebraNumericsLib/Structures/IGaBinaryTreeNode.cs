namespace GeometricAlgebraNumericsLib.Structures
{
    public interface IGaBinaryTreeNode<T>
    {
        bool IsParentNode { get; }

        bool IsLeafNode { get; }

        IGaBinaryTreeNode<T> ChildNode0 { get; }

        IGaBinaryTreeNode<T> ChildNode1 { get; }

        GaBinaryTreeLeafNode<T> LeafChildNode0 { get; }

        GaBinaryTreeLeafNode<T> LeafChildNode1 { get; }
        
        T Value { get; }

        /// <summary>
        /// The number of child node levels under this node. For a leaf node this is zero, and
        /// for a root node this is equal to the vector space dimension of the space
        /// </summary>
        int TreeDepth { get; }

        bool HasChildNode0 { get; }

        bool HasChildNode1 { get; }

        bool HasNoChildNodes { get; }

        ulong BitMask { get; }
    }
}