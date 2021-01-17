using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Tuples
{
    public sealed class GaSparseScalarsTuple<T> : IGaSparseScalarsTuple<T>
    {
        public static GaSparseScalarsTuple<T> operator -(GaSparseScalarsTuple<T> list1)
        {
            var scalarDomain = list1.ItemsScalarDomain;
            var result = new GaSparseScalarsTuple<T>(list1.Count, scalarDomain);

            foreach (var pair in list1._itemsDictionary)
                result._itemsDictionary.Add(pair.Key, scalarDomain.Negative(pair.Value));

            return result;
        }
        
        public static GaSparseScalarsTuple<T> operator +(GaSparseScalarsTuple<T> list1, GaSparseScalarsTuple<T> list2)
        {
            Debug.Assert(list1.Count == list2.Count);
            
            var scalarDomain = list1.ItemsScalarDomain;
            var result = new GaSparseScalarsTuple<T>(list1.Count, scalarDomain);

            foreach (var pair in list1._itemsDictionary)
                result._itemsDictionary.Add(pair.Key, pair.Value);
            
            foreach (var pair in list2._itemsDictionary)
                result[pair.Key] = scalarDomain.Add(result[pair.Key], pair.Value);

            return result;
        }
        
        public static GaSparseScalarsTuple<T> operator -(GaSparseScalarsTuple<T> list1, GaSparseScalarsTuple<T> list2)
        {
            Debug.Assert(list1.Count == list2.Count);
            
            var scalarDomain = list1.ItemsScalarDomain;
            var result = new GaSparseScalarsTuple<T>(list1.Count, scalarDomain);

            foreach (var pair in list1._itemsDictionary)
                result._itemsDictionary.Add(pair.Key, pair.Value);
            
            foreach (var pair in list2._itemsDictionary)
                result[pair.Key] = scalarDomain.Subtract(result[pair.Key], pair.Value);

            return result;
        }
        
        public static GaSparseScalarsTuple<T> operator *(GaSparseScalarsTuple<T> list1, GaSparseScalarsTuple<T> list2)
        {
            Debug.Assert(list1.Count == list2.Count);
            
            var scalarDomain = list1.ItemsScalarDomain;
            var result = new GaSparseScalarsTuple<T>(list1.Count, list1.ItemsScalarDomain);

            foreach (var pair in list1._itemsDictionary)
                result._itemsDictionary.Add(pair.Key, pair.Value);
            
            foreach (var pair in list2._itemsDictionary)
                result[pair.Key] = scalarDomain.Times(result[pair.Key], pair.Value);

            return result;
        }

        
        private readonly Dictionary<int, T> _itemsDictionary
            = new Dictionary<int, T>();


        public int Count { get; }
        
        public int StoredItemsCount 
            => _itemsDictionary.Count;

        public IEnumerable<int> Keys 
            => _itemsDictionary.Keys;
        
        public IEnumerable<T> Values 
            => _itemsDictionary.Values;
        
        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index < Count);
                
                return _itemsDictionary.TryGetValue(index, out var value)
                    ? value
                    : ItemsScalarDomain.GetZero();
            }
            set
            {
                Debug.Assert(index >= 0 && index < Count);
                
                if (ItemsScalarDomain.IsZero(value))
                {
                    if (_itemsDictionary.ContainsKey(index))
                        _itemsDictionary.Remove(index);

                    return;
                }
                
                if (_itemsDictionary.ContainsKey(index))
                    _itemsDictionary[index] = value;
                else
                    _itemsDictionary.Add(index, value);
            }
        }

        public IGaScalarDomain<T> ItemsScalarDomain { get; }


        public GaSparseScalarsTuple(int count, IGaScalarDomain<T> itemsScalarDomain)
        {
            Debug.Assert(count > 0);

            ItemsScalarDomain = itemsScalarDomain;
            Count = count;
        }
        
        public GaSparseScalarsTuple(GaSparseScalarsTuple<T> inputList)
        {
            ItemsScalarDomain = inputList.ItemsScalarDomain;
            Count = inputList.Count;
            
            foreach (var pair in inputList._itemsDictionary)
                _itemsDictionary.Add(pair.Key, pair.Value);
        }
        
        
        public void Clear()
        {
            _itemsDictionary.Clear();
        }
        
        public bool ContainsKey(int index)
        {
            Debug.Assert(index >= 0 && index < Count);
            
            return _itemsDictionary.ContainsKey(index);
        }

        public bool RemoveItem(int index)
        {
            Debug.Assert(index >= 0 && index < Count);
            
            return _itemsDictionary.Remove(index);
        }

        public bool TryGetValue(int index, out T value)
        {
            Debug.Assert(index >= 0 && index < Count);
            
            return _itemsDictionary.TryGetValue(index, out value);
        }


        public void AddItems(IEnumerable<KeyValuePair<int, T>> itemsList)
        {
            foreach (var pair in itemsList)
                this[pair.Key] = ItemsScalarDomain.Add(this[pair.Key], pair.Value);
        }

        public void AddItems(IEnumerable<T> itemsList)
        {
            var i = 0;
            foreach (var item in itemsList)
            {
                this[i] = ItemsScalarDomain.Add(this[i], item);

                i++;
            }
        }

        public void SubtractItems(IEnumerable<KeyValuePair<int, T>> itemsList)
        {
            foreach (var pair in itemsList)
                this[pair.Key] = ItemsScalarDomain.Subtract(this[pair.Key], pair.Value);
        }

        public void SubtractItems(IReadOnlyList<T> itemsList)
        {
            for (var i = 0; i < itemsList.Count; i++)
                this[i] = ItemsScalarDomain.Subtract(this[i], itemsList[i]);
        }

        public void SubtractItems(IEnumerable<T> itemsList)
        {
            var i = 0;
            foreach (var item in itemsList)
            {
                this[i] = ItemsScalarDomain.Subtract(this[i], item);

                i++;
            }
        }
        
        public IEnumerable<KeyValuePair<int, T>> GetNonZeroIndexedItems()
        {
            return _itemsDictionary;
        }

        public IEnumerable<int> GetNonZeroIndices()
        {
            return _itemsDictionary.Keys;
        }

        public IEnumerable<T> GetNonZeroItems()
        {
            return _itemsDictionary.Values;
        }


        public override string ToString()
        {
            var composer = new StringBuilder();

            composer.Append("{");

            var indexedItemsList = 
                _itemsDictionary.OrderBy(p => p.Key);

            foreach (var pair in indexedItemsList)
                composer
                    .Append(pair.Key)
                    .Append(": ")
                    .Append(pair.Value);
            
            composer.Append("}");
            
            return composer.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _itemsDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}