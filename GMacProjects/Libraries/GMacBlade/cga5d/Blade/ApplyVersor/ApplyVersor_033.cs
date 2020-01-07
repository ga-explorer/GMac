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
        private static double[] ApplyVersor_033(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:53 ص
            //Macro: main.cga5d.ApplyVersor
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 82 sub-expressions, 0 generated temps, 82 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 10 total.
            //Computations: 1.41304347826087 average, 130 total.
            //Memory Reads: 1.96739130434783 average, 181 total.
            //Memory Writes: 92 total.
            
            double[] tempArray = new double[10];
            
            tempArray[0] = Math.Pow(coefs1[0], -2);
            tempArray[1] = (-1 * coefs1[0] * coefs2[3]);
            tempArray[1] = (coefs1[0] * tempArray[1]);
            c[3] = (tempArray[0] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * coefs2[4]);
            tempArray[1] = (coefs1[0] * tempArray[1]);
            c[4] = (tempArray[0] * tempArray[1]);
            tempArray[1] = (coefs1[0] * coefs2[5]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[1]);
            c[5] = (tempArray[0] * tempArray[1]);
            tempArray[1] = (coefs1[0] * coefs2[7]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[1]);
            c[7] = (tempArray[0] * tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (-1 * coefs2[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs2[6] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (coefs1[0] * tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[2]);
            tempArray[4] = (coefs2[0] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (coefs1[0] * tempArray[3]);
            tempArray[4] = (tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[2] + tempArray[4]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[2]);
            tempArray[3] = (tempArray[5] + tempArray[2]);
            tempArray[3] = (coefs1[0] * tempArray[3]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[1] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[2] = (coefs1[0] * tempArray[2]);
            tempArray[4] = (tempArray[1] * tempArray[2]);
            tempArray[4] = (tempArray[3] + tempArray[4]);
            c[0] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[4] = (-1 * coefs2[1] * tempArray[1]);
            tempArray[5] = (-1 * coefs2[8] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (coefs1[0] * tempArray[4]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (coefs2[1] * tempArray[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (coefs1[0] * tempArray[5]);
            tempArray[6] = (tempArray[1] * tempArray[5]);
            tempArray[6] = (tempArray[4] + tempArray[6]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[5] = (tempArray[7] + tempArray[4]);
            tempArray[5] = (coefs1[0] * tempArray[5]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[6] = (tempArray[1] * tempArray[6]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[4] = (coefs1[0] * tempArray[4]);
            tempArray[6] = (tempArray[1] * tempArray[4]);
            tempArray[6] = (tempArray[5] + tempArray[6]);
            c[1] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[6] = (-1 * coefs2[2] * tempArray[1]);
            tempArray[7] = (-1 * coefs2[9] * tempArray[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[6] = (-1 * coefs1[0] * tempArray[6]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[8] = (coefs2[2] * tempArray[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (-1 * coefs1[0] * tempArray[7]);
            tempArray[8] = (tempArray[1] * tempArray[7]);
            tempArray[8] = (tempArray[6] + tempArray[8]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[8]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[7] = (tempArray[9] + tempArray[6]);
            tempArray[7] = (-1 * coefs1[0] * tempArray[7]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[8] = (tempArray[1] * tempArray[8]);
            tempArray[6] = (tempArray[6] + tempArray[8]);
            tempArray[6] = (-1 * coefs1[0] * tempArray[6]);
            tempArray[8] = (tempArray[1] * tempArray[6]);
            tempArray[8] = (tempArray[7] + tempArray[8]);
            c[2] = (-1 * tempArray[0] * tempArray[8]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[2]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            c[6] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[2] = (tempArray[5] + tempArray[2]);
            c[8] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[1] = (tempArray[7] + tempArray[1]);
            c[9] = (-1 * tempArray[0] * tempArray[1]);
            
            return c;
        }
        
    }
}
