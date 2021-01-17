using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarMultivectorAsDarKVectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public IReadOnlyList<T> DarValues { get; }

        public int Grade { get; }

        public int Count { get; }

        public T this[int index]
            => DarValues[GaFrameUtils.BasisBladeId(Grade, index)];


        internal GaDarMultivectorAsDarKVectorReadOnlyList(int grade, IReadOnlyList<T> darValues)
        {
            Grade = grade;
            Count = GaFrameUtils.KvSpaceDimension(darValues.Count.ToVSpaceDimension(), grade);
            DarValues = darValues;
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