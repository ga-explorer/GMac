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
        private static double[] SelfEGP_000(double[] coefs)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.3845449+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
            //   mv.#E0# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            c[0] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.3845449+02:00
            
            return c;
        }
        
        
        private static cga0001Blade[] SelfEGP_00(double[] coefs)
        {
            return new[]
            {
                new cga0001Blade(0, SelfEGP_000(coefs))
            };
        }
        
        private static double[] SelfEGP_110(double[] coefs)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.3875451+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 14 total.
            //Memory Reads: 1.28571428571429 average, 18 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
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
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.3895452+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_112(double[] coefs)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.3935454+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
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
            //   mv.#no# <=> <Variable> coefs[0]
            //   mv.#e1# <=> <Variable> coefs[1]
            //   mv.#e2# <=> <Variable> coefs[2]
            //   mv.#e3# <=> <Variable> coefs[3]
            //   mv.#ni# <=> <Variable> coefs[4]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            c[5] = 0;
            c[6] = 0;
            c[7] = 0;
            c[8] = 0;
            c[9] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.3955455+02:00
            
            return c;
        }
        
        
        private static cga0001Blade[] SelfEGP_11(double[] coefs)
        {
            return new[]
            {
                new cga0001Blade(0, SelfEGP_110(coefs)),
                new cga0001Blade(2, SelfEGP_112(coefs))
            };
        }
        
        private static double[] SelfEGP_220(double[] coefs)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4005458+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 18 sub-expressions, 0 generated temps, 18 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 19 total.
            //Memory Reads: 1.47368421052632 average, 28 total.
            //Memory Writes: 19 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
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
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4025459+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_222(double[] coefs)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4085463+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
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
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            c[5] = 0;
            c[6] = 0;
            c[7] = 0;
            c[8] = 0;
            c[9] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4095463+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_224(double[] coefs)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4155467+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1.6 average, 40 total.
            //Memory Reads: 2 average, 50 total.
            //Memory Writes: 25 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
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
            
            tempVar0000 = (-2 * coefs[2] * coefs[3]);
            tempVar0001 = (2 * coefs[1] * coefs[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[5]);
            c[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs[2] * coefs[6]);
            tempVar0001 = (2 * coefs[1] * coefs[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[8]);
            c[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs[4] * coefs[6]);
            tempVar0001 = (2 * coefs[3] * coefs[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[9]);
            c[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs[5] * coefs[6]);
            tempVar0001 = (2 * coefs[3] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[1] * coefs[9]);
            c[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs[5] * coefs[7]);
            tempVar0001 = (2 * coefs[4] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[2] * coefs[9]);
            c[4] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4185468+02:00
            
            return c;
        }
        
        
        private static cga0001Blade[] SelfEGP_22(double[] coefs)
        {
            return new[]
            {
                new cga0001Blade(0, SelfEGP_220(coefs)),
                new cga0001Blade(2, SelfEGP_222(coefs)),
                new cga0001Blade(4, SelfEGP_224(coefs))
            };
        }
        
        private static double[] SelfEGP_330(double[] coefs)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4245472+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 18 sub-expressions, 0 generated temps, 18 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 19 total.
            //Memory Reads: 1.47368421052632 average, 28 total.
            //Memory Writes: 19 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
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
            tempVar0001 = Math.Pow(coefs[1], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[2], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[3], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[4], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[5], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[6], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[7], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[8], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(coefs[9], 2);
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4265473+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_332(double[] coefs)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4325476+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
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
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            c[5] = 0;
            c[6] = 0;
            c[7] = 0;
            c[8] = 0;
            c[9] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4335477+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_334(double[] coefs)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4395480+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 10 not used, 10 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 1.6 average, 40 total.
            //Memory Reads: 2 average, 50 total.
            //Memory Writes: 25 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
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
            
            tempVar0000 = (-2 * coefs[6] * coefs[7]);
            tempVar0001 = (2 * coefs[5] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[4] * coefs[9]);
            c[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (2 * coefs[3] * coefs[7]);
            tempVar0001 = (-2 * coefs[2] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * coefs[1] * coefs[9]);
            c[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs[3] * coefs[5]);
            tempVar0001 = (2 * coefs[2] * coefs[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[9]);
            c[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (2 * coefs[3] * coefs[4]);
            tempVar0001 = (-2 * coefs[1] * coefs[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * coefs[0] * coefs[8]);
            c[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * coefs[2] * coefs[4]);
            tempVar0001 = (2 * coefs[1] * coefs[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[7]);
            c[4] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4425482+02:00
            
            return c;
        }
        
        
        private static cga0001Blade[] SelfEGP_33(double[] coefs)
        {
            return new[]
            {
                new cga0001Blade(0, SelfEGP_330(coefs)),
                new cga0001Blade(2, SelfEGP_332(coefs)),
                new cga0001Blade(4, SelfEGP_334(coefs))
            };
        }
        
        private static double[] SelfEGP_440(double[] coefs)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4475485+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 5 not used, 5 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 14 total.
            //Memory Reads: 1.28571428571429 average, 18 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
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
            c[0] = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4495486+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_442(double[] coefs)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4535488+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
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
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            c[5] = 0;
            c[6] = 0;
            c[7] = 0;
            c[8] = 0;
            c[9] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4555489+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_444(double[] coefs)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4605492+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 5 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   mv.#no^e1^e2^e3# <=> <Variable> coefs[0]
            //   mv.#no^e1^e2^ni# <=> <Variable> coefs[1]
            //   mv.#no^e1^e3^ni# <=> <Variable> coefs[2]
            //   mv.#no^e2^e3^ni# <=> <Variable> coefs[3]
            //   mv.#e1^e2^e3^ni# <=> <Variable> coefs[4]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4605492+02:00
            
            return c;
        }
        
        
        private static cga0001Blade[] SelfEGP_44(double[] coefs)
        {
            return new[]
            {
                new cga0001Blade(0, SelfEGP_440(coefs)),
                new cga0001Blade(2, SelfEGP_442(coefs)),
                new cga0001Blade(4, SelfEGP_444(coefs))
            };
        }
        
        private static double[] SelfEGP_550(double[] coefs)
        {
            var c = new double[1];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4625493+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> c[0]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(coefs[0], 2);
            c[0] = (-1 * tempVar0000);
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4635494+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_552(double[] coefs)
        {
            var c = new double[10];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4665496+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 10 total.
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
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            c[5] = 0;
            c[6] = 0;
            c[7] = 0;
            c[8] = 0;
            c[9] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4665496+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_554(double[] coefs)
        {
            var c = new double[5];
        
            //Bagin GMac Macro Code Generation, 2015-12-20T21:46:09.4695497+02:00
            //Macro: geometry3d.cga.SelfEGP
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 5 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#no^e1^e2^e3# <=> <Variable> c[0]
            //   result.#no^e1^e2^ni# <=> <Variable> c[1]
            //   result.#no^e1^e3^ni# <=> <Variable> c[2]
            //   result.#no^e2^e3^ni# <=> <Variable> c[3]
            //   result.#e1^e2^e3^ni# <=> <Variable> c[4]
            //   mv.#no^e1^e2^e3^ni# <=> <Variable> coefs[0]
            
            
            c[0] = 0;
            c[1] = 0;
            c[2] = 0;
            c[3] = 0;
            c[4] = 0;
            
            //Finish GMac Macro Code Generation, 2015-12-20T21:46:09.4695497+02:00
            
            return c;
        }
        
        
        private static cga0001Blade[] SelfEGP_55(double[] coefs)
        {
            return new[]
            {
                new cga0001Blade(0, SelfEGP_550(coefs)),
                new cga0001Blade(2, SelfEGP_552(coefs)),
                new cga0001Blade(4, SelfEGP_554(coefs))
            };
        }
        
        public cga0001Blade[] SelfEGP()
        {
            if (IsZero)
                return new cga0001Blade[0];
        
            switch (Grade)
            {
                //grade: 0
                case 0:
                    return SelfEGP_00(Coefs);
                //grade: 1
                case 1:
                    return SelfEGP_11(Coefs);
                //grade: 2
                case 2:
                    return SelfEGP_22(Coefs);
                //grade: 3
                case 3:
                    return SelfEGP_33(Coefs);
                //grade: 4
                case 4:
                    return SelfEGP_44(Coefs);
                //grade: 5
                case 5:
                    return SelfEGP_55(Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
