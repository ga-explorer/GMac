using System.Collections.Generic;
using DataStructuresLib.Basic;
using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicShapes.Lines;

namespace GraphicsComposerLib.Geometry.Lines
{
    public interface IGraphicsLinesGeometry3D 
        : IGraphicsGeometry3D<ILineSegment3D>
    {
        IReadOnlyList<Pair<ITuple3D>> LineVerticesPoints { get; }

        IReadOnlyList<Pair<int>> LineVerticesIndices { get; }
    }
}