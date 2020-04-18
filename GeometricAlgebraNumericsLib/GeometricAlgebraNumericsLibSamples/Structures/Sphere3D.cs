namespace GeometricAlgebraNumericsLibSamples.Structures
{
    public sealed class Sphere3D
    {
        public Point3D Center { get; }
            = new Point3D();

        public double RadiusSquared { get; set; }

        public SphereKind Kind
        {
            get
            {
                if (RadiusSquared > 0)
                    return SphereKind.Real;

                if (RadiusSquared < 0)
                    return SphereKind.Imaginary;

                return SphereKind.Point;
            }
        }

        public bool IsPoint
            => Kind == SphereKind.Point;

        public bool IsReal
            => RadiusSquared > 0;

        public bool IsPointOrReal
            => RadiusSquared >= 0;

        public bool IsImaginary
            => RadiusSquared < 0;

        public bool IsPointOrImaginary
            => RadiusSquared <= 0;


    }
}
