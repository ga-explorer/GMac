using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymScalarBinaryTree1D
    {
        public int VSpaceDimension { get; }

        internal GaBtrInternalNode<Expr> RootNode { get; }

        public MathematicaScalar this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue(VSpaceDimension, (ulong)id1, out var scalar);

                return scalar?.ToMathematicaScalar() ?? GaSymbolicsUtils.Constants.Zero;
            }
            set =>
                RootNode.SetLeafValue(
                    VSpaceDimension,
                    (ulong)id1,
                    value?.Expression ?? Expr.INT_ZERO
                );
        }


        public GaSymScalarBinaryTree1D(int vSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<Expr>();

            VSpaceDimension = vSpaceDimension;
        }
    }
}