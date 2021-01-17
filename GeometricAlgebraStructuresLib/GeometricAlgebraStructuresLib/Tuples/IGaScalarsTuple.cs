using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Tuples
{
    public interface IGaScalarsTuple<T> : IReadOnlyList<T>
    {
        IGaScalarDomain<T> ItemsScalarDomain { get; }

        IEnumerable<KeyValuePair<int, T>> GetNonZeroIndexedItems();
        
        IEnumerable<int> GetNonZeroIndices();
        
        IEnumerable<T> GetNonZeroItems();
    }
}