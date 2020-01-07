using System.Numerics;
using DataStructuresLib.Basic;

namespace GeometryComposerLib.BasicMath.Tuples
{
    public interface IComplexTuple3D : IGeometryElement, ITriplet<Complex>
    {
        Complex X { get; }

        Complex Y { get; }

        Complex Z { get; }
    }
}