using System;
using System.IO;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static double[] Add1(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                coefs1[0] + coefs2[0]
            };
        }
        
        private static double[] Subtract1(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                coefs1[0] - coefs2[0]
            };
        }
        
        private static double[] Times1(double[] coefs, double scalar)
        {
            return new[]
            {
                scalar * coefs[0]
            };
        }
        
        private static double[] Add5(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                coefs1[0] + coefs2[0],
                coefs1[1] + coefs2[1],
                coefs1[2] + coefs2[2],
                coefs1[3] + coefs2[3],
                coefs1[4] + coefs2[4]
            };
        }
        
        private static double[] Subtract5(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                coefs1[0] - coefs2[0],
                coefs1[1] - coefs2[1],
                coefs1[2] - coefs2[2],
                coefs1[3] - coefs2[3],
                coefs1[4] - coefs2[4]
            };
        }
        
        private static double[] Times5(double[] coefs, double scalar)
        {
            return new[]
            {
                scalar * coefs[0],
                scalar * coefs[1],
                scalar * coefs[2],
                scalar * coefs[3],
                scalar * coefs[4]
            };
        }
        
        private static double[] Add10(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                coefs1[0] + coefs2[0],
                coefs1[1] + coefs2[1],
                coefs1[2] + coefs2[2],
                coefs1[3] + coefs2[3],
                coefs1[4] + coefs2[4],
                coefs1[5] + coefs2[5],
                coefs1[6] + coefs2[6],
                coefs1[7] + coefs2[7],
                coefs1[8] + coefs2[8],
                coefs1[9] + coefs2[9]
            };
        }
        
        private static double[] Subtract10(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                coefs1[0] - coefs2[0],
                coefs1[1] - coefs2[1],
                coefs1[2] - coefs2[2],
                coefs1[3] - coefs2[3],
                coefs1[4] - coefs2[4],
                coefs1[5] - coefs2[5],
                coefs1[6] - coefs2[6],
                coefs1[7] - coefs2[7],
                coefs1[8] - coefs2[8],
                coefs1[9] - coefs2[9]
            };
        }
        
        private static double[] Times10(double[] coefs, double scalar)
        {
            return new[]
            {
                scalar * coefs[0],
                scalar * coefs[1],
                scalar * coefs[2],
                scalar * coefs[3],
                scalar * coefs[4],
                scalar * coefs[5],
                scalar * coefs[6],
                scalar * coefs[7],
                scalar * coefs[8],
                scalar * coefs[9]
            };
        }
        
        private static double[] EuclideanDual0(double[] coefs)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:36 ص
            //Macro: main.cga5d.EDual
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            
            
            c[0] = coefs[0];
            
            return c;
        }
        
        private static double[] EuclideanDual1(double[] coefs)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:36 ص
            //Macro: main.cga5d.EDual
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 5 total.
            //Computations: 0.4 average, 2 total.
            //Memory Reads: 1 average, 5 total.
            //Memory Writes: 5 total.
            
            
            c[0] = coefs[4];
            c[1] = (-1 * coefs[3]);
            c[2] = coefs[2];
            c[3] = (-1 * coefs[1]);
            c[4] = coefs[0];
            
            return c;
        }
        
        private static double[] EuclideanDual2(double[] coefs)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:37 ص
            //Macro: main.cga5d.EDual
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0.6 average, 6 total.
            //Memory Reads: 1 average, 10 total.
            //Memory Writes: 10 total.
            
            
            c[0] = (-1 * coefs[9]);
            c[1] = coefs[8];
            c[2] = (-1 * coefs[7]);
            c[3] = coefs[6];
            c[4] = (-1 * coefs[5]);
            c[5] = coefs[4];
            c[6] = (-1 * coefs[3]);
            c[7] = (-1 * coefs[2]);
            c[8] = coefs[1];
            c[9] = (-1 * coefs[0]);
            
            return c;
        }
        
        private static double[] EuclideanDual3(double[] coefs)
        {
            var c = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:38 ص
            //Macro: main.cga5d.EDual
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 10 total.
            //Computations: 0.6 average, 6 total.
            //Memory Reads: 1 average, 10 total.
            //Memory Writes: 10 total.
            
            
            c[0] = (-1 * coefs[9]);
            c[1] = coefs[8];
            c[2] = (-1 * coefs[7]);
            c[3] = (-1 * coefs[6]);
            c[4] = coefs[5];
            c[5] = (-1 * coefs[4]);
            c[6] = coefs[3];
            c[7] = (-1 * coefs[2]);
            c[8] = coefs[1];
            c[9] = (-1 * coefs[0]);
            
            return c;
        }
        
        private static double[] EuclideanDual4(double[] coefs)
        {
            var c = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:38 ص
            //Macro: main.cga5d.EDual
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 5 total.
            //Computations: 0.4 average, 2 total.
            //Memory Reads: 1 average, 5 total.
            //Memory Writes: 5 total.
            
            
            c[0] = coefs[4];
            c[1] = (-1 * coefs[3]);
            c[2] = coefs[2];
            c[3] = (-1 * coefs[1]);
            c[4] = coefs[0];
            
            return c;
        }
        
        private static double[] EuclideanDual5(double[] coefs)
        {
            var c = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:39 ص
            //Macro: main.cga5d.EDual
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            
            
            c[0] = coefs[0];
            
            return c;
        }
        
        private static int SelfDPGrade2(double[] coefs)
        {
            double c = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:39 ص
            //Macro: main.cga5d.SelfEGP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 15 total.
            //Computations: 1.14285714285714 average, 40 total.
            //Memory Reads: 1.42857142857143 average, 50 total.
            //Memory Writes: 35 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-2 * coefs[2] * coefs[3]);
            tempVar0001 = (2 * coefs[1] * coefs[4]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[5]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (-2 * coefs[2] * coefs[6]);
            tempVar0001 = (2 * coefs[1] * coefs[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[8]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (-2 * coefs[4] * coefs[6]);
            tempVar0001 = (2 * coefs[3] * coefs[7]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[9]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (-2 * coefs[5] * coefs[6]);
            tempVar0001 = (2 * coefs[3] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[1] * coefs[9]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (-2 * coefs[5] * coefs[7]);
            tempVar0001 = (2 * coefs[4] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[2] * coefs[9]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            
            return 0;
        }
        
        private static int SelfDPGrade3(double[] coefs)
        {
            double c = 0.0D;
        
            //GMac Generated Processing Code, 14/04/2015 09:53:39 ص
            //Macro: main.cga5d.SelfEGP
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 20 sub-expressions, 0 generated temps, 20 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 15 total.
            //Computations: 1.14285714285714 average, 40 total.
            //Memory Reads: 1.42857142857143 average, 50 total.
            //Memory Writes: 35 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-2 * coefs[6] * coefs[7]);
            tempVar0001 = (2 * coefs[5] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[4] * coefs[9]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (2 * coefs[3] * coefs[7]);
            tempVar0001 = (-2 * coefs[2] * coefs[8]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * coefs[1] * coefs[9]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (-2 * coefs[3] * coefs[5]);
            tempVar0001 = (2 * coefs[2] * coefs[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[9]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (2 * coefs[3] * coefs[4]);
            tempVar0001 = (-2 * coefs[1] * coefs[6]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (2 * coefs[0] * coefs[8]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            tempVar0000 = (-2 * coefs[2] * coefs[4]);
            tempVar0001 = (2 * coefs[1] * coefs[5]);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-2 * coefs[0] * coefs[7]);
            c = (tempVar0000 + tempVar0001);
            if (c <= -Epsilon || c >= Epsilon) return 4;
            
            
            return 0;
        }
        
        public cga5dBlade Add(cga5dBlade blade2)
        {
            if (blade2.IsZero)
                return this;
        
            if (IsZero)
                return blade2;
        
            if (Grade != blade2.Grade)
                throw new InvalidOperationException("Can't add two non-zero blades of different grades");
        
            switch (Grade)
            {
                case 0:
                    return new cga5dBlade(0, Add1(Coefs, blade2.Coefs));
                case 1:
                    return new cga5dBlade(1, Add5(Coefs, blade2.Coefs));
                case 2:
                    return new cga5dBlade(2, Add10(Coefs, blade2.Coefs));
                case 3:
                    return new cga5dBlade(3, Add10(Coefs, blade2.Coefs));
                case 4:
                    return new cga5dBlade(4, Add5(Coefs, blade2.Coefs));
                case 5:
                    return new cga5dBlade(5, Add1(Coefs, blade2.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public cga5dBlade Subtract(cga5dBlade blade2)
        {
            if (blade2.IsZero)
                return this;
        
            if (IsZero)
                return blade2.Negative;
        
            if (Grade != blade2.Grade)
                throw new InvalidOperationException("Can't subtract two non-zero blades of different grades");
        
            switch (Grade)
            {
                case 0:
                    return new cga5dBlade(0, Subtract1(Coefs, blade2.Coefs));
                case 1:
                    return new cga5dBlade(1, Subtract5(Coefs, blade2.Coefs));
                case 2:
                    return new cga5dBlade(2, Subtract10(Coefs, blade2.Coefs));
                case 3:
                    return new cga5dBlade(3, Subtract10(Coefs, blade2.Coefs));
                case 4:
                    return new cga5dBlade(4, Subtract5(Coefs, blade2.Coefs));
                case 5:
                    return new cga5dBlade(5, Subtract1(Coefs, blade2.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public cga5dBlade Times(double scalar)
        {
            switch (Grade)
            {
                case 0:
                    return new cga5dBlade(0, Times1(Coefs, scalar));
                case 1:
                    return new cga5dBlade(1, Times5(Coefs, scalar));
                case 2:
                    return new cga5dBlade(2, Times10(Coefs, scalar));
                case 3:
                    return new cga5dBlade(3, Times10(Coefs, scalar));
                case 4:
                    return new cga5dBlade(4, Times5(Coefs, scalar));
                case 5:
                    return new cga5dBlade(5, Times1(Coefs, scalar));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public cga5dBlade Divide(double scalar)
        {
            switch (Grade)
            {
                case 0:
                    return new cga5dBlade(0, Times1(Coefs, 1.0D / scalar));
                case 1:
                    return new cga5dBlade(1, Times5(Coefs, 1.0D / scalar));
                case 2:
                    return new cga5dBlade(2, Times10(Coefs, 1.0D / scalar));
                case 3:
                    return new cga5dBlade(3, Times10(Coefs, 1.0D / scalar));
                case 4:
                    return new cga5dBlade(4, Times5(Coefs, 1.0D / scalar));
                case 5:
                    return new cga5dBlade(5, Times1(Coefs, 1.0D / scalar));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public cga5dBlade Inverse
        {
            get
            {
                var scalar = Norm2;
        
                if ((scalar <= -Epsilon || scalar >= Epsilon) == false)
                    throw new InvalidOperationException("Null blade has no inverse");
        
                switch (Grade)
                {
                    case 0:
                        return new cga5dBlade(0, Times1(Coefs, 1.0D / scalar));
                    case 1:
                        return new cga5dBlade(1, Times5(Coefs, 1.0D / scalar));
                    case 2:
                        return new cga5dBlade(2, Times10(Coefs, -1.0D / scalar));
                    case 3:
                        return new cga5dBlade(3, Times10(Coefs, -1.0D / scalar));
                    case 4:
                        return new cga5dBlade(4, Times5(Coefs, 1.0D / scalar));
                    case 5:
                        return new cga5dBlade(5, Times1(Coefs, 1.0D / scalar));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        public cga5dBlade EuclideanInverse
        {
            get
            {
                var scalar = EMag2;
        
                if ((scalar <= -Epsilon || scalar >= Epsilon) == false)
                    throw new InvalidOperationException("Null blade has no inverse");
        
                switch (Grade)
                {
                    case 0:
                        return new cga5dBlade(0, Times1(Coefs, 1.0D / scalar));
                    case 1:
                        return new cga5dBlade(1, Times5(Coefs, 1.0D / scalar));
                    case 2:
                        return new cga5dBlade(2, Times10(Coefs, -1.0D / scalar));
                    case 3:
                        return new cga5dBlade(3, Times10(Coefs, -1.0D / scalar));
                    case 4:
                        return new cga5dBlade(4, Times5(Coefs, 1.0D / scalar));
                    case 5:
                        return new cga5dBlade(5, Times1(Coefs, 1.0D / scalar));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        public cga5dBlade EuclideanDual
        {
            get
            {
                if (IsZero)
                    return ZeroBlade;
        
                switch (Grade)
                {
                    case 0:
                        return new cga5dBlade(5, EuclideanDual0(Coefs));
                    case 1:
                        return new cga5dBlade(4, EuclideanDual1(Coefs));
                    case 2:
                        return new cga5dBlade(3, EuclideanDual2(Coefs));
                    case 3:
                        return new cga5dBlade(2, EuclideanDual3(Coefs));
                    case 4:
                        return new cga5dBlade(1, EuclideanDual4(Coefs));
                    case 5:
                        return new cga5dBlade(0, EuclideanDual5(Coefs));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        /// <summary>
        /// The grade of the delta product of this k-vector with itself. If the grade is 0 this 
        /// k-vector is a blade
        /// </summary>
        /// <returns></returns>
        public int SelfDPGrade()
        {
            if (Grade <= 1 || Grade >= MaxGrade - 1 || IsZero)
                return 0;
        
            switch (Grade)
            {
                case 2:
                    return SelfDPGrade2(Coefs);
                case 3:
                    return SelfDPGrade3(Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
