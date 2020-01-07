using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Metrics
{
    public class GaNumMetricOrthonormal : IReadOnlyList<int>, IGaNumMetricOrthogonal
    {
        public static GaNumMetricOrthonormal Create(IReadOnlyList<int> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaNumMetricOrthonormal(vSpaceDim);

            bbsList[0] = 1;

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs == 0) continue;

                bbsList[1 << m] = bvs;
            }

            var idsSeq = GaNumFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                bbsList[id] = bbsList[id1] * bbsList[id2];
            }

            return bbsList;
        }


        private readonly BitArray _bitArray;

        public int VSpaceDimension
            => _bitArray.Count.ToVSpaceDimension();

        public int GaSpaceDimension
            => _bitArray.Count;

        public int Count
            => _bitArray.Count;

        public int this[int index]
        {
            get => _bitArray[index] ? -1 : 1;
            private set => _bitArray[index] = (value != 1);
        }


        private GaNumMetricOrthonormal(int vSpaceDim)
        {
            _bitArray = new BitArray(vSpaceDim.ToGaSpaceDimension());
        }


        public double GetBasisBladeSignature(int id)
        {
            return _bitArray[id] ? -1.0d : 1.0d;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (var i = 0; i < GaSpaceDimension; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
