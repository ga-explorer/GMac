namespace GMacModel.e3d
{
    public sealed partial class e3dScalar : e3dMultivector
    {
        
        public e3dScalar OP(e3dScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.7333155+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.25 average, 2 total.
            //Memory Reads: 0.25 average, 2 total.
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
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.7343156+02:00
            
            return result;
        }
        
        public e3dScalar GP(e3dScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.7383158+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.25 average, 2 total.
            //Memory Reads: 0.25 average, 2 total.
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
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.7403159+02:00
            
            return result;
        }
        
        public e3dScalar LCP(e3dScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.7423160+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.25 average, 2 total.
            //Memory Reads: 0.25 average, 2 total.
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
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.7433161+02:00
            
            return result;
        }
        
        public e3dScalar RCP(e3dScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.7463163+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.25 average, 2 total.
            //Memory Reads: 0.25 average, 2 total.
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
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.7473163+02:00
            
            return result;
        }
        
        public double SP(e3dScalar mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:57.7483164+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 0 not used, 2 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 2 average, 2 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            
            
            result = (-1 * G0I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:57.7483164+02:00
            
            return result;
        }
        
        public e3dScalar Add(e3dScalar mv)
        {
            return new e3dScalar()
            {
                G0I0 = G0I0 + mv.G0I0
            };
        }
        
        public e3dScalar Subtract(e3dScalar mv)
        {
            return new e3dScalar()
            {
                G0I0 = G0I0 - mv.G0I0
            };
        }
        
        public bool IsEqual(e3dScalar mv)
        {
            return !(
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon
            );
        }
        
    }
}