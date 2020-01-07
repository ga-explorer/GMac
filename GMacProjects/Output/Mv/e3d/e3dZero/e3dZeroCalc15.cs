using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dFull mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dFull mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dFull mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dFull mv)
        {
            return Zero;
        }
        
        public double SP(e3dFull mv)
        {
            return 0.0D;
        }
        
        public e3dFull Add(e3dFull mv)
        {
            return new e3dFull()
            {
                G0I0 = mv.G0I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dFull Subtract(e3dFull mv)
        {
            return new e3dFull()
            {
                G0I0 = -mv.G0I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dFull mv)
        {
            return !(
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}