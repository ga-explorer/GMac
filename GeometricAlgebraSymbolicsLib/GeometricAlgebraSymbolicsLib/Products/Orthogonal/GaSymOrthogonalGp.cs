﻿using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Products.Orthogonal
{
    public sealed class GaSymOrthogonalGp : GaSymBilinearProductOrthogonal
    {
        internal GaSymOrthogonalGp(GaSymMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            tempMultivector.SetTermCoef(
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2),
                OrthogonalMetric[id1 & id2]
            );

            return tempMultivector;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GaSymbolicsException("Multivector size mismatch");

            return
                GaSymMultivector
                    .CreateZeroTemp(TargetVSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForGp(mv2, OrthogonalMetric));
        }

        public override GaSymMultivectorTerm MapToTerm(ulong id1, ulong id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetVSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2)
                    ? Mfs.Minus[OrthogonalMetric[id1 & id2]]
                    : OrthogonalMetric[id1 & id2]
            );
        }
    }
}