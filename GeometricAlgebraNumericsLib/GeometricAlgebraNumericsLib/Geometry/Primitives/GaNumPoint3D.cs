namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumPoint3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }


        public GaNumPoint3D()
        {
        }


        public GaNumPoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public GaNumPoint3D Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            return this;
        }
    }
}