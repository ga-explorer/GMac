using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UtilLib.DataStructures
{
    /// <summary>
    /// This data structure can be used for storing values as a dictionary using keys or 
    /// as a list using integer indexes. A value can be inserted, updated, removed using either key or index
    /// A value can have its index or key changed or swapped with another value.
    /// </summary>
    public class IndexedDictionary<TKey, TValue> : IADictionary<TKey, TValue>, IList<TValue>
    {
        /// <summary>
        /// Dictionary for storing values by keys
        /// </summary>
        private readonly Dictionary<TKey, TValue> _valueDictionary =
            new Dictionary<TKey, TValue>();

        /// <summary>
        /// List for storing values by indexes
        /// </summary>
        private readonly List<TValue> _valueList =
            new List<TValue>();

        /// <summary>
        /// A dictionary to store the index in the value list of each key in the value dictionary
        /// </summary>
        private readonly Dictionary<TKey, int> _indexDictionary =
            new Dictionary<TKey, int>();

        /// <summary>
        /// A list to store the key in the value dictionary of each index in the value list
        /// </summary>
        private readonly List<TKey> _keyList =
            new List<TKey>();


        public IList<TValue> AsIList { get { return this; } }


        public void AddOrSetValue(TKey key, TValue value)
        {
            if (_valueDictionary.ContainsKey(key))
                SetValue(key, value);

            else
                Add(key, value);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (_valueDictionary.ContainsKey(key))
                return false;

            Add(key, value);

            return true;
        }

        public bool TryInsert(int index, TKey key, TValue value)
        {
            if (index < 0 || index >= _valueList.Count || _valueDictionary.ContainsKey(key))
                return false;

            for (var i = index; i < _keyList.Count; i++)
            {
                var k = _keyList[i];

                _indexDictionary[k] = i + 1;
            }

            _valueDictionary.Add(key, value);
            _valueList.Insert(index, value);
            _indexDictionary.Add(key, index);
            _keyList.Insert(index, key);

            return true;
        }

        public bool TryRemove(TKey key)
        {
            return Remove(key);
        }

        public bool TryRemoveAt(int index)
        {
            if (index < 0 || index >= _valueList.Count)
                return false;

            RemoveAt(index);

            return true;
        }

        public bool TryGetValueAt(int index, out TValue value)
        {
            if (index < 0 || index >= _valueList.Count)
            {
                value = default(TValue);
                return false;
            }

            value = _valueList[index];

            return true;
        }

        public bool TryGetKeyAt(int index, out TKey key)
        {
            if (index < 0 || index >= _valueList.Count)
            {
                key = default(TKey);
                return false;
            }

            key = _keyList[index];

            return true;
        }

        public bool TryGetIndex(TKey key, out int index)
        {
            return _indexDictionary.TryGetValue(key, out index);
        }

        public bool TrySetValue(TKey key, TValue value)
        {
            if (_valueDictionary.ContainsKey(key) == false) 
                return false;

            SetValue(key, value);
            return true;
        }

        public bool TrySwapValues(TKey key1, TKey key2)
        {
            if (ContainsKey(key1) == false || ContainsKey(key2) == false)
                return false;

            var value = this[key1];
            this[key1] = this[key2];
            this[key2] = value;

            return true;
        }

        public bool TrySwapValuesAt(int index1, int index2)
        {
            if (ContainsIndex(index1) == false || ContainsIndex(index2) == false)
                return false;

            var thisAsIList = AsIList;

            var value = thisAsIList[index1];
            thisAsIList[index1] = thisAsIList[index2];
            thisAsIList[index2] = value;

            return true;
        }

        public bool ContainsIndex(int index)
        {
            return index >= 0 && index < _valueList.Count;
        }

        public void Insert(int index, TKey key, TValue value)
        {
            if (index < 0 || index >= _valueList.Count)
                throw new IndexOutOfRangeException();

            if (_valueDictionary.ContainsKey(key))
                throw new ArgumentException();

            for (var i = index; i < _keyList.Count; i++)
            {
                var k = _keyList[i];

                _indexDictionary[k] = i + 1;
            }

            _valueDictionary.Add(key, value);
            _valueList.Insert(index, value);
            _indexDictionary.Add(key, index);
            _keyList.Insert(index, key);
        }

        public void RemoveAt(int index)
        {
            var key = _keyList[index];

            _valueList.RemoveAt(index);

            _keyList.RemoveAt(index);

            _valueDictionary.Remove(key);

            _indexDictionary.Remove(key);

            for (var i = index; i < _keyList.Count; i++)
            {
                var k = _keyList[i];

                _indexDictionary[k] = i;
            }
        }

        public TValue GetValue(TKey key)
        {
            return _valueDictionary[key];
        }

        public TValue GetValueAt(int index)
        {
            return _valueList[index];
        }

        public int GetIndex(TKey key)
        {
            return _indexDictionary[key];
        }

        public TKey GetKeyAt(int index)
        {
            return _keyList[index];
        }

        public void SetValue(TKey key, TValue value)
        {
            this[key] = value;
        }

        public void SetValueAt(int index, TValue value)
        {
            AsIList[index] = value;
        }

        public void SwapValues(TKey key1, TKey key2)
        {
            var value = this[key1];
            this[key1] = this[key2];
            this[key2] = value;
        }

        public void SwapValuesAt(int index1, int index2)
        {
            var thisAsIList = AsIList;

            var value = thisAsIList[index1];
            thisAsIList[index1] = thisAsIList[index2];
            thisAsIList[index2] = value;
        }



        public void Add(TKey key, TValue value)
        {
            var index = _valueList.Count;

            _valueDictionary.Add(key, value);

            _valueList.Add(value);

            _indexDictionary.Add(key, index);

            _keyList.Add(key);
        }

        public bool ContainsKey(TKey key)
        {
            return _valueDictionary.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _keyList; }
        }

        public bool Remove(TKey key)
        {
            if (_valueDictionary.Remove(key) == false)
                return false;

            var index = _indexDictionary[key];

            _indexDictionary.Remove(key);

            _valueList.RemoveAt(index);

            _keyList.RemoveAt(index);

            for (var i = index; i < _keyList.Count; i++)
            {
                var k = _keyList[i];

                _indexDictionary[k] = i;
            }

            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _valueDictionary.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _valueList; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return _valueDictionary[key];
            }
            set
            {
                var index = _indexDictionary[key];

                _valueDictionary[key] = value;

                _valueList[index] = value;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            var index = _valueList.Count;

            _valueDictionary.Add(item.Key, item.Value);

            _valueList.Add(item.Value);

            _indexDictionary.Add(item.Key, index);

            _keyList.Add(item.Key);
        }

        public void Clear()
        {
            _valueDictionary.Clear();
            _valueList.Clear();
            _indexDictionary.Clear();
            _keyList.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _valueDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var i = arrayIndex;
            foreach (var pair in _valueDictionary)
                array[i++] = pair;
        }

        public int Count
        {
            get { return _valueDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (_valueDictionary.Contains(item) == false)
                return false;

            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _valueDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _valueList.GetEnumerator();
        }

        int IList<TValue>.IndexOf(TValue item)
        {
            return _valueList.IndexOf(item);
        }

        void IList<TValue>.Insert(int index, TValue item)
        {
            throw new NotImplementedException();
        }

        void IList<TValue>.RemoveAt(int index)
        {
            var key = _keyList[index];

            _valueList.RemoveAt(index);

            _keyList.RemoveAt(index);

            _valueDictionary.Remove(key);

            _indexDictionary.Remove(key);

            for (var i = index; i < _keyList.Count; i++)
            {
                var k = _keyList[i];

                _indexDictionary[k] = i;
            }
        }

        TValue IList<TValue>.this[int index]
        {
            get
            {
                return _valueList[index];
            }
            set
            {
                var key = _keyList[index];

                _valueList[index] = value;

                _valueDictionary[key] = value;
            }
        }

        void ICollection<TValue>.Add(TValue item)
        {
            throw new NotImplementedException();
        }

        void ICollection<TValue>.Clear()
        {
            _valueDictionary.Clear();
            _valueList.Clear();
            _indexDictionary.Clear();
            _keyList.Clear();
        }

        bool ICollection<TValue>.Contains(TValue item)
        {
            return _valueList.Contains(item);
        }

        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
        {
            _valueList.CopyTo(array, arrayIndex);
        }

        int ICollection<TValue>.Count
        {
            get { return _valueList.Count; }
        }

        bool ICollection<TValue>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<TValue>.Remove(TValue item)
        {
            var index = _valueList.IndexOf(item);

            if (index < 0)
                return false;

            ((IList<TValue>)this).RemoveAt(index);

            return true;
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            return _valueList.GetEnumerator();
        }
    }
}
