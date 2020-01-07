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
        private static double[] ApplyReflector_411(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:57.5102975+02:00
            //Macro: geometry3d.cga.ApplyReflector
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 130 sub-expressions, 0 generated temps, 130 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 5 total.
            //Computations: 1.35555555555556 average, 183 total.
            //Memory Reads: 1.94814814814815 average, 263 total.
            //Memory Writes: 135 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   v.#no^e1^e2^e3# <=> <Variable> coefs1[0]
            //   v.#no^e1^e2^ni# <=> <Variable> coefs1[1]
            //   v.#no^e1^e3^ni# <=> <Variable> coefs1[2]
            //   v.#no^e2^e3^ni# <=> <Variable> coefs1[3]
            //   v.#e1^e2^e3^ni# <=> <Variable> coefs1[4]
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
            
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (coefs1[0] * tempVar0000);
            tempVar0002 = (-1 * coefs1[4] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0003 = (-1 * coefs2[1] * tempVar0001);
            tempVar0004 = (-1 * coefs2[0] * tempVar0000);
            tempVar0005 = (coefs2[4] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0006 = (-1 * coefs1[3] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0006 = (-1 * coefs1[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0006 = (-1 * coefs2[1] * tempVar0002);
            tempVar0007 = (coefs2[0] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (coefs1[3] * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (tempVar0000 * tempVar0006);
            tempVar0007 = (tempVar0003 + tempVar0007);
            tempVar0008 = (tempVar0000 * tempVar0007);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0006 = (tempVar0008 + tempVar0003);
            tempVar0008 = (coefs1[3] * coefs2[1]);
            tempVar0009 = (-1 * coefs1[2] * coefs2[2]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (coefs1[1] * coefs2[3]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0005 = (tempVar0002 * tempVar0005);
            tempVar0005 = (tempVar0008 + tempVar0005);
            tempVar0004 = (-1 * tempVar0001 * tempVar0004);
            tempVar0004 = (tempVar0005 + tempVar0004);
            tempVar0005 = (-1 * tempVar0000 * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0005 = (-1 * coefs1[2] * coefs2[1]);
            tempVar0007 = (-1 * coefs1[3] * coefs2[2]);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (-1 * coefs1[2] * tempVar0005);
            tempVar0008 = (-1 * coefs1[1] * coefs2[1]);
            tempVar0009 = (coefs1[3] * coefs2[3]);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (-1 * coefs1[1] * tempVar0008);
            tempVar0007 = (tempVar0007 + tempVar0009);
            tempVar0009 = (coefs1[3] * tempVar0004);
            tempVar0007 = (tempVar0007 + tempVar0009);
            tempVar0003 = (-1 * tempVar0001 * tempVar0003);
            tempVar0003 = (tempVar0007 + tempVar0003);
            tempVar0006 = (-1 * tempVar0002 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            c[1] = (-1 * tempVar0003);
            tempVar0003 = (-1 * coefs2[2] * tempVar0001);
            tempVar0006 = (coefs1[2] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0006 = (-1 * coefs2[2] * tempVar0002);
            tempVar0007 = (-1 * coefs1[2] * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (tempVar0000 * tempVar0006);
            tempVar0007 = (tempVar0003 + tempVar0007);
            tempVar0009 = (tempVar0000 * tempVar0007);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0006 = (tempVar0009 + tempVar0003);
            tempVar0007 = (-1 * tempVar0000 * tempVar0007);
            tempVar0003 = (tempVar0003 + tempVar0007);
            tempVar0005 = (coefs1[3] * tempVar0005);
            tempVar0007 = (-1 * coefs1[1] * coefs2[2]);
            tempVar0009 = (-1 * coefs1[2] * coefs2[3]);
            tempVar0007 = (tempVar0007 + tempVar0009);
            tempVar0009 = (-1 * coefs1[1] * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0009);
            tempVar0009 = (coefs1[2] * tempVar0004);
            tempVar0005 = (tempVar0005 + tempVar0009);
            tempVar0003 = (-1 * tempVar0001 * tempVar0003);
            tempVar0003 = (tempVar0005 + tempVar0003);
            tempVar0005 = (-1 * tempVar0002 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0005);
            c[2] = (-1 * tempVar0003);
            tempVar0003 = (coefs2[3] * tempVar0001);
            tempVar0005 = (coefs1[1] * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0005 = (coefs2[3] * tempVar0002);
            tempVar0006 = (-1 * coefs1[1] * tempVar0005);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (tempVar0000 * tempVar0005);
            tempVar0006 = (tempVar0003 + tempVar0006);
            tempVar0009 = (tempVar0000 * tempVar0006);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0005 = (tempVar0009 + tempVar0003);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0003 = (tempVar0003 + tempVar0006);
            tempVar0006 = (-1 * coefs1[3] * tempVar0008);
            tempVar0007 = (-1 * coefs1[2] * tempVar0007);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0004 = (-1 * coefs1[1] * tempVar0004);
            tempVar0004 = (tempVar0006 + tempVar0004);
            tempVar0001 = (tempVar0001 * tempVar0003);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0002 = (tempVar0002 * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0002);
            c[3] = (-1 * tempVar0001);
            tempVar0001 = (-1 * coefs1[3] * tempVar0006);
            tempVar0002 = (-1 * coefs1[2] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * coefs1[1] * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * tempVar0001 * tempVar0005);
            tempVar0003 = (-1 * tempVar0004 * tempVar0002);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * tempVar0001 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (-1 * tempVar0002 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0001 = (tempVar0000 * tempVar0001);
            tempVar0003 = (coefs1[3] * tempVar0003);
            tempVar0004 = (coefs1[2] * tempVar0003);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (coefs1[1] * tempVar0003);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (tempVar0001 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0002 = (-1 * tempVar0002 * tempVar0002);
            tempVar0002 = (tempVar0003 + tempVar0002);
            tempVar0003 = (-1 * tempVar0000 * tempVar0002);
            tempVar0003 = (tempVar0001 + tempVar0003);
            c[0] = (-1 * tempVar0003);
            tempVar0000 = (tempVar0000 * tempVar0002);
            tempVar0000 = (tempVar0001 + tempVar0000);
            c[4] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:57.5222982+02:00
            
            return c;
        }
        
        
    }
}
