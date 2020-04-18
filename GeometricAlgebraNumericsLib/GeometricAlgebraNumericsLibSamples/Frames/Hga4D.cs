using System;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLibSamples.Structures;

namespace GeometricAlgebraNumericsLibSamples.Frames
{
    /// <summary>
    /// 4D homogeneous GA frame (ex, ey, ez, eo)
    /// </summary>
    public static class Hga4D
    {
        public static GaNumFrameEuclidean Frame { get; }
            = GaNumFrame.CreateEuclidean(4);


        public static GaNumDgrMultivector PointToMultivector(Point3D point)
        {
            return GaNumDgrMultivector.CreateVector(
                point.X,
                point.Y,
                point.Z,
                1
            );
        }

        public static GaNumDgrMultivector LineToMultivector(LineSegment3D line)
        {
            var mv1 = PointToMultivector(line.Point1);
            var mv2 = PointToMultivector(line.Point2);

            return mv1.Op(mv2);
        }

        public static GaNumDgrMultivector PlaneToMultivector(Triangle3D plane)
        {
            var mv1 = PointToMultivector(plane.Point1);
            var mv2 = PointToMultivector(plane.Point2);
            var mv3 = PointToMultivector(plane.Point3);

            return mv1.Op(mv2).Op(mv3);
        }

        public static Point3D MultivectorToPoint(GaNumDgrMultivector mv)
        {
            return new Point3D(
                mv[1, 0],
                mv[1, 1],
                mv[1, 2]
            );
        }

        public static Point3D ReflectPointOnPlaneHga4D(this Point3D point, Triangle3D plane)
        {
            throw new NotImplementedException();

            //var mvA = PointToMultivector(point);
            
            //var mvQ1 = PointToMultivector(plane.Point1);
            //var mvQ2 = PointToMultivector(plane.Point2);
            //var mvQ3 = PointToMultivector(plane.Point3);

            //var mvV = (mvQ1 - mvQ2).Op(mvQ2 - mvQ3);

            //var mvB = mvQ1 + Frame.EvenVersorProduct(mvV, mvA - mvQ1);

            //return MultivectorToPoint(mvB);
        }

    }
}