using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarMultivectorAsVectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public IReadOnlyList<T> DarValues { get; }

        public int Count { get; }

        public T this[int index]
            => DarValues[1 << index];


        public GaDarMultivectorAsVectorReadOnlyList(IReadOnlyList<T> darValues)
        {
            Count = darValues.Count.ToVSpaceDimension();
            DarValues = darValues;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return DarValues[1 << index];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return DarValues[1 << index];
        }
    }
}