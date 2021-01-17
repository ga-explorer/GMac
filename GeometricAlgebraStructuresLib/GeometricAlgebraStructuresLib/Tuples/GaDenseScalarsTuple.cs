using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Tuples
{
    public sealed class GaDenseScalarsTuple<T> : IGaScalarsTuple<T>
    {
        private readonly T[] _itemsArray;


        public int Count 
            => _itemsArray.Length;

        public T this[int index]
        {
            get => _itemsArray[index];
            set => _itemsArray[index] = value;
        }

        public IGaScalarDomain<T> ItemsScalarDomain { get; }


        public GaDenseScalarsTuple(int itemsCount, IGaScalarDomain<T> itemsScalarDomain)
        {
            ItemsScalarDomain = itemsScalarDomain;
            _itemsArray = new T[itemsCount];
        }
        
        
        public IEnumerable<KeyValuePair<int, T>> GetNonZeroIndexedItems()
        {
            var index = 0;
            foreach (var item in _itemsArray)
            {
                if (ItemsScalarDomain.IsNotZero(item)) 
                    yield return new KeyValuePair<int, T>(index, item);

                index++;
            }
        }

        public IEnumerable<int> GetNonZeroIndices()
        {
            var index = 0;
            foreach (var item in _itemsArray)
            {
                if (ItemsScalarDomain.IsNotZero(item)) 
                    yield return index;

                index++;
            }
        }

        public IEnumerable<T> GetNonZeroItems()
        {
            return _itemsArray.Where(item => ItemsScalarDomain.IsNotZero(item));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_itemsArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}