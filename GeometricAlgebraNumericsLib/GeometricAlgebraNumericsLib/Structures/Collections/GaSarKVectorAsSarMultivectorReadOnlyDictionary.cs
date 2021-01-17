using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarKVectorAsSarMultivectorReadOnlyDictionary<T> : IReadOnlyDictionary<int, T>
    {
        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public IReadOnlyDictionary<int, T> KVectorScalarValues { get; }

        public int Count 
            => KVectorScalarValues.Count;

        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == KVectorGrade)
                    return KVectorScalarValues[index];

                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<int> Keys
            => KVectorScalarValues.Keys.Select(
                index => GaFrameUtils.BasisBladeId(KVectorGrade, index)
            );

        public IEnumerable<T> Values 
            => KVectorScalarValues.Values;


        internal GaSarKVectorAsSarMultivectorReadOnlyDictionary(int vSpaceDim, int grade, IReadOnlyDictionary<int, T> kVectorScalarValues)
        {
            Debug.Assert(
                vSpaceDim.IsValidVSpaceDimension() &&
                grade >= 0 && grade <= vSpaceDim
            );

            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            KVectorScalarValues = kVectorScalarValues;
        }


        public bool ContainsKey(int key)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            return grade == KVectorGrade && 
                   KVectorScalarValues.ContainsKey(index);
        }

        public bool TryGetValue(int key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == KVectorGrade)
                return KVectorScalarValues.TryGetValue(index, out value);

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return KVectorScalarValues
                .Select(
                    p => new KeyValuePair<int, T>(
                        GaFrameUtils.BasisBladeId(KVectorGrade, p.Key),
                        p.Value
                    )
                )
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return KVectorScalarValues
                .Select(
                    p => new KeyValuePair<int, T>(
                        GaFrameUtils.BasisBladeId(KVectorGrade, p.Key),
                        p.Value
                    )
                )
                .GetEnumerator();
        }
    }
}