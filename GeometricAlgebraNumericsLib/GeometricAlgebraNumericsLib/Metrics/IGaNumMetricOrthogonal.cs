namespace GeometricAlgebraNumericsLib.Metrics
{
    public interface IGaNumMetricOrthogonal : IGaNumMetric
    {
        double GetBasisBladeSignature(int id);
    }
}