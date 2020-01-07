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
        private static double[] GP_231(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:49.4334037+02:00
            //Macro: geometry3d.cga.GP
            //Input Variables: 0 used, 20 not used, 20 total.
            //Temp Variables: 86 sub-expressions, 0 generated temps, 86 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 5 total.
            //Computations: 1.27472527472527 average, 116 total.
            //Memory Reads: 1.97802197802198 average, 180 total.
            //Memory Writes: 91 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   mv1.#no^e1# <=> <Variable> coefs1[0]
            //   mv1.#no^e2# <=> <Variable> coefs1[1]
            //   mv1.#e1^e2# <=> <Variable> coefs1[2]
            //   mv1.#no^e3# <=> <Variable> coefs1[3]
            //   mv1.#e1^e3# <=> <Variable> coefs1[4]
            //   mv1.#e2^e3# <=> <Variable> coefs1[5]
            //   mv1.#no^ni# <=> <Variable> coefs1[6]
            //   mv1.#e1^ni# <=> <Variable> coefs1[7]
            //   mv1.#e2^ni# <=> <Variable> coefs1[8]
            //   mv1.#e3^ni# <=> <Variable> coefs1[9]
            //   mv2.#no^e1^e2# <=> <Variable> coefs2[0]
            //   mv2.#no^e1^e3# <=> <Variable> coefs2[1]
            //   mv2.#no^e2^e3# <=> <Variable> coefs2[2]
            //   mv2.#e1^e2^e3# <=> <Variable> coefs2[3]
            //   mv2.#no^e1^ni# <=> <Variable> coefs2[4]
            //   mv2.#no^e2^ni# <=> <Variable> coefs2[5]
            //   mv2.#e1^e2^ni# <=> <Variable> coefs2[6]
            //   mv2.#no^e3^ni# <=> <Variable> coefs2[7]
            //   mv2.#e1^e3^ni# <=> <Variable> coefs2[8]
            //   mv2.#e2^e3^ni# <=> <Variable> coefs2[9]
            
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
            tempVar0001 = (coefs1[3] * tempVar0000);
            tempVar0002 = (coefs1[9] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (coefs1[1] * tempVar0000);
            tempVar0004 = (coefs1[8] * tempVar0000);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0005 = (-1 * coefs2[1] * tempVar0000);
            tempVar0006 = (-1 * coefs2[8] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0007 = (-1 * coefs2[0] * tempVar0000);
            tempVar0008 = (-1 * coefs2[6] * tempVar0000);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0009 = (-1 * coefs1[3] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0009);
            tempVar0009 = (-1 * coefs1[1] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0009);
            tempVar0009 = (coefs2[1] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0009);
            tempVar0009 = (coefs2[0] * tempVar0000);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (coefs1[5] * coefs2[3]);
            tempVar000A = (-1 * coefs1[6] * coefs2[4]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * tempVar0005 * tempVar0002);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * tempVar0001 * tempVar0006);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * tempVar0007 * tempVar0004);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * tempVar0003 * tempVar0008);
            c[1] = (tempVar0009 + tempVar000A);
            tempVar0009 = (coefs1[0] * tempVar0000);
            tempVar000A = (coefs1[7] * tempVar0000);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000B = (-1 * coefs2[2] * tempVar0000);
            tempVar000C = (-1 * coefs2[9] * tempVar0000);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000D = (-1 * coefs1[0] * tempVar0000);
            tempVar000A = (tempVar000A + tempVar000D);
            tempVar000D = (coefs2[2] * tempVar0000);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (coefs1[4] * coefs2[3]);
            tempVar000E = (coefs1[6] * coefs2[5]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar0002 = (tempVar000B * tempVar0002);
            tempVar0002 = (tempVar000D + tempVar0002);
            tempVar0001 = (tempVar0001 * tempVar000C);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0002 = (-1 * tempVar0007 * tempVar000A);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0009 * tempVar0008);
            c[2] = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * coefs1[2] * coefs2[3]);
            tempVar0002 = (coefs1[6] * coefs2[7]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar000B * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0003 * tempVar000C);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0005 * tempVar000A);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0009 * tempVar0006);
            c[3] = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * coefs2[7] * tempVar0001);
            tempVar0002 = (-1 * coefs2[5] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs2[4] * tempVar0009);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[5] * tempVar000B);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[4] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[2] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (tempVar0000 * tempVar0001);
            tempVar0002 = (coefs2[7] * tempVar0002);
            tempVar0003 = (coefs2[5] * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs2[4] * tempVar000A);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[5] * tempVar000C);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[4] * tempVar0006);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[2] * tempVar0008);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * tempVar0000 * tempVar0002);
            c[0] = (tempVar0001 + tempVar0003);
            tempVar0000 = (tempVar0000 * tempVar0002);
            c[4] = (tempVar0001 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:49.4414042+02:00
            
            return c;
        }
        
        
    }
}
