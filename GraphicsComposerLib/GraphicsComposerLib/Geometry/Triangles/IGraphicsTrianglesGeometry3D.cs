using System.Collections.Generic;
using System.Drawing;
using DataStructuresLib.Basic;
using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicShapes.Triangles;
using GraphicsComposerLib.Geometry.Vertices;

namespace GraphicsComposerLib.Geometry.Triangles
{
    public interface IGraphicsTrianglesGeometry3D 
        : IGraphicsGeometry3D<ITriangle3D>
    {
        IReadOnlyList<Triplet<ITuple3D>> TriangleVerticesPoints { get; }

        IReadOnlyList<Triplet<int>> TriangleVerticesIndices { get; }

        IReadOnlyList<GraphicsNormal3D> VertexNormals { get; }

        IReadOnlyList<ITuple2D> VertexUVs { get; }

        IReadOnlyList<Color> VertexColors { get; }

        bool ContainsVertexNormals { get; }

        bool ContainsVertexUVs { get; }

        bool ContainsVertexColors { get; }

        GraphicsVertexNormalComputationMethod NormalComputationMethod { get; }

        void ComputeVertexNormals(bool inverseNormals);
    }
}