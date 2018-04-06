namespace GMac.GMacMath.Symbolic.Multivectors.Tree
{
    public interface IGaTreeMultivectorParentNode : IGaTreeMultivectorNode
    {
        IGaTreeMultivectorNode LeftChild { get; }

        IGaTreeMultivectorNode RightChild { get; }

        bool HasLeftChild { get; }

        bool HasRightChild { get; }
    }
}