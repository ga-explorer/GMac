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
        private static double[] ApplyEVersor_211(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:17 ص
            //Macro: main.cga5d.ApplyEVersor
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 210 sub-expressions, 0 generated temps, 210 total.
            //Target Temp Variables: 17 total.
            //Output Variables: 5 total.
            //Computations: 1.27906976744186 average, 275 total.
            //Memory Reads: 1.90232558139535 average, 409 total.
            //Memory Writes: 215 total.
            
            double[] tempArray = new double[17];
            
            tempArray[0] = (coefs1[0] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[2] * coefs2[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[4] * coefs2[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[7] * coefs2[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[2] = (coefs1[1] * coefs2[0]);
            tempArray[3] = (coefs1[2] * coefs2[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[5] * coefs2[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[8] * coefs2[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * coefs2[0]);
            tempArray[4] = (coefs1[1] * coefs2[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[0] * coefs2[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (coefs1[3] * coefs2[0]);
            tempArray[5] = (coefs1[4] * coefs2[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[5] * coefs2[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[9] * coefs2[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[6] = (coefs1[3] * coefs2[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[0] * coefs2[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[5] * coefs2[0]);
            tempArray[7] = (coefs1[3] * coefs2[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[1] * coefs2[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (coefs1[6] * coefs2[0]);
            tempArray[8] = (coefs1[7] * coefs2[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[8] * coefs2[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[9] * coefs2[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[7] * coefs2[0]);
            tempArray[9] = (coefs1[6] * coefs2[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[0] * coefs2[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[7] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[8] * coefs2[0]);
            tempArray[10] = (coefs1[6] * coefs2[2]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[1] * coefs2[4]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[9] * coefs2[0]);
            tempArray[11] = (coefs1[6] * coefs2[3]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[3] * coefs2[4]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[9] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[11]);
            tempArray[11] = Math.Pow(coefs1[0], 2);
            tempArray[11] = (-1 * tempArray[11]);
            tempArray[12] = Math.Pow(coefs1[1], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[2], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[3], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[4], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[5], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[6], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[7], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[8], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[9], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[11] = Math.Pow(tempArray[11], -1);
            c[0] = (tempArray[1] * tempArray[11]);
            tempArray[1] = (-1 * coefs1[0] * coefs2[1]);
            tempArray[12] = (-1 * coefs1[1] * coefs2[2]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[3] * coefs2[3]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[6] * coefs2[4]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (coefs1[0] * tempArray[1]);
            tempArray[13] = (-1 * coefs1[2] * tempArray[2]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[1] * tempArray[3]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * coefs1[4] * tempArray[4]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[3] * tempArray[5]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * coefs1[5] * coefs2[1]);
            tempArray[14] = (coefs1[4] * coefs2[2]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[2] * coefs2[3]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[5] * tempArray[13]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[7] * tempArray[7]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (coefs1[6] * tempArray[8]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[8] * coefs2[1]);
            tempArray[15] = (coefs1[7] * coefs2[2]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[2] * coefs2[4]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[8] * tempArray[14]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[9] * coefs2[1]);
            tempArray[16] = (coefs1[7] * coefs2[3]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[4] * coefs2[4]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[9] * tempArray[15]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            c[1] = (tempArray[11] * tempArray[12]);
            tempArray[12] = (coefs1[1] * tempArray[1]);
            tempArray[16] = (coefs1[2] * tempArray[0]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[3] = (-1 * coefs1[0] * tempArray[3]);
            tempArray[3] = (tempArray[12] + tempArray[3]);
            tempArray[12] = (-1 * coefs1[5] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[12]);
            tempArray[12] = (coefs1[3] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[12]);
            tempArray[12] = (coefs1[4] * tempArray[13]);
            tempArray[3] = (tempArray[3] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[8] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[12]);
            tempArray[12] = (coefs1[6] * tempArray[9]);
            tempArray[3] = (tempArray[3] + tempArray[12]);
            tempArray[12] = (coefs1[7] * tempArray[14]);
            tempArray[3] = (tempArray[3] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[9] * coefs2[2]);
            tempArray[16] = (coefs1[8] * coefs2[3]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[5] * coefs2[4]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[9] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[16]);
            c[2] = (tempArray[11] * tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[1]);
            tempArray[16] = (coefs1[4] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[16]);
            tempArray[16] = (coefs1[5] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[16]);
            tempArray[5] = (-1 * coefs1[0] * tempArray[5]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[2] * tempArray[13]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[9] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (coefs1[6] * tempArray[10]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (coefs1[7] * tempArray[15]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (coefs1[8] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            c[3] = (tempArray[11] * tempArray[3]);
            tempArray[1] = (coefs1[6] * tempArray[1]);
            tempArray[0] = (coefs1[7] * tempArray[0]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (coefs1[8] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[9] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[1] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[2] * tempArray[14]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[10]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[4] * tempArray[15]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[5] * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            c[4] = (tempArray[11] * tempArray[0]);
            
            return c;
        }
        
    }
}
