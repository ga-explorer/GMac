using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Metrics
{
    public class GaSymMetricOrthonormal 
        : IReadOnlyList<int>, IGaSymMetricOrthogonal
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

                bbsList[(int)id] = bbsList[(int)id1] * bbsList[(int)id2];
            }

            return bbsList;
        }


        private readonly BitArray _bitArray;

        public int VSpaceDimension
            => _bitArray.Count.ToVSpaceDimension();

        public ulong GaSpaceDimension
            => (ulong)_bitArray.Count;

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
            _bitArray = new BitArray((int)vSpaceDim.ToGaSpaceDimension());
        }


        public Expr GetExprSignature(ulong id)
        {
            return _bitArray[(int)id] ? Expr.INT_MINUSONE : Expr.INT_ONE;
        }

        public MathematicaScalar GetSignature(ulong id)
        {
            return _bitArray[(int)id] ? GaSymbolicsUtils.Constants.MinusOne : GaSymbolicsUtils.Constants.One;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (var i = 0UL; i < GaSpaceDimension; i++)
                yield return this[(int)i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}