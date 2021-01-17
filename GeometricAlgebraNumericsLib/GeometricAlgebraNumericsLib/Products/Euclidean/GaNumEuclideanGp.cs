using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products.Euclidean
{
    public sealed class GaNumEuclideanGp 
        : GaNumBilinearProductEuclidean, IGaNumOrthogonalGeometricProduct
    {
        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtEGpTerms(mv2).SumAsSarMultivector(TargetVSpaceDimension);
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtEGpTerms(mv2).SumAsDgrMultivector(TargetVSpaceDimension);
            }
        }


        internal GaNumEuclideanGp(int targetVSpaceDim) 
            : base(targetVSpaceDim, GaNumMapBilinearAssociativity.LeftRightAssociative)
        {
        }


        public override GaNumTerm MapToTerm(int id1, int id2)
        {
            return GaNumTerm.Create(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNegativeEGp(id1, id2) ? -1.0d : 1.0d
            );
        }

        public GaNumTerm MapToTermLa(int id1, int id2, int id3)
        {
            var idXor12 = id1 ^ id2;
            var value = 
                (GaFrameUtils.IsNegativeEGp(id1, id2) != GaFrameUtils.IsNegativeEGp(idXor12, id3)) 
                    ? -1 : 1;

            return GaNumTerm.Create(TargetGaSpaceDimension, idXor12 ^ id3, value);
        }

        public GaNumTerm MapToTermRa(int id1, int id2, int id3)
        {
            var idXor23 = id2 ^ id3;
            var value =
                (GaFrameUtils.IsNegativeEGp(id1, idXor23) != GaFrameUtils.IsNegativeEGp(id2, id3))
                    ? -1 : 1;

            return GaNumTerm.Create(TargetGaSpaceDimension, id1 ^ idXor23, value);
        }
    }
}