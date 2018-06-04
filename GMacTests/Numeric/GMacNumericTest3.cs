using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps;

namespace GMacTests.Numeric
{
    public sealed class GMacNumericTest3 : IGMacTest
    {
        public string Title { get; } 
            = "Generate GraphViz diagrams from multivectors and linear maps";

        public string Execute()
        {
            var randGen = new GaRandomGenerator(10);
            var frame = GaNumFrame.CreateEuclidean(3);

            //var mv = randGen.GetNumMultivectorByGrades(frame.GaSpaceDimension, 2);
            //var dotGraph = mv.ToGraphViz(true, true);

            var bilinearMap = frame.ComputedLcp.ToTreeMap();
            var dotGraph = bilinearMap.ToGraphViz(false, true);

            //var unilinearMap = frame.NonOrthogonalMetric.BaseToDerivedCba.ToTreeMap();
            //var dotGraph = unilinearMap.ToGraphViz(true, true);

            return dotGraph.GenerateDotCode();
        }
    }
}
