namespace GeometricAlgebraNumericsLib.Geometry.Primitives
{
    public sealed class GaNumQuadricPoints3D
    {
        public GaNumPoint3D[] Points { get; }


        public GaNumQuadricPoints3D()
        {
            Points = new GaNumPoint3D[9];

            for (var i = 0; i < 9; i++)
                Points[i] = new GaNumPoint3D();
        }
    }
}