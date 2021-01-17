namespace GeometricAlgebraSymbolicsLib.Multivectors.Tree
{
    public sealed class GaTreeMultivectorNode : IGaTreeMultivectorParentNode
    {
        public bool IsRoot { get; } = false;

        public bool IsInternal { get; } = true;

        public bool IsLeaf { get; } = false;

        public IGaTreeMultivectorNode LeftChild { get; internal set; }

        public IGaTreeMultivectorNode RightChild { get; internal set; }

        public bool HasLeftChild => !ReferenceEquals(LeftChild, null);

        public bool HasRightChild => !ReferenceEquals(RightChild, null);
    }
}
