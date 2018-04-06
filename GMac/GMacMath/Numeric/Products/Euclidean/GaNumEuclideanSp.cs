using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;

namespace GMac.GMacMath.Numeric.Products.Euclidean
{
    public sealed class GaNumEuclideanSp : GaNumBilinearProductEuclidean
    {
        internal GaNumEuclideanSp(int targetVSpaceDim) : base(targetVSpaceDim)
        {
        }


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GMacNumericException("Multivector size mismatch");

            return
                GaNumMultivector
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForESp(mv2));
        }

        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                0,
                GMacMathUtils.IsNonZeroESp(id1, id2)
                    ? (GMacMathUtils.IsNegativeEGp(id1, id1) ? -1.0d : 1.0d)
                    : 0.0d
            );
        }
    }
}