using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDgrMultivectorAsDarMultivectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public T DefaultValue { get; }

        public IReadOnlyList<IReadOnlyList<T>> KVectorsList { get; }

        public int Count { get; }

        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var scalarValues = KVectorsList[grade];

                return scalarValues == null 
                    ? DefaultValue
                    : scalarValues[index];
            }
        }


        internal GaDgrMultivectorAsDarMultivectorReadOnlyList(IReadOnlyList<IReadOnlyList<T>> kVectorsArray)
        {
            DefaultValue = default;
            Count = (kVectorsArray.Count - 1).ToGaSpaceDimension();
            KVectorsList = kVectorsArray;
        }

        internal GaDgrMultivectorAsDarMultivectorReadOnlyList(T defaultValue, IReadOnlyList<IReadOnlyList<T>> kVectorsArray)
        {
            DefaultValue = defaultValue;
            Count = (kVectorsArray.Count - 1).ToGaSpaceDimension();
            KVectorsList = kVectorsArray;
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