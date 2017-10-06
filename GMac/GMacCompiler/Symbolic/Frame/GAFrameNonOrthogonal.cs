using System;
using GMac.GMacCompiler.Symbolic.GAOM;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic.Frame
{
    public sealed class GaFrameNonOrthogonal : GaFrame
    {
        /// <summary>
        /// Derived Frame System
        /// </summary>
        public DerivedFrameSystem Dfs { get; }

        public GaOuterMorphism ThisToBaseFrameOm => Dfs.DerivedToBaseOm;

        public GaOuterMorphism BaseFrameToThisOm => Dfs.BaseToDerivedOm;


        public override GaFrame BaseOrthogonalFrame => Dfs.BaseFrame;

        public override bool IsEuclidean => false;

        public override bool IsOrthogonal => false;

        public override bool IsOrthonormal => false;

        public override int VSpaceDimension => InnerProductMatrix.Rows;


        internal GaFrameNonOrthogonal(GaFrame baseOrthoFrame, ISymbolicMatrix ipm, GaOuterMorphism derivedToBaseOm, GaOuterMorphism baseToDerivedOm)
        {
            if (baseOrthoFrame.IsOrthogonal == false)
                throw new GMacSymbolicException("Base frame must be orthogonal");

            if (ipm.IsSymmetric() == false || ipm.IsDiagonal())
                throw new GMacSymbolicException("Inner product matrix must be symmetric and non-diagonal");

            InnerProductMatrix = ipm.ToMathematicaMatrix();
            Dfs = new DerivedFrameSystem(baseOrthoFrame, this, derivedToBaseOm, baseToDerivedOm);
        }


        protected override void ComputeIpm()
        {
            throw new NotImplementedException();
        }

        protected override void ComputeUnitPseudoScalarCoef()
        {
            if (MaxBasisBladeId.BasisBladeIdHasNegativeReverse())
                InnerUnitPseudoScalarCoef = CasConstants.MinusOne / BasisBladeSignature(MaxBasisBladeId)[0];

            else
                InnerUnitPseudoScalarCoef = CasConstants.One / BasisBladeSignature(MaxBasisBladeId)[0];
        }

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            return InnerProductMatrix[basisVectorIndex, basisVectorIndex];
        }

        public override GaMultivector BasisBladeSignature(int id)
        {
            var basisBlade = GaMultivector.CreateBasisBlade(GaSpaceDimension, id);

            var sig = Gp(basisBlade, basisBlade);

            return id.BasisBladeIdHasNegativeReverse() ? -sig : sig;
        }

        public override GaMultivector Gp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Gp);
        }

        public override GaMultivector Sp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Sp);
        }

        public override GaMultivector Lcp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Lcp);
        }

        public override GaMultivector Rcp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Rcp);
        }

        public override GaMultivector Hip(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Hip);
        }

        public override GaMultivector Fdp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Fdp);
        }

        public override GaMultivector Acp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Acp);
        }

        public override GaMultivector Cp(GaMultivector mv1, GaMultivector mv2)
        {
            return BilinearProduct(mv1, mv2, BaseOrthogonalFrame.Cp);
        }


        private GaMultivector BilinearProduct(GaMultivector mv1, GaMultivector mv2, Func<GaMultivector, GaMultivector, GaMultivector> bofProduct)
        {
            var omv1 = Dfs.DerivedToBaseOm.Transform(mv1);
            var omv2 = Dfs.DerivedToBaseOm.Transform(mv2);

            var mv = bofProduct(omv1, omv2);

            return Dfs.BaseToDerivedOm.Transform(mv);
        }
    }
}
