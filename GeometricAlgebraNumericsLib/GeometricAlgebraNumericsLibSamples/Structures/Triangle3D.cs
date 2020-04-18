namespace GeometricAlgebraNumericsLibSamples.Structures
{
    public sealed class Triangle3D
    {
        public Point3D Point1 { get; }
            = new Point3D();

        public Point3D Point2 { get; }
            = new Point3D();

        public Point3D Point3 { get; }
            = new Point3D();


        public Triangle3D(Point3D point1, Point3D point2, Point3D point3)
        {
            Point1.X = point1.X;
            Point1.Y = point1.Y;
            Point1.Z = point1.Z;

            Point2.X = point2.X;
            Point2.Y = point2.Y;
            Point2.Z = point2.Z;

            Point3.X = point3.X;
            Point3.Y = point3.Y;
            Point3.Z = point3.Z;
        }
    }
}
