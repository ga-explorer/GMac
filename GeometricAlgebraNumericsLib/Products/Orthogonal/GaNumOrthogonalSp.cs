using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Products.Orthogonal
{
    public sealed class GaNumOrthogonalSp : GaNumBilinearProductOrthogonal
    {
        internal GaNumOrthogonalSp(GaNumMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GaNumericsException("Multivector size mismatch");

            return
                GaNumMultivector
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForSp(mv2, OrthogonalMetric));
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