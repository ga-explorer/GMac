using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products.Euclidean
{
    public sealed class GaNumEuclideanGp : GaNumBilinearProductEuclidean
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
                        .AddFactors(mv1.GetBiTermsForEGp(mv2));
            }
        }


        internal GaNumEuclideanGp(int targetVSpaceDim) 
            : base(targetVSpaceDim, GaNumMapBilinearAssociativity.LeftRightAssociative)
        {
        }


        public override GaNumMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaNumMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaNumFrameUtils.IsNegativeEGp(id1, id2) ? -1.0d : 1.0d
            );
        }
    }
}