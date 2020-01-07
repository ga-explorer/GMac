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
        private static double Mag_2(double[] coefs)
        {
            var result = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:28 ص
            //Macro: main.cga5d.Mag
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 49 sub-expressions, 0 generated temps, 49 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.1 average, 55 total.
            //Memory Reads: 1.76 average, 88 total.
            //Memory Writes: 50 total.
            
            double[] tempArray = new double[8];
            
            tempArray[0] = Math.Pow(coefs[2], 2);
            tempArray[0] = (-1 * tempArray[0]);
            tempArray[1] = Math.Pow(coefs[4], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(coefs[5], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(coefs[6], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (coefs[0] * tempArray[1]);
            tempArray[3] = (-1 * coefs[7] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[5] = (-1 * coefs[0] * tempArray[1]);
            tempArray[6] = (coefs[7] * tempArray[1]);
            tempArray[7] = (tempArray[5] + tempArray[6]);
            tempArray[4] = (tempArray[4] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[6]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs[1] * tempArray[1]);
            tempArray[3] = (-1 * coefs[8] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[5] = (-1 * coefs[1] * tempArray[1]);
            tempArray[6] = (coefs[8] * tempArray[1]);
            tempArray[7] = (tempArray[5] + tempArray[6]);
            tempArray[4] = (tempArray[4] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[6]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (coefs[3] * tempArray[1]);
            tempArray[3] = (-1 * coefs[9] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[5] = (-1 * coefs[3] * tempArray[1]);
            tempArray[1] = (coefs[9] * tempArray[1]);
            tempArray[6] = (tempArray[5] + tempArray[1]);
            tempArray[4] = (tempArray[4] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[1] = (tempArray[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = Math.Abs(tempArray[0]);
            result = Math.Pow(tempArray[0], 0.5);
            
            return result;
        }
        
    }
}
