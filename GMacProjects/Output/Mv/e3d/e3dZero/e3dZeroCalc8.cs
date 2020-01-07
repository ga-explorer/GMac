using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dPseudoScalar mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dPseudoScalar mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dPseudoScalar mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dPseudoScalar mv)
        {
            return Zero;
        }
        
        public double SP(e3dPseudoScalar mv)
        {
            return 0.0D;
        }
        
        public e3dPseudoScalar Add(e3dPseudoScalar mv)
        {
            return new e3dPseudoScalar()
            {
                G3I0 = mv.G3I0
            };
        }
        
        public e3dPseudoScalar Subtract(e3dPseudoScalar mv)
        {
            return new e3dPseudoScalar()
            {
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dPseudoScalar mv)
        {
            return !(
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}