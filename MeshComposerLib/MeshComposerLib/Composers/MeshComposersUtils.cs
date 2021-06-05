using System.Collections.Generic;
using System.Linq;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using MeshComposerLib.Geometry.PathsMesh;
using MeshComposerLib.Geometry.PathsMesh.Space3D;
using MeshComposerLib.Geometry.PointsPath;
using MeshComposerLib.Geometry.PointsPath.Space3D;

namespace MeshComposerLib.Composers
{
    public static class MeshComposersUtils
    {
        public static IPathsMesh3D ComposeParallelogramPathMesh(Tuple3D cornerPoint, Tuple3D baseVector, Tuple3D sideVector, int basePointsCount, int sidePointsCount)
        {
            var meshPathsList = new List<IPointsPath3D>(basePointsCount);

            var sidePoints = 
                sidePointsCount
                    .GetRegularSamples(0.0d, 1.0d)
                    .Lerp(cornerPoint, cornerPoint + sideVector);

            foreach (var pathFirstPoint in sidePoints)
            {
                var basePoints = 
                    basePointsCount
                        .GetRegularSamples(0.0d, 1.0d)
                        .Lerp(pathFirstPoint, pathFirstPoint + baseVector)
                        .Cast<ITuple3D>();

                var path = new ArrayPointsPath3D(basePoints);

                meshPathsList.Add(path);
            }

            return new ListPathsMesh3D(basePointsCount, meshPathsList);
        }
    }
}
