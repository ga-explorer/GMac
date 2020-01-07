﻿namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] ApplyERotor_144(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:18 ص
            //Macro: main.cga5d.ApplyERotor
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 79 sub-expressions, 0 generated temps, 79 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 5 total.
            //Computations: 1.30952380952381 average, 110 total.
            //Memory Reads: 2 average, 168 total.
            //Memory Writes: 84 total.
            
            double[] tempArray = new double[9];
            
            tempArray[0] = (coefs1[3] * coefs2[0]);
            tempArray[1] = (coefs1[4] * coefs2[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[2] * coefs2[0]);
            tempArray[3] = (coefs1[4] * coefs2[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs1[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (coefs1[1] * coefs2[0]);
            tempArray[4] = (coefs1[4] * coefs2[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[0] * coefs2[0]);
            tempArray[5] = (coefs1[4] * coefs2[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[6] = (coefs1[3] * coefs2[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[2] * coefs2[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs1[1] * coefs2[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[0] * coefs2[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[4] * tempArray[5]);
            c[0] = (tempArray[1] + tempArray[6]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[0]);
            tempArray[1] = (-1 * coefs1[2] * coefs2[1]);
            tempArray[6] = (-1 * coefs1[3] * coefs2[2]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (coefs1[2] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (coefs1[1] * coefs2[1]);
            tempArray[7] = (-1 * coefs1[3] * coefs2[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[1] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[0] * coefs2[1]);
            tempArray[8] = (-1 * coefs1[3] * coefs2[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[0] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[8] = (coefs1[3] * tempArray[5]);
            c[1] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[2]);
            tempArray[1] = (coefs1[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[1] * coefs2[2]);
            tempArray[2] = (coefs1[2] * coefs2[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[0] * coefs2[2]);
            tempArray[8] = (coefs1[2] * coefs2[4]);
            tempArray[2] = (tempArray[2] + tempArray[8]);
            tempArray[8] = (coefs1[0] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[2] * tempArray[5]);
            c[2] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[1] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * coefs2[3]);
            tempArray[3] = (-1 * coefs1[1] * coefs2[4]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (coefs1[0] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (coefs1[1] * tempArray[5]);
            c[3] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[4]);
            tempArray[3] = (coefs1[3] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[2] = (-1 * coefs1[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (coefs1[1] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[5]);
            c[4] = (tempArray[0] + tempArray[1]);
            
            return c;
        }
        
    }
}