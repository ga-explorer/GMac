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
        private static double[] RCP_330(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:54:01 ص
            //Macro: main.cga5d.RCP
            //Input Variables: 20 used, 0 not used, 20 total.
            //Temp Variables: 49 sub-expressions, 0 generated temps, 49 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 1 total.
            //Computations: 1.24 average, 62 total.
            //Memory Reads: 1.96 average, 98 total.
            //Memory Writes: 50 total.
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (coefs1[3] * coefs2[3]);
            tempArray[1] = (coefs1[4] * coefs2[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[5] * coefs2[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[7] * coefs2[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[9] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (-1 * coefs2[2] * tempArray[1]);
            tempArray[5] = (-1 * coefs2[9] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[2] = (tempArray[2] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs1[2] * tempArray[1]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[3] = (coefs2[2] * tempArray[1]);
            tempArray[3] = (tempArray[5] + tempArray[3]);
            tempArray[2] = (tempArray[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[8] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (-1 * coefs2[1] * tempArray[1]);
            tempArray[5] = (-1 * coefs2[8] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[2] = (tempArray[2] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs1[1] * tempArray[1]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[3] = (coefs2[1] * tempArray[1]);
            tempArray[3] = (tempArray[5] + tempArray[3]);
            tempArray[2] = (tempArray[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[6] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[4] = (-1 * coefs2[0] * tempArray[1]);
            tempArray[5] = (-1 * coefs2[6] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[2] = (tempArray[2] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs1[0] * tempArray[1]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[1] = (coefs2[0] * tempArray[1]);
            tempArray[1] = (tempArray[5] + tempArray[1]);
            tempArray[1] = (tempArray[2] * tempArray[1]);
            c[0] = (tempArray[0] + tempArray[1]);
            
            return c;
        }
        
    }
}
