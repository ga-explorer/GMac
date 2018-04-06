using UtilLib.DataStructures.SparseTable;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public class GaSymMultivectorHashTable1D : SparseTable1D<int, IGaSymMultivector>
    {
        public override bool IsDefaultValue(IGaSymMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}