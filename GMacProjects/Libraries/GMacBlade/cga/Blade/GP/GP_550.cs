namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] GP_550(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:58.6359301+02:00
            //Macro: geometry3d.cga.GP
            //Input Variables: 0 used, 2 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 2 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
            //   mv1.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
            //   mv2.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            
            c[0] = (-1 * coefs1[0] * coefs2[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:58.6359301+02:00
            
            return c;
        }
        
        
    }
}
