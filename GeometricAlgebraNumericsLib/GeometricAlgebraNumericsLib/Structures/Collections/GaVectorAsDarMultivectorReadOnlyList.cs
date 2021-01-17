using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaVectorAsDarMultivectorReadOnlyList<T> : IReadOnlyList<T>
    {
        public T DefaultValue { get; }

        public IReadOnlyList<T> VectorValues { get; }

        public int Count { get; }

        public T this[int id] 
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                return grade == 1 ? VectorValues[index] : DefaultValue;
            }
        }


        internal GaVectorAsDarMultivectorReadOnlyList(IReadOnlyList<T> vectorScalarValues)
        {
            Debug.Assert(vectorScalarValues.Count.IsValidVSpaceDimension());

            DefaultValue = default;
            Count = vectorScalarValues.Count.ToGaSpaceDimension();
            VectorValues = vectorScalarValues;
        }

        internal GaVectorAsDarMultivectorReadOnlyList(T defaultValue, IReadOnlyList<T> vectorScalarValues)
        {
            Debug.Assert(vectorScalarValues.Count.IsValidVSpaceDimension());

            DefaultValue = defaultValue;
            Count = vectorScalarValues.Count.ToGaSpaceDimension();
            VectorValues = vectorScalarValues;
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
