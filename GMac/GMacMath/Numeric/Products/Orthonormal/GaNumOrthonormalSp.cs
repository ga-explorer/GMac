using GMac.GMacMath.Numeric.Metrics;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Products.Orthonormal
{
    public sealed class GaNumOrthonormalSp : GaNumBilinearProductOrthonormal
    {
        internal GaNumOrthonormalSp(GaNumMetricOrthonormal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GMacNumericException("Multivector size mismatch");

            return
                GaNumMultivector
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(
                        mv1.GetBiTermsForESp(mv2),
                        OrthonormalMetric
                    );
        }

        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                0,
                GMacMathUtils.IsNonZeroESp(id1, id2)
                    ? (GMacMathUtils.IsNegativeEGp(id1, id1)
                        ? -OrthonormalMetric[id1]
                        : OrthonormalMetric[id1])
                    : 0.0d
            );
        }
    }
}
