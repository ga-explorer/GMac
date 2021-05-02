using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Products.Orthogonal
{
    public sealed class GaSymOrthogonalSp : GaSymBilinearProductOrthogonal
    {
        internal GaSymOrthogonalSp(GaSymMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            if (GaFrameUtils.IsNonZeroESp(id1, id2))
                tempMultivector.SetTermCoef(
                    0,
                    GaFrameUtils.IsNegativeEGp(id1, id1),
                    OrthogonalMetric[id1]
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
                    .AddFactors(mv1.GetBiTermsForSp(mv2, OrthogonalMetric));
        }

        public override GaSymMultivectorTerm MapToTerm(ulong id1, ulong id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetVSpaceDimension,
                0,
                GaFrameUtils.IsNonZeroESp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id1)
                        ? Mfs.Minus[OrthogonalMetric[id1]]
                        : OrthogonalMetric[id1])
                    : Expr.INT_ZERO
            );
        }
    }
}