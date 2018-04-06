using UtilLib.DataStructures.SparseTable;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public class GaSymMultivectorHashTable2D : SparseTable2D<int, int, IGaSymMultivector>
    {
        public override bool IsDefaultValue(IGaSymMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}
