using System.IO;

namespace Ega3D
{
    /// <summary>
    /// This class represents a mutable outermorphism in the Ega3D frame by only storing a 3 by 3
    /// matrix of the original vector linear transform and computing the other k-vectors matrices as needed
    /// </summary>
    public sealed partial class Ega3DOutermorphism
    {
        public double[,] Scalars { get; private set; }
        
        
        public Ega3DOutermorphism()
        {
            Scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        }
        
        private Ega3DOutermorphism(double[,] scalars)
        {
            Scalars = scalars;
        }
        
        
        public Ega3DOutermorphism Transpose()
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions,
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = Scalars[0, 0];
            scalars[0, 1] = Scalars[1, 0];
            scalars[0, 2] = Scalars[2, 0];
            scalars[1, 0] = Scalars[0, 1];
            scalars[1, 1] = Scalars[1, 1];
            scalars[1, 2] = Scalars[2, 1];
            scalars[2, 0] = Scalars[0, 2];
            scalars[2, 1] = Scalars[1, 2];
            scalars[2, 2] = Scalars[2, 2];
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public double MetricDeterminant()
        {
            double det = 0;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:41:57.9310292+02:00
            //Macro: geometry3d.Ega3D.DetOM
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.28571428571429 average, 18 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result = variable: det
            //   om.ImageV1.#e1# = variable: Scalars[0, 0]
            //   om.ImageV2.#e1# = variable: Scalars[0, 1]
            //   om.ImageV3.#e1# = variable: Scalars[0, 2]
            //   om.ImageV1.#e2# = variable: Scalars[1, 0]
            //   om.ImageV2.#e2# = variable: Scalars[1, 1]
            //   om.ImageV3.#e2# = variable: Scalars[1, 2]
            //   om.ImageV1.#e3# = variable: Scalars[2, 0]
            //   om.ImageV2.#e3# = variable: Scalars[2, 1]
            //   om.ImageV3.#e3# = variable: Scalars[2, 2]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = -1 * Scalars[0, 2] * Scalars[1, 1];
            tempVar0001 = Scalars[0, 1] * Scalars[1, 2];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = Scalars[2, 0] * tempVar0000;
            tempVar0001 = -1 * Scalars[0, 2] * Scalars[2, 1];
            tempVar0002 = Scalars[0, 1] * Scalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = -1 * Scalars[1, 0] * tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * Scalars[1, 2] * Scalars[2, 1];
            tempVar0002 = Scalars[1, 1] * Scalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = Scalars[0, 0] * tempVar0001;
            det = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:41:57.9320283+02:00
            
        
            return det;
        }
        
        public double EuclideanDeterminant()
        {
            double det = 0;
        
            //Begin GMac Macro Code Generation, 2019-09-18T22:42:02.8279700+02:00
            //Macro: geometry3d.Ega3D.EDetOM
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 13 sub-expressions, 0 generated temps, 13 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.28571428571429 average, 18 total.
            //Memory Reads: 2 average, 28 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result = variable: det
            //   om.ImageV1.#e1# = variable: Scalars[0, 0]
            //   om.ImageV2.#e1# = variable: Scalars[0, 1]
            //   om.ImageV3.#e1# = variable: Scalars[0, 2]
            //   om.ImageV1.#e2# = variable: Scalars[1, 0]
            //   om.ImageV2.#e2# = variable: Scalars[1, 1]
            //   om.ImageV3.#e2# = variable: Scalars[1, 2]
            //   om.ImageV1.#e3# = variable: Scalars[2, 0]
            //   om.ImageV2.#e3# = variable: Scalars[2, 1]
            //   om.ImageV3.#e3# = variable: Scalars[2, 2]
            
            double tempVar0000;
            double tempVar0001;
            double tempVar0002;
            
            tempVar0000 = -1 * Scalars[0, 2] * Scalars[1, 1];
            tempVar0001 = Scalars[0, 1] * Scalars[1, 2];
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0000 = Scalars[2, 0] * tempVar0000;
            tempVar0001 = -1 * Scalars[0, 2] * Scalars[2, 1];
            tempVar0002 = Scalars[0, 1] * Scalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = -1 * Scalars[1, 0] * tempVar0001;
            tempVar0000 = tempVar0000 + tempVar0001;
            tempVar0001 = -1 * Scalars[1, 2] * Scalars[2, 1];
            tempVar0002 = Scalars[1, 1] * Scalars[2, 2];
            tempVar0001 = tempVar0001 + tempVar0002;
            tempVar0001 = Scalars[0, 0] * tempVar0001;
            det = tempVar0000 + tempVar0001;
            
            //Finish GMac Macro Code Generation, 2019-09-18T22:42:02.8289686+02:00
            
        
            return det;
        }
        
