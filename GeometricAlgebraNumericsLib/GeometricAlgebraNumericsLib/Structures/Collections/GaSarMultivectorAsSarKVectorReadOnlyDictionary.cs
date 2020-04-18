using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarMultivectorAsSarKVectorReadOnlyDictionary<T> : IReadOnlyDictionary<int, T>
    {
        public IReadOnlyDictionary<int, T> SarValues { get; }

        public int KVectorGrade { get; }

        public int Count { get; }

        public T this[int index]
            => SarValues[GaNumFrameUtils.BasisBladeId(KVectorGrade, index)];

        public IEnumerable<int> Keys
            => SarValues
                .Where(p => p.Key.BasisBladeGrade() == KVectorGrade)
                .Select(p => p.Key.BasisBladeIndex());

        public IEnumerable<T> Values 
            => SarValues.Values;


        public GaSarMultivectorAsSarKVectorReadOnlyDictionary(int count, int grade, IReadOnlyDictionary<int, T> sarValues)
        {
            Count = count;
            KVectorGrade = grade;
            SarValues = sarValues;
        }


        public bool ContainsKey(int key)
        {
            var id = GaNumFrameUtils.BasisBladeId(KVectorGrade, key);

            return SarValues.ContainsKey(id);
        }

        public bool TryGetValue(int key, out T value)
        {
            var id = GaNumFrameUtils.BasisBladeId(KVectorGrade, key);

            return SarValues.TryGetValue(id, out value);
        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return SarValues
                .Where(p => p.Key.BasisBladeGrade() == KVectorGrade)
                .Select(p => 
                    new KeyValuePair<int, T>(p.Key.BasisBladeIndex(), p.Value)
                )
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}