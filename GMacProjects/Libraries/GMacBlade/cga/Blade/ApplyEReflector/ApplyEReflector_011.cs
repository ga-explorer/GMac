namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyEReflector_011(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:03.7156524+02:00
            //Macro: geometry3d.cga.ApplyEReflector
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 5 total.
            //Computations: 1.66666666666667 average, 25 total.
            //Memory Reads: 1.66666666666667 average, 25 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   v.#E0# <=> <Variable> coefs1[0]
            //   mv.#no# <=> <Variable> coefs2[0]
            //   mv.#e1# <=> <Variable> coefs2[1]
            //   mv.#e2# <=> <Variable> coefs2[2]
            //   mv.#e3# <=> <Variable> coefs2[3]
            //   mv.#ni# <=> <Variable> coefs2[4]
            
            double tempVar0000;
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[0] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[1] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[2]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[3] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[4] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:03.7186526+02:00
            
            return c;
        }
        
        
    }
}
