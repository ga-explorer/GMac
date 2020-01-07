using System.IO;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        private static cga5dBlade OP2(cga5dVector[] vectors)
        {
            var coefs = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:40 ص
            //Macro: main.cga5d.VectorsOP
            //Input Variables: 10 used, 3 not used, 13 total.
            //Temp Variables: 30 sub-expressions, 0 generated temps, 30 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 10 total.
            //Computations: 1.25 average, 50 total.
            //Memory Reads: 1.75 average, 70 total.
            //Memory Writes: 40 total.
            
            double tempVar0000;
            double tempVar0001;
            
            tempVar0000 = (vectors[0].C2 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C2);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[0] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C3 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C3);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[1] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C3 * vectors[1].C2);
            tempVar0001 = (-1 * vectors[0].C2 * vectors[1].C3);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[2] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C4 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C4);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[3] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C4 * vectors[1].C2);
            tempVar0001 = (-1 * vectors[0].C2 * vectors[1].C4);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[4] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C4 * vectors[1].C3);
            tempVar0001 = (-1 * vectors[0].C3 * vectors[1].C4);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[5] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C1);
            tempVar0001 = (-1 * vectors[0].C1 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[6] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C2);
            tempVar0001 = (-1 * vectors[0].C2 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[7] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C3);
            tempVar0001 = (-1 * vectors[0].C3 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[8] = (-1 * tempVar0000);
            tempVar0000 = (vectors[0].C5 * vectors[1].C4);
            tempVar0001 = (-1 * vectors[0].C4 * vectors[1].C5);
            tempVar0000 = (tempVar0000 + tempVar0001);
            coefs[9] = (-1 * tempVar0000);
            
            return new cga5dBlade(2, coefs);
        }
        
        private static cga5dBlade OP3(cga5dVector[] vectors)
        {
            var coefs = new double[10];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:41 ص
            //Macro: main.cga5d.VectorsOP
            //Input Variables: 15 used, 2 not used, 17 total.
            //Temp Variables: 70 sub-expressions, 0 generated temps, 70 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 10 total.
            //Computations: 1.375 average, 110 total.
            //Memory Reads: 2 average, 160 total.
            //Memory Writes: 80 total.
            
            double[] tempArray = new double[9];
            
            tempArray[0] = (vectors[0].C2 * vectors[1].C1);
            tempArray[1] = (-1 * vectors[0].C1 * vectors[1].C2);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * vectors[2].C3 * tempArray[0]);
            tempArray[2] = (vectors[0].C3 * vectors[1].C1);
            tempArray[3] = (-1 * vectors[0].C1 * vectors[1].C3);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (vectors[2].C2 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (vectors[0].C3 * vectors[1].C2);
            tempArray[4] = (-1 * vectors[0].C2 * vectors[1].C3);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * vectors[2].C1 * tempArray[3]);
            coefs[0] = (tempArray[1] + tempArray[4]);
            tempArray[1] = (-1 * vectors[2].C4 * tempArray[0]);
            tempArray[4] = (vectors[0].C4 * vectors[1].C1);
            tempArray[5] = (-1 * vectors[0].C1 * vectors[1].C4);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (vectors[2].C2 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (vectors[0].C4 * vectors[1].C2);
            tempArray[6] = (-1 * vectors[0].C2 * vectors[1].C4);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * vectors[2].C1 * tempArray[5]);
            coefs[1] = (tempArray[1] + tempArray[6]);
            tempArray[1] = (-1 * vectors[2].C4 * tempArray[2]);
            tempArray[6] = (vectors[2].C3 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[6]);
            tempArray[6] = (vectors[0].C4 * vectors[1].C3);
            tempArray[7] = (-1 * vectors[0].C3 * vectors[1].C4);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * vectors[2].C1 * tempArray[6]);
            coefs[2] = (tempArray[1] + tempArray[7]);
            tempArray[1] = (-1 * vectors[2].C4 * tempArray[3]);
            tempArray[7] = (vectors[2].C3 * tempArray[5]);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (-1 * vectors[2].C2 * tempArray[6]);
            coefs[3] = (tempArray[1] + tempArray[7]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[0]);
            tempArray[1] = (vectors[0].C5 * vectors[1].C1);
            tempArray[7] = (-1 * vectors[0].C1 * vectors[1].C5);
            tempArray[1] = (tempArray[1] + tempArray[7]);
            tempArray[7] = (vectors[2].C2 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[7] = (vectors[0].C5 * vectors[1].C2);
            tempArray[8] = (-1 * vectors[0].C2 * vectors[1].C5);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * vectors[2].C1 * tempArray[7]);
            coefs[4] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[2]);
            tempArray[2] = (vectors[2].C3 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (vectors[0].C5 * vectors[1].C3);
            tempArray[8] = (-1 * vectors[0].C3 * vectors[1].C5);
            tempArray[2] = (tempArray[2] + tempArray[8]);
            tempArray[8] = (-1 * vectors[2].C1 * tempArray[2]);
            coefs[5] = (tempArray[0] + tempArray[8]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[3]);
            tempArray[3] = (vectors[2].C3 * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * vectors[2].C2 * tempArray[2]);
            coefs[6] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[4]);
            tempArray[1] = (vectors[2].C4 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (vectors[0].C5 * vectors[1].C4);
            tempArray[3] = (-1 * vectors[0].C4 * vectors[1].C5);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * vectors[2].C1 * tempArray[1]);
            coefs[7] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[5]);
            tempArray[3] = (vectors[2].C4 * tempArray[7]);
            tempArray[0] = (tempArray[0] + tempArray[3]);
            tempArray[3] = (-1 * vectors[2].C2 * tempArray[1]);
            coefs[8] = (tempArray[0] + tempArray[3]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[6]);
            tempArray[2] = (vectors[2].C4 * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * vectors[2].C3 * tempArray[1]);
            coefs[9] = (tempArray[0] + tempArray[1]);
            
            return new cga5dBlade(3, coefs);
        }
        
        private static cga5dBlade OP4(cga5dVector[] vectors)
        {
            var coefs = new double[5];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:41 ص
            //Macro: main.cga5d.VectorsOP
            //Input Variables: 20 used, 1 not used, 21 total.
            //Temp Variables: 115 sub-expressions, 0 generated temps, 115 total.
            //Target Temp Variables: 14 total.
            //Output Variables: 5 total.
            //Computations: 1.33333333333333 average, 160 total.
            //Memory Reads: 1.95833333333333 average, 235 total.
            //Memory Writes: 120 total.
            
            double[] tempArray = new double[14];
            
            tempArray[0] = (vectors[0].C2 * vectors[1].C1);
            tempArray[1] = (-1 * vectors[0].C1 * vectors[1].C2);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * vectors[2].C3 * tempArray[0]);
            tempArray[2] = (vectors[0].C3 * vectors[1].C1);
            tempArray[3] = (-1 * vectors[0].C1 * vectors[1].C3);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (vectors[2].C2 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (vectors[0].C3 * vectors[1].C2);
            tempArray[4] = (-1 * vectors[0].C2 * vectors[1].C3);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * vectors[2].C1 * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * vectors[3].C4 * tempArray[1]);
            tempArray[5] = (-1 * vectors[2].C4 * tempArray[0]);
            tempArray[6] = (vectors[0].C4 * vectors[1].C1);
            tempArray[7] = (-1 * vectors[0].C1 * vectors[1].C4);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (vectors[2].C2 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (vectors[0].C4 * vectors[1].C2);
            tempArray[8] = (-1 * vectors[0].C2 * vectors[1].C4);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * vectors[2].C1 * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[8]);
            tempArray[8] = (vectors[3].C3 * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (-1 * vectors[2].C4 * tempArray[2]);
            tempArray[9] = (vectors[2].C3 * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (vectors[0].C4 * vectors[1].C3);
            tempArray[10] = (-1 * vectors[0].C3 * vectors[1].C4);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * vectors[2].C1 * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[10] = (-1 * vectors[3].C2 * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * vectors[2].C4 * tempArray[3]);
            tempArray[11] = (vectors[2].C3 * tempArray[7]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * vectors[2].C2 * tempArray[9]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (vectors[3].C1 * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[11]);
            coefs[0] = (-1 * tempArray[4]);
            tempArray[1] = (-1 * vectors[3].C5 * tempArray[1]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[0]);
            tempArray[4] = (vectors[0].C5 * vectors[1].C1);
            tempArray[11] = (-1 * vectors[0].C1 * vectors[1].C5);
            tempArray[4] = (tempArray[4] + tempArray[11]);
            tempArray[11] = (vectors[2].C2 * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[11]);
            tempArray[11] = (vectors[0].C5 * vectors[1].C2);
            tempArray[12] = (-1 * vectors[0].C2 * vectors[1].C5);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (-1 * vectors[2].C1 * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[12]);
            tempArray[12] = (vectors[3].C3 * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[12]);
            tempArray[2] = (-1 * vectors[2].C5 * tempArray[2]);
            tempArray[12] = (vectors[2].C3 * tempArray[4]);
            tempArray[2] = (tempArray[2] + tempArray[12]);
            tempArray[12] = (vectors[0].C5 * vectors[1].C3);
            tempArray[13] = (-1 * vectors[0].C3 * vectors[1].C5);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * vectors[2].C1 * tempArray[12]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[13] = (-1 * vectors[3].C2 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            tempArray[3] = (-1 * vectors[2].C5 * tempArray[3]);
            tempArray[13] = (vectors[2].C3 * tempArray[11]);
            tempArray[3] = (tempArray[3] + tempArray[13]);
            tempArray[13] = (-1 * vectors[2].C2 * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[13]);
            tempArray[13] = (vectors[3].C1 * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            coefs[1] = (-1 * tempArray[1]);
            tempArray[1] = (-1 * vectors[3].C5 * tempArray[5]);
            tempArray[0] = (vectors[3].C4 * tempArray[0]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * vectors[2].C5 * tempArray[6]);
            tempArray[4] = (vectors[2].C4 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (vectors[0].C5 * vectors[1].C4);
            tempArray[5] = (-1 * vectors[0].C4 * vectors[1].C5);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (-1 * vectors[2].C1 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[5]);
            tempArray[5] = (-1 * vectors[3].C2 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[5]);
            tempArray[5] = (-1 * vectors[2].C5 * tempArray[7]);
            tempArray[6] = (vectors[2].C4 * tempArray[11]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * vectors[2].C2 * tempArray[4]);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (vectors[3].C1 * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            coefs[2] = (-1 * tempArray[0]);
            tempArray[0] = (-1 * vectors[3].C5 * tempArray[8]);
            tempArray[2] = (vectors[3].C4 * tempArray[2]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (-1 * vectors[3].C3 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * vectors[2].C5 * tempArray[9]);
            tempArray[2] = (vectors[2].C4 * tempArray[12]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * vectors[2].C3 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (vectors[3].C1 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            coefs[3] = (-1 * tempArray[0]);
            tempArray[0] = (-1 * vectors[3].C5 * tempArray[10]);
            tempArray[2] = (vectors[3].C4 * tempArray[3]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[2] = (-1 * vectors[3].C3 * tempArray[5]);
            tempArray[0] = (tempArray[0] + tempArray[2]);
            tempArray[1] = (vectors[3].C2 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            coefs[4] = (-1 * tempArray[0]);
            
            return new cga5dBlade(4, coefs);
        }
        
        private static cga5dBlade OP5(cga5dVector[] vectors)
        {
            var coefs = new double[1];
        
            //GMac Generated Processing Code, 14/04/2015 09:53:41 ص
            //Macro: main.cga5d.VectorsOP
            //Input Variables: 25 used, 0 not used, 25 total.
            //Temp Variables: 123 sub-expressions, 0 generated temps, 123 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 1 total.
            //Computations: 1.34677419354839 average, 167 total.
            //Memory Reads: 2 average, 248 total.
            //Memory Writes: 124 total.
            
            double[] tempArray = new double[15];
            
            tempArray[0] = (vectors[0].C2 * vectors[1].C1);
            tempArray[1] = (-1 * vectors[0].C1 * vectors[1].C2);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * vectors[2].C3 * tempArray[0]);
            tempArray[2] = (vectors[0].C3 * vectors[1].C1);
            tempArray[3] = (-1 * vectors[0].C1 * vectors[1].C3);
            tempArray[2] = (tempArray[2] + tempArray[3]);
            tempArray[3] = (vectors[2].C2 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (vectors[0].C3 * vectors[1].C2);
            tempArray[4] = (-1 * vectors[0].C2 * vectors[1].C3);
            tempArray[3] = (tempArray[3] + tempArray[4]);
            tempArray[4] = (-1 * vectors[2].C1 * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[4] = (-1 * vectors[3].C4 * tempArray[1]);
            tempArray[5] = (-1 * vectors[2].C4 * tempArray[0]);
            tempArray[6] = (vectors[0].C4 * vectors[1].C1);
            tempArray[7] = (-1 * vectors[0].C1 * vectors[1].C4);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (vectors[2].C2 * tempArray[6]);
            tempArray[5] = (tempArray[5] + tempArray[7]);
            tempArray[7] = (vectors[0].C4 * vectors[1].C2);
            tempArray[8] = (-1 * vectors[0].C2 * vectors[1].C4);
            tempArray[7] = (tempArray[7] + tempArray[8]);
            tempArray[8] = (-1 * vectors[2].C1 * tempArray[7]);
            tempArray[5] = (tempArray[5] + tempArray[8]);
            tempArray[8] = (vectors[3].C3 * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[8]);
            tempArray[8] = (-1 * vectors[2].C4 * tempArray[2]);
            tempArray[9] = (vectors[2].C3 * tempArray[6]);
            tempArray[8] = (tempArray[8] + tempArray[9]);
            tempArray[9] = (vectors[0].C4 * vectors[1].C3);
            tempArray[10] = (-1 * vectors[0].C3 * vectors[1].C4);
            tempArray[9] = (tempArray[9] + tempArray[10]);
            tempArray[10] = (-1 * vectors[2].C1 * tempArray[9]);
            tempArray[8] = (tempArray[8] + tempArray[10]);
            tempArray[10] = (-1 * vectors[3].C2 * tempArray[8]);
            tempArray[4] = (tempArray[4] + tempArray[10]);
            tempArray[10] = (-1 * vectors[2].C4 * tempArray[3]);
            tempArray[11] = (vectors[2].C3 * tempArray[7]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (-1 * vectors[2].C2 * tempArray[9]);
            tempArray[10] = (tempArray[10] + tempArray[11]);
            tempArray[11] = (vectors[3].C1 * tempArray[10]);
            tempArray[4] = (tempArray[4] + tempArray[11]);
            tempArray[4] = (-1 * vectors[4].C5 * tempArray[4]);
            tempArray[1] = (-1 * vectors[3].C5 * tempArray[1]);
            tempArray[0] = (-1 * vectors[2].C5 * tempArray[0]);
            tempArray[11] = (vectors[0].C5 * vectors[1].C1);
            tempArray[12] = (-1 * vectors[0].C1 * vectors[1].C5);
            tempArray[11] = (tempArray[11] + tempArray[12]);
            tempArray[12] = (vectors[2].C2 * tempArray[11]);
            tempArray[0] = (tempArray[0] + tempArray[12]);
            tempArray[12] = (vectors[0].C5 * vectors[1].C2);
            tempArray[13] = (-1 * vectors[0].C2 * vectors[1].C5);
            tempArray[12] = (tempArray[12] + tempArray[13]);
            tempArray[13] = (-1 * vectors[2].C1 * tempArray[12]);
            tempArray[0] = (tempArray[0] + tempArray[13]);
            tempArray[13] = (vectors[3].C3 * tempArray[0]);
            tempArray[1] = (tempArray[1] + tempArray[13]);
            tempArray[2] = (-1 * vectors[2].C5 * tempArray[2]);
            tempArray[13] = (vectors[2].C3 * tempArray[11]);
            tempArray[2] = (tempArray[2] + tempArray[13]);
            tempArray[13] = (vectors[0].C5 * vectors[1].C3);
            tempArray[14] = (-1 * vectors[0].C3 * vectors[1].C5);
            tempArray[13] = (tempArray[13] + tempArray[14]);
            tempArray[14] = (-1 * vectors[2].C1 * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[14]);
            tempArray[14] = (-1 * vectors[3].C2 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[3] = (-1 * vectors[2].C5 * tempArray[3]);
            tempArray[14] = (vectors[2].C3 * tempArray[12]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (-1 * vectors[2].C2 * tempArray[13]);
            tempArray[3] = (tempArray[3] + tempArray[14]);
            tempArray[14] = (vectors[3].C1 * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[14]);
            tempArray[1] = (vectors[4].C4 * tempArray[1]);
            tempArray[1] = (tempArray[4] + tempArray[1]);
            tempArray[4] = (-1 * vectors[3].C5 * tempArray[5]);
            tempArray[0] = (vectors[3].C4 * tempArray[0]);
            tempArray[0] = (tempArray[4] + tempArray[0]);
            tempArray[4] = (-1 * vectors[2].C5 * tempArray[6]);
            tempArray[5] = (vectors[2].C4 * tempArray[11]);
            tempArray[4] = (tempArray[4] + tempArray[5]);
            tempArray[5] = (vectors[0].C5 * vectors[1].C4);
            tempArray[6] = (-1 * vectors[0].C4 * vectors[1].C5);
            tempArray[5] = (tempArray[5] + tempArray[6]);
            tempArray[6] = (-1 * vectors[2].C1 * tempArray[5]);
            tempArray[4] = (tempArray[4] + tempArray[6]);
            tempArray[6] = (-1 * vectors[3].C2 * tempArray[4]);
            tempArray[0] = (tempArray[0] + tempArray[6]);
            tempArray[6] = (-1 * vectors[2].C5 * tempArray[7]);
            tempArray[7] = (vectors[2].C4 * tempArray[12]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (-1 * vectors[2].C2 * tempArray[5]);
            tempArray[6] = (tempArray[6] + tempArray[7]);
            tempArray[7] = (vectors[3].C1 * tempArray[6]);
            tempArray[0] = (tempArray[0] + tempArray[7]);
            tempArray[0] = (-1 * vectors[4].C3 * tempArray[0]);
            tempArray[0] = (tempArray[1] + tempArray[0]);
            tempArray[1] = (-1 * vectors[3].C5 * tempArray[8]);
            tempArray[2] = (vectors[3].C4 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * vectors[3].C3 * tempArray[4]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[2] = (-1 * vectors[2].C5 * tempArray[9]);
            tempArray[4] = (vectors[2].C4 * tempArray[13]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (-1 * vectors[2].C3 * tempArray[5]);
            tempArray[2] = (tempArray[2] + tempArray[4]);
            tempArray[4] = (vectors[3].C1 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[4]);
            tempArray[1] = (vectors[4].C2 * tempArray[1]);
            tempArray[0] = (tempArray[0] + tempArray[1]);
            tempArray[1] = (-1 * vectors[3].C5 * tempArray[10]);
            tempArray[3] = (vectors[3].C4 * tempArray[3]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[3] = (-1 * vectors[3].C3 * tempArray[6]);
            tempArray[1] = (tempArray[1] + tempArray[3]);
            tempArray[2] = (vectors[3].C2 * tempArray[2]);
            tempArray[1] = (tempArray[1] + tempArray[2]);
            tempArray[1] = (-1 * vectors[4].C1 * tempArray[1]);
            coefs[0] = (tempArray[0] + tempArray[1]);
            
            return new cga5dBlade(5, coefs);
        }
        
        public static cga5dBlade OP(cga5dVector[] vectors)
        {
            switch (vectors.Length)
            {
                case 0:
                    return ZeroBlade;
                case 1:
                    return vectors[0].ToBlade();
                case 2:
                    return OP2(vectors);
                case 3:
                    return OP3(vectors);
                case 4:
                    return OP4(vectors);
                case 5:
                    return OP5(vectors);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
