using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products
{
    public sealed class GaNumBilinearProductCba : GaNumBilinearProduct
    {
        public static GaNumBilinearProductCba CreateGp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Gp
            );
        }

        public static GaNumBilinearProductCba CreateSp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Sp
            );
        }

        public static GaNumBilinearProductCba CreateLcp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Lcp
            );
        }

        public static GaNumBilinearProductCba CreateRcp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Rcp
            );
        }

        public static GaNumBilinearProductCba CreateFdp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Fdp
            );
        }

        public static GaNumBilinearProductCba CreateHip(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Hip
            );
        }

        public static GaNumBilinearProductCba CreateAcp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Acp
            );
        }

        public static GaNumBilinearProductCba CreateCp(GaNumMetricNonOrthogonal metric)
        {
            return new GaNumBilinearProductCba(
                metric,
                metric.BaseFrame.Cp
            );
        }


        public GaNumMetricNonOrthogonal NonOrthogonalMetric { get; }

        public override IGaNumMetric Metric
            => NonOrthogonalMetric;

        public IGaNumMapUnilinear DerivedToBaseCba
            => NonOrthogonalMetric.DerivedToBaseCba;

        public IGaNumMapUnilinear BaseToDerivedCba
            => NonOrthogonalMetric.DerivedToBaseCba;

        public IGaNumMapBilinear BaseProductMap { get; }

        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                var baseMv1 = NonOrthogonalMetric.DerivedToBaseCba[id1].GetSarMultivector();
                var baseMv2 = NonOrthogonalMetric.DerivedToBaseCba[id2].GetSarMultivector();

                var baseMv = BaseProductMap[baseMv1, baseMv2];

                return NonOrthogonalMetric.BaseToDerivedCba[baseMv];
            }
        }

        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                var baseMv1 = NonOrthogonalMetric.DerivedToBaseCba[mv1];
                var baseMv2 = NonOrthogonalMetric.DerivedToBaseCba[mv2];

                var baseMv = BaseProductMap[baseMv1, baseMv2];

                return NonOrthogonalMetric.BaseToDerivedCba[baseMv];
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                var baseMv1 = NonOrthogonalMetric.DerivedToBaseCba[mv1];
                var baseMv2 = NonOrthogonalMetric.DerivedToBaseCba[mv2];

                var baseMv = BaseProductMap[baseMv1, baseMv2];

                return NonOrthogonalMetric.BaseToDerivedCba[baseMv];
            }
        }


        private GaNumBilinearProductCba(GaNumMetricNonOrthogonal metric, IGaNumMapBilinear baseProductMap)
            : base(baseProductMap.Associativity)
        {
            NonOrthogonalMetric = metric;
            BaseProductMap = baseProductMap;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0; id2 < DomainGaSpaceDimension2; id2++)
            {
                var mv = this[id1, id2];

                if (!mv.IsNullOrEmpty())
                    yield return Tuple.Create(id1, id2, mv);
            }
        }

        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension2; index2++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, index1);
                var id2 = GaFrameUtils.BasisBladeId(1, index2);
                var mv = this[id1, id2];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, int, IGaNumMultivector>(index1, index2, mv);
            }
        }
    }
}
