namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumPlane3D
    {
        public GaNumVector3D Normal { get; } 

        public double OriginDistance { get; set; }


        public GaNumPlane3D()
        {
            Normal = new GaNumVector3D();
        }

        public GaNumPlane3D(GaNumVector3D normal, double originDistance)
        {
            Normal = normal ?? new GaNumVector3D();
            OriginDistance = originDistance;
        }


    }
}
