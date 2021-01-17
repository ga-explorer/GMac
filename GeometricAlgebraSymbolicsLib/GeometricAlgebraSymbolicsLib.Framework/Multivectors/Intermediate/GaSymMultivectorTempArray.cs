using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors.Intermediate
{
    public sealed class GaSymMultivectorTempArray : IGaSymMultivectorTemp
    {
        public static GaSymMultivectorTempArray Create(int gaSpaceDim)
        {
            return new GaSymMultivectorTempArray(gaSpaceDim);
        }


        private readonly List<Expr>[] _coefsArray;


        public Expr this[int id]
            => _coefsArray[id].Simplify();

        public Expr this[int grade, int index]
            => _coefsArray[GaFrameUtils.BasisBladeId(grade, index)].Simplify();

        public IEnumerable<int> BasisBladeIds
        {
            get
            {
                var id = 0;
                foreach (var coef in _coefsArray)
                { 
                    if (coef != null)
                        yield return id;

                    id++;
                }
            }
        }

        public IEnumerable<int> NonZeroBasisBladeIds
        {
            get
            {
                var id = 0;
                foreach (var coef in _coefsArray.Select(c => c.Simplify()))
                {
                    if (!coef.IsZero())
                        yield return id;

                    id++;
                }
            }
        }

        public IEnumerable<MathematicaScalar> BasisBladeScalars
            => _coefsArray
                .Select(c => c.Simplify())
                .Where(c => !c.IsNullOrZero())
                .Select(c => c.ToMathematicaScalar());

        public IEnumerable<Expr> BasisBladeExprScalars
            => _coefsArray
                .Where(c => c != null)
                .Select(c => c.Simplify());

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars 
            => _coefsArray
            .Select(c => c.Simplify())
            .Where(c => !c.IsNullOrZero())
            .Select(c => c.ToMathematicaScalar());


        public IEnumerable<Expr> NonZeroBasisBladeExprScalars
            => _coefsArray
                .Select(c => c.Simplify())
                .Where(c => !c.IsNullOrZero());

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> Terms
        {
            get
            {
                var id = 0;
                foreach (var coef in _coefsArray)
                {
                    if (coef != null)
                        yield return new KeyValuePair<int, MathematicaScalar>(
                            id, 
                            coef.Simplify().ToMathematicaScalar()
                        );

                    id++;
                }
            }
        }


        public IEnumerable<KeyValuePair<int, Expr>> ExprTerms
        {
            get
            {
                var id = 0;
                foreach (var coef in _coefsArray)
                {
                    if (coef != null)
                        yield return new KeyValuePair<int, Expr>(
                            id, 
                            coef.Simplify()
                        );

                    id++;
                }
            }
        }

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> NonZeroTerms
        {
            get
            {
                var id = 0;
                foreach (var coef in _coefsArray.Select(c => c.Simplify()))
                {
                    if (!coef.IsZero())
                        yield return new KeyValuePair<int, MathematicaScalar>(
                            id, 
                            coef.ToMathematicaScalar()
                        );

                    id++;
                }
            }
        }

        public IEnumerable<KeyValuePair<int, Expr>> NonZeroExprTerms
        {
            get
            {
                var id = 0;
                foreach (var coef in _coefsArray.Select(c => c.Simplify()))
                {
                    if (!coef.IsZero())
                        yield return new KeyValuePair<int, Expr>(id, coef);

                    id++;
                }
            }
        }


        public bool ContainsBasisBlade(int id)
        {
            return _coefsArray[id] != null;
        }

        public bool IsTemp 
            => true;

        public int TermsCount 
            => _coefsArray.Count(c => c != null);

        public int VSpaceDimension
            => _coefsArray.Length.ToVSpaceDimension();

        public int GaSpaceDimension
            => _coefsArray.Length;


        private GaSymMultivectorTempArray(int gaSpaceDim)
        {
            _coefsArray = new List<Expr>[gaSpaceDim];
        }


        public IGaSymMultivectorTemp AddFactor(int id, Expr coef)
        {
            if (ReferenceEquals(_coefsArray[id], null))
                _coefsArray[id] = new List<Expr>();

            _coefsArray[id].Add(coef);

            return this;
        }

        public bool IsTerm()
        {
            return NonZeroBasisBladeScalars.Count() <= 1;
        }

        public bool IsScalar()
        {
            return ExprTerms.All(t => t.Key == 0 || t.Value.IsNullOrZero());
        }

        public bool IsZero()
        {
            return BasisBladeScalars.All(t => t.IsNullOrZero());
        }

        public bool IsEqualZero()
        {
            return BasisBladeScalars.All(t => t.IsNullOrEqualZero());
        }

        public IGaSymMultivectorTemp AddFactor(int id, bool isNegative, Expr coef)
        {
            if (ReferenceEquals(_coefsArray[id], null))
                _coefsArray[id] = new List<Expr>();

            _coefsArray[id].Add(isNegative ? Mfs.Minus[coef] : coef);

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(int id, Expr coef)
        {
            if (ReferenceEquals(_coefsArray[id], null))
            {
                _coefsArray[id] = new List<Expr> {coef};

                return this;
            }

            _coefsArray[id].Clear();
            _coefsArray[id].Add(coef);

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(int id, bool isNegative, Expr coef)
        {
            if (ReferenceEquals(_coefsArray[id], null))
            {
                _coefsArray[id] = new List<Expr> { isNegative ? Mfs.Minus[coef] : coef };

                return this;
            }

            _coefsArray[id].Clear();
            _coefsArray[id].Add(isNegative ? Mfs.Minus[coef] : coef);

            return this;
        }

        public void Simplify()
        {
            for (var id = 0; id < _coefsArray.Length; id++)
                if (_coefsArray[id].IsNullOrZero())
                    _coefsArray[id] = null;
        }

        public MathematicaScalar[] TermsToArray()
        {
            var scalarsArray = new MathematicaScalar[GaSpaceDimension];

            var id = 0;
            foreach (var coef in _coefsArray.Select(c => c.Simplify()))
            {
                if (!coef.IsZero())
                    scalarsArray[id] = coef.ToMathematicaScalar();

                id++;
            }

            return scalarsArray;
        }

        public Expr[] TermsToExprArray()
        {
            var scalarsArray = new Expr[GaSpaceDimension];

            var id = 0;
            foreach (var coef in _coefsArray.Select(c => c.Simplify()))
            {
                if (!coef.IsZero())
                    scalarsArray[id] = coef;

                id++;
            }

            return scalarsArray;
        }

        public GaSymMultivector ToMultivector()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            var id = 0;
            foreach (var coef in _coefsArray.Select(c => c.Simplify()))
            {
                if (!coef.IsZero())
                    mv.SetTermCoef(id, coef);

                id++;
            }

            return mv;
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            foreach (var id in GaFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public IEnumerator<KeyValuePair<int, Expr>> GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }
    }
}
