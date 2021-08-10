﻿using System;
using System.Drawing;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public sealed class GraphicsVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public ITuple2D TextureUv
        {
            get => Tuple2D.Zero;
            set => throw new InvalidOperationException();
        }

        public IGraphicsNormal3D Normal
            => null;

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => false;

        public bool HasColor 
            => false;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.PositionVertex;

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

        public bool IsValid
            => Point.IsValid;

        public bool IsInvalid
            => Point.IsInvalid;


        public GraphicsVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
        }

        public GraphicsVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
        }

    }
}