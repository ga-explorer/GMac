//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace GMacBlade.cga5d
//{
//    //public static class <#=frame#>BladeUtils
//    public static class cga5dBladeUtils
//    {
//        /// <summary>
//        /// A table to convert a grade into a true\false value where a true means: 
//        /// "keep the sign of the blade's coefficients".
//        /// Used as lookup for the reverse operation on blades
//        /// </summary>
//        public static readonly bool[] GradeToReverseTable = { true, true, false, false, true, true };

//        /// <summary>
//        /// A table to convert a grade into a true\false value where a true means: 
//        /// "keep the sign of the blade's coefficients".
//        /// Used as lookup for the grade inversion operation on blades
//        /// </summary>
//        public static readonly bool[] GradeToGradeInvTable = { true, false, true, false, true, false };
//        //private static readonly bool[] _GradeToGradeInvTable = new bool[] { <#grade_inv_table#> };

//        /// <summary>
//        /// A table to convert a grade into a true\false value where a true means: 
//        /// "keep the sign of the blade's coefficients".
//        /// Used as lookup for the Clifford conjugate operation on blades
//        /// </summary>
//        public static readonly bool[] GradeToCliffConjTable = { true, false, false, true, true, false };



//        public static string[] BasisBladesNames(this cga5dBlade blade)
//        {
//            return _BasisBladesNames[blade.Grade];
//        }

//        /// <summary>
//        /// Make any coefficients with absolute value less than Epsilon equal to zero
//        /// </summary>
//        /// <param name="blade"></param>
//        /// <returns></returns>
//        public static cga5dBlade CleanCoefs(this cga5dBlade blade)
//        {
//            if (blade.IsZeroBlade)
//                return blade;

//            return 
//                new cga5dBlade(
//                    blade.Grade, 
//                    blade._coefs.Select(
//                        c => 
//                            (c > -cga5dBlade.Epsilon && c < cga5dBlade.Epsilon) ? 0.0D : c
//                        ).ToArray()
//                    );
//        }

//        /// <summary>
//        /// True if the given blade is a zero blade or has all-zero coefficients
//        /// </summary>
//        /// <param name="blade"></param>
//        /// <returns></returns>
//        public static bool IsZero(this cga5dBlade blade)
//        {
//            //A zero blade is always assumed having all-zero coefficients
//            if (blade.IsZeroBlade)
//                return true;

//            //Find the first coefficient in the blade with value outside Epsilon boundaries. 
//            //If no coefficient is found d will contain 0
//            double d = blade._coefs.FirstOrDefault(c => (c <= -cga5dBlade.Epsilon || c >= cga5dBlade.Epsilon));

//            //Return true if d contains 0 (i.e. no coefficient is found outside Epsilon boundaries)
//            return (d == default(double));
//        }

//        /// <summary>
//        /// The reverse of a blade
//        /// </summary>
//        /// <param name="blade"></param>
//        /// <returns></returns>
//        public static cga5dBlade Reverse(this cga5dBlade blade)
//        {
//            if (blade.IsZeroBlade || GradeToReverseTable[blade.Grade])
//                return blade;

//            return 
//                new cga5dBlade(
//                    blade.Grade, 
//                    blade._coefs.Select(c => -c).ToArray()
//                    );
//        }

//        /// <summary>
//        /// The grade inversion of a blade
//        /// </summary>
//        /// <param name="blade"></param>
//        /// <returns></returns>
//        public static cga5dBlade GradeInv(this cga5dBlade blade)
//        {
//            if (blade.IsZeroBlade || GradeToGradeInvTable[blade.Grade])
//                return blade;

//            return
//                new cga5dBlade(
//                    blade.Grade,
//                    blade._coefs.Select(c => -c).ToArray()
//                    );
//        }

//        /// <summary>
//        /// The Clifford conjugate of a blade
//        /// </summary>
//        /// <param name="blade"></param>
//        /// <returns></returns>
//        public static cga5dBlade CliffConj(this cga5dBlade blade)
//        {
//            if (blade.IsZeroBlade || GradeToCliffConjTable[blade.Grade])
//                return blade;

//            return
//                new cga5dBlade(
//                    blade.Grade,
//                    blade._coefs.Select(c => -c).ToArray()
//                    );
//        }
//    }
//}
