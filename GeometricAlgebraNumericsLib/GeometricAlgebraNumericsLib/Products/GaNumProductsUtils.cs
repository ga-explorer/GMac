using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products
{
    public static class GaNumProductsUtils
    {
        //public static GaNumImmutableMultivector Op(this GaNumImmutableMultivector mv1, GaNumImmutableMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    return GaNumImmutableMultivector.CreateFromTerms(
        //        mv1.GaSpaceDimension,
        //        GaNumBtrMultivector
        //            .CreateZero(mv1.GaSpaceDimension)
        //            .AddFactors(mv1.GetBiTermsForOp(mv2))
        //            .NonZeroTerms
        //    );
        //}

        //private static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsOp(GaNumKVectorMultivector mv1, GaNumKVectorMultivector mv2)
        //{
        //    return mv1.Terms.SelectMany(
        //        term1 => mv2.Terms,
        //        (term1, term2) => new GaNumMultivectorBiTerm(
        //            term1.Key, 
        //            term2.Key, 
        //            term1.Value, 
        //            term2.Value)
        //    );
        //}

        //public static GaNumKVector Op1(this GaNumKVector mv1, GaNumKVector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    if (mv1.Grade + mv2.Grade > mv1.VSpaceDimension)
        //        return GaNumKVector.CreateScalar(mv1.GaSpaceDimension);

        //    var resultMv = GaNumKVector.Create(mv1.GaSpaceDimension, mv1.Grade + mv2.Grade);

        //    for (var index1 = 0; index1 < mv1.TermsCount; index1++)
        //    {
        //        var id1 = mv1.GetBasisBladeId(index1);
        //        var value1 = mv1.GetTermValue(index1);

        //        var factors = mv2.Terms
        //            //.AsParallel()
        //            .Where(term2 => (id1 & term2.Key) == 0)
        //            .Select(term2 => Tuple.Create(
        //                (id1 | term2.Key).BasisBladeIndex(),
        //                GaNumFrameUtils.IsNegativeEGp(id1, term2.Key)
        //                    ? (-value1 * term2.Value)
        //                    : (value1 * term2.Value)
        //            ));

        //        foreach (var factor in factors)
        //            resultMv.UpdateTermValue(factor.Item1, factor.Item2);

        //    }

        //    return resultMv;
        //}

        public static GaNumSarMultivector Op(this GaNumSarMultivector mv1, IEnumerable<GaNumSarMultivector> mvList)
        {
            return mvList.Aggregate(mv1, (current, mv) => current.Op(mv));
        }

        public static GaNumDarKVector Op(this IGaNumKVector mv1, IGaNumKVector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            if (mv1.Grade + mv2.Grade > mv1.VSpaceDimension)
                return GaNumDarKVector.CreateScalar(mv1.VSpaceDimension, 1);

            var scalarValuesLength = 
                GaFrameUtils.KvSpaceDimension(mv1.VSpaceDimension, mv1.Grade + mv2.Grade);

            var scalarValues = new double[scalarValuesLength];

            for (var index1 = 0; index1 < mv1.StoredTermsCount; index1++)
            {
                var id1 = GaFrameUtils.BasisBladeId(mv1.Grade, index1);
                var value1 = mv1.ScalarValuesArray[index1];

                for (var index2 = 0; index2 < mv2.StoredTermsCount; index2++)
                {
                    var id2 = GaFrameUtils.BasisBladeId(mv2.Grade, index2);

                    if ((id1 & id2) == 0)
                    {
                        var value2 = mv2.ScalarValuesArray[index2];

                        var index = (id1 | id2).BasisBladeIndex();
                        var value = GaFrameUtils.IsNegativeEGp(id1, id2)
                            ? (-value1 * value2)
                            : (value1 * value2);

                        scalarValues[index] += value;
                    }
                }
            }

            return new GaNumDarKVector(mv1.VSpaceDimension, mv1.Grade + mv2.Grade, scalarValues);
        }

        public static GaNumSarMultivector Op(this GaNumSarMultivector[] mvArray)
        {
            return mvArray.Skip(1).Aggregate(
                mvArray[0],
                (current, mv) => current.Op(mv)
            );
        }

        public static GaNumSarMultivector Op(this GaNumSarMultivector mv1, params GaNumSarMultivector[] mvList)
        {
            return mvList.Aggregate(mv1, (current, mv) => current.Op(mv));
        }

        public static GaNumSarMultivector EGp(this GaNumSarMultivector mv1, params GaNumSarMultivector[] mvList)
        {
            return mvList.Aggregate(mv1, (current, mv) => current.EGp(mv));
        }
    }
}
