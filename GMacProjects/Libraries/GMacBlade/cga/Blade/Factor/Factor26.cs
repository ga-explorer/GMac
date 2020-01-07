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
        private static cga0001Vector[] Factor26(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga0001Vector(),
                new cga0001Vector(),
                new cga0001Vector()
            };
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:17.5274424+02:00
            //Macro: geometry3d.cga.Factor3
            //Input Variables: 3 used, 10 not used, 13 total.
            //Temp Variables: 240 sub-expressions, 0 generated temps, 240 total.
            //Target Temp Variables: 17 total.
            //Output Variables: 15 total.
            //Computations: 1.25490196078431 average, 320 total.
            //Memory Reads: 1.97254901960784 average, 503 total.
            //Memory Writes: 255 total.
            //
            //Macro Binding Data: 
            //   B.#no^e1^e2# <=> <Variable> coefs[0]
            //   B.#no^e1^e3# <=> <Variable> coefs[1]
            //   B.#no^e2^e3# <=> <Variable> coefs[2]
            //   B.#e1^e2^e3# <=> <Variable> coefs[3]
            //   B.#no^e1^ni# <=> <Variable> coefs[4]
            //   B.#no^e2^ni# <=> <Variable> coefs[5]
            //   B.#e1^e2^ni# <=> <Variable> coefs[6]
            //   B.#no^e3^ni# <=> <Variable> coefs[7]
            //   B.#e1^e3^ni# <=> <Variable> coefs[8]
            //   B.#e2^e3^ni# <=> <Variable> coefs[9]
            //   inputVectors.f1.#e1# <=> <Constant> 1
            //   result.f1.#no# <=> <Variable> vectors[0].C1
            //   result.f1.#e1# <=> <Variable> vectors[0].C2
            //   result.f1.#e2# <=> <Variable> vectors[0].C3
            //   result.f1.#e3# <=> <Variable> vectors[0].C4
            //   result.f1.#ni# <=> <Variable> vectors[0].C5
            //   inputVectors.f2.#e3# <=> <Constant> 1
            //   result.f2.#no# <=> <Variable> vectors[1].C1
            //   result.f2.#e1# <=> <Variable> vectors[1].C2
            //   result.f2.#e2# <=> <Variable> vectors[1].C3
            //   result.f2.#e3# <=> <Variable> vectors[1].C4
            //   result.f2.#ni# <=> <Variable> vectors[1].C5
            //   inputVectors.f3.#ni# <=> <Constant> 1
            //   result.f3.#no# <=> <Variable> vectors[2].C1
            //   result.f3.#e1# <=> <Variable> vectors[2].C2
            //   result.f3.#e2# <=> <Variable> vectors[2].C3
            //   result.f3.#e3# <=> <Variable> vectors[2].C4
            //   result.f3.#ni# <=> <Variable> vectors[2].C5
            
            double[] tempArray = new double[17];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (-1 * coefs[2] * tempArray[0]);
            tempArray[2] = (-1 * coefs[9] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (-1 * coefs[1] * tempArray[0]);
            tempArray[4] = (-1 * coefs[8] * tempArray[0]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[5] = (tempArray[0] * tempArray[3]);
            tempArray[6] = (coefs[1] * tempArray[0]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[7] = (tempArray[0] * tempArray[5]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[6] = (tempArray[6] + tempArray[8]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[7] + tempArray[6]);
            tempArray[8] = (coefs[2] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[8]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[5] = (tempArray[6] + tempArray[5]);
            tempArray[6] = (-1 * coefs[4] * coefs[5]);
            tempArray[5] = (tempArray[1] * tempArray[5]);
            tempArray[5] = (tempArray[6] + tempArray[5]);
            tempArray[6] = (tempArray[7] * tempArray[2]);
            vectors[0].C3 = (tempArray[5] + tempArray[6]);
            tempArray[5] = (-1 * coefs[0] * tempArray[0]);
            tempArray[6] = (-1 * coefs[6] * tempArray[0]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[7] = (tempArray[0] * tempArray[5]);
            tempArray[8] = (coefs[0] * tempArray[0]);
            tempArray[6] = (tempArray[6] + tempArray[8]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[9] = (tempArray[0] * tempArray[7]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            tempArray[9] = (tempArray[9] + tempArray[8]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[7] = (tempArray[8] + tempArray[7]);
            tempArray[8] = (-1 * coefs[4] * coefs[7]);
            tempArray[7] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[7] = (tempArray[8] + tempArray[7]);
            tempArray[8] = (-1 * tempArray[9] * tempArray[2]);
            vectors[0].C4 = (tempArray[7] + tempArray[8]);
            tempArray[7] = (coefs[3] * tempArray[1]);
            tempArray[8] = (-1 * coefs[7] * tempArray[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * coefs[5] * tempArray[9]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            tempArray[8] = (coefs[3] * tempArray[2]);
            tempArray[9] = (coefs[7] * tempArray[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (coefs[5] * tempArray[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[8]);
            vectors[0].C1 = (tempArray[7] + tempArray[9]);
            tempArray[9] = Math.Pow(coefs[3], 2);
            tempArray[10] = Math.Pow(coefs[4], 2);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[3] * tempArray[5]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[4] * tempArray[7]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[5] * tempArray[7]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * tempArray[6] * tempArray[9]);
            vectors[0].C2 = (tempArray[9] + tempArray[10]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            vectors[0].C5 = (tempArray[7] + tempArray[8]);
            tempArray[7] = (tempArray[9] + tempArray[10]);
            tempArray[8] = (coefs[3] * tempArray[7]);
            tempArray[9] = (tempArray[7] + tempArray[9]);
            tempArray[10] = (tempArray[0] * tempArray[9]);
            tempArray[11] = (tempArray[7] + tempArray[8]);
            tempArray[11] = (tempArray[0] * tempArray[11]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[12] = (-1 * tempArray[1] * tempArray[10]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[9] = (tempArray[11] + tempArray[9]);
            tempArray[11] = (-1 * tempArray[2] * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[11]);
            tempArray[11] = (tempArray[7] + tempArray[8]);
            tempArray[12] = (tempArray[5] + tempArray[6]);
            tempArray[13] = (-1 * tempArray[3] * tempArray[11]);
            tempArray[14] = (-1 * tempArray[5] * tempArray[12]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (coefs[4] * tempArray[9]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[0] * tempArray[13]);
            tempArray[15] = (-1 * tempArray[4] * tempArray[11]);
            tempArray[16] = (-1 * tempArray[6] * tempArray[12]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[16] = (-1 * coefs[4] * tempArray[10]);
            tempArray[15] = (tempArray[15] + tempArray[16]);
            tempArray[15] = (tempArray[0] * tempArray[15]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[16] = (-1 * tempArray[0] * tempArray[14]);
            tempArray[13] = (tempArray[0] * tempArray[13]);
            tempArray[13] = (tempArray[15] + tempArray[13]);
            tempArray[13] = (tempArray[0] * tempArray[13]);
            tempArray[15] = (tempArray[16] + tempArray[13]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[12]);
            tempArray[3] = (-1 * tempArray[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * coefs[7] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[12]);
            tempArray[4] = (-1 * tempArray[4] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (coefs[7] * tempArray[10]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[2]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[4] + tempArray[1]);
            tempArray[4] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[3] = (tempArray[4] + tempArray[3]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[12] = (tempArray[0] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[12]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[7] = (tempArray[7] + tempArray[4]);
            tempArray[12] = (tempArray[0] * tempArray[14]);
            tempArray[12] = (tempArray[13] + tempArray[12]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[3] = (tempArray[4] + tempArray[3]);
            tempArray[4] = (-1 * coefs[3] * tempArray[11]);
            tempArray[5] = (tempArray[5] * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (tempArray[6] * tempArray[9]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[8] * tempArray[4]);
            tempArray[6] = (tempArray[15] * tempArray[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[7] * tempArray[12]);
            vectors[1].C2 = (tempArray[5] + tempArray[6]);
            tempArray[5] = (tempArray[1] * tempArray[11]);
            tempArray[6] = (-1 * tempArray[5] * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * coefs[5] * tempArray[9]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[9] = (tempArray[2] * tempArray[11]);
            tempArray[10] = (-1 * tempArray[6] * tempArray[7]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (coefs[5] * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[9] = (tempArray[0] * tempArray[9]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[5] = (tempArray[9] + tempArray[5]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[9] = (tempArray[10] + tempArray[5]);
            tempArray[10] = (coefs[3] * tempArray[12]);
            tempArray[11] = (tempArray[3] * tempArray[10]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (tempArray[4] * tempArray[9]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[10] * tempArray[4]);
            tempArray[11] = (-1 * tempArray[9] * tempArray[3]);
            tempArray[6] = (tempArray[6] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[7] * tempArray[5]);
            vectors[1].C3 = (tempArray[6] + tempArray[11]);
            tempArray[6] = Math.Pow(tempArray[8], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[11] = Math.Pow(tempArray[10], 2);
            tempArray[6] = (tempArray[6] + tempArray[11]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[6] + tempArray[3]);
            tempArray[6] = (-1 * tempArray[1] * tempArray[7]);
            vectors[1].C4 = (tempArray[3] + tempArray[6]);
            tempArray[3] = (-1 * tempArray[8] * tempArray[9]);
            tempArray[6] = (tempArray[10] * tempArray[15]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (coefs[7] * tempArray[11]);
            tempArray[7] = (coefs[5] * tempArray[12]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (coefs[4] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (tempArray[6] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[7]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[7] = (-1 * tempArray[8] * tempArray[5]);
            tempArray[11] = (tempArray[10] * tempArray[12]);
            tempArray[7] = (tempArray[7] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[6] * tempArray[3]);
            tempArray[7] = (tempArray[7] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[7]);
            vectors[1].C1 = (tempArray[3] + tempArray[11]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            vectors[1].C5 = (tempArray[3] + tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[7] = (tempArray[6] + tempArray[11]);
            tempArray[11] = (tempArray[3] + tempArray[11]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[11]);
            tempArray[14] = (tempArray[3] + tempArray[7]);
            tempArray[14] = (tempArray[0] * tempArray[14]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[11] = (tempArray[0] * tempArray[11]);
            tempArray[11] = (tempArray[14] + tempArray[11]);
            tempArray[14] = (tempArray[10] * tempArray[3]);
            tempArray[16] = (tempArray[4] * tempArray[7]);
            tempArray[14] = (tempArray[14] + tempArray[16]);
            tempArray[15] = (tempArray[15] * tempArray[11]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[12] = (tempArray[12] * tempArray[13]);
            vectors[2].C2 = (tempArray[14] + tempArray[12]);
            tempArray[12] = (tempArray[5] + tempArray[6]);
            tempArray[3] = (-1 * tempArray[8] * tempArray[3]);
            tempArray[4] = (tempArray[4] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[9] * tempArray[11]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[5] * tempArray[13]);
            vectors[2].C3 = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[8] * tempArray[7]);
            tempArray[4] = (tempArray[10] * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[11]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[13]);
            vectors[2].C4 = (tempArray[2] + tempArray[1]);
            tempArray[1] = (tempArray[2] * tempArray[3]);
            tempArray[2] = (tempArray[9] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[15] * tempArray[12]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[6] * tempArray[13]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[1] * tempArray[3]);
            tempArray[3] = (tempArray[5] * tempArray[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[12] * tempArray[12]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[6] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            vectors[2].C1 = (tempArray[1] + tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            vectors[2].C5 = (tempArray[1] + tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:17.5544440+02:00
            
        
            return vectors;
        }
        
        
    }
}
