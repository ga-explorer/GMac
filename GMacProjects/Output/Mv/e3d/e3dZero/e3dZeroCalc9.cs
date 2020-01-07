using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector9 mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dMultivector9 mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dMultivector9 mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dMultivector9 mv)
        {
            return Zero;
        }
        
        public double SP(e3dMultivector9 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector9 Add(e3dMultivector9 mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = mv.G0I0,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dMultivector9 Subtract(e3dMultivector9 mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = -mv.G0I0,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector9 mv)
        {
            return !(
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}