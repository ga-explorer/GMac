using GeometricAlgebraStructuresLib.MotherAlgebra.Basis;

namespace GeometricAlgebraStructuresLib.MotherAlgebra
{
    public sealed class GamMultivectorTerm<T>
    {
        public GamBasisBlade BasisBladeId { get; } 

        public T Scalar { get; set; }


        internal GamMultivectorTerm(ulong positiveId, ulong negativeId, ulong zeroId, T scalar)
        {
            BasisBladeId = GamBasisBlade.Create(positiveId, negativeId, zeroId);

            Scalar = scalar;
        }
    }
}