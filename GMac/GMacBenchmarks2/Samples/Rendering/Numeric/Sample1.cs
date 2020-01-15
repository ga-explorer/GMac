using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using GeometryComposerLib.BasicMath.Tuples.Immutable;
using GeometryComposerLib.GeometricAlgebra.CGA5D;
using GeometryComposerLib.SdfGeometry;
using GeometryComposerLib.SdfGeometry.Operations;
using GeometryComposerLib.SdfGeometry.Primitives;
using GMacBenchmarks2.Samples.Computations;
using GraphicsComposerLib.RayMarching;

namespace GMacBenchmarks2.Samples.Rendering.Numeric
{
    public sealed class Sample1 : IGMacSample
    {
        public string Title
            => "Render scalar distance functions using ray marching";

        public string Description
            => "Render scalar distance functions using ray marching";


        //private SdfBladeOpnsCga5D CreateSphereOpnsBlade(double radius)
        //{
        //    var frame = GaNumFrame.CreateOrthonormal("++++-");

        //    var p1 = new Tuple3D(radius, 0, 0);
        //    var p2 = new Tuple3D(0, radius, 0);
        //    var p3 = new Tuple3D(0, 0, radius);
        //    var p4 = new Tuple3D(radius, radius, radius).Normalize();

        //    var e1 = GaNumMultivector.CreateBasisBlade(frame.GaSpaceDimension, 1 << 0);
        //    var e2 = GaNumMultivector.CreateBasisBlade(frame.GaSpaceDimension, 1 << 1);
        //    var e3 = GaNumMultivector.CreateBasisBlade(frame.GaSpaceDimension, 1 << 2);
        //    var ep = GaNumMultivector.CreateBasisBlade(frame.GaSpaceDimension, 1 << 3);
        //    var en = GaNumMultivector.CreateBasisBlade(frame.GaSpaceDimension, 1 << 4);

        //    var no = (en - ep) / 2;
        //    var ni = en + ep;

        //    var mv1 = no + p1.X * e1 + 0.5 * p1.LengthSquared() * ni;
        //    var mv2 = no + p2.Y * e2 + 0.5 * p2.LengthSquared() * ni;
        //    var mv3 = no + p3.Z * e3 + 0.5 * p3.LengthSquared() * ni;
        //    var mv4 = no + p4.X * e1 + p4.Y * e2 + p4.Z * e3 + 0.5 * p4.LengthSquared() * ni;

        //    var blade = mv1.Op(mv2, mv3, mv4);

        //    var bladeScalars = new double[5];

        //    for (var index = 0; index < bladeScalars.Length; index++)
        //    {
        //        //var id = GaNumFrameUtils.BasisBladeId(4, i);
        //        bladeScalars[index] = blade[4, index];
        //    }

        //    return new SdfBladeOpnsCga5D(bladeScalars);
        //}

        public string Execute()
        {
            var scene = new RayMarchingScene3D();
            scene.Camera.ResolutionX = 640;
            scene.Camera.ResolutionY = 640;

            var sphere = new SdfSphere3D() { Radius = 0.25 };
            var xAxis = new SdfLineSegmentX3D() { Length = 1, Radius = 0.15 };
            var yAxis = new SdfLineSegmentY3D() { Length = 1, Radius = 0.15 };
            var zAxis = new SdfLineSegmentZ3D() { Length = 1, Radius = 0.15 };
            var surface = new SdfOr3D();
            surface.Surfaces.Add(sphere);
            surface.Surfaces.Add(xAxis);
            surface.Surfaces.Add(yAxis);
            surface.Surfaces.Add(zAxis);
            var surface1 = surface
                .RotateX(Math.PI / 10)
                .RotateY(Math.PI / 10);

            scene.Shape = new RayMarchingShape3D(
                //Cga5DMultivectorGeometry.CreateOpnsSphere(new Tuple3D(0, 0, 0), 0.5)
                Cga5DMultivectorGeometry.CreateIpnsSphere(new Tuple3D(0, 0, 0), 0.5)
                //new SdfSphere3D() { Radius = 0.5 }
            );

            var clock = new Stopwatch();

            clock.Start();
            var image = scene.Render();
            clock.Stop();

            image.Save("RayMarching.png", ImageFormat.Png);

            return "Done in " + clock.Elapsed;
        }
    }
}
