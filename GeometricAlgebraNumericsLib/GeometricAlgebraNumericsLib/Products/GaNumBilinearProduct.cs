using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Products
{
    public abstract class GaNumBilinearProduct : IGaNumMapBilinear
    {
        public GaNumMapBilinearAssociativity Associativity { get; }

        public abstract IGaNumMetric Metric { get; }

        public int TargetVSpaceDimension
            => Metric.VSpaceDimension;

        public int TargetGaSpaceDimension
            => Metric.GaSpaceDimension;

        public int DomainVSpaceDimension
            => Metric.VSpaceDimension;

        public int DomainGaSpaceDimension
            => Metric.GaSpaceDimension;

        public int DomainVSpaceDimension2
            => Metric.VSpaceDimension;

        public int DomainGaSpaceDimension2
            => Metric.GaSpaceDimension;

        public abstract IGaNumMultivector this[int id1, int id2] { get; }

        public abstract GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2] { get; }

        public abstract GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2] { get; }

        public GaNumSarMultivector this[params GaNumSarMultivector[] mvList]
        {
            get
            {
                if (Associativity == GaNumMapBilinearAssociativity.NoneAssociative)
                    throw new InvalidOperationException();

                var n = mvList.Length - 1;
                GaNumSarMultivector resultMv;

                if (Associativity == GaNumMapBilinearAssociativity.RightAssociative)
                {
                    resultMv = mvList[0];

                    for (var i = 1; i <= n; i++)
                        resultMv = this[resultMv, mvList[i]];
                }
                else
                {
                    resultMv = mvList[n];

                    for (var i = n - 1; i >= 0; i--)
                        resultMv = this[mvList[i], resultMv];
                }

                return resultMv;
            }
        }

        public GaNumDgrMultivector this[params GaNumDgrMultivector[] mvList]
        {
            get
            {
                if (Associativity == GaNumMapBilinearAssociativity.NoneAssociative)
                    throw new InvalidOperationException();

                var n = mvList.Length - 1;
                GaNumDgrMultivector resultMv;

                if (Associativity == GaNumMapBilinearAssociativity.RightAssociative)
                {
                    resultMv = mvList[0];

                    for (var i = 1; i <= n; i++)
                        resultMv = this[resultMv, mvList[i]];
                }
                else
                {
                    resultMv = mvList[n];

                    for (var i = n - 1; i >= 0; i--)
                        resultMv = this[mvList[i], resultMv];
                }

                return resultMv;
            }
        }


        public abstract IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        public abstract IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps();


        protected GaNumBilinearProduct(GaNumMapBilinearAssociativity associativity)
        {
            Associativity = associativity;
        }
    }
}
