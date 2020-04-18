using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymScalarBinaryTree2D
    {
        public int VSpaceDimension { get; }

        internal GaBtrInternalNode<GaSymScalarBinaryTree1D> RootNode { get; }

        public GaSymScalarBinaryTree1D this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue(VSpaceDimension, (ulong)id1, out var mvTree1D);

                return mvTree1D;
            }
            set =>
                RootNode.SetLeafValue(
                    VSpaceDimension,
                    (ulong)id1,
                    value ?? new GaSymScalarBinaryTree1D(VSpaceDimension)
                );
        }

        public MathematicaScalar this[int id1, int id2]
        {
            get
            {
                RootNode.TryGetLeafValue(VSpaceDimension, (ulong)id1, out var mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(VSpaceDimension, (ulong)id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymScalarBinaryTree1D(VSpaceDimension);

                    RootNode.SetLeafValue(VSpaceDimension, (ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }


        public GaSymScalarBinaryTree2D(int vSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<GaSymScalarBinaryTree1D>();

            VSpaceDimension = vSpaceDimension;
        }
    }
}