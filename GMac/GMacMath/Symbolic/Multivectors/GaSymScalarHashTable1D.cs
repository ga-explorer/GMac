using SymbolicInterface.Mathematica;
using UtilLib.DataStructures.SparseTable;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public class GaSymScalarHashTable1D : SparseTable1D<int, Expr>
    {
        public override bool IsDefaultValue(Expr value)
        {
            return value.IsNullOrZero();
        }
    }
}