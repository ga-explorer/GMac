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
        private static double[] ApplyVersor_000(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:55:48 ص
            //Macro: main.cga5d.ApplyVersor
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 3 sub-expressions, 0 generated temps, 3 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.75 average, 7 total.
            //Memory Reads: 1.75 average, 7 total.
            //Memory Writes: 4 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs1[0], -2);
            tempVar0001 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[0] = (-1 * tempVar0000 * tempVar0001);
            
            return c;
        }
        
    }
}
