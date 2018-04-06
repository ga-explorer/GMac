using System.Collections.Generic;
using GMac.GMacMath.Structures;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic
{
    public static class SymbolicUtils
    {
        private static MathematicaInterface _cas;

        public static MathematicaInterface Cas
        {
            get
            {
                if (ReferenceEquals(_cas, null))
                    _cas = MathematicaInterface.Create();

                return _cas;
            }
        }

        public static MathematicaEvaluator Evaluator => Cas.Evaluator;

        public static MathematicaConstants Constants => Cas.Constants;


        public static bool IsNullOrZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsZero();
        }

        public static bool IsNullOrEqualZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsEqualZero();
        }

        public static MathematicaScalar ToMathematicaScalar(this Expr e)
        {
            return ReferenceEquals(e, null)
                ? Constants.Zero
                : MathematicaScalar.Create(Cas, e);
        }

        public static MathematicaScalar ToMathematicaScalar(this double e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }

        public static MathematicaScalar ToMathematicaScalar(this int e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }

        //public static BinaryTreeNode<MathematicaScalar> ComputeBasisBladesSignatures(this IReadOnlyList<MathematicaScalar> basisVectorsSignaturesList)
        //{
        //    var vSpaceDim = basisVectorsSignaturesList.Count;
        //    var bbsList = new BinaryTreeNode<MathematicaScalar>(vSpaceDim);

        //    bbsList.SetValue(0ul, Constants.One);

        //    for (var m = 0; m < vSpaceDim; m++)
        //    {
        //        var bvs = basisVectorsSignaturesList[m];

        //        if (bvs.IsNullOrZero()) continue;

        //        bbsList.SetValue(1ul << m, bvs);
        //    }

        //    var idsSeq = FrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
        //    foreach (var id in idsSeq)
        //    {
        //        int id1, id2;
        //        id.SplitBySmallestBasisVectorId(out id1, out id2);

        //        var bvs1 = bbsList.GetValue((ulong)id1);
        //        if (bvs1.IsNullOrZero()) continue;

        //        var bvs2 = bbsList.GetValue((ulong)id2);
        //        if (bvs2.IsNullOrZero()) continue;

        //        bbsList.SetValue((ulong)id, bvs1 * bvs2);
        //    }

        //    return bbsList;
        //}

        public static GMacBinaryTree<int> ComputeBasisBladesSignatures(this IReadOnlyList<int> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GMacBinaryTree<int>(vSpaceDim);

            bbsList.SetLeafValue(0ul, 1);

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs == 0) continue;

                bbsList.SetLeafValue(1ul << m, bvs);
            }

            var idsSeq = GMacMathUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                int id1, id2;
                id.SplitBySmallestBasisVectorId(out id1, out id2);

                var bvs1 = bbsList.GetLeafValue((ulong)id1);
                if (bvs1 == 0) continue;

                var bvs2 = bbsList.GetLeafValue((ulong)id2);
                if (bvs2 == 0) continue;

                bbsList.SetLeafValue((ulong)id, bvs1 * bvs2);
            }

            return bbsList;
        }

        public static GMacBinaryTree<double> ComputeBasisBladesSignatures(this IReadOnlyList<double> basisVectorsSignaturesList)
        {
            var vSpaceDim = basisVectorsSignaturesList.Count;
            var bbsList = new GMacBinaryTree<double>(vSpaceDim);

            bbsList.SetLeafValue(0ul, 1.0d);

            for (var m = 0; m < vSpaceDim; m++)
            {
                var bvs = basisVectorsSignaturesList[m];

                if (bvs == 0.0d) continue;

                bbsList.SetLeafValue(1ul << m, bvs);
            }

            var idsSeq = GMacMathUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);
            foreach (var id in idsSeq)
            {
                int id1, id2;
                id.SplitBySmallestBasisVectorId(out id1, out id2);

                var bvs1 = bbsList.GetLeafValue((ulong)id1);
                if (bvs1 == 0.0d) continue;

                var bvs2 = bbsList.GetLeafValue((ulong)id2);
                if (bvs2 == 0.0d) continue;

                bbsList.SetLeafValue((ulong)id, bvs1 * bvs2);
            }

            return bbsList;
        }


        public static GMacBinaryTree<MathematicaScalar> ToSymbolicTree(this GMacBinaryTree<int> rootTreeNode)
        {
            return rootTreeNode?.MapLeafValues(
                i => MathematicaScalar.Create(Cas, i)
            );
        }

        public static GMacBinaryTree<MathematicaScalar> ToSymbolicTree(this GMacBinaryTree<double> rootTreeNode)
        {
            return rootTreeNode?.MapLeafValues(
                i => MathematicaScalar.Create(Cas, i)
            );
        }
    }
}
