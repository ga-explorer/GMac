using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRounding3D : SdfUnaryOperation
    {
        public double Radius { get; set; }
            = 0.1d;


        public override double ComputeSdf(Tuple3D point)
        {
            return Surface.ComputeSdf(point) - Radius;
        }
    }
}
