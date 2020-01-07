using System;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyEVersor_100(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:01.5545288+02:00
            //Macro: geometry3d.cga.ApplyEVersor
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 29 sub-expressions, 0 generated temps, 29 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.33333333333333 average, 40 total.
            //Memory Reads: 1.63333333333333 average, 49 total.
            //Memory Writes: 30 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
            //   v.#no# <=> <Variable> coefs1[0]
            //   v.#e1# <=> <Variable> coefs1[1]
            //   v.#e2# <=> <Variable> coefs1[2]
            //   v.#e3# <=> <Variable> coefs1[3]
            //   v.#ni# <=> <Variable> coefs1[4]
            //   mv.#E0# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            tempVar0001 = (-1 * coefs1[1] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[1] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[3] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[4] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs1[0], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0002 = Math.Pow(coefs1[1], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(coefs1[2], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(coefs1[3], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(coefs1[4], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Pow(tempVar0001, -1);
            c[0] = (tempVar0000 * tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:01.5565289+02:00
            
            return c;
        }
        
        
    }
}
