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
        private static double[] SP_440(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:44.6701313+02:00
            //Macro: geometry3d.cga.SP
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 19 sub-expressions, 0 generated temps, 19 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 1 total.
            //Computations: 1.45 average, 29 total.
            //Memory Reads: 1.9 average, 38 total.
            //Memory Writes: 20 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> c[0]
            //   mv1.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   mv1.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   mv1.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   mv1.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   mv1.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
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
            double tempVar0005;
            
            tempVar0000 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs1[0] * tempVar0001);
            tempVar0003 = (-1 * coefs1[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0004 = (coefs2[0] * tempVar0001);
            tempVar0005 = (-1 * coefs2[4] * tempVar0001);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0002 = (-1 * tempVar0002 * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (-1 * coefs1[0] * tempVar0001);
            tempVar0002 = (tempVar0003 + tempVar0002);
            tempVar0001 = (-1 * coefs2[0] * tempVar0001);
            tempVar0001 = (tempVar0005 + tempVar0001);
            tempVar0001 = (-1 * tempVar0002 * tempVar0001);
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:44.6721314+02:00
            
            return c;
        }
        
        
    }
}
