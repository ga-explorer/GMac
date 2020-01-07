using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraNumericsLib.Structures
{
    public sealed class GaHashTable2D<T> : IEnumerable<Tuple<int, int, T>>
    {
        private readonly Dictionary<ulong, T> _dictionary =
            new Dictionary<ulong, T>();

        public Func<T, bool> IsDefaultValue { get; }


        public T this[int key1, int key2]
        {
            get
            {
                _dictionary.TryGetValue((ulong)key1 + ((ulong)key2 << 32), out var value);

                return value;
            }
            set
            {
                var key = (ulong)key1 + ((ulong)key2 << 32);

                if (IsDefaultValue(value))
                {
                    _dictionary.Remove(key);
                    return;
                }

                if (_dictionary.ContainsKey(key))
                    _dictionary[key] = value;
                else
                    _dictionary.Add(key, value);
            }
        }

        public int Count => _dictionary.Count;

        public IEnumerable<Tuple<int, int>> Keys 
            => _dictionary.Keys.Select(
                key => Tuple.Create(
                    (int) (key & int.MaxValue),
                    (int) ((key >> 32) & int.MaxValue)
                ));

        public ICollection<T> Values 
            => _dictionary.Values;


        public GaHashTable2D(Func<T, bool> isDefaultValueFunc)
        {
            IsDefaultValue = isDefaultValueFunc;
        }


        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool ContainsKey(int key1, int key2)
        {
            return _dictionary.ContainsKey((ulong)key1 + ((ulong)key2 << 32));
        }

        public bool TryGetValue(int key1, int key2, out T value)
        {
            return _dictionary.TryGetValue((ulong)key1 + ((ulong)key2 << 32), out value);
        }

        public GaHashTable2D<T> Remove(int key1, int key2)
        {
            _dictionary.Remove((ulong)key1 + ((ulong)key2 << 32));

            return this;
        }

        public IEnumerator<Tuple<int, int, T>> GetEnumerator()
        {
            return _dictionary.Select(
                pair => Tuple.Create(
                    (int)(pair.Key & int.MaxValue),
                    (int)((pair.Key >> 32) & int.MaxValue),
                    pair.Value
                )).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.Select(
                pair => Tuple.Create(
                    (int)(pair.Key & int.MaxValue),
                    (int)((pair.Key >> 32) & int.MaxValue),
                    pair.Value
                )).GetEnumerator();
        }
    }
}