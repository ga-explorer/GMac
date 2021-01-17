using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public sealed class GaScaledTerms<T> : IEnumerable<IGaTerm<T>>
    {
        public T ScalingFactor { get; }
        
        public IEnumerable<IGaTerm<T>> BaseTerms { get; }
        
        public IGaScalarDomain<T> ScalarDomain { get; }


        public GaScaledTerms(T scalingFactor, IEnumerable<IGaTerm<T>> baseTerms, IGaScalarDomain<T> scalarDomain)
        {
            ScalingFactor = scalingFactor;
            BaseTerms = baseTerms;
            ScalarDomain = scalarDomain;
        }


        public IEnumerator<IGaTerm<T>> GetEnumerator()
        {
            return BaseTerms
                .Select(t => t.GetCopy(ScalarDomain.Times(ScalingFactor, t.Scalar)))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}