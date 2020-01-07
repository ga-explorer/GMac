using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public static class e3d
   {
      public sealed class Multivector
      {
         public readonly double[] Coef = new double[8];
         
         
         public Multivector()
         {
         }
         
         public Multivector(params double[] coefs)
         {
             int i = 0;
             foreach (var coef in coefs.Take(8))
                 Coef[i++] = coef;
         }
         
         public Multivector(IEnumerable<double> coefs)
         {
             int i = 0;
             foreach (var coef in coefs.Take(8))
                 Coef[i++] = coef;
         }
         
         
      }
      
      public static readonly main.e3d.Multivector I = new main.e3d.Multivector(0, 0, 0, 0, 0, 0, 0, 1);
      
      public static readonly main.e3d.Multivector Ii = new main.e3d.Multivector(0, 0, 0, 0, 0, 0, 0, -1);
      
      public static main.e3d.Multivector UnaryMinus(main.e3d.Multivector mv1)
      {
         var result = new main.e3d.Multivector();
         
         
         result.Coef[0] = (-1 * mv1.Coef[0]);
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = (-1 * mv1.Coef[3]);
         result.Coef[4] = (-1 * mv1.Coef[4]);
         result.Coef[5] = (-1 * mv1.Coef[5]);
         result.Coef[6] = (-1 * mv1.Coef[6]);
         result.Coef[7] = (-1 * mv1.Coef[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector GradeInversion(main.e3d.Multivector mv1)
      {
         var result = new main.e3d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = mv1.Coef[3];
         result.Coef[4] = (-1 * mv1.Coef[4]);
         result.Coef[5] = mv1.Coef[5];
         result.Coef[6] = mv1.Coef[6];
         result.Coef[7] = (-1 * mv1.Coef[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector Reverse(main.e3d.Multivector mv1)
      {
         var result = new main.e3d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = mv1.Coef[1];
         result.Coef[2] = mv1.Coef[2];
         result.Coef[3] = (-1 * mv1.Coef[3]);
         result.Coef[4] = mv1.Coef[4];
         result.Coef[5] = (-1 * mv1.Coef[5]);
         result.Coef[6] = (-1 * mv1.Coef[6]);
         result.Coef[7] = (-1 * mv1.Coef[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector CliffordConjugate(main.e3d.Multivector mv1)
      {
         var result = new main.e3d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = (-1 * mv1.Coef[3]);
         result.Coef[4] = (-1 * mv1.Coef[4]);
         result.Coef[5] = (-1 * mv1.Coef[5]);
         result.Coef[6] = (-1 * mv1.Coef[6]);
         result.Coef[7] = mv1.Coef[7];
         
         
         return result;
      }
      
      public static main.e3d.Multivector ACP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[7]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[7]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[7]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[6] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[5] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[3] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[7]);
         result.Coef[7] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector CP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         double tempVar0002;
         double tempVar0003;
         
         result.Coef[0] = 0;
         result.Coef[7] = 0;
         tempVar0000 = (mv1.Coef[3] * mv2.Coef[2]);
         tempVar0001 = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempVar0002 = (mv1.Coef[5] * mv2.Coef[4]);
         tempVar0003 = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         result.Coef[1] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[3]);
         tempVar0002 = (mv1.Coef[6] * mv2.Coef[4]);
         tempVar0003 = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         result.Coef[2] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempVar0001 = (mv1.Coef[1] * mv2.Coef[2]);
         tempVar0002 = (mv1.Coef[6] * mv2.Coef[5]);
         tempVar0003 = (-1 * mv1.Coef[5] * mv2.Coef[6]);
         result.Coef[3] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempVar0002 = (mv1.Coef[1] * mv2.Coef[5]);
         tempVar0003 = (mv1.Coef[2] * mv2.Coef[6]);
         result.Coef[4] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[1]);
         tempVar0001 = (-1 * mv1.Coef[6] * mv2.Coef[3]);
         tempVar0002 = (mv1.Coef[1] * mv2.Coef[4]);
         tempVar0003 = (mv1.Coef[3] * mv2.Coef[6]);
         result.Coef[5] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         tempVar0000 = (-1 * mv1.Coef[4] * mv2.Coef[2]);
         tempVar0001 = (mv1.Coef[5] * mv2.Coef[3]);
         tempVar0002 = (mv1.Coef[2] * mv2.Coef[4]);
         tempVar0003 = (-1 * mv1.Coef[3] * mv2.Coef[5]);
         result.Coef[6] = (tempVar0000 + tempVar0001 + tempVar0002 + tempVar0003);
         
         
         return result;
      }
      
      public static main.e3d.Multivector FDP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[5] * mv2.Coef[7]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[7]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[7]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[7]);
         result.Coef[7] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector GP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[5] * mv2.Coef[7]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[6] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[4] * mv2.Coef[7]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[4] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[1] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[3] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[2] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[1] * mv2.Coef[7]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[6] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[5] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[3] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[7]);
         result.Coef[7] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector HIP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[7];
         
         result.Coef[7] = 0;
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[4] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[6] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[4] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[5] = (mv1.Coef[5] * mv2.Coef[7]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[4] * mv2.Coef[7]);
         result.Coef[3] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         result.Coef[5] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[7]);
         result.Coef[6] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector LCP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         result.Coef[7] = (mv1.Coef[0] * mv2.Coef[7]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[7]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[1] = (mv1.Coef[4] * mv2.Coef[7]);
         result.Coef[3] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         result.Coef[5] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[7]);
         result.Coef[6] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector OP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         result.Coef[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         result.Coef[1] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[2]);
         result.Coef[2] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[3]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[4]);
         result.Coef[4] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[4] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[5]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[4] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[6]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[6] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[5] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[3] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[7]);
         result.Coef[7] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector Plus(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         
         result.Coef[0] = (mv1.Coef[0] + mv2.Coef[0]);
         result.Coef[1] = (mv1.Coef[1] + mv2.Coef[1]);
         result.Coef[2] = (mv1.Coef[2] + mv2.Coef[2]);
         result.Coef[3] = (mv1.Coef[3] + mv2.Coef[3]);
         result.Coef[4] = (mv1.Coef[4] + mv2.Coef[4]);
         result.Coef[5] = (mv1.Coef[5] + mv2.Coef[5]);
         result.Coef[6] = (mv1.Coef[6] + mv2.Coef[6]);
         result.Coef[7] = (mv1.Coef[7] + mv2.Coef[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector RCP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         result.Coef[7] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[7] * mv2.Coef[5]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[4]);
         result.Coef[3] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         result.Coef[5] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         result.Coef[6] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector Subtract(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         var result = new main.e3d.Multivector();
         
         double tempVar0000;
         
         tempVar0000 = (-1 * mv2.Coef[0]);
         result.Coef[0] = (mv1.Coef[0] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[1]);
         result.Coef[1] = (mv1.Coef[1] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[2]);
         result.Coef[2] = (mv1.Coef[2] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[3]);
         result.Coef[3] = (mv1.Coef[3] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[4]);
         result.Coef[4] = (mv1.Coef[4] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[5]);
         result.Coef[5] = (mv1.Coef[5] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[6]);
         result.Coef[6] = (mv1.Coef[6] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[7]);
         result.Coef[7] = (mv1.Coef[7] + tempVar0000);
         
         
         return result;
      }
      
      public static main.e3d.Multivector Divide(main.e3d.Multivector mv1, double s2)
      {
         var result = new main.e3d.Multivector();
         
         double tempVar0000;
         
         tempVar0000 = Math.Pow(s2, -1);
         result.Coef[0] = (mv1.Coef[0] * tempVar0000);
         result.Coef[1] = (mv1.Coef[1] * tempVar0000);
         result.Coef[2] = (mv1.Coef[2] * tempVar0000);
         result.Coef[3] = (mv1.Coef[3] * tempVar0000);
         result.Coef[4] = (mv1.Coef[4] * tempVar0000);
         result.Coef[5] = (mv1.Coef[5] * tempVar0000);
         result.Coef[6] = (mv1.Coef[6] * tempVar0000);
         result.Coef[7] = (mv1.Coef[7] * tempVar0000);
         
         
         return result;
      }
      
      public static double SP(main.e3d.Multivector mv1, main.e3d.Multivector mv2)
      {
         double result;
         
         double[] tempArray = new double[8];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         result = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector vinv(main.e3d.Multivector inMv)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         tempArray[0] = Math.Pow(inMv.Coef[0], 2);
         tempArray[1] = Math.Pow(inMv.Coef[1], 2);
         tempArray[2] = Math.Pow(inMv.Coef[2], 2);
         tempArray[3] = Math.Pow(inMv.Coef[3], 2);
         tempArray[4] = Math.Pow(inMv.Coef[4], 2);
         tempArray[5] = Math.Pow(inMv.Coef[5], 2);
         tempArray[6] = Math.Pow(inMv.Coef[6], 2);
         tempArray[7] = Math.Pow(inMv.Coef[7], 2);
         tempArray[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = Math.Pow(tempArray[0], -1);
         result.Coef[0] = (inMv.Coef[0] * tempArray[0]);
         result.Coef[1] = (inMv.Coef[1] * tempArray[0]);
         result.Coef[2] = (inMv.Coef[2] * tempArray[0]);
         result.Coef[3] = (-1 * inMv.Coef[3] * tempArray[0]);
         result.Coef[4] = (inMv.Coef[4] * tempArray[0]);
         result.Coef[5] = (-1 * inMv.Coef[5] * tempArray[0]);
         result.Coef[6] = (-1 * inMv.Coef[6] * tempArray[0]);
         result.Coef[7] = (-1 * inMv.Coef[7] * tempArray[0]);
         
         
         return result;
      }
      
      public static main.e3d.Multivector div_by_norm(main.e3d.Multivector inMv)
      {
         var result = new main.e3d.Multivector();
         
         double tempVar0000;
         
         tempVar0000 = Math.Pow(inMv.Coef[0], 0.5);
         result.Coef[0] = (0.5 * tempVar0000);
         tempVar0000 = Math.Pow(inMv.Coef[0], -0.5);
         result.Coef[1] = (0.5 * inMv.Coef[1] * tempVar0000);
         result.Coef[2] = (0.5 * inMv.Coef[2] * tempVar0000);
         result.Coef[3] = (0.5 * inMv.Coef[3] * tempVar0000);
         result.Coef[4] = (0.5 * inMv.Coef[4] * tempVar0000);
         result.Coef[5] = (0.5 * inMv.Coef[5] * tempVar0000);
         result.Coef[6] = (0.5 * inMv.Coef[6] * tempVar0000);
         result.Coef[7] = (0.5 * inMv.Coef[7] * tempVar0000);
         
         
         return result;
      }
      
      public static main.e3d.Multivector GetNormalToVectors(main.e3d.Multivector u, main.e3d.Multivector v)
      {
         var result = new main.e3d.Multivector();
         
         double[] tempArray = new double[8];
         
         tempArray[0] = (u.Coef[7] * v.Coef[0]);
         tempArray[1] = (u.Coef[6] * v.Coef[1]);
         tempArray[2] = (-1 * u.Coef[5] * v.Coef[2]);
         tempArray[3] = (u.Coef[4] * v.Coef[3]);
         tempArray[4] = (u.Coef[3] * v.Coef[4]);
         tempArray[5] = (-1 * u.Coef[2] * v.Coef[5]);
         tempArray[6] = (u.Coef[1] * v.Coef[6]);
         tempArray[7] = (u.Coef[0] * v.Coef[7]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (u.Coef[6] * v.Coef[0]);
         tempArray[1] = (-1 * u.Coef[4] * v.Coef[2]);
         tempArray[2] = (u.Coef[2] * v.Coef[4]);
         tempArray[3] = (u.Coef[0] * v.Coef[6]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (u.Coef[3] * v.Coef[0]);
         tempArray[1] = (-1 * u.Coef[2] * v.Coef[1]);
         tempArray[2] = (u.Coef[1] * v.Coef[2]);
         tempArray[3] = (u.Coef[0] * v.Coef[3]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (u.Coef[2] * v.Coef[0]);
         tempArray[1] = (u.Coef[0] * v.Coef[2]);
         result.Coef[5] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (u.Coef[0] * v.Coef[0]);
         result.Coef[7] = (-1 * tempArray[0]);
         tempArray[0] = (u.Coef[5] * v.Coef[0]);
         tempArray[1] = (-1 * u.Coef[4] * v.Coef[1]);
         tempArray[2] = (u.Coef[1] * v.Coef[4]);
         tempArray[3] = (u.Coef[0] * v.Coef[5]);
         tempArray[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         result.Coef[2] = (-1 * tempArray[0]);
         tempArray[0] = (u.Coef[4] * v.Coef[0]);
         tempArray[1] = (u.Coef[0] * v.Coef[4]);
         tempArray[0] = (tempArray[0] + tempArray[1]);
         result.Coef[3] = (-1 * tempArray[0]);
         tempArray[0] = (u.Coef[1] * v.Coef[0]);
         tempArray[1] = (u.Coef[0] * v.Coef[1]);
         tempArray[0] = (tempArray[0] + tempArray[1]);
         result.Coef[6] = (-1 * tempArray[0]);
         
         
         return result;
      }
      
   }
}
