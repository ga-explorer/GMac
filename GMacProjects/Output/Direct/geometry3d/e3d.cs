using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geometry3d
{
    public static class e3d
    {
        public sealed class Multivector
        {
            public readonly double[] Coef = new double[8];
            
            
            public Multivector()
            {
            }
            
            public Multivector(params double[] coefs)
            {
                int i = 0;
                foreach (var coef in coefs.Take(8))
                    Coef[i++] = coef;
            }
            
            public Multivector(IEnumerable<double> coefs)
            {
                int i = 0;
                foreach (var coef in coefs.Take(8))
                    Coef[i++] = coef;
            }
            
            
        }
        
        public static readonly geometry3d.e3d.Multivector I = new geometry3d.e3d.Multivector(0, 0, 0, 0, 0, 0, 0, 1);
        
        public static readonly geometry3d.e3d.Multivector E0 = new geometry3d.e3d.Multivector(1, 0, 0, 0, 0, 0, 0, 0);
        
        public static readonly geometry3d.e3d.Multivector E1 = new geometry3d.e3d.Multivector(0, 1, 0, 0, 0, 0, 0, 0);
        
        public static readonly geometry3d.e3d.Multivector E2 = new geometry3d.e3d.Multivector(0, 0, 1, 0, 0, 0, 0, 0);
        
        public static readonly geometry3d.e3d.Multivector E3 = new geometry3d.e3d.Multivector(0, 0, 0, 1, 0, 0, 0, 0);
        
        public static readonly geometry3d.e3d.Multivector E4 = new geometry3d.e3d.Multivector(0, 0, 0, 0, 1, 0, 0, 0);
        
        public static readonly geometry3d.e3d.Multivector E5 = new geometry3d.e3d.Multivector(0, 0, 0, 0, 0, 1, 0, 0);
        
        public static readonly geometry3d.e3d.Multivector E6 = new geometry3d.e3d.Multivector(0, 0, 0, 0, 0, 0, 1, 0);
        
        public static readonly geometry3d.e3d.Multivector E7 = new geometry3d.e3d.Multivector(0, 0, 0, 0, 0, 0, 0, 1);
        
        public static double Mag(geometry3d.e3d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:57 م
            //Macro: geometry3d.e3d.Mag
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 25 sub-expressions, 0 generated temps, 25 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 0.846153846153846 average, 22 total.
            //Memory Reads: 1.26923076923077 average, 33 total.
            //Memory Writes: 26 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = Math.Abs(mv.Coef[0]);
            tempVar0000 = Math.Pow(tempVar0000, 2);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0002 = Math.Pow(mv.Coef[2], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(mv.Coef[4], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Abs(tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0002 = Math.Pow(mv.Coef[5], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(mv.Coef[6], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Abs(tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Abs(mv.Coef[7]);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            return result;
        }
        
        public static double Mag2(geometry3d.e3d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:57 م
            //Macro: geometry3d.e3d.Mag2
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 0.84 average, 21 total.
            //Memory Reads: 1.28 average, 32 total.
            //Memory Writes: 25 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = Math.Abs(mv.Coef[0]);
            tempVar0000 = Math.Pow(tempVar0000, 2);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0002 = Math.Pow(mv.Coef[2], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(mv.Coef[4], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Abs(tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0002 = Math.Pow(mv.Coef[5], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = Math.Pow(mv.Coef[6], 2);
            tempVar0002 = (-1 * tempVar0002);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = Math.Abs(tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Abs(mv.Coef[7]);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double Norm2(geometry3d.e3d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:57 م
            //Macro: geometry3d.e3d.Norm2
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 22 sub-expressions, 0 generated temps, 22 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 23 total.
            //Memory Reads: 1.30434782608696 average, 30 total.
            //Memory Writes: 23 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector InvVersor(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:57 م
            //Macro: geometry3d.e3d.InvVersor
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.125 average, 36 total.
            //Memory Reads: 1.46875 average, 47 total.
            //Memory Writes: 32 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(tempVar0000, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (-1 * mv.Coef[3] * tempVar0000);
            result.Coef[4] = (mv.Coef[4] * tempVar0000);
            result.Coef[5] = (-1 * mv.Coef[5] * tempVar0000);
            result.Coef[6] = (-1 * mv.Coef[6] * tempVar0000);
            result.Coef[7] = (-1 * mv.Coef[7] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector Dual(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:57 م
            //Macro: geometry3d.e3d.Dual
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            
            result.Coef[0] = mv.Coef[7];
            result.Coef[1] = mv.Coef[6];
            result.Coef[2] = (-1 * mv.Coef[5]);
            result.Coef[3] = (-1 * mv.Coef[4]);
            result.Coef[4] = mv.Coef[3];
            result.Coef[5] = mv.Coef[2];
            result.Coef[6] = (-1 * mv.Coef[1]);
            result.Coef[7] = (-1 * mv.Coef[0]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector SelfGP(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.SelfGP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.36363636363636 average, 60 total.
            //Memory Reads: 1.72727272727273 average, 76 total.
            //Memory Writes: 44 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[1]);
            tempVar0001 = (2 * mv.Coef[6] * mv.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[2]);
            tempVar0001 = (-2 * mv.Coef[5] * mv.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[3]);
            tempVar0001 = (-2 * mv.Coef[4] * mv.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[4]);
            tempVar0001 = (2 * mv.Coef[3] * mv.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[5]);
            tempVar0001 = (2 * mv.Coef[2] * mv.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[6]);
            tempVar0001 = (-2 * mv.Coef[1] * mv.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[3] * mv.Coef[4]);
            tempVar0001 = (2 * mv.Coef[2] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[1] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[0] * mv.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector SelfGPRev(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.SelfGPRev
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.16666666666667 average, 56 total.
            //Memory Reads: 1.5 average, 72 total.
            //Memory Writes: 48 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = 0;
            result.Coef[5] = 0;
            result.Coef[6] = 0;
            result.Coef[7] = 0;
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[1]);
            tempVar0001 = (-2 * mv.Coef[2] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[4] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[6] * mv.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[2]);
            tempVar0001 = (2 * mv.Coef[1] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[4] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * mv.Coef[5] * mv.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[4]);
            tempVar0001 = (2 * mv.Coef[1] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * mv.Coef[2] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[3] * mv.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector Negative(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.Negative
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            
            result.Coef[0] = (-1 * mv.Coef[0]);
            result.Coef[1] = (-1 * mv.Coef[1]);
            result.Coef[2] = (-1 * mv.Coef[2]);
            result.Coef[3] = (-1 * mv.Coef[3]);
            result.Coef[4] = (-1 * mv.Coef[4]);
            result.Coef[5] = (-1 * mv.Coef[5]);
            result.Coef[6] = (-1 * mv.Coef[6]);
            result.Coef[7] = (-1 * mv.Coef[7]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector Reverse(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.Reverse
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            
            result.Coef[0] = mv.Coef[0];
            result.Coef[1] = mv.Coef[1];
            result.Coef[2] = mv.Coef[2];
            result.Coef[3] = (-1 * mv.Coef[3]);
            result.Coef[4] = mv.Coef[4];
            result.Coef[5] = (-1 * mv.Coef[5]);
            result.Coef[6] = (-1 * mv.Coef[6]);
            result.Coef[7] = (-1 * mv.Coef[7]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector GradeInv(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.GradeInv
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            
            result.Coef[0] = mv.Coef[0];
            result.Coef[1] = (-1 * mv.Coef[1]);
            result.Coef[2] = (-1 * mv.Coef[2]);
            result.Coef[3] = mv.Coef[3];
            result.Coef[4] = (-1 * mv.Coef[4]);
            result.Coef[5] = mv.Coef[5];
            result.Coef[6] = mv.Coef[6];
            result.Coef[7] = (-1 * mv.Coef[7]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector CliffConj(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.CliffConj
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 6 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            
            result.Coef[0] = mv.Coef[0];
            result.Coef[1] = (-1 * mv.Coef[1]);
            result.Coef[2] = (-1 * mv.Coef[2]);
            result.Coef[3] = (-1 * mv.Coef[3]);
            result.Coef[4] = (-1 * mv.Coef[4]);
            result.Coef[5] = (-1 * mv.Coef[5]);
            result.Coef[6] = (-1 * mv.Coef[6]);
            result.Coef[7] = mv.Coef[7];
            
            return result;
        }
        
        public static double EMag(geometry3d.e3d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.EMag
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 23 sub-expressions, 0 generated temps, 23 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 24 total.
            //Memory Reads: 1.29166666666667 average, 31 total.
            //Memory Writes: 24 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            return result;
        }
        
        public static double EMag2(geometry3d.e3d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.EMag2
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 22 sub-expressions, 0 generated temps, 22 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 23 total.
            //Memory Reads: 1.30434782608696 average, 30 total.
            //Memory Writes: 23 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector InvEVersor(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:58 م
            //Macro: geometry3d.e3d.InvEVersor
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.125 average, 36 total.
            //Memory Reads: 1.46875 average, 47 total.
            //Memory Writes: 32 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(tempVar0000, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (-1 * mv.Coef[3] * tempVar0000);
            result.Coef[4] = (mv.Coef[4] * tempVar0000);
            result.Coef[5] = (-1 * mv.Coef[5] * tempVar0000);
            result.Coef[6] = (-1 * mv.Coef[6] * tempVar0000);
            result.Coef[7] = (-1 * mv.Coef[7] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EDual(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.EDual
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            
            result.Coef[0] = mv.Coef[7];
            result.Coef[1] = mv.Coef[6];
            result.Coef[2] = (-1 * mv.Coef[5]);
            result.Coef[3] = (-1 * mv.Coef[4]);
            result.Coef[4] = mv.Coef[3];
            result.Coef[5] = mv.Coef[2];
            result.Coef[6] = (-1 * mv.Coef[1]);
            result.Coef[7] = (-1 * mv.Coef[0]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector SelfEGP(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.SelfEGP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.36363636363636 average, 60 total.
            //Memory Reads: 1.72727272727273 average, 76 total.
            //Memory Writes: 44 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[1]);
            tempVar0001 = (2 * mv.Coef[6] * mv.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[2]);
            tempVar0001 = (-2 * mv.Coef[5] * mv.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[3]);
            tempVar0001 = (-2 * mv.Coef[4] * mv.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[4]);
            tempVar0001 = (2 * mv.Coef[3] * mv.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[5]);
            tempVar0001 = (2 * mv.Coef[2] * mv.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[6]);
            tempVar0001 = (-2 * mv.Coef[1] * mv.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[3] * mv.Coef[4]);
            tempVar0001 = (2 * mv.Coef[2] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[1] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[0] * mv.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector SelfEGPRev(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.SelfEGPRev
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.16666666666667 average, 56 total.
            //Memory Reads: 1.5 average, 72 total.
            //Memory Writes: 48 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = 0;
            result.Coef[5] = 0;
            result.Coef[6] = 0;
            result.Coef[7] = 0;
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[1]);
            tempVar0001 = (-2 * mv.Coef[2] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[4] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[6] * mv.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[2]);
            tempVar0001 = (2 * mv.Coef[1] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[4] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * mv.Coef[5] * mv.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[4]);
            tempVar0001 = (2 * mv.Coef[1] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * mv.Coef[2] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * mv.Coef[3] * mv.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double SP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.SP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.26666666666667 average, 19 total.
            //Memory Reads: 2 average, 30 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector GP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.GP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 112 sub-expressions, 0 generated temps, 112 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.33333333333333 average, 160 total.
            //Memory Reads: 2 average, 240 total.
            //Memory Writes: 120 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector LCP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.3695652173913 average, 63 total.
            //Memory Reads: 2 average, 92 total.
            //Memory Writes: 46 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[7] = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector RCP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.3695652173913 average, 63 total.
            //Memory Reads: 2 average, 92 total.
            //Memory Writes: 46 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[7] = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector FDP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.FDP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 76 sub-expressions, 0 generated temps, 76 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.35714285714286 average, 114 total.
            //Memory Reads: 2 average, 168 total.
            //Memory Writes: 84 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector HIP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.HIP
            //Input Variables: 14 used, 2 not used, 16 total.
            //Temp Variables: 48 sub-expressions, 0 generated temps, 48 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.25 average, 70 total.
            //Memory Reads: 1.96428571428571 average, 110 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[7] = 0;
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector CP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.CP
            //Input Variables: 12 used, 4 not used, 16 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.22727272727273 average, 54 total.
            //Memory Reads: 1.90909090909091 average, 84 total.
            //Memory Writes: 44 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = 0;
            result.Coef[7] = 0;
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[6]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[5]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ACP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.ACP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 64 sub-expressions, 0 generated temps, 64 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.38888888888889 average, 100 total.
            //Memory Reads: 2 average, 144 total.
            //Memory Writes: 72 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector GPDual(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.GPDual
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 116 sub-expressions, 0 generated temps, 116 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.32258064516129 average, 164 total.
            //Memory Reads: 1.96774193548387 average, 244 total.
            //Memory Writes: 124 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[3] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[6] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[7] = (-1 * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector DWP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.DWP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 232 sub-expressions, 0 generated temps, 232 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.33333333333333 average, 320 total.
            //Memory Reads: 2 average, 480 total.
            //Memory Writes: 240 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[5] * mv2.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[6] * mv2.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[7] * mv2.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[4] * mv2.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[7] * mv2.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[6] * mv2.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempArray[4] = (mv1.Coef[3] * mv2.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[4] * mv2.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[5] * mv2.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempArray[6] = (mv1.Coef[5] * mv2.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[6] * mv2.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[7] * mv2.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[3] * mv2.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempArray[7] = (mv1.Coef[4] * mv2.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[7] * mv2.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[6] * mv2.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[2] * mv2.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempArray[8] = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[4] * mv2.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[3] * mv2.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempArray[9] = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * mv2.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * mv2.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[8]);
            result.Coef[0] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[8]);
            result.Coef[1] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[9] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[8]);
            result.Coef[2] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[8]);
            result.Coef[3] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[0]);
            tempArray[9] = (mv1.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[8]);
            result.Coef[4] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[8]);
            result.Coef[5] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[6] * tempArray[0]);
            tempArray[9] = (mv1.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * tempArray[8]);
            result.Coef[6] = (tempArray[1] + tempArray[9]);
            tempArray[0] = (mv1.Coef[7] * tempArray[0]);
            tempArray[1] = (mv1.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[8]);
            result.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector GWP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.GWP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 232 sub-expressions, 0 generated temps, 232 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.3 average, 312 total.
            //Memory Reads: 2 average, 480 total.
            //Memory Writes: 240 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[4] * mv2.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[5] * mv2.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[6] * mv2.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[5] * mv2.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[4] * mv2.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[7] * mv2.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[0] * mv2.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[6] * mv2.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[4] * mv2.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[5] * mv2.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[7] * mv2.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[5] * mv2.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[4] * mv2.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[7] * mv2.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[0] * mv2.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempArray[7] = (-1 * mv1.Coef[4] * mv2.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[6] * mv2.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[1] * mv2.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempArray[8] = (mv1.Coef[7] * mv2.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[4] * mv2.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[2] * mv2.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[3] * mv2.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[1] * mv2.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempArray[9] = (mv1.Coef[6] * mv2.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * mv2.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[3] * mv2.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * mv2.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[0] * mv2.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[8]);
            result.Coef[0] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[8]);
            result.Coef[1] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[9] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[8]);
            result.Coef[2] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[8]);
            result.Coef[3] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[0]);
            tempArray[9] = (mv1.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[8]);
            result.Coef[4] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[8]);
            result.Coef[5] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[6] * tempArray[0]);
            tempArray[9] = (mv1.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * tempArray[8]);
            result.Coef[6] = (tempArray[1] + tempArray[9]);
            tempArray[0] = (mv1.Coef[7] * tempArray[0]);
            tempArray[1] = (mv1.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[8]);
            result.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector Times(geometry3d.e3d.Multivector mv, double s)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.Times
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 2 average, 16 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            //    s = <variable>
            
            
            result.Coef[0] = (mv.Coef[0] * s);
            result.Coef[1] = (mv.Coef[1] * s);
            result.Coef[2] = (mv.Coef[2] * s);
            result.Coef[3] = (mv.Coef[3] * s);
            result.Coef[4] = (mv.Coef[4] * s);
            result.Coef[5] = (mv.Coef[5] * s);
            result.Coef[6] = (mv.Coef[6] * s);
            result.Coef[7] = (mv.Coef[7] * s);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector Divide(geometry3d.e3d.Multivector mv, double s)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.Divide
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 9 total.
            //Memory Reads: 1.88888888888889 average, 17 total.
            //Memory Writes: 9 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            //    s = <variable>
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(s, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (mv.Coef[3] * tempVar0000);
            result.Coef[4] = (mv.Coef[4] * tempVar0000);
            result.Coef[5] = (mv.Coef[5] * tempVar0000);
            result.Coef[6] = (mv.Coef[6] * tempVar0000);
            result.Coef[7] = (mv.Coef[7] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector OP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.OP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.47826086956522 average, 68 total.
            //Memory Reads: 2 average, 92 total.
            //Memory Writes: 46 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double ESP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.ESP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.26666666666667 average, 19 total.
            //Memory Reads: 2 average, 30 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EGP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:59:59 م
            //Macro: geometry3d.e3d.EGP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 112 sub-expressions, 0 generated temps, 112 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.33333333333333 average, 160 total.
            //Memory Reads: 2 average, 240 total.
            //Memory Writes: 120 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ELCP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.ELCP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.3695652173913 average, 63 total.
            //Memory Reads: 2 average, 92 total.
            //Memory Writes: 46 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[7] = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ERCP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.ERCP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.3695652173913 average, 63 total.
            //Memory Reads: 2 average, 92 total.
            //Memory Writes: 46 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[7] = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EFDP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.EFDP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 76 sub-expressions, 0 generated temps, 76 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.35714285714286 average, 114 total.
            //Memory Reads: 2 average, 168 total.
            //Memory Writes: 84 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EHIP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.EHIP
            //Input Variables: 14 used, 2 not used, 16 total.
            //Temp Variables: 48 sub-expressions, 0 generated temps, 48 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.25 average, 70 total.
            //Memory Reads: 1.96428571428571 average, 110 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[7] = 0;
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ECP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.ECP
            //Input Variables: 12 used, 4 not used, 16 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.22727272727273 average, 54 total.
            //Memory Reads: 1.90909090909091 average, 84 total.
            //Memory Writes: 44 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = 0;
            result.Coef[7] = 0;
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[6]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[5]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EACP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.EACP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 64 sub-expressions, 0 generated temps, 64 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.38888888888889 average, 100 total.
            //Memory Reads: 2 average, 144 total.
            //Memory Writes: 72 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EGPDual(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.EGPDual
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 116 sub-expressions, 0 generated temps, 116 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.32258064516129 average, 164 total.
            //Memory Reads: 1.96774193548387 average, 244 total.
            //Memory Writes: 124 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[3] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[4] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[6] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[5] * mv2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[6] * mv2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[7] * mv2.Coef[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[7] = (-1 * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EDWP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.EDWP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 232 sub-expressions, 0 generated temps, 232 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.33333333333333 average, 320 total.
            //Memory Reads: 2 average, 480 total.
            //Memory Writes: 240 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[4] * mv2.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[5] * mv2.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[6] * mv2.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[7] * mv2.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[5] * mv2.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[4] * mv2.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[7] * mv2.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[6] * mv2.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempArray[4] = (mv1.Coef[3] * mv2.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[6] * mv2.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[4] * mv2.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[5] * mv2.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[7] * mv2.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[5] * mv2.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempArray[6] = (mv1.Coef[5] * mv2.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[6] * mv2.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[7] * mv2.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[0] * mv2.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[3] * mv2.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempArray[7] = (mv1.Coef[4] * mv2.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[7] * mv2.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[6] * mv2.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[1] * mv2.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[2] * mv2.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempArray[8] = (-1 * mv1.Coef[7] * mv2.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[4] * mv2.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[2] * mv2.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[3] * mv2.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[1] * mv2.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempArray[9] = (-1 * mv1.Coef[6] * mv2.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * mv2.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * mv2.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * mv2.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * mv2.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[8]);
            result.Coef[0] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[8]);
            result.Coef[1] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[9] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[8]);
            result.Coef[2] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[8]);
            result.Coef[3] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[0]);
            tempArray[9] = (mv1.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[8]);
            result.Coef[4] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[8]);
            result.Coef[5] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[6] * tempArray[0]);
            tempArray[9] = (mv1.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * tempArray[8]);
            result.Coef[6] = (tempArray[1] + tempArray[9]);
            tempArray[0] = (mv1.Coef[7] * tempArray[0]);
            tempArray[1] = (mv1.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[8]);
            result.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector EGWP(geometry3d.e3d.Multivector mv1, geometry3d.e3d.Multivector mv2)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.EGWP
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 232 sub-expressions, 0 generated temps, 232 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.3 average, 312 total.
            //Memory Reads: 2 average, 480 total.
            //Memory Writes: 240 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv1.#e3# = <variable>
            //    mv1.#e1^e3# = <variable>
            //    mv1.#e2^e3# = <variable>
            //    mv1.#e1^e2^e3# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            //    mv2.#e3# = <variable>
            //    mv2.#e1^e3# = <variable>
            //    mv2.#e2^e3# = <variable>
            //    mv2.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[4] * mv2.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[5] * mv2.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[6] * mv2.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[5] * mv2.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[4] * mv2.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[7] * mv2.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[0] * mv2.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[6] * mv2.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[4] * mv2.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (mv1.Coef[5] * mv2.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[7] * mv2.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[5] * mv2.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[4] * mv2.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[0]);
            tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[7] * mv2.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (mv1.Coef[0] * mv2.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[1] * mv2.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[2] * mv2.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[0]);
            tempArray[7] = (-1 * mv1.Coef[4] * mv2.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[6] * mv2.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (mv1.Coef[1] * mv2.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[0] * mv2.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[0]);
            tempArray[8] = (mv1.Coef[7] * mv2.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[4] * mv2.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[5] * mv2.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[2] * mv2.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[3] * mv2.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[0] * mv2.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (mv1.Coef[1] * mv2.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * mv1.Coef[7] * mv2.Coef[0]);
            tempArray[9] = (mv1.Coef[6] * mv2.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * mv2.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * mv2.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[3] * mv2.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * mv2.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * mv2.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (mv1.Coef[0] * mv2.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[8]);
            result.Coef[0] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[8]);
            result.Coef[1] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[9] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[8]);
            result.Coef[2] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[8]);
            result.Coef[3] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[0]);
            tempArray[9] = (mv1.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[8]);
            result.Coef[4] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[8]);
            result.Coef[5] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (mv1.Coef[6] * tempArray[0]);
            tempArray[9] = (mv1.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (mv1.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * mv1.Coef[1] * tempArray[8]);
            result.Coef[6] = (tempArray[1] + tempArray[9]);
            tempArray[0] = (mv1.Coef[7] * tempArray[0]);
            tempArray[1] = (mv1.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[8]);
            result.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyLT(geometry3d.e3d.LTStruct tr, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.ApplyLT
            //Input Variables: 72 used, 0 not used, 72 total.
            //Temp Variables: 112 sub-expressions, 0 generated temps, 112 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 120 total.
            //Memory Reads: 2 average, 240 total.
            //Memory Writes: 120 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    tr.ImageE7.#E0# = <variable>
            //    tr.ImageE7.#e1# = <variable>
            //    tr.ImageE7.#e2# = <variable>
            //    tr.ImageE7.#e1^e2# = <variable>
            //    tr.ImageE7.#e3# = <variable>
            //    tr.ImageE7.#e1^e3# = <variable>
            //    tr.ImageE7.#e2^e3# = <variable>
            //    tr.ImageE7.#e1^e2^e3# = <variable>
            //    tr.ImageE6.#E0# = <variable>
            //    tr.ImageE6.#e1# = <variable>
            //    tr.ImageE6.#e2# = <variable>
            //    tr.ImageE6.#e1^e2# = <variable>
            //    tr.ImageE6.#e3# = <variable>
            //    tr.ImageE6.#e1^e3# = <variable>
            //    tr.ImageE6.#e2^e3# = <variable>
            //    tr.ImageE6.#e1^e2^e3# = <variable>
            //    tr.ImageE5.#E0# = <variable>
            //    tr.ImageE5.#e1# = <variable>
            //    tr.ImageE5.#e2# = <variable>
            //    tr.ImageE5.#e1^e2# = <variable>
            //    tr.ImageE5.#e3# = <variable>
            //    tr.ImageE5.#e1^e3# = <variable>
            //    tr.ImageE5.#e2^e3# = <variable>
            //    tr.ImageE5.#e1^e2^e3# = <variable>
            //    tr.ImageE4.#E0# = <variable>
            //    tr.ImageE4.#e1# = <variable>
            //    tr.ImageE4.#e2# = <variable>
            //    tr.ImageE4.#e1^e2# = <variable>
            //    tr.ImageE4.#e3# = <variable>
            //    tr.ImageE4.#e1^e3# = <variable>
            //    tr.ImageE4.#e2^e3# = <variable>
            //    tr.ImageE4.#e1^e2^e3# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE3.#e3# = <variable>
            //    tr.ImageE3.#e1^e3# = <variable>
            //    tr.ImageE3.#e2^e3# = <variable>
            //    tr.ImageE3.#e1^e2^e3# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE2.#e3# = <variable>
            //    tr.ImageE2.#e1^e3# = <variable>
            //    tr.ImageE2.#e2^e3# = <variable>
            //    tr.ImageE2.#e1^e2^e3# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE1.#e3# = <variable>
            //    tr.ImageE1.#e1^e3# = <variable>
            //    tr.ImageE1.#e2^e3# = <variable>
            //    tr.ImageE1.#e1^e2^e3# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE0.#e3# = <variable>
            //    tr.ImageE0.#e1^e3# = <variable>
            //    tr.ImageE0.#e2^e3# = <variable>
            //    tr.ImageE0.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (tr.ImageE0.Coef[0] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[0] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[0] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[0] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[0] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[0] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[0] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[0] * mv.Coef[7]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[1] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[1] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[1] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[1] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[1] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[1] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[1] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[1] * mv.Coef[7]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[2] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[2] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[2] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[2] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[2] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[2] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[2] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[2] * mv.Coef[7]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[3] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[3] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[3] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[3] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[3] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[3] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[3] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[3] * mv.Coef[7]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[4] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[4] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[4] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[4] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[4] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[4] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[4] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[4] * mv.Coef[7]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[5] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[5] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[5] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[5] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[5] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[5] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[5] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[5] * mv.Coef[7]);
            result.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[6] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[6] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[6] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[6] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[6] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[6] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[6] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[6] * mv.Coef[7]);
            result.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[7] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[7] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[7] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[7] * mv.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE4.Coef[7] * mv.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE5.Coef[7] * mv.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE6.Coef[7] * mv.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE7.Coef[7] * mv.Coef[7]);
            result.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct TransLT(geometry3d.e3d.LTStruct tr)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:00 م
            //Macro: geometry3d.e3d.TransLT
            //Input Variables: 64 used, 0 not used, 64 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 64 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 64 total.
            //Memory Writes: 64 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    tr.ImageE7.#E0# = <variable>
            //    tr.ImageE7.#e1# = <variable>
            //    tr.ImageE7.#e2# = <variable>
            //    tr.ImageE7.#e1^e2# = <variable>
            //    tr.ImageE7.#e3# = <variable>
            //    tr.ImageE7.#e1^e3# = <variable>
            //    tr.ImageE7.#e2^e3# = <variable>
            //    tr.ImageE7.#e1^e2^e3# = <variable>
            //    tr.ImageE6.#E0# = <variable>
            //    tr.ImageE6.#e1# = <variable>
            //    tr.ImageE6.#e2# = <variable>
            //    tr.ImageE6.#e1^e2# = <variable>
            //    tr.ImageE6.#e3# = <variable>
            //    tr.ImageE6.#e1^e3# = <variable>
            //    tr.ImageE6.#e2^e3# = <variable>
            //    tr.ImageE6.#e1^e2^e3# = <variable>
            //    tr.ImageE5.#E0# = <variable>
            //    tr.ImageE5.#e1# = <variable>
            //    tr.ImageE5.#e2# = <variable>
            //    tr.ImageE5.#e1^e2# = <variable>
            //    tr.ImageE5.#e3# = <variable>
            //    tr.ImageE5.#e1^e3# = <variable>
            //    tr.ImageE5.#e2^e3# = <variable>
            //    tr.ImageE5.#e1^e2^e3# = <variable>
            //    tr.ImageE4.#E0# = <variable>
            //    tr.ImageE4.#e1# = <variable>
            //    tr.ImageE4.#e2# = <variable>
            //    tr.ImageE4.#e1^e2# = <variable>
            //    tr.ImageE4.#e3# = <variable>
            //    tr.ImageE4.#e1^e3# = <variable>
            //    tr.ImageE4.#e2^e3# = <variable>
            //    tr.ImageE4.#e1^e2^e3# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE3.#e3# = <variable>
            //    tr.ImageE3.#e1^e3# = <variable>
            //    tr.ImageE3.#e2^e3# = <variable>
            //    tr.ImageE3.#e1^e2^e3# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE2.#e3# = <variable>
            //    tr.ImageE2.#e1^e3# = <variable>
            //    tr.ImageE2.#e2^e3# = <variable>
            //    tr.ImageE2.#e1^e2^e3# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE1.#e3# = <variable>
            //    tr.ImageE1.#e1^e3# = <variable>
            //    tr.ImageE1.#e2^e3# = <variable>
            //    tr.ImageE1.#e1^e2^e3# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE0.#e3# = <variable>
            //    tr.ImageE0.#e1^e3# = <variable>
            //    tr.ImageE0.#e2^e3# = <variable>
            //    tr.ImageE0.#e1^e2^e3# = <variable>
            
            
            result.ImageE7.Coef[0] = tr.ImageE0.Coef[7];
            result.ImageE7.Coef[1] = tr.ImageE1.Coef[7];
            result.ImageE7.Coef[2] = tr.ImageE2.Coef[7];
            result.ImageE7.Coef[3] = tr.ImageE3.Coef[7];
            result.ImageE7.Coef[4] = tr.ImageE4.Coef[7];
            result.ImageE7.Coef[5] = tr.ImageE5.Coef[7];
            result.ImageE7.Coef[6] = tr.ImageE6.Coef[7];
            result.ImageE7.Coef[7] = tr.ImageE7.Coef[7];
            result.ImageE6.Coef[0] = tr.ImageE0.Coef[6];
            result.ImageE6.Coef[1] = tr.ImageE1.Coef[6];
            result.ImageE6.Coef[2] = tr.ImageE2.Coef[6];
            result.ImageE6.Coef[3] = tr.ImageE3.Coef[6];
            result.ImageE6.Coef[4] = tr.ImageE4.Coef[6];
            result.ImageE6.Coef[5] = tr.ImageE5.Coef[6];
            result.ImageE6.Coef[6] = tr.ImageE6.Coef[6];
            result.ImageE6.Coef[7] = tr.ImageE7.Coef[6];
            result.ImageE5.Coef[0] = tr.ImageE0.Coef[5];
            result.ImageE5.Coef[1] = tr.ImageE1.Coef[5];
            result.ImageE5.Coef[2] = tr.ImageE2.Coef[5];
            result.ImageE5.Coef[3] = tr.ImageE3.Coef[5];
            result.ImageE5.Coef[4] = tr.ImageE4.Coef[5];
            result.ImageE5.Coef[5] = tr.ImageE5.Coef[5];
            result.ImageE5.Coef[6] = tr.ImageE6.Coef[5];
            result.ImageE5.Coef[7] = tr.ImageE7.Coef[5];
            result.ImageE4.Coef[0] = tr.ImageE0.Coef[4];
            result.ImageE4.Coef[1] = tr.ImageE1.Coef[4];
            result.ImageE4.Coef[2] = tr.ImageE2.Coef[4];
            result.ImageE4.Coef[3] = tr.ImageE3.Coef[4];
            result.ImageE4.Coef[4] = tr.ImageE4.Coef[4];
            result.ImageE4.Coef[5] = tr.ImageE5.Coef[4];
            result.ImageE4.Coef[6] = tr.ImageE6.Coef[4];
            result.ImageE4.Coef[7] = tr.ImageE7.Coef[4];
            result.ImageE3.Coef[0] = tr.ImageE0.Coef[3];
            result.ImageE3.Coef[1] = tr.ImageE1.Coef[3];
            result.ImageE3.Coef[2] = tr.ImageE2.Coef[3];
            result.ImageE3.Coef[3] = tr.ImageE3.Coef[3];
            result.ImageE3.Coef[4] = tr.ImageE4.Coef[3];
            result.ImageE3.Coef[5] = tr.ImageE5.Coef[3];
            result.ImageE3.Coef[6] = tr.ImageE6.Coef[3];
            result.ImageE3.Coef[7] = tr.ImageE7.Coef[3];
            result.ImageE2.Coef[0] = tr.ImageE0.Coef[2];
            result.ImageE2.Coef[1] = tr.ImageE1.Coef[2];
            result.ImageE2.Coef[2] = tr.ImageE2.Coef[2];
            result.ImageE2.Coef[3] = tr.ImageE3.Coef[2];
            result.ImageE2.Coef[4] = tr.ImageE4.Coef[2];
            result.ImageE2.Coef[5] = tr.ImageE5.Coef[2];
            result.ImageE2.Coef[6] = tr.ImageE6.Coef[2];
            result.ImageE2.Coef[7] = tr.ImageE7.Coef[2];
            result.ImageE1.Coef[0] = tr.ImageE0.Coef[1];
            result.ImageE1.Coef[1] = tr.ImageE1.Coef[1];
            result.ImageE1.Coef[2] = tr.ImageE2.Coef[1];
            result.ImageE1.Coef[3] = tr.ImageE3.Coef[1];
            result.ImageE1.Coef[4] = tr.ImageE4.Coef[1];
            result.ImageE1.Coef[5] = tr.ImageE5.Coef[1];
            result.ImageE1.Coef[6] = tr.ImageE6.Coef[1];
            result.ImageE1.Coef[7] = tr.ImageE7.Coef[1];
            result.ImageE0.Coef[0] = tr.ImageE0.Coef[0];
            result.ImageE0.Coef[1] = tr.ImageE1.Coef[0];
            result.ImageE0.Coef[2] = tr.ImageE2.Coef[0];
            result.ImageE0.Coef[3] = tr.ImageE3.Coef[0];
            result.ImageE0.Coef[4] = tr.ImageE4.Coef[0];
            result.ImageE0.Coef[5] = tr.ImageE5.Coef[0];
            result.ImageE0.Coef[6] = tr.ImageE6.Coef[0];
            result.ImageE0.Coef[7] = tr.ImageE7.Coef[0];
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct ComposeLT(geometry3d.e3d.LTStruct tr1, geometry3d.e3d.LTStruct tr2)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:06 م
            //Macro: geometry3d.e3d.ComposeLT
            //Input Variables: 128 used, 0 not used, 128 total.
            //Temp Variables: 896 sub-expressions, 0 generated temps, 896 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 64 total.
            //Computations: 1 average, 960 total.
            //Memory Reads: 2 average, 1920 total.
            //Memory Writes: 960 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    tr1.ImageE7.#E0# = <variable>
            //    tr1.ImageE7.#e1# = <variable>
            //    tr1.ImageE7.#e2# = <variable>
            //    tr1.ImageE7.#e1^e2# = <variable>
            //    tr1.ImageE7.#e3# = <variable>
            //    tr1.ImageE7.#e1^e3# = <variable>
            //    tr1.ImageE7.#e2^e3# = <variable>
            //    tr1.ImageE7.#e1^e2^e3# = <variable>
            //    tr1.ImageE6.#E0# = <variable>
            //    tr1.ImageE6.#e1# = <variable>
            //    tr1.ImageE6.#e2# = <variable>
            //    tr1.ImageE6.#e1^e2# = <variable>
            //    tr1.ImageE6.#e3# = <variable>
            //    tr1.ImageE6.#e1^e3# = <variable>
            //    tr1.ImageE6.#e2^e3# = <variable>
            //    tr1.ImageE6.#e1^e2^e3# = <variable>
            //    tr1.ImageE5.#E0# = <variable>
            //    tr1.ImageE5.#e1# = <variable>
            //    tr1.ImageE5.#e2# = <variable>
            //    tr1.ImageE5.#e1^e2# = <variable>
            //    tr1.ImageE5.#e3# = <variable>
            //    tr1.ImageE5.#e1^e3# = <variable>
            //    tr1.ImageE5.#e2^e3# = <variable>
            //    tr1.ImageE5.#e1^e2^e3# = <variable>
            //    tr1.ImageE4.#E0# = <variable>
            //    tr1.ImageE4.#e1# = <variable>
            //    tr1.ImageE4.#e2# = <variable>
            //    tr1.ImageE4.#e1^e2# = <variable>
            //    tr1.ImageE4.#e3# = <variable>
            //    tr1.ImageE4.#e1^e3# = <variable>
            //    tr1.ImageE4.#e2^e3# = <variable>
            //    tr1.ImageE4.#e1^e2^e3# = <variable>
            //    tr1.ImageE3.#E0# = <variable>
            //    tr1.ImageE3.#e1# = <variable>
            //    tr1.ImageE3.#e2# = <variable>
            //    tr1.ImageE3.#e1^e2# = <variable>
            //    tr1.ImageE3.#e3# = <variable>
            //    tr1.ImageE3.#e1^e3# = <variable>
            //    tr1.ImageE3.#e2^e3# = <variable>
            //    tr1.ImageE3.#e1^e2^e3# = <variable>
            //    tr1.ImageE2.#E0# = <variable>
            //    tr1.ImageE2.#e1# = <variable>
            //    tr1.ImageE2.#e2# = <variable>
            //    tr1.ImageE2.#e1^e2# = <variable>
            //    tr1.ImageE2.#e3# = <variable>
            //    tr1.ImageE2.#e1^e3# = <variable>
            //    tr1.ImageE2.#e2^e3# = <variable>
            //    tr1.ImageE2.#e1^e2^e3# = <variable>
            //    tr1.ImageE1.#E0# = <variable>
            //    tr1.ImageE1.#e1# = <variable>
            //    tr1.ImageE1.#e2# = <variable>
            //    tr1.ImageE1.#e1^e2# = <variable>
            //    tr1.ImageE1.#e3# = <variable>
            //    tr1.ImageE1.#e1^e3# = <variable>
            //    tr1.ImageE1.#e2^e3# = <variable>
            //    tr1.ImageE1.#e1^e2^e3# = <variable>
            //    tr1.ImageE0.#E0# = <variable>
            //    tr1.ImageE0.#e1# = <variable>
            //    tr1.ImageE0.#e2# = <variable>
            //    tr1.ImageE0.#e1^e2# = <variable>
            //    tr1.ImageE0.#e3# = <variable>
            //    tr1.ImageE0.#e1^e3# = <variable>
            //    tr1.ImageE0.#e2^e3# = <variable>
            //    tr1.ImageE0.#e1^e2^e3# = <variable>
            //    tr2.ImageE7.#E0# = <variable>
            //    tr2.ImageE7.#e1# = <variable>
            //    tr2.ImageE7.#e2# = <variable>
            //    tr2.ImageE7.#e1^e2# = <variable>
            //    tr2.ImageE7.#e3# = <variable>
            //    tr2.ImageE7.#e1^e3# = <variable>
            //    tr2.ImageE7.#e2^e3# = <variable>
            //    tr2.ImageE7.#e1^e2^e3# = <variable>
            //    tr2.ImageE6.#E0# = <variable>
            //    tr2.ImageE6.#e1# = <variable>
            //    tr2.ImageE6.#e2# = <variable>
            //    tr2.ImageE6.#e1^e2# = <variable>
            //    tr2.ImageE6.#e3# = <variable>
            //    tr2.ImageE6.#e1^e3# = <variable>
            //    tr2.ImageE6.#e2^e3# = <variable>
            //    tr2.ImageE6.#e1^e2^e3# = <variable>
            //    tr2.ImageE5.#E0# = <variable>
            //    tr2.ImageE5.#e1# = <variable>
            //    tr2.ImageE5.#e2# = <variable>
            //    tr2.ImageE5.#e1^e2# = <variable>
            //    tr2.ImageE5.#e3# = <variable>
            //    tr2.ImageE5.#e1^e3# = <variable>
            //    tr2.ImageE5.#e2^e3# = <variable>
            //    tr2.ImageE5.#e1^e2^e3# = <variable>
            //    tr2.ImageE4.#E0# = <variable>
            //    tr2.ImageE4.#e1# = <variable>
            //    tr2.ImageE4.#e2# = <variable>
            //    tr2.ImageE4.#e1^e2# = <variable>
            //    tr2.ImageE4.#e3# = <variable>
            //    tr2.ImageE4.#e1^e3# = <variable>
            //    tr2.ImageE4.#e2^e3# = <variable>
            //    tr2.ImageE4.#e1^e2^e3# = <variable>
            //    tr2.ImageE3.#E0# = <variable>
            //    tr2.ImageE3.#e1# = <variable>
            //    tr2.ImageE3.#e2# = <variable>
            //    tr2.ImageE3.#e1^e2# = <variable>
            //    tr2.ImageE3.#e3# = <variable>
            //    tr2.ImageE3.#e1^e3# = <variable>
            //    tr2.ImageE3.#e2^e3# = <variable>
            //    tr2.ImageE3.#e1^e2^e3# = <variable>
            //    tr2.ImageE2.#E0# = <variable>
            //    tr2.ImageE2.#e1# = <variable>
            //    tr2.ImageE2.#e2# = <variable>
            //    tr2.ImageE2.#e1^e2# = <variable>
            //    tr2.ImageE2.#e3# = <variable>
            //    tr2.ImageE2.#e1^e3# = <variable>
            //    tr2.ImageE2.#e2^e3# = <variable>
            //    tr2.ImageE2.#e1^e2^e3# = <variable>
            //    tr2.ImageE1.#E0# = <variable>
            //    tr2.ImageE1.#e1# = <variable>
            //    tr2.ImageE1.#e2# = <variable>
            //    tr2.ImageE1.#e1^e2# = <variable>
            //    tr2.ImageE1.#e3# = <variable>
            //    tr2.ImageE1.#e1^e3# = <variable>
            //    tr2.ImageE1.#e2^e3# = <variable>
            //    tr2.ImageE1.#e1^e2^e3# = <variable>
            //    tr2.ImageE0.#E0# = <variable>
            //    tr2.ImageE0.#e1# = <variable>
            //    tr2.ImageE0.#e2# = <variable>
            //    tr2.ImageE0.#e1^e2# = <variable>
            //    tr2.ImageE0.#e3# = <variable>
            //    tr2.ImageE0.#e1^e3# = <variable>
            //    tr2.ImageE0.#e2^e3# = <variable>
            //    tr2.ImageE0.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE7.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE7.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE7.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE7.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE7.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE7.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE7.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE6.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE6.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE6.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE6.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE6.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE6.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE6.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE5.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE5.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE5.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE5.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE5.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE5.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE5.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE4.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE4.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE4.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE4.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE4.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE4.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE4.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE3.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE3.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE3.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE3.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE2.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE2.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE2.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE1.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE1.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE1.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[7] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[0] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[0] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[0] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[0] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[1] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[1] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[1] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[1] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[2] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[2] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[2] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[2] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[3] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[3] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[3] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[3] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[4] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[4] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[4] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[4] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[4] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[4] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[4] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[4] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[5] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[5] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[5] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[5] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[5] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[5] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[5] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[5] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[6] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[6] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[6] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[6] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[6] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[6] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[6] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[6] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[7] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[7] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[7] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[7] * tr2.ImageE0.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE4.Coef[7] * tr2.ImageE0.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE5.Coef[7] * tr2.ImageE0.Coef[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE6.Coef[7] * tr2.ImageE0.Coef[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE7.Coef[7] * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct AddLT(geometry3d.e3d.LTStruct tr1, geometry3d.e3d.LTStruct tr2)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:06 م
            //Macro: geometry3d.e3d.AddLT
            //Input Variables: 128 used, 0 not used, 128 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 64 total.
            //Computations: 1 average, 64 total.
            //Memory Reads: 2 average, 128 total.
            //Memory Writes: 64 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    tr1.ImageE7.#E0# = <variable>
            //    tr1.ImageE7.#e1# = <variable>
            //    tr1.ImageE7.#e2# = <variable>
            //    tr1.ImageE7.#e1^e2# = <variable>
            //    tr1.ImageE7.#e3# = <variable>
            //    tr1.ImageE7.#e1^e3# = <variable>
            //    tr1.ImageE7.#e2^e3# = <variable>
            //    tr1.ImageE7.#e1^e2^e3# = <variable>
            //    tr1.ImageE6.#E0# = <variable>
            //    tr1.ImageE6.#e1# = <variable>
            //    tr1.ImageE6.#e2# = <variable>
            //    tr1.ImageE6.#e1^e2# = <variable>
            //    tr1.ImageE6.#e3# = <variable>
            //    tr1.ImageE6.#e1^e3# = <variable>
            //    tr1.ImageE6.#e2^e3# = <variable>
            //    tr1.ImageE6.#e1^e2^e3# = <variable>
            //    tr1.ImageE5.#E0# = <variable>
            //    tr1.ImageE5.#e1# = <variable>
            //    tr1.ImageE5.#e2# = <variable>
            //    tr1.ImageE5.#e1^e2# = <variable>
            //    tr1.ImageE5.#e3# = <variable>
            //    tr1.ImageE5.#e1^e3# = <variable>
            //    tr1.ImageE5.#e2^e3# = <variable>
            //    tr1.ImageE5.#e1^e2^e3# = <variable>
            //    tr1.ImageE4.#E0# = <variable>
            //    tr1.ImageE4.#e1# = <variable>
            //    tr1.ImageE4.#e2# = <variable>
            //    tr1.ImageE4.#e1^e2# = <variable>
            //    tr1.ImageE4.#e3# = <variable>
            //    tr1.ImageE4.#e1^e3# = <variable>
            //    tr1.ImageE4.#e2^e3# = <variable>
            //    tr1.ImageE4.#e1^e2^e3# = <variable>
            //    tr1.ImageE3.#E0# = <variable>
            //    tr1.ImageE3.#e1# = <variable>
            //    tr1.ImageE3.#e2# = <variable>
            //    tr1.ImageE3.#e1^e2# = <variable>
            //    tr1.ImageE3.#e3# = <variable>
            //    tr1.ImageE3.#e1^e3# = <variable>
            //    tr1.ImageE3.#e2^e3# = <variable>
            //    tr1.ImageE3.#e1^e2^e3# = <variable>
            //    tr1.ImageE2.#E0# = <variable>
            //    tr1.ImageE2.#e1# = <variable>
            //    tr1.ImageE2.#e2# = <variable>
            //    tr1.ImageE2.#e1^e2# = <variable>
            //    tr1.ImageE2.#e3# = <variable>
            //    tr1.ImageE2.#e1^e3# = <variable>
            //    tr1.ImageE2.#e2^e3# = <variable>
            //    tr1.ImageE2.#e1^e2^e3# = <variable>
            //    tr1.ImageE1.#E0# = <variable>
            //    tr1.ImageE1.#e1# = <variable>
            //    tr1.ImageE1.#e2# = <variable>
            //    tr1.ImageE1.#e1^e2# = <variable>
            //    tr1.ImageE1.#e3# = <variable>
            //    tr1.ImageE1.#e1^e3# = <variable>
            //    tr1.ImageE1.#e2^e3# = <variable>
            //    tr1.ImageE1.#e1^e2^e3# = <variable>
            //    tr1.ImageE0.#E0# = <variable>
            //    tr1.ImageE0.#e1# = <variable>
            //    tr1.ImageE0.#e2# = <variable>
            //    tr1.ImageE0.#e1^e2# = <variable>
            //    tr1.ImageE0.#e3# = <variable>
            //    tr1.ImageE0.#e1^e3# = <variable>
            //    tr1.ImageE0.#e2^e3# = <variable>
            //    tr1.ImageE0.#e1^e2^e3# = <variable>
            //    tr2.ImageE7.#E0# = <variable>
            //    tr2.ImageE7.#e1# = <variable>
            //    tr2.ImageE7.#e2# = <variable>
            //    tr2.ImageE7.#e1^e2# = <variable>
            //    tr2.ImageE7.#e3# = <variable>
            //    tr2.ImageE7.#e1^e3# = <variable>
            //    tr2.ImageE7.#e2^e3# = <variable>
            //    tr2.ImageE7.#e1^e2^e3# = <variable>
            //    tr2.ImageE6.#E0# = <variable>
            //    tr2.ImageE6.#e1# = <variable>
            //    tr2.ImageE6.#e2# = <variable>
            //    tr2.ImageE6.#e1^e2# = <variable>
            //    tr2.ImageE6.#e3# = <variable>
            //    tr2.ImageE6.#e1^e3# = <variable>
            //    tr2.ImageE6.#e2^e3# = <variable>
            //    tr2.ImageE6.#e1^e2^e3# = <variable>
            //    tr2.ImageE5.#E0# = <variable>
            //    tr2.ImageE5.#e1# = <variable>
            //    tr2.ImageE5.#e2# = <variable>
            //    tr2.ImageE5.#e1^e2# = <variable>
            //    tr2.ImageE5.#e3# = <variable>
            //    tr2.ImageE5.#e1^e3# = <variable>
            //    tr2.ImageE5.#e2^e3# = <variable>
            //    tr2.ImageE5.#e1^e2^e3# = <variable>
            //    tr2.ImageE4.#E0# = <variable>
            //    tr2.ImageE4.#e1# = <variable>
            //    tr2.ImageE4.#e2# = <variable>
            //    tr2.ImageE4.#e1^e2# = <variable>
            //    tr2.ImageE4.#e3# = <variable>
            //    tr2.ImageE4.#e1^e3# = <variable>
            //    tr2.ImageE4.#e2^e3# = <variable>
            //    tr2.ImageE4.#e1^e2^e3# = <variable>
            //    tr2.ImageE3.#E0# = <variable>
            //    tr2.ImageE3.#e1# = <variable>
            //    tr2.ImageE3.#e2# = <variable>
            //    tr2.ImageE3.#e1^e2# = <variable>
            //    tr2.ImageE3.#e3# = <variable>
            //    tr2.ImageE3.#e1^e3# = <variable>
            //    tr2.ImageE3.#e2^e3# = <variable>
            //    tr2.ImageE3.#e1^e2^e3# = <variable>
            //    tr2.ImageE2.#E0# = <variable>
            //    tr2.ImageE2.#e1# = <variable>
            //    tr2.ImageE2.#e2# = <variable>
            //    tr2.ImageE2.#e1^e2# = <variable>
            //    tr2.ImageE2.#e3# = <variable>
            //    tr2.ImageE2.#e1^e3# = <variable>
            //    tr2.ImageE2.#e2^e3# = <variable>
            //    tr2.ImageE2.#e1^e2^e3# = <variable>
            //    tr2.ImageE1.#E0# = <variable>
            //    tr2.ImageE1.#e1# = <variable>
            //    tr2.ImageE1.#e2# = <variable>
            //    tr2.ImageE1.#e1^e2# = <variable>
            //    tr2.ImageE1.#e3# = <variable>
            //    tr2.ImageE1.#e1^e3# = <variable>
            //    tr2.ImageE1.#e2^e3# = <variable>
            //    tr2.ImageE1.#e1^e2^e3# = <variable>
            //    tr2.ImageE0.#E0# = <variable>
            //    tr2.ImageE0.#e1# = <variable>
            //    tr2.ImageE0.#e2# = <variable>
            //    tr2.ImageE0.#e1^e2# = <variable>
            //    tr2.ImageE0.#e3# = <variable>
            //    tr2.ImageE0.#e1^e3# = <variable>
            //    tr2.ImageE0.#e2^e3# = <variable>
            //    tr2.ImageE0.#e1^e2^e3# = <variable>
            
            
            result.ImageE7.Coef[0] = (tr1.ImageE7.Coef[0] + tr2.ImageE7.Coef[0]);
            result.ImageE7.Coef[1] = (tr1.ImageE7.Coef[1] + tr2.ImageE7.Coef[1]);
            result.ImageE7.Coef[2] = (tr1.ImageE7.Coef[2] + tr2.ImageE7.Coef[2]);
            result.ImageE7.Coef[3] = (tr1.ImageE7.Coef[3] + tr2.ImageE7.Coef[3]);
            result.ImageE7.Coef[4] = (tr1.ImageE7.Coef[4] + tr2.ImageE7.Coef[4]);
            result.ImageE7.Coef[5] = (tr1.ImageE7.Coef[5] + tr2.ImageE7.Coef[5]);
            result.ImageE7.Coef[6] = (tr1.ImageE7.Coef[6] + tr2.ImageE7.Coef[6]);
            result.ImageE7.Coef[7] = (tr1.ImageE7.Coef[7] + tr2.ImageE7.Coef[7]);
            result.ImageE6.Coef[0] = (tr1.ImageE6.Coef[0] + tr2.ImageE6.Coef[0]);
            result.ImageE6.Coef[1] = (tr1.ImageE6.Coef[1] + tr2.ImageE6.Coef[1]);
            result.ImageE6.Coef[2] = (tr1.ImageE6.Coef[2] + tr2.ImageE6.Coef[2]);
            result.ImageE6.Coef[3] = (tr1.ImageE6.Coef[3] + tr2.ImageE6.Coef[3]);
            result.ImageE6.Coef[4] = (tr1.ImageE6.Coef[4] + tr2.ImageE6.Coef[4]);
            result.ImageE6.Coef[5] = (tr1.ImageE6.Coef[5] + tr2.ImageE6.Coef[5]);
            result.ImageE6.Coef[6] = (tr1.ImageE6.Coef[6] + tr2.ImageE6.Coef[6]);
            result.ImageE6.Coef[7] = (tr1.ImageE6.Coef[7] + tr2.ImageE6.Coef[7]);
            result.ImageE5.Coef[0] = (tr1.ImageE5.Coef[0] + tr2.ImageE5.Coef[0]);
            result.ImageE5.Coef[1] = (tr1.ImageE5.Coef[1] + tr2.ImageE5.Coef[1]);
            result.ImageE5.Coef[2] = (tr1.ImageE5.Coef[2] + tr2.ImageE5.Coef[2]);
            result.ImageE5.Coef[3] = (tr1.ImageE5.Coef[3] + tr2.ImageE5.Coef[3]);
            result.ImageE5.Coef[4] = (tr1.ImageE5.Coef[4] + tr2.ImageE5.Coef[4]);
            result.ImageE5.Coef[5] = (tr1.ImageE5.Coef[5] + tr2.ImageE5.Coef[5]);
            result.ImageE5.Coef[6] = (tr1.ImageE5.Coef[6] + tr2.ImageE5.Coef[6]);
            result.ImageE5.Coef[7] = (tr1.ImageE5.Coef[7] + tr2.ImageE5.Coef[7]);
            result.ImageE4.Coef[0] = (tr1.ImageE4.Coef[0] + tr2.ImageE4.Coef[0]);
            result.ImageE4.Coef[1] = (tr1.ImageE4.Coef[1] + tr2.ImageE4.Coef[1]);
            result.ImageE4.Coef[2] = (tr1.ImageE4.Coef[2] + tr2.ImageE4.Coef[2]);
            result.ImageE4.Coef[3] = (tr1.ImageE4.Coef[3] + tr2.ImageE4.Coef[3]);
            result.ImageE4.Coef[4] = (tr1.ImageE4.Coef[4] + tr2.ImageE4.Coef[4]);
            result.ImageE4.Coef[5] = (tr1.ImageE4.Coef[5] + tr2.ImageE4.Coef[5]);
            result.ImageE4.Coef[6] = (tr1.ImageE4.Coef[6] + tr2.ImageE4.Coef[6]);
            result.ImageE4.Coef[7] = (tr1.ImageE4.Coef[7] + tr2.ImageE4.Coef[7]);
            result.ImageE3.Coef[0] = (tr1.ImageE3.Coef[0] + tr2.ImageE3.Coef[0]);
            result.ImageE3.Coef[1] = (tr1.ImageE3.Coef[1] + tr2.ImageE3.Coef[1]);
            result.ImageE3.Coef[2] = (tr1.ImageE3.Coef[2] + tr2.ImageE3.Coef[2]);
            result.ImageE3.Coef[3] = (tr1.ImageE3.Coef[3] + tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[4] = (tr1.ImageE3.Coef[4] + tr2.ImageE3.Coef[4]);
            result.ImageE3.Coef[5] = (tr1.ImageE3.Coef[5] + tr2.ImageE3.Coef[5]);
            result.ImageE3.Coef[6] = (tr1.ImageE3.Coef[6] + tr2.ImageE3.Coef[6]);
            result.ImageE3.Coef[7] = (tr1.ImageE3.Coef[7] + tr2.ImageE3.Coef[7]);
            result.ImageE2.Coef[0] = (tr1.ImageE2.Coef[0] + tr2.ImageE2.Coef[0]);
            result.ImageE2.Coef[1] = (tr1.ImageE2.Coef[1] + tr2.ImageE2.Coef[1]);
            result.ImageE2.Coef[2] = (tr1.ImageE2.Coef[2] + tr2.ImageE2.Coef[2]);
            result.ImageE2.Coef[3] = (tr1.ImageE2.Coef[3] + tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[4] = (tr1.ImageE2.Coef[4] + tr2.ImageE2.Coef[4]);
            result.ImageE2.Coef[5] = (tr1.ImageE2.Coef[5] + tr2.ImageE2.Coef[5]);
            result.ImageE2.Coef[6] = (tr1.ImageE2.Coef[6] + tr2.ImageE2.Coef[6]);
            result.ImageE2.Coef[7] = (tr1.ImageE2.Coef[7] + tr2.ImageE2.Coef[7]);
            result.ImageE1.Coef[0] = (tr1.ImageE1.Coef[0] + tr2.ImageE1.Coef[0]);
            result.ImageE1.Coef[1] = (tr1.ImageE1.Coef[1] + tr2.ImageE1.Coef[1]);
            result.ImageE1.Coef[2] = (tr1.ImageE1.Coef[2] + tr2.ImageE1.Coef[2]);
            result.ImageE1.Coef[3] = (tr1.ImageE1.Coef[3] + tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[4] = (tr1.ImageE1.Coef[4] + tr2.ImageE1.Coef[4]);
            result.ImageE1.Coef[5] = (tr1.ImageE1.Coef[5] + tr2.ImageE1.Coef[5]);
            result.ImageE1.Coef[6] = (tr1.ImageE1.Coef[6] + tr2.ImageE1.Coef[6]);
            result.ImageE1.Coef[7] = (tr1.ImageE1.Coef[7] + tr2.ImageE1.Coef[7]);
            result.ImageE0.Coef[0] = (tr1.ImageE0.Coef[0] + tr2.ImageE0.Coef[0]);
            result.ImageE0.Coef[1] = (tr1.ImageE0.Coef[1] + tr2.ImageE0.Coef[1]);
            result.ImageE0.Coef[2] = (tr1.ImageE0.Coef[2] + tr2.ImageE0.Coef[2]);
            result.ImageE0.Coef[3] = (tr1.ImageE0.Coef[3] + tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[4] = (tr1.ImageE0.Coef[4] + tr2.ImageE0.Coef[4]);
            result.ImageE0.Coef[5] = (tr1.ImageE0.Coef[5] + tr2.ImageE0.Coef[5]);
            result.ImageE0.Coef[6] = (tr1.ImageE0.Coef[6] + tr2.ImageE0.Coef[6]);
            result.ImageE0.Coef[7] = (tr1.ImageE0.Coef[7] + tr2.ImageE0.Coef[7]);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct SubtractLT(geometry3d.e3d.LTStruct tr1, geometry3d.e3d.LTStruct tr2)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:06 م
            //Macro: geometry3d.e3d.SubtractLT
            //Input Variables: 128 used, 0 not used, 128 total.
            //Temp Variables: 64 sub-expressions, 0 generated temps, 64 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 64 total.
            //Computations: 1 average, 128 total.
            //Memory Reads: 1.5 average, 192 total.
            //Memory Writes: 128 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    tr1.ImageE7.#E0# = <variable>
            //    tr1.ImageE7.#e1# = <variable>
            //    tr1.ImageE7.#e2# = <variable>
            //    tr1.ImageE7.#e1^e2# = <variable>
            //    tr1.ImageE7.#e3# = <variable>
            //    tr1.ImageE7.#e1^e3# = <variable>
            //    tr1.ImageE7.#e2^e3# = <variable>
            //    tr1.ImageE7.#e1^e2^e3# = <variable>
            //    tr1.ImageE6.#E0# = <variable>
            //    tr1.ImageE6.#e1# = <variable>
            //    tr1.ImageE6.#e2# = <variable>
            //    tr1.ImageE6.#e1^e2# = <variable>
            //    tr1.ImageE6.#e3# = <variable>
            //    tr1.ImageE6.#e1^e3# = <variable>
            //    tr1.ImageE6.#e2^e3# = <variable>
            //    tr1.ImageE6.#e1^e2^e3# = <variable>
            //    tr1.ImageE5.#E0# = <variable>
            //    tr1.ImageE5.#e1# = <variable>
            //    tr1.ImageE5.#e2# = <variable>
            //    tr1.ImageE5.#e1^e2# = <variable>
            //    tr1.ImageE5.#e3# = <variable>
            //    tr1.ImageE5.#e1^e3# = <variable>
            //    tr1.ImageE5.#e2^e3# = <variable>
            //    tr1.ImageE5.#e1^e2^e3# = <variable>
            //    tr1.ImageE4.#E0# = <variable>
            //    tr1.ImageE4.#e1# = <variable>
            //    tr1.ImageE4.#e2# = <variable>
            //    tr1.ImageE4.#e1^e2# = <variable>
            //    tr1.ImageE4.#e3# = <variable>
            //    tr1.ImageE4.#e1^e3# = <variable>
            //    tr1.ImageE4.#e2^e3# = <variable>
            //    tr1.ImageE4.#e1^e2^e3# = <variable>
            //    tr1.ImageE3.#E0# = <variable>
            //    tr1.ImageE3.#e1# = <variable>
            //    tr1.ImageE3.#e2# = <variable>
            //    tr1.ImageE3.#e1^e2# = <variable>
            //    tr1.ImageE3.#e3# = <variable>
            //    tr1.ImageE3.#e1^e3# = <variable>
            //    tr1.ImageE3.#e2^e3# = <variable>
            //    tr1.ImageE3.#e1^e2^e3# = <variable>
            //    tr1.ImageE2.#E0# = <variable>
            //    tr1.ImageE2.#e1# = <variable>
            //    tr1.ImageE2.#e2# = <variable>
            //    tr1.ImageE2.#e1^e2# = <variable>
            //    tr1.ImageE2.#e3# = <variable>
            //    tr1.ImageE2.#e1^e3# = <variable>
            //    tr1.ImageE2.#e2^e3# = <variable>
            //    tr1.ImageE2.#e1^e2^e3# = <variable>
            //    tr1.ImageE1.#E0# = <variable>
            //    tr1.ImageE1.#e1# = <variable>
            //    tr1.ImageE1.#e2# = <variable>
            //    tr1.ImageE1.#e1^e2# = <variable>
            //    tr1.ImageE1.#e3# = <variable>
            //    tr1.ImageE1.#e1^e3# = <variable>
            //    tr1.ImageE1.#e2^e3# = <variable>
            //    tr1.ImageE1.#e1^e2^e3# = <variable>
            //    tr1.ImageE0.#E0# = <variable>
            //    tr1.ImageE0.#e1# = <variable>
            //    tr1.ImageE0.#e2# = <variable>
            //    tr1.ImageE0.#e1^e2# = <variable>
            //    tr1.ImageE0.#e3# = <variable>
            //    tr1.ImageE0.#e1^e3# = <variable>
            //    tr1.ImageE0.#e2^e3# = <variable>
            //    tr1.ImageE0.#e1^e2^e3# = <variable>
            //    tr2.ImageE7.#E0# = <variable>
            //    tr2.ImageE7.#e1# = <variable>
            //    tr2.ImageE7.#e2# = <variable>
            //    tr2.ImageE7.#e1^e2# = <variable>
            //    tr2.ImageE7.#e3# = <variable>
            //    tr2.ImageE7.#e1^e3# = <variable>
            //    tr2.ImageE7.#e2^e3# = <variable>
            //    tr2.ImageE7.#e1^e2^e3# = <variable>
            //    tr2.ImageE6.#E0# = <variable>
            //    tr2.ImageE6.#e1# = <variable>
            //    tr2.ImageE6.#e2# = <variable>
            //    tr2.ImageE6.#e1^e2# = <variable>
            //    tr2.ImageE6.#e3# = <variable>
            //    tr2.ImageE6.#e1^e3# = <variable>
            //    tr2.ImageE6.#e2^e3# = <variable>
            //    tr2.ImageE6.#e1^e2^e3# = <variable>
            //    tr2.ImageE5.#E0# = <variable>
            //    tr2.ImageE5.#e1# = <variable>
            //    tr2.ImageE5.#e2# = <variable>
            //    tr2.ImageE5.#e1^e2# = <variable>
            //    tr2.ImageE5.#e3# = <variable>
            //    tr2.ImageE5.#e1^e3# = <variable>
            //    tr2.ImageE5.#e2^e3# = <variable>
            //    tr2.ImageE5.#e1^e2^e3# = <variable>
            //    tr2.ImageE4.#E0# = <variable>
            //    tr2.ImageE4.#e1# = <variable>
            //    tr2.ImageE4.#e2# = <variable>
            //    tr2.ImageE4.#e1^e2# = <variable>
            //    tr2.ImageE4.#e3# = <variable>
            //    tr2.ImageE4.#e1^e3# = <variable>
            //    tr2.ImageE4.#e2^e3# = <variable>
            //    tr2.ImageE4.#e1^e2^e3# = <variable>
            //    tr2.ImageE3.#E0# = <variable>
            //    tr2.ImageE3.#e1# = <variable>
            //    tr2.ImageE3.#e2# = <variable>
            //    tr2.ImageE3.#e1^e2# = <variable>
            //    tr2.ImageE3.#e3# = <variable>
            //    tr2.ImageE3.#e1^e3# = <variable>
            //    tr2.ImageE3.#e2^e3# = <variable>
            //    tr2.ImageE3.#e1^e2^e3# = <variable>
            //    tr2.ImageE2.#E0# = <variable>
            //    tr2.ImageE2.#e1# = <variable>
            //    tr2.ImageE2.#e2# = <variable>
            //    tr2.ImageE2.#e1^e2# = <variable>
            //    tr2.ImageE2.#e3# = <variable>
            //    tr2.ImageE2.#e1^e3# = <variable>
            //    tr2.ImageE2.#e2^e3# = <variable>
            //    tr2.ImageE2.#e1^e2^e3# = <variable>
            //    tr2.ImageE1.#E0# = <variable>
            //    tr2.ImageE1.#e1# = <variable>
            //    tr2.ImageE1.#e2# = <variable>
            //    tr2.ImageE1.#e1^e2# = <variable>
            //    tr2.ImageE1.#e3# = <variable>
            //    tr2.ImageE1.#e1^e3# = <variable>
            //    tr2.ImageE1.#e2^e3# = <variable>
            //    tr2.ImageE1.#e1^e2^e3# = <variable>
            //    tr2.ImageE0.#E0# = <variable>
            //    tr2.ImageE0.#e1# = <variable>
            //    tr2.ImageE0.#e2# = <variable>
            //    tr2.ImageE0.#e1^e2# = <variable>
            //    tr2.ImageE0.#e3# = <variable>
            //    tr2.ImageE0.#e1^e3# = <variable>
            //    tr2.ImageE0.#e2^e3# = <variable>
            //    tr2.ImageE0.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            
            tempVar0000 = (-1 * tr2.ImageE7.Coef[0]);
            result.ImageE7.Coef[0] = (tr1.ImageE7.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[1]);
            result.ImageE7.Coef[1] = (tr1.ImageE7.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[2]);
            result.ImageE7.Coef[2] = (tr1.ImageE7.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[3]);
            result.ImageE7.Coef[3] = (tr1.ImageE7.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[4]);
            result.ImageE7.Coef[4] = (tr1.ImageE7.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[5]);
            result.ImageE7.Coef[5] = (tr1.ImageE7.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[6]);
            result.ImageE7.Coef[6] = (tr1.ImageE7.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE7.Coef[7]);
            result.ImageE7.Coef[7] = (tr1.ImageE7.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[0]);
            result.ImageE6.Coef[0] = (tr1.ImageE6.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[1]);
            result.ImageE6.Coef[1] = (tr1.ImageE6.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[2]);
            result.ImageE6.Coef[2] = (tr1.ImageE6.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[3]);
            result.ImageE6.Coef[3] = (tr1.ImageE6.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[4]);
            result.ImageE6.Coef[4] = (tr1.ImageE6.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[5]);
            result.ImageE6.Coef[5] = (tr1.ImageE6.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[6]);
            result.ImageE6.Coef[6] = (tr1.ImageE6.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE6.Coef[7]);
            result.ImageE6.Coef[7] = (tr1.ImageE6.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[0]);
            result.ImageE5.Coef[0] = (tr1.ImageE5.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[1]);
            result.ImageE5.Coef[1] = (tr1.ImageE5.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[2]);
            result.ImageE5.Coef[2] = (tr1.ImageE5.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[3]);
            result.ImageE5.Coef[3] = (tr1.ImageE5.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[4]);
            result.ImageE5.Coef[4] = (tr1.ImageE5.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[5]);
            result.ImageE5.Coef[5] = (tr1.ImageE5.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[6]);
            result.ImageE5.Coef[6] = (tr1.ImageE5.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE5.Coef[7]);
            result.ImageE5.Coef[7] = (tr1.ImageE5.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[0]);
            result.ImageE4.Coef[0] = (tr1.ImageE4.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[1]);
            result.ImageE4.Coef[1] = (tr1.ImageE4.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[2]);
            result.ImageE4.Coef[2] = (tr1.ImageE4.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[3]);
            result.ImageE4.Coef[3] = (tr1.ImageE4.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[4]);
            result.ImageE4.Coef[4] = (tr1.ImageE4.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[5]);
            result.ImageE4.Coef[5] = (tr1.ImageE4.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[6]);
            result.ImageE4.Coef[6] = (tr1.ImageE4.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE4.Coef[7]);
            result.ImageE4.Coef[7] = (tr1.ImageE4.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[0]);
            result.ImageE3.Coef[0] = (tr1.ImageE3.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[1]);
            result.ImageE3.Coef[1] = (tr1.ImageE3.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[2]);
            result.ImageE3.Coef[2] = (tr1.ImageE3.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[3] = (tr1.ImageE3.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[4]);
            result.ImageE3.Coef[4] = (tr1.ImageE3.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[5]);
            result.ImageE3.Coef[5] = (tr1.ImageE3.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[6]);
            result.ImageE3.Coef[6] = (tr1.ImageE3.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[7]);
            result.ImageE3.Coef[7] = (tr1.ImageE3.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[0]);
            result.ImageE2.Coef[0] = (tr1.ImageE2.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[1]);
            result.ImageE2.Coef[1] = (tr1.ImageE2.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[2]);
            result.ImageE2.Coef[2] = (tr1.ImageE2.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[3] = (tr1.ImageE2.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[4]);
            result.ImageE2.Coef[4] = (tr1.ImageE2.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[5]);
            result.ImageE2.Coef[5] = (tr1.ImageE2.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[6]);
            result.ImageE2.Coef[6] = (tr1.ImageE2.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[7]);
            result.ImageE2.Coef[7] = (tr1.ImageE2.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[0]);
            result.ImageE1.Coef[0] = (tr1.ImageE1.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[1]);
            result.ImageE1.Coef[1] = (tr1.ImageE1.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[2]);
            result.ImageE1.Coef[2] = (tr1.ImageE1.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[3] = (tr1.ImageE1.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[4]);
            result.ImageE1.Coef[4] = (tr1.ImageE1.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[5]);
            result.ImageE1.Coef[5] = (tr1.ImageE1.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[6]);
            result.ImageE1.Coef[6] = (tr1.ImageE1.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[7]);
            result.ImageE1.Coef[7] = (tr1.ImageE1.Coef[7] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[0]);
            result.ImageE0.Coef[0] = (tr1.ImageE0.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[1]);
            result.ImageE0.Coef[1] = (tr1.ImageE0.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[2]);
            result.ImageE0.Coef[2] = (tr1.ImageE0.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[3] = (tr1.ImageE0.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[4]);
            result.ImageE0.Coef[4] = (tr1.ImageE0.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[5]);
            result.ImageE0.Coef[5] = (tr1.ImageE0.Coef[5] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[6]);
            result.ImageE0.Coef[6] = (tr1.ImageE0.Coef[6] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[7]);
            result.ImageE0.Coef[7] = (tr1.ImageE0.Coef[7] + tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct TimesLT(geometry3d.e3d.LTStruct tr, double s)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:06 م
            //Macro: geometry3d.e3d.TimesLT
            //Input Variables: 65 used, 0 not used, 65 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 64 total.
            //Computations: 1 average, 64 total.
            //Memory Reads: 2 average, 128 total.
            //Memory Writes: 64 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    tr.ImageE7.#E0# = <variable>
            //    tr.ImageE7.#e1# = <variable>
            //    tr.ImageE7.#e2# = <variable>
            //    tr.ImageE7.#e1^e2# = <variable>
            //    tr.ImageE7.#e3# = <variable>
            //    tr.ImageE7.#e1^e3# = <variable>
            //    tr.ImageE7.#e2^e3# = <variable>
            //    tr.ImageE7.#e1^e2^e3# = <variable>
            //    tr.ImageE6.#E0# = <variable>
            //    tr.ImageE6.#e1# = <variable>
            //    tr.ImageE6.#e2# = <variable>
            //    tr.ImageE6.#e1^e2# = <variable>
            //    tr.ImageE6.#e3# = <variable>
            //    tr.ImageE6.#e1^e3# = <variable>
            //    tr.ImageE6.#e2^e3# = <variable>
            //    tr.ImageE6.#e1^e2^e3# = <variable>
            //    tr.ImageE5.#E0# = <variable>
            //    tr.ImageE5.#e1# = <variable>
            //    tr.ImageE5.#e2# = <variable>
            //    tr.ImageE5.#e1^e2# = <variable>
            //    tr.ImageE5.#e3# = <variable>
            //    tr.ImageE5.#e1^e3# = <variable>
            //    tr.ImageE5.#e2^e3# = <variable>
            //    tr.ImageE5.#e1^e2^e3# = <variable>
            //    tr.ImageE4.#E0# = <variable>
            //    tr.ImageE4.#e1# = <variable>
            //    tr.ImageE4.#e2# = <variable>
            //    tr.ImageE4.#e1^e2# = <variable>
            //    tr.ImageE4.#e3# = <variable>
            //    tr.ImageE4.#e1^e3# = <variable>
            //    tr.ImageE4.#e2^e3# = <variable>
            //    tr.ImageE4.#e1^e2^e3# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE3.#e3# = <variable>
            //    tr.ImageE3.#e1^e3# = <variable>
            //    tr.ImageE3.#e2^e3# = <variable>
            //    tr.ImageE3.#e1^e2^e3# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE2.#e3# = <variable>
            //    tr.ImageE2.#e1^e3# = <variable>
            //    tr.ImageE2.#e2^e3# = <variable>
            //    tr.ImageE2.#e1^e2^e3# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE1.#e3# = <variable>
            //    tr.ImageE1.#e1^e3# = <variable>
            //    tr.ImageE1.#e2^e3# = <variable>
            //    tr.ImageE1.#e1^e2^e3# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE0.#e3# = <variable>
            //    tr.ImageE0.#e1^e3# = <variable>
            //    tr.ImageE0.#e2^e3# = <variable>
            //    tr.ImageE0.#e1^e2^e3# = <variable>
            //    s = <variable>
            
            
            result.ImageE7.Coef[0] = (tr.ImageE7.Coef[0] * s);
            result.ImageE7.Coef[1] = (tr.ImageE7.Coef[1] * s);
            result.ImageE7.Coef[2] = (tr.ImageE7.Coef[2] * s);
            result.ImageE7.Coef[3] = (tr.ImageE7.Coef[3] * s);
            result.ImageE7.Coef[4] = (tr.ImageE7.Coef[4] * s);
            result.ImageE7.Coef[5] = (tr.ImageE7.Coef[5] * s);
            result.ImageE7.Coef[6] = (tr.ImageE7.Coef[6] * s);
            result.ImageE7.Coef[7] = (tr.ImageE7.Coef[7] * s);
            result.ImageE6.Coef[0] = (tr.ImageE6.Coef[0] * s);
            result.ImageE6.Coef[1] = (tr.ImageE6.Coef[1] * s);
            result.ImageE6.Coef[2] = (tr.ImageE6.Coef[2] * s);
            result.ImageE6.Coef[3] = (tr.ImageE6.Coef[3] * s);
            result.ImageE6.Coef[4] = (tr.ImageE6.Coef[4] * s);
            result.ImageE6.Coef[5] = (tr.ImageE6.Coef[5] * s);
            result.ImageE6.Coef[6] = (tr.ImageE6.Coef[6] * s);
            result.ImageE6.Coef[7] = (tr.ImageE6.Coef[7] * s);
            result.ImageE5.Coef[0] = (tr.ImageE5.Coef[0] * s);
            result.ImageE5.Coef[1] = (tr.ImageE5.Coef[1] * s);
            result.ImageE5.Coef[2] = (tr.ImageE5.Coef[2] * s);
            result.ImageE5.Coef[3] = (tr.ImageE5.Coef[3] * s);
            result.ImageE5.Coef[4] = (tr.ImageE5.Coef[4] * s);
            result.ImageE5.Coef[5] = (tr.ImageE5.Coef[5] * s);
            result.ImageE5.Coef[6] = (tr.ImageE5.Coef[6] * s);
            result.ImageE5.Coef[7] = (tr.ImageE5.Coef[7] * s);
            result.ImageE4.Coef[0] = (tr.ImageE4.Coef[0] * s);
            result.ImageE4.Coef[1] = (tr.ImageE4.Coef[1] * s);
            result.ImageE4.Coef[2] = (tr.ImageE4.Coef[2] * s);
            result.ImageE4.Coef[3] = (tr.ImageE4.Coef[3] * s);
            result.ImageE4.Coef[4] = (tr.ImageE4.Coef[4] * s);
            result.ImageE4.Coef[5] = (tr.ImageE4.Coef[5] * s);
            result.ImageE4.Coef[6] = (tr.ImageE4.Coef[6] * s);
            result.ImageE4.Coef[7] = (tr.ImageE4.Coef[7] * s);
            result.ImageE3.Coef[0] = (tr.ImageE3.Coef[0] * s);
            result.ImageE3.Coef[1] = (tr.ImageE3.Coef[1] * s);
            result.ImageE3.Coef[2] = (tr.ImageE3.Coef[2] * s);
            result.ImageE3.Coef[3] = (tr.ImageE3.Coef[3] * s);
            result.ImageE3.Coef[4] = (tr.ImageE3.Coef[4] * s);
            result.ImageE3.Coef[5] = (tr.ImageE3.Coef[5] * s);
            result.ImageE3.Coef[6] = (tr.ImageE3.Coef[6] * s);
            result.ImageE3.Coef[7] = (tr.ImageE3.Coef[7] * s);
            result.ImageE2.Coef[0] = (tr.ImageE2.Coef[0] * s);
            result.ImageE2.Coef[1] = (tr.ImageE2.Coef[1] * s);
            result.ImageE2.Coef[2] = (tr.ImageE2.Coef[2] * s);
            result.ImageE2.Coef[3] = (tr.ImageE2.Coef[3] * s);
            result.ImageE2.Coef[4] = (tr.ImageE2.Coef[4] * s);
            result.ImageE2.Coef[5] = (tr.ImageE2.Coef[5] * s);
            result.ImageE2.Coef[6] = (tr.ImageE2.Coef[6] * s);
            result.ImageE2.Coef[7] = (tr.ImageE2.Coef[7] * s);
            result.ImageE1.Coef[0] = (tr.ImageE1.Coef[0] * s);
            result.ImageE1.Coef[1] = (tr.ImageE1.Coef[1] * s);
            result.ImageE1.Coef[2] = (tr.ImageE1.Coef[2] * s);
            result.ImageE1.Coef[3] = (tr.ImageE1.Coef[3] * s);
            result.ImageE1.Coef[4] = (tr.ImageE1.Coef[4] * s);
            result.ImageE1.Coef[5] = (tr.ImageE1.Coef[5] * s);
            result.ImageE1.Coef[6] = (tr.ImageE1.Coef[6] * s);
            result.ImageE1.Coef[7] = (tr.ImageE1.Coef[7] * s);
            result.ImageE0.Coef[0] = (tr.ImageE0.Coef[0] * s);
            result.ImageE0.Coef[1] = (tr.ImageE0.Coef[1] * s);
            result.ImageE0.Coef[2] = (tr.ImageE0.Coef[2] * s);
            result.ImageE0.Coef[3] = (tr.ImageE0.Coef[3] * s);
            result.ImageE0.Coef[4] = (tr.ImageE0.Coef[4] * s);
            result.ImageE0.Coef[5] = (tr.ImageE0.Coef[5] * s);
            result.ImageE0.Coef[6] = (tr.ImageE0.Coef[6] * s);
            result.ImageE0.Coef[7] = (tr.ImageE0.Coef[7] * s);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct DivideLT(geometry3d.e3d.LTStruct tr, double s)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:06 م
            //Macro: geometry3d.e3d.DivideLT
            //Input Variables: 65 used, 0 not used, 65 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 64 total.
            //Computations: 1 average, 65 total.
            //Memory Reads: 1.98461538461538 average, 129 total.
            //Memory Writes: 65 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    tr.ImageE7.#E0# = <variable>
            //    tr.ImageE7.#e1# = <variable>
            //    tr.ImageE7.#e2# = <variable>
            //    tr.ImageE7.#e1^e2# = <variable>
            //    tr.ImageE7.#e3# = <variable>
            //    tr.ImageE7.#e1^e3# = <variable>
            //    tr.ImageE7.#e2^e3# = <variable>
            //    tr.ImageE7.#e1^e2^e3# = <variable>
            //    tr.ImageE6.#E0# = <variable>
            //    tr.ImageE6.#e1# = <variable>
            //    tr.ImageE6.#e2# = <variable>
            //    tr.ImageE6.#e1^e2# = <variable>
            //    tr.ImageE6.#e3# = <variable>
            //    tr.ImageE6.#e1^e3# = <variable>
            //    tr.ImageE6.#e2^e3# = <variable>
            //    tr.ImageE6.#e1^e2^e3# = <variable>
            //    tr.ImageE5.#E0# = <variable>
            //    tr.ImageE5.#e1# = <variable>
            //    tr.ImageE5.#e2# = <variable>
            //    tr.ImageE5.#e1^e2# = <variable>
            //    tr.ImageE5.#e3# = <variable>
            //    tr.ImageE5.#e1^e3# = <variable>
            //    tr.ImageE5.#e2^e3# = <variable>
            //    tr.ImageE5.#e1^e2^e3# = <variable>
            //    tr.ImageE4.#E0# = <variable>
            //    tr.ImageE4.#e1# = <variable>
            //    tr.ImageE4.#e2# = <variable>
            //    tr.ImageE4.#e1^e2# = <variable>
            //    tr.ImageE4.#e3# = <variable>
            //    tr.ImageE4.#e1^e3# = <variable>
            //    tr.ImageE4.#e2^e3# = <variable>
            //    tr.ImageE4.#e1^e2^e3# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE3.#e3# = <variable>
            //    tr.ImageE3.#e1^e3# = <variable>
            //    tr.ImageE3.#e2^e3# = <variable>
            //    tr.ImageE3.#e1^e2^e3# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE2.#e3# = <variable>
            //    tr.ImageE2.#e1^e3# = <variable>
            //    tr.ImageE2.#e2^e3# = <variable>
            //    tr.ImageE2.#e1^e2^e3# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE1.#e3# = <variable>
            //    tr.ImageE1.#e1^e3# = <variable>
            //    tr.ImageE1.#e2^e3# = <variable>
            //    tr.ImageE1.#e1^e2^e3# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE0.#e3# = <variable>
            //    tr.ImageE0.#e1^e3# = <variable>
            //    tr.ImageE0.#e2^e3# = <variable>
            //    tr.ImageE0.#e1^e2^e3# = <variable>
            //    s = <variable>
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(s, -1);
            result.ImageE7.Coef[0] = (tr.ImageE7.Coef[0] * tempVar0000);
            result.ImageE7.Coef[1] = (tr.ImageE7.Coef[1] * tempVar0000);
            result.ImageE7.Coef[2] = (tr.ImageE7.Coef[2] * tempVar0000);
            result.ImageE7.Coef[3] = (tr.ImageE7.Coef[3] * tempVar0000);
            result.ImageE7.Coef[4] = (tr.ImageE7.Coef[4] * tempVar0000);
            result.ImageE7.Coef[5] = (tr.ImageE7.Coef[5] * tempVar0000);
            result.ImageE7.Coef[6] = (tr.ImageE7.Coef[6] * tempVar0000);
            result.ImageE7.Coef[7] = (tr.ImageE7.Coef[7] * tempVar0000);
            result.ImageE6.Coef[0] = (tr.ImageE6.Coef[0] * tempVar0000);
            result.ImageE6.Coef[1] = (tr.ImageE6.Coef[1] * tempVar0000);
            result.ImageE6.Coef[2] = (tr.ImageE6.Coef[2] * tempVar0000);
            result.ImageE6.Coef[3] = (tr.ImageE6.Coef[3] * tempVar0000);
            result.ImageE6.Coef[4] = (tr.ImageE6.Coef[4] * tempVar0000);
            result.ImageE6.Coef[5] = (tr.ImageE6.Coef[5] * tempVar0000);
            result.ImageE6.Coef[6] = (tr.ImageE6.Coef[6] * tempVar0000);
            result.ImageE6.Coef[7] = (tr.ImageE6.Coef[7] * tempVar0000);
            result.ImageE5.Coef[0] = (tr.ImageE5.Coef[0] * tempVar0000);
            result.ImageE5.Coef[1] = (tr.ImageE5.Coef[1] * tempVar0000);
            result.ImageE5.Coef[2] = (tr.ImageE5.Coef[2] * tempVar0000);
            result.ImageE5.Coef[3] = (tr.ImageE5.Coef[3] * tempVar0000);
            result.ImageE5.Coef[4] = (tr.ImageE5.Coef[4] * tempVar0000);
            result.ImageE5.Coef[5] = (tr.ImageE5.Coef[5] * tempVar0000);
            result.ImageE5.Coef[6] = (tr.ImageE5.Coef[6] * tempVar0000);
            result.ImageE5.Coef[7] = (tr.ImageE5.Coef[7] * tempVar0000);
            result.ImageE4.Coef[0] = (tr.ImageE4.Coef[0] * tempVar0000);
            result.ImageE4.Coef[1] = (tr.ImageE4.Coef[1] * tempVar0000);
            result.ImageE4.Coef[2] = (tr.ImageE4.Coef[2] * tempVar0000);
            result.ImageE4.Coef[3] = (tr.ImageE4.Coef[3] * tempVar0000);
            result.ImageE4.Coef[4] = (tr.ImageE4.Coef[4] * tempVar0000);
            result.ImageE4.Coef[5] = (tr.ImageE4.Coef[5] * tempVar0000);
            result.ImageE4.Coef[6] = (tr.ImageE4.Coef[6] * tempVar0000);
            result.ImageE4.Coef[7] = (tr.ImageE4.Coef[7] * tempVar0000);
            result.ImageE3.Coef[0] = (tr.ImageE3.Coef[0] * tempVar0000);
            result.ImageE3.Coef[1] = (tr.ImageE3.Coef[1] * tempVar0000);
            result.ImageE3.Coef[2] = (tr.ImageE3.Coef[2] * tempVar0000);
            result.ImageE3.Coef[3] = (tr.ImageE3.Coef[3] * tempVar0000);
            result.ImageE3.Coef[4] = (tr.ImageE3.Coef[4] * tempVar0000);
            result.ImageE3.Coef[5] = (tr.ImageE3.Coef[5] * tempVar0000);
            result.ImageE3.Coef[6] = (tr.ImageE3.Coef[6] * tempVar0000);
            result.ImageE3.Coef[7] = (tr.ImageE3.Coef[7] * tempVar0000);
            result.ImageE2.Coef[0] = (tr.ImageE2.Coef[0] * tempVar0000);
            result.ImageE2.Coef[1] = (tr.ImageE2.Coef[1] * tempVar0000);
            result.ImageE2.Coef[2] = (tr.ImageE2.Coef[2] * tempVar0000);
            result.ImageE2.Coef[3] = (tr.ImageE2.Coef[3] * tempVar0000);
            result.ImageE2.Coef[4] = (tr.ImageE2.Coef[4] * tempVar0000);
            result.ImageE2.Coef[5] = (tr.ImageE2.Coef[5] * tempVar0000);
            result.ImageE2.Coef[6] = (tr.ImageE2.Coef[6] * tempVar0000);
            result.ImageE2.Coef[7] = (tr.ImageE2.Coef[7] * tempVar0000);
            result.ImageE1.Coef[0] = (tr.ImageE1.Coef[0] * tempVar0000);
            result.ImageE1.Coef[1] = (tr.ImageE1.Coef[1] * tempVar0000);
            result.ImageE1.Coef[2] = (tr.ImageE1.Coef[2] * tempVar0000);
            result.ImageE1.Coef[3] = (tr.ImageE1.Coef[3] * tempVar0000);
            result.ImageE1.Coef[4] = (tr.ImageE1.Coef[4] * tempVar0000);
            result.ImageE1.Coef[5] = (tr.ImageE1.Coef[5] * tempVar0000);
            result.ImageE1.Coef[6] = (tr.ImageE1.Coef[6] * tempVar0000);
            result.ImageE1.Coef[7] = (tr.ImageE1.Coef[7] * tempVar0000);
            result.ImageE0.Coef[0] = (tr.ImageE0.Coef[0] * tempVar0000);
            result.ImageE0.Coef[1] = (tr.ImageE0.Coef[1] * tempVar0000);
            result.ImageE0.Coef[2] = (tr.ImageE0.Coef[2] * tempVar0000);
            result.ImageE0.Coef[3] = (tr.ImageE0.Coef[3] * tempVar0000);
            result.ImageE0.Coef[4] = (tr.ImageE0.Coef[4] * tempVar0000);
            result.ImageE0.Coef[5] = (tr.ImageE0.Coef[5] * tempVar0000);
            result.ImageE0.Coef[6] = (tr.ImageE0.Coef[6] * tempVar0000);
            result.ImageE0.Coef[7] = (tr.ImageE0.Coef[7] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct OMToLT(geometry3d.e3d.OMStruct om)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:06 م
            //Macro: geometry3d.e3d.OMToLT
            //Input Variables: 9 used, 15 not used, 24 total.
            //Temp Variables: 25 sub-expressions, 0 generated temps, 25 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 64 total.
            //Computations: 0.51685393258427 average, 46 total.
            //Memory Reads: 0.887640449438202 average, 79 total.
            //Memory Writes: 89 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.ImageE7.Coef[0] = 0;
            result.ImageE7.Coef[1] = 0;
            result.ImageE7.Coef[2] = 0;
            result.ImageE7.Coef[3] = 0;
            result.ImageE7.Coef[4] = 0;
            result.ImageE7.Coef[5] = 0;
            result.ImageE7.Coef[6] = 0;
            result.ImageE6.Coef[0] = 0;
            result.ImageE6.Coef[1] = 0;
            result.ImageE6.Coef[2] = 0;
            result.ImageE6.Coef[4] = 0;
            result.ImageE6.Coef[7] = 0;
            result.ImageE5.Coef[0] = 0;
            result.ImageE5.Coef[1] = 0;
            result.ImageE5.Coef[2] = 0;
            result.ImageE5.Coef[4] = 0;
            result.ImageE5.Coef[7] = 0;
            result.ImageE4.Coef[0] = 0;
            result.ImageE4.Coef[3] = 0;
            result.ImageE4.Coef[5] = 0;
            result.ImageE4.Coef[6] = 0;
            result.ImageE4.Coef[7] = 0;
            result.ImageE3.Coef[0] = 0;
            result.ImageE3.Coef[1] = 0;
            result.ImageE3.Coef[2] = 0;
            result.ImageE3.Coef[4] = 0;
            result.ImageE3.Coef[7] = 0;
            result.ImageE2.Coef[0] = 0;
            result.ImageE2.Coef[3] = 0;
            result.ImageE2.Coef[5] = 0;
            result.ImageE2.Coef[6] = 0;
            result.ImageE2.Coef[7] = 0;
            result.ImageE1.Coef[0] = 0;
            result.ImageE1.Coef[3] = 0;
            result.ImageE1.Coef[5] = 0;
            result.ImageE1.Coef[6] = 0;
            result.ImageE1.Coef[7] = 0;
            result.ImageE0.Coef[0] = 1;
            result.ImageE0.Coef[1] = 0;
            result.ImageE0.Coef[2] = 0;
            result.ImageE0.Coef[3] = 0;
            result.ImageE0.Coef[4] = 0;
            result.ImageE0.Coef[5] = 0;
            result.ImageE0.Coef[6] = 0;
            result.ImageE0.Coef[7] = 0;
            result.ImageE4.Coef[1] = om.ImageV3.Coef[1];
            result.ImageE4.Coef[2] = om.ImageV3.Coef[2];
            result.ImageE4.Coef[4] = om.ImageV3.Coef[4];
            result.ImageE2.Coef[1] = om.ImageV2.Coef[1];
            result.ImageE2.Coef[2] = om.ImageV2.Coef[2];
            result.ImageE2.Coef[4] = om.ImageV2.Coef[4];
            result.ImageE1.Coef[1] = om.ImageV1.Coef[1];
            result.ImageE1.Coef[2] = om.ImageV1.Coef[2];
            result.ImageE1.Coef[4] = om.ImageV1.Coef[4];
            tempVar0000 = (-1 * om.ImageV3.Coef[2] * om.ImageV2.Coef[1]);
            tempVar0001 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[2]);
            result.ImageE6.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[1]);
            tempVar0001 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[4]);
            result.ImageE6.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[2]);
            tempVar0001 = (om.ImageV3.Coef[2] * om.ImageV2.Coef[4]);
            result.ImageE6.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV3.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV3.Coef[1] * om.ImageV1.Coef[2]);
            result.ImageE5.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV3.Coef[4] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV3.Coef[1] * om.ImageV1.Coef[4]);
            result.ImageE5.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV3.Coef[4] * om.ImageV1.Coef[2]);
            tempVar0001 = (om.ImageV3.Coef[2] * om.ImageV1.Coef[4]);
            result.ImageE5.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV2.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[2]);
            result.ImageE3.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV2.Coef[4] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[4]);
            result.ImageE3.Coef[5] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV2.Coef[4] * om.ImageV1.Coef[2]);
            tempVar0001 = (om.ImageV2.Coef[2] * om.ImageV1.Coef[4]);
            result.ImageE3.Coef[6] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV1.Coef[4] * tempVar0000);
            tempVar0001 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV1.Coef[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * om.ImageV1.Coef[1] * tempVar0001);
            result.ImageE7.Coef[7] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyOM(geometry3d.e3d.OMStruct om, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:08 م
            //Macro: geometry3d.e3d.ApplyOM
            //Input Variables: 17 used, 15 not used, 32 total.
            //Temp Variables: 56 sub-expressions, 0 generated temps, 56 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 8 total.
            //Computations: 1.15625 average, 74 total.
            //Memory Reads: 1.984375 average, 127 total.
            //Memory Writes: 64 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            double tempVar0003;
            double tempVar0004;
            
            result.Coef[0] = mv.Coef[0];
            tempVar0000 = (om.ImageV1.Coef[1] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV3.Coef[1] * mv.Coef[4]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om.ImageV1.Coef[2] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[2] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV3.Coef[2] * mv.Coef[4]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om.ImageV1.Coef[4] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[4] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV3.Coef[4] * mv.Coef[4]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV2.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv.Coef[3] * tempVar0000);
            tempVar0001 = (-1 * om.ImageV3.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0002 = (om.ImageV3.Coef[1] * om.ImageV1.Coef[2]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (mv.Coef[5] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * om.ImageV3.Coef[2] * om.ImageV2.Coef[1]);
            tempVar0002 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[2]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0002 = (mv.Coef[6] * tempVar0001);
            result.Coef[3] = (tempVar0000 + tempVar0002);
            tempVar0000 = (-1 * om.ImageV2.Coef[4] * om.ImageV1.Coef[1]);
            tempVar0002 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0000 = (mv.Coef[3] * tempVar0000);
            tempVar0002 = (-1 * om.ImageV3.Coef[4] * om.ImageV1.Coef[1]);
            tempVar0003 = (om.ImageV3.Coef[1] * om.ImageV1.Coef[4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0002 = (mv.Coef[5] * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0002);
            tempVar0002 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[1]);
            tempVar0003 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[4]);
            tempVar0002 = (tempVar0002 + tempVar0003);
            tempVar0003 = (mv.Coef[6] * tempVar0002);
            result.Coef[5] = (tempVar0000 + tempVar0003);
            tempVar0000 = (-1 * om.ImageV2.Coef[4] * om.ImageV1.Coef[2]);
            tempVar0003 = (om.ImageV2.Coef[2] * om.ImageV1.Coef[4]);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0000 = (mv.Coef[3] * tempVar0000);
            tempVar0003 = (-1 * om.ImageV3.Coef[4] * om.ImageV1.Coef[2]);
            tempVar0004 = (om.ImageV3.Coef[2] * om.ImageV1.Coef[4]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0003 = (mv.Coef[5] * tempVar0003);
            tempVar0000 = (tempVar0000 + tempVar0003);
            tempVar0003 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[2]);
            tempVar0004 = (om.ImageV3.Coef[2] * om.ImageV2.Coef[4]);
            tempVar0003 = (tempVar0003 + tempVar0004);
            tempVar0004 = (mv.Coef[6] * tempVar0003);
            result.Coef[6] = (tempVar0000 + tempVar0004);
            tempVar0000 = (-1 * om.ImageV1.Coef[4] * tempVar0001);
            tempVar0001 = (om.ImageV1.Coef[2] * tempVar0002);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * om.ImageV1.Coef[1] * tempVar0003);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[7] = (mv.Coef[7] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector AVOM(geometry3d.e3d.OMStruct om, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:08 م
            //Macro: geometry3d.e3d.AVOM
            //Input Variables: 12 used, 20 not used, 32 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 15 total.
            //Memory Reads: 1.5 average, 30 total.
            //Memory Writes: 20 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = 0;
            result.Coef[3] = 0;
            result.Coef[5] = 0;
            result.Coef[6] = 0;
            result.Coef[7] = 0;
            tempVar0000 = (om.ImageV1.Coef[1] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV3.Coef[1] * mv.Coef[4]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om.ImageV1.Coef[2] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[2] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV3.Coef[2] * mv.Coef[4]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om.ImageV1.Coef[4] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[4] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om.ImageV3.Coef[4] * mv.Coef[4]);
            result.Coef[4] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct TransOM(geometry3d.e3d.OMStruct om)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:08 م
            //Macro: geometry3d.e3d.TransOM
            //Input Variables: 9 used, 15 not used, 24 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 24 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0.375 average, 9 total.
            //Memory Writes: 24 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            result.ImageV3.Coef[1] = om.ImageV1.Coef[4];
            result.ImageV3.Coef[2] = om.ImageV2.Coef[4];
            result.ImageV3.Coef[4] = om.ImageV3.Coef[4];
            result.ImageV2.Coef[1] = om.ImageV1.Coef[2];
            result.ImageV2.Coef[2] = om.ImageV2.Coef[2];
            result.ImageV2.Coef[4] = om.ImageV3.Coef[2];
            result.ImageV1.Coef[1] = om.ImageV1.Coef[1];
            result.ImageV1.Coef[2] = om.ImageV2.Coef[1];
            result.ImageV1.Coef[4] = om.ImageV3.Coef[1];
            
            return result;
        }
        
        public static double DetOM(geometry3d.e3d.OMStruct om)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.DetOM
            //Input Variables: 9 used, 15 not used, 24 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.35714285714286 average, 19 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = (-1 * om.ImageV3.Coef[2] * om.ImageV2.Coef[1]);
            tempVar0001 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV1.Coef[4] * tempVar0000);
            tempVar0001 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[1]);
            tempVar0002 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[4]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (om.ImageV1.Coef[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[2]);
            tempVar0002 = (om.ImageV3.Coef[2] * om.ImageV2.Coef[4]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * om.ImageV1.Coef[1] * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double EDetOM(geometry3d.e3d.OMStruct om)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.EDetOM
            //Input Variables: 9 used, 15 not used, 24 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.35714285714286 average, 19 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = (-1 * om.ImageV3.Coef[2] * om.ImageV2.Coef[1]);
            tempVar0001 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV1.Coef[4] * tempVar0000);
            tempVar0001 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[1]);
            tempVar0002 = (om.ImageV3.Coef[1] * om.ImageV2.Coef[4]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (om.ImageV1.Coef[2] * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * om.ImageV3.Coef[4] * om.ImageV2.Coef[2]);
            tempVar0002 = (om.ImageV3.Coef[2] * om.ImageV2.Coef[4]);
            tempVar0001 = (tempVar0001 + tempVar0002);
            tempVar0001 = (-1 * om.ImageV1.Coef[1] * tempVar0001);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct ComposeOM(geometry3d.e3d.OMStruct om1, geometry3d.e3d.OMStruct om2)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.ComposeOM
            //Input Variables: 18 used, 30 not used, 48 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 24 total.
            //Computations: 0.75 average, 45 total.
            //Memory Reads: 1.5 average, 90 total.
            //Memory Writes: 60 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    om1.ImageV3.#E0# = <variable>
            //    om1.ImageV3.#e1# = <variable>
            //    om1.ImageV3.#e2# = <variable>
            //    om1.ImageV3.#e1^e2# = <variable>
            //    om1.ImageV3.#e3# = <variable>
            //    om1.ImageV3.#e1^e3# = <variable>
            //    om1.ImageV3.#e2^e3# = <variable>
            //    om1.ImageV3.#e1^e2^e3# = <variable>
            //    om1.ImageV2.#E0# = <variable>
            //    om1.ImageV2.#e1# = <variable>
            //    om1.ImageV2.#e2# = <variable>
            //    om1.ImageV2.#e1^e2# = <variable>
            //    om1.ImageV2.#e3# = <variable>
            //    om1.ImageV2.#e1^e3# = <variable>
            //    om1.ImageV2.#e2^e3# = <variable>
            //    om1.ImageV2.#e1^e2^e3# = <variable>
            //    om1.ImageV1.#E0# = <variable>
            //    om1.ImageV1.#e1# = <variable>
            //    om1.ImageV1.#e2# = <variable>
            //    om1.ImageV1.#e1^e2# = <variable>
            //    om1.ImageV1.#e3# = <variable>
            //    om1.ImageV1.#e1^e3# = <variable>
            //    om1.ImageV1.#e2^e3# = <variable>
            //    om1.ImageV1.#e1^e2^e3# = <variable>
            //    om2.ImageV3.#E0# = <variable>
            //    om2.ImageV3.#e1# = <variable>
            //    om2.ImageV3.#e2# = <variable>
            //    om2.ImageV3.#e1^e2# = <variable>
            //    om2.ImageV3.#e3# = <variable>
            //    om2.ImageV3.#e1^e3# = <variable>
            //    om2.ImageV3.#e2^e3# = <variable>
            //    om2.ImageV3.#e1^e2^e3# = <variable>
            //    om2.ImageV2.#E0# = <variable>
            //    om2.ImageV2.#e1# = <variable>
            //    om2.ImageV2.#e2# = <variable>
            //    om2.ImageV2.#e1^e2# = <variable>
            //    om2.ImageV2.#e3# = <variable>
            //    om2.ImageV2.#e1^e3# = <variable>
            //    om2.ImageV2.#e2^e3# = <variable>
            //    om2.ImageV2.#e1^e2^e3# = <variable>
            //    om2.ImageV1.#E0# = <variable>
            //    om2.ImageV1.#e1# = <variable>
            //    om2.ImageV1.#e2# = <variable>
            //    om2.ImageV1.#e1^e2# = <variable>
            //    om2.ImageV1.#e3# = <variable>
            //    om2.ImageV1.#e1^e3# = <variable>
            //    om2.ImageV1.#e2^e3# = <variable>
            //    om2.ImageV1.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            tempVar0000 = (om1.ImageV1.Coef[1] * om2.ImageV3.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[1] * om2.ImageV3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[1] * om2.ImageV3.Coef[4]);
            result.ImageV3.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[2] * om2.ImageV3.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[2] * om2.ImageV3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[2] * om2.ImageV3.Coef[4]);
            result.ImageV3.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[4] * om2.ImageV3.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[4] * om2.ImageV3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[4] * om2.ImageV3.Coef[4]);
            result.ImageV3.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[1] * om2.ImageV2.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[1] * om2.ImageV2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[1] * om2.ImageV2.Coef[4]);
            result.ImageV2.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[2] * om2.ImageV2.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[2] * om2.ImageV2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[2] * om2.ImageV2.Coef[4]);
            result.ImageV2.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[4] * om2.ImageV2.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[4] * om2.ImageV2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[4] * om2.ImageV2.Coef[4]);
            result.ImageV2.Coef[4] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[1] * om2.ImageV1.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[1] * om2.ImageV1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[1] * om2.ImageV1.Coef[4]);
            result.ImageV1.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[2] * om2.ImageV1.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[2] * om2.ImageV1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[2] * om2.ImageV1.Coef[4]);
            result.ImageV1.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[4] * om2.ImageV1.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[4] * om2.ImageV1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (om1.ImageV3.Coef[4] * om2.ImageV1.Coef[4]);
            result.ImageV1.Coef[4] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct AddOM(geometry3d.e3d.OMStruct om1, geometry3d.e3d.OMStruct om2)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.AddOM
            //Input Variables: 18 used, 30 not used, 48 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 24 total.
            //Computations: 0.375 average, 9 total.
            //Memory Reads: 0.75 average, 18 total.
            //Memory Writes: 24 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    om1.ImageV3.#E0# = <variable>
            //    om1.ImageV3.#e1# = <variable>
            //    om1.ImageV3.#e2# = <variable>
            //    om1.ImageV3.#e1^e2# = <variable>
            //    om1.ImageV3.#e3# = <variable>
            //    om1.ImageV3.#e1^e3# = <variable>
            //    om1.ImageV3.#e2^e3# = <variable>
            //    om1.ImageV3.#e1^e2^e3# = <variable>
            //    om1.ImageV2.#E0# = <variable>
            //    om1.ImageV2.#e1# = <variable>
            //    om1.ImageV2.#e2# = <variable>
            //    om1.ImageV2.#e1^e2# = <variable>
            //    om1.ImageV2.#e3# = <variable>
            //    om1.ImageV2.#e1^e3# = <variable>
            //    om1.ImageV2.#e2^e3# = <variable>
            //    om1.ImageV2.#e1^e2^e3# = <variable>
            //    om1.ImageV1.#E0# = <variable>
            //    om1.ImageV1.#e1# = <variable>
            //    om1.ImageV1.#e2# = <variable>
            //    om1.ImageV1.#e1^e2# = <variable>
            //    om1.ImageV1.#e3# = <variable>
            //    om1.ImageV1.#e1^e3# = <variable>
            //    om1.ImageV1.#e2^e3# = <variable>
            //    om1.ImageV1.#e1^e2^e3# = <variable>
            //    om2.ImageV3.#E0# = <variable>
            //    om2.ImageV3.#e1# = <variable>
            //    om2.ImageV3.#e2# = <variable>
            //    om2.ImageV3.#e1^e2# = <variable>
            //    om2.ImageV3.#e3# = <variable>
            //    om2.ImageV3.#e1^e3# = <variable>
            //    om2.ImageV3.#e2^e3# = <variable>
            //    om2.ImageV3.#e1^e2^e3# = <variable>
            //    om2.ImageV2.#E0# = <variable>
            //    om2.ImageV2.#e1# = <variable>
            //    om2.ImageV2.#e2# = <variable>
            //    om2.ImageV2.#e1^e2# = <variable>
            //    om2.ImageV2.#e3# = <variable>
            //    om2.ImageV2.#e1^e3# = <variable>
            //    om2.ImageV2.#e2^e3# = <variable>
            //    om2.ImageV2.#e1^e2^e3# = <variable>
            //    om2.ImageV1.#E0# = <variable>
            //    om2.ImageV1.#e1# = <variable>
            //    om2.ImageV1.#e2# = <variable>
            //    om2.ImageV1.#e1^e2# = <variable>
            //    om2.ImageV1.#e3# = <variable>
            //    om2.ImageV1.#e1^e3# = <variable>
            //    om2.ImageV1.#e2^e3# = <variable>
            //    om2.ImageV1.#e1^e2^e3# = <variable>
            
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            result.ImageV3.Coef[1] = (om1.ImageV3.Coef[1] + om2.ImageV3.Coef[1]);
            result.ImageV3.Coef[2] = (om1.ImageV3.Coef[2] + om2.ImageV3.Coef[2]);
            result.ImageV3.Coef[4] = (om1.ImageV3.Coef[4] + om2.ImageV3.Coef[4]);
            result.ImageV2.Coef[1] = (om1.ImageV2.Coef[1] + om2.ImageV2.Coef[1]);
            result.ImageV2.Coef[2] = (om1.ImageV2.Coef[2] + om2.ImageV2.Coef[2]);
            result.ImageV2.Coef[4] = (om1.ImageV2.Coef[4] + om2.ImageV2.Coef[4]);
            result.ImageV1.Coef[1] = (om1.ImageV1.Coef[1] + om2.ImageV1.Coef[1]);
            result.ImageV1.Coef[2] = (om1.ImageV1.Coef[2] + om2.ImageV1.Coef[2]);
            result.ImageV1.Coef[4] = (om1.ImageV1.Coef[4] + om2.ImageV1.Coef[4]);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct SubtractOM(geometry3d.e3d.OMStruct om1, geometry3d.e3d.OMStruct om2)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.SubtractOM
            //Input Variables: 18 used, 30 not used, 48 total.
            //Temp Variables: 9 sub-expressions, 0 generated temps, 9 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 24 total.
            //Computations: 0.545454545454545 average, 18 total.
            //Memory Reads: 0.818181818181818 average, 27 total.
            //Memory Writes: 33 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    om1.ImageV3.#E0# = <variable>
            //    om1.ImageV3.#e1# = <variable>
            //    om1.ImageV3.#e2# = <variable>
            //    om1.ImageV3.#e1^e2# = <variable>
            //    om1.ImageV3.#e3# = <variable>
            //    om1.ImageV3.#e1^e3# = <variable>
            //    om1.ImageV3.#e2^e3# = <variable>
            //    om1.ImageV3.#e1^e2^e3# = <variable>
            //    om1.ImageV2.#E0# = <variable>
            //    om1.ImageV2.#e1# = <variable>
            //    om1.ImageV2.#e2# = <variable>
            //    om1.ImageV2.#e1^e2# = <variable>
            //    om1.ImageV2.#e3# = <variable>
            //    om1.ImageV2.#e1^e3# = <variable>
            //    om1.ImageV2.#e2^e3# = <variable>
            //    om1.ImageV2.#e1^e2^e3# = <variable>
            //    om1.ImageV1.#E0# = <variable>
            //    om1.ImageV1.#e1# = <variable>
            //    om1.ImageV1.#e2# = <variable>
            //    om1.ImageV1.#e1^e2# = <variable>
            //    om1.ImageV1.#e3# = <variable>
            //    om1.ImageV1.#e1^e3# = <variable>
            //    om1.ImageV1.#e2^e3# = <variable>
            //    om1.ImageV1.#e1^e2^e3# = <variable>
            //    om2.ImageV3.#E0# = <variable>
            //    om2.ImageV3.#e1# = <variable>
            //    om2.ImageV3.#e2# = <variable>
            //    om2.ImageV3.#e1^e2# = <variable>
            //    om2.ImageV3.#e3# = <variable>
            //    om2.ImageV3.#e1^e3# = <variable>
            //    om2.ImageV3.#e2^e3# = <variable>
            //    om2.ImageV3.#e1^e2^e3# = <variable>
            //    om2.ImageV2.#E0# = <variable>
            //    om2.ImageV2.#e1# = <variable>
            //    om2.ImageV2.#e2# = <variable>
            //    om2.ImageV2.#e1^e2# = <variable>
            //    om2.ImageV2.#e3# = <variable>
            //    om2.ImageV2.#e1^e3# = <variable>
            //    om2.ImageV2.#e2^e3# = <variable>
            //    om2.ImageV2.#e1^e2^e3# = <variable>
            //    om2.ImageV1.#E0# = <variable>
            //    om2.ImageV1.#e1# = <variable>
            //    om2.ImageV1.#e2# = <variable>
            //    om2.ImageV1.#e1^e2# = <variable>
            //    om2.ImageV1.#e3# = <variable>
            //    om2.ImageV1.#e1^e3# = <variable>
            //    om2.ImageV1.#e2^e3# = <variable>
            //    om2.ImageV1.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            tempVar0000 = (-1 * om2.ImageV3.Coef[1]);
            result.ImageV3.Coef[1] = (om1.ImageV3.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV3.Coef[2]);
            result.ImageV3.Coef[2] = (om1.ImageV3.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV3.Coef[4]);
            result.ImageV3.Coef[4] = (om1.ImageV3.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV2.Coef[1]);
            result.ImageV2.Coef[1] = (om1.ImageV2.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV2.Coef[2]);
            result.ImageV2.Coef[2] = (om1.ImageV2.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV2.Coef[4]);
            result.ImageV2.Coef[4] = (om1.ImageV2.Coef[4] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV1.Coef[1]);
            result.ImageV1.Coef[1] = (om1.ImageV1.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV1.Coef[2]);
            result.ImageV1.Coef[2] = (om1.ImageV1.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV1.Coef[4]);
            result.ImageV1.Coef[4] = (om1.ImageV1.Coef[4] + tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct TimesOM(geometry3d.e3d.OMStruct om, double s)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.TimesOM
            //Input Variables: 10 used, 15 not used, 25 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 24 total.
            //Computations: 0.375 average, 9 total.
            //Memory Reads: 0.75 average, 18 total.
            //Memory Writes: 24 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            //    s = <variable>
            
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            result.ImageV3.Coef[1] = (om.ImageV3.Coef[1] * s);
            result.ImageV3.Coef[2] = (om.ImageV3.Coef[2] * s);
            result.ImageV3.Coef[4] = (om.ImageV3.Coef[4] * s);
            result.ImageV2.Coef[1] = (om.ImageV2.Coef[1] * s);
            result.ImageV2.Coef[2] = (om.ImageV2.Coef[2] * s);
            result.ImageV2.Coef[4] = (om.ImageV2.Coef[4] * s);
            result.ImageV1.Coef[1] = (om.ImageV1.Coef[1] * s);
            result.ImageV1.Coef[2] = (om.ImageV1.Coef[2] * s);
            result.ImageV1.Coef[4] = (om.ImageV1.Coef[4] * s);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct DivideOM(geometry3d.e3d.OMStruct om, double s)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:09 م
            //Macro: geometry3d.e3d.DivideOM
            //Input Variables: 10 used, 15 not used, 25 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 24 total.
            //Computations: 0.4 average, 10 total.
            //Memory Reads: 0.76 average, 19 total.
            //Memory Writes: 25 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    om.ImageV3.#E0# = <variable>
            //    om.ImageV3.#e1# = <variable>
            //    om.ImageV3.#e2# = <variable>
            //    om.ImageV3.#e1^e2# = <variable>
            //    om.ImageV3.#e3# = <variable>
            //    om.ImageV3.#e1^e3# = <variable>
            //    om.ImageV3.#e2^e3# = <variable>
            //    om.ImageV3.#e1^e2^e3# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV2.#e3# = <variable>
            //    om.ImageV2.#e1^e3# = <variable>
            //    om.ImageV2.#e2^e3# = <variable>
            //    om.ImageV2.#e1^e2^e3# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    om.ImageV1.#e3# = <variable>
            //    om.ImageV1.#e1^e3# = <variable>
            //    om.ImageV1.#e2^e3# = <variable>
            //    om.ImageV1.#e1^e2^e3# = <variable>
            //    s = <variable>
            
            double tempVar0000;
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            tempVar0000 = Math.Pow(s, -1);
            result.ImageV3.Coef[1] = (om.ImageV3.Coef[1] * tempVar0000);
            result.ImageV3.Coef[2] = (om.ImageV3.Coef[2] * tempVar0000);
            result.ImageV3.Coef[4] = (om.ImageV3.Coef[4] * tempVar0000);
            result.ImageV2.Coef[1] = (om.ImageV2.Coef[1] * tempVar0000);
            result.ImageV2.Coef[2] = (om.ImageV2.Coef[2] * tempVar0000);
            result.ImageV2.Coef[4] = (om.ImageV2.Coef[4] * tempVar0000);
            result.ImageV1.Coef[1] = (om.ImageV1.Coef[1] * tempVar0000);
            result.ImageV1.Coef[2] = (om.ImageV1.Coef[2] * tempVar0000);
            result.ImageV1.Coef[4] = (om.ImageV1.Coef[4] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct VersorToOM(geometry3d.e3d.Multivector v)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:10 م
            //Macro: geometry3d.e3d.VersorToOM
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 135 sub-expressions, 0 generated temps, 135 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 24 total.
            //Computations: 1.06289308176101 average, 169 total.
            //Memory Reads: 1.70440251572327 average, 271 total.
            //Memory Writes: 159 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[13];
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            tempArray[0] = Math.Pow(v.Coef[0], 2);
            tempArray[0] = (-1 * tempArray[0]);
            tempArray[1] = Math.Pow(v.Coef[1], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[2], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[3], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[4], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[5], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[6], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[7], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[4] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[5] = (v.Coef[4] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[5] * tempArray[0]);
            tempArray[7] = (-1 * v.Coef[6] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[8] = (v.Coef[4] * tempArray[2]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[4]);
            tempArray[10] = (v.Coef[1] * tempArray[5]);
            tempArray[11] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[12] = (v.Coef[5] * tempArray[1]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[3] = (v.Coef[7] * tempArray[3]);
            tempArray[3] = (tempArray[8] + tempArray[3]);
            tempArray[3] = (tempArray[9] + tempArray[3]);
            tempArray[3] = (tempArray[10] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[11] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[2] * tempArray[0]);
            result.ImageV3.Coef[1] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (v.Coef[4] * tempArray[3]);
            tempArray[6] = (v.Coef[5] * tempArray[4]);
            tempArray[8] = (v.Coef[2] * tempArray[5]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (v.Coef[6] * tempArray[1]);
            tempArray[2] = (-1 * v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[1] = (tempArray[6] + tempArray[1]);
            tempArray[1] = (tempArray[8] + tempArray[1]);
            tempArray[1] = (tempArray[9] + tempArray[1]);
            tempArray[2] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[0] = (v.Coef[1] * tempArray[0]);
            result.ImageV3.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[2] = (v.Coef[5] * tempArray[6]);
            tempArray[3] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[4] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[6] + tempArray[0]);
            result.ImageV3.Coef[4] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (v.Coef[2] * tempArray[2]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[2] = (v.Coef[6] * tempArray[6]);
            tempArray[3] = (v.Coef[5] * tempArray[7]);
            tempArray[4] = (v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[3] + tempArray[0]);
            tempArray[1] = (v.Coef[4] * tempArray[0]);
            result.ImageV2.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[4]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[2] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            result.ImageV2.Coef[2] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (-1 * v.Coef[6] * tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[1] = (v.Coef[0] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            result.ImageV2.Coef[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageV1.Coef[1] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            result.ImageV1.Coef[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[10]);
            tempArray[1] = (v.Coef[0] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[1] = (v.Coef[2] * tempArray[0]);
            result.ImageV1.Coef[4] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct VersorToLT(geometry3d.e3d.Multivector v)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:11 م
            //Macro: geometry3d.e3d.VersorToLT
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 169 sub-expressions, 0 generated temps, 169 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 64 total.
            //Computations: 0.96137339055794 average, 224 total.
            //Memory Reads: 1.54077253218884 average, 359 total.
            //Memory Writes: 233 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[13];
            
            result.ImageE7.Coef[0] = 0;
            result.ImageE7.Coef[1] = 0;
            result.ImageE7.Coef[2] = 0;
            result.ImageE7.Coef[3] = 0;
            result.ImageE7.Coef[4] = 0;
            result.ImageE7.Coef[5] = 0;
            result.ImageE7.Coef[6] = 0;
            result.ImageE6.Coef[0] = 0;
            result.ImageE6.Coef[1] = 0;
            result.ImageE6.Coef[2] = 0;
            result.ImageE6.Coef[4] = 0;
            result.ImageE6.Coef[7] = 0;
            result.ImageE5.Coef[0] = 0;
            result.ImageE5.Coef[1] = 0;
            result.ImageE5.Coef[2] = 0;
            result.ImageE5.Coef[4] = 0;
            result.ImageE5.Coef[7] = 0;
            result.ImageE4.Coef[0] = 0;
            result.ImageE4.Coef[3] = 0;
            result.ImageE4.Coef[5] = 0;
            result.ImageE4.Coef[6] = 0;
            result.ImageE4.Coef[7] = 0;
            result.ImageE3.Coef[0] = 0;
            result.ImageE3.Coef[1] = 0;
            result.ImageE3.Coef[2] = 0;
            result.ImageE3.Coef[4] = 0;
            result.ImageE3.Coef[7] = 0;
            result.ImageE2.Coef[0] = 0;
            result.ImageE2.Coef[3] = 0;
            result.ImageE2.Coef[5] = 0;
            result.ImageE2.Coef[6] = 0;
            result.ImageE2.Coef[7] = 0;
            result.ImageE1.Coef[0] = 0;
            result.ImageE1.Coef[3] = 0;
            result.ImageE1.Coef[5] = 0;
            result.ImageE1.Coef[6] = 0;
            result.ImageE1.Coef[7] = 0;
            result.ImageE0.Coef[0] = 1;
            result.ImageE0.Coef[1] = 0;
            result.ImageE0.Coef[2] = 0;
            result.ImageE0.Coef[3] = 0;
            result.ImageE0.Coef[4] = 0;
            result.ImageE0.Coef[5] = 0;
            result.ImageE0.Coef[6] = 0;
            result.ImageE0.Coef[7] = 0;
            tempArray[0] = Math.Pow(v.Coef[0], 2);
            tempArray[0] = (-1 * tempArray[0]);
            tempArray[1] = Math.Pow(v.Coef[1], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[2], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[3], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[4], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[5], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[6], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[7], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[4] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[5] = (v.Coef[4] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[5] * tempArray[0]);
            tempArray[7] = (-1 * v.Coef[6] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[8] = (v.Coef[4] * tempArray[2]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[4]);
            tempArray[10] = (v.Coef[1] * tempArray[5]);
            tempArray[11] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[12] = (v.Coef[5] * tempArray[1]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[3] = (v.Coef[7] * tempArray[3]);
            tempArray[3] = (tempArray[8] + tempArray[3]);
            tempArray[3] = (tempArray[9] + tempArray[3]);
            tempArray[3] = (tempArray[10] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[11] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[2] * tempArray[0]);
            result.ImageE4.Coef[1] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (v.Coef[4] * tempArray[3]);
            tempArray[6] = (v.Coef[5] * tempArray[4]);
            tempArray[8] = (v.Coef[2] * tempArray[5]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (v.Coef[6] * tempArray[1]);
            tempArray[2] = (-1 * v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[1] = (tempArray[6] + tempArray[1]);
            tempArray[1] = (tempArray[8] + tempArray[1]);
            tempArray[1] = (tempArray[9] + tempArray[1]);
            tempArray[2] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[0] = (v.Coef[1] * tempArray[0]);
            result.ImageE4.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[2] = (v.Coef[5] * tempArray[6]);
            tempArray[3] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[4] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[6] + tempArray[0]);
            result.ImageE4.Coef[4] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (v.Coef[2] * tempArray[2]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[2] = (v.Coef[6] * tempArray[6]);
            tempArray[3] = (v.Coef[5] * tempArray[7]);
            tempArray[4] = (v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[3] + tempArray[0]);
            tempArray[1] = (v.Coef[4] * tempArray[0]);
            result.ImageE2.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[4]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[2] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            result.ImageE2.Coef[2] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (-1 * v.Coef[6] * tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[1] = (v.Coef[0] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            result.ImageE2.Coef[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageE1.Coef[1] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            result.ImageE1.Coef[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[10]);
            tempArray[1] = (v.Coef[0] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[1] = (v.Coef[2] * tempArray[0]);
            result.ImageE1.Coef[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (tempArray[3] + tempArray[0]);
            tempArray[2] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[1] + tempArray[0]);
            tempArray[4] = (tempArray[1] * tempArray[2]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[3]);
            result.ImageE6.Coef[3] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (tempArray[0] + tempArray[1]);
            tempArray[5] = (tempArray[3] + tempArray[0]);
            tempArray[2] = (tempArray[4] * tempArray[2]);
            tempArray[0] = (-1 * tempArray[0] * tempArray[5]);
            result.ImageE6.Coef[5] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[4] * tempArray[3]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[5]);
            result.ImageE6.Coef[6] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (tempArray[0] + tempArray[3]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[3]);
            result.ImageE5.Coef[3] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (tempArray[0] + tempArray[1]);
            tempArray[3] = (tempArray[2] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[5]);
            result.ImageE5.Coef[5] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[2] * tempArray[3]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[5]);
            result.ImageE5.Coef[6] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[0]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[1]);
            result.ImageE3.Coef[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            result.ImageE3.Coef[5] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[4]);
            result.ImageE3.Coef[6] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[4] + tempArray[5]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[0] * tempArray[3]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[2] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[2]);
            result.ImageE7.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyVersor(geometry3d.e3d.Multivector v, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:11 م
            //Macro: geometry3d.e3d.ApplyVersor
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 264 sub-expressions, 0 generated temps, 264 total.
            //Target Temp Variables: 11 total.
            //Output Variables: 8 total.
            //Computations: 1.29411764705882 average, 352 total.
            //Memory Reads: 1.9375 average, 527 total.
            //Memory Writes: 272 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[11];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * mv.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[5] * mv.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[6] * mv.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * mv.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[5] * mv.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[4] * mv.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[7] * mv.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[6] * mv.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[2] * mv.Coef[0]);
            tempArray[4] = (v.Coef[3] * mv.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[0] * mv.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[1] * mv.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[6] * mv.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[7] * mv.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (v.Coef[4] * mv.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[5] * mv.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[7] * mv.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[6] * mv.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (v.Coef[5] * mv.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[0]);
            tempArray[6] = (v.Coef[5] * mv.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * mv.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[7] * mv.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * mv.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[1] * mv.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * mv.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[3] * mv.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[5] * mv.Coef[0]);
            tempArray[7] = (v.Coef[4] * mv.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[7] * mv.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[6] * mv.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[1] * mv.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[0] * mv.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[3] * mv.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[2] * mv.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[6] * mv.Coef[0]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[4] * mv.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[5] * mv.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[2] * mv.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[3] * mv.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[0] * mv.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[1] * mv.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[0]);
            tempArray[9] = (-1 * v.Coef[6] * mv.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * mv.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * mv.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * mv.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * mv.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * mv.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * mv.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = Math.Pow(v.Coef[0], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[10] = Math.Pow(v.Coef[1], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[2], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[3], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[4], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[5], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[6], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[7], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[9] = Math.Pow(tempArray[9], -1);
            result.Coef[0] = (tempArray[1] * tempArray[9]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[6] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[1] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[10] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[5] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[2] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[10] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[3] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            tempArray[10] = (v.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[3] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[4] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (v.Coef[5] * tempArray[0]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[5] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (v.Coef[6] * tempArray[0]);
            tempArray[10] = (v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[1] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[6] = (tempArray[9] * tempArray[1]);
            tempArray[0] = (v.Coef[7] * tempArray[0]);
            tempArray[1] = (v.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[7] = (tempArray[9] * tempArray[0]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyRotor(geometry3d.e3d.Multivector v, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:11 م
            //Macro: geometry3d.e3d.ApplyRotor
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 232 sub-expressions, 0 generated temps, 232 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.33333333333333 average, 320 total.
            //Memory Reads: 2 average, 480 total.
            //Memory Writes: 240 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * mv.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[5] * mv.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[6] * mv.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * mv.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[5] * mv.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[4] * mv.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[7] * mv.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[6] * mv.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[2] * mv.Coef[0]);
            tempArray[4] = (v.Coef[3] * mv.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[0] * mv.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[1] * mv.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[6] * mv.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[7] * mv.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (v.Coef[4] * mv.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[5] * mv.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[7] * mv.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[6] * mv.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (v.Coef[5] * mv.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[0]);
            tempArray[6] = (v.Coef[5] * mv.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * mv.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[7] * mv.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * mv.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[1] * mv.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * mv.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[3] * mv.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[5] * mv.Coef[0]);
            tempArray[7] = (v.Coef[4] * mv.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[7] * mv.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[6] * mv.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[1] * mv.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[0] * mv.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[3] * mv.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[2] * mv.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[6] * mv.Coef[0]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[4] * mv.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[5] * mv.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[2] * mv.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[3] * mv.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[0] * mv.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[1] * mv.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[0]);
            tempArray[9] = (-1 * v.Coef[6] * mv.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * mv.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * mv.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * mv.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * mv.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * mv.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * mv.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[8]);
            result.Coef[0] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[8]);
            result.Coef[1] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[9] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[8]);
            result.Coef[2] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[8]);
            result.Coef[3] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            tempArray[9] = (v.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[8]);
            result.Coef[4] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (v.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[8]);
            result.Coef[5] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (v.Coef[6] * tempArray[0]);
            tempArray[9] = (v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * tempArray[8]);
            result.Coef[6] = (tempArray[1] + tempArray[9]);
            tempArray[0] = (v.Coef[7] * tempArray[0]);
            tempArray[1] = (v.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[8]);
            result.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyReflector(geometry3d.e3d.Multivector v, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:11 م
            //Macro: geometry3d.e3d.ApplyReflector
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 240 sub-expressions, 0 generated temps, 240 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.32258064516129 average, 328 total.
            //Memory Reads: 1.96774193548387 average, 488 total.
            //Memory Writes: 248 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * mv.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[5] * mv.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[6] * mv.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * mv.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[5] * mv.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[4] * mv.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[7] * mv.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[6] * mv.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[2] * mv.Coef[0]);
            tempArray[4] = (v.Coef[3] * mv.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[0] * mv.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[1] * mv.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[6] * mv.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[7] * mv.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (v.Coef[4] * mv.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[5] * mv.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[7] * mv.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[6] * mv.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (v.Coef[5] * mv.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[0]);
            tempArray[6] = (v.Coef[5] * mv.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * mv.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[7] * mv.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * mv.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[1] * mv.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * mv.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[3] * mv.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[5] * mv.Coef[0]);
            tempArray[7] = (v.Coef[4] * mv.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[7] * mv.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[6] * mv.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[1] * mv.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[0] * mv.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[3] * mv.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[2] * mv.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[6] * mv.Coef[0]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[4] * mv.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[5] * mv.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[2] * mv.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[3] * mv.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[0] * mv.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[1] * mv.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[0]);
            tempArray[9] = (-1 * v.Coef[6] * mv.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * mv.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * mv.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * mv.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * mv.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * mv.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * mv.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[0] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[1] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[9] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[2] = (-1 * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[3] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            tempArray[9] = (v.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[4] = (-1 * tempArray[1]);
            tempArray[1] = (v.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[5] = (-1 * tempArray[1]);
            tempArray[1] = (v.Coef[6] * tempArray[0]);
            tempArray[9] = (v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[6] = (-1 * tempArray[1]);
            tempArray[0] = (v.Coef[7] * tempArray[0]);
            tempArray[1] = (v.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[7] = (-1 * tempArray[0]);
            
            return result;
        }
        
        public static geometry3d.e3d.OMStruct EVersorToOM(geometry3d.e3d.Multivector v)
        {
            var result = new geometry3d.e3d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.EVersorToOM
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 135 sub-expressions, 0 generated temps, 135 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 24 total.
            //Computations: 1.06289308176101 average, 169 total.
            //Memory Reads: 1.70440251572327 average, 271 total.
            //Memory Writes: 159 total.
            //
            //Macro Binding Data: 
            //    result.ImageV3.#E0# = <variable>
            //    result.ImageV3.#e1# = <variable>
            //    result.ImageV3.#e2# = <variable>
            //    result.ImageV3.#e1^e2# = <variable>
            //    result.ImageV3.#e3# = <variable>
            //    result.ImageV3.#e1^e3# = <variable>
            //    result.ImageV3.#e2^e3# = <variable>
            //    result.ImageV3.#e1^e2^e3# = <variable>
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV2.#e3# = <variable>
            //    result.ImageV2.#e1^e3# = <variable>
            //    result.ImageV2.#e2^e3# = <variable>
            //    result.ImageV2.#e1^e2^e3# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    result.ImageV1.#e3# = <variable>
            //    result.ImageV1.#e1^e3# = <variable>
            //    result.ImageV1.#e2^e3# = <variable>
            //    result.ImageV1.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[13];
            
            result.ImageV3.Coef[0] = 0;
            result.ImageV3.Coef[3] = 0;
            result.ImageV3.Coef[5] = 0;
            result.ImageV3.Coef[6] = 0;
            result.ImageV3.Coef[7] = 0;
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV2.Coef[5] = 0;
            result.ImageV2.Coef[6] = 0;
            result.ImageV2.Coef[7] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV1.Coef[5] = 0;
            result.ImageV1.Coef[6] = 0;
            result.ImageV1.Coef[7] = 0;
            tempArray[0] = Math.Pow(v.Coef[0], 2);
            tempArray[0] = (-1 * tempArray[0]);
            tempArray[1] = Math.Pow(v.Coef[1], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[2], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[3], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[4], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[5], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[6], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[7], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[4] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[5] = (v.Coef[4] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[5] * tempArray[0]);
            tempArray[7] = (-1 * v.Coef[6] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[8] = (v.Coef[4] * tempArray[2]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[4]);
            tempArray[10] = (v.Coef[1] * tempArray[5]);
            tempArray[11] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[12] = (v.Coef[5] * tempArray[1]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[3] = (v.Coef[7] * tempArray[3]);
            tempArray[3] = (tempArray[8] + tempArray[3]);
            tempArray[3] = (tempArray[9] + tempArray[3]);
            tempArray[3] = (tempArray[10] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[11] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[2] * tempArray[0]);
            result.ImageV3.Coef[1] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (v.Coef[4] * tempArray[3]);
            tempArray[6] = (v.Coef[5] * tempArray[4]);
            tempArray[8] = (v.Coef[2] * tempArray[5]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (v.Coef[6] * tempArray[1]);
            tempArray[2] = (-1 * v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[1] = (tempArray[6] + tempArray[1]);
            tempArray[1] = (tempArray[8] + tempArray[1]);
            tempArray[1] = (tempArray[9] + tempArray[1]);
            tempArray[2] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[0] = (v.Coef[1] * tempArray[0]);
            result.ImageV3.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[2] = (v.Coef[5] * tempArray[6]);
            tempArray[3] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[4] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[6] + tempArray[0]);
            result.ImageV3.Coef[4] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (v.Coef[2] * tempArray[2]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[2] = (v.Coef[6] * tempArray[6]);
            tempArray[3] = (v.Coef[5] * tempArray[7]);
            tempArray[4] = (v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[3] + tempArray[0]);
            tempArray[1] = (v.Coef[4] * tempArray[0]);
            result.ImageV2.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[4]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[2] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            result.ImageV2.Coef[2] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (-1 * v.Coef[6] * tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[1] = (v.Coef[0] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            result.ImageV2.Coef[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageV1.Coef[1] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            result.ImageV1.Coef[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[10]);
            tempArray[1] = (v.Coef[0] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[1] = (v.Coef[2] * tempArray[0]);
            result.ImageV1.Coef[4] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.LTStruct EVersorToLT(geometry3d.e3d.Multivector v)
        {
            var result = new geometry3d.e3d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.EVersorToLT
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 169 sub-expressions, 0 generated temps, 169 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 64 total.
            //Computations: 0.96137339055794 average, 224 total.
            //Memory Reads: 1.54077253218884 average, 359 total.
            //Memory Writes: 233 total.
            //
            //Macro Binding Data: 
            //    result.ImageE7.#E0# = <variable>
            //    result.ImageE7.#e1# = <variable>
            //    result.ImageE7.#e2# = <variable>
            //    result.ImageE7.#e1^e2# = <variable>
            //    result.ImageE7.#e3# = <variable>
            //    result.ImageE7.#e1^e3# = <variable>
            //    result.ImageE7.#e2^e3# = <variable>
            //    result.ImageE7.#e1^e2^e3# = <variable>
            //    result.ImageE6.#E0# = <variable>
            //    result.ImageE6.#e1# = <variable>
            //    result.ImageE6.#e2# = <variable>
            //    result.ImageE6.#e1^e2# = <variable>
            //    result.ImageE6.#e3# = <variable>
            //    result.ImageE6.#e1^e3# = <variable>
            //    result.ImageE6.#e2^e3# = <variable>
            //    result.ImageE6.#e1^e2^e3# = <variable>
            //    result.ImageE5.#E0# = <variable>
            //    result.ImageE5.#e1# = <variable>
            //    result.ImageE5.#e2# = <variable>
            //    result.ImageE5.#e1^e2# = <variable>
            //    result.ImageE5.#e3# = <variable>
            //    result.ImageE5.#e1^e3# = <variable>
            //    result.ImageE5.#e2^e3# = <variable>
            //    result.ImageE5.#e1^e2^e3# = <variable>
            //    result.ImageE4.#E0# = <variable>
            //    result.ImageE4.#e1# = <variable>
            //    result.ImageE4.#e2# = <variable>
            //    result.ImageE4.#e1^e2# = <variable>
            //    result.ImageE4.#e3# = <variable>
            //    result.ImageE4.#e1^e3# = <variable>
            //    result.ImageE4.#e2^e3# = <variable>
            //    result.ImageE4.#e1^e2^e3# = <variable>
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE3.#e3# = <variable>
            //    result.ImageE3.#e1^e3# = <variable>
            //    result.ImageE3.#e2^e3# = <variable>
            //    result.ImageE3.#e1^e2^e3# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE2.#e3# = <variable>
            //    result.ImageE2.#e1^e3# = <variable>
            //    result.ImageE2.#e2^e3# = <variable>
            //    result.ImageE2.#e1^e2^e3# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE1.#e3# = <variable>
            //    result.ImageE1.#e1^e3# = <variable>
            //    result.ImageE1.#e2^e3# = <variable>
            //    result.ImageE1.#e1^e2^e3# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    result.ImageE0.#e3# = <variable>
            //    result.ImageE0.#e1^e3# = <variable>
            //    result.ImageE0.#e2^e3# = <variable>
            //    result.ImageE0.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[13];
            
            result.ImageE7.Coef[0] = 0;
            result.ImageE7.Coef[1] = 0;
            result.ImageE7.Coef[2] = 0;
            result.ImageE7.Coef[3] = 0;
            result.ImageE7.Coef[4] = 0;
            result.ImageE7.Coef[5] = 0;
            result.ImageE7.Coef[6] = 0;
            result.ImageE6.Coef[0] = 0;
            result.ImageE6.Coef[1] = 0;
            result.ImageE6.Coef[2] = 0;
            result.ImageE6.Coef[4] = 0;
            result.ImageE6.Coef[7] = 0;
            result.ImageE5.Coef[0] = 0;
            result.ImageE5.Coef[1] = 0;
            result.ImageE5.Coef[2] = 0;
            result.ImageE5.Coef[4] = 0;
            result.ImageE5.Coef[7] = 0;
            result.ImageE4.Coef[0] = 0;
            result.ImageE4.Coef[3] = 0;
            result.ImageE4.Coef[5] = 0;
            result.ImageE4.Coef[6] = 0;
            result.ImageE4.Coef[7] = 0;
            result.ImageE3.Coef[0] = 0;
            result.ImageE3.Coef[1] = 0;
            result.ImageE3.Coef[2] = 0;
            result.ImageE3.Coef[4] = 0;
            result.ImageE3.Coef[7] = 0;
            result.ImageE2.Coef[0] = 0;
            result.ImageE2.Coef[3] = 0;
            result.ImageE2.Coef[5] = 0;
            result.ImageE2.Coef[6] = 0;
            result.ImageE2.Coef[7] = 0;
            result.ImageE1.Coef[0] = 0;
            result.ImageE1.Coef[3] = 0;
            result.ImageE1.Coef[5] = 0;
            result.ImageE1.Coef[6] = 0;
            result.ImageE1.Coef[7] = 0;
            result.ImageE0.Coef[0] = 1;
            result.ImageE0.Coef[1] = 0;
            result.ImageE0.Coef[2] = 0;
            result.ImageE0.Coef[3] = 0;
            result.ImageE0.Coef[4] = 0;
            result.ImageE0.Coef[5] = 0;
            result.ImageE0.Coef[6] = 0;
            result.ImageE0.Coef[7] = 0;
            tempArray[0] = Math.Pow(v.Coef[0], 2);
            tempArray[0] = (-1 * tempArray[0]);
            tempArray[1] = Math.Pow(v.Coef[1], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[2], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[3], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[4], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[5], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[6], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = Math.Pow(v.Coef[7], 2);
            tempArray[1] = (-1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[4] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[5] = (v.Coef[4] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[5] * tempArray[0]);
            tempArray[7] = (-1 * v.Coef[6] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[8] = (v.Coef[4] * tempArray[2]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[4]);
            tempArray[10] = (v.Coef[1] * tempArray[5]);
            tempArray[11] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[12] = (v.Coef[5] * tempArray[1]);
            tempArray[8] = (tempArray[8] + tempArray[12]);
            tempArray[3] = (v.Coef[7] * tempArray[3]);
            tempArray[3] = (tempArray[8] + tempArray[3]);
            tempArray[3] = (tempArray[9] + tempArray[3]);
            tempArray[3] = (tempArray[10] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[3] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[11] + tempArray[3]);
            tempArray[6] = (-1 * v.Coef[2] * tempArray[0]);
            result.ImageE4.Coef[1] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (v.Coef[4] * tempArray[3]);
            tempArray[6] = (v.Coef[5] * tempArray[4]);
            tempArray[8] = (v.Coef[2] * tempArray[5]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (v.Coef[6] * tempArray[1]);
            tempArray[2] = (-1 * v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (tempArray[3] + tempArray[1]);
            tempArray[1] = (tempArray[6] + tempArray[1]);
            tempArray[1] = (tempArray[8] + tempArray[1]);
            tempArray[1] = (tempArray[9] + tempArray[1]);
            tempArray[2] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[0] = (v.Coef[1] * tempArray[0]);
            result.ImageE4.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[2] = (v.Coef[5] * tempArray[6]);
            tempArray[3] = (-1 * v.Coef[7] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[4] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[6] + tempArray[0]);
            result.ImageE4.Coef[4] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (v.Coef[2] * tempArray[2]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[2] = (v.Coef[6] * tempArray[6]);
            tempArray[3] = (v.Coef[5] * tempArray[7]);
            tempArray[4] = (v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[3] + tempArray[0]);
            tempArray[1] = (v.Coef[4] * tempArray[0]);
            result.ImageE2.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[4]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[2] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            result.ImageE2.Coef[2] = (tempArray[3] + tempArray[0]);
            tempArray[0] = (-1 * v.Coef[6] * tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[1] = (v.Coef[0] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            result.ImageE2.Coef[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageE1.Coef[1] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            result.ImageE1.Coef[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[5] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[8]);
            tempArray[1] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[9]);
            tempArray[0] = (tempArray[0] + tempArray[10]);
            tempArray[1] = (v.Coef[0] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[1] = (v.Coef[2] * tempArray[0]);
            result.ImageE1.Coef[4] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (tempArray[3] + tempArray[0]);
            tempArray[2] = (tempArray[3] + tempArray[6]);
            tempArray[3] = (tempArray[1] + tempArray[0]);
            tempArray[4] = (tempArray[1] * tempArray[2]);
            tempArray[5] = (-1 * tempArray[0] * tempArray[3]);
            result.ImageE6.Coef[3] = (tempArray[4] + tempArray[5]);
            tempArray[4] = (tempArray[0] + tempArray[1]);
            tempArray[5] = (tempArray[3] + tempArray[0]);
            tempArray[2] = (tempArray[4] * tempArray[2]);
            tempArray[0] = (-1 * tempArray[0] * tempArray[5]);
            result.ImageE6.Coef[5] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[4] * tempArray[3]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[5]);
            result.ImageE6.Coef[6] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (tempArray[0] + tempArray[3]);
            tempArray[2] = (tempArray[0] * tempArray[2]);
            tempArray[3] = (-1 * tempArray[1] * tempArray[3]);
            result.ImageE5.Coef[3] = (tempArray[2] + tempArray[3]);
            tempArray[2] = (tempArray[0] + tempArray[1]);
            tempArray[3] = (tempArray[2] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[5]);
            result.ImageE5.Coef[5] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[2] * tempArray[3]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[5]);
            result.ImageE5.Coef[6] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[0]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[1]);
            result.ImageE3.Coef[3] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[0] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[1] * tempArray[4]);
            result.ImageE3.Coef[5] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[1] * tempArray[2]);
            tempArray[4] = (-1 * tempArray[0] * tempArray[4]);
            result.ImageE3.Coef[6] = (tempArray[3] + tempArray[4]);
            tempArray[3] = (tempArray[4] + tempArray[5]);
            tempArray[2] = (-1 * tempArray[2] * tempArray[3]);
            tempArray[3] = (tempArray[2] + tempArray[0]);
            tempArray[0] = (tempArray[0] * tempArray[3]);
            tempArray[0] = (tempArray[2] + tempArray[0]);
            tempArray[2] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[2]);
            result.ImageE7.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyEVersor(geometry3d.e3d.Multivector v, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.ApplyEVersor
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 264 sub-expressions, 0 generated temps, 264 total.
            //Target Temp Variables: 11 total.
            //Output Variables: 8 total.
            //Computations: 1.29411764705882 average, 352 total.
            //Memory Reads: 1.9375 average, 527 total.
            //Memory Writes: 272 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[11];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * mv.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[5] * mv.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[6] * mv.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * mv.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[5] * mv.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[4] * mv.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[7] * mv.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[6] * mv.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[2] * mv.Coef[0]);
            tempArray[4] = (v.Coef[3] * mv.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[0] * mv.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[1] * mv.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[6] * mv.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[7] * mv.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (v.Coef[4] * mv.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[5] * mv.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[7] * mv.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[6] * mv.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (v.Coef[5] * mv.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[0]);
            tempArray[6] = (v.Coef[5] * mv.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * mv.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[7] * mv.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * mv.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[1] * mv.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * mv.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[3] * mv.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[5] * mv.Coef[0]);
            tempArray[7] = (v.Coef[4] * mv.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[7] * mv.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[6] * mv.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[1] * mv.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[0] * mv.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[3] * mv.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[2] * mv.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[6] * mv.Coef[0]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[4] * mv.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[5] * mv.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[2] * mv.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[3] * mv.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[0] * mv.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[1] * mv.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[0]);
            tempArray[9] = (-1 * v.Coef[6] * mv.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * mv.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * mv.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * mv.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * mv.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * mv.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * mv.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = Math.Pow(v.Coef[0], 2);
            tempArray[9] = (-1 * tempArray[9]);
            tempArray[10] = Math.Pow(v.Coef[1], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[2], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[3], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[4], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[5], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[6], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = Math.Pow(v.Coef[7], 2);
            tempArray[10] = (-1 * tempArray[10]);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[9] = Math.Pow(tempArray[9], -1);
            result.Coef[0] = (tempArray[1] * tempArray[9]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[6] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[1] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[10] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[5] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[2] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[10] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[3] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            tempArray[10] = (v.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[3] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[4] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (v.Coef[5] * tempArray[0]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[5] = (tempArray[9] * tempArray[1]);
            tempArray[1] = (v.Coef[6] * tempArray[0]);
            tempArray[10] = (v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            tempArray[10] = (-1 * v.Coef[1] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[10]);
            result.Coef[6] = (tempArray[9] * tempArray[1]);
            tempArray[0] = (v.Coef[7] * tempArray[0]);
            tempArray[1] = (v.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[7] = (tempArray[9] * tempArray[0]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyERotor(geometry3d.e3d.Multivector v, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.ApplyERotor
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 232 sub-expressions, 0 generated temps, 232 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.33333333333333 average, 320 total.
            //Memory Reads: 2 average, 480 total.
            //Memory Writes: 240 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * mv.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[5] * mv.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[6] * mv.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * mv.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[5] * mv.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[4] * mv.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[7] * mv.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[6] * mv.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[2] * mv.Coef[0]);
            tempArray[4] = (v.Coef[3] * mv.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[0] * mv.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[1] * mv.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[6] * mv.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[7] * mv.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (v.Coef[4] * mv.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[5] * mv.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[7] * mv.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[6] * mv.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (v.Coef[5] * mv.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[0]);
            tempArray[6] = (v.Coef[5] * mv.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * mv.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[7] * mv.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * mv.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[1] * mv.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * mv.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[3] * mv.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[5] * mv.Coef[0]);
            tempArray[7] = (v.Coef[4] * mv.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[7] * mv.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[6] * mv.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[1] * mv.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[0] * mv.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[3] * mv.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[2] * mv.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[6] * mv.Coef[0]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[4] * mv.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[5] * mv.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[2] * mv.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[3] * mv.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[0] * mv.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[1] * mv.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[0]);
            tempArray[9] = (-1 * v.Coef[6] * mv.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * mv.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * mv.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * mv.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * mv.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * mv.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * mv.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[8]);
            result.Coef[0] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[8]);
            result.Coef[1] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[9] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[8]);
            result.Coef[2] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[8]);
            result.Coef[3] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            tempArray[9] = (v.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[8]);
            result.Coef[4] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (v.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[8]);
            result.Coef[5] = (tempArray[1] + tempArray[9]);
            tempArray[1] = (v.Coef[6] * tempArray[0]);
            tempArray[9] = (v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * tempArray[8]);
            result.Coef[6] = (tempArray[1] + tempArray[9]);
            tempArray[0] = (v.Coef[7] * tempArray[0]);
            tempArray[1] = (v.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[8]);
            result.Coef[7] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector ApplyEReflector(geometry3d.e3d.Multivector v, geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.ApplyEReflector
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 240 sub-expressions, 0 generated temps, 240 total.
            //Target Temp Variables: 10 total.
            //Output Variables: 8 total.
            //Computations: 1.32258064516129 average, 328 total.
            //Memory Reads: 1.96774193548387 average, 488 total.
            //Memory Writes: 248 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    v.#e3# = <variable>
            //    v.#e1^e3# = <variable>
            //    v.#e2^e3# = <variable>
            //    v.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double[] tempArray = new double[10];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * mv.Coef[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[5] * mv.Coef[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[6] * mv.Coef[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[7] * mv.Coef[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[5] * mv.Coef[4]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[4] * mv.Coef[5]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[7] * mv.Coef[6]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[6] * mv.Coef[7]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[2] * mv.Coef[0]);
            tempArray[4] = (v.Coef[3] * mv.Coef[1]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[0] * mv.Coef[2]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[1] * mv.Coef[3]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[6] * mv.Coef[4]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[7] * mv.Coef[5]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (v.Coef[4] * mv.Coef[6]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[5] * mv.Coef[7]);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[7] * mv.Coef[4]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[6] * mv.Coef[5]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (v.Coef[5] * mv.Coef[6]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[7]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[4] * mv.Coef[0]);
            tempArray[6] = (v.Coef[5] * mv.Coef[1]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[6] * mv.Coef[2]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[7] * mv.Coef[3]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * mv.Coef[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[1] * mv.Coef[5]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * mv.Coef[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (v.Coef[3] * mv.Coef[7]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[4] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[5] * mv.Coef[0]);
            tempArray[7] = (v.Coef[4] * mv.Coef[1]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[7] * mv.Coef[2]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[6] * mv.Coef[3]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[1] * mv.Coef[4]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[0] * mv.Coef[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[3] * mv.Coef[6]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (v.Coef[2] * mv.Coef[7]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[5] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * v.Coef[6] * mv.Coef[0]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[1]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[4] * mv.Coef[2]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[5] * mv.Coef[3]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[2] * mv.Coef[4]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (v.Coef[3] * mv.Coef[5]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[0] * mv.Coef[6]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[1] * mv.Coef[7]);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[6] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[8]);
            tempArray[8] = (-1 * v.Coef[7] * mv.Coef[0]);
            tempArray[9] = (-1 * v.Coef[6] * mv.Coef[1]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * mv.Coef[2]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * mv.Coef[3]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * mv.Coef[4]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * mv.Coef[5]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * mv.Coef[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * mv.Coef[7]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[0] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[1] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[9] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[2] = (-1 * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[7] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[6] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[5] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[3] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[0]);
            tempArray[9] = (v.Coef[5] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[4] = (-1 * tempArray[1]);
            tempArray[1] = (v.Coef[5] * tempArray[0]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[7] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[6] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[1] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[3] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[5] = (-1 * tempArray[1]);
            tempArray[1] = (v.Coef[6] * tempArray[0]);
            tempArray[9] = (v.Coef[7] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[4] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[5] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[2] * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (v.Coef[3] * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[0] * tempArray[7]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            tempArray[9] = (-1 * v.Coef[1] * tempArray[8]);
            tempArray[1] = (tempArray[1] + tempArray[9]);
            result.Coef[6] = (-1 * tempArray[1]);
            tempArray[0] = (v.Coef[7] * tempArray[0]);
            tempArray[1] = (v.Coef[6] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[5] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[4] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[2] * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[8]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[7] = (-1 * tempArray[0]);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector versorInverse(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.versorInverse
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.125 average, 36 total.
            //Memory Reads: 1.46875 average, 47 total.
            //Memory Writes: 32 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(tempVar0000, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (-1 * mv.Coef[3] * tempVar0000);
            result.Coef[4] = (mv.Coef[4] * tempVar0000);
            result.Coef[5] = (-1 * mv.Coef[5] * tempVar0000);
            result.Coef[6] = (-1 * mv.Coef[6] * tempVar0000);
            result.Coef[7] = (-1 * mv.Coef[7] * tempVar0000);
            
            return result;
        }
        
        public static geometry3d.e3d.Multivector normalize(geometry3d.e3d.Multivector mv)
        {
            var result = new geometry3d.e3d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 10:00:12 م
            //Macro: geometry3d.e3d.normalize
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 32 total.
            //Memory Reads: 1.46875 average, 47 total.
            //Memory Writes: 32 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    result.#e3# = <variable>
            //    result.#e1^e3# = <variable>
            //    result.#e2^e3# = <variable>
            //    result.#e1^e2^e3# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    mv.#e3# = <variable>
            //    mv.#e1^e3# = <variable>
            //    mv.#e2^e3# = <variable>
            //    mv.#e1^e2^e3# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[4], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[5], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[6], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[7], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0000 = Math.Pow(tempVar0000, -0.5);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (mv.Coef[3] * tempVar0000);
            result.Coef[4] = (mv.Coef[4] * tempVar0000);
            result.Coef[5] = (mv.Coef[5] * tempVar0000);
            result.Coef[6] = (mv.Coef[6] * tempVar0000);
            result.Coef[7] = (mv.Coef[7] * tempVar0000);
            
            return result;
        }
        
    }
}
