using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector7 : e3dMultivector
    {
        
        public e3dFull OP(e3dMultivector9 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7244866+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 2 average, 16 total.
            //Memory Reads: 2 average, 16 total.
            //Memory Writes: 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7264867+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector9 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7304870+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.55 average, 31 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (G2I2 * mv.G3I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G2I1 * mv.G3I0);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I2 * mv.G3I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G3I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (G1I1 * mv.G3I0);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G3I0);
            result.G2I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7334871+02:00
            
            return result;
        }
        
        public e3dFull LCP(e3dMultivector9 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7374874+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.625 average, 13 total.
            //Memory Reads: 2 average, 16 total.
            //Memory Writes: 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (G2I2 * mv.G3I0);
            result.G1I1 = (-1 * G2I1 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G1I2 = (G2I0 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7384874+02:00
            
            return result;
        }
        
        public e3dMultivector7 RCP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector7();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7424876+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 8 used, 1 not used, 9 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.75 average, 14 total.
            //Memory Reads: 1.75 average, 14 total.
            //Memory Writes: 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G2I0 = (-1 * G2I0 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G2I1 = (-1 * G2I1 * mv.G0I0);
            result.G2I2 = (-1 * G2I2 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7444878+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector9 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:00.7464879+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 7 not used, 9 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 2 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:00.7464879+02:00
            
            return result;
        }
        
        public e3dFull Add(e3dMultivector9 mv)
        {
            return new e3dFull()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = mv.G3I0,
                G0I0 = G0I0 + mv.G0I0
            };
        }
        
        public e3dFull Subtract(e3dMultivector9 mv)
        {
            return new e3dFull()
            {
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G3I0 = -mv.G3I0,
                G0I0 = G0I0 - mv.G0I0
            };
        }
        
        public bool IsEqual(e3dMultivector9 mv)
        {
            return !(
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon || 
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon
            );
        }
        
    }
}