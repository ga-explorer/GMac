using System;
using System.IO;

namespace Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        private static double[] Add1(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                scalars1[0] + scalars2[0]
            };
        }
        
        private static double[] Subtract1(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                scalars1[0] - scalars2[0]
            };
        }
        
        private static double[] Times1(double[] scalars, double scalar)
        {
            return new[]
            {
                scalar * scalars[0]
            };
        }
        
        private static double[] Add3(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                scalars1[0] + scalars2[0],
                scalars1[1] + scalars2[1],
                scalars1[2] + scalars2[2]
            };
        }
        
        private static double[] Subtract3(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                scalars1[0] - scalars2[0],
                scalars1[1] - scalars2[1],
                scalars1[2] - scalars2[2]
            };
        }
        
        private static double[] Times3(double[] scalars, double scalar)
        {
            return new[]
            {
                scalar * scalars[0],
                scalar * scalars[1],
                scalar * scalars[2]
            };
        }
        
        private static double[] EuclideanDual0(double[] scalars)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.4357116+02:00
            //Macro: geometry3d.Ega3D.EDual
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3# = variable: c[0]
            //   mv.#E0# = variable: scalars[0]
            
            
            c[0] = -scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.4357116+02:00
            
            return c;
        }
        
        private static double[] EuclideanDual1(double[] scalars)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:44.1429210+02:00
            //Macro: geometry3d.Ega3D.EDual
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 3 total.
            //Computations: 0.666666666666667 average, 2 total.
            //Memory Reads: 1 average, 3 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: c[0]
            //   result.#e1^e3# = variable: c[1]
            //   result.#e2^e3# = variable: c[2]
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            
            c[0] = -scalars[2];
            
            c[1] = scalars[1];
            
            c[2] = -scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:44.1429210+02:00
            
            return c;
        }
        
        private static double[] EuclideanDual2(double[] scalars)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:44.4007621+02:00
            //Macro: geometry3d.Ega3D.EDual
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 3 total.
            //Computations: 0.333333333333333 average, 1 total.
            //Memory Reads: 1 average, 3 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: c[0]
            //   result.#e2# = variable: c[1]
            //   result.#e3# = variable: c[2]
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            
            c[0] = scalars[2];
            
            c[1] = -scalars[1];
            
            c[2] = scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:44.4007621+02:00
            
            return c;
        }
        
        private static double[] EuclideanDual3(double[] scalars)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:44.4817116+02:00
            //Macro: geometry3d.Ega3D.EDual
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            
            c[0] = scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:44.4817116+02:00
            
            return c;
        }
        
        public Ega3DkVector Add(Ega3DkVector blade2)
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
                    return new Ega3DkVector(0, Add1(Scalars, blade2.Scalars));
                case 1:
                    return new Ega3DkVector(1, Add3(Scalars, blade2.Scalars));
                case 2:
                    return new Ega3DkVector(2, Add3(Scalars, blade2.Scalars));
                case 3:
                    return new Ega3DkVector(3, Add1(Scalars, blade2.Scalars));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public Ega3DkVector Subtract(Ega3DkVector blade2)
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
                    return new Ega3DkVector(0, Subtract1(Scalars, blade2.Scalars));
                case 1:
                    return new Ega3DkVector(1, Subtract3(Scalars, blade2.Scalars));
                case 2:
                    return new Ega3DkVector(2, Subtract3(Scalars, blade2.Scalars));
                case 3:
                    return new Ega3DkVector(3, Subtract1(Scalars, blade2.Scalars));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public Ega3DkVector Times(double scalar)
        {
            switch (Grade)
            {
                case 0:
                    return new Ega3DkVector(0, Times1(Scalars, scalar));
                case 1:
                    return new Ega3DkVector(1, Times3(Scalars, scalar));
                case 2:
                    return new Ega3DkVector(2, Times3(Scalars, scalar));
                case 3:
                    return new Ega3DkVector(3, Times1(Scalars, scalar));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public Ega3DkVector Divide(double scalar)
        {
            switch (Grade)
            {
                case 0:
                    return new Ega3DkVector(0, Times1(Scalars, 1.0D / scalar));
                case 1:
                    return new Ega3DkVector(1, Times3(Scalars, 1.0D / scalar));
                case 2:
                    return new Ega3DkVector(2, Times3(Scalars, 1.0D / scalar));
                case 3:
                    return new Ega3DkVector(3, Times1(Scalars, 1.0D / scalar));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        public Ega3DkVector Inverse
        {
            get
            {
                var scalar = Norm2;
        
                if (scalar.IsNearZero())
                    throw new InvalidOperationException("Null blade has no inverse");
        
                switch (Grade)
                {
                    case 0:
                        return new Ega3DkVector(0, Times1(Scalars, 1.0D / scalar));
                    case 1:
                        return new Ega3DkVector(1, Times3(Scalars, 1.0D / scalar));
                    case 2:
                        return new Ega3DkVector(2, Times3(Scalars, -1.0D / scalar));
                    case 3:
                        return new Ega3DkVector(3, Times1(Scalars, -1.0D / scalar));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        public Ega3DkVector EuclideanInverse
        {
            get
            {
                var scalar = EMag2;
        
                if (scalar.IsNearZero())
                    throw new InvalidOperationException("Null blade has no inverse");
        
                switch (Grade)
                {
                    case 0:
                        return new Ega3DkVector(0, Times1(Scalars, 1.0D / scalar));
                    case 1:
                        return new Ega3DkVector(1, Times3(Scalars, 1.0D / scalar));
                    case 2:
                        return new Ega3DkVector(2, Times3(Scalars, -1.0D / scalar));
                    case 3:
                        return new Ega3DkVector(3, Times1(Scalars, -1.0D / scalar));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        public Ega3DkVector EuclideanDual
        {
            get
            {
                if (IsZero)
                    return Ega3DMultivector.Zero;
        
                switch (Grade)
                {
                    case 0:
                        return new Ega3DkVector(3, EuclideanDual0(Scalars));
                    case 1:
                        return new Ega3DkVector(2, EuclideanDual1(Scalars));
                    case 2:
                        return new Ega3DkVector(1, EuclideanDual2(Scalars));
                    case 3:
                        return new Ega3DkVector(0, EuclideanDual3(Scalars));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }

        public int SelfDPGrade() { return 0; }
    }
}
