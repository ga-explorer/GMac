using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public class GaNumMultivectorHashTable3D : GaSparseTable3D<ulong, ulong, ulong, IGaNumMultivector>
    {
        public override bool IsDefaultValue(IGaNumMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}