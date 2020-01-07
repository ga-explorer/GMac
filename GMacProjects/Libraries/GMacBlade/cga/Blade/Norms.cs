using System;
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
        #region Norm2
        private static double Norm2_0(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:34.9885775+02:00
            //Macro: geometry3d.cga.Norm2
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#E0# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            result = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:34.9885775+02:00
            
            return result;
        }
        
        
        private static double Norm2_1(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.1315857+02:00
            //Macro: geometry3d.cga.Norm2
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 19 sub-expressions, 0 generated temps, 19 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1.05 average, 21 total.
            //Memory Reads: 1.4 average, 28 total.
            //Memory Writes: 20 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no# <=> <Variable> coefs[0]
            //   mv.#e1# <=> <Variable> coefs[1]
            //   mv.#e2# <=> <Variable> coefs[2]
            //   mv.#e3# <=> <Variable> coefs[3]
            //   mv.#ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.1335858+02:00
            
            return result;
        }
        
        
        private static double Norm2_2(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.2355917+02:00
            //Macro: geometry3d.cga.Norm2
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 47 sub-expressions, 0 generated temps, 47 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.125 average, 54 total.
            //Memory Reads: 1.79166666666667 average, 86 total.
            //Memory Writes: 48 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1# <=> <Variable> coefs[0]
            //   mv.#no^e2# <=> <Variable> coefs[1]
            //   mv.#e1^e2# <=> <Variable> coefs[2]
            //   mv.#no^e3# <=> <Variable> coefs[3]
            //   mv.#e1^e3# <=> <Variable> coefs[4]
            //   mv.#e2^e3# <=> <Variable> coefs[5]
            //   mv.#no^ni# <=> <Variable> coefs[6]
            //   mv.#e1^ni# <=> <Variable> coefs[7]
            //   mv.#e2^ni# <=> <Variable> coefs[8]
            //   mv.#e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = Math.Pow(coefs[2], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[7] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[0] * tempVar0001);
            tempVar0006 = (coefs[7] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[1] * tempVar0001);
            tempVar0003 = (-1 * coefs[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[1] * tempVar0001);
            tempVar0006 = (coefs[8] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[3] * tempVar0001);
            tempVar0003 = (-1 * coefs[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[3] * tempVar0001);
            tempVar0001 = (coefs[9] * tempVar0001);
            tempVar0006 = (tempVar0005 + tempVar0001);
            tempVar0004 = (tempVar0004 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0001 = (tempVar0003 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.2405920+02:00
            
            return result;
        }
        
        
        private static double Norm2_3(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.3425978+02:00
            //Macro: geometry3d.cga.Norm2
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 47 sub-expressions, 0 generated temps, 47 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.125 average, 54 total.
            //Memory Reads: 1.79166666666667 average, 86 total.
            //Memory Writes: 48 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2# <=> <Variable> coefs[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = Math.Pow(coefs[3], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[6] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[0] * tempVar0001);
            tempVar0006 = (coefs[6] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = Math.Pow(coefs[7], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[1] * tempVar0001);
            tempVar0003 = (-1 * coefs[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[1] * tempVar0001);
            tempVar0006 = (coefs[8] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[2] * tempVar0001);
            tempVar0003 = (-1 * coefs[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[2] * tempVar0001);
            tempVar0001 = (coefs[9] * tempVar0001);
            tempVar0006 = (tempVar0005 + tempVar0001);
            tempVar0004 = (tempVar0004 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0001 = (tempVar0003 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.3485981+02:00
            
            return result;
        }
        
        
        private static double Norm2_4(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.4886061+02:00
            //Macro: geometry3d.cga.Norm2
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 19 sub-expressions, 0 generated temps, 19 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1.1 average, 22 total.
            //Memory Reads: 1.4 average, 28 total.
            //Memory Writes: 20 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.4906063+02:00
            
            return result;
        }
        
        
        private static double Norm2_5(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.5396091+02:00
            //Macro: geometry3d.cga.Norm2
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            result = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.5396091+02:00
            
            return result;
        }
        
        
        public double Norm2
        {
            get
            {
                if (IsZeroBlade)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Norm2_0(Coefs);
                    case 1:
                        return Norm2_1(Coefs);
                    case 2:
                        return Norm2_2(Coefs);
                    case 3:
                        return Norm2_3(Coefs);
                    case 4:
                        return Norm2_4(Coefs);
                    case 5:
                        return Norm2_5(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region Mag
        private static double Mag_0(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.5916120+02:00
            //Macro: geometry3d.cga.Mag
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#E0# <=> <Variable> coefs[0]
            
            
            result = Math.Abs(coefs[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.5916120+02:00
            
            return result;
        }
        
        
        private static double Mag_1(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.7426207+02:00
            //Macro: geometry3d.cga.Mag
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 21 sub-expressions, 0 generated temps, 21 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 22 total.
            //Memory Reads: 1.36363636363636 average, 30 total.
            //Memory Writes: 22 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no# <=> <Variable> coefs[0]
            //   mv.#e1# <=> <Variable> coefs[1]
            //   mv.#e2# <=> <Variable> coefs[2]
            //   mv.#e3# <=> <Variable> coefs[3]
            //   mv.#ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Abs(tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.7446208+02:00
            
            return result;
        }
        
        
        private static double Mag_2(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.8536270+02:00
            //Macro: geometry3d.cga.Mag
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 49 sub-expressions, 0 generated temps, 49 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.1 average, 55 total.
            //Memory Reads: 1.76 average, 88 total.
            //Memory Writes: 50 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1# <=> <Variable> coefs[0]
            //   mv.#no^e2# <=> <Variable> coefs[1]
            //   mv.#e1^e2# <=> <Variable> coefs[2]
            //   mv.#no^e3# <=> <Variable> coefs[3]
            //   mv.#e1^e3# <=> <Variable> coefs[4]
            //   mv.#e2^e3# <=> <Variable> coefs[5]
            //   mv.#no^ni# <=> <Variable> coefs[6]
            //   mv.#e1^ni# <=> <Variable> coefs[7]
            //   mv.#e2^ni# <=> <Variable> coefs[8]
            //   mv.#e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = Math.Pow(coefs[2], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[7] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[0] * tempVar0001);
            tempVar0006 = (coefs[7] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[1] * tempVar0001);
            tempVar0003 = (-1 * coefs[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[1] * tempVar0001);
            tempVar0006 = (coefs[8] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[3] * tempVar0001);
            tempVar0003 = (-1 * coefs[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[3] * tempVar0001);
            tempVar0001 = (coefs[9] * tempVar0001);
            tempVar0006 = (tempVar0005 + tempVar0001);
            tempVar0004 = (tempVar0004 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0001 = (tempVar0003 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Abs(tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.8606274+02:00
            
            return result;
        }
        
        
        private static double Mag_3(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:35.9696337+02:00
            //Macro: geometry3d.cga.Mag
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 49 sub-expressions, 0 generated temps, 49 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.1 average, 55 total.
            //Memory Reads: 1.76 average, 88 total.
            //Memory Writes: 50 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2# <=> <Variable> coefs[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = Math.Pow(coefs[3], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[6] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[0] * tempVar0001);
            tempVar0006 = (coefs[6] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = Math.Pow(coefs[7], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[1] * tempVar0001);
            tempVar0003 = (-1 * coefs[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[1] * tempVar0001);
            tempVar0006 = (coefs[8] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[2] * tempVar0001);
            tempVar0003 = (-1 * coefs[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[2] * tempVar0001);
            tempVar0001 = (coefs[9] * tempVar0001);
            tempVar0006 = (tempVar0005 + tempVar0001);
            tempVar0004 = (tempVar0004 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0001 = (tempVar0003 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Abs(tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:35.9756340+02:00
            
            return result;
        }
        
        
        private static double Mag_4(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.1256426+02:00
            //Macro: geometry3d.cga.Mag
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 21 sub-expressions, 0 generated temps, 21 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1.04545454545455 average, 23 total.
            //Memory Reads: 1.36363636363636 average, 30 total.
            //Memory Writes: 22 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Abs(tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.1276427+02:00
            
            return result;
        }
        
        
        private static double Mag_5(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.1776456+02:00
            //Macro: geometry3d.cga.Mag
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            
            result = Math.Abs(coefs[0]);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.1776456+02:00
            
            return result;
        }
        
        
        public double Mag
        {
            get
            {
                if (IsZeroBlade)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Mag_0(Coefs);
                    case 1:
                        return Mag_1(Coefs);
                    case 2:
                        return Mag_2(Coefs);
                    case 3:
                        return Mag_3(Coefs);
                    case 4:
                        return Mag_4(Coefs);
                    case 5:
                        return Mag_5(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region Mag2
        private static double Mag2_0(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.2296485+02:00
            //Macro: geometry3d.cga.Mag2
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 0.5 average, 1 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#E0# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Abs(coefs[0]);
            result = Math.Pow(tempVar0000, 2);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.2306486+02:00
            
            return result;
        }
        
        
        private static double Mag2_1(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.3796571+02:00
            //Macro: geometry3d.cga.Mag2
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 21 total.
            //Memory Reads: 1.38095238095238 average, 29 total.
            //Memory Writes: 21 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no# <=> <Variable> coefs[0]
            //   mv.#e1# <=> <Variable> coefs[1]
            //   mv.#e2# <=> <Variable> coefs[2]
            //   mv.#e3# <=> <Variable> coefs[3]
            //   mv.#ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Abs(tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.3816572+02:00
            
            return result;
        }
        
        
        private static double Mag2_2(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.4896634+02:00
            //Macro: geometry3d.cga.Mag2
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 48 sub-expressions, 0 generated temps, 48 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.10204081632653 average, 54 total.
            //Memory Reads: 1.77551020408163 average, 87 total.
            //Memory Writes: 49 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1# <=> <Variable> coefs[0]
            //   mv.#no^e2# <=> <Variable> coefs[1]
            //   mv.#e1^e2# <=> <Variable> coefs[2]
            //   mv.#no^e3# <=> <Variable> coefs[3]
            //   mv.#e1^e3# <=> <Variable> coefs[4]
            //   mv.#e2^e3# <=> <Variable> coefs[5]
            //   mv.#no^ni# <=> <Variable> coefs[6]
            //   mv.#e1^ni# <=> <Variable> coefs[7]
            //   mv.#e2^ni# <=> <Variable> coefs[8]
            //   mv.#e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = Math.Pow(coefs[2], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[7] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[0] * tempVar0001);
            tempVar0006 = (coefs[7] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[1] * tempVar0001);
            tempVar0003 = (-1 * coefs[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[1] * tempVar0001);
            tempVar0006 = (coefs[8] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[3] * tempVar0001);
            tempVar0003 = (-1 * coefs[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[3] * tempVar0001);
            tempVar0001 = (coefs[9] * tempVar0001);
            tempVar0006 = (tempVar0005 + tempVar0001);
            tempVar0004 = (tempVar0004 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0001 = (tempVar0003 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Abs(tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.4956637+02:00
            
            return result;
        }
        
        
        private static double Mag2_3(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.6076702+02:00
            //Macro: geometry3d.cga.Mag2
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 48 sub-expressions, 0 generated temps, 48 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 1 total.
            //Computations: 1.10204081632653 average, 54 total.
            //Memory Reads: 1.77551020408163 average, 87 total.
            //Memory Writes: 49 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2# <=> <Variable> coefs[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            double tempVar0005;
            double tempVar0006;
            double tempVar0007;
            
            tempVar0000 = Math.Pow(coefs[3], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[6] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[0] * tempVar0001);
            tempVar0006 = (coefs[6] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = Math.Pow(coefs[7], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[1] * tempVar0001);
            tempVar0003 = (-1 * coefs[8] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[1] * tempVar0001);
            tempVar0006 = (coefs[8] * tempVar0001);
            tempVar0007 = (tempVar0005 + tempVar0006);
            tempVar0004 = (tempVar0004 * tempVar0007);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0002 = (tempVar0002 + tempVar0006);
            tempVar0002 = (tempVar0003 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (coefs[2] * tempVar0001);
            tempVar0003 = (-1 * coefs[9] * tempVar0001);
            tempVar0004 = (tempVar0002 + tempVar0003);
            tempVar0005 = (-1 * coefs[2] * tempVar0001);
            tempVar0001 = (coefs[9] * tempVar0001);
            tempVar0006 = (tempVar0005 + tempVar0001);
            tempVar0004 = (tempVar0004 * tempVar0006);
            tempVar0000 = (tempVar0000 + tempVar0004);
            tempVar0003 = (tempVar0003 + tempVar0005);
            tempVar0001 = (tempVar0002 + tempVar0001);
            tempVar0001 = (tempVar0003 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Abs(tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.6136705+02:00
            
            return result;
        }
        
        
        private static double Mag2_4(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.7636791+02:00
            //Macro: geometry3d.cga.Mag2
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1.04761904761905 average, 22 total.
            //Memory Reads: 1.38095238095238 average, 29 total.
            //Memory Writes: 21 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            
            tempVar0000 = Math.Pow(coefs[1], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(2, -0.5);
            tempVar0002 = (-1 * coefs[0] * tempVar0001);
            tempVar0003 = (-1 * coefs[4] * tempVar0001);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = Math.Pow(tempVar0002, 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0001 = (coefs[0] * tempVar0001);
            tempVar0001 = (tempVar0003 + tempVar0001);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Abs(tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.7666792+02:00
            
            return result;
        }
        
        
        private static double Mag2_5(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8176822+02:00
            //Macro: geometry3d.cga.Mag2
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 0.5 average, 1 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Abs(coefs[0]);
            result = Math.Pow(tempVar0000, 2);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8176822+02:00
            
            return result;
        }
        
        
        public double Mag2
        {
            get
            {
                if (IsZeroBlade)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Mag2_0(Coefs);
                    case 1:
                        return Mag2_1(Coefs);
                    case 2:
                        return Mag2_2(Coefs);
                    case 3:
                        return Mag2_3(Coefs);
                    case 4:
                        return Mag2_4(Coefs);
                    case 5:
                        return Mag2_5(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region EMag
        private static double EMag_0(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8186822+02:00
            //Macro: geometry3d.cga.EMag
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 3 total.
            //Memory Reads: 1 average, 3 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#E0# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8196823+02:00
            
            return result;
        }
        
        
        private static double EMag_1(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8206823+02:00
            //Macro: geometry3d.cga.EMag
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 15 total.
            //Memory Reads: 1.26666666666667 average, 19 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no# <=> <Variable> coefs[0]
            //   mv.#e1# <=> <Variable> coefs[1]
            //   mv.#e2# <=> <Variable> coefs[2]
            //   mv.#e3# <=> <Variable> coefs[3]
            //   mv.#ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8226824+02:00
            
            return result;
        }
        
        
        private static double EMag_2(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8256826+02:00
            //Macro: geometry3d.cga.EMag
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 29 sub-expressions, 0 generated temps, 29 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 30 total.
            //Memory Reads: 1.3 average, 39 total.
            //Memory Writes: 30 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1# <=> <Variable> coefs[0]
            //   mv.#no^e2# <=> <Variable> coefs[1]
            //   mv.#e1^e2# <=> <Variable> coefs[2]
            //   mv.#no^e3# <=> <Variable> coefs[3]
            //   mv.#e1^e3# <=> <Variable> coefs[4]
            //   mv.#e2^e3# <=> <Variable> coefs[5]
            //   mv.#no^ni# <=> <Variable> coefs[6]
            //   mv.#e1^ni# <=> <Variable> coefs[7]
            //   mv.#e2^ni# <=> <Variable> coefs[8]
            //   mv.#e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8286828+02:00
            
            return result;
        }
        
        
        private static double EMag_3(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8316830+02:00
            //Macro: geometry3d.cga.EMag
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 29 sub-expressions, 0 generated temps, 29 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 30 total.
            //Memory Reads: 1.3 average, 39 total.
            //Memory Writes: 30 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2# <=> <Variable> coefs[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8356832+02:00
            
            return result;
        }
        
        
        private static double EMag_4(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8376833+02:00
            //Macro: geometry3d.cga.EMag
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 15 total.
            //Memory Reads: 1.26666666666667 average, 19 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8386834+02:00
            
            return result;
        }
        
        
        private static double EMag_5(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8406835+02:00
            //Macro: geometry3d.cga.EMag
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 3 total.
            //Memory Reads: 1 average, 3 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8406835+02:00
            
            return result;
        }
        
        
        public double EMag
        {
            get
            {
                if (IsZeroBlade)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return EMag_0(Coefs);
                    case 1:
                        return EMag_1(Coefs);
                    case 2:
                        return EMag_2(Coefs);
                    case 3:
                        return EMag_3(Coefs);
                    case 4:
                        return EMag_4(Coefs);
                    case 5:
                        return EMag_5(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region EMag2
        private static double EMag2_0(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8416835+02:00
            //Macro: geometry3d.cga.EMag2
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#E0# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            result = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8416835+02:00
            
            return result;
        }
        
        
        private static double EMag2_1(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8436837+02:00
            //Macro: geometry3d.cga.EMag2
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 14 total.
            //Memory Reads: 1.28571428571429 average, 18 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no# <=> <Variable> coefs[0]
            //   mv.#e1# <=> <Variable> coefs[1]
            //   mv.#e2# <=> <Variable> coefs[2]
            //   mv.#e3# <=> <Variable> coefs[3]
            //   mv.#ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8446837+02:00
            
            return result;
        }
        
        
        private static double EMag2_2(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8476839+02:00
            //Macro: geometry3d.cga.EMag2
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 28 sub-expressions, 0 generated temps, 28 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 29 total.
            //Memory Reads: 1.31034482758621 average, 38 total.
            //Memory Writes: 29 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1# <=> <Variable> coefs[0]
            //   mv.#no^e2# <=> <Variable> coefs[1]
            //   mv.#e1^e2# <=> <Variable> coefs[2]
            //   mv.#no^e3# <=> <Variable> coefs[3]
            //   mv.#e1^e3# <=> <Variable> coefs[4]
            //   mv.#e2^e3# <=> <Variable> coefs[5]
            //   mv.#no^ni# <=> <Variable> coefs[6]
            //   mv.#e1^ni# <=> <Variable> coefs[7]
            //   mv.#e2^ni# <=> <Variable> coefs[8]
            //   mv.#e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8506841+02:00
            
            return result;
        }
        
        
        private static double EMag2_3(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8536842+02:00
            //Macro: geometry3d.cga.EMag2
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 28 sub-expressions, 0 generated temps, 28 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 29 total.
            //Memory Reads: 1.31034482758621 average, 38 total.
            //Memory Writes: 29 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2# <=> <Variable> coefs[0]
            //   mv.#no^e1^e3# <=> <Variable> coefs[1]
            //   mv.#no^e2^e3# <=> <Variable> coefs[2]
            //   mv.#e1^e2^e3# <=> <Variable> coefs[3]
            //   mv.#no^e1^ni# <=> <Variable> coefs[4]
            //   mv.#no^e2^ni# <=> <Variable> coefs[5]
            //   mv.#e1^e2^ni# <=> <Variable> coefs[6]
            //   mv.#no^e3^ni# <=> <Variable> coefs[7]
            //   mv.#e1^e3^ni# <=> <Variable> coefs[8]
            //   mv.#e2^e3^ni# <=> <Variable> coefs[9]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8556843+02:00
            
            return result;
        }
        
        
        private static double EMag2_4(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8576845+02:00
            //Macro: geometry3d.cga.EMag2
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 14 total.
            //Memory Reads: 1.28571428571429 average, 18 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8586845+02:00
            
            return result;
        }
        
        
        private static double EMag2_5(double[] coefs)
        {
            var result = 0.0D;
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:45:36.8606846+02:00
            //Macro: geometry3d.cga.EMag2
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            result = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:45:36.8616847+02:00
            
            return result;
        }
        
        
        public double EMag2
        {
            get
            {
                if (IsZeroBlade)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return EMag2_0(Coefs);
                    case 1:
                        return EMag2_1(Coefs);
                    case 2:
                        return EMag2_2(Coefs);
                    case 3:
                        return EMag2_3(Coefs);
                    case 4:
                        return EMag2_4(Coefs);
                    case 5:
                        return EMag2_5(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
    }
}
