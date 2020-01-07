namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] EGPDual_443(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:08.1314732+02:00
            //Macro: geometry3d.cga.EGPDual
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 26 sub-expressions, 0 generated temps, 26 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 10 total.
            //Computations: 1.27777777777778 average, 46 total.
            //Memory Reads: 1.83333333333333 average, 66 total.
            //Memory Writes: 36 total.
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
            
            tempVar0000 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0001 = (coefs1[0] * coefs2[2]);
            c[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0001 = (coefs1[0] * coefs2[4]);
            c[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * coefs1[3] * coefs2[1]);
            tempVar0001 = (coefs1[1] * coefs2[3]);
            c[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * coefs1[4] * coefs2[2]);
            tempVar0001 = (coefs1[2] * coefs2[4]);
            c[8] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[1] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[0] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[2] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[2] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[1] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[4] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[4] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[1] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[6] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[3] * coefs2[2]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[7] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[4] * coefs2[3]);
            tempVar0001 = (-1 * coefs1[3] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[9] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:08.1364735+02:00
            
            return c;
        }
        
        
    }
}
