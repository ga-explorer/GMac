using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products.Orthonormal
{
    public sealed class GaNumOrthonormalAcp : GaNumBilinearProductOrthonormal
    {
        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtAcpTerms(mv2, OrthonormalMetric).SumAsSarMultivector(TargetVSpaceDimension);
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtAcpTerms(mv2, OrthonormalMetric).SumAsDgrMultivector(TargetVSpaceDimension);
            }
        }


        internal GaNumOrthonormalAcp(GaNumMetricOrthonormal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.NoneAssociative)
        {
        }


        public override GaNumTerm MapToTerm(int id1, int id2)
        {
            return GaNumTerm.Create(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNonZeroEAcp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id2)
                        ? -OrthonormalMetric[id1 & id2]
                        : OrthonormalMetric[id1 & id2])
                    : 0.0d
            );
        }
    }
}