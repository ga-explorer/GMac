using System;
using System.Globalization;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class Sample4
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var mvU = 
                "'Sqrt[2] * Subscript[v,1] * Cos[x]'<1>, 'Sqrt[2] * Subscript[v,2] * Sin[x]'<2>"
                    .GaPoTSymParseSinglePhaseVector();

            var mvI = 
                "'Sqrt[2] * Subscript[i,1] * Cos[x]'<1>, 'Sqrt[2] * Subscript[i,2] * Sin[x]'<2>"
                    .GaPoTSymParseSinglePhaseVector();

            var mvM = mvU * mvI;

            Console.WriteLine(@"Display multivector terms in text form");
            Console.WriteLine($@"U = {mvU}");
            Console.WriteLine($@"I = {mvI}");
            Console.WriteLine($@"M = {mvM}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector terms in LaTeX form");
            Console.WriteLine($@"U = {mvU.TermsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.TermsToLaTeX()}");
            Console.WriteLine($@"M = {mvM.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector polar phasors in text form");
            Console.WriteLine($@"U = {mvU.PolarPhasorsToText()}");
            Console.WriteLine($@"I = {mvI.PolarPhasorsToText()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector polar phasors in LaTeX form");
            Console.WriteLine($@"U = {mvU.PolarPhasorsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.PolarPhasorsToLaTeX()}");
            Console.WriteLine();
        }
    }
}