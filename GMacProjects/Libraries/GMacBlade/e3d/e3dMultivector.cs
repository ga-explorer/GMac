
namespace GMacModel.e3d
{
    public abstract class e3dMultivector
    {
        #region Static Members

        public static e3dZero Zero { get; private set; }

        public static double Epsilon { get; set; }

        static e3dMultivector()
        {
            Epsilon = 1e-12;
            Zero = new e3dZero();
        }

        #endregion

        public abstract double Coef0 { get; }
        public abstract double Coef1 { get; }
        public abstract double Coef2 { get; }
        public abstract double Coef3 { get; }
        public abstract double Coef4 { get; }
        public abstract double Coef5 { get; }
        public abstract double Coef6 { get; }
        public abstract double Coef7 { get; }
        

        public int ClassId { get; protected set; }


        public abstract double Norm2 { get; }

        public abstract bool IsZero { get; }


        public abstract bool IsEqual(e3dMultivector mv);

        public abstract e3dMultivector Simplify();

        public abstract e3dMultivector OP(e3dMultivector mv);

        public abstract e3dMultivector GP(e3dMultivector mv);

        public abstract e3dMultivector LCP(e3dMultivector mv);

        public abstract e3dMultivector RCP(e3dMultivector mv);

        public abstract double SP(e3dMultivector mv);

        public abstract e3dMultivector Add(e3dMultivector mv);

        public abstract e3dMultivector Subtract(e3dMultivector mv);
    }
}
