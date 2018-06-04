using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public class GaNumMultivectorHashTable1D : GaSparseTable1D<int, IGaNumMultivector>
    {
        public override bool IsDefaultValue(IGaNumMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}
