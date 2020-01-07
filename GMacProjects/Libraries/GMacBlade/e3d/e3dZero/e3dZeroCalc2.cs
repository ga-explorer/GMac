namespace GMacModel.e3d
{
    public sealed partial class e3dZero : e3dMultivector
    {
        
        public e3dZero OP(e3dVector mv)
        {
            return Zero;
        }
        
        public e3dZero GP(e3dVector mv)
        {
            return Zero;
        }
        
        public e3dZero LCP(e3dVector mv)
        {
            return Zero;
        }
        
        public e3dZero RCP(e3dVector mv)
        {
            return Zero;
        }
        
        public double SP(e3dVector mv)
        {
            return 0.0D;
        }
        
        public e3dVector Add(e3dVector mv)
        {
            return new e3dVector()
            {
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2
            };
        }
        
        public e3dVector Subtract(e3dVector mv)
        {
            return new e3dVector()
            {
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2
            };
        }
        
        public bool IsEqual(e3dVector mv)
        {
            return !(
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon
            );
        }
        
    }
}