namespace GMacModel.Prototypes.MvLib
{
    public sealed class cga5dVector
    {
        public double Coef00001 { get; private set; }

        public double Coef00010 { get; private set; }
        
        public double Coef00100 { get; private set; }
        
        public double Coef01000 { get; private set; }
        
        public double Coef10000 { get; private set; }

        public int Grade { get { return 1; } }

        public bool IsBlade { get { return true; } }

    }
}
