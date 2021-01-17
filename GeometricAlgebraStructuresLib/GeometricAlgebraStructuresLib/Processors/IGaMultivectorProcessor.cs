using GeometricAlgebraStructuresLib.Multivectors;

namespace GeometricAlgebraStructuresLib.Processors
{
    public interface IGaMultivectorProcessor<T>
    {
        IGaMultivector<T> Add(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Subtract(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Gp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Op(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Sp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Lcp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Rcp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Hip(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Fdp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Acp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
        
        IGaMultivector<T> Cp(IGaMultivector<T> mv1, IGaMultivector<T> mv2);
    }
}