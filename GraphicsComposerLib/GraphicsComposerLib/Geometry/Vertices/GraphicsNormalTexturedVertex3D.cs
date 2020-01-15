using System;
using System.Drawing;
using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Vertices
{
    public sealed class GraphicsNormalTexturedVertex3D 
        : IGraphicsNormalVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv { get; set; }

        public GraphicsNormal3D Normal { get; }
            = new GraphicsNormal3D();

        public double X 
            => Point.X;

        public double Y 
            => Point.Y;

        public double Z 
            => Point.Z;

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        public double TextureU 
            => TextureUv.X;

        public double TextureV 
            => TextureUv.Y;

        public double NormalX 
            => Normal.X;

        public double NormalY 
            => Normal.Y;

        public double NormalZ 
            => Normal.Z;

        public bool HasColor 
            => false;

        public bool HasTextureUv 
            => true;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataInfo3D DataInfo
            => GraphicsVertexDataInfo3D.CreateNormalTexturedVertexDataInfo();

        public bool HasNaNComponent
            => Point.HasNaNComponent ||
               TextureUv.HasNaNComponent ||
               Normal.HasNaNComponent;


        public GraphicsNormalTexturedVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
            TextureUv = new Tuple2D();
        }

        public GraphicsNormalTexturedVertex3D(int index, ITuple3D point, ITuple2D textureUv)
        {
            Index = index;
            Point = point;
            TextureUv = textureUv;
        }

        public GraphicsNormalTexturedVertex3D(int index, ITuple3D point, ITuple2D textureUv, ITuple3D normal)
        {
            Index = index;
            Point = point;
            TextureUv = textureUv;
            Normal.Set(normal);
        }

        public GraphicsNormalTexturedVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            TextureUv = vertex.TextureUv;
            Normal.Set(vertex.Normal);
        }


        public Tuple3D ToTuple3D()
        {
            return Point.ToTuple3D();
        }
    }
}
