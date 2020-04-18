using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarKVectorAsSarMultivectorReadOnlyDictionary<T> : IReadOnlyDictionary<int, T>
    {
        public IReadOnlyList<T> KVectorValues { get; }

        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public int Count 
            => KVectorValues.Count;

        public IEnumerable<int> Keys 
            => Enumerable.Range(0, Count).Select(index => 1 << index);

        public IEnumerable<T> Values 
            => KVectorValues;

        public bool ContainsKey(int key)
        {
            return key.BasisBladeGrade() == KVectorGrade;
        }


        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == KVectorGrade) 
                    return KVectorValues[index];

                throw new KeyNotFoundException();
            }
        }


        internal GaDarKVectorAsSarMultivectorReadOnlyDictionary(int vSpaceDim, int grade, IReadOnlyList<T> kVectorScalarValues)
        {
            Debug.Assert(kVectorScalarValues.Count.IsValidVSpaceDimension());

            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            KVectorValues = kVectorScalarValues;
        }


        public bool TryGetValue(int key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == KVectorGrade)
            {
                value = KVectorValues[index];
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
            {
                yield return new KeyValuePair<int, T>(
                    GaNumFrameUtils.BasisBladeId(KVectorGrade, index),
                    KVectorValues[index]
                );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
            {
                yield return new KeyValuePair<int, T>(
                    GaNumFrameUtils.BasisBladeId(KVectorGrade, index),
                    KVectorValues[index]
                );
            }
        }
    }
}