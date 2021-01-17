using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Metrics
{
    public class GaSymMetricOrthonormal : IReadOnlyList<int>, IGaSymMetricOrthogonal
    {
        public static GaSymMetricOrthonormal Create(IReadOnlyList<int> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaSymMetricOrthonormal(vSpaceDim);

            bbsList[0] = 1;

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs == 0) continue;

                bbsList[1 << m] = bvs;
            }

            var idsSeq = GaFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
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
            get
            {
                return _bitArray[index] ? -1 : 1;
            }
            private set
            {
                _bitArray[index] = (value != 1);
            }
        }


        private GaSymMetricOrthonormal(int vSpaceDim)
        {
            _bitArray = new BitArray(vSpaceDim.ToGaSpaceDimension());
        }


        public Expr GetExprSignature(int id)
        {
            return _bitArray[id] ? Expr.INT_MINUSONE : Expr.INT_ONE;
        }

        public MathematicaScalar GetSignature(int id)
        {
            return _bitArray[id] ? GaSymbolicsUtils.Constants.MinusOne : GaSymbolicsUtils.Constants.One;
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