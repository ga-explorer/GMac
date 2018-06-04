using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public class GaSymScalarHashTable2D : GaSparseTable2D<int, int, Expr>
    {
        public override bool IsDefaultValue(Expr value)
        {
            return value.IsNullOrZero();
        }
    }
}