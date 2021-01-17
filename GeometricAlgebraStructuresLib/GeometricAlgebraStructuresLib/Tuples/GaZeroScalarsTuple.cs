using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Tuples
{
    public sealed class GaZeroScalarsTuple<T> : IGaScalarsTuple<T>
    {
        public int Count { get; }

        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index < Count);
                
                return ItemsScalarDomain.GetZero();
            }
        }

        public IGaScalarDomain<T> ItemsScalarDomain { get; }


        public GaZeroScalarsTuple(int itemsCount, IGaScalarDomain<T> itemsScalarDomain)
        {
            Debug.Assert(itemsCount >= 0);
            
            ItemsScalarDomain = itemsScalarDomain;
            Count = itemsCount;
        }
        
        
        public IEnumerable<KeyValuePair<int, T>> GetNonZeroIndexedItems()
        {
            return Enumerable.Empty<KeyValuePair<int, T>>();
        }

        public IEnumerable<int> GetNonZeroIndices()
        {
            return Enumerable.Empty<int>();
        }

        public IEnumerable<T> GetNonZeroItems()
        {
            return Enumerable.Empty<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(i => ItemsScalarDomain.GetZero())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}