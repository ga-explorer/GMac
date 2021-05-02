using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarKVectorAsSarMultivectorReadOnlyDictionary<T> 
        : IReadOnlyDictionary<ulong, T>
    {
        public IReadOnlyList<T> KVectorValues { get; }

        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public int Count 
            => KVectorValues.Count;

        public IEnumerable<ulong> Keys 
            => Enumerable.Range(0, Count).Select(index => 1UL << index);

        public IEnumerable<T> Values 
            => KVectorValues;

        public bool ContainsKey(ulong key)
        {
            return key.BasisBladeGrade() == KVectorGrade;
        }


        public T this[ulong id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == KVectorGrade) 
                    return KVectorValues[(int)index];

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


        public bool TryGetValue(ulong key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == KVectorGrade)
            {
                value = KVectorValues[(int)index];
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
            {
                yield return new KeyValuePair<ulong, T>(
                    GaFrameUtils.BasisBladeId(KVectorGrade, (ulong)index),
                    KVectorValues[index]
                );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
            {
                yield return new KeyValuePair<ulong, T>(
                    GaFrameUtils.BasisBladeId(KVectorGrade, (ulong)index),
                    KVectorValues[index]
                );
            }
        }
    }
}