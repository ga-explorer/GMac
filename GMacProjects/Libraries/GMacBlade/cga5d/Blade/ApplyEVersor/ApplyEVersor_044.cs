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
        private static double[] ApplyEVersor_044(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:17 ص
            //Macro: main.cga5d.ApplyEVersor
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 11 sub-expressions, 0 generated temps, 11 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1.9375 average, 31 total.
            //Memory Reads: 1.9375 average, 31 total.
            //Memory Writes: 16 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs1[0], -2);
            tempVar0001 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[0] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[1] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[2]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[2] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[3] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[4] = (-1 * tempVar0000 * tempVar0001);
            
            return c;
        }
        
    }
}
