using System;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Rendering;
using GMacBenchmarks.Samples.Computations;

namespace GMacBenchmarks.Samples.Rendering.Numeric
{
    public sealed class Sample3 : IGMacSample
    {
        public string Title
            => "Generate GraphViz diagrams from multivectors and linear maps";

        public string Description
            => "Generate GraphViz diagrams from multivectors and linear maps";


        public string Execute()
        {
            //var randGen = new GaRandomGenerator(10);
            var frame = GaNumFrame.CreateOrthogonal(2, 0, -0.5);
            var mvD = ((GaNumMetricOrthogonal) frame.Metric).GetMetricSignatureMultivector();

            var mvX = GaNumMultivector.CreateZero(frame.GaSpaceDimension);
            mvX.SetTerm(1, 3.0d);
            mvX.SetTerm(2, 2.0d);
            mvX.SetTerm(5, -0.5d);

            var mvY = GaNumMultivector.CreateZero(frame.GaSpaceDimension);
            mvY.SetTerm(1, 2.0d);
            mvY.SetTerm(3, -2.0d);
            mvY.SetTerm(7, 1d);


            //var mv = randGen.GetNumMultivectorByGrades(frame.GaSpaceDimension, 2);
            var dotGraphXCode = mvX.ToGraphViz(false, true).GenerateDotCode();
            var dotGraphYCode = mvY.ToGraphViz(false, true).GenerateDotCode();
            var dotGraphDCode = mvD.ToGraphViz(false, true).GenerateDotCode();

            //var bilinearMap = frame.ComputedLcp.ToTreeMap();
            //var dotGraph = bilinearMap.ToGraphViz(false, true);

            //var unilinearMap = frame.NonOrthogonalMetric.BaseToDerivedCba.ToTreeMap();
            //var dotGraph = unilinearMap.ToGraphViz(true, true);

            return 
                dotGraphXCode + Environment.NewLine +
                dotGraphYCode + Environment.NewLine +
                dotGraphDCode + Environment.NewLine;
        }
    }
}
