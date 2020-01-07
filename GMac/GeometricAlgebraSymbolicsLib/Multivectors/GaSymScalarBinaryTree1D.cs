using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymScalarBinaryTree1D
    {
        internal GaBinaryTreeInternalNode<Expr> RootNode { get; }

        public MathematicaScalar this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue((ulong)id1, out var scalar);

                return scalar?.ToMathematicaScalar() ?? GaSymbolicsUtils.Constants.Zero;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1,
                    value?.Expression ?? Expr.INT_ZERO
                );
            }
        }


        public GaSymScalarBinaryTree1D(int vSpaceDimension)
        {
            RootNode = new GaBinaryTreeInternalNode<Expr>(
                0, 
                vSpaceDimension
            );
        }
    }
}