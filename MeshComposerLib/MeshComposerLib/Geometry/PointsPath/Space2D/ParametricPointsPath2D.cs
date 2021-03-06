﻿using System;
using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace MeshComposerLib.Geometry.PointsPath.Space2D
{
    public sealed class ParametricPointsPath2D 
        : PSeqMapped1D<double, ITuple2D>, IPointsPath2D
    {
        public Func<double, ITuple2D> Mapping { get; set; }


        public ParametricPointsPath2D(IPeriodicSequence1D<double> parameterSequence)
            : base(parameterSequence)
        {
        }

        public ParametricPointsPath2D(IPeriodicSequence1D<double> parameterSequence, Func<double, ITuple2D> mapping)
            : base(parameterSequence)
        {
            Mapping = mapping;
        }


        protected override ITuple2D MappingFunction(double input)
        {
            return Mapping(input);
        }
    }
}