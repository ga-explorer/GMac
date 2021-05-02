using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaSgrMultivectorAsSarMultivectorReadOnlyDictionary<T> : IReadOnlyDictionary<ulong, T>
    {
        public IReadOnlyList<IReadOnlyDictionary<ulong, T>> KVectorsList { get; }

        public int Count { get; }

        public T this[ulong id]
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

        public IEnumerable<ulong> Keys
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
                        yield return GaFrameUtils.BasisBladeId(grade, index);
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


        internal GaSgrMultivectorAsSarMultivectorReadOnlyDictionary(IReadOnlyList<IReadOnlyDictionary<ulong, T>> kVectorsArray)
        {
            Count = (int)(kVectorsArray.Count - 1).ToGaSpaceDimension();
            KVectorsList = kVectorsArray;
        }


        public bool ContainsKey(ulong key)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                KVectorsList[grade];

            return !scalarValues.IsNullOrEmpty() && scalarValues.ContainsKey(index);
        }

        public bool TryGetValue(ulong key, out T value)
        {
            key.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                KVectorsList[grade];

            if (!scalarValues.IsNullOrEmpty())
                return scalarValues.TryGetValue(index, out value);

            value = default;
            return false;

        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            for (var grade = 0; grade < KVectorsList.Count; grade++)
            {
                var scalarValues =
                    KVectorsList[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValues)
                    yield return new KeyValuePair<ulong, T>(
                        GaFrameUtils.BasisBladeId(grade, pair.Key),
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
                    yield return new KeyValuePair<ulong, T>(
                        GaFrameUtils.BasisBladeId(grade, pair.Key),
                        pair.Value
                    );
            }
        }
    }
}