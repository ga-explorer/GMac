using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector10 : e3dMultivector
    {
        
        public e3dMultivector14 OP(e3dMultivector11 mv)
        {
            var result = new e3dMultivector14();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.9835586+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 8 used, 1 not used, 9 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I1);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.9855587+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector11 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.9915591+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.4375 average, 46 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I1 * mv.G1I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G3I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (G1I2 * mv.G1I0);
            tempVar0001 = (G3I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G3I0);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G1I0);
            tempVar0001 = (G1I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G3I0);
            result.G2I2 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.9955593+02:00
            
            return result;
        }
        
        public e3dMultivector5 LCP(e3dMultivector11 mv)
        {
            var result = new e3dMultivector5();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.9995595+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 8 used, 1 not used, 9 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.07142857142857 average, 15 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G2I0 = (-1 * G1I2 * mv.G3I0);
            result.G2I1 = (G1I1 * mv.G3I0);
            result.G2I2 = (-1 * G1I0 * mv.G3I0);
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.0005596+02:00
            
            return result;
        }
        
        public e3dFull RCP(e3dMultivector11 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.0055599+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 9 used, 0 not used, 9 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.64285714285714 average, 23 total.
            //Memory Reads: 2 average, 28 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G1I0 = (-1 * G1I0 * mv.G0I0);
            result.G1I1 = (-1 * G1I1 * mv.G0I0);
            result.G2I0 = (-1 * G3I0 * mv.G1I2);
            result.G1I2 = (-1 * G1I2 * mv.G0I0);
            result.G2I1 = (G3I0 * mv.G1I1);
            result.G2I2 = (-1 * G3I0 * mv.G1I0);
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.0075600+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector11 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:02.0095601+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 8 used, 1 not used, 9 total.
            //Temp Variables: 6 sub-expressions, 0 generated temps, 6 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.42857142857143 average, 10 total.
            //Memory Reads: 2 average, 14 total.
            //Memory Writes: 7 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1# <=> <Variable> mv.G1I0
            //   mv2.#e2# <=> <Variable> mv.G1I1
            //   mv2.#e3# <=> <Variable> mv.G1I2
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G1I0 * mv.G1I0);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G3I0);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:02.0115602+02:00
            
            return result;
        }
        
        public e3dMultivector11 Add(e3dMultivector11 mv)
        {
            return new e3dMultivector11()
            {
                G0I0 = mv.G0I0,
                G1I0 = G1I0 + mv.G1I0,
                G1I1 = G1I1 + mv.G1I1,
                G1I2 = G1I2 + mv.G1I2,
                G3I0 = G3I0 + mv.G3I0
            };
        }
        
        public e3dMultivector11 Subtract(e3dMultivector11 mv)
        {
            return new e3dMultivector11()
            {
                G0I0 = -mv.G0I0,
                G1I0 = G1I0 - mv.G1I0,
                G1I1 = G1I1 - mv.G1I1,
                G1I2 = G1I2 - mv.G1I2,
                G3I0 = G3I0 - mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector11 mv)
        {
            return !(
                mv.G0I0 <= -Epsilon || mv.G0I0 >= Epsilon || 
                (G1I0 - mv.G1I0) <= -Epsilon || (G1I0 - mv.G1I0) >= Epsilon || 
                (G1I1 - mv.G1I1) <= -Epsilon || (G1I1 - mv.G1I1) >= Epsilon || 
                (G1I2 - mv.G1I2) <= -Epsilon || (G1I2 - mv.G1I2) >= Epsilon || 
                (G3I0 - mv.G3I0) <= -Epsilon || (G3I0 - mv.G3I0) >= Epsilon
            );
        }
        
    }
}