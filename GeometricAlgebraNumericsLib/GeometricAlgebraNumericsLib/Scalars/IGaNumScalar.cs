namespace GeometricAlgebraNumericsLib.Scalars
{
    public interface IGaNumScalar
    {
        IGaNumScalar Negative();

        IGaNumScalar Add(IGaNumScalar s2);

        IGaNumScalar Subtract(IGaNumScalar s2);

        IGaNumScalar Times(IGaNumScalar s2);

        IGaNumScalar Divide(IGaNumScalar s2);
    }
}