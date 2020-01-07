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
        private static double[] ApplyEVersor_033(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:01.5245271+02:00
            //Macro: geometry3d.cga.ApplyEVersor
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 21 sub-expressions, 0 generated temps, 21 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 10 total.
            //Computations: 1.96774193548387 average, 61 total.
            //Memory Reads: 1.96774193548387 average, 61 total.
            //Memory Writes: 31 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2# <=> <Variable> c[0]
            //   result.#no^e1^e3# <=> <Variable> c[1]
            //   result.#no^e2^e3# <=> <Variable> c[2]
            //   result.#e1^e2^e3# <=> <Variable> c[3]
            //   result.#no^e1^ni# <=> <Variable> c[4]
            //   result.#no^e2^ni# <=> <Variable> c[5]
            //   result.#e1^e2^ni# <=> <Variable> c[6]
            //   result.#no^e3^ni# <=> <Variable> c[7]
            //   result.#e1^e3^ni# <=> <Variable> c[8]
            //   result.#e2^e3^ni# <=> <Variable> c[9]
            //   v.#E0# <=> <Variable> coefs1[0]
            //   mv.#no^e1^e2# <=> <Variable> coefs2[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs2[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs2[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs2[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs2[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs2[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs2[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs2[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs2[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs2[9]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs1[0], -2);
            tempVar0001 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[0] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[1] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[2]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[2] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[3] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[4] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[5]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[5] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[6]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[6] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[7]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[7] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[8]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[8] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[9]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[9] = (-1 * tempVar0000 * tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:01.5285273+02:00
            
            return c;
        }
        
        
    }
}
