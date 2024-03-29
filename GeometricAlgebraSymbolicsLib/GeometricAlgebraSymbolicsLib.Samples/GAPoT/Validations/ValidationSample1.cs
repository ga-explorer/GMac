﻿using System;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT.Validations
{
    public static class ValidationSample1
    {
        public static void Execute1()
        {
            var bv =
                "'3.1009536313039' <>, '-1.28445705037617' <1,3>, '-0.985598559653489' <2,3>, '0.408248290463863' <1,2>"
                    .GaPoTSymParseBiversor();

            Console.WriteLine($"Biversor Squared Norm: {bv.Norm2()}");
            Console.WriteLine();

            var mv = bv.ToMultivector();

            Console.WriteLine($"Multivector Squared Norm: {mv.Norm2()}");
            Console.WriteLine();
        }

        public static void Execute2()
        {
            var bitsCount = 4;

            for (var i = 0; i < (1 << bitsCount); i++)
            {
                var patternText1 = 
                    Convert.ToString(i, 2).PadLeft(bitsCount, '0');

                var j = i.ReverseBits(bitsCount);

                var patternText2 = 
                    Convert.ToString(j, 2).PadLeft(bitsCount, '0');

                Console.WriteLine($"{patternText1} <=> {patternText2}, ({i.CountOnes()} bits)");
            }
        }

        public static void Execute3()
        {
            var bitsCount = 4;

            //var mv = GaPoTSymMultivector.CreateZero().AddTerms(
            //    Enumerable.Range(0, 1 << bitsCount).Select(id => new GaPoTSymMultivectorTerm(id, 1))
            //);

            var mv = GaPoTSymMultivector
                .CreateZero()
                .AddTerm(3, 1.ToExpr())
                .AddTerm(5, 2.ToExpr())
                .AddTerm(0, (-2).ToExpr());

            Console.WriteLine(mv.TermsToText());
        }

        public static void Execute()
        {
            var rotor =
                "'0.880476239217149' <>, '0.115916895959295' <1,2>, '-0.364705199631001' <1,3>, '-0.279848142333121' <2,3>"
                    .GaPoTSymParseBiversor()
                    .ToMultivector();

            Console.WriteLine($"Rotor Inverse: {rotor.Inverse()}");
            Console.WriteLine();

            Console.WriteLine($"Rotor Reverse: {rotor.Reverse()}");
            Console.WriteLine();
        }
    }
}
