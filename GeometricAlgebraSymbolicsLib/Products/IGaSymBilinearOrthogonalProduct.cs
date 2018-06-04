using GeometricAlgebraSymbolicsLib.Multivectors;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public interface IGaSymBilinearOrthogonalProduct
    {
        GaSymMultivectorTerm MapToTerm(int id1, int id2);
    }
}
