using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector5 mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dMultivector5 mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dMultivector5 mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dMultivector5 mv)
        {
            return Zero;
        }
        
        public double SP(e3dMultivector5 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector5 Add(e3dMultivector5 mv)
        {
            return new e3dMultivector5()
            {
                G0I0 = mv.G0I0,
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2
            };
        }
        
        public e3dMultivector5 Subtract(e3dMultivector5 mv)
        {
            return new e3dMultivector5()
            {
                G0I0 = -mv.G0I0,
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2
            };
        }
        
        public bool IsEqual(e3dMultivector5 mv)
        {
            return !(
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon
            );
        }
        
    }
}