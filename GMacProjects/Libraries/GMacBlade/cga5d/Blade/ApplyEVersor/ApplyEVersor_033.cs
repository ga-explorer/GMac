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
        private static double[] ApplyEVersor_033(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:17 ص
            //Macro: main.cga5d.ApplyEVersor
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 21 sub-expressions, 0 generated temps, 21 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 10 total.
            //Computations: 1.96774193548387 average, 61 total.
            //Memory Reads: 1.96774193548387 average, 61 total.
            //Memory Writes: 31 total.
            
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
            tempVar0001 = (-1 * coefs1[0] * coefs2[5]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[5] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[6]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[6] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[7]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[7] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[8]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[8] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[9]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[9] = (-1 * tempVar0000 * tempVar0001);
            
            return c;
        }
        
    }
}
