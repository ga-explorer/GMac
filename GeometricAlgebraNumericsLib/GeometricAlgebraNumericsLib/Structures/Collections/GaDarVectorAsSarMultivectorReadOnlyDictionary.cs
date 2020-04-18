using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaDarVectorAsSarMultivectorReadOnlyDictionary<T> : IReadOnlyDictionary<int, T>
    {
        public IReadOnlyList<T> VectorValues { get; }

        public int Count
            => VectorValues.Count;

        public IEnumerable<int> Keys
            => Enumerable.Range(0, Count).Select(index => 1 << index);

        public IEnumerable<T> Values
            => VectorValues;

        public bool ContainsKey(int key)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            return grade == 1 && index < VectorValues.Count;
        }


        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == 1)
                    return VectorValues[index];

                throw new KeyNotFoundException();
            }
        }


        internal GaDarVectorAsSarMultivectorReadOnlyDictionary(IReadOnlyList<T> vectorScalarValues)
        {
            Debug.Assert(vectorScalarValues.Count.IsValidVSpaceDimension());

            VectorValues = vectorScalarValues;
        }


        public bool TryGetValue(int key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1)
            {
                value = VectorValues[index];
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return VectorValues.Select((t, index) => 
                new KeyValuePair<int, T>(1 << index, t)
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