namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector14 : e3dMultivector
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
        
        public e3dMultivector14 Add(e3dZero mv)
        {
            return new e3dMultivector14()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0
            };
        }
        
        public e3dMultivector14 Subtract(e3dZero mv)
        {
            return new e3dMultivector14()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = G3I0
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                G3I0 <= -Epsilon || G3I0 >= Epsilon
            );
        }
        
    }
}