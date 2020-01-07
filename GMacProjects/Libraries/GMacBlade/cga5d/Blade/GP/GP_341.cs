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
        private static double[] GP_341(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:54:44 ص
            //Macro: main.cga5d.GP
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 56 sub-expressions, 0 generated temps, 56 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 5 total.
            //Computations: 1.27868852459016 average, 78 total.
            //Memory Reads: 1.9672131147541 average, 120 total.
            //Memory Writes: 61 total.
            
            double[] tempArray = new double[7];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs1[2] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[9] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (coefs2[0] * tempArray[0]);
            tempArray[4] = (-1 * coefs2[4] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[5] = (-1 * coefs1[2] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[5] * coefs2[1]);
            tempArray[6] = (coefs1[7] * coefs2[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[2] = (-1 * tempArray[3] * tempArray[2]);
            tempArray[2] = (tempArray[5] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[4]);
            c[1] = (tempArray[2] + tempArray[1]);
            tempArray[1] = (coefs1[1] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[8] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (coefs1[4] * coefs2[1]);
            tempArray[6] = (-1 * coefs1[7] * coefs2[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[2] = (-1 * tempArray[3] * tempArray[2]);
            tempArray[2] = (tempArray[5] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[4]);
            c[2] = (tempArray[2] + tempArray[1]);
            tempArray[1] = (coefs1[0] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[6] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[5] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[5]);
            tempArray[5] = (coefs1[4] * coefs2[2]);
            tempArray[6] = (coefs1[5] * coefs2[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[2] = (tempArray[5] + tempArray[2]);
            tempArray[1] = (tempArray[1] * tempArray[4]);
            c[3] = (tempArray[2] + tempArray[1]);
            tempArray[1] = (-1 * coefs2[3] * tempArray[1]);
            tempArray[2] = (-1 * coefs2[2] * tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs2[1] * tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (coefs2[3] * tempArray[2]);
            tempArray[3] = (coefs2[2] * tempArray[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs2[1] * tempArray[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            c[0] = (tempArray[1] + tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            c[4] = (tempArray[1] + tempArray[0]);
            
            return c;
        }
        
    }
}
