using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors.Intermediate
{
    public sealed class GaSymMultivectorTempHash : IGaSymMultivectorTemp
    {
        public static GaSymMultivectorTempHash Create(int vSpaceDim)
        {
            return new GaSymMultivectorTempHash(vSpaceDim);
        }


        private readonly Dictionary<ulong, List<Expr>> _termsDictionary;


        public Expr this[ulong id]
        {
            get
            {
                _termsDictionary.TryGetValue(id, out var coef);

                return ReferenceEquals(coef, null) 
                    ? Expr.INT_ZERO 
                    : coef.Simplify();
            }
        }

        public Expr this[int grade, ulong index]
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

        public IEnumerable<ulong> BasisBladeIds 
            => _termsDictionary.Keys;

        public IEnumerable<ulong> NonZeroBasisBladeIds
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

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> Terms
            => _termsDictionary
                .Select(p => new KeyValuePair<ulong, MathematicaScalar>(
                    p.Key,
                    p.Value.Simplify().ToMathematicaScalar()
                ));

        public IEnumerable<KeyValuePair<ulong, Expr>> ExprTerms
            => _termsDictionary
                .Select(p => new KeyValuePair<ulong, Expr>(
                    p.Key,
                    p.Value.Simplify()
                ));

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> NonZeroTerms
            => _termsDictionary
                .Select(p => new KeyValuePair<ulong, MathematicaScalar>(
                    p.Key, 
                    p.Value.Simplify().ToMathematicaScalar()
                ))
                .Where(p => !p.Value.Expression.IsNullOrZero());

        public IEnumerable<KeyValuePair<ulong, Expr>> NonZeroExprTerms 
            => _termsDictionary
            .Select(p => new KeyValuePair<ulong, Expr>(p.Key, p.Value.Simplify()))
            .Where(p => !p.Value.IsNullOrZero());

        public bool IsTemp 
            => true;

        public ulong TermsCount 
            => (ulong)_termsDictionary.Count;

        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();


        private GaSymMultivectorTempHash(int vSpaceDim)
        {
            VSpaceDimension = vSpaceDim;
            _termsDictionary = new Dictionary<ulong, List<Expr>>();
        }


        public bool ContainsBasisBlade(ulong id)
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

        public IGaSymMultivectorTemp AddFactor(ulong id, Expr coef)
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

        public IGaSymMultivectorTemp AddFactor(ulong id, bool isNegative, Expr coef)
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

        public IGaSymMultivectorTemp SetTermCoef(ulong id, Expr coef)
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

        public IGaSymMultivectorTemp SetTermCoef(ulong id, bool isNegative, Expr coef)
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
            var mv = GaSymMultivector.CreateZero(VSpaceDimension);

            foreach (var term in NonZeroExprTerms)
                mv.SetTermCoef(term.Key, term.Value);

            return mv;
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(VSpaceDimension);

            foreach (var id in GaFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public IEnumerator<KeyValuePair<ulong, Expr>> GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }
    }
}
