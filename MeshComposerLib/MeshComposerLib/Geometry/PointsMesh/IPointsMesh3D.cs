using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using MeshComposerLib.Geometry.PointsPath.Space3D;

namespace MeshComposerLib.Geometry.PointsMesh
{
    public interface IPointsMesh3D
        : IPeriodicSequence2D<ITuple3D>
    {
        PointsMeshSlicePointsPath3D GetSlicePathAt(int dimension, int index);
    }
}