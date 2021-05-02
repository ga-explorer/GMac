using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Products
{
    public interface IGaNumOrthogonalGeometricProduct : IGaNumBilinearOrthogonalProduct
    {
        /// <summary>
        /// Compute the geometric product of 3 basis blades (b1 gp b2) gp b3
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        GaNumTerm MapToTermLa(ulong id1, ulong id2, ulong id3);

        /// <summary>
        /// Compute the geometric product of 3 basis blades b1 gp (b2 gp b3)
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        GaNumTerm MapToTermRa(ulong id1, ulong id2, ulong id3);
    }
}