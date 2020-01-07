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
        private static double[] SelfEGP_000(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:47 ص
            //Macro: main.cga5d.SelfEGP
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs1[0], 2);
            c[0] = (-1 * tempVar0000);
            
            return c;
        }
        
    }
}
