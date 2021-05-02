using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorTerm : IGaSymMultivector
    {
        public static GaSymMultivectorTerm CreateTerm(int vSpaceDim, ulong id, Expr coef)
        {
            return new GaSymMultivectorTerm(vSpaceDim, id, coef);
        }

        public static GaSymMultivectorTerm CreateScalar(int vSpaceDim, Expr coef)
        {
            return new GaSymMultivectorTerm(vSpaceDim, 0, coef);
        }

        public static GaSymMultivectorTerm CreateZero(int vSpaceDim)
        {
            return new GaSymMultivectorTerm(vSpaceDim, 0, Expr.INT_ZERO);
        }

        public static GaSymMultivectorTerm CreateBasisBlade(int vSpaceDim, ulong id)
        {
            return new GaSymMultivectorTerm(vSpaceDim, id);
        }


        public ulong TermId { get; }

        public Expr TermCoef { get; set; }

        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public Expr this[ulong id]
            => (id == TermId) ? TermCoef : Expr.INT_ZERO;

        public Expr this[int grade, ulong index]
        {
            get
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                return (id == TermId) ? TermCoef : Expr.INT_ZERO;
            }
        }

        public IEnumerable<ulong> BasisBladeIds
        {
            get { yield return TermId; }
        }

        public IEnumerable<ulong> NonZeroBasisBladeIds
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

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> Terms
        {
            get
            {
                yield return new KeyValuePair<ulong, MathematicaScalar>(
                    TermId, 
                    TermCoef.ToMathematicaScalar()
                );
            }
        }

        public IEnumerable<KeyValuePair<ulong, Expr>> ExprTerms
        {
            get
            {
                yield return new KeyValuePair<ulong, Expr>(TermId, TermCoef ?? Expr.INT_ZERO);
            }
        }


        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> NonZeroTerms
        {
            get
            {
                if (!TermCoef.IsNullOrZero())
                    yield return new KeyValuePair<ulong, MathematicaScalar>(
                        TermId, 
                        TermCoef.ToMathematicaScalar()
                    );
            }
        }

        public IEnumerable<KeyValuePair<ulong, Expr>> NonZeroExprTerms
        {
            get
            {
                if (!TermCoef.IsNullOrZero())
                    yield return new KeyValuePair<ulong, Expr>(TermId, TermCoef);
            }
        }

        public bool IsTemp => false;

        public ulong TermsCount => 1;


        private GaSymMultivectorTerm(int vSpaceDim, ulong id)
        {
            VSpaceDimension = vSpaceDim;
            TermId = id;
            TermCoef = Expr.INT_ONE;
        }

        private GaSymMultivectorTerm(int vSpaceDim, ulong id, Expr coef)
        {
            VSpaceDimension = vSpaceDim;
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

        public bool ContainsBasisBlade(ulong id)
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
                ? GaSymMultivector.CreateZero(VSpaceDimension)
                : GaSymMultivector.CreateTerm(VSpaceDimension, TermId, TermCoef);
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(VSpaceDimension);

            if (TermId.BasisBladeGrade() == 1)
                mv.SetTermCoef(TermId, TermCoef);

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
