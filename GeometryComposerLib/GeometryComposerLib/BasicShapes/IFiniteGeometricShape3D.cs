using GeometryComposerLib.BasicMath;
using GeometryComposerLib.Borders.Space3D.Immutable;
using GeometryComposerLib.Borders.Space3D.Mutable;

namespace GeometryComposerLib.BasicShapes
{
    public interface IFiniteGeometricShape3D 
        : IGeometryElement, IIntersectable
    {
        BoundingBox3D GetBoundingBox();

        MutableBoundingBox3D GetMutableBoundingBox();
    }
}