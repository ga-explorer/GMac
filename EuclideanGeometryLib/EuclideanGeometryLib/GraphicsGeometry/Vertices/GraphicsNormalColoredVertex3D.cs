using System;
using System.Drawing;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public sealed class GraphicsNormalColoredVertex3D 
        : IGraphicsNormalVertex3D
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
            => 0;

        public double TextureV 
            => 0;

        public double NormalX 
            => Normal.X;

        public double NormalY 
            => Normal.Y;

        public double NormalZ 
            => Normal.Z;

        public bool HasColor 
            => true;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataInfo3D DataInfo
            => GraphicsVertexDataInfo3D.CreateNormalColoredVertexDataInfo();

        public bool HasNaNComponent 
            => Point.HasNaNComponent || 
               double.IsNaN(NormalX) ||
               double.IsNaN(NormalY) ||
               double.IsNaN(NormalZ);


        public GraphicsNormalColoredVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
        }

        public GraphicsNormalColoredVertex3D(int index, ITuple3D point, Color color)
        {
            Index = index;
            Point = point;
            Color = color;
        }

        public GraphicsNormalColoredVertex3D(int index, ITuple3D point, Color color, ITuple3D normal)
        {
            Index = index;
            Point = point;
            Color = color;
            Normal.Set(normal);
        }

        public GraphicsNormalColoredVertex3D(int index, IGraphicsVertex3D point)
        {
            Index = index;
            Point = point.Point;
            Color = point.Color;
        }


        public Tuple3D GetDisplacedPoint(double d)
        {
            return new Tuple3D(
                Point.X + d * Normal.X,
                Point.Y + d * Normal.Y,
                Point.Z + d * Normal.Z
            );
        }
        
        public Tuple3D ToTuple3D()
        {
            return Point.ToTuple3D();
        }
    }
}