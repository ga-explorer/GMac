using System.Collections.Generic;

namespace GeometryComposerLib.BasicShapes
{
    public interface IGeometricObjectsContainer2D<out T> 
        : IFiniteGeometricShape2D, IReadOnlyList<T>
        where T : IFiniteGeometricShape2D
    {

    }
}