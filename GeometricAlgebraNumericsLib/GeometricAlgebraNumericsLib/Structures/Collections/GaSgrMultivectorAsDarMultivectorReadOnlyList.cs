using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSgrMultivectorAsDarMultivectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public T DefaultValue { get; }

        public IReadOnlyList<IReadOnlyDictionary<int, T>> KVectorsList { get; }

        public int Count { get; }

        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var scalarValues = KVectorsList[grade];

                if (scalarValues == null || scalarValues.Count == 0)
                    return DefaultValue;

                return scalarValues.TryGetValue(index, out var value) ? value : DefaultValue;
            }
        }


        internal GaSgrMultivectorAsDarMultivectorReadOnlyList(IReadOnlyList<IReadOnlyDictionary<int, T>> kVectorsArray)
        {
            DefaultValue = default;
            Count = (kVectorsArray.Count - 1).ToGaSpaceDimension();
            KVectorsList = kVectorsArray;
        }

        internal GaSgrMultivectorAsDarMultivectorReadOnlyList(T defaultValue, IReadOnlyList<IReadOnlyDictionary<int, T>> kVectorsArray)
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