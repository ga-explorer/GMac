namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector11 : e3dMultivector
    {
        
        public e3dPseudoScalar OP(e3dPseudoScalar mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.4205836+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 2 used, 4 not used, 6 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.4205836+02:00
            
            return result;
        }
        
        public e3dMultivector13 GP(e3dPseudoScalar mv)
        {
            var result = new e3dMultivector13();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.4245838+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.4255839+02:00
            
            return result;
        }
        
        public e3dMultivector13 LCP(e3dPseudoScalar mv)
        {
            var result = new e3dMultivector13();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.4305842+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 1 average, 8 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.4315843+02:00
            
            return result;
        }
        
        public e3dScalar RCP(e3dPseudoScalar mv)
        {
            var result = new e3dScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.4335844+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 2 used, 4 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.125 average, 1 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G0I0 = (G3I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.4355845+02:00
            
            return result;
        }
        
        public double SP(e3dPseudoScalar mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.4365845+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 2 used, 4 not used, 6 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 1 total.
            //Computations: 1 average, 1 total.
            //Memory Reads: 2 average, 2 total.
            //Memory Writes: 1 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result = (G3I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.4365845+02:00
            
            return result;
        }
        
        public e3dMultivector11 Add(e3dPseudoScalar mv)
        {
            return new e3dMultivector11()
            {
                G0I0 = G0I0,
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G3I0 = G3I0 + mv.G3I0
            };
        }
        
        public e3dMultivector11 Subtract(e3dPseudoScalar mv)
        {
            return new e3dMultivector11()
            {
                G0I0 = G0I0,
                G1I0 = G1I0,
                G1I1 = G1I1,
                G1I2 = G1I2,
                G3I0 = G3I0 - mv.G3I0
            };
        }
        
        public bool IsEqual(e3dPseudoScalar mv)
        {
            return !(
                G0I0 <= -Epsilon || G0I0 >= Epsilon || 
                G1I0 <= -Epsilon || G1I0 >= Epsilon || 
                G1I1 <= -Epsilon || G1I1 >= Epsilon || 
                G1I2 <= -Epsilon || G1I2 >= Epsilon || 
                (G3I0 - mv.G3I0) <= -Epsilon || (G3I0 - mv.G3I0) >= Epsilon
            );
        }
        
    }
}