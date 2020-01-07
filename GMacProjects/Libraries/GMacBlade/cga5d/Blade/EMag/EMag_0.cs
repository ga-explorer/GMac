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
        private static double EMag_0(double[] coefs)
        {
            var result = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:25 ص
            //Macro: main.cga5d.EMag
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 3 total.
            //Memory Reads: 1 average, 3 total.
            //Memory Writes: 3 total.
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            return result;
        }
        
    }
}
