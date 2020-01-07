using DataStructuresLib.Basic;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.BasicMath.Tuples
{
    public interface ITuple2D : IGeometryElement, IPair<double>
    {
        double X { get; }

        double Y { get; }


        Tuple2D ToTuple2D();
    }
}