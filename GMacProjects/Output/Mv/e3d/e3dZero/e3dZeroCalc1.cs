using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dScalar mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dScalar mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dScalar mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dScalar mv)
        {
            return Zero;
        }
        
        public double SP(e3dScalar mv)
        {
            return 0.0D;
        }
        
        public e3dScalar Add(e3dScalar mv)
        {
            return new e3dScalar()
            {
                G0I0 = mv.G0I0
            };
        }
        
        public e3dScalar Subtract(e3dScalar mv)
        {
            return new e3dScalar()
            {
                G0I0 = -mv.G0I0
            };
        }
        
        public bool IsEqual(e3dScalar mv)
        {
            return !(
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon
            );
        }
        
    }
}