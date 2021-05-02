namespace GeometricAlgebraStructuresLib.Storage
{
    public interface IGaKVectorStorage<T> : IGaMultivectorStorage<T>
    {
        int Grade { get; }
        
        ulong KvSpaceDimension { get; }
    }
}