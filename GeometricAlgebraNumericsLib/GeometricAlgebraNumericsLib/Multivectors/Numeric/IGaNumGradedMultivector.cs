namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    public interface IGaNumGradedMultivector : IGaNumMultivector
    {
        int MaxStoredGrade { get; }

        bool ContainsStoredKVector(int grade);
    }
}