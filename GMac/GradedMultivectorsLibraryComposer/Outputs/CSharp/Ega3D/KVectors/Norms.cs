using System;
using System.IO;

namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        #region Norm2
        private static double Norm2_0(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:41.7941055+02:00
            //Macro: geometry3d.Ega3D.Norm2
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#E0# = variable: scalars[0]
            
            
            result = scalars[0] * scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:41.8950472+02:00
            
            return result;
        }
        
        
        private static double Norm2_1(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:42.3607582+02:00
            //Macro: geometry3d.Ega3D.Norm2
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 5 total.
            //Memory Reads: 1.4 average, 7 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            result = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:42.3607582+02:00
            
            return result;
        }
        
        
        private static double Norm2_2(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:42.3647551+02:00
            //Macro: geometry3d.Ega3D.Norm2
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 5 total.
            //Memory Reads: 1.4 average, 7 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            result = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:42.3657555+02:00
            
            return result;
        }
        
        
        private static double Norm2_3(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:42.3667548+02:00
            //Macro: geometry3d.Ega3D.Norm2
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            
            result = scalars[0] * scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:42.3667548+02:00
            
            return result;
        }
        
        
        public double Norm2
        {
            get
            {
                if (IsZero)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Norm2_0(Scalars);
                    case 1:
                        return Norm2_1(Scalars);
                    case 2:
                        return Norm2_2(Scalars);
                    case 3:
                        return Norm2_3(Scalars);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region Mag
        private static double Mag_0(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:42.8930454+02:00
            //Macro: geometry3d.Ega3D.Mag
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#E0# = variable: scalars[0]
            
            
            result = Math.Abs(scalars[0]);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:42.8940445+02:00
            
            return result;
        }
        
        
        private static double Mag_1(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0549459+02:00
            //Macro: geometry3d.Ega3D.Mag
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 0.857142857142857 average, 6 total.
            //Memory Reads: 1.28571428571429 average, 9 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = Math.Abs(tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0559465+02:00
            
            return result;
        }
        
        
        private static double Mag_2(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0589425+02:00
            //Macro: geometry3d.Ega3D.Mag
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 0.857142857142857 average, 6 total.
            //Memory Reads: 1.28571428571429 average, 9 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = Math.Abs(tempVar0000);
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0589425+02:00
            
            return result;
        }
        
        
        private static double Mag_3(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0609412+02:00
            //Macro: geometry3d.Ega3D.Mag
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 0 average, 0 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            
            result = Math.Abs(scalars[0]);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0609412+02:00
            
            return result;
        }
        
        
        public double Mag
        {
            get
            {
                if (IsZero)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Mag_0(Scalars);
                    case 1:
                        return Mag_1(Scalars);
                    case 2:
                        return Mag_2(Scalars);
                    case 3:
                        return Mag_3(Scalars);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region Mag2
        private static double Mag2_0(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0699363+02:00
            //Macro: geometry3d.Ega3D.Mag2
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 0.5 average, 1 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#E0# = variable: scalars[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Abs(scalars[0]);
            result = tempVar0000 * tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0699363+02:00
            
            return result;
        }
        
        
        private static double Mag2_1(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0729341+02:00
            //Macro: geometry3d.Ega3D.Mag2
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 0.833333333333333 average, 5 total.
            //Memory Reads: 1.33333333333333 average, 8 total.
            //Memory Writes: 6 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            result = Math.Abs(tempVar0000);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0729341+02:00
            
            return result;
        }
        
        
        private static double Mag2_2(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0759324+02:00
            //Macro: geometry3d.Ega3D.Mag2
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 0.833333333333333 average, 5 total.
            //Memory Reads: 1.33333333333333 average, 8 total.
            //Memory Writes: 6 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            result = Math.Abs(tempVar0000);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0759324+02:00
            
            return result;
        }
        
        
        private static double Mag2_3(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.0789302+02:00
            //Macro: geometry3d.Ega3D.Mag2
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 0.5 average, 1 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            double tempVar0000;
            
            tempVar0000 = Math.Abs(scalars[0]);
            result = tempVar0000 * tempVar0000;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.0789302+02:00
            
            return result;
        }
        
        
        public double Mag2
        {
            get
            {
                if (IsZero)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Mag2_0(Scalars);
                    case 1:
                        return Mag2_1(Scalars);
                    case 2:
                        return Mag2_2(Scalars);
                    case 3:
                        return Mag2_3(Scalars);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region EMag
        private static double EMag_0(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3467673+02:00
            //Macro: geometry3d.Ega3D.EMag
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#E0# = variable: scalars[0]
            
            double tempVar0000;
            
            tempVar0000 = scalars[0] * scalars[0];
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3477667+02:00
            
            return result;
        }
        
        
        private static double EMag_1(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3537638+02:00
            //Macro: geometry3d.Ega3D.EMag
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 6 total.
            //Memory Reads: 1.33333333333333 average, 8 total.
            //Memory Writes: 6 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3537638+02:00
            
            return result;
        }
        
        
        private static double EMag_2(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3577605+02:00
            //Macro: geometry3d.Ega3D.EMag
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 5 sub-expressions, 0 generated temps, 5 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 6 total.
            //Memory Reads: 1.33333333333333 average, 8 total.
            //Memory Writes: 6 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            tempVar0000 = tempVar0000 + tempVar0001;
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3577605+02:00
            
            return result;
        }
        
        
        private static double EMag_3(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3617592+02:00
            //Macro: geometry3d.Ega3D.EMag
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 1 sub-expressions, 0 generated temps, 1 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 2 total.
            //Memory Reads: 1 average, 2 total.
            //Memory Writes: 2 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            double tempVar0000;
            
            tempVar0000 = scalars[0] * scalars[0];
            result = Math.Pow(tempVar0000, 0.5);
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3617592+02:00
            
            return result;
        }
        
        
        public double EMag
        {
            get
            {
                if (IsZero)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return EMag_0(Scalars);
                    case 1:
                        return EMag_1(Scalars);
                    case 2:
                        return EMag_2(Scalars);
                    case 3:
                        return EMag_3(Scalars);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
        #region EMag2
        private static double EMag2_0(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3667547+02:00
            //Macro: geometry3d.Ega3D.EMag2
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#E0# = variable: scalars[0]
            
            
            result = scalars[0] * scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3667547+02:00
            
            return result;
        }
        
        
        private static double EMag2_1(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3697531+02:00
            //Macro: geometry3d.Ega3D.EMag2
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 5 total.
            //Memory Reads: 1.4 average, 7 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1# = variable: scalars[0]
            //   mv.#e2# = variable: scalars[1]
            //   mv.#e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            result = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3697531+02:00
            
            return result;
        }
        
        
        private static double EMag2_2(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3717511+02:00
            //Macro: geometry3d.Ega3D.EMag2
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 5 total.
            //Memory Reads: 1.4 average, 7 total.
            //Memory Writes: 5 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2# = variable: scalars[0]
            //   mv.#e1^e3# = variable: scalars[1]
            //   mv.#e2^e3# = variable: scalars[2]
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = scalars[0] * scalars[0];
            tempVar0001 = scalars[1] * scalars[1];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = scalars[2] * scalars[2];
            result = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3727503+02:00
            
            return result;
        }
        
        
        private static double EMag2_3(double[] scalars)
        {
            var result = 0.0D;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:43.3737507+02:00
            //Macro: geometry3d.Ega3D.EMag2
            //Input Variables: 1 used, 0 not used, 1 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 1 average, 1 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result = variable: result
            //   mv.#e1^e2^e3# = variable: scalars[0]
            
            
            result = scalars[0] * scalars[0];
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:43.3747493+02:00
            
            return result;
        }
        
        
        public double EMag2
        {
            get
            {
                if (IsZero)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return EMag2_0(Scalars);
                    case 1:
                        return EMag2_1(Scalars);
                    case 2:
                        return EMag2_2(Scalars);
                    case 3:
                        return EMag2_3(Scalars);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        #endregion
    }
}
