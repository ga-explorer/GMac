using System;
using GeometricAlgebraSymbolicsLibSamples.GAPoT;

namespace GeometricAlgebraSymbolicsLibSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            RotorsSequenceSample.Execute();
            //GramSchmidtRotation3DSample.Execute();
            //FbdFrameSample.Execute();

            Console.WriteLine();
            Console.WriteLine(@"Press any key to exit...");
            Console.ReadKey();
        }
    }
}
