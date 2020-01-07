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
        private static double[] GP_222(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:54:30 ص
            //Macro: main.cga5d.GP
            //Input Variables: 20 used, 0 not used, 20 total.
            //Temp Variables: 147 sub-expressions, 0 generated temps, 147 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 10 total.
            //Computations: 1.2484076433121 average, 196 total.
            //Memory Reads: 1.98089171974522 average, 311 total.
            //Memory Writes: 157 total.
            
            double[] tempArray = new double[15];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (-1 * coefs1[1] * tempArray[0]);
            tempArray[2] = (coefs1[8] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (coefs1[0] * tempArray[0]);
            tempArray[4] = (coefs1[7] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[5] = (-1 * coefs2[1] * tempArray[0]);
            tempArray[6] = (coefs2[8] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[7] = (coefs2[0] * tempArray[0]);
            tempArray[8] = (coefs2[7] * tempArray[0]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[9] = (coefs1[1] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[9]);
            tempArray[9] = (coefs2[1] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[5] * coefs2[4]);
            tempArray[10] = (coefs1[4] * coefs2[5]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (tempArray[5] * tempArray[4]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (tempArray[3] * tempArray[6]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[1] * tempArray[8]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[7] * tempArray[2]);
            c[2] = (tempArray[9] + tempArray[10]);
            tempArray[9] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[10] = (coefs1[9] * tempArray[0]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[11] = (-1 * coefs2[3] * tempArray[0]);
            tempArray[12] = (coefs2[9] * tempArray[0]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[13] = (coefs1[3] * tempArray[0]);
            tempArray[10] = (tempArray[10] + tempArray[13]);
            tempArray[13] = (coefs2[3] * tempArray[0]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[5] * coefs2[2]);
            tempArray[14] = (-1 * coefs1[2] * coefs2[5]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (tempArray[11] * tempArray[4]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (tempArray[3] * tempArray[12]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[9] * tempArray[8]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[7] * tempArray[10]);
            c[4] = (tempArray[13] + tempArray[14]);
            tempArray[13] = (coefs1[4] * coefs2[2]);
            tempArray[14] = (-1 * coefs1[2] * coefs2[4]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[1] * tempArray[11]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[2] * tempArray[12]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (tempArray[9] * tempArray[5]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (tempArray[10] * tempArray[6]);
            c[5] = (tempArray[13] + tempArray[14]);
            tempArray[10] = (-1 * tempArray[11] * tempArray[10]);
            tempArray[9] = (tempArray[9] * tempArray[12]);
            tempArray[9] = (tempArray[10] + tempArray[9]);
            tempArray[2] = (-1 * tempArray[5] * tempArray[2]);
            tempArray[2] = (tempArray[9] + tempArray[2]);
            tempArray[1] = (tempArray[1] * tempArray[6]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[2] = (-1 * tempArray[3] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[6] = (-1 * tempArray[1]);
            tempArray[1] = (coefs2[4] * tempArray[9]);
            tempArray[2] = (coefs2[2] * tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs2[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[4] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[3] = (coefs2[4] * tempArray[10]);
            tempArray[4] = (coefs2[2] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs2[6] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[4] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[2] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (coefs1[6] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            c[0] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (-1 * coefs2[5] * tempArray[9]);
            tempArray[4] = (-1 * coefs2[6] * tempArray[2]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (coefs2[2] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (coefs1[5] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (coefs1[6] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[2] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[5] = (-1 * coefs2[5] * tempArray[10]);
            tempArray[6] = (coefs2[6] * tempArray[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs2[2] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs1[5] * tempArray[12]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[6] * tempArray[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[2] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            c[1] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (-1 * coefs2[6] * tempArray[10]);
            tempArray[6] = (coefs2[5] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (coefs2[4] * tempArray[4]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (coefs1[6] * tempArray[12]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[5] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[4] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[7] = (coefs2[6] * tempArray[9]);
            tempArray[8] = (coefs2[5] * tempArray[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs2[4] * tempArray[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[6] * tempArray[11]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[5] * tempArray[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[4] * tempArray[7]);
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
