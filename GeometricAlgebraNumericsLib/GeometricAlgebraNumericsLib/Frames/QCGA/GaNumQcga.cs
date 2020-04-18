using System.Linq;
using GeometricAlgebraNumericsLib.Geometry.Primitives;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GeometricAlgebraNumericsLib.Frames.QCGA
{
    public static class GaNumQcga
    {
        public static GaNumFrameNonOrthogonal Frame { get; }
            = GaNumFrame.CreateQuadricConformal();


        public static int e1Id => 1;
        public static int e2Id => 1 << 1;
        public static int e3Id => 1 << 2;

        public static int no1Id => 1 << 3;
        public static int no2Id => 1 << 4;
        public static int no3Id => 1 << 5;
        public static int no4Id => 1 << 6;
        public static int no5Id => 1 << 7;
        public static int no6Id => 1 << 8;

        public static int ni1Id => 1 << 9;
        public static int ni2Id => 1 << 10;
        public static int ni3Id => 1 << 11;
        public static int ni4Id => 1 << 12;
        public static int ni5Id => 1 << 13;
        public static int ni6Id => 1 << 14;

        public static int IeId 
            => e1Id | e2Id | e3Id;

        public static int InoId 
            => no1Id | no2Id | no3Id | no4Id | no5Id | no6Id;

        public static int IniId 
            => ni1Id | ni2Id | ni3Id | ni4Id | ni5Id | ni6Id;

        public static int IId 
            => e1Id | e2Id | e3Id | 
               no1Id | no2Id | no3Id | no4Id | no5Id | no6Id |
               ni1Id | ni2Id | ni3Id | ni4Id | ni5Id | ni6Id;


        public static GaNumSarMultivector e1 { get; }
        public static GaNumSarMultivector e2 { get; }
        public static GaNumSarMultivector e3 { get; }

        public static GaNumSarMultivector no1 { get; }
        public static GaNumSarMultivector no2 { get; }
        public static GaNumSarMultivector no3 { get; }
        public static GaNumSarMultivector no4 { get; }
        public static GaNumSarMultivector no5 { get; }
        public static GaNumSarMultivector no6 { get; }

        public static GaNumSarMultivector ni1 { get; }
        public static GaNumSarMultivector ni2 { get; }
        public static GaNumSarMultivector ni3 { get; }
        public static GaNumSarMultivector ni4 { get; }
        public static GaNumSarMultivector ni5 { get; }
        public static GaNumSarMultivector ni6 { get; }

        public static GaNumSarMultivector no { get; }
        public static GaNumSarMultivector ni { get; }

        public static GaNumSarMultivector Ie { get; }
        public static GaNumSarMultivector Ino { get; }
        public static GaNumSarMultivector Ini { get; }

        public static GaNumSarMultivector Itno { get; }
        public static GaNumSarMultivector Itni { get; }

        public static GaNumSarMultivector I { get; }
        public static GaNumSarMultivector Iinv { get; }


        static GaNumQcga()
        {
            var vSpaceDim = Frame.VSpaceDimension;
            var gaSpaceDim = Frame.GaSpaceDimension;

            e1 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, e1Id);
            e2 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, e2Id);
            e3 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, e3Id);

            no1 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, no1Id);
            ni1 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, ni1Id);

            no2 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, no2Id);
            ni2 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, ni2Id);

            no3 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, no3Id);
            ni3 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, ni3Id);

            no4 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, no4Id);
            ni4 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, ni4Id);

            no5 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, no5Id);
            ni5 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, ni5Id);

            no6 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, no6Id);
            ni6 = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, ni6Id);

            no = no1 + no2 + no3;
            ni = (ni1 + ni2 + ni3) / 3;

            Ie = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, IeId);
            Ino = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, InoId);
            Ini = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, IniId);

            I = GaNumSarMultivector.CreateBasisBlade(vSpaceDim, IId);
            Iinv = -I;

            Itno = (no1 - no2).Op(no2 - no3, no4, no5, no6);
            Itni = (ni1 - ni2).Op(ni2 - ni3, ni4, ni5, ni6);
        }


        public static GaNumSarMultivector ToQcgaMultivector(this GaNumPoint3D point)
        {
            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            var mv = 
                new GaNumSarMultivectorFactory(Frame.VSpaceDimension)
                    .SetTerm(e1Id, x)
                    .SetTerm(e2Id, y)
                    .SetTerm(e3Id, z)
                    .SetTerm(ni1Id, x * x / 2)
                    .SetTerm(ni2Id, y * y / 2)
                    .SetTerm(ni3Id, z * z / 2)
                    .SetTerm(ni4Id, x * y)
                    .SetTerm(ni5Id, x * z)
                    .SetTerm(ni6Id, y * z)
                    .SetTerm(no1Id, 1)
                    .SetTerm(no2Id, 1)
                    .SetTerm(no3Id, 1)
                    .GetSarMultivector();

            return mv;
        }

        public static GaNumSarMultivector NormalizePointQcga(this GaNumSarMultivector mvPoint)
        {
            return mvPoint / (-Frame.Lcp[mvPoint, ni][0]);
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumSpherePoints3D sphere)
        {
            var mv1 = sphere.Point1.ToQcgaMultivector();
            var mv2 = sphere.Point2.ToQcgaMultivector();
            var mv3 = sphere.Point3.ToQcgaMultivector();
            var mv4 = sphere.Point4.ToQcgaMultivector();

            return mv1.Op(mv2, mv3, mv4, Itni, Itno);
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumSphere3D sphere)
        {
            return sphere.Center.ToQcgaMultivector() - (sphere.RadiusSquared / 2) * ni;
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumPlanePoints3D plane)
        {
            var mv1 = plane.Point1.ToQcgaMultivector();
            var mv2 = plane.Point2.ToQcgaMultivector();
            var mv3 = plane.Point3.ToQcgaMultivector();

            return mv1.Op(mv2, mv3, ni, Itni, Itno);
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumPlane3D plane)
        {
            var mvNormal = 
                new GaNumSarMultivectorFactory(Frame.VSpaceDimension)
                    .SetTerm(e1Id, plane.Normal.X)
                    .SetTerm(e2Id, plane.Normal.Y)
                    .SetTerm(e3Id, plane.Normal.Z)
                    .GetSarMultivector();

            return mvNormal + plane.OriginDistance * ni;
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumLinePoints3D line)
        {
            var mv1 = line.Point1.ToQcgaMultivector();
            var mv2 = line.Point2.ToQcgaMultivector();

            return mv1.Op(mv2, ni, Itni, Itno);
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumLinePlucker3D line)
        {
            var mvSupport = 
                new GaNumSarMultivectorFactory(Frame.VSpaceDimension)
                    .SetTerm(e1Id, line.Support.X)
                    .SetTerm(e2Id, line.Support.Y)
                    .SetTerm(e3Id, line.Support.Z)
                    .GetSarMultivector();

            var mvMoment = 
                new GaNumSarMultivectorFactory(Frame.VSpaceDimension)
                    .SetTerm(e1Id | e2Id, line.Moment.Xy)
                    .SetTerm(e1Id | e3Id, line.Moment.Xz)
                    .SetTerm(e2Id | e3Id, line.Moment.Yz)
                    .GetSarMultivector();

            return -3 * (mvSupport.Op(Ini, Ino) - mvMoment.Op(Ini, Itno));
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumQuadricPoints3D quadric)
        {
            var mv1 = quadric.Points[0].ToQcgaMultivector();
            var mvList = quadric.Points.Skip(1).Select(p => p.ToQcgaMultivector());

            return mv1.Op(mvList).Op(Itno);
        }

        public static GaNumSarMultivector ToQcgaMultivector(this GaNumQuadric3D quadric)
        {
            var j = -quadric.Coefficients[9] / 3.0d;

            return new GaNumSarMultivectorFactory(Frame.VSpaceDimension)
                .SetTerm(no1Id, -2 * quadric.Coefficients[0])
                .SetTerm(no2Id, -2 * quadric.Coefficients[1])
                .SetTerm(no3Id, -2 * quadric.Coefficients[2])
                .SetTerm(no4Id, -2 * quadric.Coefficients[3])
                .SetTerm(no5Id, -2 * quadric.Coefficients[4])
                .SetTerm(no6Id, -2 * quadric.Coefficients[5])
                .SetTerm(e1Id, quadric.Coefficients[6])
                .SetTerm(e2Id, quadric.Coefficients[7])
                .SetTerm(e3Id, quadric.Coefficients[8])
                .SetTerm(ni1Id, j)
                .SetTerm(ni2Id, j)
                .SetTerm(ni3Id, j)
                .GetSarMultivector();
        }
    }
}
