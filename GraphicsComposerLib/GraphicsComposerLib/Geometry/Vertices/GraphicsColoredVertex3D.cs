using System;
using System.Drawing;
using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Vertices
{
    public sealed class GraphicsColoredVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public Color Color { get; set; }
            = Color.Black;

        public ITuple2D TextureUv
        {
            get => Tuple2D.Zero;
            set => throw new InvalidOperationException();
        }

        public GraphicsNormal3D Normal
            => null;

        public bool HasColor 
            => true;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => false;

        public GraphicsVertexDataInfo3D DataInfo
            => GraphicsVertexDataInfo3D.CreateColoredVertexDataInfo();

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
            => 0;

        public double TextureV 
            => 0;

        public double NormalX 
            => 0;

        public double NormalY 
            => 0;

        public double NormalZ 
            => 0;

        public bool HasNaNComponent
            => Point.HasNaNComponent;


        public GraphicsColoredVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
        }

        public GraphicsColoredVertex3D(int index, ITuple3D point, Color color)
        {
            Index = index;
            Point = point;
            Color = color;
        }

        public GraphicsColoredVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            Color = vertex.Color;
        }


        public Tuple3D ToTuple3D()
        {
            return new Tuple3D(X, Y, Z);
        }
    }
}