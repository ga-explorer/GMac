using System;
using System.Collections.Generic;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib.Text.Structured;

namespace GMac.GMacCompiler.Symbolic.Frame
{
    public sealed class GaFrameOrthogonal : GaFrame
    {
        private static MathematicaScalar[] ComputeBasisBladesSignatures(MathematicaScalar[] basisVectorsSignaturesList)
        {
            var vspacedim = basisVectorsSignaturesList.Length;
            var bbsList = new MathematicaScalar[FrameUtils.GaSpaceDimension(vspacedim)];

            bbsList[0] = SymbolicUtils.Constants.One;

            for (var m = 0; m < vspacedim; m++)
                bbsList[1 << m] = basisVectorsSignaturesList[m];

            var idsSeq = FrameUtils.BasisBladeIDsSortedByGrade(vspacedim, 2);

            foreach (var id in idsSeq)
            {
                int id1, id2;

                id.SplitBySmallestBasisVectorId(out id1, out id2);

                bbsList[id] = bbsList[id1] * bbsList[id2];
            }

            return bbsList;
        }

        
        private readonly MathematicaScalar[] _basisVectorsSignatures;

        private readonly MathematicaScalar[] _basisBladesSignatures;


        public override GaFrame BaseOrthogonalFrame => this;

        public override bool IsEuclidean => false;

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => false;

        public override int VSpaceDimension => _basisVectorsSignatures.Length;


        public GaFrameOrthogonal(MathematicaScalar[] basisVectorsSignaturesList)
        {
            _basisVectorsSignatures = basisVectorsSignaturesList;
            _basisBladesSignatures = ComputeBasisBladesSignatures(basisVectorsSignaturesList);
        }


        protected override void ComputeIpm()
        {
            var v = MathematicaVector.CreateFullVector(CasInterface, _basisVectorsSignatures);

            InnerProductMatrix = MathematicaMatrix.CreateDiagonal(v);
        }

        protected override void ComputeUnitPseudoScalarCoef()
        {
            if (MaxBasisBladeId.BasisBladeIdHasNegativeReverse())
                InnerUnitPseudoScalarCoef = CasConstants.MinusOne / _basisBladesSignatures[MaxBasisBladeId];

            else
                InnerUnitPseudoScalarCoef = CasConstants.One / _basisBladesSignatures[MaxBasisBladeId];
        }

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            return _basisVectorsSignatures[basisVectorIndex];
        }

        public override GaMultivector BasisBladeSignature(int id)
        {
            return GaMultivector.CreateScalar(GaSpaceDimension, _basisBladesSignatures[id]);
        }

        public override GaMultivector Gp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanGp);
        }

        public override GaMultivector Sp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanSp);
        }

        public override GaMultivector Lcp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanLcp);
        }

        public override GaMultivector Rcp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanRcp);
        }

        public override GaMultivector Fdp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanFdp);
        }

        public override GaMultivector Hip(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanHip);
        }

        public override GaMultivector Acp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanAcp);
        }

        public override GaMultivector Cp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, EuclideanUtils.IsZeroEuclideanCp);
        }

        //TODO: This requires more acceleration (try to build the expressions then evaluate once per basis blade id for result_mv)
        //private GAMultivectorCoefficients BilinearProduct(GAMultivectorCoefficients mv1, GAMultivectorCoefficients mv2, Func<int, int, bool> term_discard_function)
        //{
        //    if (mv1.GASpaceDim != mv2.GASpaceDim || mv1.GASpaceDim != GASpaceDim)
        //        throw new GMacSymbolicException("Multivector size mismatch");

        //    GAMultivectorCoefficients mv = GAMultivectorCoefficients.CreateZero(mv1.GASpaceDim);

        //    foreach (var term1 in mv1)
        //    {
        //        int id1 = term1.Key;
        //        MathematicaScalar coef1 = term1.Value;

        //        foreach (var term2 in mv2.FilterTermsUsing(id1, term_discard_function))
        //        {
        //            int id2 = term2.Key;
        //            MathematicaScalar coef2 = term2.Value;

        //            int id = id1 ^ id2;

        //            if (GAUtils.IDs_To_EGP_Sign(id1, id2))
        //                mv[id] -= coef1 * coef2 * _BasisBladesSignatures[id];
        //            else
        //                mv[id] += coef1 * coef2 * _BasisBladesSignatures[id];
        //        }
        //    }

        //    return mv;
        //}
        private GaMultivector BilinearProduct(GaMultivector mv1, GaMultivector mv2, Func<int, int, bool> termDiscardFunction)
        {
            if (mv1.GaSpaceDim != mv2.GaSpaceDim || mv1.GaSpaceDim != GaSpaceDimension)
                throw new GMacSymbolicException("Multivector size mismatch");

            var terms1 = mv1.ToStringsDictionary();

            var terms2 = mv2.ToStringsDictionary();

            var accumExprDict = new Dictionary<int, ListComposer>();

            foreach (var term1 in terms1)
            {
                var id1 = term1.Key;
                var coef1 = term1.Value;

                foreach (var term2 in terms2.FilterTermsUsing(id1, termDiscardFunction))
                {
                    var id2 = term2.Key;
                    var coef2 = term2.Value;

                    var resultId = id1 ^ id2;
                    var sigId = id1 & id2;

                    var resultCoefDelta =
                        EuclideanUtils.Times(
                            coef1,
                            coef2,
                            _basisBladesSignatures[sigId].MathExpr.ToString(),
                            FrameUtils.IsNegativeEGp(id1, id2)
                            );

                    accumExprDict.AddTerm(resultId, resultCoefDelta);
                }
            }

            return accumExprDict.ToMultivector(mv1.GaSpaceDim);
        }

    }
}
