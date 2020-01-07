﻿using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products.Orthonormal
{
    public sealed class GaNumOrthonormalCp : GaNumBilinearProductOrthonormal
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
                        .AddFactors(
                            mv1.GetBiTermsForECp(mv2),
                            OrthonormalMetric
                        );
            }
        }


        internal GaNumOrthonormalCp(GaNumMetricOrthonormal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.NoneAssociative)
        {
        }


        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNonZeroECp(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id2)
                        ? -OrthonormalMetric[id1 & id2]
                        : OrthonormalMetric[id1 & id2])
                    : 0.0d
            );
        }
    }
}