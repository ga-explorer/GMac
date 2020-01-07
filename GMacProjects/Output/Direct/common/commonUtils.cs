using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace common
{
    public static class commonUtils
    {
        public static readonly double Pi = Math.PI;
        
        public static readonly double E = Math.E;
        
        public static double sin(double d)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: common.sin
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    d = <variable>
            
            
            result = Math.Sin(d);
            
            return result;
        }
        
        public static double cos(double d)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: common.cos
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    d = <variable>
            
            
            result = Math.Cos(d);
            
            return result;
        }
        
        public static double sqrt(double d)
        {
            double result;
            
            //GMac Generated Processing Code, 04/12/2015 09:57:37 م
            //Macro: common.sqrt
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //    result = <variable>
            //    d = <variable>
            
            
            result = Math.Pow(d, 0.5);
            
            return result;
        }
        
    }
}
