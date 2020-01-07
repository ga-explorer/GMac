using System;

namespace GMacModel.e3d
{
    public sealed partial class e3dMultivector9 : e3dMultivector
    {
        
        public e3dMultivector9 OP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector9();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5155319+02:00
            //Macro: geometry3d.e3d.OP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.7 average, 7 total.
            //Memory Reads: 0.8 average, 8 total.
            //Memory Writes: 10 total.
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
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G0I0 = (-1 * G0I0 * mv.G0I0);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G3I0);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5175320+02:00
            
            return result;
        }
        
        public e3dMultivector9 GP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector9();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5205321+02:00
            //Macro: geometry3d.e3d.GP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 4 sub-expressions, 0 generated temps, 4 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.75 average, 9 total.
            //Memory Reads: 1 average, 12 total.
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
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            tempVar0000 = (-1 * G3I0 * mv.G0I0);
            tempVar0001 = (-1 * G0I0 * mv.G3I0);
            result.G3I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5215322+02:00
            
            return result;
        }
        
        public e3dMultivector9 LCP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector9();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5245324+02:00
            //Macro: geometry3d.e3d.LCP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.6 average, 6 total.
            //Memory Reads: 0.8 average, 8 total.
            //Memory Writes: 10 total.
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
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G3I0 = (-1 * G0I0 * mv.G3I0);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5255324+02:00
            
            return result;
        }
        
        public e3dMultivector9 RCP(e3dMultivector9 mv)
        {
            var result = new e3dMultivector9();
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5285326+02:00
            //Macro: geometry3d.e3d.RCP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 8 total.
            //Computations: 0.6 average, 6 total.
            //Memory Reads: 0.8 average, 8 total.
            //Memory Writes: 10 total.
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
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            result.G3I0 = (-1 * G3I0 * mv.G0I0);
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G3I0 * mv.G3I0);
            result.G0I0 = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5295327+02:00
            
            return result;
        }
        
        public double SP(e3dMultivector9 mv)
        {
            var result = 0.0D;
            
            //Bagin GMac Macro Code Generation, 2016-01-11T22:36:01.5305327+02:00
            //Macro: geometry3d.e3d.SP
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 2 sub-expressions, 0 generated temps, 2 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.33333333333333 average, 4 total.
            //Memory Reads: 2 average, 6 total.
            //Memory Writes: 3 total.
            //
            //Macro Binding Data: 
            //   result <=> <Variable> result
            //   mv1.#E0# <=> <Variable> G0I0
            //   mv1.#e1^e2^e3# <=> <Variable> G3I0
            //   mv2.#E0# <=> <Variable> mv.G0I0
            //   mv2.#e1^e2^e3# <=> <Variable> mv.G3I0
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (-1 * G0I0 * mv.G0I0);
            tempVar0001 = (G3I0 * mv.G3I0);
            result = (tempVar0000 + tempVar0001);
            
            //Finish GMac Macro Code Generation, 2016-01-11T22:36:01.5315328+02:00
            
            return result;
        }
        
        public e3dMultivector9 Add(e3dMultivector9 mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0 + mv.G0I0,
                G3I0 = G3I0 + mv.G3I0
            };
        }
        
        public e3dMultivector9 Subtract(e3dMultivector9 mv)
        {
            return new e3dMultivector9()
            {
                G0I0 = G0I0 - mv.G0I0,
                G3I0 = G3I0 - mv.G3I0
            };
        }
        
        public bool IsEqual(e3dMultivector9 mv)
        {
            return !(
                (G0I0 - mv.G0I0) <= -Epsilon || (G0I0 - mv.G0I0) >= Epsilon || 
                (G3I0 - mv.G3I0) <= -Epsilon || (G3I0 - mv.G3I0) >= Epsilon
            );
        }
        
    }
}