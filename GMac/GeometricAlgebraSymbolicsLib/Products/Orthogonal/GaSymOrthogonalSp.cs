using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Products.Orthogonal
{
    public sealed class GaSymOrthogonalSp : GaSymBilinearProductOrthogonal
    {
        internal GaSymOrthogonalSp(GaSymMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GaNumFrameUtils.IsNonZeroESp(id1, id2))
                tempMultivector.SetTermCoef(
                    0,
                    GaNumFrameUtils.IsNegativeEGp(id1, id1),
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
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForSp(mv2, OrthogonalMetric));
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                0,
                GaNumFrameUtils.IsNonZeroESp(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id1)
                        ? Mfs.Minus[OrthogonalMetric[id1]]
                        : OrthogonalMetric[id1])
                    : Expr.INT_ZERO
            );
        }
    }
}