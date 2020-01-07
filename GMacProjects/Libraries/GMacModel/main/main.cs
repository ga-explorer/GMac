using System;

namespace GMacModel
{
    public static partial class main
    {
        public static readonly double Zero = 0;

        public static readonly double Pi = Math.PI;


        public static double sin(double d)
        {
            double result;
            
            result = Math.Sin(d);

            return result;
        }

        public static double cos(double d)
        {
            double result;

            result = Math.Cos(d);

            return result;
        }

        public static double sqrt(double d)
        {
            double result;

            result = Math.Sqrt(d);

            return result;
        }
    }
}
