namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] EGPDual_311(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:04.3702581+02:00
            //Macro: geometry3d.cga.EGPDual
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 32 sub-expressions, 0 generated temps, 32 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1.27027027027027 average, 47 total.
            //Memory Reads: 1.94594594594595 average, 72 total.
            //Memory Writes: 37 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   mv1.#no^e1^e2# <=> <Variable> coefs1[0]
            //   mv1.#no^e1^e3# <=> <Variable> coefs1[1]
            //   mv1.#no^e2^e3# <=> <Variable> coefs1[2]
            //   mv1.#e1^e2^e3# <=> <Variable> coefs1[3]
            //   mv1.#no^e1^ni# <=> <Variable> coefs1[4]
            //   mv1.#no^e2^ni# <=> <Variable> coefs1[5]
            //   mv1.#e1^e2^ni# <=> <Variable> coefs1[6]
            //   mv1.#no^e3^ni# <=> <Variable> coefs1[7]
            //   mv1.#e1^e3^ni# <=> <Variable> coefs1[8]
            //   mv1.#e2^e3^ni# <=> <Variable> coefs1[9]
            //   mv2.#no# <=> <Variable> coefs2[0]
            //   mv2.#e1# <=> <Variable> coefs2[1]
            //   mv2.#e2# <=> <Variable> coefs2[2]
            //   mv2.#e3# <=> <Variable> coefs2[3]
            //   mv2.#ni# <=> <Variable> coefs2[4]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (coefs1[9] * coefs2[1]);
            tempVar0001 = (-1 * coefs1[8] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[6] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[4]);
            c[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[8] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[7] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[1] * coefs2[4]);
            c[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[1] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            c[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (coefs1[9] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[7] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[5] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[1] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[6] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[5] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[4] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[3] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:04.3772585+02:00
            
            return c;
        }
        
        
    }
}
