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
        private static double[] ApplyRotor_522(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:43.5985018+02:00
            //Macro: geometry3d.cga.ApplyRotor
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 72 sub-expressions, 0 generated temps, 72 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 10 total.
            //Computations: 1.32926829268293 average, 109 total.
            //Memory Reads: 1.96341463414634 average, 161 total.
            //Memory Writes: 82 total.
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
            //   v.#no^e1^e2^e3^ni# <=> <Variable> coefs1[0]
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
            
            tempVar0000 = (coefs1[0] * coefs2[2]);
            c[2] = (-1 * coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[4]);
            c[4] = (coefs1[0] * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[5]);
            c[5] = (coefs1[0] * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[6]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[6] = (-1 * tempVar0000);
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (coefs2[0] * tempVar0000);
            tempVar0002 = (coefs2[7] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (coefs1[0] * tempVar0001);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0003 = (-1 * coefs2[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (-1 * coefs1[0] * tempVar0002);
            tempVar0003 = (tempVar0000 * tempVar0002);
            tempVar0003 = (tempVar0001 + tempVar0003);
            tempVar0004 = (tempVar0000 * tempVar0003);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0002 = (tempVar0004 + tempVar0001);
            tempVar0002 = (coefs1[0] * tempVar0002);
            tempVar0004 = (-1 * tempVar0000 * tempVar0002);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            tempVar0001 = (tempVar0000 * tempVar0001);
            c[0] = (tempVar0004 + tempVar0001);
            tempVar0003 = (coefs2[1] * tempVar0000);
            tempVar0004 = (coefs2[8] * tempVar0000);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (coefs1[0] * tempVar0003);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0005 = (-1 * coefs2[1] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0004 = (-1 * coefs1[0] * tempVar0004);
            tempVar0005 = (tempVar0000 * tempVar0004);
            tempVar0005 = (tempVar0003 + tempVar0005);
            tempVar0006 = (tempVar0000 * tempVar0005);
            tempVar0004 = (-1 * tempVar0000 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0004 = (tempVar0006 + tempVar0003);
            tempVar0004 = (coefs1[0] * tempVar0004);
            tempVar0006 = (-1 * tempVar0000 * tempVar0004);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0003 = (-1 * coefs1[0] * tempVar0003);
            tempVar0003 = (tempVar0000 * tempVar0003);
            c[1] = (tempVar0006 + tempVar0003);
            tempVar0005 = (coefs2[3] * tempVar0000);
            tempVar0006 = (coefs2[9] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (-1 * coefs1[0] * tempVar0005);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0007 = (-1 * coefs2[3] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0006 = (coefs1[0] * tempVar0006);
            tempVar0007 = (tempVar0000 * tempVar0006);
            tempVar0007 = (tempVar0005 + tempVar0007);
            tempVar0008 = (tempVar0000 * tempVar0007);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0006 = (tempVar0008 + tempVar0005);
            tempVar0006 = (-1 * coefs1[0] * tempVar0006);
            tempVar0008 = (-1 * tempVar0000 * tempVar0006);
            tempVar0007 = (-1 * tempVar0000 * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0005 = (coefs1[0] * tempVar0005);
            tempVar0005 = (tempVar0000 * tempVar0005);
            c[3] = (tempVar0008 + tempVar0005);
            tempVar0002 = (tempVar0000 * tempVar0002);
            c[7] = (tempVar0001 + tempVar0002);
            tempVar0001 = (tempVar0000 * tempVar0004);
            c[8] = (tempVar0003 + tempVar0001);
            tempVar0000 = (tempVar0000 * tempVar0006);
            c[9] = (tempVar0005 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:43.6075023+02:00
            
            return c;
        }
        
        
    }
}
