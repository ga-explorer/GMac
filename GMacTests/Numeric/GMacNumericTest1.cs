using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Frames;
using TextComposerLib.Text.Linear;

namespace GMacTests.Numeric
{
    /// <summary>
    /// Verify computations on numerical multivetors using symbolic multivectors
    /// </summary>
    public sealed class GMacNumericTest1 : IGMacTest
    {
        public string Title { get; }

        public string Execute()
        {
            var randGen = new GaRandomGenerator(10);
            var textComposer = new LinearComposer();

            var numFrame = GaNumFrame.CreateConformal(5);
            var symFrame = GaSymFrame.CreateConformal(5);

            var numMv1 = randGen.GetNumMultivectorFull(numFrame.GaSpaceDimension);
            var numMv2 = randGen.GetNumMultivectorFull(numFrame.GaSpaceDimension);
            var symMv1 = numMv1.ToSymbolic();
            var symMv2 = numMv2.ToSymbolic();

            var symMvGp = symFrame.Gp[symMv1, symMv2];

            //textComposer.AppendLineAtNewLine("Symbolic Multivector 1: ").AppendLine(symMv1);
            //textComposer.AppendLineAtNewLine("Numeric Multivector 1: ").AppendLine(numMv1);

            //textComposer.AppendLineAtNewLine("Symbolic Multivector 2: ").AppendLine(symMv2);
            //textComposer.AppendLineAtNewLine("Numeric Multivector 2: ").AppendLine(numMv2);

            //textComposer.AppendLineAtNewLine("Symbolic Gp: ").AppendLine(symMvGp);
            //textComposer.AppendLineAtNewLine("Numeric GP: ").AppendLine(numMvGp);


            numFrame.SetProductsImplementation(GaBilinearProductImplementation.Computed);
            GaNumMultivector.ResetAddFactorsCallCount();
            var numMvGp = numFrame.Gp[numMv1, numMv2];
            var callsCount = GaNumMultivector.AddFactorsCallCount;
            var diff = symMvGp.ToNumeric() - numMvGp;
            diff.Simplify();
            GaNumMultivector.ResetAddFactorsCallCount();
            textComposer
                .AppendLineAtNewLine("Difference, Computed Tree: ")
                .AppendLine(callsCount)
                .AppendLine(diff);


            numFrame.SetProductsImplementation(GaBilinearProductImplementation.LookupArray);
            GaNumMultivector.ResetAddFactorsCallCount();
            numMvGp = numFrame.Gp[numMv1, numMv2];
            callsCount = GaNumMultivector.AddFactorsCallCount;
            //var factorsCount = ((GaNumMapBilinearArray) numFrame.Gp).FactorsCount;
            diff = symMvGp.ToNumeric() - numMvGp;
            diff.Simplify();
            GaNumMultivector.ResetAddFactorsCallCount();
            textComposer
                .AppendLineAtNewLine("Difference, Lookup Array: ")
                .AppendLine(callsCount)
                //.AppendLine(factorsCount)
                .AppendLine(diff);


            numFrame.SetProductsImplementation(GaBilinearProductImplementation.LookupHash);
            GaNumMultivector.ResetAddFactorsCallCount();
            numMvGp = numFrame.Gp[numMv1, numMv2];
            callsCount = GaNumMultivector.AddFactorsCallCount;
            //factorsCount = ((GaNumMapBilinearHash)numFrame.Gp).FactorsCount;
            diff = symMvGp.ToNumeric() - numMvGp;
            diff.Simplify();
            GaNumMultivector.ResetAddFactorsCallCount();
            textComposer
                .AppendLineAtNewLine("Difference, Lookup Hash: ")
                .AppendLine(callsCount)
                //.AppendLine(factorsCount)
                .AppendLine(diff);


            numFrame.SetProductsImplementation(GaBilinearProductImplementation.LookupTree);
            GaNumMultivector.ResetAddFactorsCallCount();
            numMvGp = numFrame.Gp[numMv1, numMv2];
            callsCount = GaNumMultivector.AddFactorsCallCount;
            //factorsCount = ((GaNumMapBilinearTree)numFrame.Gp).FactorsCount;
            diff = symMvGp.ToNumeric() - numMvGp;
            diff.Simplify();
            GaNumMultivector.ResetAddFactorsCallCount();
            textComposer
                .AppendLineAtNewLine("Difference, Lookup Tree: ")
                .AppendLine(callsCount)
                //.AppendLine(factorsCount)
                .AppendLine(diff);


            numFrame.SetProductsImplementation(GaBilinearProductImplementation.LookupCoefSums);
            GaNumMultivector.ResetAddFactorsCallCount();
            numMvGp = numFrame.Gp[numMv1, numMv2];
            callsCount = GaNumMultivector.AddFactorsCallCount;
            //factorsCount = ((GaNumMapBilinearCoefSums)numFrame.Gp).FactorsCount;
            diff = symMvGp.ToNumeric() - numMvGp;
            diff.Simplify();
            GaNumMultivector.ResetAddFactorsCallCount();
            textComposer
                .AppendLineAtNewLine("Difference, Lookup CoefSums: ")
                .AppendLine(callsCount)
                //.AppendLine(factorsCount)
                .AppendLine(diff);


            return textComposer.ToString();
        }
    }
}
