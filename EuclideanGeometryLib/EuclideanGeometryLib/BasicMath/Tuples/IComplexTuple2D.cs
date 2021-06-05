using System.Numerics;
using DataStructuresLib.Basic;

namespace EuclideanGeometryLib.BasicMath.Tuples
{
    public interface IComplexTuple2D : IGeometryElement, IPair<Complex>
    {
        Complex X { get; }

        Complex Y { get; }
    }
}