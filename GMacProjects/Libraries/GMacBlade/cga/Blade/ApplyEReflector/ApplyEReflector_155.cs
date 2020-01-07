namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyEReflector_155(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:03.9066634+02:00
            //Macro: geometry3d.cga.ApplyEReflector
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.4 average, 21 total.
            //Memory Reads: 1.93333333333333 average, 29 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
            //   v.#no# <=> <Variable> coefs1[0]
            //   v.#e1# <=> <Variable> coefs1[1]
            //   v.#e2# <=> <Variable> coefs1[2]
            //   v.#e3# <=> <Variable> coefs1[3]
            //   v.#ni# <=> <Variable> coefs1[4]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0000 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (coefs1[3] * coefs2[0]);
            tempVar0001 = (coefs1[3] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[1] * coefs2[0]);
            tempVar0001 = (coefs1[1] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            c[0] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:03.9086635+02:00
            
            return c;
        }
        
        
    }
}
