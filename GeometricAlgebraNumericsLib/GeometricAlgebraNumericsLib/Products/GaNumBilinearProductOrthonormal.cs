﻿using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products
{
    public abstract class GaNumBilinearProductOrthonormal 
        : GaNumBilinearProduct, IGaNumBilinearOrthogonalProduct
    {
        public GaNumMetricOrthonormal OrthonormalMetric { get; }


        public override IGaNumMetric Metric
            => OrthonormalMetric;

        public override IGaNumMultivector this[ulong id1, ulong id2]
            => MapToTerm(id1, id2);


        protected GaNumBilinearProductOrthonormal(GaNumMetricOrthonormal basisBladesSignatures, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            OrthonormalMetric = basisBladesSignatures;
        }

        public abstract GaNumTerm MapToTerm(ulong id1, ulong id2);

        public override IEnumerable<Tuple<ulong, ulong, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0UL; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0UL; id2 < DomainGaSpaceDimension2; id2++)
            {
                var mv = MapToTerm(id1, id2);

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<ulong, ulong, IGaNumMultivector>(id1, id2, mv);
            }
        }

        public override IEnumerable<Tuple<ulong, ulong, IGaNumMultivector>> BasisVectorsMaps()
        {
            for (var index1 = 0; index1 < DomainVSpaceDimension; index1++)
            for (var index2 = 0; index2 < DomainVSpaceDimension2; index2++)
            {
                var id1 = GaFrameUtils.BasisBladeId(1, (ulong)index1);
                var id2 = GaFrameUtils.BasisBladeId(1, (ulong)index2);
                var mv = MapToTerm(id1, id2);

                if (!mv.IsNullOrEmpty())
                    yield return new Tuple<ulong, ulong, IGaNumMultivector>((ulong)index1, (ulong)index2, mv);
            }
        }
    }
}