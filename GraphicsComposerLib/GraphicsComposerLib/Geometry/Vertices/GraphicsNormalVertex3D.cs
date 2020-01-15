using System;
using System.Drawing;
using GeometryComposerLib.BasicMath.Tuples;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Vertices
{
    public sealed class GraphicsNormalVertex3D 
        : IGraphicsNormalVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

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
            => false;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataInfo3D DataInfo
            => GraphicsVertexDataInfo3D.CreateNormalVertexDataInfo();

        public bool HasNaNComponent 
            => Point.HasNaNComponent || 
               double.IsNaN(NormalX) ||
               double.IsNaN(NormalY) ||
               double.IsNaN(NormalZ);


        public GraphicsNormalVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
        }

        public GraphicsNormalVertex3D(int index, ITuple3D point, ITuple3D normal)
        {
            Index = index;
            Point = point;
            Normal.Set(normal);
        }

        public GraphicsNormalVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            Normal.Set(vertex.Normal);
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