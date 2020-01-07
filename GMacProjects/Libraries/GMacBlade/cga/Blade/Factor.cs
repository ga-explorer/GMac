using System;
using System.IO;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static int MaxCoefId2(double[] coefs)
        {
            var c = Math.Abs(coefs[0]);
            var maxCoef = c;
            var maxCoefId = 3;
        
            c = Math.Abs(coefs[1]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 5;
            }
            
            c = Math.Abs(coefs[2]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 6;
            }
            
            c = Math.Abs(coefs[3]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 9;
            }
            
            c = Math.Abs(coefs[4]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 10;
            }
            
            c = Math.Abs(coefs[5]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 12;
            }
            
            c = Math.Abs(coefs[6]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 17;
            }
            
            c = Math.Abs(coefs[7]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 18;
            }
            
            c = Math.Abs(coefs[8]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 20;
            }
            
            c = Math.Abs(coefs[9]);
            if (c > maxCoef)
                maxCoefId = 24;
        
            return maxCoefId;
        }
        
        private static cga0001Vector[] FactorGrade2(double[] coefs)
        {
            var maxCoefId = MaxCoefId2(coefs);
        
            switch (maxCoefId)
            {
                case 5:
                    return Factor5(coefs);
                case 6:
                    return Factor6(coefs);
                case 9:
                    return Factor9(coefs);
                case 10:
                    return Factor10(coefs);
                case 12:
                    return Factor12(coefs);
                case 17:
                    return Factor17(coefs);
                case 18:
                    return Factor18(coefs);
                case 20:
                    return Factor20(coefs);
                case 24:
                    return Factor24(coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        private static int MaxCoefId3(double[] coefs)
        {
            var c = Math.Abs(coefs[0]);
            var maxCoef = c;
            var maxCoefId = 7;
        
            c = Math.Abs(coefs[1]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 11;
            }
            
            c = Math.Abs(coefs[2]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 13;
            }
            
            c = Math.Abs(coefs[3]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 14;
            }
            
            c = Math.Abs(coefs[4]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 19;
            }
            
            c = Math.Abs(coefs[5]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 21;
            }
            
            c = Math.Abs(coefs[6]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 22;
            }
            
            c = Math.Abs(coefs[7]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 25;
            }
            
            c = Math.Abs(coefs[8]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 26;
            }
            
            c = Math.Abs(coefs[9]);
            if (c > maxCoef)
                maxCoefId = 28;
        
            return maxCoefId;
        }
        
        private static cga0001Vector[] FactorGrade3(double[] coefs)
        {
            var maxCoefId = MaxCoefId3(coefs);
        
            switch (maxCoefId)
            {
                case 11:
                    return Factor11(coefs);
                case 13:
                    return Factor13(coefs);
                case 14:
                    return Factor14(coefs);
                case 19:
                    return Factor19(coefs);
                case 21:
                    return Factor21(coefs);
                case 22:
                    return Factor22(coefs);
                case 25:
                    return Factor25(coefs);
                case 26:
                    return Factor26(coefs);
                case 28:
                    return Factor28(coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        private static int MaxCoefId4(double[] coefs)
        {
            var c = Math.Abs(coefs[0]);
            var maxCoef = c;
            var maxCoefId = 15;
        
            c = Math.Abs(coefs[1]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 23;
            }
            
            c = Math.Abs(coefs[2]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 27;
            }
            
            c = Math.Abs(coefs[3]);
            if (c > maxCoef)
            {
                maxCoef = c;
                maxCoefId = 29;
            }
            
            c = Math.Abs(coefs[4]);
            if (c > maxCoef)
                maxCoefId = 30;
        
            return maxCoefId;
        }
        
        private static cga0001Vector[] FactorGrade4(double[] coefs)
        {
            var maxCoefId = MaxCoefId4(coefs);
        
            switch (maxCoefId)
            {
                case 23:
                    return Factor23(coefs);
                case 27:
                    return Factor27(coefs);
                case 29:
                    return Factor29(coefs);
                case 30:
                    return Factor30(coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public cga0001FactoredBlade Factor()
        {
            if (IsZero)
                return new cga0001FactoredBlade(0.0D);
        
            switch (Grade)
            {
                case 0:
                    return new cga0001FactoredBlade(Coefs[0]);
        
                case 1:
                    var vector = ToVector();
                    var norm = vector.Normalize();
                    return new cga0001FactoredBlade(norm, vector);
        
                case 2:
                    return new cga0001FactoredBlade(EMag_2(Coefs), FactorGrade2(Coefs));
                
                case 3:
                    return new cga0001FactoredBlade(EMag_3(Coefs), FactorGrade3(Coefs));
                
                case 4:
                    return new cga0001FactoredBlade(EMag_4(Coefs), FactorGrade4(Coefs));
                
                case 5:
                    return new cga0001FactoredBlade(Coefs[0],  cga0001Vector.BasisVectors());
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
