using System;
using System.Globalization;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Rendering;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class Sample1
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Voltage
            var mvU = $"p('1','Pi/6') <1,2>"
                .GaPoTSymParseVector();

            //Current
            var mvI = $"p('1/2','Pi/6') <1,2>, p('1/2','-Pi/6') <3,4>"
                .GaPoTSymParseVector();

            //Power
            var mvM = mvU * mvI;

            //Impedance
            var mvZ = mvU * mvI.Inverse();

            Console.WriteLine($@"U = {mvU.ToLaTeX()}");
            Console.WriteLine($@"I = {mvI.ToLaTeX()}");
            Console.WriteLine($@"M = U I = {mvM.ToLaTeX()}");
            Console.WriteLine($@"Z = U inverse(I) = {mvZ.ToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($@"norm2(U) = {mvU.Norm2().ToLaTeX()}");
            Console.WriteLine($@"norm2(I) = {mvI.Norm2().ToLaTeX()}");
            Console.WriteLine($@"norm2(M) = {mvM.Norm2().ToLaTeX()}");
            Console.WriteLine($@"norm2(Z) = {mvZ.Norm2().ToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($@"reverse(U) = {mvU.Reverse().ToLaTeX()}");
            Console.WriteLine($@"reverse(I) = {mvI.Reverse().ToLaTeX()}");
            Console.WriteLine($@"reverse(M) = {mvM.Reverse().ToLaTeX()}");
            Console.WriteLine($@"reverse(Z) = {mvZ.Reverse().ToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($@"U reverse(U) = {(mvU * mvU.Reverse()).ToLaTeX()}");
            Console.WriteLine($@"I reverse(I) = {(mvI * mvI.Reverse()).ToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($@"inverse(U) = {mvU.Inverse().ToLaTeX()}");
            Console.WriteLine($@"inverse(I) = {mvI.Inverse().ToLaTeX()}");
            Console.WriteLine();
        }
    }
}
