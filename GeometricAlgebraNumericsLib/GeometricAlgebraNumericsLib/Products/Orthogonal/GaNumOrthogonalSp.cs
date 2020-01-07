using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products.Orthogonal
{
    public sealed class GaNumOrthogonalSp : GaNumBilinearProductOrthogonal
    {
        public override GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return
                    GaNumMultivector
                        .CreateZero(TargetGaSpaceDimension)
                        .AddFactors(mv1.GetBiTermsForSp(mv2, OrthogonalMetric));
            }
        }


        internal GaNumOrthogonalSp(GaNumMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.NoneAssociative)
        {
        }


        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                0,
                GaNumFrameUtils.IsNonZeroESp(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id1)
                        ? -OrthogonalMetric[id1]
                        : OrthogonalMetric[id1])
                    : 0.0d
            );
        }
    }
}