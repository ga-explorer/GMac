namespace GeometricAlgebraSymbolicsLib.Multivectors.Tree
{
    public interface IGaTreeMultivectorNode
    {
        bool IsRoot { get; }

        bool IsInternal { get; }

        bool IsLeaf { get; }

    }
}
