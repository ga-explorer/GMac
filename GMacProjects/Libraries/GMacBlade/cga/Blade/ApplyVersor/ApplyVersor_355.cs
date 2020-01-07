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
        private static double[] ApplyVersor_355(double[] coefs1, double[] coefs2)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:23.0193247+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 108 sub-expressions, 0 generated temps, 108 total.
            //Target Temp Variables: 18 total.
            //Output Variables: 1 total.
            //Computations: 1.20183486238532 average, 131 total.
            //Memory Reads: 1.89908256880734 average, 207 total.
            //Memory Writes: 109 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> c[0]
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
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs2[0]
            
            double[] tempArray = new double[18];
            
            tempArray[0] = (-1 * coefs1[3] * coefs2[0]);
            tempArray[0] = (coefs1[3] * tempArray[0]);
            tempArray[1] = (-1 * coefs1[4] * coefs2[0]);
            tempArray[1] = (coefs1[4] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * coefs1[5] * coefs2[0]);
            tempArray[1] = (-1 * coefs1[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (coefs1[7] * coefs2[0]);
            tempArray[1] = (coefs1[7] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(2, -0.5);
            tempArray[2] = (coefs1[2] * tempArray[1]);
            tempArray[3] = (-1 * coefs1[9] * tempArray[1]);
            tempArray[4] = (tempArray[2] + tempArray[3]);
            tempArray[5] = (coefs2[0] * tempArray[4]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[5]);
            tempArray[7] = (-1 * coefs1[2] * tempArray[1]);
            tempArray[3] = (tempArray[3] + tempArray[7]);
            tempArray[8] = (-1 * coefs2[0] * tempArray[3]);
            tempArray[8] = (tempArray[1] * tempArray[8]);
            tempArray[6] = (tempArray[6] + tempArray[8]);
            tempArray[9] = (tempArray[1] * tempArray[6]);
            tempArray[5] = (tempArray[1] * tempArray[5]);
            tempArray[5] = (tempArray[8] + tempArray[5]);
            tempArray[5] = (tempArray[1] * tempArray[5]);
            tempArray[8] = (tempArray[9] + tempArray[5]);
            tempArray[9] = (coefs1[9] * tempArray[1]);
            tempArray[2] = (tempArray[2] + tempArray[9]);
            tempArray[8] = (tempArray[8] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[7] + tempArray[9]);
            tempArray[5] = (-1 * tempArray[5] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[5] = (coefs1[1] * tempArray[1]);
            tempArray[7] = (-1 * coefs1[8] * tempArray[1]);
            tempArray[8] = (tempArray[5] + tempArray[7]);
            tempArray[9] = (coefs2[0] * tempArray[8]);
            tempArray[10] = (-1 * tempArray[1] * tempArray[9]);
            tempArray[11] = (-1 * coefs1[1] * tempArray[1]);
            tempArray[7] = (tempArray[7] + tempArray[11]);
            tempArray[12] = (-1 * coefs2[0] * tempArray[7]);
            tempArray[12] = (tempArray[1] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[13] = (tempArray[1] * tempArray[10]);
            tempArray[9] = (tempArray[1] * tempArray[9]);
            tempArray[9] = (tempArray[12] + tempArray[9]);
            tempArray[9] = (tempArray[1] * tempArray[9]);
            tempArray[12] = (tempArray[13] + tempArray[9]);
            tempArray[13] = (coefs1[8] * tempArray[1]);
            tempArray[5] = (tempArray[5] + tempArray[13]);
            tempArray[12] = (-1 * tempArray[12] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[12]);
            tempArray[10] = (-1 * tempArray[1] * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (tempArray[11] + tempArray[13]);
            tempArray[9] = (tempArray[9] * tempArray[10]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[9] = (coefs1[0] * tempArray[1]);
            tempArray[11] = (-1 * coefs1[6] * tempArray[1]);
            tempArray[12] = (tempArray[9] + tempArray[11]);
            tempArray[13] = (-1 * coefs2[0] * tempArray[12]);
            tempArray[14] = (-1 * tempArray[1] * tempArray[13]);
            tempArray[15] = (-1 * coefs1[0] * tempArray[1]);
            tempArray[11] = (tempArray[11] + tempArray[15]);
            tempArray[16] = (coefs2[0] * tempArray[11]);
            tempArray[16] = (tempArray[1] * tempArray[16]);
            tempArray[14] = (tempArray[14] + tempArray[16]);
            tempArray[17] = (tempArray[1] * tempArray[14]);
            tempArray[13] = (tempArray[1] * tempArray[13]);
            tempArray[13] = (tempArray[16] + tempArray[13]);
            tempArray[13] = (tempArray[1] * tempArray[13]);
            tempArray[16] = (tempArray[17] + tempArray[13]);
            tempArray[17] = (coefs1[6] * tempArray[1]);
            tempArray[9] = (tempArray[9] + tempArray[17]);
            tempArray[16] = (tempArray[16] * tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[16]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[14]);
            tempArray[1] = (tempArray[13] + tempArray[1]);
            tempArray[13] = (tempArray[15] + tempArray[17]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[13]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(coefs1[3], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[14] = Math.Pow(coefs1[4], 2);
            tempArray[14] = (-1 * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[14] = Math.Pow(coefs1[5], 2);
            tempArray[14] = (-1 * tempArray[14]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[12] = (tempArray[12] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[9] = (tempArray[11] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = Math.Pow(coefs1[7], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[8] = (tempArray[8] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[5] = (tempArray[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[4] = (tempArray[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[2] = (tempArray[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = Math.Pow(tempArray[1], -1);
            c[0] = (tempArray[0] * tempArray[1]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:23.0293253+02:00
            
            return c;
        }
        
        
    }
}
