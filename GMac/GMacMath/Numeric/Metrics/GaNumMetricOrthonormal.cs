using System.Collections;
using System.Collections.Generic;

namespace GMac.GMacMath.Numeric.Metrics
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

            var idsSeq = GMacMathUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                int id1, id2;
                id.SplitBySmallestBasisVectorId(out id1, out id2);

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
            get
            {
                return _bitArray[index] ? -1 : 1;
            }
            private set
            {
                _bitArray[index] = (value != 1);
            }
        }


        private GaNumMetricOrthonormal(int vSpaceDim)
        {
            _bitArray = new BitArray(vSpaceDim.ToGaSpaceDimension());
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
