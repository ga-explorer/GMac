using UtilLib.DataStructures.SparseTable;

namespace GMac.GMacMath.Numeric.Multivectors
{
    public class GaNumMultivectorHashTable3D : SparseTable3D<int, int, int, IGaNumMultivector>
    {
        public override bool IsDefaultValue(IGaNumMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}