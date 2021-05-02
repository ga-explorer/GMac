using System;
using DataStructuresLib.Dictionary;
using GeometricAlgebraStructuresLib.Scalars;

namespace GeometricAlgebraStructuresLib.MotherAlgebra
{
    public sealed class GamMultivector<T>
    {
        public static GamMultivector<T> CreateZero(IGaScalarDomain<T> scalarDomain)
        {
            return new GamMultivector<T>(scalarDomain);
        }


        private readonly Dictionary3Keys<ulong, GamMultivectorTerm<T>> _termsDictionary
            = new Dictionary3Keys<ulong, GamMultivectorTerm<T>>();


        public IGaScalarDomain<T> ScalarDomain { get; }

        public T this[ulong positiveId]
        {
            get =>
                _termsDictionary.TryGetValue(positiveId, 0ul, 0ul, out var term)
                    ? term.Scalar
                    : ScalarDomain.GetZero();
            set
            {
                if (ScalarDomain.IsZero(value))
                {
                    _termsDictionary.Remove(positiveId, 0ul, 0ul);

                    return;
                }

                var term = new GamMultivectorTerm<T>(positiveId, 0ul, 0ul, value);

                if (_termsDictionary.ContainsKey(positiveId, 0ul, 0ul))
                    _termsDictionary[positiveId, 0ul, 0ul] = term;
                else
                    _termsDictionary.Add(positiveId, 0ul, 0ul, term);
            }
        }

        public T this[ulong positiveId, ulong negativeId]
        {
            get =>
                _termsDictionary.TryGetValue(positiveId, negativeId, 0ul, out var term)
                    ? term.Scalar
                    : ScalarDomain.GetZero();
            set
            {
                if (ScalarDomain.IsZero(value))
                {
                    _termsDictionary.Remove(positiveId, negativeId, 0ul);

                    return;
                }

                var term = new GamMultivectorTerm<T>(positiveId, negativeId, 0ul, value);

                if (_termsDictionary.ContainsKey(positiveId, negativeId, 0ul))
                    _termsDictionary[positiveId, negativeId, 0ul] = term;
                else
                    _termsDictionary.Add(positiveId, negativeId, 0ul, term);
            }
        }
        
        public T this[ulong positiveId, ulong negativeId, ulong zeroId]
        {
            get =>
                _termsDictionary.TryGetValue(positiveId, negativeId, zeroId, out var term)
                    ? term.Scalar
                    : ScalarDomain.GetZero();
            set
            {
                if (ScalarDomain.IsZero(value))
                {
                    _termsDictionary.Remove(positiveId, negativeId, zeroId);

                    return;
                }

                var term = new GamMultivectorTerm<T>(positiveId, negativeId, zeroId, value);

                if (_termsDictionary.ContainsKey(positiveId, negativeId, zeroId))
                    _termsDictionary[positiveId, negativeId, zeroId] = term;
                else
                    _termsDictionary.Add(positiveId, negativeId, zeroId, term);
            }
        }


        internal GamMultivector(IGaScalarDomain<T> scalarDomain)
        {
            ScalarDomain = scalarDomain 
                           ?? throw new ArgumentNullException(nameof(scalarDomain));
        }
    }
}