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
        private static double Mag2_4(double[] coefs)
        {
            var result = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:33 ص
            //Macro: main.cga5d.Mag2
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1.04761904761905 average, 22 total.
            //Memory Reads: 1.38095238095238 average, 29 total.
            //Memory Writes: 21 total.
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Abs(tempVar0000);
            
            return result;
        }
        
    }
}
