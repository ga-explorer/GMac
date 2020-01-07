using GeometryComposerLib.BasicShapes;

namespace GeometryComposerLib.Accelerators
{
    public interface IAccelerator2D<out T> 
        : IGeometricObjectsContainer2D<T>, IIntersectable
        where T : IFiniteGeometricShape2D
    {
    }
}