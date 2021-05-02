using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarMultivectorAsSarKVectorReadOnlyDictionary<T> : IReadOnlyDictionary<ulong, T>
    {
        public IReadOnlyDictionary<ulong, T> SarValues { get; }

        public int KVectorGrade { get; }

        public int Count { get; }

        public T this[ulong index]
            => SarValues[GaFrameUtils.BasisBladeId(KVectorGrade, index)];

        public IEnumerable<ulong> Keys
            => SarValues
                .Where(p => p.Key.BasisBladeGrade() == KVectorGrade)
                .Select(p => p.Key.BasisBladeIndex());

        public IEnumerable<T> Values 
            => SarValues.Values;


        public GaSarMultivectorAsSarKVectorReadOnlyDictionary(int count, int grade, IReadOnlyDictionary<ulong, T> sarValues)
        {
            Count = count;
            KVectorGrade = grade;
            SarValues = sarValues;
        }


        public bool ContainsKey(ulong key)
        {
            var id = GaFrameUtils.BasisBladeId(KVectorGrade, key);

            return SarValues.ContainsKey(id);
        }

        public bool TryGetValue(ulong key, out T value)
        {
            var id = GaFrameUtils.BasisBladeId(KVectorGrade, key);

            return SarValues.TryGetValue(id, out value);
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            return SarValues
                .Where(p => p.Key.BasisBladeGrade() == KVectorGrade)
                .Select(p => 
                    new KeyValuePair<ulong, T>(p.Key.BasisBladeIndex(), p.Value)
                )
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}