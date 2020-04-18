namespace GeometricAlgebraNumericsLibSamples.Structures
{
    public sealed class LineSegment3D
    {
        public Point3D Point1 { get; }
            = new Point3D();

        public Point3D Point2 { get; }
            = new Point3D();


        public LineSegment3D(Point3D point1, Point3D point2)
        {
            Point1.X = point1.X;
            Point1.Y = point1.Y;
            Point1.Z = point1.Z;

            Point2.X = point2.X;
            Point2.Y = point2.Y;
            Point2.Z = point2.Z;
        }
    }
}
