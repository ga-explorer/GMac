using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector10 : e3dMultivector
    {
        
        public e3dZero OP(e3dZero mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dZero mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dZero mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dZero mv)
        {
            return Zero;
        }
        
        public double SP(e3dZero mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector10 Add(e3dZero mv)
        {
            return new e3dMultivector10()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G3I0 = G3I0
            };
        }
        
        public e3dMultivector10 Subtract(e3dZero mv)
        {
            return new e3dMultivector10()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G3I0 = G3I0
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon
            );
        }
        
    }
}