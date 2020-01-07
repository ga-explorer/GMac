namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector12 : e3dMultivector
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
        
        public e3dMultivector12 Add(e3dZero mv)
        {
            return new e3dMultivector12()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0
            };
        }
        
        public e3dMultivector12 Subtract(e3dZero mv)
        {
            return new e3dMultivector12()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon
            );
        }
        
    }
}