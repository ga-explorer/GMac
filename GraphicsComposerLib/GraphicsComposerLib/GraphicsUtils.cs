using GeometryComposerLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Constants;

namespace GraphicsComposerLib
{
    public static class GraphicsUtils
    {
        public static Tuple2D GetVector2D(this GraphicsAxis2D axis)
        {
            if (axis == GraphicsAxis2D.PositiveX)
                return new Tuple2D(1, 0);

            if (axis == GraphicsAxis2D.NegativeX)
                return new Tuple2D(-1, 0);

            if (axis == GraphicsAxis2D.PositiveY)
                return new Tuple2D(0, 1);

            return new Tuple2D(0, -1);
        }

        public static Tuple3D GetVector3D(this GraphicsAxis2D axis)
        {
            if (axis == GraphicsAxis2D.PositiveX)
                return new Tuple3D(1, 0, 0);

            if (axis == GraphicsAxis2D.NegativeX)
                return new Tuple3D(-1, 0, 0);

            if (axis == GraphicsAxis2D.PositiveY)
                return new Tuple3D(0, 1, 0);

            return new Tuple3D(0, -1, 0);
        }

        public static Tuple3D GetVector3D(this GraphicsAxis3D axis)
        {
            if (axis == GraphicsAxis3D.PositiveX)
                return new Tuple3D(1, 0, 0);

            if (axis == GraphicsAxis3D.NegativeX)
                return new Tuple3D(-1, 0, 0);

            if (axis == GraphicsAxis3D.PositiveY)
                return new Tuple3D(0, 1, 0);

            if (axis == GraphicsAxis3D.NegativeY)
                return new Tuple3D(0, -1, 0);

            if (axis == GraphicsAxis3D.PositiveZ)
                return new Tuple3D(0, 0, 1);

            return new Tuple3D(0, 0, -1);
        }
    }
}
