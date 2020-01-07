using DataStructuresLib.Basic;
using GeometryComposerLib.BasicMath.Tuples.Immutable;

namespace GeometryComposerLib.BasicMath.Tuples
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