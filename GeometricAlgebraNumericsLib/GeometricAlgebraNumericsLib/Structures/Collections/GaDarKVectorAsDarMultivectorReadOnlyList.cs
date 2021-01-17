using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarKVectorAsDarMultivectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public T DefaultValue { get; }

        public IReadOnlyList<T> KVectorScalarValues { get; }

        public int Count { get; }

        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                return grade == KVectorGrade ? KVectorScalarValues[index] : DefaultValue;
            }
        }


        internal GaDarKVectorAsDarMultivectorReadOnlyList(int vSpaceDim, int grade, IReadOnlyList<T> kVectorScalarValues)
        {
            Debug.Assert(
                vSpaceDim.IsValidVSpaceDimension() &&
                grade >= 0 && grade <= vSpaceDim &&
                kVectorScalarValues.Count == GaFrameUtils.KvSpaceDimension(vSpaceDim, grade)
            );

            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            Count = vSpaceDim.ToGaSpaceDimension();
            DefaultValue = default;
            KVectorScalarValues = kVectorScalarValues;
        }

        internal GaDarKVectorAsDarMultivectorReadOnlyList(int vSpaceDim, int grade, T defaultValue, IReadOnlyList<T> kVectorScalarValues)
        {
            Debug.Assert(
                vSpaceDim.IsValidVSpaceDimension() &&
                grade >= 0 && grade <= vSpaceDim &&
                kVectorScalarValues.Count == GaFrameUtils.KvSpaceDimension(vSpaceDim, grade)
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