using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products.Euclidean
{
    public sealed class GaNumEuclideanRcp : GaNumBilinearProductEuclidean
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
                        .AddFactors(mv1.GetBiTermsForERcp(mv2));
            }
        }


        internal GaNumEuclideanRcp(int targetVSpaceDim) 
            : base(targetVSpaceDim, GaNumMapBilinearAssociativity.NoneAssociative)
        {
        }


        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNonZeroERcp(id1, id2)
                    ? (GaNumFrameUtils.IsNegativeEGp(id1, id2) ? -1.0d : 1.0d)
                    : 0.0d
            );
        }
    }
}