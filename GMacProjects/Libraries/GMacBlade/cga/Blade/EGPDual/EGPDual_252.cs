namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] EGPDual_252(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:03.6172150+02:00
            //Macro: geometry3d.cga.EGPDual
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 10 total.
            //Computations: 1.25 average, 20 total.
            //Memory Reads: 1.625 average, 26 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1# <=> <Variable> c[0]
            //   result.#no^e2# <=> <Variable> c[1]
            //   result.#e1^e2# <=> <Variable> c[2]
            //   result.#no^e3# <=> <Variable> c[3]
            //   result.#e1^e3# <=> <Variable> c[4]
            //   result.#e2^e3# <=> <Variable> c[5]
            //   result.#no^ni# <=> <Variable> c[6]
            //   result.#e1^ni# <=> <Variable> c[7]
            //   result.#e2^ni# <=> <Variable> c[8]
            //   result.#e3^ni# <=> <Variable> c[9]
            //   mv1.#no^e1# <=> <Variable> coefs1[0]
            //   mv1.#no^e2# <=> <Variable> coefs1[1]
            //   mv1.#e1^e2# <=> <Variable> coefs1[2]
            //   mv1.#no^e3# <=> <Variable> coefs1[3]
            //   mv1.#e1^e3# <=> <Variable> coefs1[4]
            //   mv1.#e2^e3# <=> <Variable> coefs1[5]
            //   mv1.#no^ni# <=> <Variable> coefs1[6]
            //   mv1.#e1^ni# <=> <Variable> coefs1[7]
            //   mv1.#e2^ni# <=> <Variable> coefs1[8]
            //   mv1.#e3^ni# <=> <Variable> coefs1[9]
            //   mv2.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            
            c[1] = (-1 * coefs1[1] * coefs2[0]);
            c[4] = (-1 * coefs1[4] * coefs2[0]);
            c[6] = (-1 * coefs1[6] * coefs2[0]);
            c[8] = (-1 * coefs1[8] * coefs2[0]);
            tempVar0000 = (coefs1[0] * coefs2[0]);
            c[0] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[2] * coefs2[0]);
            c[2] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[3] * coefs2[0]);
            c[3] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[5] * coefs2[0]);
            c[5] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[7] * coefs2[0]);
            c[7] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[9] * coefs2[0]);
            c[9] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:03.6202152+02:00
            
            return c;
        }
        
        
    }
}
