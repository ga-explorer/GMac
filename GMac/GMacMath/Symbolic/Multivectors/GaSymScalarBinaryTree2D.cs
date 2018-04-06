using GMac.GMacMath.Structures;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public sealed class GaSymScalarBinaryTree2D
    {
        internal GMacBinaryTree<GaSymScalarBinaryTree1D> RootNode { get; }

        public GaSymScalarBinaryTree1D this[int id1]
        {
            get
            {
                GaSymScalarBinaryTree1D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

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
                GaSymScalarBinaryTree1D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                GaSymScalarBinaryTree1D mvTree1D;
                if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymScalarBinaryTree1D(RootNode.TreeDepth);

                    RootNode.SetLeafValue((ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }


        public GaSymScalarBinaryTree2D(int vSpaceDimension)
        {
            RootNode = new GMacBinaryTree<GaSymScalarBinaryTree1D>(vSpaceDimension);
        }
    }
}