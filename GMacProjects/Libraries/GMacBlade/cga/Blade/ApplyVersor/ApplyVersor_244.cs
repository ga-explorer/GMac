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
        private static double[] ApplyVersor_244(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:18.4980661+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 279 sub-expressions, 0 generated temps, 279 total.
            //Target Temp Variables: 29 total.
            //Output Variables: 5 total.
            //Computations: 1.23943661971831 average, 352 total.
            //Memory Reads: 1.9612676056338 average, 557 total.
            //Memory Writes: 284 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
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
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs2[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs2[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs2[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs2[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs2[4]
            
            double[] tempArray = new double[29];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs1[1] * tempArray[0]);
            tempArray[2] = (coefs1[8] * tempArray[0]);
            tempArray[3] = (tempArray[1] + tempArray[2]);
            tempArray[4] = (-1 * coefs2[3] * tempArray[3]);
            tempArray[5] = (coefs1[0] * tempArray[0]);
            tempArray[6] = (coefs1[7] * tempArray[0]);
            tempArray[7] = (tempArray[5] + tempArray[6]);
            tempArray[8] = (-1 * coefs2[2] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (coefs2[0] * tempArray[0]);
            tempArray[9] = (-1 * coefs2[4] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[10] = (coefs1[2] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[11] = (-1 * coefs1[1] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[11]);
            tempArray[12] = (coefs2[3] * tempArray[2]);
            tempArray[13] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[13]);
            tempArray[14] = (coefs2[2] * tempArray[6]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[9] = (tempArray[9] + tempArray[14]);
            tempArray[14] = (coefs1[2] * tempArray[9]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[12] = (tempArray[0] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[14] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[4] = (tempArray[12] + tempArray[4]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[12] = (tempArray[14] + tempArray[4]);
            tempArray[14] = (coefs1[3] * tempArray[0]);
            tempArray[15] = (coefs1[9] * tempArray[0]);
            tempArray[16] = (tempArray[14] + tempArray[15]);
            tempArray[17] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[15] = (tempArray[15] + tempArray[17]);
            tempArray[18] = (-1 * coefs2[2] * tempArray[16]);
            tempArray[19] = (-1 * coefs2[1] * tempArray[3]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = (-1 * coefs1[5] * tempArray[8]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = (-1 * tempArray[0] * tempArray[18]);
            tempArray[20] = (coefs2[2] * tempArray[15]);
            tempArray[21] = (coefs2[1] * tempArray[2]);
            tempArray[20] = (tempArray[20] + tempArray[21]);
            tempArray[21] = (-1 * coefs1[5] * tempArray[9]);
            tempArray[20] = (tempArray[20] + tempArray[21]);
            tempArray[20] = (tempArray[0] * tempArray[20]);
            tempArray[19] = (tempArray[19] + tempArray[20]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[19]);
            tempArray[18] = (tempArray[0] * tempArray[18]);
            tempArray[18] = (tempArray[20] + tempArray[18]);
            tempArray[18] = (tempArray[0] * tempArray[18]);
            tempArray[20] = (tempArray[21] + tempArray[18]);
            tempArray[21] = (-1 * coefs2[1] * tempArray[16]);
            tempArray[22] = (coefs2[2] * tempArray[3]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (-1 * coefs2[3] * tempArray[7]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (-1 * coefs1[6] * tempArray[9]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (tempArray[0] * tempArray[21]);
            tempArray[23] = (coefs2[1] * tempArray[15]);
            tempArray[24] = (-1 * coefs2[2] * tempArray[2]);
            tempArray[23] = (tempArray[23] + tempArray[24]);
            tempArray[24] = (coefs2[3] * tempArray[6]);
            tempArray[23] = (tempArray[23] + tempArray[24]);
            tempArray[24] = (coefs1[6] * tempArray[8]);
            tempArray[23] = (tempArray[23] + tempArray[24]);
            tempArray[23] = (-1 * tempArray[0] * tempArray[23]);
            tempArray[22] = (tempArray[22] + tempArray[23]);
            tempArray[24] = (-1 * tempArray[0] * tempArray[22]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[21]);
            tempArray[21] = (tempArray[23] + tempArray[21]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[21]);
            tempArray[23] = (tempArray[24] + tempArray[21]);
            tempArray[24] = (-1 * coefs1[9] * tempArray[0]);
            tempArray[14] = (tempArray[14] + tempArray[24]);
            tempArray[25] = (-1 * coefs1[5] * coefs2[2]);
            tempArray[26] = (coefs1[4] * coefs2[3]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[26] = (tempArray[8] * tempArray[16]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[26] = (-1 * tempArray[9] * tempArray[15]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[17] = (tempArray[17] + tempArray[24]);
            tempArray[24] = (coefs1[6] * coefs2[2]);
            tempArray[26] = (tempArray[8] * tempArray[2]);
            tempArray[24] = (tempArray[24] + tempArray[26]);
            tempArray[26] = (tempArray[3] * tempArray[9]);
            tempArray[24] = (tempArray[24] + tempArray[26]);
            tempArray[26] = (-1 * coefs1[8] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[26]);
            tempArray[11] = (tempArray[11] + tempArray[26]);
            tempArray[26] = (-1 * coefs1[7] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[26]);
            tempArray[27] = (coefs1[4] * coefs2[1]);
            tempArray[28] = (-1 * coefs1[2] * coefs2[2]);
            tempArray[27] = (tempArray[27] + tempArray[28]);
            tempArray[8] = (-1 * tempArray[7] * tempArray[8]);
            tempArray[8] = (tempArray[27] + tempArray[8]);
            tempArray[9] = (tempArray[6] * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (tempArray[13] + tempArray[26]);
            tempArray[10] = (tempArray[0] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (tempArray[0] * tempArray[19]);
            tempArray[10] = (tempArray[18] + tempArray[10]);
            tempArray[13] = (tempArray[0] * tempArray[22]);
            tempArray[13] = (tempArray[21] + tempArray[13]);
            tempArray[18] = Math.Pow(coefs1[2], 2);
            tempArray[18] = (-1 * tempArray[18]);
            tempArray[19] = Math.Pow(coefs1[4], 2);
            tempArray[19] = (-1 * tempArray[19]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = Math.Pow(coefs1[5], 2);
            tempArray[19] = (-1 * tempArray[19]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = Math.Pow(coefs1[6], 2);
            tempArray[19] = (-1 * tempArray[19]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[6] = (tempArray[6] * tempArray[5]);
            tempArray[6] = (tempArray[18] + tempArray[6]);
            tempArray[7] = (tempArray[7] * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[2] = (tempArray[2] * tempArray[1]);
            tempArray[2] = (tempArray[6] + tempArray[2]);
            tempArray[3] = (tempArray[3] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[15] * tempArray[14]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[16] * tempArray[17]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[2] = Math.Pow(tempArray[2], -1);
            tempArray[3] = (coefs1[2] * coefs2[1]);
            tempArray[6] = (coefs1[4] * coefs2[2]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (coefs1[5] * coefs2[3]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (-1 * coefs1[4] * tempArray[3]);
            tempArray[7] = (-1 * coefs1[2] * tempArray[8]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (coefs1[6] * tempArray[24]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (coefs1[5] * tempArray[25]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[14] * tempArray[10]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[20] * tempArray[17]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[23] * tempArray[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[11] * tempArray[13]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[5] * tempArray[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[12] * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            c[2] = (tempArray[2] * tempArray[6]);
            tempArray[6] = (coefs2[3] * tempArray[16]);
            tempArray[7] = (-1 * coefs2[1] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[4] * tempArray[8]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[8] = (-1 * coefs2[3] * tempArray[15]);
            tempArray[10] = (coefs2[1] * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[4] * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[6] = (tempArray[8] + tempArray[6]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[8] = (tempArray[10] + tempArray[6]);
            tempArray[10] = (coefs1[5] * coefs2[1]);
            tempArray[15] = (-1 * coefs1[2] * coefs2[3]);
            tempArray[10] = (tempArray[10] + tempArray[15]);
            tempArray[15] = (-1 * tempArray[3] * tempArray[8]);
            tempArray[10] = (tempArray[10] + tempArray[15]);
            tempArray[15] = (tempArray[2] * tempArray[9]);
            tempArray[10] = (tempArray[10] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[6] * coefs2[3]);
            tempArray[16] = (tempArray[8] * tempArray[6]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (tempArray[7] * tempArray[9]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[3] = (coefs1[5] * tempArray[3]);
            tempArray[7] = (-1 * coefs1[6] * tempArray[15]);
            tempArray[3] = (tempArray[3] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[2] * tempArray[10]);
            tempArray[3] = (tempArray[3] + tempArray[7]);
            tempArray[7] = (coefs1[4] * tempArray[25]);
            tempArray[3] = (tempArray[3] + tempArray[7]);
            tempArray[6] = (tempArray[14] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[8] * tempArray[17]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[3] = (tempArray[12] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[23] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[9] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            c[3] = (tempArray[2] * tempArray[1]);
            tempArray[1] = (coefs1[6] * coefs2[1]);
            tempArray[3] = (-1 * tempArray[8] * tempArray[15]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[9] * tempArray[16]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[3]);
            tempArray[4] = (coefs1[4] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * coefs1[5] * tempArray[10]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[1] = (coefs1[6] * tempArray[1]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[3] = (tempArray[23] * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[17] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[20] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            c[1] = (tempArray[2] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[2] * tempArray[12]);
            tempArray[3] = (coefs1[4] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[5] * tempArray[20]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[6] * tempArray[23]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[25] * tempArray[17]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[24] * tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[10] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[15] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[1]);
            tempArray[4] = (-1 * coefs1[2] * tempArray[4]);
            tempArray[5] = (coefs1[4] * tempArray[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[5] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (coefs1[6] * tempArray[13]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[14] * tempArray[25]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[1] * tempArray[17]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[1] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[24] * tempArray[11]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[5] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[15] * tempArray[9]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            c[0] = (tempArray[3] * tempArray[2]);
            tempArray[0] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[0] = (tempArray[4] + tempArray[0]);
            c[4] = (tempArray[2] * tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:18.5200674+02:00
            
            return c;
        }
        
        
    }
}