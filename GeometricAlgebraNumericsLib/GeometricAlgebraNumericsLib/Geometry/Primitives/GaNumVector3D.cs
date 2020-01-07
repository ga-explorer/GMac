namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumVector3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }


        public GaNumVector3D()
        {
        }


        public GaNumVector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public GaNumVector3D Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            return this;
        }
    }
}