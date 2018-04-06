using GMac.GMacMath.Symbolic.Multivectors;

namespace GMac.GMacMath.Symbolic.Products
{
    public interface IGaSymBilinearOrthogonalProduct
    {
        GaSymMultivectorTerm MapToTerm(int id1, int id2);
    }
}
