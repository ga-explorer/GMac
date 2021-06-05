using DataStructuresLib.Sequences.Periodic1D;
using EuclideanGeometryLib.BasicMath.Tuples;
using MeshComposerLib.Geometry.PointsMesh;

namespace MeshComposerLib.Geometry.PointsPath.Space3D
{
    public sealed class PointsMeshSlicePointsPath3D
        : PSeqSlice1D<ITuple3D>, IPointsPath3D
    {
        public IPointsMesh3D BaseMesh { get; }


        internal PointsMeshSlicePointsPath3D(IPointsMesh3D baseMesh, int sliceDimension, int sliceIndex) 
            : base(baseMesh, sliceDimension, sliceIndex)
        {
            BaseMesh = baseMesh;
        }
    }
}
