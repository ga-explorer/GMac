using GMac.GMacCompiler.Symbolic.GALT;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic.GAOM
{
    public abstract class GaOuterMorphism : GaLinearTransform
    {
        //public Mathematica CAS { get; private set; }

        //public MathematicaConnection CASConnection { get { return CAS.Connection; } }

        //public MathematicaExprEvaluator CASEvaluator { get { return CAS.Evaluator; } }

        //public MathematicaConstants CASConstants { get { return CAS.Constants; } }


        public abstract ISymbolicMatrix VectorTransformMatrix { get; }

        public abstract ISymbolicMatrix MultivectorTransformMatrix { get; }

        public abstract MathematicaScalar Determinant { get; }

        //public abstract int DomainVSpaceDim { get; }

        //public abstract int CodomainVSpaceDim { get; }


        public override int DomainGaSpaceDim => FrameUtils.GaSpaceDimension(DomainVSpaceDim);

        public override int CodomainGaSpaceDim => FrameUtils.GaSpaceDimension(CodomainVSpaceDim);


        public override GaLinearTransform Adjoint()
        {
            return AdjointOm();
        }

        public override GaLinearTransform Inverse()
        {
            return InverseOm();
        }

        public override GaLinearTransform InverseAdjoint()
        {
            return InverseAdjointOm();
        }

        //public abstract GAMultivectorCoefficients Transform(GAMultivectorCoefficients mv);

        public abstract GaOuterMorphism AdjointOm();

        public abstract GaOuterMorphism InverseOm();

        public abstract GaOuterMorphism InverseAdjointOm();


        protected override void ComputeAssociatedMatrix()
        {
            InnerAssociatedMatrix = MultivectorTransformMatrix;
        }
    }
}
