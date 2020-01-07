namespace GMacModel.e3d
{
    public sealed partial class e3dFull : e3dMultivector
    {
        
        public e3dFull OP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:04.5347045+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.47727272727273 average, 65 total.
            //Memory Reads: 2 average, 88 total.
            //Memory Writes: 44 total.
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
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I0);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (G1I1 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I0);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (G1I2 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I1);
            result.G2I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I2 * mv.G0I0);
            tempVar0001 = (G1I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:04.5407049+02:00
            
            return result;
        }
        
        public e3dFull GP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:04.5497054+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 96 sub-expressions, 0 generated temps, 96 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.34615384615385 average, 140 total.
            //Memory Reads: 2 average, 208 total.
            //Memory Writes: 104 total.
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
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I2 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I2);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I2 * mv.G2I2);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I0 * mv.G0I0);
            tempVar0001 = (G1I1 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I2 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I2);
            result.G2I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (G2I1 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G2I2);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G2I1 * mv.G0I0);
            tempVar0001 = (G1I2 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G1I2);
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
            tempVar0001 = (G1I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G0I0 * mv.G2I2);
            result.G2I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G2I2 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I0 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G1I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I0 * mv.G2I2);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:04.5607060+02:00
            
            return result;
        }
        
        public e3dMultivector7 LCP(e3dMultivector7 mv)
        {
            var result = new e3dMultivector7();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:04.5657063+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 14 used, 1 not used, 15 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.375 average, 44 total.
            //Memory Reads: 1.9375 average, 62 total.
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
            //   mv1.#e1# <=> <Variable> G1I0
            //   mv1.#e2# <=> <Variable> G1I1
            //   mv1.#e3# <=> <Variable> G1I2
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
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:04.5707066+02:00
            
            return result;
        }
        
        public e3dFull RCP(e3dMultivector7 mv)
        {
            var result = new e3dFull();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:04.5777070+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 15 used, 0 not used, 15 total.
            //Temp Variables: 36 sub-expressions, 0 generated temps, 36 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 1.38636363636364 average, 61 total.
            //Memory Reads: 2 average, 88 total.
            //Memory Writes: 44 total.
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
            tempVar0000 = (-1 * G1I0 * mv.G0I0);
            tempVar0001 = (-1 * G2I0 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I1 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I2);
            result.G1I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I1 * mv.G0I0);
            tempVar0001 = (G2I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G2I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G3I0 * mv.G2I1);
            result.G1I1 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G1I2 * mv.G0I0);
            tempVar0001 = (G2I1 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G3I0 * mv.G2I0);
            result.G1I2 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:04.5827073+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector7 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:04.5857075+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 14 used, 1 not used, 15 total.
            //Temp Variables: 12 sub-expressions, 0 generated temps, 12 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.30769230769231 average, 17 total.
            //Memory Reads: 2 average, 26 total.
            //Memory Writes: 13 total.
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
            tempVar0001 = (-1 * G1I0 * mv.G1I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I1 * mv.G1I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (-1 * G1I2 * mv.G1I2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I0 * mv.G2I0);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I1 * mv.G2I1);
            tempVar0000 = (tempVar0000 + tempVar0001);
            tempVar0001 = (G2I2 * mv.G2I2);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:04.5877076+02:00
            
            return result;
        }
        
        public e3dFull Add(e3dMultivector7 mv)
        {
            return new e3dFull()
            {
                G3I0 = G3I0,
                G0I0 = G0I0 + mv.G0I0,
                G1I0 = G1I0 + mv.G1I0,
                G1I1 = G1I1 + mv.G1I1,
                G1I2 = G1I2 + mv.G1I2,
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
                G0I0 = G0I0 - mv.G0I0,
                G1I0 = G1I0 - mv.G1I0,
                G1I1 = G1I1 - mv.G1I1,
                G1I2 = G1I2 - mv.G1I2,
                G2I0 = G2I0 - mv.G2I0,
                G2I1 = G2I1 - mv.G2I1,
                G2I2 = G2I2 - mv.G2I2
            };
        }
        
        public bool IsEqual(e3dMultivector7 mv)
        {
            return !(
                G3I0 <= -Epsilon || G3I0 >= Epsilon || 
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon || 
                (G1I0 - mv.G1I0) <= -Epsilon || (G1I0 - mv.G1I0) >= Epsilon || 
                (G1I1 - mv.G1I1) <= -Epsilon || (G1I1 - mv.G1I1) >= Epsilon || 
                (G1I2 - mv.G1I2) <= -Epsilon || (G1I2 - mv.G1I2) >= Epsilon || 
                (G2I0 - mv.G2I0) <= -Epsilon || (G2I0 - mv.G2I0) >= Epsilon || 
                (G2I1 - mv.G2I1) <= -Epsilon || (G2I1 - mv.G2I1) >= Epsilon || 
                (G2I2 - mv.G2I2) <= -Epsilon || (G2I2 - mv.G2I2) >= Epsilon
            );
        }
        
    }
}