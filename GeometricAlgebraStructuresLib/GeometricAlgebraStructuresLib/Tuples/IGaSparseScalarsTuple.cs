namespace GeometricAlgebraStructuresLib.Tuples
{
    public interface IGaSparseScalarsTuple<T> : IGaScalarsTuple<T>
    {
        int StoredItemsCount { get; }
        
        void Clear();

        bool RemoveItem(int index);
    }
}