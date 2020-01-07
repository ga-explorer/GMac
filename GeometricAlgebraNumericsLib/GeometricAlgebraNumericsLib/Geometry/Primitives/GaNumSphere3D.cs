using System;

namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumSphere3D
    {
        public GaNumPoint3D Center { get; }
            

        public double RadiusSquared { get; set; }

        public double Radius
            => Math.Sqrt(RadiusSquared >= 0 ? RadiusSquared : -RadiusSquared);

        public bool IsReal
            => RadiusSquared > 0;

        public bool IsPoint
            => RadiusSquared == 0.0d;

        public bool IsImaginary
            => RadiusSquared < 0;


        public GaNumSphere3D()
        {
            Center = new GaNumPoint3D();
        }

        public GaNumSphere3D(GaNumPoint3D center, double radiusSquared)
        {
            Center = center ?? new GaNumPoint3D();
            RadiusSquared = radiusSquared;
        }

    }
}
