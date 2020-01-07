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
        public cga5dBlade OP(cga5dBlade blade2)
        {
            if (IsZero || blade2.IsZero || Grade + blade2.Grade > MaxGrade)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga5dBlade(0, OP_000(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 1
                case 6:
                    return new cga5dBlade(1, OP_011(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 2
                case 12:
                    return new cga5dBlade(2, OP_022(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 3
                case 18:
                    return new cga5dBlade(3, OP_033(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 4
                case 24:
                    return new cga5dBlade(4, OP_044(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 5
                case 30:
                    return new cga5dBlade(5, OP_055(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 0
                case 1:
                    return new cga5dBlade(1, OP_101(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 1
                case 7:
                    return new cga5dBlade(2, OP_112(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 2
                case 13:
                    return new cga5dBlade(3, OP_123(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 3
                case 19:
                    return new cga5dBlade(4, OP_134(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 4
                case 25:
                    return new cga5dBlade(5, OP_145(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 0
                case 2:
                    return new cga5dBlade(2, OP_202(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 1
                case 8:
                    return new cga5dBlade(3, OP_213(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 2
                case 14:
                    return new cga5dBlade(4, OP_224(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 3
                case 20:
                    return new cga5dBlade(5, OP_235(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 0
                case 3:
                    return new cga5dBlade(3, OP_303(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 1
                case 9:
                    return new cga5dBlade(4, OP_314(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 2
                case 15:
                    return new cga5dBlade(5, OP_325(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 0
                case 4:
                    return new cga5dBlade(4, OP_404(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 1
                case 10:
                    return new cga5dBlade(5, OP_415(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 0
                case 5:
                    return new cga5dBlade(5, OP_505(Coefs, blade2.Coefs));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
