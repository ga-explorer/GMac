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
        private static cga0001Vector[] Factor6(double[] coefs)
        {
            var vectors = new[] 
            {
                new cga0001Vector(),
                new cga0001Vector()
            };
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:06.0767875+02:00
            //Macro: geometry3d.cga.Factor2
            //Input Variables: 2 used, 10 not used, 12 total.
            //Temp Variables: 98 sub-expressions, 0 generated temps, 98 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 10 total.
            //Computations: 1.18518518518519 average, 128 total.
            //Memory Reads: 1.96296296296296 average, 212 total.
            //Memory Writes: 108 total.
            //
            //Macro Binding Data: 
            //   B.#no^e1# <=> <Variable> coefs[0]
            //   B.#no^e2# <=> <Variable> coefs[1]
            //   B.#e1^e2# <=> <Variable> coefs[2]
            //   B.#no^e3# <=> <Variable> coefs[3]
            //   B.#e1^e3# <=> <Variable> coefs[4]
            //   B.#e2^e3# <=> <Variable> coefs[5]
            //   B.#no^ni# <=> <Variable> coefs[6]
            //   B.#e1^ni# <=> <Variable> coefs[7]
            //   B.#e2^ni# <=> <Variable> coefs[8]
            //   B.#e3^ni# <=> <Variable> coefs[9]
            //   inputVectors.f1.#e1# <=> <Constant> 1
            //   result.f1.#no# <=> <Variable> vectors[0].C1
            //   result.f1.#e1# <=> <Variable> vectors[0].C2
            //   result.f1.#e2# <=> <Variable> vectors[0].C3
            //   result.f1.#e3# <=> <Variable> vectors[0].C4
            //   result.f1.#ni# <=> <Variable> vectors[0].C5
            //   inputVectors.f2.#e2# <=> <Constant> 1
            //   result.f2.#no# <=> <Variable> vectors[1].C1
            //   result.f2.#e1# <=> <Variable> vectors[1].C2
            //   result.f2.#e2# <=> <Variable> vectors[1].C3
            //   result.f2.#e3# <=> <Variable> vectors[1].C4
            //   result.f2.#ni# <=> <Variable> vectors[1].C5
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            double tempVar0008;
            double tempVar0009;
            double tempVar000A;
            double tempVar000B;
            double tempVar000C;
            
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (-1 * coefs[0] * tempVar0000);
            tempVar0002 = (coefs[7] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (tempVar0000 * tempVar0001);
            tempVar0004 = (coefs[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * tempVar0000 * tempVar0002);
            tempVar0004 = (tempVar0003 + tempVar0004);
            tempVar0005 = (-1 * tempVar0000 * tempVar0004);
            tempVar0006 = (tempVar0000 * tempVar0002);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0003);
            tempVar0004 = (tempVar0000 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = Math.Pow(coefs[2], 2);
            tempVar0006 = Math.Pow(coefs[4], 2);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0001 * tempVar0003);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0002 * tempVar0005);
            vectors[0].C2 = (tempVar0004 + tempVar0006);
            tempVar0004 = (-1 * coefs[1] * tempVar0000);
            tempVar0006 = (coefs[8] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0007 = (coefs[1] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs[4] * coefs[5]);
            tempVar0008 = (-1 * tempVar0004 * tempVar0003);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * tempVar0005 * tempVar0006);
            vectors[0].C3 = (tempVar0007 + tempVar0008);
            tempVar0007 = (-1 * coefs[3] * tempVar0000);
            tempVar0008 = (coefs[9] * tempVar0000);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0009 = (coefs[3] * tempVar0000);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (coefs[2] * coefs[5]);
            tempVar0003 = (-1 * tempVar0007 * tempVar0003);
            tempVar0003 = (tempVar0009 + tempVar0003);
            tempVar0005 = (-1 * tempVar0005 * tempVar0008);
            vectors[0].C4 = (tempVar0003 + tempVar0005);
            tempVar0003 = (coefs[4] * tempVar0007);
            tempVar0005 = (coefs[2] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (-1 * coefs[6] * tempVar0005);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0005 = (coefs[4] * tempVar0008);
            tempVar0009 = (coefs[2] * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0009);
            tempVar0009 = (coefs[6] * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0009);
            tempVar0009 = (-1 * tempVar0000 * tempVar0005);
            vectors[0].C1 = (tempVar0003 + tempVar0009);
            tempVar0005 = (tempVar0000 * tempVar0005);
            vectors[0].C5 = (tempVar0003 + tempVar0005);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (tempVar0007 + tempVar0008);
            tempVar0009 = (tempVar0003 + tempVar0009);
            tempVar000A = (-1 * tempVar0000 * tempVar0009);
            tempVar000B = (tempVar0003 + tempVar0005);
            tempVar000B = (tempVar0000 * tempVar000B);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar0009 = (tempVar0000 * tempVar0009);
            tempVar0009 = (tempVar000B + tempVar0009);
            tempVar000B = (coefs[4] * tempVar0003);
            tempVar000C = (coefs[2] * tempVar0005);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar0001 = (tempVar0001 * tempVar0009);
            tempVar0001 = (tempVar000B + tempVar0001);
            tempVar0002 = (tempVar0002 * tempVar000A);
            vectors[1].C2 = (tempVar0001 + tempVar0002);
            tempVar0001 = (tempVar0004 + tempVar0006);
            tempVar0002 = (-1 * coefs[5] * tempVar0003);
            tempVar0003 = (coefs[2] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * tempVar0004 * tempVar0009);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * tempVar0006 * tempVar000A);
            vectors[1].C3 = (tempVar0002 + tempVar0003);
            tempVar0002 = (coefs[5] * tempVar0005);
            tempVar0001 = (coefs[4] * tempVar0001);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0002 = (-1 * tempVar0007 * tempVar0009);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0008 * tempVar000A);
            vectors[1].C4 = (tempVar0001 + tempVar0002);
            tempVar0001 = (tempVar0007 * tempVar0003);
            tempVar0002 = (tempVar0004 * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0001 * tempVar0001);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs[6] * tempVar000A);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (tempVar0000 * tempVar0001);
            tempVar0002 = (tempVar0008 * tempVar0003);
            tempVar0003 = (tempVar0006 * tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (tempVar0002 * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs[6] * tempVar0009);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * tempVar0000 * tempVar0002);
            vectors[1].C1 = (tempVar0001 + tempVar0003);
            tempVar0000 = (tempVar0000 * tempVar0002);
            vectors[1].C5 = (tempVar0001 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:06.0937885+02:00
            
        
            return vectors;
        }
        
        
    }
}
