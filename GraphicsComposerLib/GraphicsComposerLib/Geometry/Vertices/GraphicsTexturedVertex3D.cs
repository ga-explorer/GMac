using System;
using System.Drawing;
using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Vertices
{
    public sealed class GraphicsTexturedVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv { get; set; }

        public GraphicsNormal3D Normal
            => null;

        public bool HasColor 
            => false;

        public bool HasTextureUv 
            => true;

        public bool HasNormal 
            => false;

        public GraphicsVertexDataInfo3D DataInfo
            => GraphicsVertexDataInfo3D.CreateTexturedVertexDataInfo();

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
            => 0;

        public double NormalY 
            => 0;

        public double NormalZ 
            => 0;

        public bool HasNaNComponent
            => Point.HasNaNComponent || 
               TextureUv.HasNaNComponent;


        public GraphicsTexturedVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
            TextureUv = new Tuple2D(0, 0);
        }

        public GraphicsTexturedVertex3D(int index, ITuple3D point, ITuple2D textureUv)
        {
            Index = index;
            Point = point;
            TextureUv = textureUv;
        }

        public GraphicsTexturedVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            TextureUv = vertex.TextureUv;
        }


        public Tuple3D ToTuple3D()
        {
            return new Tuple3D(X, Y, Z);
        }
    }
}