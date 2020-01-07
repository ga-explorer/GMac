using System;
using System.Collections.Generic;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Maps.Bilinear;
using GeometricAlgebraSymbolicsLib.Metrics;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public abstract class GaSymBilinearProduct : IGaSymMapBilinear
    {
        public MathematicaInterface CasInterface 
            => GaSymbolicsUtils.Cas;

        public MathematicaConnection CasConnection
            => GaSymbolicsUtils.Cas.Connection;

        public MathematicaEvaluator CasEvaluator
            => GaSymbolicsUtils.Cas.Evaluator;

        public MathematicaConstants CasConstants
            => GaSymbolicsUtils.Cas.Constants;

        public abstract IGaSymMetric Metric { get; }

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

        public abstract IGaSymMultivector this[int id1, int id2] { get; }

        public GaSymMultivector this[GaSymMultivector mv1, GaSymMultivector mv2]
            => MapToTemp(mv1, mv2).ToMultivector();


        public abstract IGaSymMultivectorTemp MapToTemp(int id1, int id2);

        public abstract IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2);

        public abstract IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisBladesMaps();

        public abstract IEnumerable<Tuple<int, int, IGaSymMultivector>> BasisVectorsMaps();
    }
}