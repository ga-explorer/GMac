using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    /// <summary>
    /// This class represents a multivector used for intermediate computations
    /// Updating the basis blade coefficients in this class is more efficient
    /// than regular multivectors. The scalar coefficients are stored in an
    /// array internally
    /// </summary>
    public sealed class GaNumMultivectorArray : IGaNumMultivectorMutable
    {
        public static GaNumMultivectorArray Create(int gaSpaceDim)
        {
            return new GaNumMultivectorArray(gaSpaceDim);
        }


        private readonly double[] _termsArray;


        public double this[int id] 
            => _termsArray[id];

        public double this[int grade, int index]
            => _termsArray[GaNumFrameUtils.BasisBladeId(grade, index)];

        public IEnumerable<int> BasisBladeIds 
            => Enumerable.Range(0, GaSpaceDimension);

        public IEnumerable<int> NonZeroBasisBladeIds
        {
            get
            {
                var id = 0;
                foreach (var coef in _termsArray)
                {
                    if (!coef.IsNearZero())
                        yield return id;

                    id++;
                }
            }
        }

        public IEnumerable<double> BasisBladeScalars 
            => _termsArray;

        public IEnumerable<double> NonZeroBasisBladeScalars 
            => _termsArray.Where(v => !v.IsNearZero());

        public IEnumerable<KeyValuePair<int, double>> Terms
        {
            get
            {
                var id = 0;
                foreach (var coef in _termsArray)
                {
                    yield return new KeyValuePair<int, double>(id, coef);

                    id++;
                }
            }
        }

        public int VSpaceDimension
            => _termsArray.Length.ToVSpaceDimension();

        public int GaSpaceDimension
            => _termsArray.Length;

        public IEnumerable<KeyValuePair<int, double>> NonZeroTerms
        {
            get
            {
                var id = 0;
                foreach (var coef in _termsArray)
                {
                    if (!coef.IsNearZero())
                        yield return new KeyValuePair<int, double>(id, coef);

                    id++;
                }
            }
        }

        public bool IsTemp => true;

        public int TermsCount => _termsArray.Length;

        public bool IsTerm()
        {
            return _termsArray.Count(v => !v.IsNearZero()) <= 1;
        }

        public bool IsScalar()
        {
            return _termsArray.Skip(1).All(v => v.IsNearZero());
        }

        public bool IsZero()
        {
            return _termsArray.All(v => v.IsNearZero());
        }

        public bool IsEmpty()
        {
            return false;
        }

        public bool IsNearZero(double epsilon)
        {
            return _termsArray.All(v => v.IsNearZero(epsilon));
        }

        public bool ContainsBasisBlade(int id)
        {
            return id >= 0 && id <= GaSpaceDimension;
        }


        private GaNumMultivectorArray(int gaSpaceDim)
        {
            _termsArray = new double[gaSpaceDim];
        }


        public IGaNumMultivectorMutable UpdateTerm(int id, double coef)
        {
            _termsArray[id] += coef;

            return this;
        }

        public IGaNumMultivectorMutable UpdateTerm(int id, bool isNegative, double coef)
        {
            if (isNegative)
                _termsArray[id] -= coef;
            else
                _termsArray[id] += coef;

            return this;
        }

        public IGaNumMultivectorMutable SetTerm(int id, double coef)
        {
            _termsArray[id] = coef;

            return this;
        }

        public IGaNumMultivectorMutable SetTerm(int id, bool isNegative, double coef)
        {
            _termsArray[id] = isNegative ? -coef : coef;

            return this;
        }

        public void Simplify()
        {
            for (var id = 0; id < _termsArray.Length; id++)
                if (_termsArray[id].IsNearZero())
                    _termsArray[id] = 0.0d;
        }

        public double[] TermsToArray()
        {
            var scalarsArray = new double[GaSpaceDimension];

            var id = 0;
            foreach (var coef in _termsArray)
            {
                if (!coef.IsNearZero())
                    scalarsArray[id] = coef;

                id++;
            }

            return scalarsArray; 
        }

        public GaNumMultivector ToMultivector()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            var id = 0;
            foreach (var coef in _termsArray)
            {
                if (!coef.IsNearZero())
                    mv.SetTerm(id, coef);

                id++;
            }

            return mv;
        }

        public GaNumMultivector GetVectorPart()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            foreach (var id in GaNumFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNearZero())
                    mv.SetTerm(id, coef);
            }

            return mv;
        }

        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }
    }
}
