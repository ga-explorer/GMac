using UtilLib.DataStructures.SparseTable;

namespace GMac.GMacMath.Numeric.Multivectors
{
    public class GaNumMultivectorHashTable1D : SparseTable1D<int, IGaNumMultivector>
    {
        public override bool IsDefaultValue(IGaNumMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}
