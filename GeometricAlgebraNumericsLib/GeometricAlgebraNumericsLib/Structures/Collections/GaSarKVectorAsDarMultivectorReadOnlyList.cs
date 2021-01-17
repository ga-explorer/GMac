using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarKVectorAsDarMultivectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public T DefaultValue { get; }

        public IReadOnlyDictionary<int, T> KVectorScalarValues { get; }

        public int Count { get; }

        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade != KVectorGrade) 
                    return DefaultValue;

                return KVectorScalarValues.TryGetValue(index, out var value) ? value : DefaultValue;
            }
        }


        internal GaSarKVectorAsDarMultivectorReadOnlyList(int vSpaceDim, int grade, IReadOnlyDictionary<int, T> kVectorScalarValues)
        {
            Debug.Assert(
                vSpaceDim.IsValidVSpaceDimension() &&
                grade >= 0 && grade <= vSpaceDim
            );

            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            Count = vSpaceDim.ToGaSpaceDimension();
            DefaultValue = default;
            KVectorScalarValues = kVectorScalarValues;
        }

        internal GaSarKVectorAsDarMultivectorReadOnlyList(int vSpaceDim, int grade, T defaultValue, IReadOnlyDictionary<int, T> kVectorScalarValues)
        {
            Debug.Assert(
                vSpaceDim.IsValidVSpaceDimension() &&
                grade >= 0 && grade <= vSpaceDim
            );

            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            Count = vSpaceDim.ToGaSpaceDimension();
            DefaultValue = defaultValue;
            KVectorScalarValues = kVectorScalarValues;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var id = 0; id < Count; id++)
                yield return this[id];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var id = 0; id < Count; id++)
                yield return this[id];
        }
    }
}