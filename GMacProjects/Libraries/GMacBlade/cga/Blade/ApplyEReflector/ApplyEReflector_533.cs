namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyEReflector_533(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:04.5837021+02:00
            //Macro: geometry3d.cga.ApplyEReflector
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 10 total.
            //Computations: 1.26666666666667 average, 38 total.
            //Memory Reads: 1.66666666666667 average, 50 total.
            //Memory Writes: 30 total.
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
            //   v.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
            //   mv.#no^e1^e2# <=> <Variable> coefs2[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs2[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs2[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs2[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs2[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs2[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs2[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs2[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs2[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs2[9]
            
            double tempVar0000;
            
            tempVar0000 = (coefs1[0] * coefs2[0]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[0] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[1]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[1] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[2]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[3] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[4]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[4] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[5]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[5] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[6]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[6] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[7]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[7] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[8]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[8] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[9]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[9] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:04.5887024+02:00
            
            return c;
        }
        
        
    }
}
