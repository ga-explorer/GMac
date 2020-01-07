using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderY3D : SignedDistanceFunction
    {
        public Tuple2D CenterXz { get; set; }
            = new Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double ComputeSdf(Tuple3D point)
        {
            return (point.XzToTuple2D() - CenterXz).Length() - Radius;
        }
    }
}
