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
        private static double[] ApplyRotor_444(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:42.4224345+02:00
            //Macro: geometry3d.cga.ApplyRotor
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 125 sub-expressions, 0 generated temps, 125 total.
            //Target Temp Variables: 12 total.
            //Output Variables: 5 total.
            //Computations: 1.29230769230769 average, 168 total.
            //Memory Reads: 1.98461538461538 average, 258 total.
            //Memory Writes: 130 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   v.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   v.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   v.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   v.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   v.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs2[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs2[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs2[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs2[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs2[4]
            
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
            tempVar0001 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (-1 * coefs2[1] * tempVar0001);
            tempVar0004 = (-1 * coefs2[0] * tempVar0000);
            tempVar0005 = (-1 * coefs2[4] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0006 = (coefs1[1] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * tempVar0000 * tempVar0003);
            tempVar0007 = (coefs1[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0007);
            tempVar0007 = (coefs2[1] * tempVar0002);
            tempVar0008 = (coefs2[0] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (-1 * coefs1[1] * tempVar0005);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0007 = (tempVar0000 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0008 = (tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0007 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0007 = (tempVar0008 + tempVar0003);
            tempVar0008 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0009 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[3] * coefs2[3]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * tempVar0002 * tempVar0005);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * tempVar0001 * tempVar0004);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (coefs1[1] * tempVar0008);
            tempVar0009 = (-1 * coefs1[2] * coefs2[1]);
            tempVar000A = (coefs1[1] * coefs2[2]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs1[2] * tempVar0009);
            tempVar0006 = (tempVar0006 + tempVar000A);
            tempVar000A = (-1 * coefs1[3] * coefs2[1]);
            tempVar000B = (coefs1[1] * coefs2[3]);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (coefs1[3] * tempVar000A);
            tempVar0006 = (tempVar0006 + tempVar000B);
            tempVar0007 = (tempVar0002 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0003 = (-1 * tempVar0001 * tempVar0003);
            c[1] = (tempVar0006 + tempVar0003);
            tempVar0003 = (coefs2[2] * tempVar0001);
            tempVar0006 = (-1 * coefs1[2] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * tempVar0000 * tempVar0003);
            tempVar0007 = (-1 * coefs2[2] * tempVar0002);
            tempVar000B = (coefs1[2] * tempVar0005);
            tempVar0007 = (tempVar0007 + tempVar000B);
            tempVar0007 = (tempVar0000 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar000B = (tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0007 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0007 = (tempVar000B + tempVar0003);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (coefs1[2] * tempVar0008);
            tempVar0009 = (coefs1[1] * tempVar0009);
            tempVar0006 = (tempVar0006 + tempVar0009);
            tempVar0009 = (coefs1[3] * coefs2[2]);
            tempVar000B = (-1 * coefs1[2] * coefs2[3]);
            tempVar0009 = (tempVar0009 + tempVar000B);
            tempVar000B = (-1 * coefs1[3] * tempVar0009);
            tempVar0006 = (tempVar0006 + tempVar000B);
            tempVar0007 = (-1 * tempVar0002 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0003 = (tempVar0001 * tempVar0003);
            c[2] = (tempVar0006 + tempVar0003);
            tempVar0003 = (coefs2[3] * tempVar0001);
            tempVar0006 = (-1 * coefs1[3] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * tempVar0000 * tempVar0003);
            tempVar0007 = (-1 * coefs2[3] * tempVar0002);
            tempVar000B = (coefs1[3] * tempVar0005);
            tempVar0007 = (tempVar0007 + tempVar000B);
            tempVar0007 = (tempVar0000 * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar000B = (tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0003 = (tempVar0007 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0007 = (tempVar000B + tempVar0003);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * coefs1[3] * tempVar0008);
            tempVar0008 = (coefs1[1] * tempVar000A);
            tempVar0006 = (tempVar0006 + tempVar0008);
            tempVar0008 = (-1 * coefs1[2] * tempVar0009);
            tempVar0006 = (tempVar0006 + tempVar0008);
            tempVar0002 = (-1 * tempVar0002 * tempVar0007);
            tempVar0002 = (tempVar0006 + tempVar0002);
            tempVar0001 = (tempVar0001 * tempVar0003);
            c[3] = (tempVar0002 + tempVar0001);
            tempVar0001 = (-1 * coefs1[1] * tempVar0007);
            tempVar0002 = (coefs1[2] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs1[3] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0002 * tempVar0008);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (tempVar0001 * tempVar0005);
            tempVar0003 = (-1 * tempVar0004 * tempVar0002);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (tempVar0001 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0001);
            tempVar0004 = (coefs1[1] * tempVar0003);
            tempVar0005 = (-1 * coefs1[2] * tempVar0003);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (coefs1[3] * tempVar0003);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0002 = (-1 * tempVar0002 * tempVar0002);
            tempVar0002 = (tempVar0004 + tempVar0002);
            tempVar0004 = (tempVar0001 * tempVar0008);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            c[0] = (tempVar0003 + tempVar0002);
            tempVar0000 = (-1 * tempVar0000 * tempVar0001);
            c[4] = (tempVar0002 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:42.4334352+02:00
            
            return c;
        }
        
        
    }
}