namespace GMacModel.Prototypes.MvLib
{
    public abstract class cga5dMultivector
    {
        public abstract double Coef00000 { get; }

        public abstract double Coef00001 { get; }

        public abstract double Coef00010 { get; }

        public abstract double Coef00011 { get; }


        public abstract bool IsKVector { get; }

        public abstract bool IsBlade { get; }

        public abstract bool IsScalar { get; }

        public abstract bool IsTerm { get; }

        public abstract bool IsEven { get; }

        public abstract bool IsOdd { get; }


        public abstract double Norm2 { get; }


        public abstract cga5dMultivector Simplify();


        public abstract cga5dMultivector GP(cga5dMultivector mv2);


    }
}
