﻿using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderZ3D : SignedDistanceFunction
    {
        public Tuple2D CenterXy { get; set; }
            = new Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double ComputeSdf(Tuple3D point)
        {
            return (point.XyToTuple2D() - CenterXy).Length() - Radius;
        }
    }
}
