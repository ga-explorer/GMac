using System;
using GMac.GMacCompiler.Symbolic.Matrix;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic.Frame
{
    public sealed class GaFrameEuclidean : GaFrame
    {
        public override GaFrame BaseOrthogonalFrame => this;

        public override bool IsEuclidean => true;

        public override bool IsOrthogonal => true;

        public override bool IsOrthonormal => true;

        public override int VSpaceDimension { get; }


        public GaFrameEuclidean(int vspacedim)
        {
            VSpaceDimension = vspacedim;
        }


        protected override void ComputeIpm()
        {
            InnerProductMatrix = new MatrixIdentity(VSpaceDimension);
        }

        protected override void ComputeUnitPseudoScalarCoef()
        {
            InnerUnitPseudoScalarCoef = CasConstants.One;
        }
        //    if (GAUtils.ID_To_Reverse(MaxID))
        //        _UnitPseudoScalarCoef = CASConstants.MinusOne / this.BasisBladeSignature(MaxID)[0];
        //    else
        //        _UnitPseudoScalarCoef = CASConstants.One / this.BasisBladeSignature(MaxID)[0];
        //}

        public override MathematicaScalar BasisVectorSignature(int basisVectorIndex)
        {
            if (basisVectorIndex >= 0 && basisVectorIndex < VSpaceDimension)
                return CasConstants.One;

            throw new IndexOutOfRangeException();
        }

        public override GaMultivector BasisBladeSignature(int id)
        {
            if (id >= 0 && id < GaSpaceDimension)
                return GaMultivector.CreateScalar(GaSpaceDimension, CasConstants.One);

            throw new IndexOutOfRangeException();
        }

        public override GaMultivector Gp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanGp);
        }

        public override GaMultivector Sp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanSp);
        }

        public override GaMultivector Lcp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanLcp);
        }

        public override GaMultivector Rcp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanRcp);
        }

        public override GaMultivector Fdp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanFdp);
        }

        public override GaMultivector Hip(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanHip);
        }

        public override GaMultivector Acp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanAcp);
        }

        public override GaMultivector Cp(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.EuclideanBilinearProduct(mv2, EuclideanUtils.IsZeroEuclideanCp);
        }

    }
}
