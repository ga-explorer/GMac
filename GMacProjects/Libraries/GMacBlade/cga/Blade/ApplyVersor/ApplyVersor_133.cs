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
        private static double[] ApplyVersor_133(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:13.8958029+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 266 sub-expressions, 0 generated temps, 266 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 10 total.
            //Computations: 1.30072463768116 average, 359 total.
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
            //   v.#no# <=> <Variable> coefs1[0]
            //   v.#e1# <=> <Variable> coefs1[1]
            //   v.#e2# <=> <Variable> coefs1[2]
            //   v.#e3# <=> <Variable> coefs1[3]
            //   v.#ni# <=> <Variable> coefs1[4]
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
            tempVar0001 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (coefs1[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (-1 * coefs2[0] * tempVar0000);
            tempVar0004 = (-1 * coefs2[6] * tempVar0000);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0005 = (coefs1[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0005);
            tempVar0005 = (coefs2[0] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (coefs2[4] * tempVar0001);
            tempVar0006 = (-1 * coefs2[1] * tempVar0000);
            tempVar0007 = (-1 * coefs2[8] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0008 = (-1 * coefs1[3] * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (-1 * coefs1[2] * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (-1 * tempVar0000 * tempVar0005);
            tempVar0009 = (-1 * coefs2[4] * tempVar0002);
            tempVar000A = (coefs2[1] * tempVar0000);
            tempVar0007 = (tempVar0007 + tempVar000A);
            tempVar000A = (-1 * coefs1[3] * tempVar0007);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs1[2] * tempVar0004);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar0009 = (tempVar0000 * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar000A = (-1 * tempVar0000 * tempVar0008);
            tempVar0005 = (tempVar0000 * tempVar0005);
            tempVar0005 = (tempVar0009 + tempVar0005);
            tempVar0005 = (tempVar0000 * tempVar0005);
            tempVar0009 = (tempVar000A + tempVar0005);
            tempVar000A = (-1 * coefs1[2] * coefs2[4]);
            tempVar000B = (coefs1[1] * coefs2[5]);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar0003 = (-1 * tempVar0001 * tempVar0003);
            tempVar0003 = (tempVar000A + tempVar0003);
            tempVar0004 = (tempVar0002 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0000 * tempVar0008);
            tempVar0004 = (tempVar0005 + tempVar0004);
            tempVar0005 = Math.Pow(coefs1[1], 2);
            tempVar0005 = (-1 * tempVar0005);
            tempVar0008 = Math.Pow(coefs1[2], 2);
            tempVar0008 = (-1 * tempVar0008);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = Math.Pow(coefs1[3], 2);
            tempVar0008 = (-1 * tempVar0008);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = Math.Pow(tempVar0001, 2);
            tempVar0008 = (-1 * tempVar0008);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = Math.Pow(tempVar0002, 2);
            tempVar0008 = (-1 * tempVar0008);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0005 = Math.Pow(tempVar0005, -1);
            tempVar0008 = (-1 * coefs1[3] * coefs2[4]);
            tempVar000A = (coefs1[1] * coefs2[7]);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar0006 = (-1 * tempVar0001 * tempVar0006);
            tempVar0006 = (tempVar0008 + tempVar0006);
            tempVar0007 = (tempVar0002 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (coefs1[1] * coefs2[4]);
            tempVar0008 = (coefs1[2] * coefs2[5]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[3] * coefs2[7]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[1] * tempVar0007);
            tempVar000A = (-1 * coefs1[3] * tempVar0006);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar000A = (-1 * coefs1[2] * tempVar0003);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar0004 = (tempVar0002 * tempVar0004);
            tempVar0004 = (tempVar0008 + tempVar0004);
            tempVar0008 = (-1 * tempVar0001 * tempVar0009);
            tempVar0004 = (tempVar0004 + tempVar0008);
            c[4] = (-1 * tempVar0005 * tempVar0004);
            tempVar0004 = (-1 * coefs2[5] * tempVar0001);
            tempVar0008 = (-1 * coefs2[2] * tempVar0000);
            tempVar0009 = (-1 * coefs2[9] * tempVar0000);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar000A = (coefs1[3] * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * coefs1[1] * tempVar0003);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * tempVar0000 * tempVar0004);
            tempVar000B = (coefs2[5] * tempVar0002);
            tempVar000C = (coefs2[2] * tempVar0000);
            tempVar0009 = (tempVar0009 + tempVar000C);
            tempVar000C = (coefs1[3] * tempVar0009);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (-1 * coefs1[1] * tempVar0004);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000B = (tempVar0000 * tempVar000B);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000C = (-1 * tempVar0000 * tempVar000A);
            tempVar0004 = (tempVar0000 * tempVar0004);
            tempVar0004 = (tempVar000B + tempVar0004);
            tempVar0004 = (tempVar0000 * tempVar0004);
            tempVar000B = (tempVar000C + tempVar0004);
            tempVar000A = (tempVar0000 * tempVar000A);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (coefs1[3] * coefs2[5]);
            tempVar000C = (-1 * coefs1[2] * coefs2[7]);
            tempVar000A = (tempVar000A + tempVar000C);
            tempVar0008 = (tempVar0001 * tempVar0008);
            tempVar0008 = (tempVar000A + tempVar0008);
            tempVar0009 = (-1 * tempVar0002 * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[2] * tempVar0007);
            tempVar000A = (coefs1[3] * tempVar0008);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar0003 = (-1 * coefs1[1] * tempVar0003);
            tempVar0003 = (tempVar0009 + tempVar0003);
            tempVar0004 = (-1 * tempVar0002 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0001 * tempVar000B);
            tempVar0003 = (tempVar0003 + tempVar0004);
            c[5] = (-1 * tempVar0005 * tempVar0003);
            tempVar0003 = (-1 * coefs2[7] * tempVar0001);
            tempVar0004 = (-1 * coefs1[2] * tempVar0008);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[1] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * tempVar0000 * tempVar0003);
            tempVar0009 = (coefs2[7] * tempVar0002);
            tempVar000A = (-1 * coefs1[2] * tempVar0009);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs1[1] * tempVar0007);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar0009 = (tempVar0000 * tempVar0009);
            tempVar0004 = (tempVar0004 + tempVar0009);
            tempVar000A = (-1 * tempVar0000 * tempVar0004);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0009 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0009 = (tempVar000A + tempVar0003);
            tempVar0004 = (tempVar0000 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[3] * tempVar0007);
            tempVar0007 = (-1 * coefs1[2] * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar0007);
            tempVar0006 = (-1 * coefs1[1] * tempVar0006);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0002 = (-1 * tempVar0002 * tempVar0003);
            tempVar0002 = (tempVar0004 + tempVar0002);
            tempVar0001 = (tempVar0001 * tempVar0009);
            tempVar0001 = (tempVar0002 + tempVar0001);
            c[7] = (-1 * tempVar0005 * tempVar0001);
            tempVar0001 = (-1 * coefs2[3] * tempVar0002);
            tempVar0002 = (-1 * coefs1[1] * tempVar0008);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[2] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs1[3] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0000 * tempVar0001);
            tempVar0003 = (-1 * coefs2[3] * tempVar0001);
            tempVar0004 = (-1 * coefs1[1] * tempVar0009);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (coefs1[2] * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[3] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0004 = (tempVar0000 * tempVar0002);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0003 = (tempVar0004 + tempVar0001);
            tempVar0004 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0006 = (tempVar0003 * tempVar0002);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0001 * tempVar0004);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[2] * coefs2[3]);
            tempVar0006 = (tempVar0002 * tempVar0006);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0006 = (tempVar0001 * tempVar0007);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0006 = (coefs1[1] * coefs2[3]);
            tempVar0007 = (-1 * tempVar0008 * tempVar0002);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * tempVar0001 * tempVar0009);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0006 = (coefs1[1] * tempVar0006);
            tempVar0002 = (-1 * coefs1[2] * tempVar0002);
            tempVar0002 = (tempVar0006 + tempVar0002);
            tempVar0004 = (coefs1[3] * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0003 = (-1 * tempVar0002 * tempVar0003);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0001 = (-1 * tempVar0001 * tempVar0001);
            tempVar0001 = (tempVar0002 + tempVar0001);
            c[3] = (-1 * tempVar0005 * tempVar0001);
            tempVar0001 = (coefs1[1] * tempVar000B);
            tempVar0002 = (-1 * coefs1[2] * tempVar0009);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[3] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0002 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0001 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0002 = (coefs1[1] * tempVar0004);
            tempVar0003 = (-1 * coefs1[2] * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (coefs1[3] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * tempVar0002 * tempVar0003);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (tempVar0001 * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0002);
            tempVar0003 = (tempVar0001 + tempVar0003);
            c[0] = (tempVar0003 * tempVar0005);
            tempVar0003 = (coefs1[1] * tempVar0009);
            tempVar0004 = (-1 * coefs1[3] * tempVar0009);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[2] * tempVar0003);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0002 * tempVar0002);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0001 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0004 = (coefs1[1] * tempVar0003);
            tempVar0006 = (-1 * coefs1[3] * tempVar0004);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (-1 * coefs1[2] * tempVar0001);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (-1 * tempVar0002 * tempVar0006);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0001 * tempVar0002);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0000 * tempVar0004);
            tempVar0006 = (tempVar0003 + tempVar0006);
            c[1] = (tempVar0005 * tempVar0006);
            tempVar0006 = (-1 * coefs1[2] * tempVar0009);
            tempVar0007 = (coefs1[3] * tempVar000B);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[1] * tempVar0003);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * tempVar0002 * tempVar0006);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * tempVar0001 * tempVar0008);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0007 = (-1 * coefs1[2] * tempVar0003);
            tempVar0008 = (coefs1[3] * tempVar0004);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * coefs1[1] * tempVar0001);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (tempVar0002 * tempVar0008);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * tempVar0001 * tempVar0006);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (tempVar0000 * tempVar0007);
            tempVar0008 = (tempVar0006 + tempVar0008);
            c[2] = (tempVar0005 * tempVar0008);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            c[6] = (tempVar0005 * tempVar0001);
            tempVar0001 = (-1 * tempVar0000 * tempVar0004);
            tempVar0001 = (tempVar0003 + tempVar0001);
            c[8] = (tempVar0005 * tempVar0001);
            tempVar0000 = (-1 * tempVar0000 * tempVar0007);
            tempVar0000 = (tempVar0006 + tempVar0000);
            c[9] = (tempVar0005 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:13.9208043+02:00
            
            return c;
        }
        
        
    }
}
