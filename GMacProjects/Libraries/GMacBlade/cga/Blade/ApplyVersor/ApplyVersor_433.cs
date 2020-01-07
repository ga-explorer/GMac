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
        private static double[] ApplyVersor_433(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:25.7134788+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 266 sub-expressions, 0 generated temps, 266 total.
            //Target Temp Variables: 17 total.
            //Output Variables: 10 total.
            //Computations: 1.28985507246377 average, 356 total.
            //Memory Reads: 1.95289855072464 average, 539 total.
            //Memory Writes: 276 total.
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
            //   v.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   v.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   v.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   v.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   v.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
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
            
            double[] tempArray = new double[17];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs1[0] * tempArray[0]);
            tempArray[2] = (-1 * coefs1[4] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (coefs2[1] * tempArray[0]);
            tempArray[4] = (-1 * coefs2[8] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[5] = (coefs2[0] * tempArray[0]);
            tempArray[6] = (-1 * coefs2[6] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[7] = (-1 * coefs1[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[7]);
            tempArray[7] = (-1 * coefs2[1] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[7]);
            tempArray[7] = (-1 * coefs2[0] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs2[4] * tempArray[2]);
            tempArray[8] = (-1 * coefs1[1] * tempArray[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (coefs1[2] * tempArray[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[8] = (coefs2[4] * tempArray[1]);
            tempArray[9] = (coefs1[1] * tempArray[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * coefs1[2] * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (tempArray[0] * tempArray[8]);
            tempArray[9] = (tempArray[7] + tempArray[9]);
            tempArray[10] = (tempArray[0] * tempArray[9]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[8]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[8] = (tempArray[10] + tempArray[7]);
            tempArray[10] = (-1 * coefs1[2] * coefs2[4]);
            tempArray[11] = (-1 * coefs1[3] * coefs2[5]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[5] * tempArray[2]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[9]);
            tempArray[9] = Math.Pow(coefs1[1], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[11] = Math.Pow(coefs1[2], 2);
            tempArray[11] = (-1 * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = Math.Pow(coefs1[3], 2);
            tempArray[11] = (-1 * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = Math.Pow(tempArray[2], 2);
            tempArray[11] = (-1 * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[11] = Math.Pow(tempArray[1], 2);
            tempArray[11] = (-1 * tempArray[11]);
            tempArray[9] = (tempArray[9] + tempArray[11]);
            tempArray[9] = Math.Pow(tempArray[9], -1);
            tempArray[11] = (-1 * coefs1[1] * coefs2[4]);
            tempArray[12] = (coefs1[3] * coefs2[7]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (tempArray[1] * tempArray[4]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (tempArray[3] * tempArray[2]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (coefs1[2] * tempArray[10]);
            tempArray[13] = (coefs1[1] * tempArray[11]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (coefs1[3] * coefs2[4]);
            tempArray[14] = (-1 * coefs1[2] * coefs2[5]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (coefs1[1] * coefs2[7]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * coefs1[3] * tempArray[13]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[8] = (tempArray[1] * tempArray[8]);
            tempArray[8] = (tempArray[12] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[2] * tempArray[7]);
            tempArray[7] = (tempArray[8] + tempArray[7]);
            c[4] = (-1 * tempArray[9] * tempArray[7]);
            tempArray[7] = (coefs2[2] * tempArray[0]);
            tempArray[8] = (-1 * coefs2[9] * tempArray[0]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[12] = (-1 * coefs2[2] * tempArray[0]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[12] = (-1 * coefs2[5] * tempArray[2]);
            tempArray[14] = (-1 * coefs1[1] * tempArray[7]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (coefs1[3] * tempArray[5]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[12] = (-1 * tempArray[0] * tempArray[12]);
            tempArray[14] = (coefs2[5] * tempArray[1]);
            tempArray[15] = (coefs1[1] * tempArray[8]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[3] * tempArray[6]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (tempArray[0] * tempArray[14]);
            tempArray[15] = (tempArray[12] + tempArray[15]);
            tempArray[16] = (tempArray[0] * tempArray[15]);
            tempArray[14] = (-1 * tempArray[0] * tempArray[14]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[12] = (-1 * tempArray[0] * tempArray[12]);
            tempArray[14] = (tempArray[16] + tempArray[12]);
            tempArray[15] = (-1 * tempArray[0] * tempArray[15]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (-1 * coefs1[1] * coefs2[5]);
            tempArray[16] = (-1 * coefs1[2] * coefs2[7]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (tempArray[1] * tempArray[8]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (tempArray[7] * tempArray[2]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[10] = (-1 * coefs1[3] * tempArray[10]);
            tempArray[16] = (coefs1[1] * tempArray[15]);
            tempArray[10] = (tempArray[10] + tempArray[16]);
            tempArray[16] = (-1 * coefs1[2] * tempArray[13]);
            tempArray[10] = (tempArray[10] + tempArray[16]);
            tempArray[14] = (tempArray[1] * tempArray[14]);
            tempArray[10] = (tempArray[10] + tempArray[14]);
            tempArray[12] = (-1 * tempArray[2] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            c[5] = (-1 * tempArray[9] * tempArray[10]);
            tempArray[10] = (coefs2[7] * tempArray[2]);
            tempArray[12] = (coefs1[2] * tempArray[7]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[12] = (-1 * coefs1[3] * tempArray[3]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[12] = (-1 * coefs2[7] * tempArray[1]);
            tempArray[14] = (-1 * coefs1[2] * tempArray[8]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (coefs1[3] * tempArray[4]);
            tempArray[12] = (tempArray[12] + tempArray[14]);
            tempArray[14] = (tempArray[0] * tempArray[12]);
            tempArray[14] = (tempArray[10] + tempArray[14]);
            tempArray[16] = (tempArray[0] * tempArray[14]);
            tempArray[12] = (-1 * tempArray[0] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[12] = (tempArray[16] + tempArray[10]);
            tempArray[14] = (-1 * tempArray[0] * tempArray[14]);
            tempArray[10] = (tempArray[10] + tempArray[14]);
            tempArray[11] = (coefs1[3] * tempArray[11]);
            tempArray[14] = (coefs1[2] * tempArray[15]);
            tempArray[11] = (tempArray[11] + tempArray[14]);
            tempArray[13] = (coefs1[1] * tempArray[13]);
            tempArray[11] = (tempArray[11] + tempArray[13]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[12]);
            tempArray[1] = (tempArray[11] + tempArray[1]);
            tempArray[2] = (tempArray[2] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[7] = (-1 * tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * coefs2[3] * tempArray[1]);
            tempArray[2] = (coefs1[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (coefs1[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * coefs2[3] * tempArray[2]);
            tempArray[10] = (-1 * coefs1[3] * tempArray[8]);
            tempArray[2] = (tempArray[2] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[2] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[10]);
            tempArray[10] = (-1 * coefs1[1] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[10] = (tempArray[1] + tempArray[10]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[11] + tempArray[1]);
            tempArray[11] = (-1 * coefs1[1] * coefs2[3]);
            tempArray[6] = (tempArray[2] * tempArray[6]);
            tempArray[6] = (tempArray[11] + tempArray[6]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[5] = (tempArray[6] + tempArray[5]);
            tempArray[6] = (tempArray[0] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (coefs1[2] * coefs2[3]);
            tempArray[4] = (-1 * tempArray[2] * tempArray[4]);
            tempArray[4] = (tempArray[6] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[3]);
            tempArray[3] = (tempArray[4] + tempArray[3]);
            tempArray[4] = (coefs1[3] * coefs2[3]);
            tempArray[6] = (-1 * tempArray[2] * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (tempArray[1] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[5] = (-1 * coefs1[1] * tempArray[5]);
            tempArray[3] = (coefs1[2] * tempArray[3]);
            tempArray[3] = (tempArray[5] + tempArray[3]);
            tempArray[4] = (-1 * coefs1[3] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[1] = (tempArray[1] * tempArray[1]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[2] = (tempArray[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[3] = (-1 * tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * coefs1[1] * tempArray[2]);
            tempArray[2] = (coefs1[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * coefs1[3] * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[1] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[2] = (coefs1[1] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs1[3] * tempArray[12]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[1] * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[10]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[1] + tempArray[3]);
            c[0] = (tempArray[3] * tempArray[9]);
            tempArray[3] = (-1 * coefs1[2] * tempArray[2]);
            tempArray[4] = (-1 * coefs1[1] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (coefs1[3] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[1] * tempArray[11]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[4] = (coefs1[2] * tempArray[1]);
            tempArray[5] = (coefs1[1] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * coefs1[3] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[1] * tempArray[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[2] * tempArray[11]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[4]);
            tempArray[5] = (tempArray[3] + tempArray[5]);
            c[1] = (tempArray[9] * tempArray[5]);
            tempArray[5] = (coefs1[3] * tempArray[2]);
            tempArray[6] = (-1 * coefs1[1] * tempArray[14]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (coefs1[2] * tempArray[12]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[1] * tempArray[15]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[2] * tempArray[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[6] = (-1 * coefs1[3] * tempArray[1]);
            tempArray[7] = (coefs1[1] * tempArray[12]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * coefs1[2] * tempArray[10]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[2] * tempArray[15]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[5] + tempArray[7]);
            c[2] = (tempArray[9] * tempArray[7]);
            tempArray[2] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            c[6] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            c[8] = (tempArray[9] * tempArray[1]);
            tempArray[0] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[0] = (tempArray[5] + tempArray[0]);
            c[9] = (tempArray[9] * tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:25.7374802+02:00
            
            return c;
        }
        
        
    }
}
