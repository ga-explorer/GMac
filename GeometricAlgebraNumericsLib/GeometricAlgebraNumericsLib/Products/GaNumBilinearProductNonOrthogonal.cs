using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Products
{
    public abstract class GaNumBilinearProductNonOrthogonal
        : GaNumBilinearProduct, IGaNumMapBilinear
    {
        public GaNumMetricNonOrthogonal NonOrthogonalMetric { get; }

        public override IGaNumMetric Metric
            => NonOrthogonalMetric;
        

        protected GaNumBilinearProductNonOrthogonal(GaNumMetricNonOrthogonal basisBladesSignatures, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            NonOrthogonalMetric = basisBladesSignatures;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0; id2 < DomainGaSpaceDimension2; id2++)
            {
                var mv = this[id1, id2];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, int, IGaNumMultivector>(id1, id2, mv);
            }
        }

        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension2; index2++)
            {
                var id1 = GaNumFrameUtils.BasisBladeId(1, index1);
                var id2 = GaNumFrameUtils.BasisBladeId(1, index2);
                var mv = this[id1, id2];

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, int, IGaNumMultivector>(index1, index2, mv);
            }
        }
    }
}