using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector10 mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dMultivector10 mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dMultivector10 mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dMultivector10 mv)
        {
            return Zero;
        }
        
        public double SP(e3dMultivector10 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector10 Add(e3dMultivector10 mv)
        {
            return new e3dMultivector10()
            {
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dMultivector10 Subtract(e3dMultivector10 mv)
        {
            return new e3dMultivector10()
            {
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector10 mv)
        {
            return !(
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}