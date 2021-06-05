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
        public static GaSymMultivectorTempArray Create(int vSpaceDim)
        {
            return new GaSymMultivectorTempArray(vSpaceDim);
        }


        private readonly List<Expr>[] _coefsArray;


        public Expr this[ulong id]
            => _coefsArray[id].Simplify();

        public Expr this[int grade, ulong index]
            => _coefsArray[GaFrameUtils.BasisBladeId(grade, index)].Simplify();

        public IEnumerable<ulong> BasisBladeIds
        {
            get
            {
                var id = 0UL;
                foreach (var coef in _coefsArray)
                { 
                    if (coef != null)
                        yield return id;

                    id++;
                }
            }
        }

        public IEnumerable<ulong> NonZeroBasisBladeIds
        {
            get
            {
                var id = 0UL;
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

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> Terms
        {
            get
            {
                var id = 0UL;
                foreach (var coef in _coefsArray)
                {
                    if (coef != null)
                        yield return new KeyValuePair<ulong, MathematicaScalar>(
                            id, 
                            coef.Simplify().ToMathematicaScalar()
                        );

                    id++;
                }
            }
        }


        public IEnumerable<KeyValuePair<ulong, Expr>> ExprTerms
        {
            get
            {
                var id = 0UL;
                foreach (var coef in _coefsArray)
                {
                    if (coef != null)
                        yield return new KeyValuePair<ulong, Expr>(
                            id, 
                            coef.Simplify()
                        );

                    id++;
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> NonZeroTerms
        {
            get
            {
                var id = 0UL;
                foreach (var coef in _coefsArray.Select(c => c.Simplify()))
                {
                    if (!coef.IsZero())
                        yield return new KeyValuePair<ulong, MathematicaScalar>(
                            id, 
                            coef.ToMathematicaScalar()
                        );

                    id++;
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, Expr>> NonZeroExprTerms
        {
            get
            {
                var id = 0UL;
                foreach (var coef in _coefsArray.Select(c => c.Simplify()))
                {
                    if (!coef.IsZero())
                        yield return new KeyValuePair<ulong, Expr>(id, coef);

                    id++;
                }
            }
        }


        public bool ContainsBasisBlade(ulong id)
        {
            return _coefsArray[id] != null;
        }

        public bool IsTemp 
            => true;

        public ulong TermsCount 
            => (ulong)_coefsArray.Count(c => c != null);

        public int VSpaceDimension
            => _coefsArray.Length.ToVSpaceDimension();

        public ulong GaSpaceDimension
            => (ulong)_coefsArray.Length;


        private GaSymMultivectorTempArray(int vSpaceDim)
        {
            _coefsArray = new List<Expr>[vSpaceDim.ToGaSpaceDimension()];
        }


        public IGaSymMultivectorTemp AddFactor(ulong id, Expr coef)
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

        public IGaSymMultivectorTemp AddFactor(ulong id, bool isNegative, Expr coef)
        {
            if (ReferenceEquals(_coefsArray[id], null))
                _coefsArray[id] = new List<Expr>();

            _coefsArray[id].Add(isNegative ? Mfs.Minus[coef] : coef);

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(ulong id, Expr coef)
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

        public IGaSymMultivectorTemp SetTermCoef(ulong id, bool isNegative, Expr coef)
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
            var mv = GaSymMultivector.CreateZero(VSpaceDimension);

            var id = 0UL;
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
