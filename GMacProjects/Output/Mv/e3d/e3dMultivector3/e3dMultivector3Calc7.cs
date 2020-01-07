using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector3 : e3dMultivector
    {
        
        public e3dFull OP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.6153660+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 22 sub-expressions, 0 generated temps, 22 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.5 average, 45 total.
            //Memory Reads: 2 average, 60 total.
            //Memory Writes: 30 total.
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
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I1);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I1);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G2I0);
            tempVar0001 = (G1I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.6193662+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.6243665+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.4375 average, 69 total.
            //Memory Reads: 2 average, 96 total.
            //Memory Writes: 48 total.
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
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I1);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I1);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G2I0);
            tempVar0001 = (G1I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I2 * mv.G2I1);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I2 * mv.G2I2);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G2I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.6303668+02:00
            
            return result;
        }
        
        public e3dMultivector7 LCP(e3dMultivector7 mv)
        {
            var result = new e3dMultivector7();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.6343671+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 11 used, 0 not used, 11 total.
            //Temp Variables: 18 sub-expressions, 0 generated temps, 18 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.46153846153846 average, 38 total.
            //Memory Reads: 1.92307692307692 average, 50 total.
            //Memory Writes: 26 total.
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
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            result.G2I0 = (-1 * G0I0 * mv.G2I0);
            result.G2I1 = (-1 * G0I0 * mv.G2I1);
            result.G2I2 = (-1 * G0I0 * mv.G2I2);
            tempVar0000 = (-1 * G0I0 * mv.G1I0);
            tempVar0001 = (G1I1 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I2 * mv.G2I1);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G1I1);
            tempVar0001 = (-1 * G1I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I2 * mv.G2I2);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G1I2);
            tempVar0001 = (-1 * G1I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G2I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.6373672+02:00
            
            return result;
        }
        
        public e3dMultivector3 RCP(e3dMultivector7 mv)
        {
            var result = new e3dMultivector3();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.6413675+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 8 used, 3 not used, 11 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.21428571428571 average, 17 total.
            //Memory Reads: 1.42857142857143 average, 20 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2# <=> <Variable> mv.G2I0
            //   mv2.#e1^e3# <=> <Variable> mv.G2I1
            //   mv2.#e2^e3# <=> <Variable> mv.G2I2
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.6433676+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector7 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:35:58.6473678+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 8 used, 3 not used, 11 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.57142857142857 average, 11 total.
            //Memory Reads: 2 average, 14 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
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
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:35:58.6483679+02:00
            
            return result;
        }
        
        public e3dMultivector7 Add(e3dMultivector7 mv)
        {
            return new e3dMultivector7()
            {
                G2I0 = mv.G2I0,
                G2I1 = mv.G2I1,
                G2I2 = mv.G2I2,
                G0I0 = G0I0 + mv.G0I0,
                G1I0 = G1I0 + mv.G1I0,
                G1I1 = G1I1 + mv.G1I1,
                G1I2 = G1I2 + mv.G1I2
            };
        }
        
        public e3dMultivector7 Subtract(e3dMultivector7 mv)
        {
            return new e3dMultivector7()
            {
                G2I0 = -mv.G2I0,
                G2I1 = -mv.G2I1,
                G2I2 = -mv.G2I2,
                G0I0 = G0I0 - mv.G0I0,
                G1I0 = G1I0 - mv.G1I0,
                G1I1 = G1I1 - mv.G1I1,
                G1I2 = G1I2 - mv.G1I2
            };
        }
        
        public bool IsEqual(e3dMultivector7 mv)
        {
            return !(
                mv.G2I0 <= -Epsilon || mv.G2I0 >= Epsilon || 
                mv.G2I1 <= -Epsilon || mv.G2I1 >= Epsilon || 
                mv.G2I2 <= -Epsilon || mv.G2I2 >= Epsilon || 
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon || 
                (G1I0 - mv.G1I0) <= -Epsilon || (G1I0 - mv.G1I0) >= Epsilon || 
                (G1I1 - mv.G1I1) <= -Epsilon || (G1I1 - mv.G1I1) >= Epsilon || 
                (G1I2 - mv.G1I2) <= -Epsilon || (G1I2 - mv.G1I2) >= Epsilon
            );
        }
        
    }
}