using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.GeometricAlgebra.CGA5D;
using GMac.Benchmarks.Samples.Computations;
using TextComposerLib.Text.Linear;

namespace GMac.Benchmarks.Samples.Visualizations.Numeric
{
    public sealed class Sample2 : IGMacSample
    {
        public string Title
            => "Study scalar distance functions for rendering blades using ray marching";

        public string Description
            => "Study scalar distance functions for rendering blades using ray marching";


        public string Execute()
        {
            var composer = new LinearTextComposer();

            var blade = Cga5DMultivectorGeometry.CreateOpnsSphere(
                new Tuple3D(0, 0, 0),
                1
            );

            var rayOrigin = new Tuple3D(0, 0, 0);
            var rayDirection = new Tuple3D(1, 2, -4).ToUnitVector();
            var minValue = -2.0d;
            var maxValue = 2.0d;
            var valuesCount = 1024.0d;
            var deltaValue = (maxValue - minValue) / (valuesCount - 1);
            
            for (var i = 0; i < valuesCount; i++)
            {
                var t = minValue + deltaValue * i;

                var point = rayOrigin + t * rayDirection;

                var sdf = blade.ComputeSdf(point);

                composer
                    .AppendAtNewLine(t.ToString("G"))
                    .Append(",")
                    .AppendLine(sdf.ToString("G"));
            }

            return composer.ToString();
        }
    }
}
