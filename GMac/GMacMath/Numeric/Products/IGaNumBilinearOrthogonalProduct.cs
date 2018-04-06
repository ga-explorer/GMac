using GMac.GMacMath.Numeric.Maps.Bilinear;
using GMac.GMacMath.Numeric.Multivectors;

namespace GMac.GMacMath.Numeric.Products
{
    public interface IGaNumBilinearOrthogonalProduct : IGaNumMapBilinear
    {
        GaNumMultivectorTerm MapToTerm(int id1, int id2);
    }
}