namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyERotor_544(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:03.3236300+02:00
            //Macro: geometry3d.cga.ApplyERotor
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 5 total.
            //Computations: 1.6 average, 16 total.
            //Memory Reads: 2 average, 20 total.
            //Memory Writes: 10 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   v.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs2[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs2[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs2[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs2[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs2[4]
            
            double tempVar0000;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            c[0] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[1]);
            c[1] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[2]);
            c[2] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[3]);
            c[3] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[4]);
            c[4] = (-1 * coefs1[0] * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:03.3246301+02:00
            
            return c;
        }
        
        
    }
}