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
        private static double[] ApplyVersor_011(double[] coefs1, double[] coefs2)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:10.0775845+02:00
            //Macro: geometry3d.cga.ApplyVersor
            //Input Variables: 0 used, 6 not used, 6 total.
            //Temp Variables: 32 sub-expressions, 0 generated temps, 32 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 5 total.
            //Computations: 1.45945945945946 average, 54 total.
            //Memory Reads: 1.91891891891892 average, 71 total.
            //Memory Writes: 37 total.
            //
            //Macro Binding Data: 
            //   result.#no# <=> <Variable> c[0]
            //   result.#e1# <=> <Variable> c[1]
            //   result.#e2# <=> <Variable> c[2]
            //   result.#e3# <=> <Variable> c[3]
            //   result.#ni# <=> <Variable> c[4]
            //   v.#E0# <=> <Variable> coefs1[0]
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
            
            tempVar0000 = Math.Pow(coefs1[0], -2);
            tempVar0001 = (coefs1[0] * coefs2[1]);
            tempVar0001 = (coefs1[0] * tempVar0001);
            c[1] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[2]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[2] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = (-1 * coefs1[0] * coefs2[3]);
            tempVar0001 = (-1 * coefs1[0] * tempVar0001);
            c[3] = (-1 * tempVar0000 * tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs2[0] * tempVar0001);
            tempVar0003 = (coefs2[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (-1 * coefs1[0] * tempVar0002);
            tempVar0002 = (tempVar0001 * tempVar0002);
            tempVar0004 = (-1 * coefs2[0] * tempVar0001);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (-1 * coefs1[0] * tempVar0003);
            tempVar0004 = (-1 * tempVar0001 * tempVar0003);
            tempVar0004 = (tempVar0002 + tempVar0004);
            tempVar0005 = (tempVar0001 * tempVar0004);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (tempVar0001 * tempVar0002);
            tempVar0003 = (tempVar0005 + tempVar0002);
            tempVar0003 = (-1 * coefs1[0] * tempVar0003);
            tempVar0003 = (tempVar0001 * tempVar0003);
            tempVar0004 = (-1 * tempVar0001 * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0002 = (-1 * coefs1[0] * tempVar0002);
            tempVar0004 = (-1 * tempVar0001 * tempVar0002);
            tempVar0004 = (tempVar0003 + tempVar0004);
            c[0] = (-1 * tempVar0000 * tempVar0004);
            tempVar0001 = (tempVar0001 * tempVar0002);
            tempVar0001 = (tempVar0003 + tempVar0001);
            c[4] = (-1 * tempVar0000 * tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:10.0805847+02:00
            
            return c;
        }
        
        
    }
}