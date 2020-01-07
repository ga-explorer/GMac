using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
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
        
        public e3dZero Add(e3dZero mv)
        {
            return Zero;
        }
        
        public e3dZero Subtract(e3dZero mv)
        {
            return Zero;
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return true;
        }
        
    }
}