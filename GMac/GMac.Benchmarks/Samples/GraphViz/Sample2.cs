using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Rendering;
using GeometricAlgebraStructuresLib.Frames;
using GMac.Benchmarks.Samples.Computations;

namespace GMac.Benchmarks.Samples.GraphViz
{
    public sealed class Sample2 : IGMacSample
    {
        public string Title 
            => "Draw Binary Tree of a multivector";

        public string Description 
            => "Draw Binary Tree of a multivector";

        public string Execute()
        {
            var graph = DotGraph.Undirected();
            graph.SetRankDir(DotRankDirection.LeftToRight);

            var vSpaceDim = 5;
            var gaSpaceDim = 1 << vSpaceDim;

            var mv1 =
                GaNumDarKVector
                    .Create(
                        vSpaceDim,
                        3,
                        Enumerable.Repeat(1.0d, (int)GaFrameUtils.KvSpaceDimension(vSpaceDim, 3))
                    )
                    .GetSarMultivector();

            var mv2 =
                GaNumDarKVector
                    .Create(gaSpaceDim, 4, Enumerable.Repeat(1.0d, (int)GaFrameUtils.KvSpaceDimension(vSpaceDim, 4)))
                    .GetSarMultivector();

            var mv = mv1;// + mv2;

            var tree = mv.GetBtrRootNode();

            graph.AddBinaryTree(tree, false);

            return graph.GenerateDotCode();
        }
    }
}