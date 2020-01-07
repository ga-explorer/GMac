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
        private static double[] ApplyReflector_311(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:54.0030969+02:00
            //Macro: geometry3d.cga.ApplyReflector
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 255 sub-expressions, 0 generated temps, 255 total.
            //Target Temp Variables: 27 total.
            //Output Variables: 5 total.
            //Computations: 1.24615384615385 average, 324 total.
            //Memory Reads: 1.97307692307692 average, 513 total.
            //Memory Writes: 260 total.
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
            
            double[] tempArray = new double[27];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (-1 * coefs1[2] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[9] * tempArray[0]);
            tempArray[3] = (tempArray[1] + tempArray[2]);
            tempArray[4] = (-1 * coefs2[2] * tempArray[3]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[0]);
            tempArray[6] = (-1 * coefs1[8] * tempArray[0]);
            tempArray[7] = (tempArray[5] + tempArray[6]);
            tempArray[8] = (-1 * coefs2[1] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[9] = (coefs2[4] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[10] = (-1 * coefs1[7] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[11] = (coefs1[2] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[11]);
            tempArray[12] = (-1 * coefs2[2] * tempArray[2]);
            tempArray[13] = (coefs1[1] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[13]);
            tempArray[14] = (-1 * coefs2[1] * tempArray[6]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (coefs2[0] * tempArray[0]);
            tempArray[9] = (tempArray[9] + tempArray[14]);
            tempArray[14] = (coefs1[7] * tempArray[9]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[12] = (tempArray[0] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[14] = (tempArray[0] * tempArray[10]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[4] = (tempArray[12] + tempArray[4]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[12] = (tempArray[14] + tempArray[4]);
            tempArray[14] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[15] = (-1 * coefs1[6] * tempArray[0]);
            tempArray[16] = (tempArray[14] + tempArray[15]);
            tempArray[17] = (coefs1[0] * tempArray[0]);
            tempArray[15] = (tempArray[15] + tempArray[17]);
            tempArray[18] = (-1 * coefs2[3] * tempArray[7]);
            tempArray[19] = (-1 * coefs2[2] * tempArray[16]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = (coefs1[4] * tempArray[8]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = (-1 * tempArray[0] * tempArray[18]);
            tempArray[20] = (-1 * coefs2[3] * tempArray[6]);
            tempArray[21] = (-1 * coefs2[2] * tempArray[15]);
            tempArray[20] = (tempArray[20] + tempArray[21]);
            tempArray[21] = (-1 * coefs1[4] * tempArray[9]);
            tempArray[20] = (tempArray[20] + tempArray[21]);
            tempArray[20] = (tempArray[0] * tempArray[20]);
            tempArray[19] = (tempArray[19] + tempArray[20]);
            tempArray[21] = (tempArray[0] * tempArray[19]);
            tempArray[18] = (tempArray[0] * tempArray[18]);
            tempArray[18] = (tempArray[20] + tempArray[18]);
            tempArray[18] = (tempArray[0] * tempArray[18]);
            tempArray[20] = (tempArray[21] + tempArray[18]);
            tempArray[21] = (coefs2[1] * tempArray[3]);
            tempArray[22] = (-1 * coefs2[2] * tempArray[7]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (coefs2[3] * tempArray[16]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (coefs1[3] * tempArray[9]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (tempArray[0] * tempArray[21]);
            tempArray[23] = (coefs2[1] * tempArray[2]);
            tempArray[24] = (-1 * coefs2[2] * tempArray[6]);
            tempArray[23] = (tempArray[23] + tempArray[24]);
            tempArray[24] = (coefs2[3] * tempArray[15]);
            tempArray[23] = (tempArray[23] + tempArray[24]);
            tempArray[24] = (coefs1[3] * tempArray[8]);
            tempArray[23] = (tempArray[23] + tempArray[24]);
            tempArray[23] = (-1 * tempArray[0] * tempArray[23]);
            tempArray[22] = (tempArray[22] + tempArray[23]);
            tempArray[24] = (tempArray[0] * tempArray[22]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[21]);
            tempArray[21] = (tempArray[23] + tempArray[21]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[21]);
            tempArray[23] = (tempArray[24] + tempArray[21]);
            tempArray[24] = (coefs1[9] * tempArray[0]);
            tempArray[11] = (tempArray[11] + tempArray[24]);
            tempArray[25] = (coefs1[7] * coefs2[2]);
            tempArray[26] = (-1 * coefs1[5] * coefs2[3]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[2] = (tempArray[2] * tempArray[9]);
            tempArray[2] = (tempArray[25] + tempArray[2]);
            tempArray[3] = (-1 * tempArray[3] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[24]);
            tempArray[3] = (coefs1[3] * coefs2[2]);
            tempArray[24] = (tempArray[7] * tempArray[9]);
            tempArray[3] = (tempArray[3] + tempArray[24]);
            tempArray[24] = (tempArray[8] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[24]);
            tempArray[24] = (coefs1[8] * tempArray[0]);
            tempArray[13] = (tempArray[13] + tempArray[24]);
            tempArray[5] = (tempArray[5] + tempArray[24]);
            tempArray[24] = (coefs1[6] * tempArray[0]);
            tempArray[17] = (tempArray[17] + tempArray[24]);
            tempArray[25] = (-1 * coefs1[5] * coefs2[1]);
            tempArray[26] = (coefs1[4] * coefs2[2]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[9] = (-1 * tempArray[9] * tempArray[15]);
            tempArray[9] = (tempArray[25] + tempArray[9]);
            tempArray[8] = (tempArray[8] * tempArray[16]);
            tempArray[8] = (tempArray[9] + tempArray[8]);
            tempArray[9] = (tempArray[14] + tempArray[24]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[19]);
            tempArray[10] = (tempArray[18] + tempArray[10]);
            tempArray[14] = (-1 * tempArray[0] * tempArray[22]);
            tempArray[14] = (tempArray[21] + tempArray[14]);
            tempArray[15] = (coefs1[4] * coefs2[1]);
            tempArray[16] = (coefs1[5] * coefs2[2]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (coefs1[7] * coefs2[3]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (coefs1[5] * tempArray[15]);
            tempArray[18] = (-1 * coefs1[7] * tempArray[2]);
            tempArray[16] = (tempArray[16] + tempArray[18]);
            tempArray[3] = (-1 * coefs1[3] * tempArray[3]);
            tempArray[3] = (tempArray[16] + tempArray[3]);
            tempArray[8] = (coefs1[4] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[8]);
            tempArray[4] = (tempArray[11] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[12] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[23] * tempArray[13]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[5] * tempArray[14]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[17] * tempArray[10]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[20] * tempArray[9]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            c[2] = (-1 * tempArray[3]);
            tempArray[3] = (coefs2[3] * tempArray[3]);
            tempArray[4] = (-1 * coefs2[1] * tempArray[16]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[5] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[8] = (coefs2[3] * tempArray[2]);
            tempArray[12] = (-1 * coefs2[1] * tempArray[15]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[12] = (coefs1[5] * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[12] = (tempArray[0] * tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[3] = (tempArray[8] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[8] = (tempArray[12] + tempArray[3]);
            tempArray[12] = (-1 * coefs1[7] * coefs2[1]);
            tempArray[16] = (coefs1[4] * coefs2[3]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[6] = (-1 * tempArray[6] * tempArray[9]);
            tempArray[6] = (tempArray[12] + tempArray[6]);
            tempArray[7] = (tempArray[7] * tempArray[8]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[3] * coefs2[3]);
            tempArray[12] = (tempArray[9] * tempArray[16]);
            tempArray[7] = (tempArray[7] + tempArray[12]);
            tempArray[12] = (tempArray[8] * tempArray[15]);
            tempArray[7] = (tempArray[7] + tempArray[12]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (coefs1[7] * tempArray[15]);
            tempArray[2] = (coefs1[5] * tempArray[2]);
            tempArray[2] = (tempArray[4] + tempArray[2]);
            tempArray[4] = (coefs1[4] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (coefs1[3] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[3] = (-1 * tempArray[11] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[1] = (-1 * tempArray[8] * tempArray[1]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[2] = (-1 * tempArray[13] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[20] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[23] * tempArray[17]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[9] * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[3] = (-1 * tempArray[1]);
            tempArray[1] = (coefs1[3] * coefs2[1]);
            tempArray[2] = (-1 * tempArray[3] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[8] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[4] * tempArray[15]);
            tempArray[1] = (-1 * coefs1[3] * tempArray[1]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[2] = (coefs1[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[5] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[23] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[1] * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[13] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[12] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[17] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[1] = (-1 * tempArray[1]);
            tempArray[1] = (coefs1[7] * tempArray[12]);
            tempArray[2] = (coefs1[5] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[4] * tempArray[20]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[3] * tempArray[23]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[1] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[2] * tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[3] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[7] * tempArray[17]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * coefs1[7] * tempArray[4]);
            tempArray[3] = (-1 * coefs1[5] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[4] * tempArray[10]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[14]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[11] * tempArray[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[13] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[3] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[17] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[7] * tempArray[9]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[1] + tempArray[3]);
            c[0] = (-1 * tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            c[4] = (-1 * tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:54.0280983+02:00
            
            return c;
        }
        
        
    }
}