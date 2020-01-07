namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] EGPDual_553(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.3505429+02:00
            //Macro: geometry3d.cga.EGPDual
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
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
            //   mv1.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
            //   mv2.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            c[5] = 0;
            c[6] = 0;
            c[7] = 0;
            c[8] = 0;
            c[9] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.3515430+02:00
            
            return c;
        }
        
        
    }
}
