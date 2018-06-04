using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymScalarBinaryTree1D
    {
        internal GaBinaryTree<Expr> RootNode { get; }

        public MathematicaScalar this[int id1]
        {
            get
            {
                Expr scalar;
                RootNode.TryGetLeafValue((ulong)id1, out scalar);

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
            RootNode = new GaBinaryTree<Expr>(vSpaceDimension);
        }
    }
}