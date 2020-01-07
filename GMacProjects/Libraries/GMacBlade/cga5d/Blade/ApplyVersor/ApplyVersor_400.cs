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
        private static double[] ApplyVersor_400(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:56:48 ص
            //Macro: main.cga5d.ApplyVersor
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 45 sub-expressions, 0 generated temps, 45 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.26086956521739 average, 58 total.
            //Memory Reads: 1.71739130434783 average, 79 total.
            //Memory Writes: 46 total.
            
            double[] tempArray = new double[8];
            
            tempArray[0] = (-1 * coefs1[3] * coefs2[0]);
            tempArray[0] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[1] = (coefs1[2] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[1] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (coefs1[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[4] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (coefs2[0] * tempArray[2]);
            tempArray[5] = (tempArray[1] * tempArray[4]);
            tempArray[6] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (coefs2[0] * tempArray[3]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[7] = (tempArray[1] * tempArray[5]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[4] = (tempArray[6] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (tempArray[7] + tempArray[4]);
            tempArray[6] = (-1 * tempArray[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[1] = (-1 * tempArray[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(coefs1[1], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[4] = Math.Pow(coefs1[2], 2);
            tempArray[4] = (-1 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = Math.Pow(coefs1[3], 2);
            tempArray[4] = (-1 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[3] = Math.Pow(tempArray[3], 2);
            tempArray[3] = (-1 * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[2] = Math.Pow(tempArray[2], 2);
            tempArray[2] = (-1 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = Math.Pow(tempArray[1], -1);
            c[0] = (tempArray[0] * tempArray[1]);
            
            return c;
        }
        
    }
}
