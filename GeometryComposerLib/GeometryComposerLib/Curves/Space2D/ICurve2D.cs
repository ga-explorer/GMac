using GeometryComposerLib.BasicMath;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.Curves.Space2D
{
    public interface ICurve2D : IGeometryElement
    {
        Tuple2D GetPointAt(double t);
    }
}