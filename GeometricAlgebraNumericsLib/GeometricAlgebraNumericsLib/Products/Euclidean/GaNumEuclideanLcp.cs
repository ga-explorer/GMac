using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products.Euclidean
{
    public sealed class GaNumEuclideanLcp : GaNumBilinearProductEuclidean
    {
        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtELcpTerms(mv2).SumAsSarMultivector(TargetVSpaceDimension);
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                return mv1.GetGbtELcpTerms(mv2).SumAsDgrMultivector(TargetVSpaceDimension);
            }
        }


        internal GaNumEuclideanLcp(int targetVSpaceDim) 
            : base(targetVSpaceDim, GaNumMapBilinearAssociativity.NoneAssociative)
        {
        }


        public override GaNumTerm MapToTerm(int id1, int id2)
        {
            return GaNumTerm.Create(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNonZeroELcp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id2) ? -1.0d : 1.0d)
                    : 0.0d
            );
        }
    }
}