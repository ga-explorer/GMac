namespace GMacModel.e3d
{
    public sealed partial class e3dPseudoScalar : e3dMultivector
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
        
        public e3dPseudoScalar Add(e3dZero mv)
        {
            return new e3dPseudoScalar()
            {
                G3I0 = G3I0
            };
        }
        
        public e3dPseudoScalar Subtract(e3dZero mv)
        {
            return new e3dPseudoScalar()
            {
                G3I0 = G3I0
            };
        }
        
        public bool IsEqual(e3dZero mv)
        {
            return !(
                G3I0 <= -Epsilon || G3I0 >= Epsilon
            );
        }
        
    }
}