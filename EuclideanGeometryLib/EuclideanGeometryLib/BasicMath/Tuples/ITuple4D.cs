using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Tuples
{
    public interface ITuple4D : IGeometryElement, IQuad<double>
    {
        double X { get; }

        double Y { get; }

        double Z { get; }

        double W { get; }


        Tuple4D ToTuple4D();
    }
}