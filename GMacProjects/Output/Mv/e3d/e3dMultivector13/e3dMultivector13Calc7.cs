using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector13 : e3dMultivector
    {
        
        public e3dFull OP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.3416363+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.65 average, 33 total.
            //Memory Reads: 2 average, 40 total.
            //Memory Writes: 20 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G2I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G2I1);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G2I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.3456365+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.3516369+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 54 sub-expressions, 0 generated temps, 54 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.35483870967742 average, 84 total.
            //Memory Reads: 2 average, 124 total.
            //Memory Writes: 62 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G1I0);
            tempVar0001 = (-1 * G2I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I2);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I0 * mv.G1I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G2I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I1 * mv.G1I0);
            tempVar0001 = (G2I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (-1 * G3I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I2 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I2);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (G3I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G2I2);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G0I0);
            tempVar0001 = (-1 * G3I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.3586373+02:00
            
            return result;
        }
        
        public e3dMultivector7 LCP(e3dMultivector7 mv)
        {
            var result = new e3dMultivector7();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.3636376+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 11 used, 1 not used, 12 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.42857142857143 average, 20 total.
            //Memory Reads: 1.85714285714286 average, 26 total.
            //Memory Writes: 14 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G2I0 = (-1 * G0I0 * mv.G2I0);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G2I1 = (-1 * G0I0 * mv.G2I1);
            result.G2I2 = (-1 * G0I0 * mv.G2I2);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.3666377+02:00
            
            return result;
        }
        
        public e3dFull RCP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.3736381+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.34375 average, 43 total.
            //Memory Reads: 2 average, 64 total.
            //Memory Writes: 32 total.
            //
            //Macro Binding Data: 
            //   result.#E0# <=> <Variable> result.G0I0
            //   result.#e1# <=> <Variable> result.G1I0
            //   result.#e2# <=> <Variable> result.G1I1
            //   result.#e1^e2# <=> <Variable> result.G2I0
            //   result.#e3# <=> <Variable> result.G1I2
            //   result.#e1^e3# <=> <Variable> result.G2I1
            //   result.#e2^e3# <=> <Variable> result.G2I2
            //   result.#e1^e2^e3# <=> <Variable> result.G3I0
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (-1 * G3I0 * mv.G1I2);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (G3I0 * mv.G1I1);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G0I0);
            tempVar0001 = (-1 * G3I0 * mv.G1I0);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G1I1);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I2);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I0 * mv.G1I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G2I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I1 * mv.G1I0);
            tempVar0001 = (G2I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.3776384+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector7 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:03.3806385+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 8 used, 4 not used, 12 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.14285714285714 average, 8 total.
            //Memory Reads: 2 average, 14 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:03.3816386+02:00
            
            return result;
        }
        
        public e3dFull Add(e3dMultivector7 mv)
        {
            return new e3dFull()
            {
                G3I0 = G3I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G0I0 = G0I0 + mv.G0I0,
                G2I0 = G2I0 + mv.G2I0,
                G2I1 = G2I1 + mv.G2I1,
                G2I2 = G2I2 + mv.G2I2
            };
        }
        
        public e3dFull Subtract(e3dMultivector7 mv)
        {
            return new e3dFull()
            {
                G3I0 = G3I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G0I0 = G0I0 - mv.G0I0,
                G2I0 = G2I0 - mv.G2I0,
                G2I1 = G2I1 - mv.G2I1,
                G2I2 = G2I2 - mv.G2I2
            };
        }
        
        public bool IsEqual(e3dMultivector7 mv)
        {
            return !(
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon || 
                (G2I0 - mv.G2I0) <= -Epsilon || (G2I0 - mv.G2I0) >= Epsilon || 
                (G2I1 - mv.G2I1) <= -Epsilon || (G2I1 - mv.G2I1) >= Epsilon || 
                (G2I2 - mv.G2I2) <= -Epsilon || (G2I2 - mv.G2I2) >= Epsilon
            );
        }
        
    }
}