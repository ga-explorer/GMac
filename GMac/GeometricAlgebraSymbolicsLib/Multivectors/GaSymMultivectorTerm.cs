using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorTerm : IGaSymMultivector
    {
        public static GaSymMultivectorTerm CreateTerm(int gaSpaceDim, int id, Expr coef)
        {
            return new GaSymMultivectorTerm(gaSpaceDim, id, coef);
        }

        public static GaSymMultivectorTerm CreateScalar(int gaSpaceDim, Expr coef)
        {
            return new GaSymMultivectorTerm(gaSpaceDim, 0, coef);
        }

        public static GaSymMultivectorTerm CreateZero(int gaSpaceDim)
        {
            return new GaSymMultivectorTerm(gaSpaceDim, 0, Expr.INT_ZERO);
        }

        public static GaSymMultivectorTerm CreateBasisBlade(int gaSpaceDim, int id)
        {
            return new GaSymMultivectorTerm(gaSpaceDim, id);
        }


        public int TermId { get; }

        public Expr TermCoef { get; set; }

        public int VSpaceDimension
            => GaSpaceDimension.ToVSpaceDimension();

        public int GaSpaceDimension { get; }

        public Expr this[int id]
            => (id == TermId) ? TermCoef : Expr.INT_ZERO;

        public Expr this[int grade, int index]
        {
            get
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                return (id == TermId) ? TermCoef : Expr.INT_ZERO;
            }
        }

        public IEnumerable<int> BasisBladeIds
        {
            get { yield return TermId; }
        }

        public IEnumerable<int> NonZeroBasisBladeIds
        {
            get { if (!TermCoef.IsZero()) yield return TermId; }
        }

        public IEnumerable<Expr> BasisBladeExprScalars
        {
            get { yield return TermCoef ?? Expr.INT_ZERO; }
        }


        public IEnumerable<Expr> NonZeroBasisBladeExprScalars
        {
            get
            {
                if (!TermCoef.IsNullOrZero())
                    yield return TermCoef;
            }
        }

        public IEnumerable<MathematicaScalar> BasisBladeScalars
        {
            get { yield return TermCoef.ToMathematicaScalar(); }
        }

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars
        {
            get
            {
                if (!TermCoef.IsNullOrZero())
                    yield return TermCoef.ToMathematicaScalar();
            }
        }

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> Terms
        {
            get
            {
                yield return new KeyValuePair<int, MathematicaScalar>(
                    TermId, 
                    TermCoef.ToMathematicaScalar()
                );
            }
        }

        public IEnumerable<KeyValuePair<int, Expr>> ExprTerms
        {
            get
            {
                yield return new KeyValuePair<int, Expr>(TermId, TermCoef ?? Expr.INT_ZERO);
            }
        }


        public IEnumerable<KeyValuePair<int, MathematicaScalar>> NonZeroTerms
        {
            get
            {
                if (!TermCoef.IsNullOrZero())
                    yield return new KeyValuePair<int, MathematicaScalar>(
                        TermId, 
                        TermCoef.ToMathematicaScalar()
                    );
            }
        }

        public IEnumerable<KeyValuePair<int, Expr>> NonZeroExprTerms
        {
            get
            {
                if (!TermCoef.IsNullOrZero())
                    yield return new KeyValuePair<int, Expr>(TermId, TermCoef);
            }
        }

        public bool IsTemp => false;

        public int TermsCount => 1;


        private GaSymMultivectorTerm(int gaSpaceDim, int id)
        {
            GaSpaceDimension = gaSpaceDim;
            TermId = id;
            TermCoef = Expr.INT_ONE;
        }

        private GaSymMultivectorTerm(int gaSpaceDim, int id, Expr coef)
        {
            GaSpaceDimension = gaSpaceDim;
            TermId = id;
            TermCoef = coef;
        }


        public bool IsTerm()
        {
            return true;
        }

        public bool IsScalar()
        {
            return TermId == 0 || TermCoef.IsZero();
        }

        public bool IsZero()
        {
            return TermCoef.IsZero();
        }

        public bool IsEqualZero()
        {
            return TermCoef.IsEqualZero(GaSymbolicsUtils.Cas);
        }

        public bool ContainsBasisBlade(int id)
        {
            return id == TermId;
        }

        public void Simplify()
        {
            if (TermCoef.IsZero()) TermCoef = Expr.INT_ZERO;
        }

        MathematicaScalar[] IGaSymMultivector.TermsToArray()
        {
            var termsArray = new MathematicaScalar[GaSpaceDimension];

            termsArray[TermId] = TermCoef.ToMathematicaScalar();

            return termsArray;
        }

        public Expr[] TermsToExprArray()
        {
            var termsArray = new Expr[GaSpaceDimension];

            termsArray[TermId] = TermCoef;

            return termsArray;
        }

        public GaSymMultivector ToMultivector()
        {
            return TermCoef.IsZero()
                ? GaSymMultivector.CreateZero(GaSpaceDimension)
                : GaSymMultivector.CreateTerm(GaSpaceDimension, TermId, TermCoef);
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            if (TermId.BasisBladeGrade() == 1)
                mv.SetTermCoef(TermId, TermCoef);

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
