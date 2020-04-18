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
        private static double[] SelfEGP_000(double[] scalars)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4222171+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv.#E0# = variable: scalars[0]
            
            
            c[0] = scalars[0] * scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4222171+02:00
            
            return c;
        }
        
        
        private static Ega3DkVector[] SelfEGP_00(double[] scalars)
        {
            return new[]
            {
                new Ega3DkVector(0, SelfEGP_000(scalars))
            };
        }
        
        private static double[] SelfEGP_110(double[] scalars)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4322106+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 5 total.
            //Memory Reads: 1.4 average, 7 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            c[0] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4322106+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_112(double[] scalars)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4382088+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 0 used, 3 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 3 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: c[0]
            //   result.#e1^e3# = variable: c[1]
            //   result.#e2^e3# = variable: c[2]
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            
            c[0] = 0;
            
            c[1] = 0;
            
            c[2] = 0;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4382088+02:00
            
            return c;
        }
        
        
        private static Ega3DkVector[] SelfEGP_11(double[] scalars)
        {
            return new[]
            {
                new Ega3DkVector(0, SelfEGP_110(scalars)),
                new Ega3DkVector(2, SelfEGP_112(scalars))
            };
        }
        
        private static double[] SelfEGP_220(double[] scalars)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4501990+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 7 sub-expressions, 0 generated temps, 7 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 1.25 average, 10 total.
            //Memory Writes: 8 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0000 = -tempVar0000;
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0001 = -tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0001 = -tempVar0001;
            c[0] = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4501990+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_222(double[] scalars)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4551979+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 0 used, 3 not used, 3 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 3 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: c[0]
            //   result.#e1^e3# = variable: c[1]
            //   result.#e2^e3# = variable: c[2]
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            
            c[0] = 0;
            
            c[1] = 0;
            
            c[2] = 0;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4561955+02:00
            
            return c;
        }
        
        
        private static Ega3DkVector[] SelfEGP_22(double[] scalars)
        {
            return new[]
            {
                new Ega3DkVector(0, SelfEGP_220(scalars)),
                new Ega3DkVector(2, SelfEGP_222(scalars))
            };
        }
        
        private static double[] SelfEGP_330(double[] scalars)
        {
            var c = new double[1];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4631923+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result.#E0# = variable: c[0]
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            double tempVar0000;
            
            tempVar0000 = scalars[0] * scalars[0];
            c[0] = -tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4631923+02:00
            
            return c;
        }
        
        
        private static double[] SelfEGP_332(double[] scalars)
        {
            var c = new double[3];
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:49.4651894+02:00
            //Macro: geometry3d.Ega3D.SelfEGP
            //Input Variables: 0 used, 1 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 3 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 0 average, 0 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: c[0]
            //   result.#e1^e3# = variable: c[1]
            //   result.#e2^e3# = variable: c[2]
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            
            c[0] = 0;
            
            c[1] = 0;
            
            c[2] = 0;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:49.4661890+02:00
            
            return c;
        }
        
        
        private static Ega3DkVector[] SelfEGP_33(double[] scalars)
        {
            return new[]
            {
                new Ega3DkVector(0, SelfEGP_330(scalars)),
                new Ega3DkVector(2, SelfEGP_332(scalars))
            };
        }
        
        public Ega3DkVector[] SelfEGP()
        {
            if (IsZero)
                return new Ega3DkVector[0];
        
            switch (Grade)
            {
                //grade: 0
                case 0:
                    return SelfEGP_00(Scalars);
                //grade: 1
                case 1:
                    return SelfEGP_11(Scalars);
                //grade: 2
                case 2:
                    return SelfEGP_22(Scalars);
                //grade: 3
                case 3:
                    return SelfEGP_33(Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
