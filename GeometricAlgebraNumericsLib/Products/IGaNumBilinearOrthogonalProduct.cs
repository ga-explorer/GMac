using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products
{
    public interface IGaNumBilinearOrthogonalProduct : IGaNumMapBilinear
    {
        GaNumMultivectorTerm MapToTerm(int id1, int id2);
    }
}