using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Products.Orthonormal
{
    public sealed class GaSymOrthonormalSp : GaSymBilinearProductOrthonormal
    {
        internal GaSymOrthonormalSp(GaSymMetricOrthonormal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GaFrameUtils.IsNonZeroESp(id1, id2))
                tempMultivector.SetTermCoef(
                    0,
                    GaFrameUtils.IsNegativeEGp(id1, id1),
                    OrthonormalMetric.GetExprSignature(id1)
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
                    .AddFactors(
                        mv1.GetBiTermsForESp(mv2),
                        OrthonormalMetric
                    );
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                0,
                GaFrameUtils.IsNonZeroESp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id1)
                        ? Mfs.Minus[OrthonormalMetric.GetExprSignature(id1)]
                        : OrthonormalMetric.GetExprSignature(id1))
                    : Expr.INT_ZERO
            );
        }
    }
}