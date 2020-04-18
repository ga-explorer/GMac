using System.Collections;
using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSarMultivectorAsVectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public T DefaultValue { get; }

        public IReadOnlyDictionary<int, T> SarValues { get; }

        public int Count { get; }

        public T this[int index]
            => SarValues.TryGetValue(1 << index, out var value)
                ? value : DefaultValue;


        public GaSarMultivectorAsVectorReadOnlyList(int count, IReadOnlyDictionary<int, T> sarValues)
        {
            Count = count;
            DefaultValue = default;
            SarValues = sarValues;
        }

        public GaSarMultivectorAsVectorReadOnlyList(int count, T defaultValue, IReadOnlyDictionary<int, T> sarValues)
        {
            Count = count;
            DefaultValue = defaultValue;
            SarValues = sarValues;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return SarValues[1 << index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return SarValues[1 << index];
        }
    }
}