using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Products.Euclidean
{
    public sealed class GaNumEuclideanHip : GaNumBilinearProductEuclidean
    {
        internal GaNumEuclideanHip(int targetVSpaceDim) : base(targetVSpaceDim)
        {
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GaNumericsException("Multivector size mismatch");

            return
                GaNumMultivector
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForEHip(mv2));
        }

        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNonZeroEHip(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id2) ? -1.0d : 1.0d)
                    : 0.0d
            );
        }
    }
}