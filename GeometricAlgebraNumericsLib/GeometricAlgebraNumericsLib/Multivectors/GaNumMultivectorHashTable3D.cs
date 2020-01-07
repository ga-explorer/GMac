using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public class GaNumMultivectorHashTable3D : GaSparseTable3D<int, int, int, IGaNumMultivector>
    {
        public override bool IsDefaultValue(IGaNumMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}