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
        private static double[] ApplyReflector_211(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:50.3178861+02:00
            //Macro: geometry3d.cga.ApplyReflector
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 255 sub-expressions, 0 generated temps, 255 total.
            //Target Temp Variables: 28 total.
            //Output Variables: 5 total.
            //Computations: 1.30769230769231 average, 340 total.
            //Memory Reads: 1.97307692307692 average, 513 total.
            //Memory Writes: 260 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
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
            //   mv.#no# <=> <Variable> coefs2[0]
            //   mv.#e1# <=> <Variable> coefs2[1]
            //   mv.#e2# <=> <Variable> coefs2[2]
            //   mv.#e3# <=> <Variable> coefs2[3]
            //   mv.#ni# <=> <Variable> coefs2[4]
            
            double[] tempArray = new double[28];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (-1 * coefs1[3] * tempArray[0]);
            tempArray[2] = (coefs1[9] * tempArray[0]);
            tempArray[3] = (tempArray[1] + tempArray[2]);
            tempArray[4] = (-1 * coefs2[3] * tempArray[3]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[0]);
            tempArray[6] = (coefs1[8] * tempArray[0]);
            tempArray[7] = (tempArray[5] + tempArray[6]);
            tempArray[8] = (-1 * coefs2[2] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[9] = (coefs1[7] * tempArray[0]);
            tempArray[10] = (tempArray[8] + tempArray[9]);
            tempArray[11] = (-1 * coefs2[1] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[11]);
            tempArray[11] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[12] = (coefs2[4] * tempArray[0]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[13] = (coefs1[6] * tempArray[11]);
            tempArray[4] = (tempArray[4] + tempArray[13]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[13] = (coefs1[3] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[14] = (-1 * coefs2[3] * tempArray[2]);
            tempArray[15] = (coefs1[1] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[15]);
            tempArray[16] = (-1 * coefs2[2] * tempArray[6]);
            tempArray[14] = (tempArray[14] + tempArray[16]);
            tempArray[16] = (coefs1[0] * tempArray[0]);
            tempArray[9] = (tempArray[9] + tempArray[16]);
            tempArray[17] = (-1 * coefs2[1] * tempArray[9]);
            tempArray[14] = (tempArray[14] + tempArray[17]);
            tempArray[17] = (coefs2[0] * tempArray[0]);
            tempArray[12] = (tempArray[12] + tempArray[17]);
            tempArray[17] = (-1 * coefs1[6] * tempArray[12]);
            tempArray[14] = (tempArray[14] + tempArray[17]);
            tempArray[17] = (-1 * tempArray[0] * tempArray[14]);
            tempArray[17] = (tempArray[4] + tempArray[17]);
            tempArray[18] = (-1 * tempArray[0] * tempArray[17]);
            tempArray[14] = (tempArray[0] * tempArray[14]);
            tempArray[4] = (tempArray[4] + tempArray[14]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[14] = (tempArray[18] + tempArray[4]);
            tempArray[18] = (coefs2[1] * tempArray[3]);
            tempArray[19] = (-1 * coefs2[3] * tempArray[10]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[19] = (coefs1[4] * tempArray[12]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[18] = (-1 * tempArray[0] * tempArray[18]);
            tempArray[19] = (coefs2[1] * tempArray[2]);
            tempArray[20] = (-1 * coefs2[3] * tempArray[9]);
            tempArray[19] = (tempArray[19] + tempArray[20]);
            tempArray[20] = (coefs1[4] * tempArray[11]);
            tempArray[19] = (tempArray[19] + tempArray[20]);
            tempArray[20] = (tempArray[0] * tempArray[19]);
            tempArray[20] = (tempArray[18] + tempArray[20]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[20]);
            tempArray[19] = (-1 * tempArray[0] * tempArray[19]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[18] = (-1 * tempArray[0] * tempArray[18]);
            tempArray[19] = (tempArray[21] + tempArray[18]);
            tempArray[21] = (coefs2[1] * tempArray[7]);
            tempArray[22] = (-1 * coefs2[2] * tempArray[10]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[22] = (coefs1[2] * tempArray[12]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[21]);
            tempArray[22] = (coefs2[1] * tempArray[6]);
            tempArray[23] = (-1 * coefs2[2] * tempArray[9]);
            tempArray[22] = (tempArray[22] + tempArray[23]);
            tempArray[23] = (coefs1[2] * tempArray[11]);
            tempArray[22] = (tempArray[22] + tempArray[23]);
            tempArray[23] = (tempArray[0] * tempArray[22]);
            tempArray[23] = (tempArray[21] + tempArray[23]);
            tempArray[24] = (-1 * tempArray[0] * tempArray[23]);
            tempArray[22] = (-1 * tempArray[0] * tempArray[22]);
            tempArray[21] = (tempArray[21] + tempArray[22]);
            tempArray[21] = (-1 * tempArray[0] * tempArray[21]);
            tempArray[22] = (tempArray[24] + tempArray[21]);
            tempArray[24] = (-1 * coefs1[4] * coefs2[1]);
            tempArray[25] = (-1 * coefs1[5] * coefs2[2]);
            tempArray[24] = (tempArray[24] + tempArray[25]);
            tempArray[25] = (tempArray[3] * tempArray[12]);
            tempArray[24] = (tempArray[24] + tempArray[25]);
            tempArray[25] = (tempArray[11] * tempArray[2]);
            tempArray[24] = (tempArray[24] + tempArray[25]);
            tempArray[25] = (-1 * coefs1[9] * tempArray[0]);
            tempArray[13] = (tempArray[13] + tempArray[25]);
            tempArray[1] = (tempArray[1] + tempArray[25]);
            tempArray[25] = (-1 * coefs1[2] * coefs2[1]);
            tempArray[26] = (coefs1[5] * coefs2[3]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[26] = (tempArray[7] * tempArray[12]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[26] = (tempArray[11] * tempArray[6]);
            tempArray[25] = (tempArray[25] + tempArray[26]);
            tempArray[26] = (-1 * coefs1[8] * tempArray[0]);
            tempArray[15] = (tempArray[15] + tempArray[26]);
            tempArray[5] = (tempArray[5] + tempArray[26]);
            tempArray[26] = (-1 * coefs1[7] * tempArray[0]);
            tempArray[16] = (tempArray[16] + tempArray[26]);
            tempArray[27] = (-1 * coefs1[6] * coefs2[1]);
            tempArray[9] = (tempArray[9] * tempArray[12]);
            tempArray[9] = (tempArray[27] + tempArray[9]);
            tempArray[10] = (-1 * tempArray[10] * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[8] = (tempArray[8] + tempArray[26]);
            tempArray[10] = (tempArray[0] * tempArray[17]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (tempArray[0] * tempArray[20]);
            tempArray[10] = (tempArray[18] + tempArray[10]);
            tempArray[11] = (tempArray[0] * tempArray[23]);
            tempArray[11] = (tempArray[21] + tempArray[11]);
            tempArray[12] = (-1 * coefs1[4] * tempArray[24]);
            tempArray[17] = (-1 * coefs1[2] * tempArray[25]);
            tempArray[12] = (tempArray[12] + tempArray[17]);
            tempArray[9] = (-1 * coefs1[6] * tempArray[9]);
            tempArray[9] = (tempArray[12] + tempArray[9]);
            tempArray[12] = (coefs1[5] * coefs2[1]);
            tempArray[17] = (-1 * coefs1[4] * coefs2[2]);
            tempArray[12] = (tempArray[12] + tempArray[17]);
            tempArray[17] = (coefs1[2] * coefs2[3]);
            tempArray[12] = (tempArray[12] + tempArray[17]);
            tempArray[17] = (coefs1[5] * tempArray[12]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (-1 * tempArray[19] * tempArray[13]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (-1 * tempArray[1] * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (-1 * tempArray[22] * tempArray[15]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (-1 * tempArray[5] * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (tempArray[16] * tempArray[4]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (tempArray[14] * tempArray[8]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            c[1] = (-1 * tempArray[9]);
            tempArray[9] = (-1 * coefs2[2] * tempArray[3]);
            tempArray[17] = (coefs2[3] * tempArray[7]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[17] = (-1 * coefs1[5] * tempArray[12]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[17] = (-1 * coefs2[2] * tempArray[2]);
            tempArray[18] = (coefs2[3] * tempArray[6]);
            tempArray[17] = (tempArray[17] + tempArray[18]);
            tempArray[18] = (-1 * coefs1[5] * tempArray[11]);
            tempArray[17] = (tempArray[17] + tempArray[18]);
            tempArray[18] = (tempArray[0] * tempArray[17]);
            tempArray[18] = (tempArray[9] + tempArray[18]);
            tempArray[20] = (-1 * tempArray[0] * tempArray[18]);
            tempArray[17] = (-1 * tempArray[0] * tempArray[17]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[17] = (tempArray[20] + tempArray[9]);
            tempArray[20] = (coefs1[6] * coefs2[2]);
            tempArray[6] = (-1 * tempArray[6] * tempArray[12]);
            tempArray[6] = (tempArray[20] + tempArray[6]);
            tempArray[7] = (tempArray[7] * tempArray[11]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[2] * coefs2[2]);
            tempArray[20] = (-1 * coefs1[4] * coefs2[3]);
            tempArray[7] = (tempArray[7] + tempArray[20]);
            tempArray[20] = (-1 * tempArray[10] * tempArray[12]);
            tempArray[7] = (tempArray[7] + tempArray[20]);
            tempArray[20] = (-1 * tempArray[11] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[20]);
            tempArray[18] = (tempArray[0] * tempArray[18]);
            tempArray[9] = (tempArray[9] + tempArray[18]);
            tempArray[18] = (coefs1[5] * tempArray[24]);
            tempArray[6] = (coefs1[6] * tempArray[6]);
            tempArray[6] = (tempArray[18] + tempArray[6]);
            tempArray[18] = (-1 * coefs1[2] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[18] = (coefs1[4] * tempArray[12]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[18] = (tempArray[17] * tempArray[13]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[18] = (tempArray[1] * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[18] = (-1 * tempArray[15] * tempArray[4]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[18] = (-1 * tempArray[14] * tempArray[5]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[18] = (-1 * tempArray[22] * tempArray[16]);
            tempArray[6] = (tempArray[6] + tempArray[18]);
            tempArray[11] = (-1 * tempArray[8] * tempArray[11]);
            tempArray[6] = (tempArray[6] + tempArray[11]);
            c[2] = (-1 * tempArray[6]);
            tempArray[6] = (coefs1[6] * coefs2[3]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[12]);
            tempArray[2] = (tempArray[6] + tempArray[2]);
            tempArray[3] = (tempArray[3] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (coefs1[6] * tempArray[2]);
            tempArray[3] = (-1 * coefs1[5] * tempArray[25]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[4] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[12]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[13] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[1] = (-1 * tempArray[14] * tempArray[1]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[2] = (-1 * tempArray[17] * tempArray[15]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[5] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[19] * tempArray[16]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[8] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[3] = (-1 * tempArray[1]);
            tempArray[1] = (coefs1[6] * tempArray[14]);
            tempArray[2] = (-1 * coefs1[5] * tempArray[17]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[4] * tempArray[19]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[2] * tempArray[22]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[24] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[2] * tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[25] * tempArray[15]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[7] * tempArray[16]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[9] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * coefs1[6] * tempArray[4]);
            tempArray[3] = (-1 * coefs1[5] * tempArray[9]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[4] * tempArray[10]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[13] * tempArray[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[24] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[15] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[25] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[16] * tempArray[9]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[7] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[1] + tempArray[3]);
            c[0] = (-1 * tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            c[4] = (-1 * tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:50.3398874+02:00
            
            return c;
        }
        
        
    }
}
