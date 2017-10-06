using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica.Expression;
using TextComposerLib.Text.Structured;

namespace GMac.GMacCompiler.Symbolic.Frame
{
    public sealed class GaFrameOrthonormal : GaFrame
    {
        private static int[] ComputeBasisBladesSignatures(int[] frameSigList)
        {
            var vSpaceDim = frameSigList.Length;
            var bbsList = new int[FrameUtils.GaSpaceDimension(vSpaceDim)];

            bbsList[0] = 1;

            for (var m = 0; m < vSpaceDim; m++)
                bbsList[1 << m] = frameSigList[m];

            var idsSeq = FrameUtils.BasisBladeIDsSortedByGrade(vSpaceDim, 2);

            foreach (var id in idsSeq)
            {
                int id1, id2;

                id.SplitBySmallestBasisVectorId(out id1, out id2);

                bbsList[id] = bbsList[id1] * bbsList[id2];

            }

            return bbsList;
        }


        /// <summary>
        /// Number of positive signature basis vectors
        /// </summary>
        public int PsvCount 
        {
            get { return _basisVectorsSignatures.Count(s => s > 0); }
        }

        /// <summary>
        /// Number of nigative signature basis vectors
        /// </summary>
        public int NsvCount
        {
            get { return _basisVectorsSignatures.Count(s => s < 0); }
        }

        private readonly int[] _basisVectorsSignatures;

        private readonly int[] _basisBladesSignatures;


        public override GaFrame BaseOrthogonalFrame => this;

        public override bool IsEuclidean => !_basisVectorsSignatures.Contains(-1);

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => true;

        public override int VSpaceDimension => _basisVectorsSignatures.Length;


        /// <summary>
        /// frame_sig_list must have all items equal to 1 or -1 with at least one negative item
        /// </summary>
        /// <param name="frameSigList"></param>
        public GaFrameOrthonormal(int[] frameSigList)
        {
            _basisVectorsSignatures = frameSigList;
            _basisBladesSignatures = ComputeBasisBladesSignatures(frameSigList);
        }


        protected override void ComputeIpm()
        {
            var bvSig = _basisVectorsSignatures.Select(i => MathematicaScalar.Create(CasInterface, i));

            var v = MathematicaVector.CreateFullVector(CasInterface, bvSig);

            InnerProductMatrix = MathematicaMatrix.CreateDiagonal(v);
        }

        protected override void ComputeUnitPseudoScalarCoef()
        {
            InnerUnitPseudoScalarCoef = 
                (_basisBladesSignatures[MaxBasisBladeId] == 1) == !MaxBasisBladeId.BasisBladeIdHasNegativeReverse() 
                ? CasConstants.One 
                : CasConstants.MinusOne;
        }

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            if (basisVectorIndex >= 0 && basisVectorIndex < VSpaceDimension)
            {
                return _basisVectorsSignatures[basisVectorIndex] == 1 ? CasConstants.One : CasConstants.MinusOne;
            }

            throw new IndexOutOfRangeException();
        }

        public override GaMultivector BasisBladeSignature(int id)
        {
            if (id < 0 || id >= GaSpaceDimension) 
                throw new IndexOutOfRangeException();

            var s = _basisBladesSignatures[id] == 1 ? CasConstants.One : CasConstants.MinusOne;

            GaMultivector.CreateScalar(GaSpaceDimension, s);

            throw new IndexOutOfRangeException();
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

        //            if (GAUtils.IDs_To_EGP_Sign(id1, id2) != (_BasisBladesSignatures[id] == -1))
        //                mv[id] -= coef1 * coef2;
        //            else
        //                mv[id] += coef1 * coef2;
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
                            FrameUtils.IsNegativeEGp(id1, id2) != (_basisBladesSignatures[sigId] == -1)
                            ); 

                    accumExprDict.AddTerm(resultId, resultCoefDelta);
                }
            }

            return accumExprDict.ToMultivector(mv1.GaSpaceDim);
        }
    }
}
