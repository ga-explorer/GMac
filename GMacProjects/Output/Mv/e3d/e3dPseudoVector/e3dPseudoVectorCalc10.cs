using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dPseudoVector : e3dMultivector
    {
        
        public e3dPseudoScalar OP(e3dMultivector10 mv)
        {
            var result = new e3dPseudoScalar();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:59.2244008+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 6 used, 1 not used, 7 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.583333333333333 average, 7 total.
            //Memory Reads: 0.833333333333333 average, 10 total.
            //Memory Writes: 12 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G2I2 * mv.G1I0);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:59.2264009+02:00
            
            return result;
        }
        
        public e3dMultivector10 GP(e3dMultivector10 mv)
        {
            var result = new e3dMultivector10();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:59.2314012+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 7 used, 0 not used, 7 total.
            //Temp Variables: 16 sub-expressions, 0 generated temps, 16 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.08333333333333 average, 26 total.
            //Memory Reads: 1.66666666666667 average, 40 total.
            //Memory Writes: 24 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G2I0 * mv.G1I1);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G3I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I0 * mv.G1I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G3I0);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I1 * mv.G1I0);
            tempVar0001 = (G2I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G3I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G1I0);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:59.2334013+02:00
            
            return result;
        }
        
        public e3dVector LCP(e3dMultivector10 mv)
        {
            var result = new e3dVector();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:59.2364015+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 4 used, 3 not used, 7 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 8 total.
            //Computations: 0.5 average, 4 total.
            //Memory Reads: 0.75 average, 6 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            
            result.G1I0 = (G2I2 * mv.G3I0);
            result.G1I1 = (-1 * G2I1 * mv.G3I0);
            result.G1I2 = (G2I0 * mv.G3I0);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:59.2374016+02:00
            
            return result;
        }
        
        public e3dVector RCP(e3dMultivector10 mv)
        {
            var result = new e3dVector();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:59.2414018+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 6 used, 1 not used, 7 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.857142857142857 average, 12 total.
            //Memory Reads: 1.28571428571429 average, 18 total.
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
            //   mv1.#e1^e2# <=> <Variable> G2I0
            //   mv1.#e1^e3# <=> <Variable> G2I1
            //   mv1.#e2^e3# <=> <Variable> G2I2
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G2I0 * mv.G1I1);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I0 * mv.G1I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G2I1 * mv.G1I0);
            tempVar0001 = (G2I2 * mv.G1I1);
            result.G1I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:59.2434019+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector10 mv)
        {
            return 0.0D;
        }
        
        public e3dMultivector14 Add(e3dMultivector10 mv)
        {
            return new e3dMultivector14()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G1I0 = mv.G1I0,
                G1I1 = mv.G1I1,
                G1I2 = mv.G1I2,
                G3I0 = mv.G3I0
            };
        }
        
        public e3dMultivector14 Subtract(e3dMultivector10 mv)
        {
            return new e3dMultivector14()
            {
                G2I0 = G2I0,
                G2I1 = G2I1,
                G2I2 = G2I2,
                G1I0 = -mv.G1I0,
                G1I1 = -mv.G1I1,
                G1I2 = -mv.G1I2,
                G3I0 = -mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector10 mv)
        {
            return !(
                G2I0 <= -Epsilon || G2I0 >= Epsilon || 
                G2I1 <= -Epsilon || G2I1 >= Epsilon || 
                G2I2 <= -Epsilon || G2I2 >= Epsilon || 
                mv.G1I0 <= -Epsilon || mv.G1I0 >= Epsilon || 
                mv.G1I1 <= -Epsilon || mv.G1I1 >= Epsilon || 
                mv.G1I2 <= -Epsilon || mv.G1I2 >= Epsilon || 
                mv.G3I0 <= -Epsilon || mv.G3I0 >= Epsilon
            );
        }
        
    }
}