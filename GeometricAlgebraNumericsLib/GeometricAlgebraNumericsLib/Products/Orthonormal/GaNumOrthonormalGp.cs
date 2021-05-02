using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products.Orthonormal
{
    public sealed class GaNumOrthonormalGp 
        : GaNumBilinearProductOrthonormal, IGaNumOrthogonalGeometricProduct
    {
        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtGpTerms(mv2, OrthonormalMetric).SumAsSarMultivector(TargetVSpaceDimension);
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtGpTerms(mv2, OrthonormalMetric).SumAsDgrMultivector(TargetVSpaceDimension);
            }
        }


        internal GaNumOrthonormalGp(GaNumMetricOrthonormal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.LeftRightAssociative)
        {
        }


        public override GaNumTerm MapToTerm(ulong id1, ulong id2)
        {
            return GaNumTerm.Create(
                TargetVSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2)
                    ? -OrthonormalMetric[(int)(id1 & id2)]
                    : OrthonormalMetric[(int)(id1 & id2)]
            );
        }

        public GaNumTerm MapToTermLa(ulong id1, ulong id2, ulong id3)
        {
            var idXor12 = id1 ^ id2;
            var value = OrthonormalMetric[(int)(id1 & id2)] * OrthonormalMetric[(int)(idXor12 & id3)];

            if (GaFrameUtils.IsNegativeEGp(id1, id2) != GaFrameUtils.IsNegativeEGp(idXor12, id3))
                value = -value;

            return GaNumTerm.Create(TargetVSpaceDimension, idXor12 ^ id3, value);
        }

        public GaNumTerm MapToTermRa(ulong id1, ulong id2, ulong id3)
        {
            var idXor23 = id2 ^ id3;
            var value = OrthonormalMetric[(int)(id1 & idXor23)] * OrthonormalMetric[(int)(id2 & id3)];

            if (GaFrameUtils.IsNegativeEGp(id1, idXor23) != GaFrameUtils.IsNegativeEGp(id2, id3))
                value = -value;

            return GaNumTerm.Create(TargetVSpaceDimension, id1 ^ idXor23, value);
        }

    }
}