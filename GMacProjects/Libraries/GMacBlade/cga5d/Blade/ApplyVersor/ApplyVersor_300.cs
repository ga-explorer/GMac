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
        private static double[] ApplyVersor_300(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:56:31 ص
            //Macro: main.cga5d.ApplyVersor
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 108 sub-expressions, 0 generated temps, 108 total.
            //Target Temp Variables: 18 total.
            //Output Variables: 1 total.
            //Computations: 1.20183486238532 average, 131 total.
            //Memory Reads: 1.89908256880734 average, 207 total.
            //Memory Writes: 109 total.
            
            double[] tempArray = new double[18];
            
            tempArray[0] = (coefs1[7] * coefs2[0]);
            tempArray[0] = (coefs1[7] * tempArray[0]);
            tempArray[1] = (coefs1[5] * coefs2[0]);
            tempArray[1] = (coefs1[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[1] = (coefs1[4] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[3] * coefs2[0]);
            tempArray[1] = (coefs1[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[9] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[5] = (-1 * coefs2[0] * tempArray[4]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[6] = (coefs1[2] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[7] = (-1 * coefs2[0] * tempArray[3]);
            tempArray[8] = (tempArray[1] * tempArray[7]);
            tempArray[8] = (tempArray[5] + tempArray[8]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[8]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[7] = (tempArray[9] + tempArray[5]);
            tempArray[9] = (coefs1[9] * tempArray[1]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[7] = (tempArray[7] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[7] = (tempArray[1] * tempArray[8]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[5] = (tempArray[5] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[7] = (-1 * coefs1[8] * tempArray[1]);
            tempArray[8] = (tempArray[5] + tempArray[7]);
            tempArray[9] = (coefs2[0] * tempArray[8]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[9]);
            tempArray[10] = (coefs1[1] * tempArray[1]);
            tempArray[7] = (tempArray[7] + tempArray[10]);
            tempArray[11] = (coefs2[0] * tempArray[7]);
            tempArray[12] = (tempArray[1] * tempArray[11]);
            tempArray[12] = (tempArray[9] + tempArray[12]);
            tempArray[13] = (-1 * tempArray[1] * tempArray[12]);
            tempArray[11] = (-1 * tempArray[1] * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[9]);
            tempArray[11] = (tempArray[13] + tempArray[9]);
            tempArray[13] = (coefs1[8] * tempArray[1]);
            tempArray[10] = (tempArray[10] + tempArray[13]);
            tempArray[11] = (tempArray[11] * tempArray[10]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[11] = (tempArray[1] * tempArray[12]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[5] = (tempArray[5] + tempArray[13]);
            tempArray[9] = (tempArray[9] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[11] = (-1 * coefs1[6] * tempArray[1]);
            tempArray[12] = (tempArray[9] + tempArray[11]);
            tempArray[13] = (coefs2[0] * tempArray[12]);
            tempArray[13] = (-1 * tempArray[1] * tempArray[13]);
            tempArray[14] = (coefs1[0] * tempArray[1]);
            tempArray[11] = (tempArray[11] + tempArray[14]);
            tempArray[15] = (coefs2[0] * tempArray[11]);
            tempArray[16] = (tempArray[1] * tempArray[15]);
            tempArray[16] = (tempArray[13] + tempArray[16]);
            tempArray[17] = (-1 * tempArray[1] * tempArray[16]);
            tempArray[15] = (-1 * tempArray[1] * tempArray[15]);
            tempArray[13] = (tempArray[13] + tempArray[15]);
            tempArray[13] = (-1 * tempArray[1] * tempArray[13]);
            tempArray[15] = (tempArray[17] + tempArray[13]);
            tempArray[17] = (coefs1[6] * tempArray[1]);
            tempArray[14] = (tempArray[14] + tempArray[17]);
            tempArray[15] = (tempArray[15] * tempArray[14]);
            tempArray[0] = (tempArray[0] + tempArray[15]);
            tempArray[1] = (tempArray[1] * tempArray[16]);
            tempArray[1] = (tempArray[13] + tempArray[1]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[1] = (tempArray[1] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(coefs1[3], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[13] = Math.Pow(coefs1[4], 2);
            tempArray[13] = (-1 * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            tempArray[13] = Math.Pow(coefs1[5], 2);
            tempArray[13] = (-1 * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            tempArray[9] = (tempArray[11] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (tempArray[12] * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = Math.Pow(coefs1[7], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[5] = (tempArray[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (tempArray[8] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = Math.Pow(tempArray[1], -1);
            c[0] = (tempArray[0] * tempArray[1]);
            
            return c;
        }
        
    }
}
