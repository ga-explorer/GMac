using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector5 : e3dMultivector
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
        
        public e3dMultivector5 Add(e3dZero mv)
        {
            return new e3dMultivector5()
            {
                G0I0 = G0I0,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2
            };
        }
        
        public e3dMultivector5 Subtract(e3dZero mv)
        {
            return new e3dMultivector5()
            {
                G0I0 = G0I0,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon
            );
        }
        
    }
}