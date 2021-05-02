using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public class GaSymScalarHashTable1D : GaSparseTable1D<int, Expr>
    {
        public override bool IsDefaultValue(Expr value)
        {
            return value.IsNullOrZero();
        }
    }
}