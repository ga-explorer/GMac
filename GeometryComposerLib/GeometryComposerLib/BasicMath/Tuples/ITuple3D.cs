using DataStructuresLib.Basic;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.BasicMath.Tuples
{
    public interface ITuple3D : IGeometryElement, ITriplet<double>
    {
        double X { get; }

        double Y { get; }

        double Z { get; }


        Tuple3D ToTuple3D();
    }
}