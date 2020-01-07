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
        private static double[] GP_323(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:51.5115226+02:00
            //Macro: geometry3d.cga.GP
            //Input Variables: 0 used, 20 not used, 20 total.
            //Temp Variables: 150 sub-expressions, 0 generated temps, 150 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 10 total.
            //Computations: 1.29375 average, 207 total.
            //Memory Reads: 1.9625 average, 314 total.
            //Memory Writes: 160 total.
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
            //   mv1.#no^e1^e2# <=> <Variable> coefs1[0]
            //   mv1.#no^e1^e3# <=> <Variable> coefs1[1]
            //   mv1.#no^e2^e3# <=> <Variable> coefs1[2]
            //   mv1.#e1^e2^e3# <=> <Variable> coefs1[3]
            //   mv1.#no^e1^ni# <=> <Variable> coefs1[4]
            //   mv1.#no^e2^ni# <=> <Variable> coefs1[5]
            //   mv1.#e1^e2^ni# <=> <Variable> coefs1[6]
            //   mv1.#no^e3^ni# <=> <Variable> coefs1[7]
            //   mv1.#e1^e3^ni# <=> <Variable> coefs1[8]
            //   mv1.#e2^e3^ni# <=> <Variable> coefs1[9]
            //   mv2.#no^e1# <=> <Variable> coefs2[0]
            //   mv2.#no^e2# <=> <Variable> coefs2[1]
            //   mv2.#e1^e2# <=> <Variable> coefs2[2]
            //   mv2.#no^e3# <=> <Variable> coefs2[3]
            //   mv2.#e1^e3# <=> <Variable> coefs2[4]
            //   mv2.#e2^e3# <=> <Variable> coefs2[5]
            //   mv2.#no^ni# <=> <Variable> coefs2[6]
            //   mv2.#e1^ni# <=> <Variable> coefs2[7]
            //   mv2.#e2^ni# <=> <Variable> coefs2[8]
            //   mv2.#e3^ni# <=> <Variable> coefs2[9]
            
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
            double tempVar000D;
            double tempVar000E;
            
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (-1 * coefs1[1] * tempVar0000);
            tempVar0002 = (-1 * coefs1[8] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (coefs1[0] * tempVar0000);
            tempVar0004 = (-1 * coefs1[6] * tempVar0000);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0005 = (-1 * coefs2[3] * tempVar0000);
            tempVar0006 = (coefs2[9] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0007 = (coefs2[1] * tempVar0000);
            tempVar0008 = (coefs2[8] * tempVar0000);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0009 = (coefs1[1] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0009);
            tempVar0009 = (-1 * coefs1[0] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0009);
            tempVar0009 = (coefs2[3] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0009);
            tempVar0009 = (-1 * coefs2[1] * tempVar0000);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[5] * coefs2[2]);
            tempVar000A = (-1 * coefs1[7] * coefs2[4]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (tempVar0005 * tempVar0002);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * tempVar0001 * tempVar0006);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (tempVar0003 * tempVar0008);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * tempVar0007 * tempVar0004);
            tempVar0009 = (tempVar0009 + tempVar000A);
            c[4] = (-1 * tempVar0009);
            tempVar0009 = (-1 * coefs1[2] * tempVar0000);
            tempVar000A = (-1 * coefs1[9] * tempVar0000);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000B = (coefs2[0] * tempVar0000);
            tempVar000C = (coefs2[7] * tempVar0000);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000D = (coefs1[2] * tempVar0000);
            tempVar000A = (tempVar000A + tempVar000D);
            tempVar000D = (-1 * coefs2[0] * tempVar0000);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * coefs1[4] * coefs2[2]);
            tempVar000E = (coefs1[7] * coefs2[5]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar0005 = (-1 * tempVar0005 * tempVar000A);
            tempVar0005 = (tempVar000D + tempVar0005);
            tempVar0006 = (tempVar0009 * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0003 = (tempVar0003 * tempVar000C);
            tempVar0003 = (tempVar0005 + tempVar0003);
            tempVar0004 = (-1 * tempVar000B * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            c[5] = (-1 * tempVar0003);
            tempVar0003 = (-1 * coefs1[4] * coefs2[4]);
            tempVar0004 = (-1 * coefs1[5] * coefs2[5]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar000A * tempVar0008);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * tempVar0009 * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0002 = (tempVar0002 * tempVar000C);
            tempVar0002 = (tempVar0003 + tempVar0002);
            tempVar0001 = (-1 * tempVar0001 * tempVar000B);
            tempVar0001 = (tempVar0002 + tempVar0001);
            c[7] = (-1 * tempVar0001);
            tempVar0001 = (tempVar0005 * tempVar0004);
            tempVar0002 = (tempVar0003 * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0001 * tempVar0008);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0007 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0009 * tempVar000C);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar000B * tempVar000A);
            tempVar0001 = (tempVar0001 + tempVar0002);
            c[3] = (-1 * tempVar0001);
            tempVar0001 = (-1 * coefs2[4] * tempVar0009);
            tempVar0002 = (coefs2[5] * tempVar0001);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs2[6] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[3] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs1[4] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[5] * tempVar000B);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0002 = (-1 * coefs2[4] * tempVar000A);
            tempVar0003 = (coefs2[5] * tempVar0002);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * coefs2[6] * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[3] * tempVar0006);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[4] * tempVar0008);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * coefs1[5] * tempVar000C);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0002);
            c[0] = (tempVar0001 + tempVar0003);
            tempVar0003 = (coefs2[2] * tempVar0009);
            tempVar0004 = (coefs2[6] * tempVar0002);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs2[5] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[4] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[3] * tempVar0008);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (coefs1[7] * tempVar000B);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0004 = (coefs2[2] * tempVar000A);
            tempVar0005 = (-1 * coefs2[6] * tempVar0001);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * coefs2[5] * tempVar0003);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (coefs1[4] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * coefs1[3] * tempVar0007);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * coefs1[7] * tempVar000C);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (tempVar0000 * tempVar0004);
            c[1] = (tempVar0003 + tempVar0005);
            tempVar0005 = (-1 * coefs2[6] * tempVar000A);
            tempVar0006 = (coefs2[2] * tempVar0001);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs2[4] * tempVar0004);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (coefs1[5] * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[7] * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[3] * tempVar000C);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0006 = (coefs2[6] * tempVar0009);
            tempVar0007 = (coefs2[2] * tempVar0002);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs2[4] * tempVar0003);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[5] * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (coefs1[7] * tempVar0008);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[3] * tempVar000B);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (tempVar0000 * tempVar0006);
            c[2] = (tempVar0005 + tempVar0007);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            c[6] = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * tempVar0000 * tempVar0004);
            c[8] = (tempVar0003 + tempVar0001);
            tempVar0000 = (-1 * tempVar0000 * tempVar0006);
            c[9] = (tempVar0005 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:51.5275235+02:00
            
            return c;
        }
        
        
    }
}