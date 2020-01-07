using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector9 : e3dMultivector
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
        
        public e3dMultivector9 Add(e3dZero mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0,
                G3I0 = G3I0
            };
        }
        
        public e3dMultivector9 Subtract(e3dZero mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0,
                G3I0 = G3I0
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon
            );
        }
        
    }
}