using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSgrMultivectorAsSarMultivectorReadOnlyDictionary<T> : IReadOnlyDictionary<int, T>
    {
        public IReadOnlyList<IReadOnlyDictionary<int, T>> KVectorsList { get; }

        public int Count { get; }

        public T this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var scalarValues = 
                    KVectorsList[grade];

                if (scalarValues.IsNullOrEmpty())
                    throw new KeyNotFoundException();

                return scalarValues[index];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                for (var grade = 0; grade < KVectorsList.Count; grade++)
                {
                    var scalarValues =
                        KVectorsList[grade];

                    if (scalarValues.IsNullOrEmpty())
                        continue;

                    foreach (var index in scalarValues.Keys)
                        yield return GaNumFrameUtils.BasisBladeId(grade, index);
                }
            }
        }

        public IEnumerable<T> Values
        {
            get
            {
                var kVectors =
                    KVectorsList.Where(a => !a.IsNullOrEmpty());

                foreach (var scalarValues in kVectors)
                {
                    foreach (var value in scalarValues.Values)
                        yield return value;
                }
            }
        }


        internal GaSgrMultivectorAsSarMultivectorReadOnlyDictionary(IReadOnlyList<IReadOnlyDictionary<int, T>> kVectorsArray)
        {
            Count = (kVectorsArray.Count - 1).ToGaSpaceDimension();
            KVectorsList = kVectorsArray;
        }


        public bool ContainsKey(int key)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                KVectorsList[grade];

            return !scalarValues.IsNullOrEmpty() && scalarValues.ContainsKey(index);
        }

        public bool TryGetValue(int key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                KVectorsList[grade];

            if (!scalarValues.IsNullOrEmpty())
                return scalarValues.TryGetValue(index, out value);

            value = default;
            return false;

        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            for (var grade = 0; grade < KVectorsList.Count; grade++)
            {
                var scalarValues =
                    KVectorsList[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValues)
                    yield return new KeyValuePair<int, T>(
                        GaNumFrameUtils.BasisBladeId(grade, pair.Key),
                        pair.Value
                    );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var grade = 0; grade < KVectorsList.Count; grade++)
            {
                var scalarValues =
                    KVectorsList[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValues)
                    yield return new KeyValuePair<int, T>(
                        GaNumFrameUtils.BasisBladeId(grade, pair.Key),
                        pair.Value
                    );
            }
        }
    }
}