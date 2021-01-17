using GeometricAlgebraStructuresLib.Storage;

namespace GeometricAlgebraStructuresLib.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtMultivectorStorageStack1<T>
    {
        IGaMultivectorStorage<T> MultivectorStorage { get; }
    }
}