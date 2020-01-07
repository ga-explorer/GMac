using System.IO;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static cga5dBlade[] SelfEGP_00(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga5dBlade(0, SelfEGP_000(coefs1, coefs2))
            };
        }
        
        private static cga5dBlade[] SelfEGP_11(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga5dBlade(0, SelfEGP_110(coefs1, coefs2)),
                new cga5dBlade(2, SelfEGP_112(coefs1, coefs2))
            };
        }
        
        private static cga5dBlade[] SelfEGP_22(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga5dBlade(0, SelfEGP_220(coefs1, coefs2)),
                new cga5dBlade(2, SelfEGP_222(coefs1, coefs2)),
                new cga5dBlade(4, SelfEGP_224(coefs1, coefs2))
            };
        }
        
        private static cga5dBlade[] SelfEGP_33(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga5dBlade(0, SelfEGP_330(coefs1, coefs2)),
                new cga5dBlade(2, SelfEGP_332(coefs1, coefs2)),
                new cga5dBlade(4, SelfEGP_334(coefs1, coefs2))
            };
        }
        
        private static cga5dBlade[] SelfEGP_44(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga5dBlade(0, SelfEGP_440(coefs1, coefs2)),
                new cga5dBlade(2, SelfEGP_442(coefs1, coefs2)),
                new cga5dBlade(4, SelfEGP_444(coefs1, coefs2))
            };
        }
        
        private static cga5dBlade[] SelfEGP_55(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga5dBlade(0, SelfEGP_550(coefs1, coefs2)),
                new cga5dBlade(2, SelfEGP_552(coefs1, coefs2)),
                new cga5dBlade(4, SelfEGP_554(coefs1, coefs2))
            };
        }
        
        public cga5dBlade[] SelfEGP(cga5dBlade blade2)
        {
            if (IsZero || blade2.IsZero)
                return new cga5dBlade[0];
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return SelfEGP_00(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 1
                case 1:
                    return SelfEGP_11(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 2
                case 2:
                    return SelfEGP_22(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 3
                case 3:
                    return SelfEGP_33(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 4
                case 4:
                    return SelfEGP_44(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 5
                case 5:
                    return SelfEGP_55(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
