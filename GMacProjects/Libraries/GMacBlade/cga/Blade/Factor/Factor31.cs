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
        private static cga0001Vector[] Factor31(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga0001Vector(),
                new cga0001Vector(),
                new cga0001Vector(),
                new cga0001Vector(),
                new cga0001Vector()
            };
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:24.0918179+02:00
            //Macro: geometry3d.cga.Factor5
            //Input Variables: 5 used, 1 not used, 6 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 25 total.
            //Computations: 0.333333333333333 average, 10 total.
            //Memory Reads: 0.333333333333333 average, 10 total.
            //Memory Writes: 30 total.
            //
            //Macro Binding Data: 
            //   B.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
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
            //   inputVectors.f3.#e2# <=> <Constant> 1
            //   result.f3.#no# <=> <Variable> vectors[2].C1
            //   result.f3.#e1# <=> <Variable> vectors[2].C2
            //   result.f3.#e2# <=> <Variable> vectors[2].C3
            //   result.f3.#e3# <=> <Variable> vectors[2].C4
            //   result.f3.#ni# <=> <Variable> vectors[2].C5
            //   inputVectors.f4.#e3# <=> <Constant> 1
            //   result.f4.#no# <=> <Variable> vectors[3].C1
            //   result.f4.#e1# <=> <Variable> vectors[3].C2
            //   result.f4.#e2# <=> <Variable> vectors[3].C3
            //   result.f4.#e3# <=> <Variable> vectors[3].C4
            //   result.f4.#ni# <=> <Variable> vectors[3].C5
            //   inputVectors.f5.#ni# <=> <Constant> 1
            //   result.f5.#no# <=> <Variable> vectors[4].C1
            //   result.f5.#e1# <=> <Variable> vectors[4].C2
            //   result.f5.#e2# <=> <Variable> vectors[4].C3
            //   result.f5.#e3# <=> <Variable> vectors[4].C4
            //   result.f5.#ni# <=> <Variable> vectors[4].C5
            
            double tempVar0000;
            
            vectors[0].C2 = 0;
            vectors[0].C3 = 0;
            vectors[0].C4 = 0;
            vectors[0].C5 = 0;
            vectors[1].C1 = 0;
            vectors[1].C3 = 0;
            vectors[1].C4 = 0;
            vectors[1].C5 = 0;
            vectors[2].C1 = 0;
            vectors[2].C2 = 0;
            vectors[2].C4 = 0;
            vectors[2].C5 = 0;
            vectors[3].C1 = 0;
            vectors[3].C2 = 0;
            vectors[3].C3 = 0;
            vectors[3].C5 = 0;
            vectors[4].C1 = 0;
            vectors[4].C2 = 0;
            vectors[4].C3 = 0;
            vectors[4].C4 = 0;
            tempVar0000 = Math.Pow(coefs[0], 2);
            vectors[0].C1 = (-1 * tempVar0000);
            tempVar0000 = Math.Pow(coefs[0], 6);
            vectors[1].C2 = (-1 * tempVar0000);
            tempVar0000 = Math.Pow(coefs[0], 18);
            vectors[2].C3 = (-1 * tempVar0000);
            tempVar0000 = Math.Pow(coefs[0], 54);
            vectors[3].C4 = (-1 * tempVar0000);
            tempVar0000 = Math.Pow(coefs[0], 81);
            vectors[4].C5 = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:24.0948181+02:00
            
        
            return vectors;
        }
        
        
    }
}
