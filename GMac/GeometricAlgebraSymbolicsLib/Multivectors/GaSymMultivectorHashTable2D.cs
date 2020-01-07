using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public class GaSymMultivectorHashTable2D : GaSparseTable2D<int, int, IGaSymMultivector>
    {
        public override bool IsDefaultValue(IGaSymMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}
