using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace main
{
   public static class h3d
   {
      public sealed class Multivector
      {
         public readonly double[] Coef = new double[16];
         
         
         public Multivector()
         {
         }
         
         public Multivector(params double[] coefs)
         {
             int i = 0;
             foreach (var coef in coefs.Take(16))
                 Coef[i++] = coef;
         }
         
         public Multivector(IEnumerable<double> coefs)
         {
             int i = 0;
             foreach (var coef in coefs.Take(16))
                 Coef[i++] = coef;
         }
         
         
      }
      
      public static readonly main.h3d.Multivector I = new main.h3d.Multivector(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
      
      public static readonly main.h3d.Multivector I3 = new main.h3d.Multivector(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0);
      
      public static readonly main.h3d.Multivector Ii = new main.h3d.Multivector(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1);
      
      public static main.h3d.Multivector UnaryMinus(main.h3d.Multivector mv1)
      {
         var result = new main.h3d.Multivector();
         
         
         result.Coef[0] = (-1 * mv1.Coef[0]);
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = (-1 * mv1.Coef[3]);
         result.Coef[4] = (-1 * mv1.Coef[4]);
         result.Coef[5] = (-1 * mv1.Coef[5]);
         result.Coef[6] = (-1 * mv1.Coef[6]);
         result.Coef[7] = (-1 * mv1.Coef[7]);
         result.Coef[8] = (-1 * mv1.Coef[8]);
         result.Coef[9] = (-1 * mv1.Coef[9]);
         result.Coef[10] = (-1 * mv1.Coef[10]);
         result.Coef[11] = (-1 * mv1.Coef[11]);
         result.Coef[12] = (-1 * mv1.Coef[12]);
         result.Coef[13] = (-1 * mv1.Coef[13]);
         result.Coef[14] = (-1 * mv1.Coef[14]);
         result.Coef[15] = (-1 * mv1.Coef[15]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector GradeInversion(main.h3d.Multivector mv1)
      {
         var result = new main.h3d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = mv1.Coef[3];
         result.Coef[4] = (-1 * mv1.Coef[4]);
         result.Coef[5] = mv1.Coef[5];
         result.Coef[6] = mv1.Coef[6];
         result.Coef[7] = (-1 * mv1.Coef[7]);
         result.Coef[8] = (-1 * mv1.Coef[8]);
         result.Coef[9] = mv1.Coef[9];
         result.Coef[10] = mv1.Coef[10];
         result.Coef[11] = (-1 * mv1.Coef[11]);
         result.Coef[12] = mv1.Coef[12];
         result.Coef[13] = (-1 * mv1.Coef[13]);
         result.Coef[14] = (-1 * mv1.Coef[14]);
         result.Coef[15] = mv1.Coef[15];
         
         
         return result;
      }
      
      public static main.h3d.Multivector Reverse(main.h3d.Multivector mv1)
      {
         var result = new main.h3d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = mv1.Coef[1];
         result.Coef[2] = mv1.Coef[2];
         result.Coef[3] = (-1 * mv1.Coef[3]);
         result.Coef[4] = mv1.Coef[4];
         result.Coef[5] = (-1 * mv1.Coef[5]);
         result.Coef[6] = (-1 * mv1.Coef[6]);
         result.Coef[7] = (-1 * mv1.Coef[7]);
         result.Coef[8] = mv1.Coef[8];
         result.Coef[9] = (-1 * mv1.Coef[9]);
         result.Coef[10] = (-1 * mv1.Coef[10]);
         result.Coef[11] = (-1 * mv1.Coef[11]);
         result.Coef[12] = (-1 * mv1.Coef[12]);
         result.Coef[13] = (-1 * mv1.Coef[13]);
         result.Coef[14] = (-1 * mv1.Coef[14]);
         result.Coef[15] = mv1.Coef[15];
         
         
         return result;
      }
      
      public static main.h3d.Multivector CliffordConjugate(main.h3d.Multivector mv1)
      {
         var result = new main.h3d.Multivector();
         
         
         result.Coef[0] = mv1.Coef[0];
         result.Coef[1] = (-1 * mv1.Coef[1]);
         result.Coef[2] = (-1 * mv1.Coef[2]);
         result.Coef[3] = (-1 * mv1.Coef[3]);
         result.Coef[4] = (-1 * mv1.Coef[4]);
         result.Coef[5] = (-1 * mv1.Coef[5]);
         result.Coef[6] = (-1 * mv1.Coef[6]);
         result.Coef[7] = mv1.Coef[7];
         result.Coef[8] = (-1 * mv1.Coef[8]);
         result.Coef[9] = (-1 * mv1.Coef[9]);
         result.Coef[10] = (-1 * mv1.Coef[10]);
         result.Coef[11] = mv1.Coef[11];
         result.Coef[12] = (-1 * mv1.Coef[12]);
         result.Coef[13] = mv1.Coef[13];
         result.Coef[14] = mv1.Coef[14];
         result.Coef[15] = mv1.Coef[15];
         
         
         return result;
      }
      
      public static main.h3d.Multivector ACP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[15] * mv2.Coef[15]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         tempArray[4] = (-1 * mv1.Coef[11] * mv2.Coef[10]);
         tempArray[5] = (-1 * mv1.Coef[10] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[13] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[12] * mv2.Coef[13]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[11] * mv2.Coef[9]);
         tempArray[5] = (mv1.Coef[9] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[14] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[12] * mv2.Coef[14]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[11] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[8] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[15] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[12] * mv2.Coef[15]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[13] * mv2.Coef[9]);
         tempArray[5] = (mv1.Coef[14] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[9] * mv2.Coef[13]);
         tempArray[7] = (mv1.Coef[10] * mv2.Coef[14]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[13] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[15] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[8] * mv2.Coef[13]);
         tempArray[7] = (mv1.Coef[10] * mv2.Coef[15]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[14] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[15] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[8] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[9] * mv2.Coef[15]);
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
         tempArray[0] = (mv1.Coef[8] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[11] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[13] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[14] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[13]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[14]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[9] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[11] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[13] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[9]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[13]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[15]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[10] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[11] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[14] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[15] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[10]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[14]);
         tempArray[7] = (mv1.Coef[5] * mv2.Coef[15]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[11] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[10] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[9] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[3] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[10]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[11]);
         result.Coef[11] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[12] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[13] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[14] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[12]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[13]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[15]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[13] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[12] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[9] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[5] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[13]);
         result.Coef[13] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[14] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[12] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[10] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[14]);
         result.Coef[14] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[12] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[10] * mv2.Coef[5]);
         tempArray[3] = (mv1.Coef[9] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[9]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[3] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[15]);
         result.Coef[15] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector CP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[8];
         
         result.Coef[0] = 0;
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[9] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[8] * mv2.Coef[9]);
         tempArray[6] = (-1 * mv1.Coef[15] * mv2.Coef[14]);
         tempArray[7] = (mv1.Coef[14] * mv2.Coef[15]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[10] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[8] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[15] * mv2.Coef[13]);
         tempArray[7] = (-1 * mv1.Coef[13] * mv2.Coef[15]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[6] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[5] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[10] * mv2.Coef[9]);
         tempArray[5] = (-1 * mv1.Coef[9] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[14] * mv2.Coef[13]);
         tempArray[7] = (-1 * mv1.Coef[13] * mv2.Coef[14]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[3] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[12] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[15] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[8] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[11] * mv2.Coef[15]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[4] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[6] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[3] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[12] * mv2.Coef[9]);
         tempArray[5] = (-1 * mv1.Coef[14] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[9] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[11] * mv2.Coef[14]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[4] * mv2.Coef[2]);
         tempArray[1] = (mv1.Coef[5] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[12] * mv2.Coef[10]);
         tempArray[5] = (mv1.Coef[13] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[10] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[11] * mv2.Coef[13]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[8]);
         tempArray[1] = (-1 * mv1.Coef[14] * mv2.Coef[9]);
         tempArray[2] = (mv1.Coef[13] * mv2.Coef[10]);
         tempArray[3] = (mv1.Coef[12] * mv2.Coef[11]);
         tempArray[4] = (-1 * mv1.Coef[11] * mv2.Coef[12]);
         tempArray[5] = (-1 * mv1.Coef[10] * mv2.Coef[13]);
         tempArray[6] = (mv1.Coef[9] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[8] * mv2.Coef[15]);
         result.Coef[7] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[9] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[10] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[12] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[15] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[1] * mv2.Coef[9]);
         tempArray[5] = (mv1.Coef[2] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[4] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[15]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[8] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[10] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[12] * mv2.Coef[5]);
         tempArray[3] = (mv1.Coef[14] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[1] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[3] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[5] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[14]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[8] * mv2.Coef[2]);
         tempArray[1] = (mv1.Coef[9] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[12] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[13] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[2] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[6] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[7] * mv2.Coef[13]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[15] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[14] * mv2.Coef[5]);
         tempArray[2] = (-1 * mv1.Coef[13] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[12] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[7] * mv2.Coef[12]);
         tempArray[5] = (mv1.Coef[6] * mv2.Coef[13]);
         tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[14]);
         tempArray[7] = (mv1.Coef[4] * mv2.Coef[15]);
         result.Coef[11] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[8] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[9] * mv2.Coef[5]);
         tempArray[2] = (mv1.Coef[10] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[11] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[9]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[10]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[11]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[14] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[11] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[10] * mv2.Coef[7]);
         tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[10]);
         tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[11]);
         tempArray[6] = (mv1.Coef[3] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[2] * mv2.Coef[15]);
         result.Coef[13] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[15] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[13] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[11] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[9] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[7] * mv2.Coef[9]);
         tempArray[5] = (mv1.Coef[5] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[3] * mv2.Coef[13]);
         tempArray[7] = (mv1.Coef[1] * mv2.Coef[15]);
         result.Coef[14] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (-1 * mv1.Coef[14] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[13] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[11] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[8] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[7] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[4] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[2] * mv2.Coef[13]);
         tempArray[7] = (mv1.Coef[1] * mv2.Coef[14]);
         result.Coef[15] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector FDP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[15] * mv2.Coef[15]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[9] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[8] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[11] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[10] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[13] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[12] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[15] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[14] * mv2.Coef[15]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[5] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[10] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[11] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[8] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[9] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[14] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[15] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[12] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[13] * mv2.Coef[15]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[11] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[8] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[15] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[12] * mv2.Coef[15]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[12] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[13] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[14] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[15] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[8] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[9] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[10] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[11] * mv2.Coef[15]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[13] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[15] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[8] * mv2.Coef[13]);
         tempArray[7] = (mv1.Coef[10] * mv2.Coef[15]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[7]);
         tempArray[4] = (mv1.Coef[14] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[15] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[8] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[9] * mv2.Coef[15]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[15] * mv2.Coef[8]);
         tempArray[3] = (-1 * mv1.Coef[8] * mv2.Coef[15]);
         result.Coef[7] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[8] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[9] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[10] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[11] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[12] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[13] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[14] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[15] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[0] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[1] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[2] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[3] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[4] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[5] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[6] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[7] * mv2.Coef[15]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[9] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[11] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[13] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[9]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[13]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[15]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[10] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[11] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[14] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[15] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[10]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[14]);
         tempArray[7] = (mv1.Coef[5] * mv2.Coef[15]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[11] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[15] * mv2.Coef[4]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[11]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[15]);
         result.Coef[11] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[12] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[13] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[14] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[12]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[13]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[15]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[13] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[15] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[13]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[15]);
         result.Coef[13] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[14] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[15] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[14]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[15]);
         result.Coef[14] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[15]);
         result.Coef[15] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector GP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[15] * mv2.Coef[15]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[9] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[8] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[11] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[10] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[13] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[12] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[15] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[14] * mv2.Coef[15]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[5] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[10] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[11] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[8] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[9] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[14] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[15] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[12] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[13] * mv2.Coef[15]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[6] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[5] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[4] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[11] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[10] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[9] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[8] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[15] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[14] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[13] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[12] * mv2.Coef[15]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[12] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[13] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[14] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[15] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[8] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[9] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[10] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[11] * mv2.Coef[15]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[4] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[1] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[3] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[13] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[12] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[15] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[14] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[9] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[8] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[11] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[10] * mv2.Coef[15]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[2] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[1] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[14] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[15] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[12] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[13] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[10] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[11] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[8] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[9] * mv2.Coef[15]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[6] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[5] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[3] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[15] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[14] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[13] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[12] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[11] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[10] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[9] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[8] * mv2.Coef[15]);
         result.Coef[7] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[8] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[9] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[10] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[11] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[12] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[13] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[14] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[15] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[0] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[1] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[2] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[3] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[4] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[5] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[6] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[7] * mv2.Coef[15]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[9] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[8] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[11] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[10] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[13] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[12] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[15] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[14] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[1] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[0] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[3] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[2] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[5] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[4] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[7] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[6] * mv2.Coef[15]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[10] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[11] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[8] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[9] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[14] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[15] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[12] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[13] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[2] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[3] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[0] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[1] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[6] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[7] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[4] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[5] * mv2.Coef[15]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[11] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[10] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[9] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[15] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[14] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[13] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[12] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[3] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[2] * mv2.Coef[9]);
         tempArray[10] = (mv1.Coef[1] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[0] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[7] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[6] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[5] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[4] * mv2.Coef[15]);
         result.Coef[11] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[12] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[13] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[14] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[8] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[9] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[10] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[11] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[4] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[5] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[6] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[7] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[0] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[1] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[2] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[3] * mv2.Coef[15]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[13] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[12] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[15] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[14] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[9] * mv2.Coef[4]);
         tempArray[5] = (mv1.Coef[8] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[11] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[10] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[5] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[4] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[7] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[6] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[1] * mv2.Coef[12]);
         tempArray[13] = (mv1.Coef[0] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[3] * mv2.Coef[14]);
         tempArray[15] = (-1 * mv1.Coef[2] * mv2.Coef[15]);
         result.Coef[13] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[14] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[15] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[12] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[13] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[10] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[11] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[8] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[9] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[6] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[7] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[4] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[5] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[2] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[3] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[0] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[1] * mv2.Coef[15]);
         result.Coef[14] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[14] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[13] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[12] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[11] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[10] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[9] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[8] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[7] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[6] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[5] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[4] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[3] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[2] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[1] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[0] * mv2.Coef[15]);
         result.Coef[15] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector HIP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[15];
         
         result.Coef[15] = 0;
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[3] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[4] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[6] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[7] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[8] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[9] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[10] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[11] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[12] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[13] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[14] = (mv1.Coef[15] * mv2.Coef[15]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[4] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         tempArray[6] = (mv1.Coef[9] * mv2.Coef[8]);
         tempArray[7] = (-1 * mv1.Coef[8] * mv2.Coef[9]);
         tempArray[8] = (-1 * mv1.Coef[11] * mv2.Coef[10]);
         tempArray[9] = (-1 * mv1.Coef[10] * mv2.Coef[11]);
         tempArray[10] = (-1 * mv1.Coef[13] * mv2.Coef[12]);
         tempArray[11] = (-1 * mv1.Coef[12] * mv2.Coef[13]);
         tempArray[12] = (-1 * mv1.Coef[15] * mv2.Coef[14]);
         tempArray[13] = (mv1.Coef[14] * mv2.Coef[15]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13]);
         tempArray[0] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[2] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[4] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[5] = (mv1.Coef[5] * mv2.Coef[7]);
         tempArray[6] = (mv1.Coef[10] * mv2.Coef[8]);
         tempArray[7] = (mv1.Coef[11] * mv2.Coef[9]);
         tempArray[8] = (-1 * mv1.Coef[8] * mv2.Coef[10]);
         tempArray[9] = (mv1.Coef[9] * mv2.Coef[11]);
         tempArray[10] = (-1 * mv1.Coef[14] * mv2.Coef[12]);
         tempArray[11] = (mv1.Coef[15] * mv2.Coef[13]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[14]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[15]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[4] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[11] * mv2.Coef[8]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[11]);
         tempArray[4] = (-1 * mv1.Coef[15] * mv2.Coef[12]);
         tempArray[5] = (-1 * mv1.Coef[12] * mv2.Coef[15]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         tempArray[6] = (mv1.Coef[12] * mv2.Coef[8]);
         tempArray[7] = (mv1.Coef[13] * mv2.Coef[9]);
         tempArray[8] = (mv1.Coef[14] * mv2.Coef[10]);
         tempArray[9] = (-1 * mv1.Coef[15] * mv2.Coef[11]);
         tempArray[10] = (-1 * mv1.Coef[8] * mv2.Coef[12]);
         tempArray[11] = (mv1.Coef[9] * mv2.Coef[13]);
         tempArray[12] = (mv1.Coef[10] * mv2.Coef[14]);
         tempArray[13] = (mv1.Coef[11] * mv2.Coef[15]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13]);
         tempArray[0] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[13] * mv2.Coef[8]);
         tempArray[3] = (mv1.Coef[15] * mv2.Coef[10]);
         tempArray[4] = (mv1.Coef[8] * mv2.Coef[13]);
         tempArray[5] = (mv1.Coef[10] * mv2.Coef[15]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[14] * mv2.Coef[8]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[9]);
         tempArray[4] = (mv1.Coef[8] * mv2.Coef[14]);
         tempArray[5] = (-1 * mv1.Coef[9] * mv2.Coef[15]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[8]);
         tempArray[1] = (-1 * mv1.Coef[8] * mv2.Coef[15]);
         result.Coef[7] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (-1 * mv1.Coef[9] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[10] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[11] * mv2.Coef[3]);
         tempArray[3] = (-1 * mv1.Coef[12] * mv2.Coef[4]);
         tempArray[4] = (-1 * mv1.Coef[13] * mv2.Coef[5]);
         tempArray[5] = (-1 * mv1.Coef[14] * mv2.Coef[6]);
         tempArray[6] = (mv1.Coef[15] * mv2.Coef[7]);
         tempArray[7] = (mv1.Coef[1] * mv2.Coef[9]);
         tempArray[8] = (mv1.Coef[2] * mv2.Coef[10]);
         tempArray[9] = (-1 * mv1.Coef[3] * mv2.Coef[11]);
         tempArray[10] = (mv1.Coef[4] * mv2.Coef[12]);
         tempArray[11] = (-1 * mv1.Coef[5] * mv2.Coef[13]);
         tempArray[12] = (-1 * mv1.Coef[6] * mv2.Coef[14]);
         tempArray[13] = (-1 * mv1.Coef[7] * mv2.Coef[15]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13]);
         tempArray[0] = (-1 * mv1.Coef[11] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[13] * mv2.Coef[4]);
         tempArray[2] = (-1 * mv1.Coef[15] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[2] * mv2.Coef[11]);
         tempArray[4] = (-1 * mv1.Coef[4] * mv2.Coef[13]);
         tempArray[5] = (-1 * mv1.Coef[6] * mv2.Coef[15]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (mv1.Coef[11] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[14] * mv2.Coef[4]);
         tempArray[2] = (mv1.Coef[15] * mv2.Coef[5]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[11]);
         tempArray[4] = (-1 * mv1.Coef[4] * mv2.Coef[14]);
         tempArray[5] = (mv1.Coef[5] * mv2.Coef[15]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (-1 * mv1.Coef[15] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[4] * mv2.Coef[15]);
         result.Coef[11] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[13] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[14] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[15] * mv2.Coef[3]);
         tempArray[3] = (mv1.Coef[1] * mv2.Coef[13]);
         tempArray[4] = (mv1.Coef[2] * mv2.Coef[14]);
         tempArray[5] = (-1 * mv1.Coef[3] * mv2.Coef[15]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[2]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[15]);
         result.Coef[13] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (-1 * mv1.Coef[15] * mv2.Coef[1]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[15]);
         result.Coef[14] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector LCP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
         result.Coef[15] = (mv1.Coef[0] * mv2.Coef[15]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[15] * mv2.Coef[15]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[1]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[5]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[7]);
         tempArray[4] = (-1 * mv1.Coef[8] * mv2.Coef[9]);
         tempArray[5] = (-1 * mv1.Coef[10] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[12] * mv2.Coef[13]);
         tempArray[7] = (mv1.Coef[14] * mv2.Coef[15]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[2]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[3]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[6]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[7]);
         tempArray[4] = (-1 * mv1.Coef[8] * mv2.Coef[10]);
         tempArray[5] = (mv1.Coef[9] * mv2.Coef[11]);
         tempArray[6] = (-1 * mv1.Coef[12] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[13] * mv2.Coef[15]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[3]);
         tempArray[1] = (mv1.Coef[4] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[8] * mv2.Coef[11]);
         tempArray[3] = (-1 * mv1.Coef[12] * mv2.Coef[15]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[4]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[5]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[6]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[7]);
         tempArray[4] = (-1 * mv1.Coef[8] * mv2.Coef[12]);
         tempArray[5] = (mv1.Coef[9] * mv2.Coef[13]);
         tempArray[6] = (mv1.Coef[10] * mv2.Coef[14]);
         tempArray[7] = (mv1.Coef[11] * mv2.Coef[15]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[5]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[8] * mv2.Coef[13]);
         tempArray[3] = (mv1.Coef[10] * mv2.Coef[15]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[6]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[7]);
         tempArray[2] = (mv1.Coef[8] * mv2.Coef[14]);
         tempArray[3] = (-1 * mv1.Coef[9] * mv2.Coef[15]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[7]);
         tempArray[1] = (-1 * mv1.Coef[8] * mv2.Coef[15]);
         result.Coef[7] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[8]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[9]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[10]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[11]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[12]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[13]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[14]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[15]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[9]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[11]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[13]);
         tempArray[3] = (-1 * mv1.Coef[6] * mv2.Coef[15]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[10]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[11]);
         tempArray[2] = (-1 * mv1.Coef[4] * mv2.Coef[14]);
         tempArray[3] = (mv1.Coef[5] * mv2.Coef[15]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[11]);
         tempArray[1] = (mv1.Coef[4] * mv2.Coef[15]);
         result.Coef[11] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[12]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[13]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[14]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[15]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[13]);
         tempArray[1] = (-1 * mv1.Coef[2] * mv2.Coef[15]);
         result.Coef[13] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[14]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[15]);
         result.Coef[14] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector OP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
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
         tempArray[0] = (mv1.Coef[8] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[0] * mv2.Coef[8]);
         result.Coef[8] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[9] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[8] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[1] * mv2.Coef[8]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[9]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[10] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[8] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[8]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[10]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[11] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[10] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[9] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[3] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[2] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[10]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[11]);
         result.Coef[11] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[12] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[8] * mv2.Coef[4]);
         tempArray[2] = (mv1.Coef[4] * mv2.Coef[8]);
         tempArray[3] = (mv1.Coef[0] * mv2.Coef[12]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[13] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[12] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[9] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[5] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[1] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[13]);
         result.Coef[13] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[14] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[12] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[10] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[8] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[6] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[4] * mv2.Coef[10]);
         tempArray[6] = (mv1.Coef[2] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[0] * mv2.Coef[14]);
         result.Coef[14] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[15] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[14] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[13] * mv2.Coef[2]);
         tempArray[3] = (mv1.Coef[12] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[11] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[10] * mv2.Coef[5]);
         tempArray[6] = (mv1.Coef[9] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[8] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[7] * mv2.Coef[8]);
         tempArray[9] = (mv1.Coef[6] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[5] * mv2.Coef[10]);
         tempArray[11] = (mv1.Coef[4] * mv2.Coef[11]);
         tempArray[12] = (mv1.Coef[3] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[2] * mv2.Coef[13]);
         tempArray[14] = (mv1.Coef[1] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[0] * mv2.Coef[15]);
         result.Coef[15] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector Plus(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         
         result.Coef[0] = (mv1.Coef[0] + mv2.Coef[0]);
         result.Coef[1] = (mv1.Coef[1] + mv2.Coef[1]);
         result.Coef[2] = (mv1.Coef[2] + mv2.Coef[2]);
         result.Coef[3] = (mv1.Coef[3] + mv2.Coef[3]);
         result.Coef[4] = (mv1.Coef[4] + mv2.Coef[4]);
         result.Coef[5] = (mv1.Coef[5] + mv2.Coef[5]);
         result.Coef[6] = (mv1.Coef[6] + mv2.Coef[6]);
         result.Coef[7] = (mv1.Coef[7] + mv2.Coef[7]);
         result.Coef[8] = (mv1.Coef[8] + mv2.Coef[8]);
         result.Coef[9] = (mv1.Coef[9] + mv2.Coef[9]);
         result.Coef[10] = (mv1.Coef[10] + mv2.Coef[10]);
         result.Coef[11] = (mv1.Coef[11] + mv2.Coef[11]);
         result.Coef[12] = (mv1.Coef[12] + mv2.Coef[12]);
         result.Coef[13] = (mv1.Coef[13] + mv2.Coef[13]);
         result.Coef[14] = (mv1.Coef[14] + mv2.Coef[14]);
         result.Coef[15] = (mv1.Coef[15] + mv2.Coef[15]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector RCP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
         result.Coef[15] = (mv1.Coef[15] * mv2.Coef[0]);
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[15] * mv2.Coef[15]);
         result.Coef[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = (mv1.Coef[1] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[3] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[5] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[6]);
         tempArray[4] = (mv1.Coef[9] * mv2.Coef[8]);
         tempArray[5] = (-1 * mv1.Coef[11] * mv2.Coef[10]);
         tempArray[6] = (-1 * mv1.Coef[13] * mv2.Coef[12]);
         tempArray[7] = (-1 * mv1.Coef[15] * mv2.Coef[14]);
         result.Coef[1] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[2] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[3] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[6] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[7] * mv2.Coef[5]);
         tempArray[4] = (mv1.Coef[10] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[11] * mv2.Coef[9]);
         tempArray[6] = (-1 * mv1.Coef[14] * mv2.Coef[12]);
         tempArray[7] = (mv1.Coef[15] * mv2.Coef[13]);
         result.Coef[2] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[3] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[4]);
         tempArray[2] = (mv1.Coef[11] * mv2.Coef[8]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[12]);
         result.Coef[3] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[4] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[5] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[6] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[7] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[12] * mv2.Coef[8]);
         tempArray[5] = (mv1.Coef[13] * mv2.Coef[9]);
         tempArray[6] = (mv1.Coef[14] * mv2.Coef[10]);
         tempArray[7] = (-1 * mv1.Coef[15] * mv2.Coef[11]);
         result.Coef[4] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[5] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[7] * mv2.Coef[2]);
         tempArray[2] = (mv1.Coef[13] * mv2.Coef[8]);
         tempArray[3] = (mv1.Coef[15] * mv2.Coef[10]);
         result.Coef[5] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[6] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[7] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[14] * mv2.Coef[8]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[9]);
         result.Coef[6] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[7] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[15] * mv2.Coef[8]);
         result.Coef[7] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[8] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[9] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[10] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[11] * mv2.Coef[3]);
         tempArray[4] = (-1 * mv1.Coef[12] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[13] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[14] * mv2.Coef[6]);
         tempArray[7] = (mv1.Coef[15] * mv2.Coef[7]);
         result.Coef[8] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7]);
         tempArray[0] = (mv1.Coef[9] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[11] * mv2.Coef[2]);
         tempArray[2] = (-1 * mv1.Coef[13] * mv2.Coef[4]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[6]);
         result.Coef[9] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[10] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[11] * mv2.Coef[1]);
         tempArray[2] = (-1 * mv1.Coef[14] * mv2.Coef[4]);
         tempArray[3] = (mv1.Coef[15] * mv2.Coef[5]);
         result.Coef[10] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[11] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[15] * mv2.Coef[4]);
         result.Coef[11] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[12] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[13] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[14] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[15] * mv2.Coef[3]);
         result.Coef[12] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3]);
         tempArray[0] = (mv1.Coef[13] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[15] * mv2.Coef[2]);
         result.Coef[13] = (tempArray[0] + tempArray[1]);
         tempArray[0] = (mv1.Coef[14] * mv2.Coef[0]);
         tempArray[1] = (-1 * mv1.Coef[15] * mv2.Coef[1]);
         result.Coef[14] = (tempArray[0] + tempArray[1]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector Subtract(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         var result = new main.h3d.Multivector();
         
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
         tempVar0000 = (-1 * mv2.Coef[8]);
         result.Coef[8] = (mv1.Coef[8] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[9]);
         result.Coef[9] = (mv1.Coef[9] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[10]);
         result.Coef[10] = (mv1.Coef[10] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[11]);
         result.Coef[11] = (mv1.Coef[11] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[12]);
         result.Coef[12] = (mv1.Coef[12] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[13]);
         result.Coef[13] = (mv1.Coef[13] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[14]);
         result.Coef[14] = (mv1.Coef[14] + tempVar0000);
         tempVar0000 = (-1 * mv2.Coef[15]);
         result.Coef[15] = (mv1.Coef[15] + tempVar0000);
         
         
         return result;
      }
      
      public static main.h3d.Multivector Divide(main.h3d.Multivector mv1, double s2)
      {
         var result = new main.h3d.Multivector();
         
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
         result.Coef[8] = (mv1.Coef[8] * tempVar0000);
         result.Coef[9] = (mv1.Coef[9] * tempVar0000);
         result.Coef[10] = (mv1.Coef[10] * tempVar0000);
         result.Coef[11] = (mv1.Coef[11] * tempVar0000);
         result.Coef[12] = (mv1.Coef[12] * tempVar0000);
         result.Coef[13] = (mv1.Coef[13] * tempVar0000);
         result.Coef[14] = (mv1.Coef[14] * tempVar0000);
         result.Coef[15] = (mv1.Coef[15] * tempVar0000);
         
         
         return result;
      }
      
      public static double SP(main.h3d.Multivector mv1, main.h3d.Multivector mv2)
      {
         double result;
         
         double[] tempArray = new double[16];
         
         tempArray[0] = (mv1.Coef[0] * mv2.Coef[0]);
         tempArray[1] = (mv1.Coef[1] * mv2.Coef[1]);
         tempArray[2] = (mv1.Coef[2] * mv2.Coef[2]);
         tempArray[3] = (-1 * mv1.Coef[3] * mv2.Coef[3]);
         tempArray[4] = (mv1.Coef[4] * mv2.Coef[4]);
         tempArray[5] = (-1 * mv1.Coef[5] * mv2.Coef[5]);
         tempArray[6] = (-1 * mv1.Coef[6] * mv2.Coef[6]);
         tempArray[7] = (-1 * mv1.Coef[7] * mv2.Coef[7]);
         tempArray[8] = (mv1.Coef[8] * mv2.Coef[8]);
         tempArray[9] = (-1 * mv1.Coef[9] * mv2.Coef[9]);
         tempArray[10] = (-1 * mv1.Coef[10] * mv2.Coef[10]);
         tempArray[11] = (-1 * mv1.Coef[11] * mv2.Coef[11]);
         tempArray[12] = (-1 * mv1.Coef[12] * mv2.Coef[12]);
         tempArray[13] = (-1 * mv1.Coef[13] * mv2.Coef[13]);
         tempArray[14] = (-1 * mv1.Coef[14] * mv2.Coef[14]);
         tempArray[15] = (mv1.Coef[15] * mv2.Coef[15]);
         result = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector vinv(main.h3d.Multivector inMv)
      {
         var result = new main.h3d.Multivector();
         
         double[] tempArray = new double[16];
         
         tempArray[0] = Math.Pow(inMv.Coef[0], 2);
         tempArray[1] = Math.Pow(inMv.Coef[1], 2);
         tempArray[2] = Math.Pow(inMv.Coef[2], 2);
         tempArray[3] = Math.Pow(inMv.Coef[3], 2);
         tempArray[4] = Math.Pow(inMv.Coef[4], 2);
         tempArray[5] = Math.Pow(inMv.Coef[5], 2);
         tempArray[6] = Math.Pow(inMv.Coef[6], 2);
         tempArray[7] = Math.Pow(inMv.Coef[7], 2);
         tempArray[8] = Math.Pow(inMv.Coef[8], 2);
         tempArray[9] = Math.Pow(inMv.Coef[9], 2);
         tempArray[10] = Math.Pow(inMv.Coef[10], 2);
         tempArray[11] = Math.Pow(inMv.Coef[11], 2);
         tempArray[12] = Math.Pow(inMv.Coef[12], 2);
         tempArray[13] = Math.Pow(inMv.Coef[13], 2);
         tempArray[14] = Math.Pow(inMv.Coef[14], 2);
         tempArray[15] = Math.Pow(inMv.Coef[15], 2);
         tempArray[0] = (tempArray[0] + tempArray[1] + tempArray[2] + tempArray[3] + tempArray[4] + tempArray[5] + tempArray[6] + tempArray[7] + tempArray[8] + tempArray[9] + tempArray[10] + tempArray[11] + tempArray[12] + tempArray[13] + tempArray[14] + tempArray[15]);
         tempArray[0] = Math.Pow(tempArray[0], -1);
         result.Coef[0] = (inMv.Coef[0] * tempArray[0]);
         result.Coef[1] = (inMv.Coef[1] * tempArray[0]);
         result.Coef[2] = (inMv.Coef[2] * tempArray[0]);
         result.Coef[3] = (-1 * inMv.Coef[3] * tempArray[0]);
         result.Coef[4] = (inMv.Coef[4] * tempArray[0]);
         result.Coef[5] = (-1 * inMv.Coef[5] * tempArray[0]);
         result.Coef[6] = (-1 * inMv.Coef[6] * tempArray[0]);
         result.Coef[7] = (-1 * inMv.Coef[7] * tempArray[0]);
         result.Coef[8] = (inMv.Coef[8] * tempArray[0]);
         result.Coef[9] = (-1 * inMv.Coef[9] * tempArray[0]);
         result.Coef[10] = (-1 * inMv.Coef[10] * tempArray[0]);
         result.Coef[11] = (-1 * inMv.Coef[11] * tempArray[0]);
         result.Coef[12] = (-1 * inMv.Coef[12] * tempArray[0]);
         result.Coef[13] = (-1 * inMv.Coef[13] * tempArray[0]);
         result.Coef[14] = (-1 * inMv.Coef[14] * tempArray[0]);
         result.Coef[15] = (inMv.Coef[15] * tempArray[0]);
         
         
         return result;
      }
      
      public static main.h3d.Multivector div_by_norm(main.h3d.Multivector inMv)
      {
         var result = new main.h3d.Multivector();
         
         double tempVar0000;
         double tempVar0001;
         
         tempVar0000 = Math.Pow(5, -0.5);
         tempVar0001 = Math.Pow(inMv.Coef[0], 0.5);
         result.Coef[0] = (tempVar0000 * tempVar0001);
         tempVar0001 = Math.Pow(inMv.Coef[0], -0.5);
         result.Coef[1] = (inMv.Coef[1] * tempVar0000 * tempVar0001);
         result.Coef[2] = (inMv.Coef[2] * tempVar0000 * tempVar0001);
         result.Coef[3] = (inMv.Coef[3] * tempVar0000 * tempVar0001);
         result.Coef[4] = (inMv.Coef[4] * tempVar0000 * tempVar0001);
         result.Coef[5] = (inMv.Coef[5] * tempVar0000 * tempVar0001);
         result.Coef[6] = (inMv.Coef[6] * tempVar0000 * tempVar0001);
         result.Coef[7] = (inMv.Coef[7] * tempVar0000 * tempVar0001);
         result.Coef[8] = (inMv.Coef[8] * tempVar0000 * tempVar0001);
         result.Coef[9] = (inMv.Coef[9] * tempVar0000 * tempVar0001);
         result.Coef[10] = (inMv.Coef[10] * tempVar0000 * tempVar0001);
         result.Coef[11] = (inMv.Coef[11] * tempVar0000 * tempVar0001);
         result.Coef[12] = (inMv.Coef[12] * tempVar0000 * tempVar0001);
         result.Coef[13] = (inMv.Coef[13] * tempVar0000 * tempVar0001);
         result.Coef[14] = (inMv.Coef[14] * tempVar0000 * tempVar0001);
         result.Coef[15] = (inMv.Coef[15] * tempVar0000 * tempVar0001);
         
         
         return result;
      }
      
   }
}
