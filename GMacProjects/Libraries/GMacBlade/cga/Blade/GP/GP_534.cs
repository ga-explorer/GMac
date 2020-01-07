namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] GP_534(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:58.0728979+02:00
            //Macro: geometry3d.cga.GP
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 5 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   mv1.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
            //   mv2.#no^e1^e2# <=> <Variable> coefs2[0]
            //   mv2.#no^e1^e3# <=> <Variable> coefs2[1]
            //   mv2.#no^e2^e3# <=> <Variable> coefs2[2]
            //   mv2.#e1^e2^e3# <=> <Variable> coefs2[3]
            //   mv2.#no^e1^ni# <=> <Variable> coefs2[4]
            //   mv2.#no^e2^ni# <=> <Variable> coefs2[5]
            //   mv2.#e1^e2^ni# <=> <Variable> coefs2[6]
            //   mv2.#no^e3^ni# <=> <Variable> coefs2[7]
            //   mv2.#e1^e3^ni# <=> <Variable> coefs2[8]
            //   mv2.#e2^e3^ni# <=> <Variable> coefs2[9]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:58.0738979+02:00
            
            return c;
        }
        
        
    }
}