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
        private static double[] ApplyVersor_111(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:56:01 ص
            //Macro: main.cga5d.ApplyVersor
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 145 sub-expressions, 0 generated temps, 145 total.
            //Target Temp Variables: 14 total.
            //Output Variables: 5 total.
            //Computations: 1.28 average, 192 total.
            //Memory Reads: 1.91333333333333 average, 287 total.
            //Memory Writes: 150 total.
            
            double[] tempArray = new double[14];
            
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
            tempArray[5] = (coefs2[1] * tempArray[1]);
            tempArray[6] = (-1 * coefs1[1] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[7] = (coefs2[1] * tempArray[2]);
            tempArray[8] = (-1 * coefs1[1] * tempArray[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[5] = (tempArray[7] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[7] = (tempArray[8] + tempArray[5]);
            tempArray[8] = (-1 * coefs1[1] * coefs2[1]);
            tempArray[9] = (-1 * coefs1[2] * coefs2[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[3] * coefs2[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[2] * tempArray[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = Math.Pow(coefs1[1], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[9] = Math.Pow(coefs1[2], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = Math.Pow(coefs1[3], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = Math.Pow(tempArray[2], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = Math.Pow(tempArray[1], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[6] = Math.Pow(tempArray[6], -1);
            tempArray[9] = (coefs1[1] * tempArray[8]);
            tempArray[10] = (coefs1[3] * coefs2[1]);
            tempArray[11] = (-1 * coefs1[1] * coefs2[3]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[3] * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = (coefs1[2] * coefs2[1]);
            tempArray[12] = (-1 * coefs1[1] * coefs2[2]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[2] * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[12]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[7] = (tempArray[9] + tempArray[7]);
            tempArray[5] = (-1 * tempArray[2] * tempArray[5]);
            tempArray[5] = (tempArray[7] + tempArray[5]);
            c[1] = (tempArray[6] * tempArray[5]);
            tempArray[5] = (-1 * coefs2[2] * tempArray[1]);
            tempArray[7] = (coefs1[2] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[9] = (-1 * coefs2[2] * tempArray[2]);
            tempArray[12] = (coefs1[2] * tempArray[4]);
            tempArray[9] = (tempArray[9] + tempArray[12]);
            tempArray[9] = (tempArray[0] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[9]);
            tempArray[12] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[5] = (tempArray[9] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[9] = (tempArray[12] + tempArray[5]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[2] * tempArray[8]);
            tempArray[12] = (-1 * coefs1[3] * coefs2[2]);
            tempArray[13] = (coefs1[2] * coefs2[3]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[3] * tempArray[12]);
            tempArray[7] = (tempArray[7] + tempArray[13]);
            tempArray[11] = (-1 * coefs1[1] * tempArray[11]);
            tempArray[7] = (tempArray[7] + tempArray[11]);
            tempArray[9] = (tempArray[1] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[9]);
            tempArray[5] = (tempArray[2] * tempArray[5]);
            tempArray[5] = (tempArray[7] + tempArray[5]);
            c[2] = (tempArray[6] * tempArray[5]);
            tempArray[5] = (-1 * coefs2[3] * tempArray[1]);
            tempArray[7] = (coefs1[3] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[9] = (-1 * coefs2[3] * tempArray[2]);
            tempArray[11] = (coefs1[3] * tempArray[4]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[9] = (tempArray[0] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[9]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[5] = (tempArray[9] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[9] = (tempArray[11] + tempArray[5]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[3] * tempArray[8]);
            tempArray[8] = (-1 * coefs1[2] * tempArray[12]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[1] * tempArray[10]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[1] = (tempArray[1] * tempArray[9]);
            tempArray[1] = (tempArray[7] + tempArray[1]);
            tempArray[2] = (tempArray[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[3] = (tempArray[6] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[9]);
            tempArray[2] = (-1 * coefs1[2] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[1] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[3] * tempArray[5]);
            tempArray[4] = (-1 * coefs1[2] * tempArray[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[1] * tempArray[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[2] = (tempArray[1] * tempArray[2]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[1] + tempArray[3]);
            c[0] = (tempArray[3] * tempArray[6]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            c[4] = (tempArray[6] * tempArray[0]);
            
            return c;
        }
        
    }
}
