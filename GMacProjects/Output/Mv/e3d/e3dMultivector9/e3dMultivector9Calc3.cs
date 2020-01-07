using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector9 : e3dMultivector
    {
        
        public e3dMultivector11 OP(e3dMultivector3 mv)
        {
            var result = new e3dMultivector11();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.3775240+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.25 average, 10 total.
            //Memory Reads: 1.25 average, 10 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.3785240+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector3 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.3825242+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.875 average, 15 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.3835243+02:00
            
            return result;
        }
        
        public e3dMultivector3 LCP(e3dMultivector3 mv)
        {
            var result = new e3dMultivector3();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.3865245+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 5 used, 1 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
            //Memory Reads: 1 average, 8 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.3875245+02:00
            
            return result;
        }
        
        public e3dMultivector13 RCP(e3dMultivector3 mv)
        {
            var result = new e3dMultivector13();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.3905247+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.125 average, 9 total.
            //Memory Reads: 1.25 average, 10 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.3915248+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector3 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.3935249+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 4 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 2 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.3945249+02:00
            
            return result;
        }
        
        public e3dMultivector11 Add(e3dMultivector3 mv)
        {
            return new e3dMultivector11()
            {
                G3I0 = G3I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G0I0 = G0I0 + mv.G0I0
            };
        }
        
        public e3dMultivector11 Subtract(e3dMultivector3 mv)
        {
            return new e3dMultivector11()
            {
                G3I0 = G3I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G0I0 = G0I0 - mv.G0I0
            };
        }
        
        public bool IsEqual(e3dMultivector3 mv)
        {
            return !(
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon
            );
        }
        
    }
}