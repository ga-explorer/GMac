
using SymbolicInterface.Mathematica;

namespace GMac.GMacCompiler.Symbolic.GALT
{
    public abstract class GaLinearTransform : ISymbolicObject
    {
        protected ISymbolicMatrix InnerAssociatedMatrix;


        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;


        public abstract int DomainVSpaceDim { get; }

        public abstract int CodomainVSpaceDim { get; }


        protected abstract void ComputeAssociatedMatrix();

        public ISymbolicMatrix AssociatedMatrix 
        {
            get
            {
                if (ReferenceEquals(InnerAssociatedMatrix, null))
                    ComputeAssociatedMatrix();

                return InnerAssociatedMatrix;
            }
        }

        public abstract int DomainGaSpaceDim { get; }

        public abstract int CodomainGaSpaceDim { get; }

        
        protected GaLinearTransform()
        {
            CasInterface = SymbolicUtils.Cas;
        }


        public abstract GaMultivector Transform(GaMultivector mv);

        public abstract GaLinearTransform Adjoint();

        public abstract GaLinearTransform Inverse();

        public abstract GaLinearTransform InverseAdjoint();
    }
}
