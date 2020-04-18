using System;
using System.Globalization;
using GeometricAlgebraNumericsLib.Applications.GAPoT;

namespace GeometricAlgebraNumericsLibSamples.GAPoT
{
    public static class Sample1
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            var a1 = 30 * Math.PI / 180;
            var a2 = -30 * Math.PI / 180;

            //Voltage
            var mvU = $"[p(1,{a1}) <1,2>] <a>"
                .GaPoTNumParseMultiPhaseVector();

            //Current
            var mvI = $"[p(0.5,{a1}) <1,2>] <a>; [p(0.5,{a2}) <1,2>] <b>"
                .GaPoTNumParseMultiPhaseVector();

            //Power
            var mvM = mvU * mvI;

            //Impedance
            var mvZ = mvU * mvI.Inverse();

            Console.WriteLine($@"U = {mvU}");
            Console.WriteLine($@"I = {mvI}");
            Console.WriteLine($@"M = U I = {mvM.ToLaTeX()}");
            Console.WriteLine($@"Z = U inverse(I) = {mvZ}");
            Console.WriteLine();

            Console.WriteLine($@"norm2(U) = {mvU.Norm2():G}");
            Console.WriteLine($@"norm2(I) = {mvI.Norm2():G}");
            Console.WriteLine($@"norm2(M) = {mvM.Norm2():G}");
            Console.WriteLine($@"norm2(Z) = {mvZ.Norm2():G}");
            Console.WriteLine();

            Console.WriteLine($@"reverse(U) = {mvU.Reverse()}");
            Console.WriteLine($@"reverse(I) = {mvI.Reverse()}");
            Console.WriteLine($@"reverse(M) = {mvM.Reverse()}");
            Console.WriteLine($@"reverse(Z) = {mvZ.Reverse()}");
            Console.WriteLine();

            Console.WriteLine($@"U reverse(U) = {mvU * mvU.Reverse()}");
            Console.WriteLine($@"I reverse(I) = {mvI * mvI.Reverse()}");
            Console.WriteLine();

            Console.WriteLine($@"inverse(U) = {mvU.Inverse()}");
            Console.WriteLine($@"inverse(I) = {mvI.Inverse()}");
            Console.WriteLine();
            
            //Note: I'm not sure this computation is as required, please verify results.
            Console.WriteLine($@"inverse(Z) = {mvZ.PerPhaseInverse()}");
            Console.WriteLine();
        }
    }
}
