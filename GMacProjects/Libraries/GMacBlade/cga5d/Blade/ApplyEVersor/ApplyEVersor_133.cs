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
        private static double[] ApplyEVersor_133(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:17 ص
            //Macro: main.cga5d.ApplyEVersor
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 190 sub-expressions, 0 generated temps, 190 total.
            //Target Temp Variables: 14 total.
            //Output Variables: 10 total.
            //Computations: 1.3 average, 260 total.
            //Memory Reads: 1.945 average, 389 total.
            //Memory Writes: 200 total.
            
            double[] tempArray = new double[14];
            
            tempArray[0] = (-1 * coefs1[2] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[3] * coefs2[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[4] * coefs2[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[2] * tempArray[0]);
            tempArray[2] = (coefs1[1] * coefs2[0]);
            tempArray[3] = (-1 * coefs1[3] * coefs2[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[4] * coefs2[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs1[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[0] * coefs2[0]);
            tempArray[4] = (-1 * coefs1[3] * coefs2[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[4] * coefs2[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (coefs1[3] * coefs2[0]);
            tempArray[5] = (-1 * coefs1[2] * coefs2[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[1] * coefs2[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[0] * coefs2[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (coefs1[4] * coefs2[0]);
            tempArray[6] = (-1 * coefs1[2] * coefs2[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs1[1] * coefs2[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[0] * coefs2[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = Math.Pow(coefs1[0], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[7] = Math.Pow(coefs1[1], 2);
            tempArray[7] = (-1 * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = Math.Pow(coefs1[2], 2);
            tempArray[7] = (-1 * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = Math.Pow(coefs1[3], 2);
            tempArray[7] = (-1 * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = Math.Pow(coefs1[4], 2);
            tempArray[7] = (-1 * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[6] = Math.Pow(tempArray[6], -1);
            c[0] = (tempArray[1] * tempArray[6]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[7] = (coefs1[1] * coefs2[1]);
            tempArray[8] = (coefs1[2] * coefs2[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[4] * coefs2[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[1] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[0] * coefs2[1]);
            tempArray[9] = (coefs1[2] * coefs2[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[4] * coefs2[8]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[0] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (coefs1[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (coefs1[4] * coefs2[1]);
            tempArray[10] = (-1 * coefs1[3] * coefs2[4]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (coefs1[1] * coefs2[7]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[0] * coefs2[8]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[4] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            c[1] = (tempArray[6] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[2]);
            tempArray[10] = (coefs1[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[0] * coefs2[2]);
            tempArray[11] = (-1 * coefs1[1] * coefs2[3]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[4] * coefs2[9]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[0] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[11]);
            tempArray[11] = (coefs1[4] * coefs2[2]);
            tempArray[12] = (-1 * coefs1[3] * coefs2[5]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (coefs1[2] * coefs2[7]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[0] * coefs2[9]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[4] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            c[2] = (tempArray[6] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[3]);
            tempArray[12] = (coefs1[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[1] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[4] = (coefs1[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (coefs1[4] * coefs2[3]);
            tempArray[12] = (-1 * coefs1[3] * coefs2[6]);
            tempArray[4] = (tempArray[4] + tempArray[12]);
            tempArray[12] = (coefs1[2] * coefs2[8]);
            tempArray[4] = (tempArray[4] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[1] * coefs2[9]);
            tempArray[4] = (tempArray[4] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[4] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            c[3] = (tempArray[6] * tempArray[1]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[0]);
            tempArray[1] = (coefs1[1] * coefs2[4]);
            tempArray[12] = (coefs1[2] * coefs2[5]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (coefs1[3] * coefs2[7]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (coefs1[1] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[0] * coefs2[4]);
            tempArray[13] = (coefs1[2] * coefs2[6]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[3] * coefs2[8]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * coefs1[0] * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (coefs1[2] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (coefs1[3] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            c[4] = (tempArray[6] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[2]);
            tempArray[2] = (coefs1[2] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[0] * coefs2[5]);
            tempArray[13] = (-1 * coefs1[1] * coefs2[6]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[13] = (coefs1[3] * coefs2[9]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[13] = (-1 * coefs1[0] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (-1 * coefs1[1] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (coefs1[3] * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            c[5] = (tempArray[6] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[3]);
            tempArray[3] = (coefs1[2] * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (coefs1[0] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            c[6] = (tempArray[6] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[7]);
            tempArray[1] = (coefs1[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * coefs2[7]);
            tempArray[3] = (-1 * coefs1[1] * coefs2[8]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * coefs2[9]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[1] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            c[7] = (tempArray[6] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[8]);
            tempArray[3] = (coefs1[3] * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (coefs1[0] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            c[8] = (tempArray[6] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[4] * tempArray[10]);
            tempArray[2] = (coefs1[3] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[0] * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[1] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            c[9] = (tempArray[6] * tempArray[0]);
            
            return c;
        }
        
    }
}
