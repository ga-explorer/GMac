using System.Drawing;
using GeometryComposerLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Vertices
{
    public interface IGraphicsVertex3D : ITuple3D
    {
        int Index { get; }

        ITuple3D Point { get; }

        Color Color { get; set; }

        ITuple2D TextureUv { get; set; }

        GraphicsNormal3D Normal { get; }

        double TextureU { get; }

        double TextureV { get; }

        double NormalX { get; }

        double NormalY { get; }

        double NormalZ { get; }

        bool HasTextureUv { get; }

        bool HasNormal { get; }

        bool HasColor { get; }

        GraphicsVertexDataInfo3D DataInfo { get; }
    }
}