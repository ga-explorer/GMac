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
    public sealed class GaSymMultivectorTempHash : IGaSymMultivectorTemp
    {
        public static GaSymMultivectorTempHash Create(int gaSpaceDim)
        {
            return new GaSymMultivectorTempHash(gaSpaceDim);
        }


        private readonly Dictionary<int, List<Expr>> _termsDictionary;


        public Expr this[int id]
        {
            get
            {
                _termsDictionary.TryGetValue(id, out var coef);

                return ReferenceEquals(coef, null) 
                    ? Expr.INT_ZERO 
                    : coef.Simplify();
            }
        }

        public Expr this[int grade, int index]
        {
            get
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                _termsDictionary.TryGetValue(id, out var coef);

                return ReferenceEquals(coef, null)
                    ? Expr.INT_ZERO
                    : coef.Simplify();
            }
        }

        public IEnumerable<int> BasisBladeIds 
            => _termsDictionary.Keys;

        public IEnumerable<int> NonZeroBasisBladeIds
            => _termsDictionary
                .Where(p => !p.Value.Simplify().IsNullOrZero())
                .Select(p => p.Key);

        public IEnumerable<MathematicaScalar> BasisBladeScalars
            => _termsDictionary
                .Values
                .Select(v => v.Simplify().ToMathematicaScalar());

        public IEnumerable<Expr> BasisBladeExprScalars
            => _termsDictionary.Values.Select(v => v.Simplify());

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars
            => _termsDictionary
                .Values
                .Select(v => v.Simplify())
                .Where(v => !v.IsNullOrZero())
                .Select(v => v.ToMathematicaScalar());

        public IEnumerable<Expr> NonZeroBasisBladeExprScalars 
            => _termsDictionary
                .Values
                .Select(v => v.Simplify())
                .Where(v => !v.IsNullOrZero());

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> Terms
            => _termsDictionary
                .Select(p => new KeyValuePair<int, MathematicaScalar>(
                    p.Key,
                    p.Value.Simplify().ToMathematicaScalar()
                ));

        public IEnumerable<KeyValuePair<int, Expr>> ExprTerms
            => _termsDictionary
                .Select(p => new KeyValuePair<int, Expr>(
                    p.Key,
                    p.Value.Simplify()
                ));

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> NonZeroTerms
            => _termsDictionary
                .Select(p => new KeyValuePair<int, MathematicaScalar>(
                    p.Key, 
                    p.Value.Simplify().ToMathematicaScalar()
                ))
                .Where(p => !p.Value.Expression.IsNullOrZero());

        public IEnumerable<KeyValuePair<int, Expr>> NonZeroExprTerms 
            => _termsDictionary
            .Select(p => new KeyValuePair<int, Expr>(p.Key, p.Value.Simplify()))
            .Where(p => !p.Value.IsNullOrZero());

        public bool IsTemp 
            => true;

        public int TermsCount 
            => _termsDictionary.Count;

        public int VSpaceDimension
            => GaSpaceDimension.ToVSpaceDimension();

        public int GaSpaceDimension { get; }


        private GaSymMultivectorTempHash(int gaSpaceDim)
        {
            GaSpaceDimension = gaSpaceDim;
            _termsDictionary = new Dictionary<int, List<Expr>>();
        }


        public bool ContainsBasisBlade(int id)
        {
            return _termsDictionary.ContainsKey(id);
        }

        public bool IsTerm()
        {
            return NonZeroExprTerms.Count() <= 1;
        }

        public bool IsScalar()
        {
            return ExprTerms.All(v => v.Key == 0 || v.Value.IsNullOrZero());
        }

        public bool IsZero()
        {
            return ExprTerms.All(v => v.Value.IsNullOrZero());
        }

        public bool IsEqualZero()
        {
            return Terms.All(v => v.Value.IsNullOrEqualZero());
        }

        public IGaSymMultivectorTemp AddFactor(int id, Expr coef)
        {
            if (_termsDictionary.TryGetValue(id, out var oldCoef))
            {
                if (ReferenceEquals(oldCoef, null))
                    _termsDictionary[id] = new List<Expr>(1) { coef };
                else
                    oldCoef.Add(coef);
            }
            else
                _termsDictionary.Add(id, new List<Expr>(1) { coef });

            return this;
        }

        public IGaSymMultivectorTemp AddFactor(int id, bool isNegative, Expr coef)
        {
            coef = isNegative ? Mfs.Minus[coef] : coef;

            if (_termsDictionary.TryGetValue(id, out var oldCoef))
            {
                if (ReferenceEquals(oldCoef, null))
                    _termsDictionary[id] = new List<Expr>(1) { coef };
                else
                    oldCoef.Add(coef);
            }
            else
                _termsDictionary.Add(id, new List<Expr>(1) { coef });

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(int id, Expr coef)
        {
            if (_termsDictionary.TryGetValue(id, out var oldCoef))
            {
                if (ReferenceEquals(oldCoef, null))
                    _termsDictionary[id] = new List<Expr>(1) { coef };
                else
                {
                    oldCoef.Clear();
                    oldCoef.Add(coef);
                }
            }
            else
                _termsDictionary.Add(id, new List<Expr>(1) { coef });

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(int id, bool isNegative, Expr coef)
        {
            coef = isNegative ? Mfs.Minus[coef] : coef;

            if (_termsDictionary.TryGetValue(id, out var oldCoef))
            {
                if (ReferenceEquals(oldCoef, null))
                    _termsDictionary[id] = new List<Expr>(1) { coef };
                else
                {
                    oldCoef.Clear();
                    oldCoef.Add(coef);
                }
            }
            else
                _termsDictionary.Add(id, new List<Expr>(1) { coef });

            return this;
        }

        public void Simplify()
        {
            var nonZeroTerms = NonZeroExprTerms.ToArray();
                
            _termsDictionary.Clear();

            foreach (var pair in nonZeroTerms)
                _termsDictionary.Add(
                    pair.Key, 
                    new List<Expr>(1) {pair.Value}
                );
        }

        public MathematicaScalar[] TermsToArray()
        {
            var termsArray = new MathematicaScalar[GaSpaceDimension];

            foreach (var term in NonZeroTerms)
                termsArray[term.Key] = term.Value;

            return termsArray;
        }

        public Expr[] TermsToExprArray()
        {
            var termsArray = new Expr[GaSpaceDimension];

            foreach (var term in NonZeroExprTerms)
                termsArray[term.Key] = term.Value;

            return termsArray;
        }

        public GaSymMultivector ToMultivector()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            foreach (var term in NonZeroExprTerms)
                mv.SetTermCoef(term.Key, term.Value);

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
