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
        private static double[] ApplyRotor_422(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:41.3443729+02:00
            //Macro: geometry3d.cga.ApplyRotor
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 242 sub-expressions, 0 generated temps, 242 total.
            //Target Temp Variables: 12 total.
            //Output Variables: 10 total.
            //Computations: 1.25793650793651 average, 317 total.
            //Memory Reads: 1.98809523809524 average, 501 total.
            //Memory Writes: 252 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1# <=> <Variable> c[0]
            //   result.#no^e2# <=> <Variable> c[1]
            //   result.#e1^e2# <=> <Variable> c[2]
            //   result.#no^e3# <=> <Variable> c[3]
            //   result.#e1^e3# <=> <Variable> c[4]
            //   result.#e2^e3# <=> <Variable> c[5]
            //   result.#no^ni# <=> <Variable> c[6]
            //   result.#e1^ni# <=> <Variable> c[7]
            //   result.#e2^ni# <=> <Variable> c[8]
            //   result.#e3^ni# <=> <Variable> c[9]
            //   v.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   v.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   v.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   v.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   v.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
            //   mv.#no^e1# <=> <Variable> coefs2[0]
            //   mv.#no^e2# <=> <Variable> coefs2[1]
            //   mv.#e1^e2# <=> <Variable> coefs2[2]
            //   mv.#no^e3# <=> <Variable> coefs2[3]
            //   mv.#e1^e3# <=> <Variable> coefs2[4]
            //   mv.#e2^e3# <=> <Variable> coefs2[5]
            //   mv.#no^ni# <=> <Variable> coefs2[6]
            //   mv.#e1^ni# <=> <Variable> coefs2[7]
            //   mv.#e2^ni# <=> <Variable> coefs2[8]
            //   mv.#e3^ni# <=> <Variable> coefs2[9]
            
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
            
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (coefs1[0] * tempVar0000);
            tempVar0002 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (coefs2[2] * tempVar0001);
            tempVar0004 = (coefs2[1] * tempVar0000);
            tempVar0005 = (coefs2[8] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0006 = (-1 * coefs1[3] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (coefs2[0] * tempVar0000);
            tempVar0007 = (coefs2[7] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0008 = (-1 * coefs1[2] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0008);
            tempVar0008 = (-1 * tempVar0000 * tempVar0003);
            tempVar0009 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0009);
            tempVar0009 = (coefs2[2] * tempVar0002);
            tempVar000A = (-1 * coefs2[1] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar000A);
            tempVar000A = (coefs1[3] * tempVar0005);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs2[0] * tempVar0000);
            tempVar0007 = (tempVar0007 + tempVar000A);
            tempVar000A = (coefs1[2] * tempVar0007);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar0009 = (tempVar0000 * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar000A = (tempVar0000 * tempVar0008);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0009 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0009 = (tempVar000A + tempVar0003);
            tempVar000A = (coefs1[2] * coefs2[2]);
            tempVar000B = (-1 * coefs1[1] * coefs2[4]);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar0007 = (-1 * tempVar0002 * tempVar0007);
            tempVar0007 = (tempVar000A + tempVar0007);
            tempVar0006 = (tempVar0001 * tempVar0006);
            tempVar0006 = (tempVar0007 + tempVar0006);
            tempVar0007 = (-1 * tempVar0000 * tempVar0008);
            tempVar0003 = (tempVar0003 + tempVar0007);
            tempVar0007 = (coefs1[3] * coefs2[2]);
            tempVar0008 = (-1 * coefs1[1] * coefs2[5]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0005 = (-1 * tempVar0002 * tempVar0005);
            tempVar0005 = (tempVar0007 + tempVar0005);
            tempVar0004 = (tempVar0001 * tempVar0004);
            tempVar0004 = (tempVar0005 + tempVar0004);
            tempVar0005 = (coefs1[1] * coefs2[2]);
            tempVar0007 = (coefs1[2] * coefs2[4]);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (coefs1[3] * coefs2[5]);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (-1 * coefs1[1] * tempVar0005);
            tempVar0008 = (coefs1[2] * tempVar0006);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * coefs1[3] * tempVar0004);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0003 = (-1 * tempVar0001 * tempVar0003);
            tempVar0003 = (tempVar0007 + tempVar0003);
            tempVar0007 = (-1 * tempVar0002 * tempVar0009);
            c[2] = (tempVar0003 + tempVar0007);
            tempVar0003 = (-1 * coefs2[4] * tempVar0001);
            tempVar0007 = (coefs2[3] * tempVar0000);
            tempVar0008 = (coefs2[9] * tempVar0000);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0009 = (coefs1[3] * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0009);
            tempVar0009 = (-1 * coefs1[1] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0009);
            tempVar0009 = (-1 * tempVar0000 * tempVar0003);
            tempVar000A = (-1 * coefs2[4] * tempVar0002);
            tempVar000B = (-1 * coefs2[3] * tempVar0000);
            tempVar0008 = (tempVar0008 + tempVar000B);
            tempVar000B = (-1 * coefs1[3] * tempVar0008);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (coefs1[1] * tempVar0007);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000A = (tempVar0000 * tempVar000A);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000B = (tempVar0000 * tempVar0009);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar000A + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar000A = (tempVar000B + tempVar0003);
            tempVar0009 = (-1 * tempVar0000 * tempVar0009);
            tempVar0003 = (tempVar0003 + tempVar0009);
            tempVar0009 = (-1 * coefs1[3] * coefs2[4]);
            tempVar000B = (coefs1[2] * coefs2[5]);
            tempVar0009 = (tempVar0009 + tempVar000B);
            tempVar0008 = (tempVar0002 * tempVar0008);
            tempVar0008 = (tempVar0009 + tempVar0008);
            tempVar0007 = (-1 * tempVar0001 * tempVar0007);
            tempVar0007 = (tempVar0008 + tempVar0007);
            tempVar0008 = (-1 * coefs1[2] * tempVar0005);
            tempVar0006 = (-1 * coefs1[1] * tempVar0006);
            tempVar0006 = (tempVar0008 + tempVar0006);
            tempVar0008 = (coefs1[3] * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0008);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0003 = (tempVar0006 + tempVar0003);
            tempVar0006 = (tempVar0002 * tempVar000A);
            c[4] = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * coefs2[5] * tempVar0001);
            tempVar0006 = (-1 * coefs1[2] * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * coefs1[1] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * tempVar0000 * tempVar0003);
            tempVar0008 = (-1 * coefs2[5] * tempVar0002);
            tempVar0009 = (coefs1[2] * tempVar0008);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (coefs1[1] * tempVar0005);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0008 = (tempVar0000 * tempVar0008);
            tempVar0006 = (tempVar0006 + tempVar0008);
            tempVar0009 = (tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0008 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0008 = (tempVar0009 + tempVar0003);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0005 = (coefs1[3] * tempVar0005);
            tempVar0004 = (-1 * coefs1[1] * tempVar0004);
            tempVar0004 = (tempVar0005 + tempVar0004);
            tempVar0005 = (coefs1[2] * tempVar0007);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0003 = (tempVar0004 + tempVar0003);
            tempVar0004 = (tempVar0002 * tempVar0008);
            c[5] = (tempVar0003 + tempVar0004);
            tempVar0003 = (coefs2[6] * tempVar0002);
            tempVar0004 = (coefs1[1] * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[2] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (coefs1[3] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0000 * tempVar0003);
            tempVar0005 = (-1 * coefs2[6] * tempVar0001);
            tempVar0006 = (-1 * coefs1[1] * tempVar0008);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (coefs1[2] * tempVar0005);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[3] * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0006 = (-1 * tempVar0000 * tempVar0004);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0005 + tempVar0003);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0005 = (tempVar0006 + tempVar0003);
            tempVar0006 = (-1 * coefs1[3] * coefs2[6]);
            tempVar0007 = (tempVar0001 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (tempVar0006 * tempVar0002);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0004 = (tempVar0000 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (coefs1[2] * coefs2[6]);
            tempVar0007 = (tempVar0001 * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0007);
            tempVar0007 = (tempVar0004 * tempVar0002);
            tempVar0004 = (tempVar0004 + tempVar0007);
            tempVar0007 = (coefs1[1] * coefs2[6]);
            tempVar0008 = (-1 * tempVar0001 * tempVar0008);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * tempVar0002 * tempVar0007);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0006 = (coefs1[3] * tempVar0006);
            tempVar0004 = (coefs1[2] * tempVar0004);
            tempVar0004 = (tempVar0006 + tempVar0004);
            tempVar0006 = (coefs1[1] * tempVar0007);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0001 = (tempVar0001 * tempVar0005);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0002 = (-1 * tempVar0002 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0002);
            c[6] = (-1 * tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * tempVar0009);
            tempVar0002 = (-1 * coefs1[1] * tempVar000A);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (coefs1[3] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0001 * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0002 * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0000 * tempVar0001);
            tempVar0003 = (coefs1[2] * tempVar0003);
            tempVar0004 = (coefs1[1] * tempVar0003);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[3] * tempVar0003);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0001 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * tempVar0002 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (tempVar0000 * tempVar0003);
            c[0] = (tempVar0002 + tempVar0003);
            tempVar0002 = (coefs1[3] * tempVar0009);
            tempVar0004 = (-1 * coefs1[1] * tempVar0008);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (coefs1[2] * tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * tempVar0001 * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * tempVar0002 * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * tempVar0000 * tempVar0002);
            tempVar0005 = (-1 * coefs1[3] * tempVar0003);
            tempVar0006 = (coefs1[1] * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[2] * tempVar0003);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (tempVar0001 * tempVar0004);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * tempVar0002 * tempVar0004);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (tempVar0000 * tempVar0005);
            c[1] = (tempVar0004 + tempVar0005);
            tempVar0004 = (-1 * coefs1[3] * tempVar000A);
            tempVar0006 = (-1 * coefs1[2] * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (-1 * coefs1[1] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0001 * tempVar0007);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (tempVar0002 * tempVar0007);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (-1 * tempVar0000 * tempVar0004);
            tempVar0007 = (coefs1[3] * tempVar0003);
            tempVar0008 = (coefs1[2] * tempVar0003);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[1] * tempVar0003);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * tempVar0001 * tempVar0007);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (tempVar0002 * tempVar0007);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0007 = (tempVar0000 * tempVar0007);
            c[3] = (tempVar0006 + tempVar0007);
            tempVar0001 = (tempVar0000 * tempVar0001);
            c[7] = (tempVar0003 + tempVar0001);
            tempVar0001 = (tempVar0000 * tempVar0002);
            c[8] = (tempVar0005 + tempVar0001);
            tempVar0000 = (tempVar0000 * tempVar0004);
            c[9] = (tempVar0007 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:41.3663741+02:00
            
            return c;
        }
        
        
    }
}
