using System.IO;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static cga0001Blade OP2(cga0001Vector[] vectors)
        {
            var coefs = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:37.9017442+02:00
            //Macro: geometry3d.cga.VectorsOP
            //Input Variables: 3 used, 10 not used, 13 total.
            //Temp Variables: 30 sub-expressions, 0 generated temps, 30 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 10 total.
            //Computations: 1.25 average, 50 total.
            //Memory Reads: 1.75 average, 70 total.
            //Memory Writes: 40 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1# <=> <Variable> coefs[0]
            //   result.#no^e2# <=> <Variable> coefs[1]
            //   result.#e1^e2# <=> <Variable> coefs[2]
            //   result.#no^e3# <=> <Variable> coefs[3]
            //   result.#e1^e3# <=> <Variable> coefs[4]
            //   result.#e2^e3# <=> <Variable> coefs[5]
            //   result.#no^ni# <=> <Variable> coefs[6]
            //   result.#e1^ni# <=> <Variable> coefs[7]
            //   result.#e2^ni# <=> <Variable> coefs[8]
            //   result.#e3^ni# <=> <Variable> coefs[9]
            //   v0.#no# <=> <Variable> vectors[0].C1
            //   v0.#e1# <=> <Variable> vectors[0].C2
            //   v0.#e2# <=> <Variable> vectors[0].C3
            //   v0.#e3# <=> <Variable> vectors[0].C4
            //   v0.#ni# <=> <Variable> vectors[0].C5
            //   v1.#no# <=> <Variable> vectors[1].C1
            //   v1.#e1# <=> <Variable> vectors[1].C2
            //   v1.#e2# <=> <Variable> vectors[1].C3
            //   v1.#e3# <=> <Variable> vectors[1].C4
            //   v1.#ni# <=> <Variable> vectors[1].C5
            //   v2.#E0# <=> <Constant> 1
            //   v3.#E0# <=> <Constant> 1
            //   v4.#E0# <=> <Constant> 1
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (vectors[0].C2 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[0] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C3 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C3);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[1] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C3 * vectors[1].C2);
            tempVar0001 = (-1 * vectors[0].C2 * vectors[1].C3);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[2] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C4 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C4);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[3] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C4 * vectors[1].C2);
            tempVar0001 = (-1 * vectors[0].C2 * vectors[1].C4);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[4] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C4 * vectors[1].C3);
            tempVar0001 = (-1 * vectors[0].C3 * vectors[1].C4);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[5] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[6] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C2);
            tempVar0001 = (-1 * vectors[0].C2 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[7] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C3);
            tempVar0001 = (-1 * vectors[0].C3 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[8] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C4);
            tempVar0001 = (-1 * vectors[0].C4 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[9] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:37.9057444+02:00
            
            return new cga0001Blade(2, coefs);
        }
        
        private static cga0001Blade OP3(cga0001Vector[] vectors)
        {
            var coefs = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:38.1427580+02:00
            //Macro: geometry3d.cga.VectorsOP
            //Input Variables: 2 used, 15 not used, 17 total.
            //Temp Variables: 70 sub-expressions, 0 generated temps, 70 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 10 total.
            //Computations: 1.375 average, 110 total.
            //Memory Reads: 2 average, 160 total.
            //Memory Writes: 80 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2# <=> <Variable> coefs[0]
            //   result.#no^e1^e3# <=> <Variable> coefs[1]
            //   result.#no^e2^e3# <=> <Variable> coefs[2]
            //   result.#e1^e2^e3# <=> <Variable> coefs[3]
            //   result.#no^e1^ni# <=> <Variable> coefs[4]
            //   result.#no^e2^ni# <=> <Variable> coefs[5]
            //   result.#e1^e2^ni# <=> <Variable> coefs[6]
            //   result.#no^e3^ni# <=> <Variable> coefs[7]
            //   result.#e1^e3^ni# <=> <Variable> coefs[8]
            //   result.#e2^e3^ni# <=> <Variable> coefs[9]
            //   v0.#no# <=> <Variable> vectors[0].C1
            //   v0.#e1# <=> <Variable> vectors[0].C2
            //   v0.#e2# <=> <Variable> vectors[0].C3
            //   v0.#e3# <=> <Variable> vectors[0].C4
            //   v0.#ni# <=> <Variable> vectors[0].C5
            //   v1.#no# <=> <Variable> vectors[1].C1
            //   v1.#e1# <=> <Variable> vectors[1].C2
            //   v1.#e2# <=> <Variable> vectors[1].C3
            //   v1.#e3# <=> <Variable> vectors[1].C4
            //   v1.#ni# <=> <Variable> vectors[1].C5
            //   v2.#no# <=> <Variable> vectors[2].C1
            //   v2.#e1# <=> <Variable> vectors[2].C2
            //   v2.#e2# <=> <Variable> vectors[2].C3
            //   v2.#e3# <=> <Variable> vectors[2].C4
            //   v2.#ni# <=> <Variable> vectors[2].C5
            //   v3.#E0# <=> <Constant> 1
            //   v4.#E0# <=> <Constant> 1
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            double tempVar0008;
            
            tempVar0000 = (vectors[0].C2 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * vectors[2].C3 * tempVar0000);
            tempVar0002 = (vectors[0].C3 * vectors[1].C1);
            tempVar0003 = (-1 * vectors[0].C1 * vectors[1].C3);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (vectors[2].C2 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (vectors[0].C3 * vectors[1].C2);
            tempVar0004 = (-1 * vectors[0].C2 * vectors[1].C3);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * vectors[2].C1 * tempVar0003);
            coefs[0] = (tempVar0001 + tempVar0004);
            tempVar0001 = (-1 * vectors[2].C4 * tempVar0000);
            tempVar0004 = (vectors[0].C4 * vectors[1].C1);
            tempVar0005 = (-1 * vectors[0].C1 * vectors[1].C4);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (vectors[2].C2 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0005);
            tempVar0005 = (vectors[0].C4 * vectors[1].C2);
            tempVar0006 = (-1 * vectors[0].C2 * vectors[1].C4);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * vectors[2].C1 * tempVar0005);
            coefs[1] = (tempVar0001 + tempVar0006);
            tempVar0001 = (-1 * vectors[2].C4 * tempVar0002);
            tempVar0006 = (vectors[2].C3 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0006);
            tempVar0006 = (vectors[0].C4 * vectors[1].C3);
            tempVar0007 = (-1 * vectors[0].C3 * vectors[1].C4);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * vectors[2].C1 * tempVar0006);
            coefs[2] = (tempVar0001 + tempVar0007);
            tempVar0001 = (-1 * vectors[2].C4 * tempVar0003);
            tempVar0007 = (vectors[2].C3 * tempVar0005);
            tempVar0001 = (tempVar0001 + tempVar0007);
            tempVar0007 = (-1 * vectors[2].C2 * tempVar0006);
            coefs[3] = (tempVar0001 + tempVar0007);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0000);
            tempVar0001 = (vectors[0].C5 * vectors[1].C1);
            tempVar0007 = (-1 * vectors[0].C1 * vectors[1].C5);
            tempVar0001 = (tempVar0001 + tempVar0007);
            tempVar0007 = (vectors[2].C2 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0007);
            tempVar0007 = (vectors[0].C5 * vectors[1].C2);
            tempVar0008 = (-1 * vectors[0].C2 * vectors[1].C5);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * vectors[2].C1 * tempVar0007);
            coefs[4] = (tempVar0000 + tempVar0008);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0002);
            tempVar0002 = (vectors[2].C3 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (vectors[0].C5 * vectors[1].C3);
            tempVar0008 = (-1 * vectors[0].C3 * vectors[1].C5);
            tempVar0002 = (tempVar0002 + tempVar0008);
            tempVar0008 = (-1 * vectors[2].C1 * tempVar0002);
            coefs[5] = (tempVar0000 + tempVar0008);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0003);
            tempVar0003 = (vectors[2].C3 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0003 = (-1 * vectors[2].C2 * tempVar0002);
            coefs[6] = (tempVar0000 + tempVar0003);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0004);
            tempVar0001 = (vectors[2].C4 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (vectors[0].C5 * vectors[1].C4);
            tempVar0003 = (-1 * vectors[0].C4 * vectors[1].C5);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (-1 * vectors[2].C1 * tempVar0001);
            coefs[7] = (tempVar0000 + tempVar0003);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0005);
            tempVar0003 = (vectors[2].C4 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0003 = (-1 * vectors[2].C2 * tempVar0001);
            coefs[8] = (tempVar0000 + tempVar0003);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0006);
            tempVar0002 = (vectors[2].C4 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (-1 * vectors[2].C3 * tempVar0001);
            coefs[9] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:38.1507584+02:00
            
            return new cga0001Blade(3, coefs);
        }
        
        private static cga0001Blade OP4(cga0001Vector[] vectors)
        {
            var coefs = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:38.1677594+02:00
            //Macro: geometry3d.cga.VectorsOP
            //Input Variables: 1 used, 20 not used, 21 total.
            //Temp Variables: 115 sub-expressions, 0 generated temps, 115 total.
            //Target Temp Variables: 14 total.
            //Output Variables: 5 total.
            //Computations: 1.33333333333333 average, 160 total.
            //Memory Reads: 1.95833333333333 average, 235 total.
            //Memory Writes: 120 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   result.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   result.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   result.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            //   v0.#no# <=> <Variable> vectors[0].C1
            //   v0.#e1# <=> <Variable> vectors[0].C2
            //   v0.#e2# <=> <Variable> vectors[0].C3
            //   v0.#e3# <=> <Variable> vectors[0].C4
            //   v0.#ni# <=> <Variable> vectors[0].C5
            //   v1.#no# <=> <Variable> vectors[1].C1
            //   v1.#e1# <=> <Variable> vectors[1].C2
            //   v1.#e2# <=> <Variable> vectors[1].C3
            //   v1.#e3# <=> <Variable> vectors[1].C4
            //   v1.#ni# <=> <Variable> vectors[1].C5
            //   v2.#no# <=> <Variable> vectors[2].C1
            //   v2.#e1# <=> <Variable> vectors[2].C2
            //   v2.#e2# <=> <Variable> vectors[2].C3
            //   v2.#e3# <=> <Variable> vectors[2].C4
            //   v2.#ni# <=> <Variable> vectors[2].C5
            //   v3.#no# <=> <Variable> vectors[3].C1
            //   v3.#e1# <=> <Variable> vectors[3].C2
            //   v3.#e2# <=> <Variable> vectors[3].C3
            //   v3.#e3# <=> <Variable> vectors[3].C4
            //   v3.#ni# <=> <Variable> vectors[3].C5
            //   v4.#E0# <=> <Constant> 1
            
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
            
            tempVar0000 = (vectors[0].C2 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * vectors[2].C3 * tempVar0000);
            tempVar0002 = (vectors[0].C3 * vectors[1].C1);
            tempVar0003 = (-1 * vectors[0].C1 * vectors[1].C3);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (vectors[2].C2 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (vectors[0].C3 * vectors[1].C2);
            tempVar0004 = (-1 * vectors[0].C2 * vectors[1].C3);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * vectors[2].C1 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (-1 * vectors[3].C4 * tempVar0001);
            tempVar0005 = (-1 * vectors[2].C4 * tempVar0000);
            tempVar0006 = (vectors[0].C4 * vectors[1].C1);
            tempVar0007 = (-1 * vectors[0].C1 * vectors[1].C4);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (vectors[2].C2 * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (vectors[0].C4 * vectors[1].C2);
            tempVar0008 = (-1 * vectors[0].C2 * vectors[1].C4);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * vectors[2].C1 * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (vectors[3].C3 * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0008);
            tempVar0008 = (-1 * vectors[2].C4 * tempVar0002);
            tempVar0009 = (vectors[2].C3 * tempVar0006);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (vectors[0].C4 * vectors[1].C3);
            tempVar000A = (-1 * vectors[0].C3 * vectors[1].C4);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * vectors[2].C1 * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar000A = (-1 * vectors[3].C2 * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * vectors[2].C4 * tempVar0003);
            tempVar000B = (vectors[2].C3 * tempVar0007);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * vectors[2].C2 * tempVar0009);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (vectors[3].C1 * tempVar000A);
            tempVar0004 = (tempVar0004 + tempVar000B);
            coefs[0] = (-1 * tempVar0004);
            tempVar0001 = (-1 * vectors[3].C5 * tempVar0001);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0000);
            tempVar0004 = (vectors[0].C5 * vectors[1].C1);
            tempVar000B = (-1 * vectors[0].C1 * vectors[1].C5);
            tempVar0004 = (tempVar0004 + tempVar000B);
            tempVar000B = (vectors[2].C2 * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar000B);
            tempVar000B = (vectors[0].C5 * vectors[1].C2);
            tempVar000C = (-1 * vectors[0].C2 * vectors[1].C5);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (-1 * vectors[2].C1 * tempVar000B);
            tempVar0000 = (tempVar0000 + tempVar000C);
            tempVar000C = (vectors[3].C3 * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar000C);
            tempVar0002 = (-1 * vectors[2].C5 * tempVar0002);
            tempVar000C = (vectors[2].C3 * tempVar0004);
            tempVar0002 = (tempVar0002 + tempVar000C);
            tempVar000C = (vectors[0].C5 * vectors[1].C3);
            tempVar000D = (-1 * vectors[0].C3 * vectors[1].C5);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * vectors[2].C1 * tempVar000C);
            tempVar0002 = (tempVar0002 + tempVar000D);
            tempVar000D = (-1 * vectors[3].C2 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar000D);
            tempVar0003 = (-1 * vectors[2].C5 * tempVar0003);
            tempVar000D = (vectors[2].C3 * tempVar000B);
            tempVar0003 = (tempVar0003 + tempVar000D);
            tempVar000D = (-1 * vectors[2].C2 * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar000D);
            tempVar000D = (vectors[3].C1 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar000D);
            coefs[1] = (-1 * tempVar0001);
            tempVar0001 = (-1 * vectors[3].C5 * tempVar0005);
            tempVar0000 = (vectors[3].C4 * tempVar0000);
            tempVar0000 = (tempVar0001 + tempVar0000);
            tempVar0001 = (-1 * vectors[2].C5 * tempVar0006);
            tempVar0004 = (vectors[2].C4 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (vectors[0].C5 * vectors[1].C4);
            tempVar0005 = (-1 * vectors[0].C4 * vectors[1].C5);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (-1 * vectors[2].C1 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0005);
            tempVar0005 = (-1 * vectors[3].C2 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0005);
            tempVar0005 = (-1 * vectors[2].C5 * tempVar0007);
            tempVar0006 = (vectors[2].C4 * tempVar000B);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * vectors[2].C2 * tempVar0004);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (vectors[3].C1 * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0006);
            coefs[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * vectors[3].C5 * tempVar0008);
            tempVar0002 = (vectors[3].C4 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (-1 * vectors[3].C3 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * vectors[2].C5 * tempVar0009);
            tempVar0002 = (vectors[2].C4 * tempVar000C);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * vectors[2].C3 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (vectors[3].C1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0002);
            coefs[3] = (-1 * tempVar0000);
            tempVar0000 = (-1 * vectors[3].C5 * tempVar000A);
            tempVar0002 = (vectors[3].C4 * tempVar0003);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (-1 * vectors[3].C3 * tempVar0005);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (vectors[3].C2 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[4] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:38.1787600+02:00
            
            return new cga0001Blade(4, coefs);
        }
        
        private static cga0001Blade OP5(cga0001Vector[] vectors)
        {
            var coefs = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:38.1937609+02:00
            //Macro: geometry3d.cga.VectorsOP
            //Input Variables: 0 used, 25 not used, 25 total.
            //Temp Variables: 123 sub-expressions, 0 generated temps, 123 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.34677419354839 average, 167 total.
            //Memory Reads: 2 average, 248 total.
            //Memory Writes: 124 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            //   v0.#no# <=> <Variable> vectors[0].C1
            //   v0.#e1# <=> <Variable> vectors[0].C2
            //   v0.#e2# <=> <Variable> vectors[0].C3
            //   v0.#e3# <=> <Variable> vectors[0].C4
            //   v0.#ni# <=> <Variable> vectors[0].C5
            //   v1.#no# <=> <Variable> vectors[1].C1
            //   v1.#e1# <=> <Variable> vectors[1].C2
            //   v1.#e2# <=> <Variable> vectors[1].C3
            //   v1.#e3# <=> <Variable> vectors[1].C4
            //   v1.#ni# <=> <Variable> vectors[1].C5
            //   v2.#no# <=> <Variable> vectors[2].C1
            //   v2.#e1# <=> <Variable> vectors[2].C2
            //   v2.#e2# <=> <Variable> vectors[2].C3
            //   v2.#e3# <=> <Variable> vectors[2].C4
            //   v2.#ni# <=> <Variable> vectors[2].C5
            //   v3.#no# <=> <Variable> vectors[3].C1
            //   v3.#e1# <=> <Variable> vectors[3].C2
            //   v3.#e2# <=> <Variable> vectors[3].C3
            //   v3.#e3# <=> <Variable> vectors[3].C4
            //   v3.#ni# <=> <Variable> vectors[3].C5
            //   v4.#no# <=> <Variable> vectors[4].C1
            //   v4.#e1# <=> <Variable> vectors[4].C2
            //   v4.#e2# <=> <Variable> vectors[4].C3
            //   v4.#e3# <=> <Variable> vectors[4].C4
            //   v4.#ni# <=> <Variable> vectors[4].C5
            
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
            
            tempVar0000 = (vectors[0].C2 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * vectors[2].C3 * tempVar0000);
            tempVar0002 = (vectors[0].C3 * vectors[1].C1);
            tempVar0003 = (-1 * vectors[0].C1 * vectors[1].C3);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (vectors[2].C2 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (vectors[0].C3 * vectors[1].C2);
            tempVar0004 = (-1 * vectors[0].C2 * vectors[1].C3);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (-1 * vectors[2].C1 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0004 = (-1 * vectors[3].C4 * tempVar0001);
            tempVar0005 = (-1 * vectors[2].C4 * tempVar0000);
            tempVar0006 = (vectors[0].C4 * vectors[1].C1);
            tempVar0007 = (-1 * vectors[0].C1 * vectors[1].C4);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (vectors[2].C2 * tempVar0006);
            tempVar0005 = (tempVar0005 + tempVar0007);
            tempVar0007 = (vectors[0].C4 * vectors[1].C2);
            tempVar0008 = (-1 * vectors[0].C2 * vectors[1].C4);
            tempVar0007 = (tempVar0007 + tempVar0008);
            tempVar0008 = (-1 * vectors[2].C1 * tempVar0007);
            tempVar0005 = (tempVar0005 + tempVar0008);
            tempVar0008 = (vectors[3].C3 * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0008);
            tempVar0008 = (-1 * vectors[2].C4 * tempVar0002);
            tempVar0009 = (vectors[2].C3 * tempVar0006);
            tempVar0008 = (tempVar0008 + tempVar0009);
            tempVar0009 = (vectors[0].C4 * vectors[1].C3);
            tempVar000A = (-1 * vectors[0].C3 * vectors[1].C4);
            tempVar0009 = (tempVar0009 + tempVar000A);
            tempVar000A = (-1 * vectors[2].C1 * tempVar0009);
            tempVar0008 = (tempVar0008 + tempVar000A);
            tempVar000A = (-1 * vectors[3].C2 * tempVar0008);
            tempVar0004 = (tempVar0004 + tempVar000A);
            tempVar000A = (-1 * vectors[2].C4 * tempVar0003);
            tempVar000B = (vectors[2].C3 * tempVar0007);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (-1 * vectors[2].C2 * tempVar0009);
            tempVar000A = (tempVar000A + tempVar000B);
            tempVar000B = (vectors[3].C1 * tempVar000A);
            tempVar0004 = (tempVar0004 + tempVar000B);
            tempVar0004 = (-1 * vectors[4].C5 * tempVar0004);
            tempVar0001 = (-1 * vectors[3].C5 * tempVar0001);
            tempVar0000 = (-1 * vectors[2].C5 * tempVar0000);
            tempVar000B = (vectors[0].C5 * vectors[1].C1);
            tempVar000C = (-1 * vectors[0].C1 * vectors[1].C5);
            tempVar000B = (tempVar000B + tempVar000C);
            tempVar000C = (vectors[2].C2 * tempVar000B);
            tempVar0000 = (tempVar0000 + tempVar000C);
            tempVar000C = (vectors[0].C5 * vectors[1].C2);
            tempVar000D = (-1 * vectors[0].C2 * vectors[1].C5);
            tempVar000C = (tempVar000C + tempVar000D);
            tempVar000D = (-1 * vectors[2].C1 * tempVar000C);
            tempVar0000 = (tempVar0000 + tempVar000D);
            tempVar000D = (vectors[3].C3 * tempVar0000);
            tempVar0001 = (tempVar0001 + tempVar000D);
            tempVar0002 = (-1 * vectors[2].C5 * tempVar0002);
            tempVar000D = (vectors[2].C3 * tempVar000B);
            tempVar0002 = (tempVar0002 + tempVar000D);
            tempVar000D = (vectors[0].C5 * vectors[1].C3);
            tempVar000E = (-1 * vectors[0].C3 * vectors[1].C5);
            tempVar000D = (tempVar000D + tempVar000E);
            tempVar000E = (-1 * vectors[2].C1 * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar000E);
            tempVar000E = (-1 * vectors[3].C2 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0003 = (-1 * vectors[2].C5 * tempVar0003);
            tempVar000E = (vectors[2].C3 * tempVar000C);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (-1 * vectors[2].C2 * tempVar000D);
            tempVar0003 = (tempVar0003 + tempVar000E);
            tempVar000E = (vectors[3].C1 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar000E);
            tempVar0001 = (vectors[4].C4 * tempVar0001);
            tempVar0001 = (tempVar0004 + tempVar0001);
            tempVar0004 = (-1 * vectors[3].C5 * tempVar0005);
            tempVar0000 = (vectors[3].C4 * tempVar0000);
            tempVar0000 = (tempVar0004 + tempVar0000);
            tempVar0004 = (-1 * vectors[2].C5 * tempVar0006);
            tempVar0005 = (vectors[2].C4 * tempVar000B);
            tempVar0004 = (tempVar0004 + tempVar0005);
            tempVar0005 = (vectors[0].C5 * vectors[1].C4);
            tempVar0006 = (-1 * vectors[0].C4 * vectors[1].C5);
            tempVar0005 = (tempVar0005 + tempVar0006);
            tempVar0006 = (-1 * vectors[2].C1 * tempVar0005);
            tempVar0004 = (tempVar0004 + tempVar0006);
            tempVar0006 = (-1 * vectors[3].C2 * tempVar0004);
            tempVar0000 = (tempVar0000 + tempVar0006);
            tempVar0006 = (-1 * vectors[2].C5 * tempVar0007);
            tempVar0007 = (vectors[2].C4 * tempVar000C);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (-1 * vectors[2].C2 * tempVar0005);
            tempVar0006 = (tempVar0006 + tempVar0007);
            tempVar0007 = (vectors[3].C1 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0007);
            tempVar0000 = (-1 * vectors[4].C3 * tempVar0000);
            tempVar0000 = (tempVar0001 + tempVar0000);
            tempVar0001 = (-1 * vectors[3].C5 * tempVar0008);
            tempVar0002 = (vectors[3].C4 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * vectors[3].C3 * tempVar0004);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (-1 * vectors[2].C5 * tempVar0009);
            tempVar0004 = (vectors[2].C4 * tempVar000D);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (-1 * vectors[2].C3 * tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0004);
            tempVar0004 = (vectors[3].C1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0004);
            tempVar0001 = (vectors[4].C2 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * vectors[3].C5 * tempVar000A);
            tempVar0003 = (vectors[3].C4 * tempVar0003);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0003 = (-1 * vectors[3].C3 * tempVar0006);
            tempVar0001 = (tempVar0001 + tempVar0003);
            tempVar0002 = (vectors[3].C2 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * vectors[4].C1 * tempVar0001);
            coefs[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:38.2067616+02:00
            
            return new cga0001Blade(5, coefs);
        }
        
        public static cga0001Blade OP(cga0001Vector[] vectors)
        {
            switch (vectors.Length)
            {
                case 0:
                    return ZeroBlade;
                case 1:
                    return vectors[0].ToBlade();
                case 2:
                    return OP2(vectors);
                case 3:
                    return OP3(vectors);
                case 4:
                    return OP4(vectors);
                case 5:
                    return OP5(vectors);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        
    }
}
