using System;
using GeometricAlgebraSymbolicsLib.Samples.GAPoT;

namespace GeometricAlgebraSymbolicsLib.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //RotorsSequenceSample.Execute();
            //GramSchmidtRotationSample.Execute();
            //HyperVectorsFrameSample.Execute();
            SimpleKirchhoffRotationSample.Execute1();
            //OrthogonalRotorsDecompositionSample.Execute();
            //ValidationSample1.Execute();

            Console.WriteLine();
            Console.WriteLine(@"Press any key to exit...");
            Console.ReadKey();
        }
    }
}
