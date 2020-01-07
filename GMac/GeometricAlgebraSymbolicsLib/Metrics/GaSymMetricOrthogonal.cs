using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Metrics
{
    public class GaSymMetricOrthogonal : IReadOnlyList<Expr>, IGaSymMetricOrthogonal
    {
        public static GaSymMetricOrthogonal Create(IReadOnlyList<MathematicaScalar> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaSymMetricOrthogonal(vSpaceDim);

            bbsList[0] = Expr.INT_ONE;

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m].Expression;

                if (bvs.IsNullOrZero()) continue;

                bbsList[1 << m] = bvs;
            }

            var idsSeq = GaNumFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                var bvs1 = bbsList[id1];
                if (bvs1.IsNullOrZero()) continue;

                var bvs2 = bbsList[id2];
                if (bvs2.IsNullOrZero()) continue;

                bbsList[id] = GaSymbolicsUtils.Cas[Mfs.Times[bvs1, bvs2]];
            }

            return bbsList;
        }

        public static GaSymMetricOrthogonal Create(IReadOnlyList<Expr> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GaSymMetricOrthogonal(vSpaceDim);

            bbsList[0] = Expr.INT_ONE;

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs.IsNullOrZero()) continue;

                bbsList[1 << m] = bvs;
            }

            var idsSeq = GaNumFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                id.SplitBySmallestBasisVectorId(out var id1, out var id2);

                var bvs1 = bbsList[id1];
                if (bvs1.IsNullOrZero()) continue;

                var bvs2 = bbsList[id2];
                if (bvs2.IsNullOrZero()) continue;

                bbsList[id] = GaSymbolicsUtils.Cas[Mfs.Times[bvs1, bvs2]];
            }

            return bbsList;
        }


        internal GaBinaryTreeInternalNode<Expr> RootNode { get; }

        public int VSpaceDimension 
            => RootNode.TreeDepth;

        public int GaSpaceDimension 
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public int Count 
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public Expr this[int index]
        {
            get
            {
                RootNode.TryGetLeafValue((ulong) index, out var value);

                return value ?? Expr.INT_ZERO;
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


        private GaSymMetricOrthogonal(int vSpaceDim)
        {
            RootNode = new GaBinaryTreeInternalNode<Expr>(0, vSpaceDim);
        }


        public MathematicaScalar GetSignature(int id)
        {
            return this[id].ToMathematicaScalar();
        }

        public IEnumerator<Expr> GetEnumerator()
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
