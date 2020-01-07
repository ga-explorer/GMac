namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dMultivector3 mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dMultivector3 mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dMultivector3 mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dMultivector3 mv)
        {
            return Zero;
        }
        
        public double SP(e3dMultivector3 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector3 Add(e3dMultivector3 mv)
        {
            return new e3dMultivector3()
            {
                G0I0 = mv.G0I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2
            };
        }
        
        public e3dMultivector3 Subtract(e3dMultivector3 mv)
        {
            return new e3dMultivector3()
            {
                G0I0 = -mv.G0I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2
            };
        }
        
        public bool IsEqual(e3dMultivector3 mv)
        {
            return !(
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon
            );
        }
        
    }
}