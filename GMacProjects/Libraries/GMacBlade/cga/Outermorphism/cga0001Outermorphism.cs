using System.IO;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents a mutable outermorphism in the cga0001 frame by only storing a 5 by 5
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class cga0001Outermorphism
    {
        public double[,] Coefs { get; private set; }
        
        
        public cga0001Outermorphism()
        {
            Coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        }
        
        private cga0001Outermorphism(double[,] coefs)
        {
            Coefs = coefs;
        }
        
        
        public cga0001Outermorphism Transpose()
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = Coefs[0, 0];
            coefs[0, 1] = Coefs[1, 0];
            coefs[0, 2] = Coefs[2, 0];
            coefs[0, 3] = Coefs[3, 0];
            coefs[0, 4] = Coefs[4, 0];
            coefs[1, 0] = Coefs[0, 1];
            coefs[1, 1] = Coefs[1, 1];
            coefs[1, 2] = Coefs[2, 1];
            coefs[1, 3] = Coefs[3, 1];
            coefs[1, 4] = Coefs[4, 1];
            coefs[2, 0] = Coefs[0, 2];
            coefs[2, 1] = Coefs[1, 2];
            coefs[2, 2] = Coefs[2, 2];
            coefs[2, 3] = Coefs[3, 2];
            coefs[2, 4] = Coefs[4, 2];
            coefs[3, 0] = Coefs[0, 3];
            coefs[3, 1] = Coefs[1, 3];
            coefs[3, 2] = Coefs[2, 3];
            coefs[3, 3] = Coefs[3, 3];
            coefs[3, 4] = Coefs[4, 3];
            coefs[4, 0] = Coefs[0, 4];
            coefs[4, 1] = Coefs[1, 4];
            coefs[4, 2] = Coefs[2, 4];
            coefs[4, 3] = Coefs[3, 4];
            coefs[4, 4] = Coefs[4, 4];
        
            return new cga0001Outermorphism(coefs);
        }
        
        public double MetricDeterminant()
        {
            double det;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:24.9808687+02:00
            //Macro: geometry3d.cga.DetOM
            //Input Variables: 0 used, 25 not used, 25 total.
            //Temp Variables: 123 sub-expressions, 0 generated temps, 123 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.34677419354839 average, 167 total.
            //Memory Reads: 2 average, 248 total.
            //Memory Writes: 124 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> det
            //   om.ImageV1.#no# <=> <Variable> Coefs[0, 0]
            //   om.ImageV2.#no# <=> <Variable> Coefs[0, 1]
            //   om.ImageV3.#no# <=> <Variable> Coefs[0, 2]
            //   om.ImageV4.#no# <=> <Variable> Coefs[0, 3]
            //   om.ImageV5.#no# <=> <Variable> Coefs[0, 4]
            //   om.ImageV1.#e1# <=> <Variable> Coefs[1, 0]
            //   om.ImageV2.#e1# <=> <Variable> Coefs[1, 1]
            //   om.ImageV3.#e1# <=> <Variable> Coefs[1, 2]
            //   om.ImageV4.#e1# <=> <Variable> Coefs[1, 3]
            //   om.ImageV5.#e1# <=> <Variable> Coefs[1, 4]
            //   om.ImageV1.#e2# <=> <Variable> Coefs[2, 0]
            //   om.ImageV2.#e2# <=> <Variable> Coefs[2, 1]
            //   om.ImageV3.#e2# <=> <Variable> Coefs[2, 2]
            //   om.ImageV4.#e2# <=> <Variable> Coefs[2, 3]
            //   om.ImageV5.#e2# <=> <Variable> Coefs[2, 4]
            //   om.ImageV1.#e3# <=> <Variable> Coefs[3, 0]
            //   om.ImageV2.#e3# <=> <Variable> Coefs[3, 1]
            //   om.ImageV3.#e3# <=> <Variable> Coefs[3, 2]
            //   om.ImageV4.#e3# <=> <Variable> Coefs[3, 3]
            //   om.ImageV5.#e3# <=> <Variable> Coefs[3, 4]
            //   om.ImageV1.#ni# <=> <Variable> Coefs[4, 0]
            //   om.ImageV2.#ni# <=> <Variable> Coefs[4, 1]
            //   om.ImageV3.#ni# <=> <Variable> Coefs[4, 2]
            //   om.ImageV4.#ni# <=> <Variable> Coefs[4, 3]
            //   om.ImageV5.#ni# <=> <Variable> Coefs[4, 4]
            
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
            
            tempVar0000 = (Coefs[0, 4] * Coefs[1, 3]);
            tempVar0001 = (-1 * Coefs[0, 3] * Coefs[1, 4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * Coefs[2, 2] * tempVar0000);
            tempVar0002 = (Coefs[0, 4] * Coefs[2, 3]);
            tempVar0003 = (-1 * Coefs[0, 3] * Coefs[2, 4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (Coefs[1, 2] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (Coefs[1, 4] * Coefs[2, 3]);
            tempVar0004 = (-1 * Coefs[1, 3] * Coefs[2, 4]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * Coefs[0, 2] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (Coefs[3, 1] * tempVar0001);
            tempVar0005 = (-1 * Coefs[3, 2] * tempVar0000);
            tempVar0006 = (Coefs[0, 4] * Coefs[3, 3]);
            tempVar0007 = (-1 * Coefs[0, 3] * Coefs[3, 4]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (Coefs[1, 2] * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (Coefs[1, 4] * Coefs[3, 3]);
            tempVar0008 = (-1 * Coefs[1, 3] * Coefs[3, 4]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * Coefs[0, 2] * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (-1 * Coefs[2, 1] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0008);
            tempVar0008 = (-1 * Coefs[3, 2] * tempVar0002);
            tempVar0009 = (Coefs[2, 2] * tempVar0006);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (Coefs[2, 4] * Coefs[3, 3]);
            tempVar000A = (-1 * Coefs[2, 3] * Coefs[3, 4]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * Coefs[0, 2] * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar000A = (Coefs[1, 1] * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * Coefs[3, 2] * tempVar0003);
            tempVar000B = (Coefs[2, 2] * tempVar0007);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * Coefs[1, 2] * tempVar0009);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * Coefs[0, 1] * tempVar000A);
            tempVar0004 = (tempVar0004 + tempVar000B);
            tempVar0004 = (-1 * Coefs[4, 0] * tempVar0004);
            tempVar0001 = (Coefs[4, 1] * tempVar0001);
            tempVar0000 = (-1 * Coefs[4, 2] * tempVar0000);
            tempVar000B = (Coefs[0, 4] * Coefs[4, 3]);
            tempVar000C = (-1 * Coefs[0, 3] * Coefs[4, 4]);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (Coefs[1, 2] * tempVar000B);
            tempVar0000 = (tempVar0000 + tempVar000C);
            tempVar000C = (Coefs[1, 4] * Coefs[4, 3]);
            tempVar000D = (-1 * Coefs[1, 3] * Coefs[4, 4]);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * Coefs[0, 2] * tempVar000C);
            tempVar0000 = (tempVar0000 + tempVar000D);
            tempVar000D = (-1 * Coefs[2, 1] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar000D);
            tempVar0002 = (-1 * Coefs[4, 2] * tempVar0002);
            tempVar000D = (Coefs[2, 2] * tempVar000B);
            tempVar0002 = (tempVar0002 + tempVar000D);
            tempVar000D = (Coefs[2, 4] * Coefs[4, 3]);
            tempVar000E = (-1 * Coefs[2, 3] * Coefs[4, 4]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar000E = (-1 * Coefs[0, 2] * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar000E);
            tempVar000E = (Coefs[1, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0003 = (-1 * Coefs[4, 2] * tempVar0003);
            tempVar000E = (Coefs[2, 2] * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * Coefs[1, 2] * tempVar000D);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * Coefs[0, 1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0001 = (Coefs[3, 0] * tempVar0001);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0004 = (Coefs[4, 1] * tempVar0005);
            tempVar0000 = (-1 * Coefs[3, 1] * tempVar0000);
            tempVar0000 = (tempVar0004 + tempVar0000);
            tempVar0004 = (-1 * Coefs[4, 2] * tempVar0006);
            tempVar0005 = (Coefs[3, 2] * tempVar000B);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (Coefs[3, 4] * Coefs[4, 3]);
            tempVar0006 = (-1 * Coefs[3, 3] * Coefs[4, 4]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * Coefs[0, 2] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (Coefs[1, 1] * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0006);
            tempVar0006 = (-1 * Coefs[4, 2] * tempVar0007);
            tempVar0007 = (Coefs[3, 2] * tempVar000C);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * Coefs[1, 2] * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * Coefs[0, 1] * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0007);
            tempVar0000 = (-1 * Coefs[2, 0] * tempVar0000);
            tempVar0000 = (tempVar0001 + tempVar0000);
            tempVar0001 = (Coefs[4, 1] * tempVar0008);
            tempVar0002 = (-1 * Coefs[3, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (Coefs[2, 1] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * Coefs[4, 2] * tempVar0009);
            tempVar0004 = (Coefs[3, 2] * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * Coefs[2, 2] * tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * Coefs[0, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0001 = (Coefs[1, 0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (Coefs[4, 1] * tempVar000A);
            tempVar0003 = (-1 * Coefs[3, 1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (Coefs[2, 1] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0002 = (-1 * Coefs[1, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * Coefs[0, 0] * tempVar0001);
            det = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:24.9978697+02:00
            
        
            return det;
        }
        
        public double EuclideanDeterminant()
        {
            double det;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:47:25.8309174+02:00
            //Macro: geometry3d.cga.EDetOM
            //Input Variables: 0 used, 25 not used, 25 total.
            //Temp Variables: 123 sub-expressions, 0 generated temps, 123 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.34677419354839 average, 167 total.
            //Memory Reads: 2 average, 248 total.
            //Memory Writes: 124 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> det
            //   om.ImageV1.#no# <=> <Variable> Coefs[0, 0]
            //   om.ImageV2.#no# <=> <Variable> Coefs[0, 1]
            //   om.ImageV3.#no# <=> <Variable> Coefs[0, 2]
            //   om.ImageV4.#no# <=> <Variable> Coefs[0, 3]
            //   om.ImageV5.#no# <=> <Variable> Coefs[0, 4]
            //   om.ImageV1.#e1# <=> <Variable> Coefs[1, 0]
            //   om.ImageV2.#e1# <=> <Variable> Coefs[1, 1]
            //   om.ImageV3.#e1# <=> <Variable> Coefs[1, 2]
            //   om.ImageV4.#e1# <=> <Variable> Coefs[1, 3]
            //   om.ImageV5.#e1# <=> <Variable> Coefs[1, 4]
            //   om.ImageV1.#e2# <=> <Variable> Coefs[2, 0]
            //   om.ImageV2.#e2# <=> <Variable> Coefs[2, 1]
            //   om.ImageV3.#e2# <=> <Variable> Coefs[2, 2]
            //   om.ImageV4.#e2# <=> <Variable> Coefs[2, 3]
            //   om.ImageV5.#e2# <=> <Variable> Coefs[2, 4]
            //   om.ImageV1.#e3# <=> <Variable> Coefs[3, 0]
            //   om.ImageV2.#e3# <=> <Variable> Coefs[3, 1]
            //   om.ImageV3.#e3# <=> <Variable> Coefs[3, 2]
            //   om.ImageV4.#e3# <=> <Variable> Coefs[3, 3]
            //   om.ImageV5.#e3# <=> <Variable> Coefs[3, 4]
            //   om.ImageV1.#ni# <=> <Variable> Coefs[4, 0]
            //   om.ImageV2.#ni# <=> <Variable> Coefs[4, 1]
            //   om.ImageV3.#ni# <=> <Variable> Coefs[4, 2]
            //   om.ImageV4.#ni# <=> <Variable> Coefs[4, 3]
            //   om.ImageV5.#ni# <=> <Variable> Coefs[4, 4]
            
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
            
            tempVar0000 = (Coefs[0, 4] * Coefs[1, 3]);
            tempVar0001 = (-1 * Coefs[0, 3] * Coefs[1, 4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * Coefs[2, 2] * tempVar0000);
            tempVar0002 = (Coefs[0, 4] * Coefs[2, 3]);
            tempVar0003 = (-1 * Coefs[0, 3] * Coefs[2, 4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (Coefs[1, 2] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (Coefs[1, 4] * Coefs[2, 3]);
            tempVar0004 = (-1 * Coefs[1, 3] * Coefs[2, 4]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * Coefs[0, 2] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (Coefs[3, 1] * tempVar0001);
            tempVar0005 = (-1 * Coefs[3, 2] * tempVar0000);
            tempVar0006 = (Coefs[0, 4] * Coefs[3, 3]);
            tempVar0007 = (-1 * Coefs[0, 3] * Coefs[3, 4]);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (Coefs[1, 2] * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (Coefs[1, 4] * Coefs[3, 3]);
            tempVar0008 = (-1 * Coefs[1, 3] * Coefs[3, 4]);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * Coefs[0, 2] * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (-1 * Coefs[2, 1] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0008);
            tempVar0008 = (-1 * Coefs[3, 2] * tempVar0002);
            tempVar0009 = (Coefs[2, 2] * tempVar0006);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (Coefs[2, 4] * Coefs[3, 3]);
            tempVar000A = (-1 * Coefs[2, 3] * Coefs[3, 4]);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * Coefs[0, 2] * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar000A = (Coefs[1, 1] * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * Coefs[3, 2] * tempVar0003);
            tempVar000B = (Coefs[2, 2] * tempVar0007);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * Coefs[1, 2] * tempVar0009);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * Coefs[0, 1] * tempVar000A);
            tempVar0004 = (tempVar0004 + tempVar000B);
            tempVar0004 = (-1 * Coefs[4, 0] * tempVar0004);
            tempVar0001 = (Coefs[4, 1] * tempVar0001);
            tempVar0000 = (-1 * Coefs[4, 2] * tempVar0000);
            tempVar000B = (Coefs[0, 4] * Coefs[4, 3]);
            tempVar000C = (-1 * Coefs[0, 3] * Coefs[4, 4]);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (Coefs[1, 2] * tempVar000B);
            tempVar0000 = (tempVar0000 + tempVar000C);
            tempVar000C = (Coefs[1, 4] * Coefs[4, 3]);
            tempVar000D = (-1 * Coefs[1, 3] * Coefs[4, 4]);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * Coefs[0, 2] * tempVar000C);
            tempVar0000 = (tempVar0000 + tempVar000D);
            tempVar000D = (-1 * Coefs[2, 1] * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar000D);
            tempVar0002 = (-1 * Coefs[4, 2] * tempVar0002);
            tempVar000D = (Coefs[2, 2] * tempVar000B);
            tempVar0002 = (tempVar0002 + tempVar000D);
            tempVar000D = (Coefs[2, 4] * Coefs[4, 3]);
            tempVar000E = (-1 * Coefs[2, 3] * Coefs[4, 4]);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar000E = (-1 * Coefs[0, 2] * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar000E);
            tempVar000E = (Coefs[1, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0003 = (-1 * Coefs[4, 2] * tempVar0003);
            tempVar000E = (Coefs[2, 2] * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * Coefs[1, 2] * tempVar000D);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * Coefs[0, 1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0001 = (Coefs[3, 0] * tempVar0001);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0004 = (Coefs[4, 1] * tempVar0005);
            tempVar0000 = (-1 * Coefs[3, 1] * tempVar0000);
            tempVar0000 = (tempVar0004 + tempVar0000);
            tempVar0004 = (-1 * Coefs[4, 2] * tempVar0006);
            tempVar0005 = (Coefs[3, 2] * tempVar000B);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (Coefs[3, 4] * Coefs[4, 3]);
            tempVar0006 = (-1 * Coefs[3, 3] * Coefs[4, 4]);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * Coefs[0, 2] * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (Coefs[1, 1] * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0006);
            tempVar0006 = (-1 * Coefs[4, 2] * tempVar0007);
            tempVar0007 = (Coefs[3, 2] * tempVar000C);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * Coefs[1, 2] * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * Coefs[0, 1] * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0007);
            tempVar0000 = (-1 * Coefs[2, 0] * tempVar0000);
            tempVar0000 = (tempVar0001 + tempVar0000);
            tempVar0001 = (Coefs[4, 1] * tempVar0008);
            tempVar0002 = (-1 * Coefs[3, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (Coefs[2, 1] * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * Coefs[4, 2] * tempVar0009);
            tempVar0004 = (Coefs[3, 2] * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * Coefs[2, 2] * tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * Coefs[0, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0001 = (Coefs[1, 0] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (Coefs[4, 1] * tempVar000A);
            tempVar0003 = (-1 * Coefs[3, 1] * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (Coefs[2, 1] * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0002 = (-1 * Coefs[1, 1] * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * Coefs[0, 0] * tempVar0001);
            det = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:47:25.8449182+02:00
            
        
            return det;
        }
        
        public cga0001Blade Apply(cga0001Blade blade)
        {
            if (blade.IsZero)
                return cga0001Blade.ZeroBlade;
        
            switch (blade.Grade)
            {
                case 0:
                    return blade;
                case 1:
                    return new cga0001Blade(1, Apply_1(Coefs, blade.Coefs));
                case 2:
                    return new cga0001Blade(2, Apply_2(Coefs, blade.Coefs));
                case 3:
                    return new cga0001Blade(3, Apply_3(Coefs, blade.Coefs));
                case 4:
                    return new cga0001Blade(4, Apply_4(Coefs, blade.Coefs));
                case 5:
                    return new cga0001Blade(5, Apply_5(Coefs, blade.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        
        public static cga0001Outermorphism operator +(cga0001Outermorphism om1, cga0001Outermorphism om2)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = om1.Coefs[0, 0] + om2.Coefs[0, 0];
            coefs[0, 1] = om1.Coefs[0, 1] + om2.Coefs[0, 1];
            coefs[0, 2] = om1.Coefs[0, 2] + om2.Coefs[0, 2];
            coefs[0, 3] = om1.Coefs[0, 3] + om2.Coefs[0, 3];
            coefs[0, 4] = om1.Coefs[0, 4] + om2.Coefs[0, 4];
            coefs[1, 0] = om1.Coefs[1, 0] + om2.Coefs[1, 0];
            coefs[1, 1] = om1.Coefs[1, 1] + om2.Coefs[1, 1];
            coefs[1, 2] = om1.Coefs[1, 2] + om2.Coefs[1, 2];
            coefs[1, 3] = om1.Coefs[1, 3] + om2.Coefs[1, 3];
            coefs[1, 4] = om1.Coefs[1, 4] + om2.Coefs[1, 4];
            coefs[2, 0] = om1.Coefs[2, 0] + om2.Coefs[2, 0];
            coefs[2, 1] = om1.Coefs[2, 1] + om2.Coefs[2, 1];
            coefs[2, 2] = om1.Coefs[2, 2] + om2.Coefs[2, 2];
            coefs[2, 3] = om1.Coefs[2, 3] + om2.Coefs[2, 3];
            coefs[2, 4] = om1.Coefs[2, 4] + om2.Coefs[2, 4];
            coefs[3, 0] = om1.Coefs[3, 0] + om2.Coefs[3, 0];
            coefs[3, 1] = om1.Coefs[3, 1] + om2.Coefs[3, 1];
            coefs[3, 2] = om1.Coefs[3, 2] + om2.Coefs[3, 2];
            coefs[3, 3] = om1.Coefs[3, 3] + om2.Coefs[3, 3];
            coefs[3, 4] = om1.Coefs[3, 4] + om2.Coefs[3, 4];
            coefs[4, 0] = om1.Coefs[4, 0] + om2.Coefs[4, 0];
            coefs[4, 1] = om1.Coefs[4, 1] + om2.Coefs[4, 1];
            coefs[4, 2] = om1.Coefs[4, 2] + om2.Coefs[4, 2];
            coefs[4, 3] = om1.Coefs[4, 3] + om2.Coefs[4, 3];
            coefs[4, 4] = om1.Coefs[4, 4] + om2.Coefs[4, 4];
        
            return new cga0001Outermorphism(coefs);
        }
        
        public static cga0001Outermorphism operator -(cga0001Outermorphism om1, cga0001Outermorphism om2)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = om1.Coefs[0, 0] - om2.Coefs[0, 0];
            coefs[0, 1] = om1.Coefs[0, 1] - om2.Coefs[0, 1];
            coefs[0, 2] = om1.Coefs[0, 2] - om2.Coefs[0, 2];
            coefs[0, 3] = om1.Coefs[0, 3] - om2.Coefs[0, 3];
            coefs[0, 4] = om1.Coefs[0, 4] - om2.Coefs[0, 4];
            coefs[1, 0] = om1.Coefs[1, 0] - om2.Coefs[1, 0];
            coefs[1, 1] = om1.Coefs[1, 1] - om2.Coefs[1, 1];
            coefs[1, 2] = om1.Coefs[1, 2] - om2.Coefs[1, 2];
            coefs[1, 3] = om1.Coefs[1, 3] - om2.Coefs[1, 3];
            coefs[1, 4] = om1.Coefs[1, 4] - om2.Coefs[1, 4];
            coefs[2, 0] = om1.Coefs[2, 0] - om2.Coefs[2, 0];
            coefs[2, 1] = om1.Coefs[2, 1] - om2.Coefs[2, 1];
            coefs[2, 2] = om1.Coefs[2, 2] - om2.Coefs[2, 2];
            coefs[2, 3] = om1.Coefs[2, 3] - om2.Coefs[2, 3];
            coefs[2, 4] = om1.Coefs[2, 4] - om2.Coefs[2, 4];
            coefs[3, 0] = om1.Coefs[3, 0] - om2.Coefs[3, 0];
            coefs[3, 1] = om1.Coefs[3, 1] - om2.Coefs[3, 1];
            coefs[3, 2] = om1.Coefs[3, 2] - om2.Coefs[3, 2];
            coefs[3, 3] = om1.Coefs[3, 3] - om2.Coefs[3, 3];
            coefs[3, 4] = om1.Coefs[3, 4] - om2.Coefs[3, 4];
            coefs[4, 0] = om1.Coefs[4, 0] - om2.Coefs[4, 0];
            coefs[4, 1] = om1.Coefs[4, 1] - om2.Coefs[4, 1];
            coefs[4, 2] = om1.Coefs[4, 2] - om2.Coefs[4, 2];
            coefs[4, 3] = om1.Coefs[4, 3] - om2.Coefs[4, 3];
            coefs[4, 4] = om1.Coefs[4, 4] - om2.Coefs[4, 4];
        
            return new cga0001Outermorphism(coefs);
        }
        
        public static cga0001Outermorphism operator *(cga0001Outermorphism om1, cga0001Outermorphism om2)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = om1.Coefs[0, 0] * om2.Coefs[0, 0] + om1.Coefs[0, 1] * om2.Coefs[1, 0] + om1.Coefs[0, 2] * om2.Coefs[2, 0] + om1.Coefs[0, 3] * om2.Coefs[3, 0] + om1.Coefs[0, 4] * om2.Coefs[4, 0];
            coefs[0, 1] = om1.Coefs[0, 0] * om2.Coefs[0, 1] + om1.Coefs[0, 1] * om2.Coefs[1, 1] + om1.Coefs[0, 2] * om2.Coefs[2, 1] + om1.Coefs[0, 3] * om2.Coefs[3, 1] + om1.Coefs[0, 4] * om2.Coefs[4, 1];
            coefs[0, 2] = om1.Coefs[0, 0] * om2.Coefs[0, 2] + om1.Coefs[0, 1] * om2.Coefs[1, 2] + om1.Coefs[0, 2] * om2.Coefs[2, 2] + om1.Coefs[0, 3] * om2.Coefs[3, 2] + om1.Coefs[0, 4] * om2.Coefs[4, 2];
            coefs[0, 3] = om1.Coefs[0, 0] * om2.Coefs[0, 3] + om1.Coefs[0, 1] * om2.Coefs[1, 3] + om1.Coefs[0, 2] * om2.Coefs[2, 3] + om1.Coefs[0, 3] * om2.Coefs[3, 3] + om1.Coefs[0, 4] * om2.Coefs[4, 3];
            coefs[0, 4] = om1.Coefs[0, 0] * om2.Coefs[0, 4] + om1.Coefs[0, 1] * om2.Coefs[1, 4] + om1.Coefs[0, 2] * om2.Coefs[2, 4] + om1.Coefs[0, 3] * om2.Coefs[3, 4] + om1.Coefs[0, 4] * om2.Coefs[4, 4];
            coefs[1, 0] = om1.Coefs[1, 0] * om2.Coefs[0, 0] + om1.Coefs[1, 1] * om2.Coefs[1, 0] + om1.Coefs[1, 2] * om2.Coefs[2, 0] + om1.Coefs[1, 3] * om2.Coefs[3, 0] + om1.Coefs[1, 4] * om2.Coefs[4, 0];
            coefs[1, 1] = om1.Coefs[1, 0] * om2.Coefs[0, 1] + om1.Coefs[1, 1] * om2.Coefs[1, 1] + om1.Coefs[1, 2] * om2.Coefs[2, 1] + om1.Coefs[1, 3] * om2.Coefs[3, 1] + om1.Coefs[1, 4] * om2.Coefs[4, 1];
            coefs[1, 2] = om1.Coefs[1, 0] * om2.Coefs[0, 2] + om1.Coefs[1, 1] * om2.Coefs[1, 2] + om1.Coefs[1, 2] * om2.Coefs[2, 2] + om1.Coefs[1, 3] * om2.Coefs[3, 2] + om1.Coefs[1, 4] * om2.Coefs[4, 2];
            coefs[1, 3] = om1.Coefs[1, 0] * om2.Coefs[0, 3] + om1.Coefs[1, 1] * om2.Coefs[1, 3] + om1.Coefs[1, 2] * om2.Coefs[2, 3] + om1.Coefs[1, 3] * om2.Coefs[3, 3] + om1.Coefs[1, 4] * om2.Coefs[4, 3];
            coefs[1, 4] = om1.Coefs[1, 0] * om2.Coefs[0, 4] + om1.Coefs[1, 1] * om2.Coefs[1, 4] + om1.Coefs[1, 2] * om2.Coefs[2, 4] + om1.Coefs[1, 3] * om2.Coefs[3, 4] + om1.Coefs[1, 4] * om2.Coefs[4, 4];
            coefs[2, 0] = om1.Coefs[2, 0] * om2.Coefs[0, 0] + om1.Coefs[2, 1] * om2.Coefs[1, 0] + om1.Coefs[2, 2] * om2.Coefs[2, 0] + om1.Coefs[2, 3] * om2.Coefs[3, 0] + om1.Coefs[2, 4] * om2.Coefs[4, 0];
            coefs[2, 1] = om1.Coefs[2, 0] * om2.Coefs[0, 1] + om1.Coefs[2, 1] * om2.Coefs[1, 1] + om1.Coefs[2, 2] * om2.Coefs[2, 1] + om1.Coefs[2, 3] * om2.Coefs[3, 1] + om1.Coefs[2, 4] * om2.Coefs[4, 1];
            coefs[2, 2] = om1.Coefs[2, 0] * om2.Coefs[0, 2] + om1.Coefs[2, 1] * om2.Coefs[1, 2] + om1.Coefs[2, 2] * om2.Coefs[2, 2] + om1.Coefs[2, 3] * om2.Coefs[3, 2] + om1.Coefs[2, 4] * om2.Coefs[4, 2];
            coefs[2, 3] = om1.Coefs[2, 0] * om2.Coefs[0, 3] + om1.Coefs[2, 1] * om2.Coefs[1, 3] + om1.Coefs[2, 2] * om2.Coefs[2, 3] + om1.Coefs[2, 3] * om2.Coefs[3, 3] + om1.Coefs[2, 4] * om2.Coefs[4, 3];
            coefs[2, 4] = om1.Coefs[2, 0] * om2.Coefs[0, 4] + om1.Coefs[2, 1] * om2.Coefs[1, 4] + om1.Coefs[2, 2] * om2.Coefs[2, 4] + om1.Coefs[2, 3] * om2.Coefs[3, 4] + om1.Coefs[2, 4] * om2.Coefs[4, 4];
            coefs[3, 0] = om1.Coefs[3, 0] * om2.Coefs[0, 0] + om1.Coefs[3, 1] * om2.Coefs[1, 0] + om1.Coefs[3, 2] * om2.Coefs[2, 0] + om1.Coefs[3, 3] * om2.Coefs[3, 0] + om1.Coefs[3, 4] * om2.Coefs[4, 0];
            coefs[3, 1] = om1.Coefs[3, 0] * om2.Coefs[0, 1] + om1.Coefs[3, 1] * om2.Coefs[1, 1] + om1.Coefs[3, 2] * om2.Coefs[2, 1] + om1.Coefs[3, 3] * om2.Coefs[3, 1] + om1.Coefs[3, 4] * om2.Coefs[4, 1];
            coefs[3, 2] = om1.Coefs[3, 0] * om2.Coefs[0, 2] + om1.Coefs[3, 1] * om2.Coefs[1, 2] + om1.Coefs[3, 2] * om2.Coefs[2, 2] + om1.Coefs[3, 3] * om2.Coefs[3, 2] + om1.Coefs[3, 4] * om2.Coefs[4, 2];
            coefs[3, 3] = om1.Coefs[3, 0] * om2.Coefs[0, 3] + om1.Coefs[3, 1] * om2.Coefs[1, 3] + om1.Coefs[3, 2] * om2.Coefs[2, 3] + om1.Coefs[3, 3] * om2.Coefs[3, 3] + om1.Coefs[3, 4] * om2.Coefs[4, 3];
            coefs[3, 4] = om1.Coefs[3, 0] * om2.Coefs[0, 4] + om1.Coefs[3, 1] * om2.Coefs[1, 4] + om1.Coefs[3, 2] * om2.Coefs[2, 4] + om1.Coefs[3, 3] * om2.Coefs[3, 4] + om1.Coefs[3, 4] * om2.Coefs[4, 4];
            coefs[4, 0] = om1.Coefs[4, 0] * om2.Coefs[0, 0] + om1.Coefs[4, 1] * om2.Coefs[1, 0] + om1.Coefs[4, 2] * om2.Coefs[2, 0] + om1.Coefs[4, 3] * om2.Coefs[3, 0] + om1.Coefs[4, 4] * om2.Coefs[4, 0];
            coefs[4, 1] = om1.Coefs[4, 0] * om2.Coefs[0, 1] + om1.Coefs[4, 1] * om2.Coefs[1, 1] + om1.Coefs[4, 2] * om2.Coefs[2, 1] + om1.Coefs[4, 3] * om2.Coefs[3, 1] + om1.Coefs[4, 4] * om2.Coefs[4, 1];
            coefs[4, 2] = om1.Coefs[4, 0] * om2.Coefs[0, 2] + om1.Coefs[4, 1] * om2.Coefs[1, 2] + om1.Coefs[4, 2] * om2.Coefs[2, 2] + om1.Coefs[4, 3] * om2.Coefs[3, 2] + om1.Coefs[4, 4] * om2.Coefs[4, 2];
            coefs[4, 3] = om1.Coefs[4, 0] * om2.Coefs[0, 3] + om1.Coefs[4, 1] * om2.Coefs[1, 3] + om1.Coefs[4, 2] * om2.Coefs[2, 3] + om1.Coefs[4, 3] * om2.Coefs[3, 3] + om1.Coefs[4, 4] * om2.Coefs[4, 3];
            coefs[4, 4] = om1.Coefs[4, 0] * om2.Coefs[0, 4] + om1.Coefs[4, 1] * om2.Coefs[1, 4] + om1.Coefs[4, 2] * om2.Coefs[2, 4] + om1.Coefs[4, 3] * om2.Coefs[3, 4] + om1.Coefs[4, 4] * om2.Coefs[4, 4];
        
            return new cga0001Outermorphism(coefs);
        }
        
        public static cga0001Outermorphism operator *(double scalar, cga0001Outermorphism om)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = om.Coefs[0, 0] * scalar;
            coefs[0, 1] = om.Coefs[0, 1] * scalar;
            coefs[0, 2] = om.Coefs[0, 2] * scalar;
            coefs[0, 3] = om.Coefs[0, 3] * scalar;
            coefs[0, 4] = om.Coefs[0, 4] * scalar;
            coefs[1, 0] = om.Coefs[1, 0] * scalar;
            coefs[1, 1] = om.Coefs[1, 1] * scalar;
            coefs[1, 2] = om.Coefs[1, 2] * scalar;
            coefs[1, 3] = om.Coefs[1, 3] * scalar;
            coefs[1, 4] = om.Coefs[1, 4] * scalar;
            coefs[2, 0] = om.Coefs[2, 0] * scalar;
            coefs[2, 1] = om.Coefs[2, 1] * scalar;
            coefs[2, 2] = om.Coefs[2, 2] * scalar;
            coefs[2, 3] = om.Coefs[2, 3] * scalar;
            coefs[2, 4] = om.Coefs[2, 4] * scalar;
            coefs[3, 0] = om.Coefs[3, 0] * scalar;
            coefs[3, 1] = om.Coefs[3, 1] * scalar;
            coefs[3, 2] = om.Coefs[3, 2] * scalar;
            coefs[3, 3] = om.Coefs[3, 3] * scalar;
            coefs[3, 4] = om.Coefs[3, 4] * scalar;
            coefs[4, 0] = om.Coefs[4, 0] * scalar;
            coefs[4, 1] = om.Coefs[4, 1] * scalar;
            coefs[4, 2] = om.Coefs[4, 2] * scalar;
            coefs[4, 3] = om.Coefs[4, 3] * scalar;
            coefs[4, 4] = om.Coefs[4, 4] * scalar;
        
            return new cga0001Outermorphism(coefs);
        }
        
        public static cga0001Outermorphism operator *(cga0001Outermorphism om, double scalar)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = om.Coefs[0, 0] * scalar;
            coefs[0, 1] = om.Coefs[0, 1] * scalar;
            coefs[0, 2] = om.Coefs[0, 2] * scalar;
            coefs[0, 3] = om.Coefs[0, 3] * scalar;
            coefs[0, 4] = om.Coefs[0, 4] * scalar;
            coefs[1, 0] = om.Coefs[1, 0] * scalar;
            coefs[1, 1] = om.Coefs[1, 1] * scalar;
            coefs[1, 2] = om.Coefs[1, 2] * scalar;
            coefs[1, 3] = om.Coefs[1, 3] * scalar;
            coefs[1, 4] = om.Coefs[1, 4] * scalar;
            coefs[2, 0] = om.Coefs[2, 0] * scalar;
            coefs[2, 1] = om.Coefs[2, 1] * scalar;
            coefs[2, 2] = om.Coefs[2, 2] * scalar;
            coefs[2, 3] = om.Coefs[2, 3] * scalar;
            coefs[2, 4] = om.Coefs[2, 4] * scalar;
            coefs[3, 0] = om.Coefs[3, 0] * scalar;
            coefs[3, 1] = om.Coefs[3, 1] * scalar;
            coefs[3, 2] = om.Coefs[3, 2] * scalar;
            coefs[3, 3] = om.Coefs[3, 3] * scalar;
            coefs[3, 4] = om.Coefs[3, 4] * scalar;
            coefs[4, 0] = om.Coefs[4, 0] * scalar;
            coefs[4, 1] = om.Coefs[4, 1] * scalar;
            coefs[4, 2] = om.Coefs[4, 2] * scalar;
            coefs[4, 3] = om.Coefs[4, 3] * scalar;
            coefs[4, 4] = om.Coefs[4, 4] * scalar;
        
            return new cga0001Outermorphism(coefs);
        }
        
        public static cga0001Outermorphism operator /(cga0001Outermorphism om, double scalar)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = om.Coefs[0, 0] / scalar;
            coefs[0, 1] = om.Coefs[0, 1] / scalar;
            coefs[0, 2] = om.Coefs[0, 2] / scalar;
            coefs[0, 3] = om.Coefs[0, 3] / scalar;
            coefs[0, 4] = om.Coefs[0, 4] / scalar;
            coefs[1, 0] = om.Coefs[1, 0] / scalar;
            coefs[1, 1] = om.Coefs[1, 1] / scalar;
            coefs[1, 2] = om.Coefs[1, 2] / scalar;
            coefs[1, 3] = om.Coefs[1, 3] / scalar;
            coefs[1, 4] = om.Coefs[1, 4] / scalar;
            coefs[2, 0] = om.Coefs[2, 0] / scalar;
            coefs[2, 1] = om.Coefs[2, 1] / scalar;
            coefs[2, 2] = om.Coefs[2, 2] / scalar;
            coefs[2, 3] = om.Coefs[2, 3] / scalar;
            coefs[2, 4] = om.Coefs[2, 4] / scalar;
            coefs[3, 0] = om.Coefs[3, 0] / scalar;
            coefs[3, 1] = om.Coefs[3, 1] / scalar;
            coefs[3, 2] = om.Coefs[3, 2] / scalar;
            coefs[3, 3] = om.Coefs[3, 3] / scalar;
            coefs[3, 4] = om.Coefs[3, 4] / scalar;
            coefs[4, 0] = om.Coefs[4, 0] / scalar;
            coefs[4, 1] = om.Coefs[4, 1] / scalar;
            coefs[4, 2] = om.Coefs[4, 2] / scalar;
            coefs[4, 3] = om.Coefs[4, 3] / scalar;
            coefs[4, 4] = om.Coefs[4, 4] / scalar;
        
            return new cga0001Outermorphism(coefs);
        }
        
        public static cga0001Outermorphism operator -(cga0001Outermorphism om)
        {
            var coefs = new double[cga0001Blade.MaxGrade, cga0001Blade.MaxGrade];
        
            coefs[0, 0] = -om.Coefs[0, 0];
            coefs[0, 1] = -om.Coefs[0, 1];
            coefs[0, 2] = -om.Coefs[0, 2];
            coefs[0, 3] = -om.Coefs[0, 3];
            coefs[0, 4] = -om.Coefs[0, 4];
            coefs[1, 0] = -om.Coefs[1, 0];
            coefs[1, 1] = -om.Coefs[1, 1];
            coefs[1, 2] = -om.Coefs[1, 2];
            coefs[1, 3] = -om.Coefs[1, 3];
            coefs[1, 4] = -om.Coefs[1, 4];
            coefs[2, 0] = -om.Coefs[2, 0];
            coefs[2, 1] = -om.Coefs[2, 1];
            coefs[2, 2] = -om.Coefs[2, 2];
            coefs[2, 3] = -om.Coefs[2, 3];
            coefs[2, 4] = -om.Coefs[2, 4];
            coefs[3, 0] = -om.Coefs[3, 0];
            coefs[3, 1] = -om.Coefs[3, 1];
            coefs[3, 2] = -om.Coefs[3, 2];
            coefs[3, 3] = -om.Coefs[3, 3];
            coefs[3, 4] = -om.Coefs[3, 4];
            coefs[4, 0] = -om.Coefs[4, 0];
            coefs[4, 1] = -om.Coefs[4, 1];
            coefs[4, 2] = -om.Coefs[4, 2];
            coefs[4, 3] = -om.Coefs[4, 3];
            coefs[4, 4] = -om.Coefs[4, 4];
        
            return new cga0001Outermorphism(coefs);
        }
        
    }
}
