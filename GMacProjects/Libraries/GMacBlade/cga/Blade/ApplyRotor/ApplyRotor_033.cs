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
        private static double[] ApplyRotor_033(double[] coefs1, double[] coefs2)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:29.8297143+02:00
            //Macro: geometry3d.cga.ApplyRotor
            //Input Variables: 0 used, 11 not used, 11 total.
            //Temp Variables: 75 sub-expressions, 0 generated temps, 75 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 10 total.
            //Computations: 1.37647058823529 average, 117 total.
            //Memory Reads: 1.92941176470588 average, 164 total.
            //Memory Writes: 85 total.
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
            //   v.#E0# <=> <Variable> coefs1[0]
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
            
            tempVar0000 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[3] = (-1 * tempVar0000);
            tempVar0000 = (-1 * coefs1[0] * coefs2[4]);
            tempVar0000 = (coefs1[0] * tempVar0000);
            c[4] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[5]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[5] = (-1 * tempVar0000);
            tempVar0000 = (coefs1[0] * coefs2[7]);
            tempVar0000 = (-1 * coefs1[0] * tempVar0000);
            c[7] = (-1 * tempVar0000);
            tempVar0000 = Math.Pow(2, -0.5);
            tempVar0001 = (-1 * coefs2[0] * tempVar0000);
            tempVar0002 = (-1 * coefs2[6] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (coefs1[0] * tempVar0001);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0003 = (coefs2[0] * tempVar0000);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (coefs1[0] * tempVar0002);
            tempVar0003 = (tempVar0000 * tempVar0002);
            tempVar0003 = (tempVar0001 + tempVar0003);
            tempVar0004 = (-1 * tempVar0000 * tempVar0003);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            tempVar0002 = (tempVar0004 + tempVar0001);
            tempVar0002 = (coefs1[0] * tempVar0002);
            tempVar0002 = (-1 * tempVar0000 * tempVar0002);
            tempVar0003 = (tempVar0000 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0001 = (coefs1[0] * tempVar0001);
            tempVar0003 = (tempVar0000 * tempVar0001);
            c[0] = (tempVar0002 + tempVar0003);
            tempVar0003 = (-1 * coefs2[1] * tempVar0000);
            tempVar0004 = (-1 * coefs2[8] * tempVar0000);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (coefs1[0] * tempVar0003);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0005 = (coefs2[1] * tempVar0000);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0004 = (coefs1[0] * tempVar0004);
            tempVar0005 = (tempVar0000 * tempVar0004);
            tempVar0005 = (tempVar0003 + tempVar0005);
            tempVar0006 = (-1 * tempVar0000 * tempVar0005);
            tempVar0004 = (-1 * tempVar0000 * tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (-1 * tempVar0000 * tempVar0003);
            tempVar0004 = (tempVar0006 + tempVar0003);
            tempVar0004 = (coefs1[0] * tempVar0004);
            tempVar0004 = (-1 * tempVar0000 * tempVar0004);
            tempVar0005 = (tempVar0000 * tempVar0005);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0003 = (coefs1[0] * tempVar0003);
            tempVar0005 = (tempVar0000 * tempVar0003);
            c[1] = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * coefs2[2] * tempVar0000);
            tempVar0006 = (-1 * coefs2[9] * tempVar0000);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (-1 * coefs1[0] * tempVar0005);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0007 = (coefs2[2] * tempVar0000);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0006 = (-1 * coefs1[0] * tempVar0006);
            tempVar0007 = (tempVar0000 * tempVar0006);
            tempVar0007 = (tempVar0005 + tempVar0007);
            tempVar0008 = (-1 * tempVar0000 * tempVar0007);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0005 = (-1 * tempVar0000 * tempVar0005);
            tempVar0006 = (tempVar0008 + tempVar0005);
            tempVar0006 = (-1 * coefs1[0] * tempVar0006);
            tempVar0006 = (-1 * tempVar0000 * tempVar0006);
            tempVar0007 = (tempVar0000 * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0005 = (-1 * coefs1[0] * tempVar0005);
            tempVar0007 = (tempVar0000 * tempVar0005);
            c[2] = (tempVar0006 + tempVar0007);
            tempVar0001 = (-1 * tempVar0000 * tempVar0001);
            c[6] = (tempVar0002 + tempVar0001);
            tempVar0001 = (-1 * tempVar0000 * tempVar0003);
            c[8] = (tempVar0004 + tempVar0001);
            tempVar0000 = (-1 * tempVar0000 * tempVar0005);
            c[9] = (tempVar0006 + tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:29.8387148+02:00
            
            return c;
        }
        
        
    }
}