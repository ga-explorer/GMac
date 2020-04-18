using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Products
{
    public interface IGaNumBilinearOrthogonalProduct : IGaNumMapBilinear
    {
        GaNumTerm MapToTerm(int id1, int id2);
    }
}