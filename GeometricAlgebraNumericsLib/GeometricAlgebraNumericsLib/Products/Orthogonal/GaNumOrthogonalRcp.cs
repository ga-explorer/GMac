﻿using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products.Orthogonal
{
    public sealed class GaNumOrthogonalRcp : GaNumBilinearProductOrthogonal
    {
        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtRcpTerms(mv2, OrthogonalMetric).SumAsSarMultivector(TargetVSpaceDimension);
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtRcpTerms(mv2, OrthogonalMetric).SumAsDgrMultivector(TargetVSpaceDimension);
            }
        }


        internal GaNumOrthogonalRcp(GaNumMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.NoneAssociative)
        {
        }


        public override GaNumTerm MapToTerm(ulong id1, ulong id2)
        {
            return GaNumTerm.Create(
                TargetVSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNonZeroERcp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id2)
                        ? -OrthogonalMetric[(int)(id1 & id2)]
                        : OrthogonalMetric[(int)(id1 & id2)])
                    : 0.0d
            );
        }
    }
}
