namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyRotor_000(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:28.9806657+02:00
            //Macro: geometry3d.cga.ApplyRotor
            //Input Variables: 0 used, 2 not used, 2 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 4 total.
            //Memory Reads: 2 average, 4 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
            //   v.#E0# <=> <Variable> coefs1[0]
            //   mv.#E0# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            c[0] = (-1 * coefs1[0] * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:28.9816658+02:00
            
            return c;
        }
        
        
    }
}
