namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumBivector3D
    {
        public double Xy { get; set; }

        public double Xz { get; set; }

        public double Yz { get; set; }


        public GaNumBivector3D()
        {
        }


        public GaNumBivector3D(double xy, double xz, double yz)
        {
            Xy = xy;
            Xz = xz;
            Yz = yz;
        }


        public GaNumBivector3D Set(double xy, double xz, double yz)
        {
            Xy = xy;
            Xz = xz;
            Yz = yz;

            return this;
        }
    }
}