using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public class GaSymMultivectorHashTable3D : GaSparseTable3D<int, int, int, IGaSymMultivector>
    {
        public override bool IsDefaultValue(IGaSymMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}