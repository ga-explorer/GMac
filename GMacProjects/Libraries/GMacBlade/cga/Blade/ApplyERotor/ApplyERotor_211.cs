namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static double[] ApplyERotor_211(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:02.7355964+02:00
            //Macro: geometry3d.cga.ApplyERotor
            //Input Variables: 0 used, 15 not used, 15 total.
            //Temp Variables: 175 sub-expressions, 0 generated temps, 175 total.
            //Target Temp Variables: 16 total.
            //Output Variables: 5 total.
            //Computations: 1.33333333333333 average, 240 total.
            //Memory Reads: 2 average, 360 total.
            //Memory Writes: 180 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   v.#no^e1# <=> <Variable> coefs1[0]
            //   v.#no^e2# <=> <Variable> coefs1[1]
            //   v.#e1^e2# <=> <Variable> coefs1[2]
            //   v.#no^e3# <=> <Variable> coefs1[3]
            //   v.#e1^e3# <=> <Variable> coefs1[4]
            //   v.#e2^e3# <=> <Variable> coefs1[5]
            //   v.#no^ni# <=> <Variable> coefs1[6]
            //   v.#e1^ni# <=> <Variable> coefs1[7]
            //   v.#e2^ni# <=> <Variable> coefs1[8]
            //   v.#e3^ni# <=> <Variable> coefs1[9]
            //   mv.#no# <=> <Variable> coefs2[0]
            //   mv.#e1# <=> <Variable> coefs2[1]
            //   mv.#e2# <=> <Variable> coefs2[2]
            //   mv.#e3# <=> <Variable> coefs2[3]
            //   mv.#ni# <=> <Variable> coefs2[4]
            
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
            double tempVar000F;
            
            tempVar0000 = (coefs1[0] * coefs2[0]);
            tempVar0001 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * coefs2[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[7] * coefs2[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (coefs1[1] * coefs2[0]);
            tempVar0003 = (coefs1[2] * coefs2[1]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * coefs1[5] * coefs2[3]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * coefs1[8] * coefs2[4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * coefs1[1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (-1 * coefs1[2] * coefs2[0]);
            tempVar0004 = (coefs1[1] * coefs2[1]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[0] * coefs2[2]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * coefs1[2] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (coefs1[3] * coefs2[0]);
            tempVar0005 = (coefs1[4] * coefs2[1]);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (coefs1[5] * coefs2[2]);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * coefs1[9] * coefs2[4]);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * coefs1[3] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0005);
            tempVar0005 = (-1 * coefs1[4] * coefs2[0]);
            tempVar0006 = (coefs1[3] * coefs2[1]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * coefs1[4] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0006);
            tempVar0006 = (-1 * coefs1[5] * coefs2[0]);
            tempVar0007 = (coefs1[3] * coefs2[2]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[1] * coefs2[3]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * coefs1[5] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0007);
            tempVar0007 = (coefs1[6] * coefs2[0]);
            tempVar0008 = (coefs1[7] * coefs2[1]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[8] * coefs2[2]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (coefs1[9] * coefs2[3]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * coefs1[6] * tempVar0007);
            tempVar0001 = (tempVar0001 + tempVar0008);
            tempVar0008 = (-1 * coefs1[7] * coefs2[0]);
            tempVar0009 = (coefs1[6] * coefs2[1]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[7] * tempVar0008);
            tempVar0001 = (tempVar0001 + tempVar0009);
            tempVar0009 = (-1 * coefs1[8] * coefs2[0]);
            tempVar000A = (coefs1[6] * coefs2[2]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs1[1] * coefs2[4]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * coefs1[8] * tempVar0009);
            tempVar0001 = (tempVar0001 + tempVar000A);
            tempVar000A = (-1 * coefs1[9] * coefs2[0]);
            tempVar000B = (coefs1[6] * coefs2[3]);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * coefs1[3] * coefs2[4]);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * coefs1[9] * tempVar000A);
            c[0] = (tempVar0001 + tempVar000B);
            tempVar0001 = (-1 * coefs1[0] * coefs2[1]);
            tempVar000B = (-1 * coefs1[1] * coefs2[2]);
            tempVar0001 = (tempVar0001 + tempVar000B);
            tempVar000B = (-1 * coefs1[3] * coefs2[3]);
            tempVar0001 = (tempVar0001 + tempVar000B);
            tempVar000B = (-1 * coefs1[6] * coefs2[4]);
            tempVar0001 = (tempVar0001 + tempVar000B);
            tempVar000B = (coefs1[0] * tempVar0001);
            tempVar000C = (-1 * coefs1[2] * tempVar0002);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (coefs1[1] * tempVar0003);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (-1 * coefs1[4] * tempVar0004);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (coefs1[3] * tempVar0005);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (-1 * coefs1[5] * coefs2[1]);
            tempVar000D = (coefs1[4] * coefs2[2]);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * coefs1[2] * coefs2[3]);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * coefs1[5] * tempVar000C);
            tempVar000B = (tempVar000B + tempVar000D);
            tempVar000D = (-1 * coefs1[7] * tempVar0007);
            tempVar000B = (tempVar000B + tempVar000D);
            tempVar000D = (coefs1[6] * tempVar0008);
            tempVar000B = (tempVar000B + tempVar000D);
            tempVar000D = (-1 * coefs1[8] * coefs2[1]);
            tempVar000E = (coefs1[7] * coefs2[2]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar000E = (-1 * coefs1[2] * coefs2[4]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar000E = (-1 * coefs1[8] * tempVar000D);
            tempVar000B = (tempVar000B + tempVar000E);
            tempVar000E = (-1 * coefs1[9] * coefs2[1]);
            tempVar000F = (coefs1[7] * coefs2[3]);
            tempVar000E = (tempVar000E + tempVar000F);
            tempVar000F = (-1 * coefs1[4] * coefs2[4]);
            tempVar000E = (tempVar000E + tempVar000F);
            tempVar000F = (-1 * coefs1[9] * tempVar000E);
            c[1] = (tempVar000B + tempVar000F);
            tempVar000B = (coefs1[1] * tempVar0001);
            tempVar000F = (coefs1[2] * tempVar0000);
            tempVar000B = (tempVar000B + tempVar000F);
            tempVar0003 = (-1 * coefs1[0] * tempVar0003);
            tempVar0003 = (tempVar000B + tempVar0003);
            tempVar000B = (-1 * coefs1[5] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar000B);
            tempVar000B = (coefs1[3] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar000B);
            tempVar000B = (coefs1[4] * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar000B);
            tempVar000B = (-1 * coefs1[8] * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar000B);
            tempVar000B = (coefs1[6] * tempVar0009);
            tempVar0003 = (tempVar0003 + tempVar000B);
            tempVar000B = (coefs1[7] * tempVar000D);
            tempVar0003 = (tempVar0003 + tempVar000B);
            tempVar000B = (-1 * coefs1[9] * coefs2[2]);
            tempVar000F = (coefs1[8] * coefs2[3]);
            tempVar000B = (tempVar000B + tempVar000F);
            tempVar000F = (-1 * coefs1[5] * coefs2[4]);
            tempVar000B = (tempVar000B + tempVar000F);
            tempVar000F = (-1 * coefs1[9] * tempVar000B);
            c[2] = (tempVar0003 + tempVar000F);
            tempVar0003 = (coefs1[3] * tempVar0001);
            tempVar000F = (coefs1[4] * tempVar0000);
            tempVar0003 = (tempVar0003 + tempVar000F);
            tempVar000F = (coefs1[5] * tempVar0002);
            tempVar0003 = (tempVar0003 + tempVar000F);
            tempVar0005 = (-1 * coefs1[0] * tempVar0005);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (-1 * coefs1[1] * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (-1 * coefs1[2] * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (-1 * coefs1[9] * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (coefs1[6] * tempVar000A);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (coefs1[7] * tempVar000E);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (coefs1[8] * tempVar000B);
            c[3] = (tempVar0003 + tempVar0005);
            tempVar0001 = (coefs1[6] * tempVar0001);
            tempVar0000 = (coefs1[7] * tempVar0000);
            tempVar0000 = (tempVar0001 + tempVar0000);
            tempVar0001 = (coefs1[8] * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (coefs1[9] * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * tempVar0008);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[1] * tempVar0009);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[2] * tempVar000D);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * tempVar000A);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[4] * tempVar000E);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * coefs1[5] * tempVar000B);
            c[4] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:02.7515973+02:00
            
            return c;
        }
        
        
    }
}
