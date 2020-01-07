namespace GMacModel.e3d
{
    public sealed partial class e3dScalar : e3dMultivector
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
        
        public e3dScalar Add(e3dZero mv)
        {
            return new e3dScalar()
            {
                G0I0 = G0I0
            };
        }
        
        public e3dScalar Subtract(e3dZero mv)
        {
            return new e3dScalar()
            {
                G0I0 = G0I0
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon
            );
        }
        
    }
}