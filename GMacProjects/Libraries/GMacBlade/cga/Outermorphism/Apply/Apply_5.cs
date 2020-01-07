namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents a mutable outermorphism in the cga0001 frame by only storing a 5 by 5
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class cga0001Outermorphism
    {
        public static double[] Apply_5(double[,] omCoefs, double[] bladeCoefs)
        {
            var coefs = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:30.6481929+02:00
            //Macro: geometry3d.cga.ApplyOM
            //Input Variables: 0 used, 26 not used, 26 total.
            //Temp Variables: 124 sub-expressions, 0 generated temps, 124 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.344 average, 168 total.
            //Memory Reads: 2 average, 250 total.
            //Memory Writes: 125 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> bladeCoefs[0]
            //   om.ImageV1.#no# <=> <Variable> omCoefs[0, 0]
            //   om.ImageV2.#no# <=> <Variable> omCoefs[0, 1]
            //   om.ImageV3.#no# <=> <Variable> omCoefs[0, 2]
            //   om.ImageV4.#no# <=> <Variable> omCoefs[0, 3]
            //   om.ImageV5.#no# <=> <Variable> omCoefs[0, 4]
            //   om.ImageV1.#e1# <=> <Variable> omCoefs[1, 0]
            //   om.ImageV2.#e1# <=> <Variable> omCoefs[1, 1]
            //   om.ImageV3.#e1# <=> <Variable> omCoefs[1, 2]
            //   om.ImageV4.#e1# <=> <Variable> omCoefs[1, 3]
            //   om.ImageV5.#e1# <=> <Variable> omCoefs[1, 4]
            //   om.ImageV1.#e2# <=> <Variable> omCoefs[2, 0]
            //   om.ImageV2.#e2# <=> <Variable> omCoefs[2, 1]
            //   om.ImageV3.#e2# <=> <Variable> omCoefs[2, 2]
            //   om.ImageV4.#e2# <=> <Variable> omCoefs[2, 3]
            //   om.ImageV5.#e2# <=> <Variable> omCoefs[2, 4]
            //   om.ImageV1.#e3# <=> <Variable> omCoefs[3, 0]
            //   om.ImageV2.#e3# <=> <Variable> omCoefs[3, 1]
            //   om.ImageV3.#e3# <=> <Variable> omCoefs[3, 2]
            //   om.ImageV4.#e3# <=> <Variable> omCoefs[3, 3]
            //   om.ImageV5.#e3# <=> <Variable> omCoefs[3, 4]
            //   om.ImageV1.#ni# <=> <Variable> omCoefs[4, 0]
            //   om.ImageV2.#ni# <=> <Variable> omCoefs[4, 1]
            //   om.ImageV3.#ni# <=> <Variable> omCoefs[4, 2]
            //   om.ImageV4.#ni# <=> <Variable> omCoefs[4, 3]
            //   om.ImageV5.#ni# <=> <Variable> omCoefs[4, 4]
            
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
            
            tempVar0000 = (omCoefs[0, 4] * omCoefs[1, 3]);
            tempVar0001 = (-1 * omCoefs[0, 3] * omCoefs[1, 4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * omCoefs[2, 2] * tempVar0000);
            tempVar0002 = (omCoefs[0, 4] * omCoefs[2, 3]);
            tempVar0003 = (-1 * omCoefs[0, 3] * omCoefs[2, 4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (omCoefs[1, 2] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (omCoefs[1, 4] * omCoefs[2, 3]);
            tempVar0004 = (-1 * omCoefs[1, 3] * omCoefs[2, 4]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * omCoefs[0, 2] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (omCoefs[3, 1] * tempVar0001);
            tempVar0005 = (-1 * omCoefs[3, 2] * tempVar0000);
            tempVar0006 = (omCoefs[0, 4] * omCoefs[3, 3]);
            tempVar0007 = (-1 * omCoefs[0, 3] * omCoefs[3, 4]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (omCoefs[1, 2] * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (omCoefs[1, 4] * omCoefs[3, 3]);
            tempVar0008 = (-1 * omCoefs[1, 3] * omCoefs[3, 4]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * omCoefs[0, 2] * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (-1 * omCoefs[2, 1] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0008);
            tempVar0008 = (-1 * omCoefs[3, 2] * tempVar0002);
            tempVar0009 = (omCoefs[2, 2] * tempVar0006);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (omCoefs[2, 4] * omCoefs[3, 3]);
            tempVar000A = (-1 * omCoefs[2, 3] * omCoefs[3, 4]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * omCoefs[0, 2] * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar000A = (omCoefs[1, 1] * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * omCoefs[3, 2] * tempVar0003);
            tempVar000B = (omCoefs[2, 2] * tempVar0007);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * omCoefs[1, 2] * tempVar0009);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * omCoefs[0, 1] * tempVar000A);
            tempVar0004 = (tempVar0004 + tempVar000B);
            tempVar0004 = (-1 * omCoefs[4, 0] * tempVar0004);
            tempVar0001 = (omCoefs[4, 1] * tempVar0001);
            tempVar0000 = (-1 * omCoefs[4, 2] * tempVar0000);
            tempVar000B = (omCoefs[0, 4] * omCoefs[4, 3]);
            tempVar000C = (-1 * omCoefs[0, 3] * omCoefs[4, 4]);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (omCoefs[1, 2] * tempVar000B);
            tempVar0000 = (tempVar0000 + tempVar000C);
            tempVar000C = (omCoefs[1, 4] * omCoefs[4, 3]);
            tempVar000D = (-1 * omCoefs[1, 3] * omCoefs[4, 4]);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * omCoefs[0, 2] * tempVar000C);
            tempVar0000 = (tempVar0000 + tempVar000D);
            tempVar000D = (-1 * omCoefs[2, 1] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar000D);
            tempVar0002 = (-1 * omCoefs[4, 2] * tempVar0002);
            tempVar000D = (omCoefs[2, 2] * tempVar000B);
            tempVar0002 = (tempVar0002 + tempVar000D);
            tempVar000D = (omCoefs[2, 4] * omCoefs[4, 3]);
            tempVar000E = (-1 * omCoefs[2, 3] * omCoefs[4, 4]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar000E = (-1 * omCoefs[0, 2] * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar000E);
            tempVar000E = (omCoefs[1, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0003 = (-1 * omCoefs[4, 2] * tempVar0003);
            tempVar000E = (omCoefs[2, 2] * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * omCoefs[1, 2] * tempVar000D);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * omCoefs[0, 1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0001 = (omCoefs[3, 0] * tempVar0001);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0004 = (omCoefs[4, 1] * tempVar0005);
            tempVar0000 = (-1 * omCoefs[3, 1] * tempVar0000);
            tempVar0000 = (tempVar0004 + tempVar0000);
            tempVar0004 = (-1 * omCoefs[4, 2] * tempVar0006);
            tempVar0005 = (omCoefs[3, 2] * tempVar000B);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (omCoefs[3, 4] * omCoefs[4, 3]);
            tempVar0006 = (-1 * omCoefs[3, 3] * omCoefs[4, 4]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * omCoefs[0, 2] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (omCoefs[1, 1] * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0006);
            tempVar0006 = (-1 * omCoefs[4, 2] * tempVar0007);
            tempVar0007 = (omCoefs[3, 2] * tempVar000C);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * omCoefs[1, 2] * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * omCoefs[0, 1] * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0007);
            tempVar0000 = (-1 * omCoefs[2, 0] * tempVar0000);
            tempVar0000 = (tempVar0001 + tempVar0000);
            tempVar0001 = (omCoefs[4, 1] * tempVar0008);
            tempVar0002 = (-1 * omCoefs[3, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (omCoefs[2, 1] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * omCoefs[4, 2] * tempVar0009);
            tempVar0004 = (omCoefs[3, 2] * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * omCoefs[2, 2] * tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * omCoefs[0, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0001 = (omCoefs[1, 0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (omCoefs[4, 1] * tempVar000A);
            tempVar0003 = (-1 * omCoefs[3, 1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (omCoefs[2, 1] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0002 = (-1 * omCoefs[1, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * omCoefs[0, 0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[0] = (bladeCoefs[0] * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:30.6611936+02:00
            
            return coefs;
        }
        
        
    }
}
