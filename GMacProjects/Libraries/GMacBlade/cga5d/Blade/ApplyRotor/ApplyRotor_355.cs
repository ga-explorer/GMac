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
        private static double[] ApplyRotor_355(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:57:58 ص
            //Macro: main.cga5d.ApplyRotor
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 83 sub-expressions, 0 generated temps, 83 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.26190476190476 average, 106 total.
            //Memory Reads: 1.97619047619048 average, 166 total.
            //Memory Writes: 84 total.
            
            double[] tempArray = new double[8];
            
            tempArray[0] = (-1 * coefs1[3] * coefs2[0]);
            tempArray[0] = (coefs1[3] * tempArray[0]);
            tempArray[1] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[1] = (coefs1[4] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[5] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[7] * coefs2[0]);
            tempArray[1] = (coefs1[7] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (coefs1[2] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[9] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (coefs2[0] * tempArray[4]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (-1 * coefs2[0] * tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[3]);
            tempArray[7] = (tempArray[1] * tempArray[5]);
            tempArray[4] = (tempArray[1] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[7] + tempArray[3]);
            tempArray[7] = (coefs1[9] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[7]);
            tempArray[2] = (tempArray[4] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[3] = (tempArray[6] + tempArray[7]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs1[1] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[8] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (coefs2[0] * tempArray[4]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (-1 * coefs2[0] * tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[3]);
            tempArray[7] = (tempArray[1] * tempArray[5]);
            tempArray[4] = (tempArray[1] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[7] + tempArray[3]);
            tempArray[7] = (coefs1[8] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[7]);
            tempArray[2] = (-1 * tempArray[4] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[3] = (tempArray[6] + tempArray[7]);
            tempArray[2] = (tempArray[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs1[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[6] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (-1 * coefs2[0] * tempArray[4]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (coefs2[0] * tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[3]);
            tempArray[7] = (tempArray[1] * tempArray[5]);
            tempArray[4] = (tempArray[1] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[7] + tempArray[3]);
            tempArray[7] = (coefs1[6] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[7]);
            tempArray[2] = (tempArray[4] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[2] = (tempArray[6] + tempArray[7]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[2]);
            c[0] = (tempArray[0] + tempArray[1]);
            
            return c;
        }
        
    }
}
