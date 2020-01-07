using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public sealed class GaSymOp : GaSymBilinearProductEuclidean
    {
        internal GaSymOp(int targetVSpaceDim) 
            : base(targetVSpaceDim)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GaNumFrameUtils.IsNonZeroOp(id1, id2))
                tempMultivector.SetTermCoef(
                    id1 ^ id2,
                    GaNumFrameUtils.IsNegativeEGp(id1, id2), 
                    Expr.INT_ONE
                );

            return tempMultivector;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(TargetGaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForOp(mv2));
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNonZeroOp(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id2) ? Expr.INT_MINUSONE : Expr.INT_ONE)
                    : Expr.INT_ZERO
            );
        }
    }
}