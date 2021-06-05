using System;
using System.Diagnostics;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public sealed class GraphicsNormal3D : ITuple3D
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        public bool HasNaNComponent 
            => double.IsNaN(X) ||
               double.IsNaN(Y) ||
               double.IsNaN(Z);


        /// <summary>
        /// Reset the normal to zero
        /// </summary>
        /// <returns></returns>
        public GraphicsNormal3D Reset()
        {
            X = 0;
            Y = 0;
            Z = 0;

            return this;
        }

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public GraphicsNormal3D Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="normal"></param>
        /// <returns></returns>
        public GraphicsNormal3D Set(ITuple3D normal)
        {
            X = normal.X;
            Y = normal.Y;
            Z = normal.Z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        /// <summary>
        /// Add the given vector to the normal of this vertex
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        /// <returns></returns>
        public GraphicsNormal3D Update(double dx, double dy, double dz)
        {
            X += dx;
            Y += dy;
            Z += dz;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        /// <summary>
        /// Add the given vector to this normal
        /// </summary>
        /// <param name="dNormal"></param>
        /// <returns></returns>
        public GraphicsNormal3D Update(Tuple3D dNormal)
        {
            X += dNormal.X;
            Y += dNormal.Y;
            Z += dNormal.Z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        /// <summary>
        /// Make the normal vector of this vertex a unit vector if not near zero
        /// </summary>
        /// <returns></returns>
        public GraphicsNormal3D MakeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (s.IsAlmostZero())
                return this;

            s = 1.0d / s;
            X *= s;
            Y *= s;
            Z *= s;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public GraphicsNormal3D MakeNegativeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (s.IsAlmostZero())
                return this;

            s = -1.0d / s;
            X *= s;
            Y *= s;
            Z *= s;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public GraphicsNormal3D MakeNegative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public Tuple3D ToTuple3D()
        {
            return new Tuple3D(X, Y, Z);
        }
    }
}