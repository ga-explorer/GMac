namespace GMacModel.e3d
{
    public sealed partial class e3dScalar : e3dMultivector
    {
        
        public e3dMultivector6 OP(e3dMultivector6 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.8263208+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.5 average, 12 total.
            //Memory Reads: 1.5 average, 12 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G2I0 = (-1 * G0I0 * mv.G2I0);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G2I1 = (-1 * G0I0 * mv.G2I1);
            result.G2I2 = (-1 * G0I0 * mv.G2I2);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.8283210+02:00
            
            return result;
        }
        
        public e3dMultivector6 GP(e3dMultivector6 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.8313211+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.5 average, 12 total.
            //Memory Reads: 1.5 average, 12 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G2I0 = (-1 * G0I0 * mv.G2I0);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G2I1 = (-1 * G0I0 * mv.G2I1);
            result.G2I2 = (-1 * G0I0 * mv.G2I2);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.8333212+02:00
            
            return result;
        }
        
        public e3dMultivector6 LCP(e3dMultivector6 mv)
        {
            var result = new e3dMultivector6();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.8363214+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1.5 average, 12 total.
            //Memory Reads: 1.5 average, 12 total.
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
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            
            result.G1I0 = (-1 * G0I0 * mv.G1I0);
            result.G1I1 = (-1 * G0I0 * mv.G1I1);
            result.G2I0 = (-1 * G0I0 * mv.G2I0);
            result.G1I2 = (-1 * G0I0 * mv.G1I2);
            result.G2I1 = (-1 * G0I0 * mv.G2I1);
            result.G2I2 = (-1 * G0I0 * mv.G2I2);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.8383215+02:00
            
            return result;
        }
        
        public e3dZero RCP(e3dMultivector6 mv)
        {
            return Zero;
        }
        
        public double SP(e3dMultivector6 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector7 Add(e3dMultivector6 mv)
        {
            return new e3dMultivector7()
            {
                G0I0 = G0I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2
            };
        }
        
        public e3dMultivector7 Subtract(e3dMultivector6 mv)
        {
            return new e3dMultivector7()
            {
                G0I0 = G0I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2
            };
        }
        
        public bool IsEqual(e3dMultivector6 mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon
            );
        }
        
    }
}