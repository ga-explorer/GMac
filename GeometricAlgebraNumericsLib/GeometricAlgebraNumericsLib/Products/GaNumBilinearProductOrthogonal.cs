﻿using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products
{
    public abstract class GaNumBilinearProductOrthogonal 
        : GaNumBilinearProduct, IGaNumBilinearOrthogonalProduct
    {
        public GaNumMetricOrthogonal OrthogonalMetric { get; }

        public override IGaNumMetric Metric
            => OrthogonalMetric;

        public override IGaNumMultivector this[int id1, int id2]
            => MapToTerm(id1, id2);


        protected GaNumBilinearProductOrthogonal(GaNumMetricOrthogonal basisBladesSignatures, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            OrthogonalMetric = basisBladesSignatures;
        }

        public abstract GaNumMultivectorTerm MapToTerm(int id1, int id2);

        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0; id2 < DomainGaSpaceDimension2; id2++)
            {
                var mv = MapToTerm(id1, id2);

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
                var mv = MapToTerm(id1, id2);

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<int, int, IGaNumMultivector>(index1, index2, mv);
            }
        }
    }
}