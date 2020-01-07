using GeometryComposerLib.BasicMath;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.Curves.Space3D
{
    public interface ICurve3D : IGeometryElement
    {
        Tuple3D GetPointAt(double t);
    }
}