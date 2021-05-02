using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarVectorAsSarMultivectorReadOnlyDictionary<T> 
        : IReadOnlyDictionary<ulong, T>
    {
        public IReadOnlyList<T> VectorValues { get; }

        public int Count
            => VectorValues.Count;

        public IEnumerable<ulong> Keys
            => Enumerable.Range(0, Count).Select(index => 1UL << index);

        public IEnumerable<T> Values
            => VectorValues;

        public bool ContainsKey(ulong key)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            return grade == 1 && index < (ulong)VectorValues.Count;
        }


        public T this[ulong id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == 1)
                    return VectorValues[(int)index];

                throw new KeyNotFoundException();
            }
        }


        internal GaDarVectorAsSarMultivectorReadOnlyDictionary(IReadOnlyList<T> vectorScalarValues)
        {
            Debug.Assert(vectorScalarValues.Count.IsValidVSpaceDimension());

            VectorValues = vectorScalarValues;
        }


        public bool TryGetValue(ulong key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1)
            {
                value = VectorValues[(int)index];
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            return VectorValues.Select((t, index) => 
                new KeyValuePair<ulong, T>(1UL << index, t)
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return VectorValues.Select((t, index) => 
                new KeyValuePair<int, T>(1 << index, t)
            ).GetEnumerator();
        }
    }
}