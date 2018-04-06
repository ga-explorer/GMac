using GMac.GMacMath.Numeric.Metrics;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Products.Orthonormal
{
    public sealed class GaNumOrthonormalLcp : GaNumBilinearProductOrthonormal
    {
        internal GaNumOrthonormalLcp(GaNumMetricOrthonormal basisBladesSignatures)
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
                        mv1.GetBiTermsForELcp(mv2), 
                        OrthonormalMetric
                    );
        }

        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GMacMathUtils.IsNonZeroELcp(id1, id2)
                    ? (GMacMathUtils.IsNegativeEGp(id1, id2)
                        ? -OrthonormalMetric[id1 & id2]
                        : OrthonormalMetric[id1 & id2])
                    : 0.0d
            );
        }
    }
}