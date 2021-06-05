using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Tuples
{
    public interface ITuple3D : IGeometryElement, ITriplet<double>
    {
        double X { get; }

        double Y { get; }

        double Z { get; }


        Tuple3D ToTuple3D();
    }
}