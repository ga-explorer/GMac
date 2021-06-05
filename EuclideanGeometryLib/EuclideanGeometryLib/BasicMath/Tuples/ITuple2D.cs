using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Tuples
{
    public interface ITuple2D : IGeometryElement, IPair<double>
    {
        double X { get; }

        double Y { get; }


        Tuple2D ToTuple2D();
    }
}