using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.SdfGeometry.Primitives
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
