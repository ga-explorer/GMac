using System;
using System.Text;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.BasicMath.Coordinates
{
    public sealed class UnitSphericalPosition3D : ISphericalPosition3D
    {
        public double Theta { get; }

        public double Phi { get; }

        public double R => 1;

        public double ThetaInDegrees
            => Theta.RadiansToDegrees();

        public double PhiInDegrees
            => Phi.RadiansToDegrees();

        public bool HasNaNComponent
            => double.IsNaN(Theta) ||
               double.IsNaN(Phi);

        public double X
            => Math.Sin(Theta) * Math.Cos(Phi);

        public double Y
            => Math.Sin(Theta) * Math.Sin(Phi);

        public double Z
            => Math.Cos(Theta);

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;


        public UnitSphericalPosition3D(double theta, double phi)
        {
            Theta = theta.ClampPeriodic(Math.PI);
            Phi = phi.ClampAngle();
        }


        public Tuple3D ToTuple3D()
            => new Tuple3D(
                Math.Sin(Theta) * Math.Cos(Phi),
                Math.Sin(Theta) * Math.Sin(Phi),
                Math.Cos(Theta)
            );

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Unit Spherical Position< theta: ")
                .Append(ThetaInDegrees.ToString("G"))
                .Append(", phi: ")
                .Append(PhiInDegrees.ToString("G"))
                .Append(" >")
                .ToString();
        }
    }
}