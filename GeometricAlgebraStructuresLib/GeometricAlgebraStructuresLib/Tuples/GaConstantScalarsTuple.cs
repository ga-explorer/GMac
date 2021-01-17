using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Tuples
{
    public sealed class GaConstantScalarsTuple<T> : IGaScalarsTuple<T>
    {
        public T Value { get; }
        
        public int Count { get; }

        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index < Count);
                
                return ItemsScalarDomain.Positive(Value);
            }
        }

        public IGaScalarDomain<T> ItemsScalarDomain { get; }


        public GaConstantScalarsTuple(T value, int itemsCount, IGaScalarDomain<T> itemsScalarDomain)
        {
            Debug.Assert(itemsCount >= 0);

            Value = value;
            ItemsScalarDomain = itemsScalarDomain;
            Count = itemsCount;
        }
        
        
        public IEnumerable<KeyValuePair<int, T>> GetNonZeroIndexedItems()
        {
            if (!ItemsScalarDomain.IsZero(Value))
                for (var i = 0; i < Count; i++)
                    yield return new KeyValuePair<int, T>(i, ItemsScalarDomain.Positive(Value));
        }

        public IEnumerable<int> GetNonZeroIndices()
        {
            return ItemsScalarDomain.IsZero(Value)
                ? Enumerable.Empty<int>()
                : Enumerable.Range(0, Count);
        }

        public IEnumerable<T> GetNonZeroItems()
        {
            if (!ItemsScalarDomain.IsZero(Value))
                for (var i = 0; i < Count; i++)
                    yield return ItemsScalarDomain.Positive(Value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(i => ItemsScalarDomain.Positive(Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}