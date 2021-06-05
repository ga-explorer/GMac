using System;
using CodeComposerLib.GraphViz;
using GeometricAlgebraNumericsLib.Rendering.LaTeX;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Rendering.GraphViz;
using GMac.Benchmarks.Samples.Computations;

namespace GMac.Benchmarks.Samples.Visualizations.Symbolic
{
    public sealed class Sample3 : IGMacSample
    {
        public string Title
            => "Generate GraphViz diagrams from multivectors and linear maps";

        public string Description
            => "Generate GraphViz diagrams from multivectors and linear maps";


        public string Execute()
        {
            var frame = GaSymFrame.CreateOrthonormal("++++-");
            
            var ep = GaSymMultivector.CreateBasisVector(
                frame.VSpaceDimension, 
                3
            );

            var en = GaSymMultivector.CreateBasisVector(
                frame.VSpaceDimension, 
                4
            );

            var no = (en - ep) / 2;
            var ni = en + ep;

            var mvA = GaSymMultivector.CreateVectorFromScalars(
                "Subscript[a,x]", "Subscript[a,y]", "Subscript[a,z]", "0", "0"
            );

            var mvB = no + mvA + (frame.Sp[mvA, mvA] / 2)[0] * ni;

            var renderer = new GaSymGraphVizRenderer(
                GaLaTeXBasisBladeForm.BasisVectorsSubscripts, 
                "x", "y", "z", "p", "n"
            );

            var dotGraph = 
                renderer.RenderDotGraph(mvB, false, true);



            //var bilinearMap = frame.ComputedLcp.ToTreeMap();
            //var dotGraph = bilinearMap.ToGraphViz(false, true);

            //var unilinearMap = frame.NonOrthogonalMetric.BaseToDerivedCba.ToTreeMap();
            //var dotGraph = unilinearMap.ToGraphViz(true, true);

            var code = dotGraph.GenerateDotCode();


            var t = DateTime.Now;
            dotGraph.SaveDotCode("code.gv");
            Console.WriteLine(DateTime.Now - t);


            GvRenderer.Dot.RenderToFile(
                code, 
                GvOutputFormat.Png, 
                @"graph.png"
            );

            return code;
        }
    }
}
