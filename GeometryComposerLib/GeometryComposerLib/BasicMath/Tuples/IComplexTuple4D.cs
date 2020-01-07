using System.Numerics;
using DataStructuresLib.Basic;

namespace GeometryComposerLib.BasicMath.Tuples
{
    public interface IComplexTuple4D : IGeometryElement, IQuad<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }

        Complex W { get; }
    }
}