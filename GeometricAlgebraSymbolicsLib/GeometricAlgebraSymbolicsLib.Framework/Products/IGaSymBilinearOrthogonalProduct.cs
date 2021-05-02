using GeometricAlgebraSymbolicsLib.Multivectors;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public interface IGaSymBilinearOrthogonalProduct
    {
        GaSymMultivectorTerm MapToTerm(ulong id1, ulong id2);
    }
}
