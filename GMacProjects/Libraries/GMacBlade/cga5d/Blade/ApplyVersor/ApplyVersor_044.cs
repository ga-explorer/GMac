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
        private static double[] ApplyVersor_044(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:54 ص
            //Macro: main.cga5d.ApplyVersor
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 32 sub-expressions, 0 generated temps, 32 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 5 total.
            //Computations: 1.40540540540541 average, 52 total.
            //Memory Reads: 1.91891891891892 average, 71 total.
            //Memory Writes: 37 total.
            
            double[] tempArray = new double[6];
            
            tempArray[0] = Math.Pow(coefs1[0], -2);
            tempArray[1] = (coefs1[0] * coefs2[1]);
            tempArray[1] = (coefs1[0] * tempArray[1]);
            c[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[1] = (coefs1[0] * coefs2[2]);
            tempArray[1] = (coefs1[0] * tempArray[1]);
            c[2] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * coefs2[3]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[1]);
            c[3] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (coefs2[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs2[4] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (coefs1[0] * tempArray[2]);
            tempArray[4] = (tempArray[1] * tempArray[2]);
            tempArray[5] = (-1 * coefs2[0] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[3] = (coefs1[0] * tempArray[3]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[4] + tempArray[3]);
            tempArray[5] = (tempArray[1] * tempArray[4]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[2]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[2]);
            tempArray[3] = (tempArray[5] + tempArray[2]);
            tempArray[3] = (coefs1[0] * tempArray[3]);
            tempArray[5] = (tempArray[1] * tempArray[3]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[2] = (coefs1[0] * tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[2]);
            tempArray[4] = (tempArray[5] + tempArray[2]);
            c[0] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            c[4] = (-1 * tempArray[0] * tempArray[1]);
            
            return c;
        }
        
    }
}
