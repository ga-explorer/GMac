using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products
{
    public static class GaNumProductsUtils
    {
        public static GaNumImmutableMultivector Op(this GaNumImmutableMultivector mv1, GaNumImmutableMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumImmutableMultivector.CreateFromTerms(
                mv1.GaSpaceDimension,
                GaNumMultivector
                    .CreateZero(mv1.GaSpaceDimension)
                    .AddFactors(mv1.GetBiTermsForOp(mv2))
                    .NonZeroTerms
            );
        }

        private static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsOp(GaNumKVector mv1, GaNumKVector mv2)
        {
            return mv1.Terms.SelectMany(
                term1 => mv2.Terms,
                (term1, term2) => new GaNumMultivectorBiTerm(
                    term1.Key, 
                    term2.Key, 
                    term1.Value, 
                    term2.Value)
            );
        }

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

        public static GaNumKVector ComputeVectorKVectorOp(this GaNumKVector vector, GaNumKVector kVector)
        {
            var resultMv = GaNumKVector.Create(vector.GaSpaceDimension, vector.Grade + kVector.Grade);

            var maxId = vector.GaSpaceDimension - 1;
            for (var index2 = 0; index2 < kVector.TermsCount; index2++)
            {
                var id2 = kVector.GetBasisBladeId(index2);
                var value2 = kVector.GetTermValue(index2);

                if (value2 == 0)
                    continue;

                var indexList = (id2 ^ maxId).PatternToPositions();
                foreach (var index1 in indexList)
                {
                    var id1 = 1 << index1;
                    var value1 = vector.GetTermValue(index1);

                    var index = (id1 | id2).BasisBladeIndex();
                    var value = GaNumFrameUtils.IsNegativeVectorEGp(index1, id2)
                        ? (-value1 * value2)
                        : (value1 * value2);

                    resultMv.UpdateTermValue(index, value);
                }
            }

            return resultMv;
        }

        public static GaNumKVector Op(this GaNumKVector mv1, GaNumKVector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            if (mv1.Grade + mv2.Grade > mv1.VSpaceDimension)
                return GaNumKVector.CreateScalar(mv1.GaSpaceDimension);

            var resultMv = GaNumKVector.Create(mv1.GaSpaceDimension, mv1.Grade + mv2.Grade);
            
            for (var index1 = 0; index1 < mv1.TermsCount; index1++)
            {
                var id1 = mv1.GetBasisBladeId(index1);
                var value1 = mv1.GetTermValue(index1);

                for(var index2 = 0; index2 < mv2.TermsCount; index2++)
                {
                    var id2 = mv2.GetBasisBladeId(index2);

                    if ((id1 & id2) == 0)
                    {
                        var value2 = mv2.GetTermValue(index2);

                        var index = (id1 | id2).BasisBladeIndex();
                        var value = GaNumFrameUtils.IsNegativeEGp(id1, id2)
                            ? (-value1 * value2)
                            : (value1 * value2);
                        
                        resultMv.UpdateTermValue(index, value);
                    }
                }
            }

            return resultMv;
        }

        public static GaNumKVector Op(this GaNumKVector[] mvArray)
        {
            return mvArray.Skip(1).Aggregate(
                mvArray[0],
                (current, mv) => current.Op(mv)
            );
        }

        public static GaNumMultivector Op(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForOp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector Op(this GaNumMultivector[] mvArray)
        {
            return mvArray.Skip(1).Aggregate(
                mvArray[0], 
                (current, mv) => current.Op(mv)
            );
        }

        public static GaNumMultivector Op(this GaNumMultivector mv1, params GaNumMultivector[] mvList)
        {
            return mvList.Aggregate(mv1, (current, mv) => current.Op(mv));
        }

        public static GaNumMultivector Op(this GaNumMultivector mv1, IEnumerable<GaNumMultivector> mvList)
        {
            return mvList.Aggregate(mv1, (current, mv) => current.Op(mv));
        }

        public static GaNumMultivector EGp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEGp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EGp(this GaNumMultivector mv1, params GaNumMultivector[] mvList)
        {
            return mvList.Aggregate(mv1, (current, mv) => current.EGp(mv));
        }

        public static GaNumMultivector ESp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForESp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ELcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForELcp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ERcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForERcp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EFdp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEFdp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EHip(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEHip(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EAcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEAcp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ECp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZero(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForECp(mv2))
                .ToMultivector();
        }

        public static double EMagnitude(this GaNumMultivector mv)
        {
            return Math.Sqrt(ESp(mv, mv.Reverse())[0]);
        }

        public static double EMagnitude2(this GaNumMultivector mv)
        {
            return ESp(mv, mv.Reverse())[0];
        }
    }
}
