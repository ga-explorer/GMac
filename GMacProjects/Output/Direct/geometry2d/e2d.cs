using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geometry2d
{
    public static class e2d
    {
        public sealed class Multivector
        {
            public readonly double[] Coef = new double[4];
            
            
            public Multivector()
            {
            }
            
            public Multivector(params double[] coefs)
            {
                int i = 0;
                foreach (var coef in coefs.Take(4))
                    Coef[i++] = coef;
            }
            
            public Multivector(IEnumerable<double> coefs)
            {
                int i = 0;
                foreach (var coef in coefs.Take(4))
                    Coef[i++] = coef;
            }
            
            
        }
        
        public static readonly geometry2d.e2d.Multivector I = new geometry2d.e2d.Multivector(0, 0, 0, 1);
        
        public static readonly geometry2d.e2d.Multivector E0 = new geometry2d.e2d.Multivector(1, 0, 0, 0);
        
        public static readonly geometry2d.e2d.Multivector E1 = new geometry2d.e2d.Multivector(0, 1, 0, 0);
        
        public static readonly geometry2d.e2d.Multivector E2 = new geometry2d.e2d.Multivector(0, 0, 1, 0);
        
        public static readonly geometry2d.e2d.Multivector E3 = new geometry2d.e2d.Multivector(0, 0, 0, 1);
        
        public static double Mag(geometry2d.e2d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.Mag
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 0.769230769230769 average, 10 total.
            //Memory Reads: 1.23076923076923 average, 16 total.
            //Memory Writes: 13 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            tempVar0001 = Math.Abs(tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Abs(mv.Coef[3]);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result = Math.Pow(tempVar0000, 0.5);
            
            return result;
        }
        
        public static double Mag2(geometry2d.e2d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.Mag2
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 11 sub-expressions, 0 generated temps, 11 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 0.75 average, 9 total.
            //Memory Reads: 1.25 average, 15 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            tempVar0001 = Math.Abs(tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Abs(mv.Coef[3]);
            tempVar0001 = Math.Pow(tempVar0001, 2);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double Norm2(geometry2d.e2d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.Norm2
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 11 total.
            //Memory Reads: 1.27272727272727 average, 14 total.
            //Memory Writes: 11 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector InvVersor(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.InvVersor
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.0625 average, 17 total.
            //Memory Reads: 1.4375 average, 23 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            tempVar0000 = Math.Pow(tempVar0000, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (-1 * mv.Coef[3] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector Dual(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.Dual
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 0.5 average, 2 total.
            //Memory Reads: 1 average, 4 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            
            result.Coef[0] = mv.Coef[3];
            result.Coef[1] = mv.Coef[2];
            result.Coef[2] = (-1 * mv.Coef[1]);
            result.Coef[3] = (-1 * mv.Coef[0]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector SelfGP(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.SelfGP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 9 sub-expressions, 0 generated temps, 9 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.23076923076923 average, 16 total.
            //Memory Reads: 1.46153846153846 average, 19 total.
            //Memory Writes: 13 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[1] = (-2 * mv.Coef[0] * mv.Coef[1]);
            result.Coef[2] = (-2 * mv.Coef[0] * mv.Coef[2]);
            result.Coef[3] = (-2 * mv.Coef[0] * mv.Coef[3]);
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector SelfGPRev(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.SelfGPRev
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.16666666666667 average, 21 total.
            //Memory Reads: 1.44444444444444 average, 26 total.
            //Memory Writes: 18 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = 0;
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[1]);
            tempVar0001 = (-2 * mv.Coef[2] * mv.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[2]);
            tempVar0001 = (2 * mv.Coef[1] * mv.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
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
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector Negative(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.Negative
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 1 average, 4 total.
            //Memory Reads: 1 average, 4 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            
            result.Coef[0] = (-1 * mv.Coef[0]);
            result.Coef[1] = (-1 * mv.Coef[1]);
            result.Coef[2] = (-1 * mv.Coef[2]);
            result.Coef[3] = (-1 * mv.Coef[3]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector Reverse(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.Reverse
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 0.25 average, 1 total.
            //Memory Reads: 1 average, 4 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            
            result.Coef[0] = mv.Coef[0];
            result.Coef[1] = mv.Coef[1];
            result.Coef[2] = mv.Coef[2];
            result.Coef[3] = (-1 * mv.Coef[3]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector GradeInv(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.GradeInv
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 0.5 average, 2 total.
            //Memory Reads: 1 average, 4 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            
            result.Coef[0] = mv.Coef[0];
            result.Coef[1] = (-1 * mv.Coef[1]);
            result.Coef[2] = (-1 * mv.Coef[2]);
            result.Coef[3] = mv.Coef[3];
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector CliffConj(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: geometry2d.e2d.CliffConj
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 0.75 average, 3 total.
            //Memory Reads: 1 average, 4 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            
            result.Coef[0] = mv.Coef[0];
            result.Coef[1] = (-1 * mv.Coef[1]);
            result.Coef[2] = (-1 * mv.Coef[2]);
            result.Coef[3] = (-1 * mv.Coef[3]);
            
            return result;
        }
        
        public static double EMag(geometry2d.e2d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EMag
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 11 sub-expressions, 0 generated temps, 11 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 12 total.
            //Memory Reads: 1.25 average, 15 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            result = Math.Pow(tempVar0000, 0.5);
            
            return result;
        }
        
        public static double EMag2(geometry2d.e2d.Multivector mv)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EMag2
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 11 total.
            //Memory Reads: 1.27272727272727 average, 14 total.
            //Memory Writes: 11 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector InvEVersor(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.InvEVersor
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.0625 average, 17 total.
            //Memory Reads: 1.4375 average, 23 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            tempVar0000 = Math.Pow(tempVar0000, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (-1 * mv.Coef[3] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EDual(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EDual
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 0.5 average, 2 total.
            //Memory Reads: 1 average, 4 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            
            result.Coef[0] = mv.Coef[3];
            result.Coef[1] = mv.Coef[2];
            result.Coef[2] = (-1 * mv.Coef[1]);
            result.Coef[3] = (-1 * mv.Coef[0]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector SelfEGP(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.SelfEGP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 9 sub-expressions, 0 generated temps, 9 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.23076923076923 average, 16 total.
            //Memory Reads: 1.46153846153846 average, 19 total.
            //Memory Writes: 13 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[1] = (-2 * mv.Coef[0] * mv.Coef[1]);
            result.Coef[2] = (-2 * mv.Coef[0] * mv.Coef[2]);
            result.Coef[3] = (-2 * mv.Coef[0] * mv.Coef[3]);
            tempVar0000 = Math.Pow(mv.Coef[0], 2);
            tempVar0000 = (-1 * tempVar0000);
            tempVar0001 = Math.Pow(mv.Coef[1], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[2], 2);
            tempVar0001 = (-1 * tempVar0001);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = Math.Pow(mv.Coef[3], 2);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector SelfEGPRev(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.SelfEGPRev
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 14 sub-expressions, 0 generated temps, 14 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.16666666666667 average, 21 total.
            //Memory Reads: 1.44444444444444 average, 26 total.
            //Memory Writes: 18 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = 0;
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[1]);
            tempVar0001 = (-2 * mv.Coef[2] * mv.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-2 * mv.Coef[0] * mv.Coef[2]);
            tempVar0001 = (2 * mv.Coef[1] * mv.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
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
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double SP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.SP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.42857142857143 average, 10 total.
            //Memory Reads: 2 average, 14 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector GP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.GP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.42857142857143 average, 40 total.
            //Memory Reads: 2 average, 56 total.
            //Memory Writes: 28 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector LCP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.LCP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.5 average, 21 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector RCP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.RCP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.5 average, 21 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector FDP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.FDP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.45833333333333 average, 35 total.
            //Memory Reads: 2 average, 48 total.
            //Memory Writes: 24 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector HIP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.HIP
            //Input Variables: 6 used, 2 not used, 8 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.25 average, 15 total.
            //Memory Reads: 1.83333333333333 average, 22 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = 0;
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector CP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.CP
            //Input Variables: 6 used, 2 not used, 8 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.2 average, 12 total.
            //Memory Reads: 1.8 average, 18 total.
            //Memory Writes: 10 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = 0;
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ACP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.ACP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.5625 average, 25 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector GPDual(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.GPDual
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 26 sub-expressions, 0 generated temps, 26 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.4 average, 42 total.
            //Memory Reads: 1.93333333333333 average, 58 total.
            //Memory Writes: 30 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[3] = (-1 * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector DWP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.DWP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 52 sub-expressions, 0 generated temps, 52 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.42857142857143 average, 80 total.
            //Memory Reads: 2 average, 112 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
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
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            result.Coef[0] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[2] * tempArray[4]);
            result.Coef[1] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[5] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * tempArray[4]);
            result.Coef[2] = (tempArray[1] + tempArray[5]);
            tempArray[0] = (mv1.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[4]);
            result.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector GWP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.GWP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 52 sub-expressions, 0 generated temps, 52 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.35714285714286 average, 76 total.
            //Memory Reads: 2 average, 112 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
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
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            result.Coef[0] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[2] * tempArray[4]);
            result.Coef[1] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[5] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * tempArray[4]);
            result.Coef[2] = (tempArray[1] + tempArray[5]);
            tempArray[0] = (mv1.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[4]);
            result.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector Times(geometry2d.e2d.Multivector mv, double s)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.Times
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 4 total.
            //Computations: 1 average, 4 total.
            //Memory Reads: 2 average, 8 total.
            //Memory Writes: 4 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    s = <variable>
            
            
            result.Coef[0] = (mv.Coef[0] * s);
            result.Coef[1] = (mv.Coef[1] * s);
            result.Coef[2] = (mv.Coef[2] * s);
            result.Coef[3] = (mv.Coef[3] * s);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector Divide(geometry2d.e2d.Multivector mv, double s)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.Divide
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 4 total.
            //Computations: 1 average, 5 total.
            //Memory Reads: 1.8 average, 9 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            //    s = <variable>
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(s, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (mv.Coef[3] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector OP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.OP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.57142857142857 average, 22 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double ESP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.ESP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.42857142857143 average, 10 total.
            //Memory Reads: 2 average, 14 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EGP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EGP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.42857142857143 average, 40 total.
            //Memory Reads: 2 average, 56 total.
            //Memory Writes: 28 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ELCP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.ELCP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.5 average, 21 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ERCP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.ERCP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.5 average, 21 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EFDP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EFDP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.45833333333333 average, 35 total.
            //Memory Reads: 2 average, 48 total.
            //Memory Writes: 24 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EHIP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EHIP
            //Input Variables: 6 used, 2 not used, 8 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.25 average, 15 total.
            //Memory Reads: 1.83333333333333 average, 22 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[3] = 0;
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ECP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.ECP
            //Input Variables: 6 used, 2 not used, 8 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.2 average, 12 total.
            //Memory Reads: 1.8 average, 18 total.
            //Memory Writes: 10 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = 0;
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EACP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EACP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.5625 average, 25 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EGPDual(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EGPDual
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 26 sub-expressions, 0 generated temps, 26 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.4 average, 42 total.
            //Memory Reads: 1.93333333333333 average, 58 total.
            //Memory Writes: 30 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[0]);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[2] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[2] = (-1 * tempVar0000);
            tempVar0000 = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempVar0001 = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (mv1.Coef[3] * mv2.Coef[3]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[3] = (-1 * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EDWP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EDWP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 52 sub-expressions, 0 generated temps, 52 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.42857142857143 average, 80 total.
            //Memory Reads: 2 average, 112 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (-1 * mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (-1 * mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
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
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            result.Coef[0] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[2] * tempArray[4]);
            result.Coef[1] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[5] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * tempArray[4]);
            result.Coef[2] = (tempArray[1] + tempArray[5]);
            tempArray[0] = (mv1.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[4]);
            result.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector EGWP(geometry2d.e2d.Multivector mv1, geometry2d.e2d.Multivector mv2)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.EGWP
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 52 sub-expressions, 0 generated temps, 52 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.35714285714286 average, 76 total.
            //Memory Reads: 2 average, 112 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv1.#E0# = <variable>
            //    mv1.#e1# = <variable>
            //    mv1.#e2# = <variable>
            //    mv1.#e1^e2# = <variable>
            //    mv2.#E0# = <variable>
            //    mv2.#e1# = <variable>
            //    mv2.#e2# = <variable>
            //    mv2.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * mv1.Coef[0] * mv2.Coef[0]);
            tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[2] * mv2.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[3] * mv2.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * mv1.Coef[1] * mv2.Coef[0]);
            tempArray[3] = (mv1.Coef[0] * mv2.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[3] * mv2.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (mv1.Coef[2] * mv2.Coef[3]);
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
            tempArray[4] = (-1 * mv1.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * mv1.Coef[3] * mv2.Coef[0]);
            tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * mv2.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * mv2.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[4]);
            result.Coef[0] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[2] * tempArray[4]);
            result.Coef[1] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[0]);
            tempArray[5] = (mv1.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * mv1.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (mv1.Coef[1] * tempArray[4]);
            result.Coef[2] = (tempArray[1] + tempArray[5]);
            tempArray[0] = (mv1.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * mv1.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (mv1.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * mv1.Coef[0] * tempArray[4]);
            result.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyLT(geometry2d.e2d.LTStruct tr, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.ApplyLT
            //Input Variables: 20 used, 0 not used, 20 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1 average, 28 total.
            //Memory Reads: 2 average, 56 total.
            //Memory Writes: 28 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (tr.ImageE0.Coef[0] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[0] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[0] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[0] * mv.Coef[3]);
            result.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[1] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[1] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[1] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[1] * mv.Coef[3]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[2] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[2] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[2] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[2] * mv.Coef[3]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr.ImageE0.Coef[3] * mv.Coef[0]);
            tempVar0001 = (tr.ImageE1.Coef[3] * mv.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE2.Coef[3] * mv.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr.ImageE3.Coef[3] * mv.Coef[3]);
            result.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct TransLT(geometry2d.e2d.LTStruct tr)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:38 م
            //Macro: geometry2d.e2d.TransLT
            //Input Variables: 16 used, 0 not used, 16 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 16 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 16 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            
            
            result.ImageE3.Coef[0] = tr.ImageE0.Coef[3];
            result.ImageE3.Coef[1] = tr.ImageE1.Coef[3];
            result.ImageE3.Coef[2] = tr.ImageE2.Coef[3];
            result.ImageE3.Coef[3] = tr.ImageE3.Coef[3];
            result.ImageE2.Coef[0] = tr.ImageE0.Coef[2];
            result.ImageE2.Coef[1] = tr.ImageE1.Coef[2];
            result.ImageE2.Coef[2] = tr.ImageE2.Coef[2];
            result.ImageE2.Coef[3] = tr.ImageE3.Coef[2];
            result.ImageE1.Coef[0] = tr.ImageE0.Coef[1];
            result.ImageE1.Coef[1] = tr.ImageE1.Coef[1];
            result.ImageE1.Coef[2] = tr.ImageE2.Coef[1];
            result.ImageE1.Coef[3] = tr.ImageE3.Coef[1];
            result.ImageE0.Coef[0] = tr.ImageE0.Coef[0];
            result.ImageE0.Coef[1] = tr.ImageE1.Coef[0];
            result.ImageE0.Coef[2] = tr.ImageE2.Coef[0];
            result.ImageE0.Coef[3] = tr.ImageE3.Coef[0];
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct ComposeLT(geometry2d.e2d.LTStruct tr1, geometry2d.e2d.LTStruct tr2)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:39 م
            //Macro: geometry2d.e2d.ComposeLT
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 96 sub-expressions, 0 generated temps, 96 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 112 total.
            //Memory Reads: 2 average, 224 total.
            //Memory Writes: 112 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    tr1.ImageE3.#E0# = <variable>
            //    tr1.ImageE3.#e1# = <variable>
            //    tr1.ImageE3.#e2# = <variable>
            //    tr1.ImageE3.#e1^e2# = <variable>
            //    tr1.ImageE2.#E0# = <variable>
            //    tr1.ImageE2.#e1# = <variable>
            //    tr1.ImageE2.#e2# = <variable>
            //    tr1.ImageE2.#e1^e2# = <variable>
            //    tr1.ImageE1.#E0# = <variable>
            //    tr1.ImageE1.#e1# = <variable>
            //    tr1.ImageE1.#e2# = <variable>
            //    tr1.ImageE1.#e1^e2# = <variable>
            //    tr1.ImageE0.#E0# = <variable>
            //    tr1.ImageE0.#e1# = <variable>
            //    tr1.ImageE0.#e2# = <variable>
            //    tr1.ImageE0.#e1^e2# = <variable>
            //    tr2.ImageE3.#E0# = <variable>
            //    tr2.ImageE3.#e1# = <variable>
            //    tr2.ImageE3.#e2# = <variable>
            //    tr2.ImageE3.#e1^e2# = <variable>
            //    tr2.ImageE2.#E0# = <variable>
            //    tr2.ImageE2.#e1# = <variable>
            //    tr2.ImageE2.#e2# = <variable>
            //    tr2.ImageE2.#e1^e2# = <variable>
            //    tr2.ImageE1.#E0# = <variable>
            //    tr2.ImageE1.#e1# = <variable>
            //    tr2.ImageE1.#e2# = <variable>
            //    tr2.ImageE1.#e1^e2# = <variable>
            //    tr2.ImageE0.#E0# = <variable>
            //    tr2.ImageE0.#e1# = <variable>
            //    tr2.ImageE0.#e2# = <variable>
            //    tr2.ImageE0.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE3.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE3.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE3.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE2.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE2.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE2.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE1.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE1.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[3] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[0] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[0] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[0] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[0] * tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[0] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[1] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[1] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[1] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[1] * tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[2] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[2] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[2] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[2] * tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (tr1.ImageE0.Coef[3] * tr2.ImageE0.Coef[0]);
            tempVar0001 = (tr1.ImageE1.Coef[3] * tr2.ImageE0.Coef[1]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE2.Coef[3] * tr2.ImageE0.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (tr1.ImageE3.Coef[3] * tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct AddLT(geometry2d.e2d.LTStruct tr1, geometry2d.e2d.LTStruct tr2)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:39 م
            //Macro: geometry2d.e2d.AddLT
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 16 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    tr1.ImageE3.#E0# = <variable>
            //    tr1.ImageE3.#e1# = <variable>
            //    tr1.ImageE3.#e2# = <variable>
            //    tr1.ImageE3.#e1^e2# = <variable>
            //    tr1.ImageE2.#E0# = <variable>
            //    tr1.ImageE2.#e1# = <variable>
            //    tr1.ImageE2.#e2# = <variable>
            //    tr1.ImageE2.#e1^e2# = <variable>
            //    tr1.ImageE1.#E0# = <variable>
            //    tr1.ImageE1.#e1# = <variable>
            //    tr1.ImageE1.#e2# = <variable>
            //    tr1.ImageE1.#e1^e2# = <variable>
            //    tr1.ImageE0.#E0# = <variable>
            //    tr1.ImageE0.#e1# = <variable>
            //    tr1.ImageE0.#e2# = <variable>
            //    tr1.ImageE0.#e1^e2# = <variable>
            //    tr2.ImageE3.#E0# = <variable>
            //    tr2.ImageE3.#e1# = <variable>
            //    tr2.ImageE3.#e2# = <variable>
            //    tr2.ImageE3.#e1^e2# = <variable>
            //    tr2.ImageE2.#E0# = <variable>
            //    tr2.ImageE2.#e1# = <variable>
            //    tr2.ImageE2.#e2# = <variable>
            //    tr2.ImageE2.#e1^e2# = <variable>
            //    tr2.ImageE1.#E0# = <variable>
            //    tr2.ImageE1.#e1# = <variable>
            //    tr2.ImageE1.#e2# = <variable>
            //    tr2.ImageE1.#e1^e2# = <variable>
            //    tr2.ImageE0.#E0# = <variable>
            //    tr2.ImageE0.#e1# = <variable>
            //    tr2.ImageE0.#e2# = <variable>
            //    tr2.ImageE0.#e1^e2# = <variable>
            
            
            result.ImageE3.Coef[0] = (tr1.ImageE3.Coef[0] + tr2.ImageE3.Coef[0]);
            result.ImageE3.Coef[1] = (tr1.ImageE3.Coef[1] + tr2.ImageE3.Coef[1]);
            result.ImageE3.Coef[2] = (tr1.ImageE3.Coef[2] + tr2.ImageE3.Coef[2]);
            result.ImageE3.Coef[3] = (tr1.ImageE3.Coef[3] + tr2.ImageE3.Coef[3]);
            result.ImageE2.Coef[0] = (tr1.ImageE2.Coef[0] + tr2.ImageE2.Coef[0]);
            result.ImageE2.Coef[1] = (tr1.ImageE2.Coef[1] + tr2.ImageE2.Coef[1]);
            result.ImageE2.Coef[2] = (tr1.ImageE2.Coef[2] + tr2.ImageE2.Coef[2]);
            result.ImageE2.Coef[3] = (tr1.ImageE2.Coef[3] + tr2.ImageE2.Coef[3]);
            result.ImageE1.Coef[0] = (tr1.ImageE1.Coef[0] + tr2.ImageE1.Coef[0]);
            result.ImageE1.Coef[1] = (tr1.ImageE1.Coef[1] + tr2.ImageE1.Coef[1]);
            result.ImageE1.Coef[2] = (tr1.ImageE1.Coef[2] + tr2.ImageE1.Coef[2]);
            result.ImageE1.Coef[3] = (tr1.ImageE1.Coef[3] + tr2.ImageE1.Coef[3]);
            result.ImageE0.Coef[0] = (tr1.ImageE0.Coef[0] + tr2.ImageE0.Coef[0]);
            result.ImageE0.Coef[1] = (tr1.ImageE0.Coef[1] + tr2.ImageE0.Coef[1]);
            result.ImageE0.Coef[2] = (tr1.ImageE0.Coef[2] + tr2.ImageE0.Coef[2]);
            result.ImageE0.Coef[3] = (tr1.ImageE0.Coef[3] + tr2.ImageE0.Coef[3]);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct SubtractLT(geometry2d.e2d.LTStruct tr1, geometry2d.e2d.LTStruct tr2)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.SubtractLT
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 16 sub-expressions, 0 generated temps, 16 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 32 total.
            //Memory Reads: 1.5 average, 48 total.
            //Memory Writes: 32 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    tr1.ImageE3.#E0# = <variable>
            //    tr1.ImageE3.#e1# = <variable>
            //    tr1.ImageE3.#e2# = <variable>
            //    tr1.ImageE3.#e1^e2# = <variable>
            //    tr1.ImageE2.#E0# = <variable>
            //    tr1.ImageE2.#e1# = <variable>
            //    tr1.ImageE2.#e2# = <variable>
            //    tr1.ImageE2.#e1^e2# = <variable>
            //    tr1.ImageE1.#E0# = <variable>
            //    tr1.ImageE1.#e1# = <variable>
            //    tr1.ImageE1.#e2# = <variable>
            //    tr1.ImageE1.#e1^e2# = <variable>
            //    tr1.ImageE0.#E0# = <variable>
            //    tr1.ImageE0.#e1# = <variable>
            //    tr1.ImageE0.#e2# = <variable>
            //    tr1.ImageE0.#e1^e2# = <variable>
            //    tr2.ImageE3.#E0# = <variable>
            //    tr2.ImageE3.#e1# = <variable>
            //    tr2.ImageE3.#e2# = <variable>
            //    tr2.ImageE3.#e1^e2# = <variable>
            //    tr2.ImageE2.#E0# = <variable>
            //    tr2.ImageE2.#e1# = <variable>
            //    tr2.ImageE2.#e2# = <variable>
            //    tr2.ImageE2.#e1^e2# = <variable>
            //    tr2.ImageE1.#E0# = <variable>
            //    tr2.ImageE1.#e1# = <variable>
            //    tr2.ImageE1.#e2# = <variable>
            //    tr2.ImageE1.#e1^e2# = <variable>
            //    tr2.ImageE0.#E0# = <variable>
            //    tr2.ImageE0.#e1# = <variable>
            //    tr2.ImageE0.#e2# = <variable>
            //    tr2.ImageE0.#e1^e2# = <variable>
            
            double tempVar0000;
            
            tempVar0000 = (-1 * tr2.ImageE3.Coef[0]);
            result.ImageE3.Coef[0] = (tr1.ImageE3.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[1]);
            result.ImageE3.Coef[1] = (tr1.ImageE3.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[2]);
            result.ImageE3.Coef[2] = (tr1.ImageE3.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE3.Coef[3]);
            result.ImageE3.Coef[3] = (tr1.ImageE3.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[0]);
            result.ImageE2.Coef[0] = (tr1.ImageE2.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[1]);
            result.ImageE2.Coef[1] = (tr1.ImageE2.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[2]);
            result.ImageE2.Coef[2] = (tr1.ImageE2.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE2.Coef[3]);
            result.ImageE2.Coef[3] = (tr1.ImageE2.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[0]);
            result.ImageE1.Coef[0] = (tr1.ImageE1.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[1]);
            result.ImageE1.Coef[1] = (tr1.ImageE1.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[2]);
            result.ImageE1.Coef[2] = (tr1.ImageE1.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE1.Coef[3]);
            result.ImageE1.Coef[3] = (tr1.ImageE1.Coef[3] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[0]);
            result.ImageE0.Coef[0] = (tr1.ImageE0.Coef[0] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[1]);
            result.ImageE0.Coef[1] = (tr1.ImageE0.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[2]);
            result.ImageE0.Coef[2] = (tr1.ImageE0.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * tr2.ImageE0.Coef[3]);
            result.ImageE0.Coef[3] = (tr1.ImageE0.Coef[3] + tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct TimesLT(geometry2d.e2d.LTStruct tr, double s)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.TimesLT
            //Input Variables: 17 used, 0 not used, 17 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 16 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    s = <variable>
            
            
            result.ImageE3.Coef[0] = (tr.ImageE3.Coef[0] * s);
            result.ImageE3.Coef[1] = (tr.ImageE3.Coef[1] * s);
            result.ImageE3.Coef[2] = (tr.ImageE3.Coef[2] * s);
            result.ImageE3.Coef[3] = (tr.ImageE3.Coef[3] * s);
            result.ImageE2.Coef[0] = (tr.ImageE2.Coef[0] * s);
            result.ImageE2.Coef[1] = (tr.ImageE2.Coef[1] * s);
            result.ImageE2.Coef[2] = (tr.ImageE2.Coef[2] * s);
            result.ImageE2.Coef[3] = (tr.ImageE2.Coef[3] * s);
            result.ImageE1.Coef[0] = (tr.ImageE1.Coef[0] * s);
            result.ImageE1.Coef[1] = (tr.ImageE1.Coef[1] * s);
            result.ImageE1.Coef[2] = (tr.ImageE1.Coef[2] * s);
            result.ImageE1.Coef[3] = (tr.ImageE1.Coef[3] * s);
            result.ImageE0.Coef[0] = (tr.ImageE0.Coef[0] * s);
            result.ImageE0.Coef[1] = (tr.ImageE0.Coef[1] * s);
            result.ImageE0.Coef[2] = (tr.ImageE0.Coef[2] * s);
            result.ImageE0.Coef[3] = (tr.ImageE0.Coef[3] * s);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct DivideLT(geometry2d.e2d.LTStruct tr, double s)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.DivideLT
            //Input Variables: 17 used, 0 not used, 17 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 17 total.
            //Memory Reads: 1.94117647058824 average, 33 total.
            //Memory Writes: 17 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    tr.ImageE3.#E0# = <variable>
            //    tr.ImageE3.#e1# = <variable>
            //    tr.ImageE3.#e2# = <variable>
            //    tr.ImageE3.#e1^e2# = <variable>
            //    tr.ImageE2.#E0# = <variable>
            //    tr.ImageE2.#e1# = <variable>
            //    tr.ImageE2.#e2# = <variable>
            //    tr.ImageE2.#e1^e2# = <variable>
            //    tr.ImageE1.#E0# = <variable>
            //    tr.ImageE1.#e1# = <variable>
            //    tr.ImageE1.#e2# = <variable>
            //    tr.ImageE1.#e1^e2# = <variable>
            //    tr.ImageE0.#E0# = <variable>
            //    tr.ImageE0.#e1# = <variable>
            //    tr.ImageE0.#e2# = <variable>
            //    tr.ImageE0.#e1^e2# = <variable>
            //    s = <variable>
            
            double tempVar0000;
            
            tempVar0000 = Math.Pow(s, -1);
            result.ImageE3.Coef[0] = (tr.ImageE3.Coef[0] * tempVar0000);
            result.ImageE3.Coef[1] = (tr.ImageE3.Coef[1] * tempVar0000);
            result.ImageE3.Coef[2] = (tr.ImageE3.Coef[2] * tempVar0000);
            result.ImageE3.Coef[3] = (tr.ImageE3.Coef[3] * tempVar0000);
            result.ImageE2.Coef[0] = (tr.ImageE2.Coef[0] * tempVar0000);
            result.ImageE2.Coef[1] = (tr.ImageE2.Coef[1] * tempVar0000);
            result.ImageE2.Coef[2] = (tr.ImageE2.Coef[2] * tempVar0000);
            result.ImageE2.Coef[3] = (tr.ImageE2.Coef[3] * tempVar0000);
            result.ImageE1.Coef[0] = (tr.ImageE1.Coef[0] * tempVar0000);
            result.ImageE1.Coef[1] = (tr.ImageE1.Coef[1] * tempVar0000);
            result.ImageE1.Coef[2] = (tr.ImageE1.Coef[2] * tempVar0000);
            result.ImageE1.Coef[3] = (tr.ImageE1.Coef[3] * tempVar0000);
            result.ImageE0.Coef[0] = (tr.ImageE0.Coef[0] * tempVar0000);
            result.ImageE0.Coef[1] = (tr.ImageE0.Coef[1] * tempVar0000);
            result.ImageE0.Coef[2] = (tr.ImageE0.Coef[2] * tempVar0000);
            result.ImageE0.Coef[3] = (tr.ImageE0.Coef[3] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct OMToLT(geometry2d.e2d.OMStruct om)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.OMToLT
            //Input Variables: 4 used, 4 not used, 8 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 16 total.
            //Computations: 0.222222222222222 average, 4 total.
            //Memory Reads: 0.555555555555556 average, 10 total.
            //Memory Writes: 18 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.ImageE3.Coef[0] = 0;
            result.ImageE3.Coef[1] = 0;
            result.ImageE3.Coef[2] = 0;
            result.ImageE2.Coef[0] = 0;
            result.ImageE2.Coef[3] = 0;
            result.ImageE1.Coef[0] = 0;
            result.ImageE1.Coef[3] = 0;
            result.ImageE0.Coef[0] = 1;
            result.ImageE0.Coef[1] = 0;
            result.ImageE0.Coef[2] = 0;
            result.ImageE0.Coef[3] = 0;
            result.ImageE2.Coef[1] = om.ImageV2.Coef[1];
            result.ImageE2.Coef[2] = om.ImageV2.Coef[2];
            result.ImageE1.Coef[1] = om.ImageV1.Coef[1];
            result.ImageE1.Coef[2] = om.ImageV1.Coef[2];
            tempVar0000 = (-1 * om.ImageV2.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[2]);
            result.ImageE3.Coef[3] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyOM(geometry2d.e2d.OMStruct om, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.ApplyOM
            //Input Variables: 8 used, 4 not used, 12 total.
            //Temp Variables: 7 sub-expressions, 0 generated temps, 7 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1 average, 11 total.
            //Memory Reads: 1.90909090909091 average, 21 total.
            //Memory Writes: 11 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = mv.Coef[0];
            tempVar0000 = (om.ImageV1.Coef[1] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * mv.Coef[2]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om.ImageV1.Coef[2] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[2] * mv.Coef[2]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * om.ImageV2.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[2]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            result.Coef[3] = (mv.Coef[3] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector AVOM(geometry2d.e2d.OMStruct om, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.AVOM
            //Input Variables: 6 used, 6 not used, 12 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 0.75 average, 6 total.
            //Memory Reads: 1.5 average, 12 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.Coef[0] = 0;
            result.Coef[3] = 0;
            tempVar0000 = (om.ImageV1.Coef[1] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * mv.Coef[2]);
            result.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om.ImageV1.Coef[2] * mv.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[2] * mv.Coef[2]);
            result.Coef[2] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct TransOM(geometry2d.e2d.OMStruct om)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:40 م
            //Macro: geometry2d.e2d.TransOM
            //Input Variables: 4 used, 4 not used, 8 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0.5 average, 4 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV2.Coef[1] = om.ImageV1.Coef[2];
            result.ImageV2.Coef[2] = om.ImageV2.Coef[2];
            result.ImageV1.Coef[1] = om.ImageV1.Coef[1];
            result.ImageV1.Coef[2] = om.ImageV2.Coef[1];
            
            return result;
        }
        
        public static double DetOM(geometry2d.e2d.OMStruct om)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.DetOM
            //Input Variables: 4 used, 4 not used, 8 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.33333333333333 average, 4 total.
            //Memory Reads: 2 average, 6 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * om.ImageV2.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[2]);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static double EDetOM(geometry2d.e2d.OMStruct om)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.EDetOM
            //Input Variables: 4 used, 4 not used, 8 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.33333333333333 average, 4 total.
            //Memory Reads: 2 average, 6 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * om.ImageV2.Coef[2] * om.ImageV1.Coef[1]);
            tempVar0001 = (om.ImageV2.Coef[1] * om.ImageV1.Coef[2]);
            result = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct ComposeOM(geometry2d.e2d.OMStruct om1, geometry2d.e2d.OMStruct om2)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.ComposeOM
            //Input Variables: 8 used, 8 not used, 16 total.
            //Temp Variables: 8 sub-expressions, 0 generated temps, 8 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 12 total.
            //Memory Reads: 1.5 average, 24 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    om1.ImageV2.#E0# = <variable>
            //    om1.ImageV2.#e1# = <variable>
            //    om1.ImageV2.#e2# = <variable>
            //    om1.ImageV2.#e1^e2# = <variable>
            //    om1.ImageV1.#E0# = <variable>
            //    om1.ImageV1.#e1# = <variable>
            //    om1.ImageV1.#e2# = <variable>
            //    om1.ImageV1.#e1^e2# = <variable>
            //    om2.ImageV2.#E0# = <variable>
            //    om2.ImageV2.#e1# = <variable>
            //    om2.ImageV2.#e2# = <variable>
            //    om2.ImageV2.#e1^e2# = <variable>
            //    om2.ImageV1.#E0# = <variable>
            //    om2.ImageV1.#e1# = <variable>
            //    om2.ImageV1.#e2# = <variable>
            //    om2.ImageV1.#e1^e2# = <variable>
            
            double tempVar0000;
            double tempVar0001;
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            tempVar0000 = (om1.ImageV1.Coef[1] * om2.ImageV2.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[1] * om2.ImageV2.Coef[2]);
            result.ImageV2.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[2] * om2.ImageV2.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[2] * om2.ImageV2.Coef[2]);
            result.ImageV2.Coef[2] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[1] * om2.ImageV1.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[1] * om2.ImageV1.Coef[2]);
            result.ImageV1.Coef[1] = (tempVar0000 + tempVar0001);
            tempVar0000 = (om1.ImageV1.Coef[2] * om2.ImageV1.Coef[1]);
            tempVar0001 = (om1.ImageV2.Coef[2] * om2.ImageV1.Coef[2]);
            result.ImageV1.Coef[2] = (tempVar0000 + tempVar0001);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct AddOM(geometry2d.e2d.OMStruct om1, geometry2d.e2d.OMStruct om2)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.AddOM
            //Input Variables: 8 used, 8 not used, 16 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    om1.ImageV2.#E0# = <variable>
            //    om1.ImageV2.#e1# = <variable>
            //    om1.ImageV2.#e2# = <variable>
            //    om1.ImageV2.#e1^e2# = <variable>
            //    om1.ImageV1.#E0# = <variable>
            //    om1.ImageV1.#e1# = <variable>
            //    om1.ImageV1.#e2# = <variable>
            //    om1.ImageV1.#e1^e2# = <variable>
            //    om2.ImageV2.#E0# = <variable>
            //    om2.ImageV2.#e1# = <variable>
            //    om2.ImageV2.#e2# = <variable>
            //    om2.ImageV2.#e1^e2# = <variable>
            //    om2.ImageV1.#E0# = <variable>
            //    om2.ImageV1.#e1# = <variable>
            //    om2.ImageV1.#e2# = <variable>
            //    om2.ImageV1.#e1^e2# = <variable>
            
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV2.Coef[1] = (om1.ImageV2.Coef[1] + om2.ImageV2.Coef[1]);
            result.ImageV2.Coef[2] = (om1.ImageV2.Coef[2] + om2.ImageV2.Coef[2]);
            result.ImageV1.Coef[1] = (om1.ImageV1.Coef[1] + om2.ImageV1.Coef[1]);
            result.ImageV1.Coef[2] = (om1.ImageV1.Coef[2] + om2.ImageV1.Coef[2]);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct SubtractOM(geometry2d.e2d.OMStruct om1, geometry2d.e2d.OMStruct om2)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.SubtractOM
            //Input Variables: 8 used, 8 not used, 16 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 8 total.
            //Computations: 0.666666666666667 average, 8 total.
            //Memory Reads: 1 average, 12 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    om1.ImageV2.#E0# = <variable>
            //    om1.ImageV2.#e1# = <variable>
            //    om1.ImageV2.#e2# = <variable>
            //    om1.ImageV2.#e1^e2# = <variable>
            //    om1.ImageV1.#E0# = <variable>
            //    om1.ImageV1.#e1# = <variable>
            //    om1.ImageV1.#e2# = <variable>
            //    om1.ImageV1.#e1^e2# = <variable>
            //    om2.ImageV2.#E0# = <variable>
            //    om2.ImageV2.#e1# = <variable>
            //    om2.ImageV2.#e2# = <variable>
            //    om2.ImageV2.#e1^e2# = <variable>
            //    om2.ImageV1.#E0# = <variable>
            //    om2.ImageV1.#e1# = <variable>
            //    om2.ImageV1.#e2# = <variable>
            //    om2.ImageV1.#e1^e2# = <variable>
            
            double tempVar0000;
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            tempVar0000 = (-1 * om2.ImageV2.Coef[1]);
            result.ImageV2.Coef[1] = (om1.ImageV2.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV2.Coef[2]);
            result.ImageV2.Coef[2] = (om1.ImageV2.Coef[2] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV1.Coef[1]);
            result.ImageV1.Coef[1] = (om1.ImageV1.Coef[1] + tempVar0000);
            tempVar0000 = (-1 * om2.ImageV1.Coef[2]);
            result.ImageV1.Coef[2] = (om1.ImageV1.Coef[2] + tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct TimesOM(geometry2d.e2d.OMStruct om, double s)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.TimesOM
            //Input Variables: 5 used, 4 not used, 9 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 1 average, 8 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    s = <variable>
            
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            result.ImageV2.Coef[1] = (om.ImageV2.Coef[1] * s);
            result.ImageV2.Coef[2] = (om.ImageV2.Coef[2] * s);
            result.ImageV1.Coef[1] = (om.ImageV1.Coef[1] * s);
            result.ImageV1.Coef[2] = (om.ImageV1.Coef[2] * s);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct DivideOM(geometry2d.e2d.OMStruct om, double s)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.DivideOM
            //Input Variables: 5 used, 4 not used, 9 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 8 total.
            //Computations: 0.555555555555556 average, 5 total.
            //Memory Reads: 1 average, 9 total.
            //Memory Writes: 9 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    om.ImageV2.#E0# = <variable>
            //    om.ImageV2.#e1# = <variable>
            //    om.ImageV2.#e2# = <variable>
            //    om.ImageV2.#e1^e2# = <variable>
            //    om.ImageV1.#E0# = <variable>
            //    om.ImageV1.#e1# = <variable>
            //    om.ImageV1.#e2# = <variable>
            //    om.ImageV1.#e1^e2# = <variable>
            //    s = <variable>
            
            double tempVar0000;
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
            tempVar0000 = Math.Pow(s, -1);
            result.ImageV2.Coef[1] = (om.ImageV2.Coef[1] * tempVar0000);
            result.ImageV2.Coef[2] = (om.ImageV2.Coef[2] * tempVar0000);
            result.ImageV1.Coef[1] = (om.ImageV1.Coef[1] * tempVar0000);
            result.ImageV1.Coef[2] = (om.ImageV1.Coef[2] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct VersorToOM(geometry2d.e2d.Multivector v)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:41 م
            //Macro: geometry2d.e2d.VersorToOM
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 8 total.
            //Computations: 1.02272727272727 average, 45 total.
            //Memory Reads: 1.61363636363636 average, 71 total.
            //Memory Writes: 44 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
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
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[4] = (v.Coef[2] * tempArray[2]);
            tempArray[5] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (v.Coef[3] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[1] = (tempArray[5] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[0] * tempArray[0]);
            result.ImageV2.Coef[1] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            result.ImageV2.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageV1.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            result.ImageV1.Coef[2] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct VersorToLT(geometry2d.e2d.Multivector v)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.VersorToLT
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 42 sub-expressions, 0 generated temps, 42 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 16 total.
            //Computations: 0.913793103448276 average, 53 total.
            //Memory Reads: 1.46551724137931 average, 85 total.
            //Memory Writes: 58 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            result.ImageE3.Coef[0] = 0;
            result.ImageE3.Coef[1] = 0;
            result.ImageE3.Coef[2] = 0;
            result.ImageE2.Coef[0] = 0;
            result.ImageE2.Coef[3] = 0;
            result.ImageE1.Coef[0] = 0;
            result.ImageE1.Coef[3] = 0;
            result.ImageE0.Coef[0] = 1;
            result.ImageE0.Coef[1] = 0;
            result.ImageE0.Coef[2] = 0;
            result.ImageE0.Coef[3] = 0;
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
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[4] = (v.Coef[2] * tempArray[2]);
            tempArray[5] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (v.Coef[3] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[1] = (tempArray[5] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[0] * tempArray[0]);
            result.ImageE2.Coef[1] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            result.ImageE2.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageE1.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            result.ImageE1.Coef[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (tempArray[0] * tempArray[1]);
            tempArray[1] = (tempArray[0] + tempArray[1]);
            tempArray[2] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[2]);
            result.ImageE3.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyVersor(geometry2d.e2d.Multivector v, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.ApplyVersor
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 68 sub-expressions, 0 generated temps, 68 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 4 total.
            //Computations: 1.33333333333333 average, 96 total.
            //Memory Reads: 1.875 average, 135 total.
            //Memory Writes: 72 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double[] tempArray = new double[7];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
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
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = Math.Pow(v.Coef[0], 2);
            tempArray[5] = (-1 * tempArray[5]);
            tempArray[6] = Math.Pow(v.Coef[1], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = Math.Pow(v.Coef[2], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = Math.Pow(v.Coef[3], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = Math.Pow(tempArray[5], -1);
            result.Coef[0] = (tempArray[1] * tempArray[5]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            result.Coef[1] = (tempArray[5] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[6] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            result.Coef[2] = (tempArray[5] * tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[3] = (tempArray[5] * tempArray[0]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyRotor(geometry2d.e2d.Multivector v, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.ApplyRotor
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 52 sub-expressions, 0 generated temps, 52 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.42857142857143 average, 80 total.
            //Memory Reads: 2 average, 112 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
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
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            result.Coef[0] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[2] * tempArray[4]);
            result.Coef[1] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[5] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (v.Coef[1] * tempArray[4]);
            result.Coef[2] = (tempArray[1] + tempArray[5]);
            tempArray[0] = (v.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            result.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyReflector(geometry2d.e2d.Multivector v, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.ApplyReflector
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 56 sub-expressions, 0 generated temps, 56 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.4 average, 84 total.
            //Memory Reads: 1.93333333333333 average, 116 total.
            //Memory Writes: 60 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
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
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            result.Coef[0] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            result.Coef[1] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[5] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            result.Coef[2] = (-1 * tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[3] = (-1 * tempArray[0]);
            
            return result;
        }
        
        public static geometry2d.e2d.OMStruct EVersorToOM(geometry2d.e2d.Multivector v)
        {
            var result = new geometry2d.e2d.OMStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.EVersorToOM
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 8 total.
            //Computations: 1.02272727272727 average, 45 total.
            //Memory Reads: 1.61363636363636 average, 71 total.
            //Memory Writes: 44 total.
            //
            //Macro Binding Data: 
            //    result.ImageV2.#E0# = <variable>
            //    result.ImageV2.#e1# = <variable>
            //    result.ImageV2.#e2# = <variable>
            //    result.ImageV2.#e1^e2# = <variable>
            //    result.ImageV1.#E0# = <variable>
            //    result.ImageV1.#e1# = <variable>
            //    result.ImageV1.#e2# = <variable>
            //    result.ImageV1.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            result.ImageV2.Coef[0] = 0;
            result.ImageV2.Coef[3] = 0;
            result.ImageV1.Coef[0] = 0;
            result.ImageV1.Coef[3] = 0;
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
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[4] = (v.Coef[2] * tempArray[2]);
            tempArray[5] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (v.Coef[3] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[1] = (tempArray[5] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[0] * tempArray[0]);
            result.ImageV2.Coef[1] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            result.ImageV2.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageV1.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            result.ImageV1.Coef[2] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.LTStruct EVersorToLT(geometry2d.e2d.Multivector v)
        {
            var result = new geometry2d.e2d.LTStruct();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.EVersorToLT
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 42 sub-expressions, 0 generated temps, 42 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 16 total.
            //Computations: 0.913793103448276 average, 53 total.
            //Memory Reads: 1.46551724137931 average, 85 total.
            //Memory Writes: 58 total.
            //
            //Macro Binding Data: 
            //    result.ImageE3.#E0# = <variable>
            //    result.ImageE3.#e1# = <variable>
            //    result.ImageE3.#e2# = <variable>
            //    result.ImageE3.#e1^e2# = <variable>
            //    result.ImageE2.#E0# = <variable>
            //    result.ImageE2.#e1# = <variable>
            //    result.ImageE2.#e2# = <variable>
            //    result.ImageE2.#e1^e2# = <variable>
            //    result.ImageE1.#E0# = <variable>
            //    result.ImageE1.#e1# = <variable>
            //    result.ImageE1.#e2# = <variable>
            //    result.ImageE1.#e1^e2# = <variable>
            //    result.ImageE0.#E0# = <variable>
            //    result.ImageE0.#e1# = <variable>
            //    result.ImageE0.#e2# = <variable>
            //    result.ImageE0.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            result.ImageE3.Coef[0] = 0;
            result.ImageE3.Coef[1] = 0;
            result.ImageE3.Coef[2] = 0;
            result.ImageE2.Coef[0] = 0;
            result.ImageE2.Coef[3] = 0;
            result.ImageE1.Coef[0] = 0;
            result.ImageE1.Coef[3] = 0;
            result.ImageE0.Coef[0] = 1;
            result.ImageE0.Coef[1] = 0;
            result.ImageE0.Coef[2] = 0;
            result.ImageE0.Coef[3] = 0;
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
            tempArray[0] = Math.Pow(tempArray[0], -1);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            tempArray[2] = (v.Coef[1] * tempArray[0]);
            tempArray[3] = (v.Coef[2] * tempArray[0]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[0]);
            tempArray[4] = (v.Coef[2] * tempArray[2]);
            tempArray[5] = (v.Coef[1] * tempArray[3]);
            tempArray[1] = (v.Coef[3] * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[1] = (tempArray[5] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[0] * tempArray[0]);
            result.ImageE2.Coef[1] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[0] * tempArray[1]);
            tempArray[1] = (v.Coef[3] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            result.ImageE2.Coef[2] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (v.Coef[1] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.ImageE1.Coef[1] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (-1 * v.Coef[3] * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[1] = (v.Coef[0] * tempArray[0]);
            result.ImageE1.Coef[2] = (tempArray[0] + tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (tempArray[1] + tempArray[0]);
            tempArray[0] = (tempArray[0] * tempArray[1]);
            tempArray[1] = (tempArray[0] + tempArray[1]);
            tempArray[2] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * tempArray[1] * tempArray[2]);
            result.ImageE3.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyEVersor(geometry2d.e2d.Multivector v, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.ApplyEVersor
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 68 sub-expressions, 0 generated temps, 68 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 4 total.
            //Computations: 1.33333333333333 average, 96 total.
            //Memory Reads: 1.875 average, 135 total.
            //Memory Writes: 72 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double[] tempArray = new double[7];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
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
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = Math.Pow(v.Coef[0], 2);
            tempArray[5] = (-1 * tempArray[5]);
            tempArray[6] = Math.Pow(v.Coef[1], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = Math.Pow(v.Coef[2], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = Math.Pow(v.Coef[3], 2);
            tempArray[6] = (-1 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[5] = Math.Pow(tempArray[5], -1);
            result.Coef[0] = (tempArray[1] * tempArray[5]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            result.Coef[1] = (tempArray[5] * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[6] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            result.Coef[2] = (tempArray[5] * tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[3] = (tempArray[5] * tempArray[0]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyERotor(geometry2d.e2d.Multivector v, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.ApplyERotor
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 52 sub-expressions, 0 generated temps, 52 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.42857142857143 average, 80 total.
            //Memory Reads: 2 average, 112 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
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
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            result.Coef[0] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[2] * tempArray[4]);
            result.Coef[1] = (tempArray[1] + tempArray[5]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[5] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (v.Coef[1] * tempArray[4]);
            result.Coef[2] = (tempArray[1] + tempArray[5]);
            tempArray[0] = (v.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            result.Coef[3] = (tempArray[0] + tempArray[1]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector ApplyEReflector(geometry2d.e2d.Multivector v, geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.ApplyEReflector
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 56 sub-expressions, 0 generated temps, 56 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 4 total.
            //Computations: 1.4 average, 84 total.
            //Memory Reads: 1.93333333333333 average, 116 total.
            //Memory Writes: 60 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    v.#E0# = <variable>
            //    v.#e1# = <variable>
            //    v.#e2# = <variable>
            //    v.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
            double[] tempArray = new double[6];
            
            tempArray[0] = (-1 * v.Coef[0] * mv.Coef[0]);
            tempArray[1] = (-1 * v.Coef[1] * mv.Coef[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * mv.Coef[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[3] * mv.Coef[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[0]);
            tempArray[2] = (-1 * v.Coef[1] * mv.Coef[0]);
            tempArray[3] = (-1 * v.Coef[0] * mv.Coef[1]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (-1 * v.Coef[3] * mv.Coef[2]);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (v.Coef[2] * mv.Coef[3]);
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
            tempArray[4] = (-1 * v.Coef[2] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * v.Coef[3] * mv.Coef[0]);
            tempArray[5] = (v.Coef[2] * mv.Coef[1]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[1] * mv.Coef[2]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * mv.Coef[3]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            result.Coef[0] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[1] * tempArray[0]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[3] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[2] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            result.Coef[1] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[0]);
            tempArray[5] = (v.Coef[3] * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * v.Coef[0] * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (v.Coef[1] * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            result.Coef[2] = (-1 * tempArray[1]);
            tempArray[0] = (v.Coef[3] * tempArray[0]);
            tempArray[1] = (-1 * v.Coef[2] * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (v.Coef[1] * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * v.Coef[0] * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            result.Coef[3] = (-1 * tempArray[0]);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector versorInverse(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.versorInverse
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1.0625 average, 17 total.
            //Memory Reads: 1.4375 average, 23 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            tempVar0000 = Math.Pow(tempVar0000, -1);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (-1 * mv.Coef[3] * tempVar0000);
            
            return result;
        }
        
        public static geometry2d.e2d.Multivector normalize(geometry2d.e2d.Multivector mv)
        {
            var result = new geometry2d.e2d.Multivector();
            
            //GMac Generated Processing Code, 04/12/2015 09:57:42 م
            //Macro: geometry2d.e2d.normalize
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 4 total.
            //Computations: 1 average, 16 total.
            //Memory Reads: 1.4375 average, 23 total.
            //Memory Writes: 16 total.
            //
            //Macro Binding Data: 
            //    result.#E0# = <variable>
            //    result.#e1# = <variable>
            //    result.#e2# = <variable>
            //    result.#e1^e2# = <variable>
            //    mv.#E0# = <variable>
            //    mv.#e1# = <variable>
            //    mv.#e2# = <variable>
            //    mv.#e1^e2# = <variable>
            
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
            tempVar0000 = Math.Pow(tempVar0000, -0.5);
            result.Coef[0] = (mv.Coef[0] * tempVar0000);
            result.Coef[1] = (mv.Coef[1] * tempVar0000);
            result.Coef[2] = (mv.Coef[2] * tempVar0000);
            result.Coef[3] = (mv.Coef[3] * tempVar0000);
            
            return result;
        }
        
    }
}
