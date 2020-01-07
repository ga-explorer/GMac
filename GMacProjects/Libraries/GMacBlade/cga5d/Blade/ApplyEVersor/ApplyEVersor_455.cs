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
        private static double[] ApplyEVersor_455(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:18 ص
            //Macro: main.cga5d.ApplyEVersor
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 29 sub-expressions, 0 generated temps, 29 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.2 average, 36 total.
            //Memory Reads: 1.63333333333333 average, 49 total.
            //Memory Writes: 30 total.
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0000 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (coefs1[3] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[1] * coefs2[0]);
            tempVar0001 = (coefs1[1] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs1[0], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0002 = Math.Pow(coefs1[1], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(coefs1[2], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(coefs1[3], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(coefs1[4], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Pow(tempVar0001, -1);
            c[0] = (tempVar0000 * tempVar0001);
            
            return c;
        }
        
    }
}