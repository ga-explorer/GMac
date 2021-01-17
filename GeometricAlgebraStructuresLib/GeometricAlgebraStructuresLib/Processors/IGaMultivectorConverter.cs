using GeometricAlgebraStructuresLib.Multivectors;

namespace GeometricAlgebraStructuresLib.Processors
{
    public interface IGaMultivectorConverter<T1, T2>
    {
        IGaMultivector<T2> Convert(IGaMultivector<T1> mv);
    }
}