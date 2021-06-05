using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using MeshComposerLib.Geometry.PointsPath.Space2D;

namespace MeshComposerLib.Geometry.PointsMesh
{
    public interface IPointsMesh2D
        : IPeriodicSequence2D<ITuple2D>
    {
        PointsMeshSlicePointsPath2D GetSlicePathAt(int dimension, int index);
    }
}