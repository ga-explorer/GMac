namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumLinePlucker3D
    {
        public GaNumVector3D Support { get; }

        public GaNumBivector3D Moment { get; }


        public GaNumLinePlucker3D()
        {
            Support = new GaNumVector3D();
            Moment = new GaNumBivector3D();
        }

        public GaNumLinePlucker3D(GaNumVector3D support, GaNumBivector3D moment)
        {
            Support = support ?? new GaNumVector3D();
            Moment = moment ?? new GaNumBivector3D();
        }
    }
}