        public Ega3DkVector Map(Ega3DkVector blade)
        {
            if (blade.IsZero)
                return Ega3DMultivector.Zero;
        
            switch (blade.Grade)
            {
                case 0:
                    return blade;
                case 1:
                    return new Ega3DkVector(1, Map_1(Scalars, blade.Scalars));
                case 2:
                    return new Ega3DkVector(2, Map_2(Scalars, blade.Scalars));
                case 3:
                    return new Ega3DkVector(3, Map_3(Scalars, blade.Scalars));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        
        public static Ega3DOutermorphism operator +(Ega3DOutermorphism om1, Ega3DOutermorphism om2)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = om1.Scalars[0, 0] + om2.Scalars[0, 0];
            scalars[0, 1] = om1.Scalars[0, 1] + om2.Scalars[0, 1];
            scalars[0, 2] = om1.Scalars[0, 2] + om2.Scalars[0, 2];
            scalars[1, 0] = om1.Scalars[1, 0] + om2.Scalars[1, 0];
            scalars[1, 1] = om1.Scalars[1, 1] + om2.Scalars[1, 1];
            scalars[1, 2] = om1.Scalars[1, 2] + om2.Scalars[1, 2];
            scalars[2, 0] = om1.Scalars[2, 0] + om2.Scalars[2, 0];
            scalars[2, 1] = om1.Scalars[2, 1] + om2.Scalars[2, 1];
            scalars[2, 2] = om1.Scalars[2, 2] + om2.Scalars[2, 2];
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public static Ega3DOutermorphism operator -(Ega3DOutermorphism om1, Ega3DOutermorphism om2)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = om1.Scalars[0, 0] - om2.Scalars[0, 0];
            scalars[0, 1] = om1.Scalars[0, 1] - om2.Scalars[0, 1];
            scalars[0, 2] = om1.Scalars[0, 2] - om2.Scalars[0, 2];
            scalars[1, 0] = om1.Scalars[1, 0] - om2.Scalars[1, 0];
            scalars[1, 1] = om1.Scalars[1, 1] - om2.Scalars[1, 1];
            scalars[1, 2] = om1.Scalars[1, 2] - om2.Scalars[1, 2];
            scalars[2, 0] = om1.Scalars[2, 0] - om2.Scalars[2, 0];
            scalars[2, 1] = om1.Scalars[2, 1] - om2.Scalars[2, 1];
            scalars[2, 2] = om1.Scalars[2, 2] - om2.Scalars[2, 2];
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public static Ega3DOutermorphism operator *(Ega3DOutermorphism om1, Ega3DOutermorphism om2)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = om1.Scalars[0, 0] * om2.Scalars[0, 0] + om1.Scalars[0, 1] * om2.Scalars[1, 0] + om1.Scalars[0, 2] * om2.Scalars[2, 0];
            scalars[0, 1] = om1.Scalars[0, 0] * om2.Scalars[0, 1] + om1.Scalars[0, 1] * om2.Scalars[1, 1] + om1.Scalars[0, 2] * om2.Scalars[2, 1];
            scalars[0, 2] = om1.Scalars[0, 0] * om2.Scalars[0, 2] + om1.Scalars[0, 1] * om2.Scalars[1, 2] + om1.Scalars[0, 2] * om2.Scalars[2, 2];
            scalars[1, 0] = om1.Scalars[1, 0] * om2.Scalars[0, 0] + om1.Scalars[1, 1] * om2.Scalars[1, 0] + om1.Scalars[1, 2] * om2.Scalars[2, 0];
            scalars[1, 1] = om1.Scalars[1, 0] * om2.Scalars[0, 1] + om1.Scalars[1, 1] * om2.Scalars[1, 1] + om1.Scalars[1, 2] * om2.Scalars[2, 1];
            scalars[1, 2] = om1.Scalars[1, 0] * om2.Scalars[0, 2] + om1.Scalars[1, 1] * om2.Scalars[1, 2] + om1.Scalars[1, 2] * om2.Scalars[2, 2];
            scalars[2, 0] = om1.Scalars[2, 0] * om2.Scalars[0, 0] + om1.Scalars[2, 1] * om2.Scalars[1, 0] + om1.Scalars[2, 2] * om2.Scalars[2, 0];
            scalars[2, 1] = om1.Scalars[2, 0] * om2.Scalars[0, 1] + om1.Scalars[2, 1] * om2.Scalars[1, 1] + om1.Scalars[2, 2] * om2.Scalars[2, 1];
            scalars[2, 2] = om1.Scalars[2, 0] * om2.Scalars[0, 2] + om1.Scalars[2, 1] * om2.Scalars[1, 2] + om1.Scalars[2, 2] * om2.Scalars[2, 2];
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public static Ega3DOutermorphism operator *(double scalar, Ega3DOutermorphism om)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = om.Scalars[0, 0] * scalar;
            scalars[0, 1] = om.Scalars[0, 1] * scalar;
            scalars[0, 2] = om.Scalars[0, 2] * scalar;
            scalars[1, 0] = om.Scalars[1, 0] * scalar;
            scalars[1, 1] = om.Scalars[1, 1] * scalar;
            scalars[1, 2] = om.Scalars[1, 2] * scalar;
            scalars[2, 0] = om.Scalars[2, 0] * scalar;
            scalars[2, 1] = om.Scalars[2, 1] * scalar;
            scalars[2, 2] = om.Scalars[2, 2] * scalar;
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public static Ega3DOutermorphism operator *(Ega3DOutermorphism om, double scalar)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = om.Scalars[0, 0] * scalar;
            scalars[0, 1] = om.Scalars[0, 1] * scalar;
            scalars[0, 2] = om.Scalars[0, 2] * scalar;
            scalars[1, 0] = om.Scalars[1, 0] * scalar;
            scalars[1, 1] = om.Scalars[1, 1] * scalar;
            scalars[1, 2] = om.Scalars[1, 2] * scalar;
            scalars[2, 0] = om.Scalars[2, 0] * scalar;
            scalars[2, 1] = om.Scalars[2, 1] * scalar;
            scalars[2, 2] = om.Scalars[2, 2] * scalar;
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public static Ega3DOutermorphism operator /(Ega3DOutermorphism om, double scalar)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = om.Scalars[0, 0] / scalar;
            scalars[0, 1] = om.Scalars[0, 1] / scalar;
            scalars[0, 2] = om.Scalars[0, 2] / scalar;
            scalars[1, 0] = om.Scalars[1, 0] / scalar;
            scalars[1, 1] = om.Scalars[1, 1] / scalar;
            scalars[1, 2] = om.Scalars[1, 2] / scalar;
            scalars[2, 0] = om.Scalars[2, 0] / scalar;
            scalars[2, 1] = om.Scalars[2, 1] / scalar;
            scalars[2, 2] = om.Scalars[2, 2] / scalar;
        
            return new Ega3DOutermorphism(scalars);
        }
        
        public static Ega3DOutermorphism operator -(Ega3DOutermorphism om)
        {
            var scalars = new double[
                Ega3DUtils.VectorSpaceDimensions, 
                Ega3DUtils.VectorSpaceDimensions
            ];
        
            scalars[0, 0] = -om.Scalars[0, 0];
            scalars[0, 1] = -om.Scalars[0, 1];
            scalars[0, 2] = -om.Scalars[0, 2];
            scalars[1, 0] = -om.Scalars[1, 0];
            scalars[1, 1] = -om.Scalars[1, 1];
            scalars[1, 2] = -om.Scalars[1, 2];
            scalars[2, 0] = -om.Scalars[2, 0];
            scalars[2, 1] = -om.Scalars[2, 1];
            scalars[2, 2] = -om.Scalars[2, 2];
        
            return new Ega3DOutermorphism(scalars);
        }
        
    }
}
