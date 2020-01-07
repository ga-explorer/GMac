using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dPseudoVector mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dPseudoVector mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dPseudoVector mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dPseudoVector mv)
        {
            return Zero;
        }
        
        public double SP(e3dPseudoVector mv)
        {
            return 0.0D;
        }
        
        public e3dPseudoVector Add(e3dPseudoVector mv)
        {
            return new e3dPseudoVector()
            {
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2
            };
        }
        
        public e3dPseudoVector Subtract(e3dPseudoVector mv)
        {
            return new e3dPseudoVector()
            {
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2
            };
        }
        
        public bool IsEqual(e3dPseudoVector mv)
        {
            return !(
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon
            );
        }
        
    }
}