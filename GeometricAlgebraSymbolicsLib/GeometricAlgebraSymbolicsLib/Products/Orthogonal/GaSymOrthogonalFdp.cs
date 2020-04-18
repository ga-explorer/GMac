using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Products.Orthogonal
{
    public sealed class GaSymOrthogonalFdp : GaSymBilinearProductOrthogonal
    {
        internal GaSymOrthogonalFdp(GaSymMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GaNumFrameUtils.IsNonZeroEFdp(id1, id2))
                tempMultivector.SetTermCoef(
                    id1 ^ id2,
                    GaNumFrameUtils.IsNegativeEGp(id1, id2),
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
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForFdp(mv2, OrthogonalMetric));
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNonZeroEFdp(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id2)
                        ? Mfs.Minus[OrthogonalMetric[id1 & id2]]
                        : OrthogonalMetric[id1 & id2])
                    : Expr.INT_ZERO
            );
        }
    }
}
