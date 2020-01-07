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
        private static double[] ApplyRotor_522(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:58:14 ص
            //Macro: main.cga5d.ApplyRotor
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 72 sub-expressions, 0 generated temps, 72 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 10 total.
            //Computations: 1.32926829268293 average, 109 total.
            //Memory Reads: 1.96341463414634 average, 161 total.
            //Memory Writes: 82 total.
            
            double[] tempArray = new double[9];
            
            tempArray[0] = (coefs1[0] * coefs2[2]);
            c[2] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[0] * coefs2[4]);
            c[4] = (coefs1[0] * tempArray[0]);
            tempArray[0] = (-1 * coefs1[0] * coefs2[5]);
            c[5] = (coefs1[0] * tempArray[0]);
            tempArray[0] = (coefs1[0] * coefs2[6]);
            tempArray[0] = (coefs1[0] * tempArray[0]);
            c[6] = (-1 * tempArray[0]);
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs2[0] * tempArray[0]);
            tempArray[2] = (coefs2[7] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (coefs1[0] * tempArray[1]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (-1 * coefs1[0] * tempArray[2]);
            tempArray[3] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[1] + tempArray[3]);
            tempArray[4] = (tempArray[0] * tempArray[3]);
            tempArray[2] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[4] + tempArray[1]);
            tempArray[2] = (coefs1[0] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            c[0] = (tempArray[4] + tempArray[1]);
            tempArray[3] = (coefs2[1] * tempArray[0]);
            tempArray[4] = (coefs2[8] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (coefs1[0] * tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[5] = (-1 * coefs2[1] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (-1 * coefs1[0] * tempArray[4]);
            tempArray[5] = (tempArray[0] * tempArray[4]);
            tempArray[5] = (tempArray[3] + tempArray[5]);
            tempArray[6] = (tempArray[0] * tempArray[5]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[4] = (tempArray[6] + tempArray[3]);
            tempArray[4] = (coefs1[0] * tempArray[4]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[3] = (-1 * coefs1[0] * tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            c[1] = (tempArray[6] + tempArray[3]);
            tempArray[5] = (coefs2[3] * tempArray[0]);
            tempArray[6] = (coefs2[9] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (-1 * coefs1[0] * tempArray[5]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[7] = (-1 * coefs2[3] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[6] = (coefs1[0] * tempArray[6]);
            tempArray[7] = (tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[5] + tempArray[7]);
            tempArray[8] = (tempArray[0] * tempArray[7]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[6] = (tempArray[8] + tempArray[5]);
            tempArray[6] = (-1 * coefs1[0] * tempArray[6]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[5] = (coefs1[0] * tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            c[3] = (tempArray[8] + tempArray[5]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            c[7] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[4]);
            c[8] = (tempArray[3] + tempArray[1]);
            tempArray[0] = (tempArray[0] * tempArray[6]);
            c[9] = (tempArray[5] + tempArray[0]);
            
            return c;
        }
        
    }
}
