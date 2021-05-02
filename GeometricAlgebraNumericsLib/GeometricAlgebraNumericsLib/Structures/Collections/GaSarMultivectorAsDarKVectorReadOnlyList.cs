using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarMultivectorAsDarKVectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public T DefaultValue { get; }

        public IReadOnlyDictionary<ulong, T> SarValues { get; }

        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public int Count { get; }

        public T this[int index]
            => SarValues.TryGetValue(GaFrameUtils.BasisBladeId(KVectorGrade, (ulong)index), out var value) 
                ? value : DefaultValue;


        public GaSarMultivectorAsDarKVectorReadOnlyList(int vSpaceDim, int grade, IReadOnlyDictionary<ulong, T> sarValues)
        {
            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            DefaultValue = default;
            Count = (int)GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            SarValues = sarValues;
        }

        public GaSarMultivectorAsDarKVectorReadOnlyList(int vSpaceDim, int grade, T defaultValue, IReadOnlyDictionary<ulong, T> sarValues)
        {
            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            DefaultValue = defaultValue;
            Count = (int)GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            SarValues = sarValues;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return this[index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return this[index];
        }
    }
}