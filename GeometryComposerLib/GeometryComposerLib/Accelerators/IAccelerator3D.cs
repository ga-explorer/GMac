using GeometryComposerLib.BasicShapes;

namespace GeometryComposerLib.Accelerators
{
    public interface IAccelerator3D<out T>
        : IGeometricObjectsContainer3D<T>, IIntersectable
        where T : IFiniteGeometricShape3D
    {
    }
}