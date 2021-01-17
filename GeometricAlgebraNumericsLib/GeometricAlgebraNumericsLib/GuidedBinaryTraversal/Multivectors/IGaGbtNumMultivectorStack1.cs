using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.GuidedBinaryTraversal;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtNumMultivectorStack1 : IGaGbtStack1<double>
    {
        IGaNumMultivector Multivector { get; }
    }
}