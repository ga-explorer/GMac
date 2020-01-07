using System.Linq;
using GeometricAlgebraNumericsLib.Geometry.Primitives;
using GeometricAlgebraNumericsLib.Multivectors;
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


        public static GaNumMultivector e1 { get; }
        public static GaNumMultivector e2 { get; }
        public static GaNumMultivector e3 { get; }

        public static GaNumMultivector no1 { get; }
        public static GaNumMultivector no2 { get; }
        public static GaNumMultivector no3 { get; }
        public static GaNumMultivector no4 { get; }
        public static GaNumMultivector no5 { get; }
        public static GaNumMultivector no6 { get; }

        public static GaNumMultivector ni1 { get; }
        public static GaNumMultivector ni2 { get; }
        public static GaNumMultivector ni3 { get; }
        public static GaNumMultivector ni4 { get; }
        public static GaNumMultivector ni5 { get; }
        public static GaNumMultivector ni6 { get; }

        public static GaNumMultivector no { get; }
        public static GaNumMultivector ni { get; }

        public static GaNumMultivector Ie { get; }
        public static GaNumMultivector Ino { get; }
        public static GaNumMultivector Ini { get; }

        public static GaNumMultivector Itno { get; }
        public static GaNumMultivector Itni { get; }

        public static GaNumMultivector I { get; }
        public static GaNumMultivector Iinv { get; }


        static GaNumQcga()
        {
            var gaSpaceDim = Frame.GaSpaceDimension;

            e1 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, e1Id);
            e2 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, e2Id);
            e3 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, e3Id);

            no1 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, no1Id);
            ni1 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, ni1Id);

            no2 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, no2Id);
            ni2 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, ni2Id);

            no3 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, no3Id);
            ni3 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, ni3Id);

            no4 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, no4Id);
            ni4 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, ni4Id);

            no5 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, no5Id);
            ni5 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, ni5Id);

            no6 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, no6Id);
            ni6 = GaNumMultivector.CreateBasisBlade(gaSpaceDim, ni6Id);

            no = no1 + no2 + no3;
            ni = (ni1 + ni2 + ni3) / 3;

            Ie = GaNumMultivector.CreateBasisBlade(gaSpaceDim, IeId);
            Ino = GaNumMultivector.CreateBasisBlade(gaSpaceDim, InoId);
            Ini = GaNumMultivector.CreateBasisBlade(gaSpaceDim, IniId);

            I = GaNumMultivector.CreateBasisBlade(gaSpaceDim, IId);
            Iinv = -I;

            Itno = (no1 - no2).Op(no2 - no3, no4, no5, no6);
            Itni = (ni1 - ni2).Op(ni2 - ni3, ni4, ni5, ni6);
        }


        public static GaNumMultivector ToQcgaMultivector(this GaNumPoint3D point)
        {
            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            var mv = GaNumMultivector
                .CreateZero(Frame.GaSpaceDimension)
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
                .ToMultivector();

            return mv;
        }

        public static GaNumMultivector NormalizePointQcga(this GaNumMultivector mvPoint)
        {
            return mvPoint / (-Frame.Lcp[mvPoint, ni][0]);
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumSpherePoints3D sphere)
        {
            var mv1 = sphere.Point1.ToQcgaMultivector();
            var mv2 = sphere.Point2.ToQcgaMultivector();
            var mv3 = sphere.Point3.ToQcgaMultivector();
            var mv4 = sphere.Point4.ToQcgaMultivector();

            return mv1.Op(mv2, mv3, mv4, Itni, Itno);
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumSphere3D sphere)
        {
            return sphere.Center.ToQcgaMultivector() - (sphere.RadiusSquared / 2) * ni;
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumPlanePoints3D plane)
        {
            var mv1 = plane.Point1.ToQcgaMultivector();
            var mv2 = plane.Point2.ToQcgaMultivector();
            var mv3 = plane.Point3.ToQcgaMultivector();

            return mv1.Op(mv2, mv3, ni, Itni, Itno);
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumPlane3D plane)
        {
            var mvNormal = GaNumMultivector
                .CreateZero(Frame.GaSpaceDimension)
                .SetTerm(e1Id, plane.Normal.X)
                .SetTerm(e2Id, plane.Normal.Y)
                .SetTerm(e3Id, plane.Normal.Z)
                .ToMultivector();

            return mvNormal + plane.OriginDistance * ni;
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumLinePoints3D line)
        {
            var mv1 = line.Point1.ToQcgaMultivector();
            var mv2 = line.Point2.ToQcgaMultivector();

            return mv1.Op(mv2, ni, Itni, Itno);
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumLinePlucker3D line)
        {
            var mvSupport = GaNumMultivector
                .CreateZero(Frame.GaSpaceDimension)
                .SetTerm(e1Id, line.Support.X)
                .SetTerm(e2Id, line.Support.Y)
                .SetTerm(e3Id, line.Support.Z)
                .ToMultivector();

            var mvMoment = GaNumMultivector
                .CreateZero(Frame.GaSpaceDimension)
                .SetTerm(e1Id | e2Id, line.Moment.Xy)
                .SetTerm(e1Id | e3Id, line.Moment.Xz)
                .SetTerm(e2Id | e3Id, line.Moment.Yz)
                .ToMultivector();

            return -3 * (mvSupport.Op(Ini, Ino) - mvMoment.Op(Ini, Itno));
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumQuadricPoints3D quadric)
        {
            var mv1 = quadric.Points[0].ToQcgaMultivector();
            var mvList = quadric.Points.Skip(1).Select(p => p.ToQcgaMultivector());

            return mv1.Op(mvList).Op(Itno);
        }

        public static GaNumMultivector ToQcgaMultivector(this GaNumQuadric3D quadric)
        {
            var j = -quadric.Coefficients[9] / 3.0d;

            return GaNumMultivector.CreateZero(Frame.GaSpaceDimension)
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
                .ToMultivector();
        }
    }
}
