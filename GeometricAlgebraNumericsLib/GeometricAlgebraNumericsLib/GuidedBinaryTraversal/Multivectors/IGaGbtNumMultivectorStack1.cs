using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtNumMultivectorStack1 : IGaGbtStack1<double>
    {
        IGaNumMultivector Multivector { get; }
    }
}