namespace GeometricAlgebraStructuresLib.Storage
{
    public interface IGaKVectorStorage<T> : IGaMultivectorStorage<T>
    {
        int Grade { get; }
        
        int KvSpaceDimension { get; }
    }
}