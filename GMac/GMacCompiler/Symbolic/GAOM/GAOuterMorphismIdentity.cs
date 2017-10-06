using System;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic.GAOM
{
    public sealed class GaOuterMorphismIdentity : GaOuterMorphism
    {
        private readonly int _vSpaceDim;

        
        public GaOuterMorphismIdentity(int vspacedim)
        {
            _vSpaceDim = vspacedim;
        }


        public override ISymbolicMatrix VectorTransformMatrix
        {
            get { throw new NotImplementedException(); }
        }

        public override ISymbolicMatrix MultivectorTransformMatrix
        {
            get { throw new NotImplementedException(); }
        }

        public override MathematicaScalar Determinant => CasConstants.One;

        public override int DomainVSpaceDim => _vSpaceDim;

        public override int CodomainVSpaceDim => _vSpaceDim;

        public override GaMultivector Transform(GaMultivector mv)
        {
            return mv;
        }

        public override GaOuterMorphism AdjointOm()
        {
            return this;
        }

        public override GaOuterMorphism InverseOm()
        {
            return this;
        }

        public override GaOuterMorphism InverseAdjointOm()
        {
            return this;
        }
    }
}
