using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Metrics
{
    public class GaSymMetricOrthogonal : IGaSymMetricOrthogonal
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

                bbsList[1UL << m] = bvs;
            }

            var idsSeq = GaFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
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

                bbsList[1UL << m] = bvs;
            }

            var idsSeq = GaFrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
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


        internal GaBtrInternalNode<Expr> RootNode { get; }

        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public Expr this[ulong index]
        {
            get
            {
                RootNode.TryGetLeafValue(VSpaceDimension, index, out var value);

                return value ?? Expr.INT_ZERO;
            }
            private set
            {
                var node = RootNode;
                for (var i = VSpaceDimension - 1; i > 0; i--)
                {
                    var bitPattern = (1UL << i) & index;
                    node = node.GetOrAddInternalChildNode(bitPattern != 0);
                }

                node.SetOrAddLeafChildNode((1 & index) != 0, value);
            }
        }


        private GaSymMetricOrthogonal(int vSpaceDim)
        {
            RootNode = new GaBtrInternalNode<Expr>();

            VSpaceDimension = vSpaceDim;
        }


        public MathematicaScalar GetSignature(ulong id)
        {
            return this[id].ToMathematicaScalar();
        }
    }
}
