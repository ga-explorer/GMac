using System.Collections;
using System.Collections.Generic;
using GMac.GMacMath.Structures;

namespace GMac.GMacMath.Numeric.Metrics
{
    public class GaNumMetricOrthogonal : IReadOnlyList<double>, IGaNumMetricOrthogonal
    {
        public static GaNumMetricOrthogonal Create(IReadOnlyList<double> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaNumMetricOrthogonal(vSpaceDim);

            bbsList[0] = 1.0d;

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                bbsList[1 << m] = bvs;
            }

            var idsSeq = GMacMathUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                int id1, id2;
                id.SplitBySmallestBasisVectorId(out id1, out id2);

                var bvs1 = bbsList[id1];
                var bvs2 = bbsList[id2];

                bbsList[id] = bvs1 * bvs2;
            }

            return bbsList;
        }


        internal GMacBinaryTree<double> RootNode { get; private set; }

        public int VSpaceDimension
            => RootNode.TreeDepth;

        public int GaSpaceDimension
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public int Count
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public double this[int index]
        {
            get
            {
                double value;

                if (RootNode.TryGetLeafValue((ulong)index, out value))
                    return value;

                return 0.0d;
            }
            private set
            {
                var node = RootNode;
                for (var i = RootNode.TreeDepth - 1; i > 0; i--)
                {
                    var bitPattern = (1 << i) & index;
                    node = node.GetOrAddInternalChildNode(bitPattern != 0);
                }

                node.SetOrAddLeafChildNode((1 & index) != 0, value);
            }
        }


        private GaNumMetricOrthogonal(int vSpaceDim)
        {
            RootNode = new GMacBinaryTree<double>(vSpaceDim);
        }


        public IEnumerator<double> GetEnumerator()
        {
            //TODO: This is not the most efficient way
            for (var i = 0; i < GaSpaceDimension; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
