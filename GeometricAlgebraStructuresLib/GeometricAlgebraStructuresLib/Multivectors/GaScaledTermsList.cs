using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public sealed class GaScaledTermsList<T> : IEnumerable<IGaTerm<T>>
    {
        private readonly List<T> _scalingFactorsList;
        
        private readonly List<IEnumerable<IGaTerm<T>>> _baseTermsList;

        
        public IGaScalarDomain<T> ScalarDomain { get; }

        public int Count 
            => _scalingFactorsList.Count;
        

        public GaScaledTermsList(IGaScalarDomain<T> scalarDomain)
        {
            ScalarDomain = scalarDomain;
            
            _scalingFactorsList = new List<T>();
            _baseTermsList = new List<IEnumerable<IGaTerm<T>>>();
        }

        public GaScaledTermsList(IGaScalarDomain<T> scalarDomain, int capacity)
        {
            ScalarDomain = scalarDomain;
            
            _scalingFactorsList = new List<T>(capacity);
            _baseTermsList = new List<IEnumerable<IGaTerm<T>>>(capacity);
        }


        public void Clear()
        {
            _scalingFactorsList.Clear();
            _baseTermsList.Clear();
        }
        
        public void Add(T scalingFactor, IEnumerable<IGaTerm<T>> baseTerms)
        {
            _scalingFactorsList.Add(scalingFactor);
            _baseTermsList.Add(baseTerms);
        }

        public IEnumerator<IGaTerm<T>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                var scalingFactor = _scalingFactorsList[i];
                var baseTerms = _baseTermsList[i];

                foreach (var baseTerm in baseTerms)
                    yield return baseTerm.GetCopy(ScalarDomain.Times(scalingFactor, baseTerm.Scalar));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}