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
        private static double Mag_5(double[] coefs)
        {
            var result = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:34 ص
            //Macro: main.cga5d.Mag
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            
            
            result = Math.Abs(coefs[0]);
            
            return result;
        }
        
    }
}
