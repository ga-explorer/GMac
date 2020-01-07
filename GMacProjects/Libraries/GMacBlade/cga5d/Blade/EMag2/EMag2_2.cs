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
        private static double EMag2_2(double[] coefs)
        {
            var result = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:28 ص
            //Macro: main.cga5d.EMag2
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 28 sub-expressions, 0 generated temps, 28 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 29 total.
            //Memory Reads: 1.31034482758621 average, 38 total.
            //Memory Writes: 29 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
    }
}
