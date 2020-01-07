﻿using System;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] GP_112(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:54:17 ص
            //Macro: main.cga5d.GP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 47 sub-expressions, 0 generated temps, 47 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 10 total.
            //Computations: 1.26315789473684 average, 72 total.
            //Memory Reads: 1.94736842105263 average, 111 total.
            //Memory Writes: 57 total.
            
            double[] tempArray = new double[9];
            
            tempArray[0] = (coefs1[2] * coefs2[1]);
            tempArray[1] = (-1 * coefs1[1] * coefs2[2]);
            c[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (coefs1[3] * coefs2[1]);
            tempArray[1] = (-1 * coefs1[1] * coefs2[3]);
            c[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * coefs1[3] * coefs2[2]);
            tempArray[1] = (coefs1[2] * coefs2[3]);
            c[5] = (tempArray[0] + tempArray[1]);
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs1[0] * tempArray[0]);
            tempArray[2] = (coefs1[4] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (coefs2[0] * tempArray[0]);
            tempArray[4] = (coefs2[4] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[5] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            c[6] = (-1 * tempArray[1]);
            tempArray[1] = (coefs2[1] * tempArray[1]);
            tempArray[2] = (-1 * coefs1[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[3] = (coefs2[1] * tempArray[2]);
            tempArray[4] = (-1 * coefs1[1] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            c[0] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (-1 * coefs2[2] * tempArray[1]);
            tempArray[4] = (coefs1[2] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[5] = (-1 * coefs2[2] * tempArray[2]);
            tempArray[6] = (coefs1[2] * tempArray[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            c[1] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (-1 * coefs2[3] * tempArray[1]);
            tempArray[6] = (coefs1[3] * tempArray[3]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[7] = (-1 * coefs2[3] * tempArray[2]);
            tempArray[8] = (coefs1[3] * tempArray[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            c[3] = (tempArray[6] + tempArray[7]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            c[7] = (tempArray[3] + tempArray[1]);
            tempArray[1] = (tempArray[0] * tempArray[2]);
            c[8] = (tempArray[5] + tempArray[1]);
            tempArray[0] = (tempArray[0] * tempArray[4]);
            c[9] = (tempArray[7] + tempArray[0]);
            
            return c;
        }
        
    }
}