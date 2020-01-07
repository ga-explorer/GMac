namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ERCP_101(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:44.0710970+02:00
            //Macro: geometry3d.cga.ERCP
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 5 total.
            //Computations: 2 average, 10 total.
            //Memory Reads: 2 average, 10 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   mv1.#no# <=> <Variable> coefs1[0]
            //   mv1.#e1# <=> <Variable> coefs1[1]
            //   mv1.#e2# <=> <Variable> coefs1[2]
            //   mv1.#e3# <=> <Variable> coefs1[3]
            //   mv1.#ni# <=> <Variable> coefs1[4]
            //   mv2.#E0# <=> <Variable> coefs2[0]
            
            
            c[0] = (-1 * coefs1[0] * coefs2[0]);
            c[1] = (-1 * coefs1[1] * coefs2[0]);
            c[2] = (-1 * coefs1[2] * coefs2[0]);
            c[3] = (-1 * coefs1[3] * coefs2[0]);
            c[4] = (-1 * coefs1[4] * coefs2[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:44.0720971+02:00
            
            return c;
        }
        
        
    }
}
