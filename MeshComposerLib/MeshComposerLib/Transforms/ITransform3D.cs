using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace MeshComposerLib.Transforms
{
    public interface ITransform3D
    {
        Tuple3D MapPoint(Tuple3D point);
    }
}
