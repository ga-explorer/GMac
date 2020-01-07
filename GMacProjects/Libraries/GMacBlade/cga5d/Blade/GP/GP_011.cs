using System;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] GP_011(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:54:13 ص
            //Macro: main.cga5d.GP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 11 sub-expressions, 0 generated temps, 11 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 5 total.
            //Computations: 1.375 average, 22 total.
            //Memory Reads: 1.875 average, 30 total.
            //Memory Writes: 16 total.
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            c[1] = (coefs1[0] * coefs2[1]);
            c[2] = (-1 * coefs1[0] * coefs2[2]);
            c[3] = (-1 * coefs1[0] * coefs2[3]);
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (coefs2[0] * tempVar0000);
            tempVar0002 = (coefs2[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            tempVar0001 = (tempVar0000 * tempVar0001);
            tempVar0003 = (-1 * coefs2[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (-1 * coefs1[0] * tempVar0002);
            tempVar0003 = (-1 * tempVar0000 * tempVar0002);
            c[0] = (tempVar0001 + tempVar0003);
            tempVar0000 = (tempVar0000 * tempVar0002);
            c[4] = (tempVar0001 + tempVar0000);
            
            return c;
        }
        
    }
}
