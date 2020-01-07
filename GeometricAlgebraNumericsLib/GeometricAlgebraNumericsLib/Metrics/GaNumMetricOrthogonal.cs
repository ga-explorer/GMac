using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Metrics
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

                if (bvs != 0.0d)
                    bbsList[1 << m] = bvs;
            }

            var idsSeq = GaNumFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                var bvs1 = bbsList[id1];
                var bvs2 = bbsList[id2];
                var bvs = bvs1 * bvs2;

                if (bvs != 0.0d)
                    bbsList[id] = bvs1 * bvs2;
            }

            return bbsList;
        }


        internal GaBinaryTreeInternalNode<double> RootNode { get; private set; }

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
                if (RootNode.TryGetLeafValue((ulong)index, out var value))
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
            RootNode = new GaBinaryTreeInternalNode<double>(0, vSpaceDim);
        }


        public GaNumMultivector GetMetricSignatureMultivector()
        {
            return GaNumMultivector.CreateCopy(RootNode);
        }

        public double GetBasisBladeSignature(int id)
        {
            if (RootNode.TryGetLeafValue((ulong)id, out var value))
                return value;

            return 0.0d;
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
