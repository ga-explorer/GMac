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
        private static double[] ApplyRotor_433(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:06 ص
            //Macro: main.cga5d.ApplyRotor
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 245 sub-expressions, 0 generated temps, 245 total.
            //Target Temp Variables: 16 total.
            //Output Variables: 10 total.
            //Computations: 1.29803921568627 average, 331 total.
            //Memory Reads: 1.97647058823529 average, 504 total.
            //Memory Writes: 255 total.
            
            double[] tempArray = new double[16];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs1[0] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[4] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (coefs2[1] * tempArray[0]);
            tempArray[4] = (-1 * coefs2[8] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[5] = (coefs2[0] * tempArray[0]);
            tempArray[6] = (-1 * coefs2[6] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[7] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[7]);
            tempArray[7] = (-1 * coefs2[1] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[7]);
            tempArray[7] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs2[4] * tempArray[2]);
            tempArray[8] = (-1 * coefs1[1] * tempArray[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[2] * tempArray[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[8] = (coefs2[4] * tempArray[1]);
            tempArray[9] = (coefs1[1] * tempArray[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[2] * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (tempArray[0] * tempArray[8]);
            tempArray[9] = (tempArray[7] + tempArray[9]);
            tempArray[10] = (tempArray[0] * tempArray[9]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[8]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[8] = (tempArray[10] + tempArray[7]);
            tempArray[10] = (-1 * coefs1[2] * coefs2[4]);
            tempArray[11] = (-1 * coefs1[3] * coefs2[5]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[5] * tempArray[2]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[1] * coefs2[4]);
            tempArray[11] = (coefs1[3] * coefs2[7]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = (tempArray[1] * tempArray[4]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = (tempArray[3] * tempArray[2]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = (coefs1[2] * tempArray[10]);
            tempArray[12] = (coefs1[1] * tempArray[9]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (coefs1[3] * coefs2[4]);
            tempArray[13] = (-1 * coefs1[2] * coefs2[5]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[1] * coefs2[7]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * coefs1[3] * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[8] = (tempArray[1] * tempArray[8]);
            tempArray[8] = (tempArray[11] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[2] * tempArray[7]);
            tempArray[7] = (tempArray[8] + tempArray[7]);
            c[4] = (-1 * tempArray[7]);
            tempArray[7] = (coefs2[2] * tempArray[0]);
            tempArray[8] = (-1 * coefs2[9] * tempArray[0]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[11] = (-1 * coefs2[2] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[11]);
            tempArray[11] = (-1 * coefs2[5] * tempArray[2]);
            tempArray[13] = (-1 * coefs1[1] * tempArray[7]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[13] = (coefs1[3] * tempArray[5]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[11]);
            tempArray[13] = (coefs2[5] * tempArray[1]);
            tempArray[14] = (coefs1[1] * tempArray[8]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[3] * tempArray[6]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (tempArray[0] * tempArray[13]);
            tempArray[14] = (tempArray[11] + tempArray[14]);
            tempArray[15] = (tempArray[0] * tempArray[14]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[13]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[11]);
            tempArray[13] = (tempArray[15] + tempArray[11]);
            tempArray[14] = (-1 * tempArray[0] * tempArray[14]);
            tempArray[11] = (tempArray[11] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[1] * coefs2[5]);
            tempArray[15] = (-1 * coefs1[2] * coefs2[7]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (tempArray[1] * tempArray[8]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (tempArray[7] * tempArray[2]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[10] = (-1 * coefs1[3] * tempArray[10]);
            tempArray[15] = (coefs1[1] * tempArray[14]);
            tempArray[10] = (tempArray[10] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[2] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[15]);
            tempArray[13] = (tempArray[1] * tempArray[13]);
            tempArray[10] = (tempArray[10] + tempArray[13]);
            tempArray[11] = (-1 * tempArray[2] * tempArray[11]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            c[5] = (-1 * tempArray[10]);
            tempArray[10] = (coefs2[7] * tempArray[2]);
            tempArray[11] = (coefs1[2] * tempArray[7]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[3] * tempArray[3]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[11] = (-1 * coefs2[7] * tempArray[1]);
            tempArray[13] = (-1 * coefs1[2] * tempArray[8]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[13] = (coefs1[3] * tempArray[4]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[13] = (tempArray[0] * tempArray[11]);
            tempArray[13] = (tempArray[10] + tempArray[13]);
            tempArray[15] = (tempArray[0] * tempArray[13]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[11]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[11] = (tempArray[15] + tempArray[10]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[13]);
            tempArray[10] = (tempArray[10] + tempArray[13]);
            tempArray[9] = (coefs1[3] * tempArray[9]);
            tempArray[13] = (coefs1[2] * tempArray[14]);
            tempArray[9] = (tempArray[9] + tempArray[13]);
            tempArray[12] = (coefs1[1] * tempArray[12]);
            tempArray[9] = (tempArray[9] + tempArray[12]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[11]);
            tempArray[1] = (tempArray[9] + tempArray[1]);
            tempArray[2] = (tempArray[2] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[7] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * coefs2[3] * tempArray[1]);
            tempArray[2] = (coefs1[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * coefs2[3] * tempArray[2]);
            tempArray[9] = (-1 * coefs1[3] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[2] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[1] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[9] = (tempArray[1] + tempArray[9]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[10] + tempArray[1]);
            tempArray[10] = (-1 * coefs1[1] * coefs2[3]);
            tempArray[6] = (tempArray[2] * tempArray[6]);
            tempArray[6] = (tempArray[10] + tempArray[6]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[5] = (tempArray[6] + tempArray[5]);
            tempArray[6] = (tempArray[0] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (coefs1[2] * coefs2[3]);
            tempArray[4] = (-1 * tempArray[2] * tempArray[4]);
            tempArray[4] = (tempArray[6] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[3] = (tempArray[4] + tempArray[3]);
            tempArray[4] = (coefs1[3] * coefs2[3]);
            tempArray[6] = (-1 * tempArray[2] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (tempArray[1] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[5]);
            tempArray[3] = (coefs1[2] * tempArray[3]);
            tempArray[3] = (tempArray[5] + tempArray[3]);
            tempArray[4] = (-1 * coefs1[3] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[1] = (tempArray[1] * tempArray[1]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[2] = (tempArray[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[3] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * coefs1[1] * tempArray[2]);
            tempArray[2] = (coefs1[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[3] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[2] = (coefs1[1] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[10]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[2]);
            c[0] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[2]);
            tempArray[4] = (-1 * coefs1[1] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (coefs1[3] * tempArray[11]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[1] * tempArray[9]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[4] = (coefs1[2] * tempArray[1]);
            tempArray[5] = (coefs1[1] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[3] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[2] * tempArray[9]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[4]);
            c[1] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (coefs1[3] * tempArray[2]);
            tempArray[6] = (-1 * coefs1[1] * tempArray[13]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs1[2] * tempArray[11]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[1] * tempArray[14]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[2] * tempArray[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[6] = (-1 * coefs1[3] * tempArray[1]);
            tempArray[7] = (coefs1[1] * tempArray[11]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[2] * tempArray[10]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[2] * tempArray[14]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[0] * tempArray[6]);
            c[2] = (tempArray[5] + tempArray[7]);
            tempArray[2] = (-1 * tempArray[0] * tempArray[2]);
            c[6] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[4]);
            c[8] = (tempArray[3] + tempArray[1]);
            tempArray[0] = (-1 * tempArray[0] * tempArray[6]);
            c[9] = (tempArray[5] + tempArray[0]);
            
            return c;
        }
        
    }
}
