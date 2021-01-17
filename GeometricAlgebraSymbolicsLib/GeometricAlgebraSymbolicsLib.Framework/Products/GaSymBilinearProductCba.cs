using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Maps.Unilinear;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public sealed class GaSymBilinearProductCba : GaSymBilinearProduct
    {
        public static GaSymBilinearProductCba CreateGp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Gp
            );
        }

        public static GaSymBilinearProductCba CreateSp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Sp
            );
        }

        public static GaSymBilinearProductCba CreateLcp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Lcp
            );
        }

        public static GaSymBilinearProductCba CreateRcp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Rcp
            );
        }

        public static GaSymBilinearProductCba CreateFdp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Fdp
            );
        }

        public static GaSymBilinearProductCba CreateHip(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Hip
            );
        }

        public static GaSymBilinearProductCba CreateAcp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Acp
            );
        }

        public static GaSymBilinearProductCba CreateCp(GaSymMetricNonOrthogonal metric)
        {
            return new GaSymBilinearProductCba(
                metric,
                metric.BaseFrame.Cp
            );
        }


        public GaSymMetricNonOrthogonal NonOrthogonalMetric { get; }

        public override IGaSymMetric Metric
            => NonOrthogonalMetric;

        public IGaSymMapUnilinear DerivedToBaseCba
            => NonOrthogonalMetric.DerivedToBaseCba;

        public IGaSymMapUnilinear BaseToDerivedCba
            => NonOrthogonalMetric.DerivedToBaseCba;

        public IGaSymMapBilinear BaseProductMap { get; }


        private GaSymBilinearProductCba(GaSymMetricNonOrthogonal metric, IGaSymMapBilinear baseProductMap)
        {
            NonOrthogonalMetric = metric;
            BaseProductMap = baseProductMap;
        }


        public override IGaSymMultivector this[int id1, int id2]
        {
            get
            {
                var baseMv1 = NonOrthogonalMetric.DerivedToBaseCba[id1].ToMultivector();
                var baseMv2 = NonOrthogonalMetric.DerivedToBaseCba[id2].ToMultivector();

                var baseMv = BaseProductMap[baseMv1, baseMv2];

                return NonOrthogonalMetric.BaseToDerivedCba[baseMv];
            }
        }
        

        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var baseMv1 = NonOrthogonalMetric.DerivedToBaseCba[id1].ToMultivector();
            var baseMv2 = NonOrthogonalMetric.DerivedToBaseCba[id2].ToMultivector();

            var baseMv = BaseProductMap[baseMv1, baseMv2];

            return NonOrthogonalMetric.BaseToDerivedCba.MapToTemp(baseMv);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            var baseMv1 = NonOrthogonalMetric.DerivedToBaseCba[mv1];
            var baseMv2 = NonOrthogonalMetric.DerivedToBaseCba[mv2];

            var baseMv = BaseProductMap[baseMv1, baseMv2];

            return NonOrthogonalMetric.BaseToDerivedCba.MapToTemp(baseMv);
        }

        public override IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0; id2 < DomainGaSpaceDimension2; id2++)
            {
                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, IGaSymMultivector>(id1, id2, mv);
            }
        }

        public override IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension2; index2++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, index1);
                var id2 = GaFrameUtils.BasisBladeId(1, index2);

                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<int, int, IGaSymMultivector>(index1, index2, mv);
            }
        }
    }
}