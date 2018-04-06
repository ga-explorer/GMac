namespace GMac.GMacMath.Symbolic.Multivectors.Tree
{
    public interface IGaTreeMultivectorNode
    {
        bool IsRoot { get; }

        bool IsInternal { get; }

        bool IsLeaf { get; }

    }
}
