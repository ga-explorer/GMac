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
        private static cga0001Vector[] Factor27(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga0001Vector(),
                new cga0001Vector(),
                new cga0001Vector(),
                new cga0001Vector()
            };
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:21.7266826+02:00
            //Macro: geometry3d.cga.Factor4
            //Input Variables: 4 used, 5 not used, 9 total.
            //Temp Variables: 329 sub-expressions, 0 generated temps, 329 total.
            //Target Temp Variables: 21 total.
            //Output Variables: 20 total.
            //Computations: 1.28939828080229 average, 450 total.
            //Memory Reads: 1.98853868194842 average, 694 total.
            //Memory Writes: 349 total.
            //
            //Macro Binding Data: 
            //   B.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   B.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   B.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   B.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   B.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            //   inputVectors.f1.#no# <=> <Constant> 1
            //   result.f1.#no# <=> <Variable> vectors[0].C1
            //   result.f1.#e1# <=> <Variable> vectors[0].C2
            //   result.f1.#e2# <=> <Variable> vectors[0].C3
            //   result.f1.#e3# <=> <Variable> vectors[0].C4
            //   result.f1.#ni# <=> <Variable> vectors[0].C5
            //   inputVectors.f2.#e1# <=> <Constant> 1
            //   result.f2.#no# <=> <Variable> vectors[1].C1
            //   result.f2.#e1# <=> <Variable> vectors[1].C2
            //   result.f2.#e2# <=> <Variable> vectors[1].C3
            //   result.f2.#e3# <=> <Variable> vectors[1].C4
            //   result.f2.#ni# <=> <Variable> vectors[1].C5
            //   inputVectors.f3.#e3# <=> <Constant> 1
            //   result.f3.#no# <=> <Variable> vectors[2].C1
            //   result.f3.#e1# <=> <Variable> vectors[2].C2
            //   result.f3.#e2# <=> <Variable> vectors[2].C3
            //   result.f3.#e3# <=> <Variable> vectors[2].C4
            //   result.f3.#ni# <=> <Variable> vectors[2].C5
            //   inputVectors.f4.#ni# <=> <Constant> 1
            //   result.f4.#no# <=> <Variable> vectors[3].C1
            //   result.f4.#e1# <=> <Variable> vectors[3].C2
            //   result.f4.#e2# <=> <Variable> vectors[3].C3
            //   result.f4.#e3# <=> <Variable> vectors[3].C4
            //   result.f4.#ni# <=> <Variable> vectors[3].C5
            
            double[] tempArray = new double[21];
            
            tempArray[0] = Math.Pow(2, -0.5);
            tempArray[1] = (coefs[0] * tempArray[0]);
            tempArray[2] = (-1 * coefs[4] * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[3] = (-1 * coefs[0] * tempArray[0]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (coefs[3] * tempArray[0] * tempArray[1]);
            tempArray[4] = (coefs[3] * tempArray[0] * tempArray[2]);
            vectors[0].C2 = (tempArray[3] + tempArray[4]);
            tempArray[3] = (-1 * coefs[2] * tempArray[0] * tempArray[1]);
            tempArray[4] = (-1 * coefs[2] * tempArray[0] * tempArray[2]);
            vectors[0].C3 = (tempArray[3] + tempArray[4]);
            tempArray[3] = (coefs[1] * tempArray[0] * tempArray[1]);
            tempArray[4] = (coefs[1] * tempArray[0] * tempArray[2]);
            vectors[0].C4 = (tempArray[3] + tempArray[4]);
            tempArray[3] = Math.Pow(coefs[1], 2);
            tempArray[4] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[5] = Math.Pow(coefs[2], 2);
            tempArray[6] = (-1 * tempArray[0] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = Math.Pow(coefs[3], 2);
            tempArray[7] = (tempArray[0] * tempArray[6]);
            tempArray[4] = (tempArray[4] + tempArray[7]);
            tempArray[7] = (tempArray[0] * tempArray[1]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * tempArray[1] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[2] * tempArray[7]);
            tempArray[3] = (tempArray[3] + tempArray[5]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[3]);
            vectors[0].C1 = (tempArray[4] + tempArray[5]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            vectors[0].C5 = (tempArray[4] + tempArray[3]);
            tempArray[3] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (tempArray[0] * tempArray[3]);
            tempArray[5] = (tempArray[4] + tempArray[3]);
            tempArray[5] = (tempArray[0] * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[3] = (tempArray[5] + tempArray[3]);
            tempArray[5] = (tempArray[3] + tempArray[4]);
            tempArray[6] = (tempArray[1] * tempArray[5]);
            tempArray[7] = (coefs[3] * tempArray[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[2] * tempArray[5]);
            tempArray[8] = (-1 * coefs[3] * tempArray[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (tempArray[0] * tempArray[7]);
            tempArray[8] = (tempArray[6] + tempArray[8]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[8]);
            tempArray[7] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[9] + tempArray[6]);
            tempArray[9] = (tempArray[3] + tempArray[4]);
            tempArray[10] = (tempArray[1] * tempArray[9]);
            tempArray[11] = (-1 * coefs[2] * tempArray[3]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[11] = (tempArray[2] * tempArray[9]);
            tempArray[12] = (coefs[2] * tempArray[4]);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (tempArray[0] * tempArray[11]);
            tempArray[12] = (tempArray[10] + tempArray[12]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[12]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[11]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[11] = (tempArray[13] + tempArray[10]);
            tempArray[13] = (tempArray[0] * tempArray[11]);
            tempArray[12] = (tempArray[0] * tempArray[12]);
            tempArray[10] = (tempArray[10] + tempArray[12]);
            tempArray[12] = (-1 * tempArray[0] * tempArray[10]);
            tempArray[13] = (tempArray[13] + tempArray[12]);
            tempArray[14] = (tempArray[0] * tempArray[13]);
            tempArray[15] = (-1 * tempArray[0] * tempArray[11]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[12] = (tempArray[0] * tempArray[12]);
            tempArray[14] = (tempArray[14] + tempArray[12]);
            tempArray[15] = (tempArray[3] + tempArray[4]);
            tempArray[16] = (-1 * coefs[3] * tempArray[15]);
            tempArray[5] = (coefs[1] * tempArray[5]);
            tempArray[5] = (tempArray[16] + tempArray[5]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            tempArray[6] = (tempArray[6] + tempArray[8]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[13]);
            tempArray[8] = (tempArray[12] + tempArray[8]);
            tempArray[12] = (coefs[2] * tempArray[15]);
            tempArray[9] = (coefs[1] * tempArray[9]);
            tempArray[9] = (tempArray[12] + tempArray[9]);
            tempArray[12] = (-1 * tempArray[5] * tempArray[9]);
            tempArray[8] = (tempArray[7] * tempArray[8]);
            tempArray[8] = (tempArray[12] + tempArray[8]);
            tempArray[12] = (tempArray[14] * tempArray[6]);
            vectors[1].C3 = (tempArray[8] + tempArray[12]);
            tempArray[8] = (coefs[3] * tempArray[9]);
            tempArray[12] = (coefs[2] * tempArray[5]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[15]);
            tempArray[3] = (-1 * coefs[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[15]);
            tempArray[3] = (coefs[1] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[1] + tempArray[3]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[2] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[4] + tempArray[1]);
            tempArray[4] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[1]);
            tempArray[4] = (tempArray[4] + tempArray[3]);
            tempArray[12] = (tempArray[0] * tempArray[4]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[3] = (tempArray[3] + tempArray[13]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[12] = (tempArray[12] + tempArray[3]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[8] * tempArray[9]);
            tempArray[3] = (-1 * tempArray[7] * tempArray[3]);
            tempArray[3] = (tempArray[4] + tempArray[3]);
            tempArray[4] = (-1 * tempArray[12] * tempArray[6]);
            vectors[1].C4 = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[4]);
            tempArray[4] = (tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * tempArray[3] * tempArray[7]);
            tempArray[12] = (tempArray[8] * tempArray[14]);
            tempArray[4] = (tempArray[4] + tempArray[12]);
            tempArray[12] = (tempArray[5] * tempArray[12]);
            tempArray[4] = (tempArray[4] + tempArray[12]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[12] = (-1 * tempArray[3] * tempArray[6]);
            tempArray[13] = (-1 * tempArray[8] * tempArray[8]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * tempArray[5] * tempArray[3]);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[12]);
            vectors[1].C1 = (tempArray[4] + tempArray[13]);
            tempArray[13] = Math.Pow(tempArray[9], 2);
            tempArray[14] = Math.Pow(tempArray[3], 2);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[11] * tempArray[8]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[10] * tempArray[14]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[2] * tempArray[3]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[1] * tempArray[12]);
            vectors[1].C2 = (tempArray[13] + tempArray[14]);
            tempArray[12] = (tempArray[0] * tempArray[12]);
            vectors[1].C5 = (tempArray[4] + tempArray[12]);
            tempArray[4] = (tempArray[13] + tempArray[14]);
            tempArray[12] = (-1 * tempArray[3] * tempArray[4]);
            tempArray[13] = (tempArray[4] + tempArray[13]);
            tempArray[14] = (tempArray[0] * tempArray[13]);
            tempArray[15] = (tempArray[4] + tempArray[12]);
            tempArray[15] = (tempArray[0] * tempArray[15]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[16] = (-1 * tempArray[7] * tempArray[14]);
            tempArray[12] = (tempArray[12] + tempArray[16]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[13]);
            tempArray[13] = (tempArray[15] + tempArray[13]);
            tempArray[15] = (-1 * tempArray[6] * tempArray[13]);
            tempArray[12] = (tempArray[12] + tempArray[15]);
            tempArray[15] = (tempArray[3] + tempArray[4]);
            tempArray[16] = (tempArray[8] + tempArray[12]);
            tempArray[17] = (-1 * tempArray[11] * tempArray[15]);
            tempArray[18] = (-1 * tempArray[2] * tempArray[16]);
            tempArray[17] = (tempArray[17] + tempArray[18]);
            tempArray[18] = (-1 * tempArray[9] * tempArray[13]);
            tempArray[17] = (tempArray[17] + tempArray[18]);
            tempArray[18] = (-1 * tempArray[0] * tempArray[17]);
            tempArray[19] = (-1 * tempArray[10] * tempArray[15]);
            tempArray[20] = (-1 * tempArray[1] * tempArray[16]);
            tempArray[19] = (tempArray[19] + tempArray[20]);
            tempArray[20] = (tempArray[9] * tempArray[14]);
            tempArray[19] = (tempArray[19] + tempArray[20]);
            tempArray[19] = (tempArray[0] * tempArray[19]);
            tempArray[18] = (tempArray[18] + tempArray[19]);
            tempArray[20] = (-1 * tempArray[0] * tempArray[18]);
            tempArray[17] = (tempArray[0] * tempArray[17]);
            tempArray[17] = (tempArray[19] + tempArray[17]);
            tempArray[17] = (tempArray[0] * tempArray[17]);
            tempArray[19] = (tempArray[20] + tempArray[17]);
            tempArray[7] = (-1 * tempArray[7] * tempArray[16]);
            tempArray[11] = (-1 * tempArray[11] * tempArray[4]);
            tempArray[7] = (tempArray[7] + tempArray[11]);
            tempArray[11] = (tempArray[8] * tempArray[13]);
            tempArray[7] = (tempArray[7] + tempArray[11]);
            tempArray[11] = (-1 * tempArray[0] * tempArray[7]);
            tempArray[6] = (-1 * tempArray[6] * tempArray[16]);
            tempArray[4] = (-1 * tempArray[10] * tempArray[4]);
            tempArray[4] = (tempArray[6] + tempArray[4]);
            tempArray[6] = (-1 * tempArray[8] * tempArray[14]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[6] = (tempArray[11] + tempArray[4]);
            tempArray[8] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[7] = (tempArray[0] * tempArray[7]);
            tempArray[4] = (tempArray[4] + tempArray[7]);
            tempArray[4] = (tempArray[0] * tempArray[4]);
            tempArray[7] = (tempArray[8] + tempArray[4]);
            tempArray[8] = (tempArray[0] * tempArray[7]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[0] * tempArray[4]);
            tempArray[6] = (tempArray[8] + tempArray[6]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[6]);
            tempArray[11] = (tempArray[0] * tempArray[4]);
            tempArray[8] = (tempArray[8] + tempArray[11]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            tempArray[10] = (tempArray[10] + tempArray[8]);
            tempArray[11] = (tempArray[0] * tempArray[18]);
            tempArray[11] = (tempArray[17] + tempArray[11]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            tempArray[6] = (tempArray[8] + tempArray[6]);
            tempArray[3] = (tempArray[3] * tempArray[15]);
            tempArray[2] = (tempArray[2] * tempArray[14]);
            tempArray[2] = (tempArray[3] + tempArray[2]);
            tempArray[1] = (tempArray[1] * tempArray[13]);
            tempArray[1] = (tempArray[2] + tempArray[1]);
            tempArray[2] = (-1 * tempArray[12] * tempArray[1]);
            tempArray[3] = (tempArray[19] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[10] * tempArray[11]);
            vectors[2].C2 = (tempArray[2] + tempArray[3]);
            tempArray[2] = (tempArray[7] * tempArray[15]);
            tempArray[3] = (-1 * tempArray[2] * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[5] * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            tempArray[8] = (tempArray[6] * tempArray[15]);
            tempArray[13] = (-1 * tempArray[1] * tempArray[4]);
            tempArray[8] = (tempArray[8] + tempArray[13]);
            tempArray[13] = (-1 * tempArray[5] * tempArray[14]);
            tempArray[8] = (tempArray[8] + tempArray[13]);
            tempArray[8] = (tempArray[0] * tempArray[8]);
            tempArray[3] = (tempArray[3] + tempArray[8]);
            tempArray[13] = (-1 * tempArray[0] * tempArray[3]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[2] = (tempArray[8] + tempArray[2]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[8] = (tempArray[13] + tempArray[2]);
            tempArray[13] = (-1 * tempArray[3] * tempArray[16]);
            tempArray[14] = (tempArray[11] * tempArray[14]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (tempArray[10] * tempArray[13]);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[13] * tempArray[1]);
            tempArray[14] = (-1 * tempArray[8] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (-1 * tempArray[10] * tempArray[2]);
            vectors[2].C3 = (tempArray[3] + tempArray[14]);
            tempArray[3] = Math.Pow(tempArray[12], 2);
            tempArray[3] = (-1 * tempArray[3]);
            tempArray[14] = Math.Pow(tempArray[13], 2);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[6] = (-1 * tempArray[7] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[4] * tempArray[10]);
            vectors[2].C4 = (tempArray[3] + tempArray[6]);
            tempArray[3] = (-1 * tempArray[12] * tempArray[8]);
            tempArray[6] = (tempArray[13] * tempArray[19]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (-1 * tempArray[8] * tempArray[15]);
            tempArray[5] = (-1 * tempArray[5] * tempArray[16]);
            tempArray[5] = (tempArray[6] + tempArray[5]);
            tempArray[6] = (-1 * tempArray[9] * tempArray[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (tempArray[5] * tempArray[10]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[0] * tempArray[3]);
            tempArray[6] = (-1 * tempArray[12] * tempArray[2]);
            tempArray[9] = (tempArray[13] * tempArray[11]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[5] * tempArray[6]);
            tempArray[6] = (tempArray[6] + tempArray[9]);
            tempArray[9] = (-1 * tempArray[0] * tempArray[6]);
            vectors[2].C1 = (tempArray[3] + tempArray[9]);
            tempArray[6] = (tempArray[0] * tempArray[6]);
            vectors[2].C5 = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[6] = (tempArray[3] + tempArray[14]);
            tempArray[9] = (tempArray[3] + tempArray[9]);
            tempArray[10] = (-1 * tempArray[0] * tempArray[9]);
            tempArray[14] = (tempArray[3] + tempArray[6]);
            tempArray[14] = (tempArray[0] * tempArray[14]);
            tempArray[10] = (tempArray[10] + tempArray[14]);
            tempArray[9] = (tempArray[0] * tempArray[9]);
            tempArray[9] = (tempArray[14] + tempArray[9]);
            tempArray[14] = (tempArray[13] * tempArray[3]);
            tempArray[15] = (tempArray[1] * tempArray[6]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[15] = (tempArray[19] * tempArray[9]);
            tempArray[14] = (tempArray[14] + tempArray[15]);
            tempArray[11] = (tempArray[11] * tempArray[10]);
            vectors[3].C2 = (tempArray[14] + tempArray[11]);
            tempArray[11] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[12] * tempArray[3]);
            tempArray[1] = (tempArray[1] * tempArray[11]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[3] = (-1 * tempArray[8] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[10]);
            vectors[3].C3 = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[12] * tempArray[6]);
            tempArray[2] = (tempArray[13] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[7] * tempArray[9]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * tempArray[4] * tempArray[10]);
            vectors[3].C4 = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[7] * tempArray[3]);
            tempArray[2] = (tempArray[8] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[19] * tempArray[11]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (tempArray[5] * tempArray[10]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[0] * tempArray[1]);
            tempArray[2] = (tempArray[4] * tempArray[3]);
            tempArray[3] = (tempArray[2] * tempArray[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (tempArray[11] * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[5] * tempArray[9]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * tempArray[0] * tempArray[2]);
            vectors[3].C1 = (tempArray[1] + tempArray[3]);
            tempArray[0] = (tempArray[0] * tempArray[2]);
            vectors[3].C5 = (tempArray[1] + tempArray[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:21.7586844+02:00
            
        
            return vectors;
        }
        
        
    }
}
