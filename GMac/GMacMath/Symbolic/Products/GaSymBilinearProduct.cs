using System;
using System.Collections.Generic;
using GMac.GMacMath.Symbolic.Maps.Bilinear;
using GMac.GMacMath.Symbolic.Metrics;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;
using SymbolicInterface.Mathematica;

namespace GMac.GMacMath.Symbolic.Products
{
    public abstract class GaSymBilinearProduct : IGaSymMapBilinear
    {
        public MathematicaInterface CasInterface 
            => SymbolicUtils.Cas;

        public MathematicaConnection CasConnection
            => SymbolicUtils.Cas.Connection;

        public MathematicaEvaluator CasEvaluator
            => SymbolicUtils.Cas.Evaluator;

        public MathematicaConstants CasConstants
            => SymbolicUtils.Cas.Constants;

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