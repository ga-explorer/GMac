﻿using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;

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


        public override GaNumTerm MapToTerm(int id1, int id2)
        {
            return GaNumTerm.Create(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNegativeEGp(id1, id2)
                    ? -OrthonormalMetric[id1 & id2]
                    : OrthonormalMetric[id1 & id2]
            );
        }

        public GaNumTerm MapToTermLa(int id1, int id2, int id3)
        {
            var idXor12 = id1 ^ id2;
            var value = OrthonormalMetric[id1 & id2] * OrthonormalMetric[idXor12 & id3];

            if (GaNumFrameUtils.IsNegativeEGp(id1, id2) != GaNumFrameUtils.IsNegativeEGp(idXor12, id3))
                value = -value;

            return GaNumTerm.Create(TargetGaSpaceDimension, idXor12 ^ id3, value);
        }

        public GaNumTerm MapToTermRa(int id1, int id2, int id3)
        {
            var idXor23 = id2 ^ id3;
            var value = OrthonormalMetric[id1 & idXor23] * OrthonormalMetric[id2 & id3];

            if (GaNumFrameUtils.IsNegativeEGp(id1, idXor23) != GaNumFrameUtils.IsNegativeEGp(id2, id3))
                value = -value;

            return GaNumTerm.Create(TargetGaSpaceDimension, id1 ^ idXor23, value);
        }

    }
}