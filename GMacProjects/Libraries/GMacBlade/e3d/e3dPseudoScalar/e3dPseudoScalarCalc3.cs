namespace GMacModel.e3d
{
    public sealed partial class e3dPseudoScalar : e3dMultivector
    {
        
        public e3dPseudoScalar OP(e3dMultivector3 mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.0785069+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 2 used, 3 not used, 5 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.0805070+02:00
            
            return result;
        }
        
        public e3dMultivector12 GP(e3dMultivector3 mv)
        {
            var result = new e3dMultivector12();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.0825071+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.875 average, 7 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.0835071+02:00
            
            return result;
        }
        
        public e3dZero LCP(e3dMultivector3 mv)
        {
            return Zero;
        }
        
        public e3dMultivector12 RCP(e3dMultivector3 mv)
        {
            var result = new e3dMultivector12();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.0905075+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 5 used, 0 not used, 5 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.875 average, 7 total.
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
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            
            
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.0915076+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector3 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector11 Add(e3dMultivector3 mv)
        {
            return new e3dMultivector11()
            {
                G3I0 = G3I0,
                G0I0 = mv.G0I0,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2
            };
        }
        
        public e3dMultivector11 Subtract(e3dMultivector3 mv)
        {
            return new e3dMultivector11()
            {
                G3I0 = G3I0,
                G0I0 = -mv.G0I0,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2
            };
        }
        
        public bool IsEqual(e3dMultivector3 mv)
        {
            return !(
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon
            );
        }
        
    }
}