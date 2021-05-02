using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Products.Orthonormal
{
    public sealed class GaSymOrthonormalAcp : GaSymBilinearProductOrthonormal
    {
        internal GaSymOrthonormalAcp(GaSymMetricOrthonormal basisBladesSignatures) 
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            if (GaFrameUtils.IsNonZeroEAcp(id1, id2))
                tempMultivector.SetTermCoef(
                    id1 ^ id2,
                    GaFrameUtils.IsNegativeEGp(id1, id2),
                    OrthonormalMetric.GetExprSignature(id1 & id2)
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
                    .AddFactors(
                        mv1.GetBiTermsForEAcp(mv2),
                        OrthonormalMetric
                    );
        }

        public override GaSymMultivectorTerm MapToTerm(ulong id1, ulong id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetVSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNonZeroEAcp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id2)
                        ? Mfs.Minus[OrthonormalMetric.GetExprSignature(id1 & id2)]
                        : OrthonormalMetric.GetExprSignature(id1 & id2))
                    : Expr.INT_ZERO
            );
        }
    }
}