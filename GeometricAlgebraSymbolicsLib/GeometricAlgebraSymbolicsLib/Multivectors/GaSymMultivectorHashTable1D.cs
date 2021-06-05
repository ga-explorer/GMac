using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public class GaSymMultivectorHashTable1D : GaSparseTable1D<ulong, IGaSymMultivector>
    {
        public override bool IsDefaultValue(IGaSymMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}