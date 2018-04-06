using GMac.GMacMath.Structures;
using SymbolicInterface.Mathematica.Expression;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public sealed class GaSymScalarBinaryTree1D
    {
        internal GMacBinaryTree<Expr> RootNode { get; }

        public MathematicaScalar this[int id1]
        {
            get
            {
                Expr scalar;
                RootNode.TryGetLeafValue((ulong)id1, out scalar);

                return scalar?.ToMathematicaScalar() ?? SymbolicUtils.Constants.Zero;
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
            RootNode = new GMacBinaryTree<Expr>(vSpaceDimension);
        }
    }
}