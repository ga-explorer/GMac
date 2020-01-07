using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymScalarBinaryTree2D
    {
        internal GaBinaryTreeInternalNode<GaSymScalarBinaryTree1D> RootNode { get; }

        public GaSymScalarBinaryTree1D this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue((ulong)id1, out var mvTree1D);

                return mvTree1D;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1,
                    value ?? new GaSymScalarBinaryTree1D(RootNode.TreeDepth)
                );
            }
        }

        public MathematicaScalar this[int id1, int id2]
        {
            get
            {
                RootNode.TryGetLeafValue((ulong)id1, out var mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                if (!RootNode.TryGetLeafValue((ulong)id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymScalarBinaryTree1D(RootNode.TreeDepth);

                    RootNode.SetLeafValue((ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }


        public GaSymScalarBinaryTree2D(int vSpaceDimension)
        {
            RootNode = new GaBinaryTreeInternalNode<GaSymScalarBinaryTree1D>(
                0,
                vSpaceDimension
            );
        }
    }
}