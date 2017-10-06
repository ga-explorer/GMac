using GMac.GMacCompiler.Symbolic.Matrix;
using GMac.GMacUtils;

namespace GMac.GMacCompiler.Symbolic.GALT
{
    public sealed class GaLinearTransformIdentity : GaLinearTransform
    {
        private readonly int _gaSpaceDim;

        public override int DomainVSpaceDim => FrameUtils.VSpaceDimension(_gaSpaceDim);

        public override int CodomainVSpaceDim => FrameUtils.VSpaceDimension(_gaSpaceDim);

        public override int DomainGaSpaceDim => _gaSpaceDim;

        public override int CodomainGaSpaceDim => _gaSpaceDim;


        public GaLinearTransformIdentity(int gaspacedim)
        {
            _gaSpaceDim = gaspacedim;
        }


        protected override void ComputeAssociatedMatrix()
        {
            InnerAssociatedMatrix = new MatrixIdentity(_gaSpaceDim);
        }


        public override GaMultivector Transform(GaMultivector mv)
        {
            return mv;
        }

        public override GaLinearTransform Adjoint()
        {
            return this;
        }

        public override GaLinearTransform Inverse()
        {
            return this;
        }

        public override GaLinearTransform InverseAdjoint()
        {
            return this;
        }
    }
}
