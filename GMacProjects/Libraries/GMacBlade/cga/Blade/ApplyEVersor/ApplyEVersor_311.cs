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
        private static double[] ApplyEVersor_311(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:02.0605578+02:00
            //Macro: geometry3d.cga.ApplyEVersor
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 210 sub-expressions, 0 generated temps, 210 total.
            //Target Temp Variables: 18 total.
            //Output Variables: 5 total.
            //Computations: 1.27906976744186 average, 275 total.
            //Memory Reads: 1.90232558139535 average, 409 total.
            //Memory Writes: 215 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   v.#no^e1^e2# <=> <Variable> coefs1[0]
            //   v.#no^e1^e3# <=> <Variable> coefs1[1]
            //   v.#no^e2^e3# <=> <Variable> coefs1[2]
            //   v.#e1^e2^e3# <=> <Variable> coefs1[3]
            //   v.#no^e1^ni# <=> <Variable> coefs1[4]
            //   v.#no^e2^ni# <=> <Variable> coefs1[5]
            //   v.#e1^e2^ni# <=> <Variable> coefs1[6]
            //   v.#no^e3^ni# <=> <Variable> coefs1[7]
            //   v.#e1^e3^ni# <=> <Variable> coefs1[8]
            //   v.#e2^e3^ni# <=> <Variable> coefs1[9]
            //   mv.#no# <=> <Variable> coefs2[0]
            //   mv.#e1# <=> <Variable> coefs2[1]
            //   mv.#e2# <=> <Variable> coefs2[2]
            //   mv.#e3# <=> <Variable> coefs2[3]
            //   mv.#ni# <=> <Variable> coefs2[4]
            
            double[] tempArray = new double[18];
            
            tempArray[0] = (-1 * coefs1[0] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[3] * coefs2[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[6] * coefs2[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[1] * coefs2[0]);
            tempArray[3] = (coefs1[3] * coefs2[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[8] * coefs2[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * coefs2[0]);
            tempArray[4] = (-1 * coefs1[3] * coefs2[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[9] * coefs2[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (coefs1[3] * coefs2[0]);
            tempArray[5] = (-1 * coefs1[2] * coefs2[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[1] * coefs2[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[0] * coefs2[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[6] = (coefs1[6] * coefs2[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs1[8] * coefs2[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[5] * coefs2[0]);
            tempArray[7] = (-1 * coefs1[6] * coefs2[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (coefs1[9] * coefs2[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (coefs1[6] * coefs2[0]);
            tempArray[8] = (-1 * coefs1[5] * coefs2[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[4] * coefs2[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[0] * coefs2[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[7] * coefs2[0]);
            tempArray[9] = (-1 * coefs1[8] * coefs2[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[9] * coefs2[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[7] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (coefs1[8] * coefs2[0]);
            tempArray[10] = (-1 * coefs1[7] * coefs2[1]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (coefs1[4] * coefs2[3]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[1] * coefs2[4]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (coefs1[9] * coefs2[0]);
            tempArray[11] = (-1 * coefs1[7] * coefs2[2]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (coefs1[5] * coefs2[3]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[2] * coefs2[4]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * coefs1[9] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[11]);
            tempArray[11] = Math.Pow(coefs1[0], 2);
            tempArray[11] = (-1 * tempArray[11]);
            tempArray[12] = Math.Pow(coefs1[1], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[2], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[3], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[4], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[5], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[6], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[7], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[8], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = Math.Pow(coefs1[9], 2);
            tempArray[12] = (-1 * tempArray[12]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[11] = Math.Pow(tempArray[11], -1);
            c[0] = (tempArray[1] * tempArray[11]);
            tempArray[1] = (coefs1[0] * coefs2[1]);
            tempArray[12] = (-1 * coefs1[2] * coefs2[3]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[5] * coefs2[4]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[12] = (coefs1[0] * tempArray[1]);
            tempArray[13] = (coefs1[1] * coefs2[1]);
            tempArray[14] = (coefs1[2] * coefs2[2]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[7] * coefs2[4]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (coefs1[1] * tempArray[13]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[3] * tempArray[3]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (coefs1[2] * tempArray[4]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (coefs1[4] * coefs2[1]);
            tempArray[15] = (coefs1[5] * coefs2[2]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (coefs1[7] * coefs2[3]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (coefs1[4] * tempArray[14]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[6] * tempArray[6]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (coefs1[5] * tempArray[7]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[8] * tempArray[8]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (coefs1[7] * tempArray[9]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (coefs1[9] * coefs2[1]);
            tempArray[16] = (-1 * coefs1[8] * coefs2[2]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (coefs1[6] * coefs2[3]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[3] * coefs2[4]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[9] * tempArray[15]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            c[1] = (tempArray[11] * tempArray[12]);
            tempArray[12] = (-1 * coefs1[0] * coefs2[2]);
            tempArray[16] = (-1 * coefs1[1] * coefs2[3]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[4] * coefs2[4]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[0] * tempArray[12]);
            tempArray[17] = (coefs1[2] * tempArray[13]);
            tempArray[16] = (tempArray[16] + tempArray[17]);
            tempArray[17] = (coefs1[3] * tempArray[2]);
            tempArray[16] = (tempArray[16] + tempArray[17]);
            tempArray[17] = (-1 * coefs1[1] * tempArray[4]);
            tempArray[16] = (tempArray[16] + tempArray[17]);
            tempArray[17] = (coefs1[5] * tempArray[14]);
            tempArray[16] = (tempArray[16] + tempArray[17]);
            tempArray[17] = (coefs1[6] * tempArray[5]);
            tempArray[16] = (tempArray[16] + tempArray[17]);
            tempArray[17] = (-1 * coefs1[4] * tempArray[7]);
            tempArray[16] = (tempArray[16] + tempArray[17]);
            tempArray[8] = (-1 * coefs1[9] * tempArray[8]);
            tempArray[8] = (tempArray[16] + tempArray[8]);
            tempArray[16] = (coefs1[7] * tempArray[10]);
            tempArray[8] = (tempArray[8] + tempArray[16]);
            tempArray[16] = (coefs1[8] * tempArray[15]);
            tempArray[8] = (tempArray[8] + tempArray[16]);
            c[2] = (tempArray[11] * tempArray[8]);
            tempArray[8] = (-1 * coefs1[1] * tempArray[12]);
            tempArray[16] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[8] = (tempArray[8] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[16]);
            tempArray[4] = (coefs1[0] * tempArray[4]);
            tempArray[4] = (tempArray[8] + tempArray[4]);
            tempArray[8] = (coefs1[7] * tempArray[14]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[5] = (coefs1[8] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[9] * tempArray[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[4] * tempArray[9]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[5] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[6] * tempArray[15]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            c[3] = (tempArray[11] * tempArray[4]);
            tempArray[4] = (-1 * coefs1[4] * tempArray[12]);
            tempArray[1] = (-1 * coefs1[5] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[0] = (-1 * coefs1[6] * tempArray[0]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * coefs1[7] * tempArray[13]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[8] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[9] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[0] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[1] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[2] * tempArray[10]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[3] * tempArray[15]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            c[4] = (tempArray[11] * tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:02.0795589+02:00
            
            return c;
        }
        
        
    }
}
