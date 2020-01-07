using GeometryComposerLib.BasicMath;
using GeometryComposerLib.Borders.Space2D.Immutable;
using GeometryComposerLib.Borders.Space2D.Mutable;

namespace GeometryComposerLib.BasicShapes
{
    public interface IFiniteGeometricShape2D 
        : IGeometryElement, IIntersectable
    {
        BoundingBox2D GetBoundingBox();

        MutableBoundingBox2D GetMutableBoundingBox();
    }
}