namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyReflector_555(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:01.4705240+02:00
            //Macro: geometry3d.cga.ApplyReflector
            //Input Variables: 0 used, 2 not used, 2 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1.33333333333333 average, 4 total.
            //Memory Reads: 1.66666666666667 average, 5 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
            //   v.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[0] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:01.4715241+02:00
            
            return c;
        }
        
        
    }
}
