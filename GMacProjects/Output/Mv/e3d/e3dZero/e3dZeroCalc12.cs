using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector12 mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dMultivector12 mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dMultivector12 mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dMultivector12 mv)
        {
            return Zero;
        }
        
        public double SP(e3dMultivector12 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector12 Add(e3dMultivector12 mv)
        {
            return new e3dMultivector12()
            {
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dMultivector12 Subtract(e3dMultivector12 mv)
        {
            return new e3dMultivector12()
            {
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector12 mv)
        {
            return !(
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}