﻿using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;

namespace GeometricAlgebraNumericsLib.Products
{
    public abstract class GaNumBilinearProduct : IGaNumMapBilinear
    {
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

        public GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
            => MapToTemp(mv1, mv2).ToMultivector();


        public abstract IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2);

        public abstract IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps();

        public abstract IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisVectorsMaps();
    }
}
