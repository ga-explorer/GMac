using System;
using System.Globalization;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class Sample6
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var mvU = 
                "'Subscript[u,1,1]'<1>, 'Subscript[u,2,1]'<2>, 'Subscript[u,3,1]'<3>, 'Subscript[u,1,2]'<4>, 'Subscript[u,2,2]'<5>, 'Subscript[u,3,2]'<6>"
                    .GaPoTSymParseVector();

            var mvI = 
                "'Subscript[i,1,1]'<1>, 'Subscript[i,2,1]'<2>, 'Subscript[i,3,1]'<3>, 'Subscript[i,1,2]'<4>, 'Subscript[i,2,2]'<5>, 'Subscript[i,3,2]'<6>"
                    .GaPoTSymParseVector();

            var mvM = mvU * mvI;

            var mvMrev = mvM.Reverse();

            var normU = mvU.Norm2();
            var normI = mvI.Norm2();
            var normUI = Mfs.Times[normU, normI].Expand();
            var normM = mvM.Norm2().Expand();
            var normDiff = Mfs.Subtract[normUI, normM].Evaluate();

            Console.WriteLine(@"Display multivector terms in LaTeX form");
            Console.WriteLine($@"U = {mvU.TermsToLaTeX()}");
            Console.WriteLine($@"I = {mvI.TermsToLaTeX()}");
            Console.WriteLine($@"M = {mvM.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(M) = {mvMrev.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine(@"Display multivector norms in LaTeX form");
            Console.WriteLine($@"norm U = {normU.GetLaTeX()}");
            Console.WriteLine($@"norm I = {normI.GetLaTeX()}");
            Console.WriteLine($@"norm U norm I = {normUI.GetLaTeX()}");
            Console.WriteLine($@"norm M = {normM.GetLaTeX()}");
            Console.WriteLine($@"norm Diff = {normDiff.GetLaTeX()}");
            Console.WriteLine();
        }
    }
}