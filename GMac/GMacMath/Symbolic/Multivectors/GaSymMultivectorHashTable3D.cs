using UtilLib.DataStructures.SparseTable;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public class GaSymMultivectorHashTable3D : SparseTable3D<int, int, int, IGaSymMultivector>
    {
        public override bool IsDefaultValue(IGaSymMultivector value)
        {
            return value.IsNullOrZero();
        }
    }
}