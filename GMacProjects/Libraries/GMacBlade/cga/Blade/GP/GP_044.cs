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
        private static double[] GP_044(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:45.4361751+02:00
            //Macro: geometry3d.cga.GP
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 11 sub-expressions, 0 generated temps, 11 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 5 total.
            //Computations: 1.3125 average, 21 total.
            //Memory Reads: 1.875 average, 30 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   mv1.#E0# <=> <Variable> coefs1[0]
            //   mv2.#no^e1^e2^e3# <=> <Variable> coefs2[0]
            //   mv2.#no^e1^e2^ni# <=> <Variable> coefs2[1]
            //   mv2.#no^e1^e3^ni# <=> <Variable> coefs2[2]
            //   mv2.#no^e2^e3^ni# <=> <Variable> coefs2[3]
            //   mv2.#e1^e2^e3^ni# <=> <Variable> coefs2[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            
            c[1] = (coefs1[0] * coefs2[1]);
            c[2] = (coefs1[0] * coefs2[2]);
            c[3] = (-1 * coefs1[0] * coefs2[3]);
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (coefs2[0] * tempVar0000);
            tempVar0002 = (-1 * coefs2[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (coefs1[0] * tempVar0001);
            tempVar0003 = (tempVar0000 * tempVar0001);
            tempVar0004 = (-1 * coefs2[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0002 = (coefs1[0] * tempVar0002);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            c[0] = (tempVar0003 + tempVar0002);
            tempVar0000 = (-1 * tempVar0000 * tempVar0001);
            c[4] = (tempVar0002 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:45.4381752+02:00
            
            return c;
        }
        
        
    }
}
