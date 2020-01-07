namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] EGPDual_445(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:08.0024658+02:00
            //Macro: geometry3d.cga.EGPDual
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.55555555555556 average, 14 total.
            //Memory Reads: 2 average, 18 total.
            //Memory Writes: 9 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
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
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[4]);
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:08.0044659+02:00
            
            return c;
        }
        
        
    }
}
