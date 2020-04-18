using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarMultivectorAsDarKVectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public T DefaultValue { get; }

        public IReadOnlyDictionary<int, T> SarValues { get; }

        public int VSpaceDimension { get; }

        public int KVectorGrade { get; }

        public int Count { get; }

        public T this[int index]
            => SarValues.TryGetValue(GaNumFrameUtils.BasisBladeId(KVectorGrade, index), out var value) 
                ? value : DefaultValue;


        public GaSarMultivectorAsDarKVectorReadOnlyList(int vSpaceDim, int grade, IReadOnlyDictionary<int, T> sarValues)
        {
            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            DefaultValue = default;
            Count = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            SarValues = sarValues;
        }

        public GaSarMultivectorAsDarKVectorReadOnlyList(int vSpaceDim, int grade, T defaultValue, IReadOnlyDictionary<int, T> sarValues)
        {
            VSpaceDimension = vSpaceDim;
            KVectorGrade = grade;
            DefaultValue = defaultValue;
            Count = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);
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