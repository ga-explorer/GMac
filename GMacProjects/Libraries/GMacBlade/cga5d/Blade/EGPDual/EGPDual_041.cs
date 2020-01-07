﻿namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] EGPDual_041(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:09 ص
            //Macro: main.cga5d.EGPDual
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 5 total.
            //Computations: 1.71428571428571 average, 12 total.
            //Memory Reads: 1.71428571428571 average, 12 total.
            //Memory Writes: 7 total.
            
            double tempVar0000;
            
            c[0] = (-1 * coefs1[0] * coefs2[4]);
            c[2] = (-1 * coefs1[0] * coefs2[2]);
            c[4] = (-1 * coefs1[0] * coefs2[0]);
            tempVar0000 = (-1 * coefs1[0] * coefs2[3]);
            c[1] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[1]);
            c[3] = (-1 * tempVar0000);
            
            return c;
        }
        
    }
}