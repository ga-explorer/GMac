using System;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyVersor_255(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:18.9740934+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 108 sub-expressions, 0 generated temps, 108 total.
            //Target Temp Variables: 18 total.
            //Output Variables: 1 total.
            //Computations: 1.25688073394495 average, 137 total.
            //Memory Reads: 1.89908256880734 average, 207 total.
            //Memory Writes: 109 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
            //   v.#no^e1# <=> <Variable> coefs1[0]
            //   v.#no^e2# <=> <Variable> coefs1[1]
            //   v.#e1^e2# <=> <Variable> coefs1[2]
            //   v.#no^e3# <=> <Variable> coefs1[3]
            //   v.#e1^e3# <=> <Variable> coefs1[4]
            //   v.#e2^e3# <=> <Variable> coefs1[5]
            //   v.#no^ni# <=> <Variable> coefs1[6]
            //   v.#e1^ni# <=> <Variable> coefs1[7]
            //   v.#e2^ni# <=> <Variable> coefs1[8]
            //   v.#e3^ni# <=> <Variable> coefs1[9]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double[] tempArray = new double[18];
            
            tempArray[0] = (coefs1[2] * coefs2[0]);
            tempArray[0] = (-1 * coefs1[2] * tempArray[0]);
            tempArray[1] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[1] = (coefs1[4] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[5] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[6] * coefs2[0]);
            tempArray[1] = (coefs1[6] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (coefs1[3] * tempArray[1]);
            tempArray[3] = (coefs1[9] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[5] = (-1 * coefs2[0] * tempArray[4]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[6] = (-1 * coefs1[3] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[7] = (coefs2[0] * tempArray[3]);
            tempArray[8] = (tempArray[1] * tempArray[7]);
            tempArray[8] = (tempArray[5] + tempArray[8]);
            tempArray[9] = (tempArray[1] * tempArray[8]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[7] = (tempArray[9] + tempArray[5]);
            tempArray[9] = (-1 * coefs1[9] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[7] = (-1 * tempArray[7] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[8]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[5] = (tempArray[5] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[5] = (coefs1[1] * tempArray[1]);
            tempArray[7] = (coefs1[8] * tempArray[1]);
            tempArray[8] = (tempArray[5] + tempArray[7]);
            tempArray[9] = (coefs2[0] * tempArray[8]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[9]);
            tempArray[10] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[7] = (tempArray[7] + tempArray[10]);
            tempArray[11] = (-1 * coefs2[0] * tempArray[7]);
            tempArray[12] = (tempArray[1] * tempArray[11]);
            tempArray[12] = (tempArray[9] + tempArray[12]);
            tempArray[13] = (tempArray[1] * tempArray[12]);
            tempArray[11] = (-1 * tempArray[1] * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[9] = (-1 * tempArray[1] * tempArray[9]);
            tempArray[11] = (tempArray[13] + tempArray[9]);
            tempArray[13] = (-1 * coefs1[8] * tempArray[1]);
            tempArray[5] = (tempArray[5] + tempArray[13]);
            tempArray[11] = (tempArray[11] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[1] * tempArray[12]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[10] = (tempArray[10] + tempArray[13]);
            tempArray[9] = (-1 * tempArray[9] * tempArray[10]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[9] = (coefs1[0] * tempArray[1]);
            tempArray[11] = (coefs1[7] * tempArray[1]);
            tempArray[12] = (tempArray[9] + tempArray[11]);
            tempArray[13] = (coefs2[0] * tempArray[12]);
            tempArray[13] = (-1 * tempArray[1] * tempArray[13]);
            tempArray[14] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[11] = (tempArray[11] + tempArray[14]);
            tempArray[15] = (-1 * coefs2[0] * tempArray[11]);
            tempArray[16] = (tempArray[1] * tempArray[15]);
            tempArray[16] = (tempArray[13] + tempArray[16]);
            tempArray[17] = (tempArray[1] * tempArray[16]);
            tempArray[15] = (-1 * tempArray[1] * tempArray[15]);
            tempArray[13] = (tempArray[13] + tempArray[15]);
            tempArray[13] = (-1 * tempArray[1] * tempArray[13]);
            tempArray[15] = (tempArray[17] + tempArray[13]);
            tempArray[17] = (-1 * coefs1[7] * tempArray[1]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[15] = (-1 * tempArray[15] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[15]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[16]);
            tempArray[1] = (tempArray[13] + tempArray[1]);
            tempArray[13] = (tempArray[14] + tempArray[17]);
            tempArray[1] = (tempArray[1] * tempArray[13]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(coefs1[2], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[14] = Math.Pow(coefs1[4], 2);
            tempArray[14] = (-1 * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[14] = Math.Pow(coefs1[5], 2);
            tempArray[14] = (-1 * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[14] = Math.Pow(coefs1[6], 2);
            tempArray[14] = (-1 * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[9] = (tempArray[11] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (tempArray[12] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[5] = (tempArray[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (tempArray[8] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = Math.Pow(tempArray[1], -1);
            c[0] = (tempArray[0] * tempArray[1]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:18.9830939+02:00
            
            return c;
        }
        
        
    }
}
