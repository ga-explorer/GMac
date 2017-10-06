using System;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;

namespace GMac.GMacCompiler.Symbolic.GALT
{
    public sealed class GaLinearTransformMatrix : GaLinearTransform
    {
        public override int DomainVSpaceDim => FrameUtils.VSpaceDimension(DomainGaSpaceDim);

        public override int CodomainVSpaceDim => FrameUtils.VSpaceDimension(CodomainGaSpaceDim);

        public override int DomainGaSpaceDim => AssociatedMatrix.Columns;

        public override int CodomainGaSpaceDim => AssociatedMatrix.Rows;


        public GaLinearTransformMatrix(ISymbolicMatrix matrix)
        {
            if (matrix.Rows.IsValidGaSpaceDimension() == false)
                throw new GMacSymbolicException("Matrix rows not a GA dimension");

            if (matrix.Columns.IsValidGaSpaceDimension() == false)
                throw new GMacSymbolicException("Matrix columns not a GA dimension");

            InnerAssociatedMatrix = matrix;
        }


        public override GaMultivector Transform(GaMultivector mv)
        {
            //TODO: Complete this
            throw new NotImplementedException();
        }

        public override GaLinearTransform Adjoint()
        {
            return new GaLinearTransformMatrix(AssociatedMatrix.Transpose());
        }

        public override GaLinearTransform Inverse()
        {
            return new GaLinearTransformMatrix(AssociatedMatrix.Inverse());
        }

        public override GaLinearTransform InverseAdjoint()
        {
            return new GaLinearTransformMatrix(AssociatedMatrix.InverseTranspose());
        }

        protected override void ComputeAssociatedMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
