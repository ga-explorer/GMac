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
        private static double[] GP_451(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:57.0548397+02:00
            //Macro: geometry3d.cga.GP
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 11 sub-expressions, 0 generated temps, 11 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 5 total.
            //Computations: 1.3125 average, 21 total.
            //Memory Reads: 1.875 average, 30 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   mv1.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   mv1.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   mv1.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   mv1.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   mv1.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
            //   mv2.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            c[1] = (coefs1[3] * coefs2[0]);
            c[2] = (coefs1[2] * coefs2[0]);
            c[3] = (-1 * coefs1[1] * coefs2[0]);
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * coefs2[0] * tempVar0001);
            tempVar0001 = (tempVar0000 * tempVar0001);
            tempVar0003 = (coefs1[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (coefs2[0] * tempVar0002);
            tempVar0003 = (-1 * tempVar0000 * tempVar0002);
            c[0] = (tempVar0001 + tempVar0003);
            tempVar0000 = (tempVar0000 * tempVar0002);
            c[4] = (tempVar0001 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:57.0578398+02:00
            
            return c;
        }
        
        
    }
}
