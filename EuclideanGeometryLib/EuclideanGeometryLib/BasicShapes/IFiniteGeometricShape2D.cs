using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Mutable;

namespace EuclideanGeometryLib.BasicShapes
{
    public interface IFiniteGeometricShape2D 
        : IGeometryElement, IIntersectable
    {
        BoundingBox2D GetBoundingBox();

        MutableBoundingBox2D GetMutableBoundingBox();
    }
}