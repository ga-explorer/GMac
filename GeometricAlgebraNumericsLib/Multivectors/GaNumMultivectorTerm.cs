using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public sealed class GaNumMultivectorTerm : IGaNumMultivector
    {
        public static GaNumMultivectorTerm CreateTerm(int gaSpaceDim, int id, double coef)
        {
            return new GaNumMultivectorTerm(gaSpaceDim, id, coef);
        }

        public static GaNumMultivectorTerm CreateScalar(int gaSpaceDim, double coef)
        {
            return new GaNumMultivectorTerm(gaSpaceDim, 0, coef);
        }

        public static GaNumMultivectorTerm CreateZero(int gaSpaceDim)
        {
            return new GaNumMultivectorTerm(gaSpaceDim, 0, 0);
        }

        public static GaNumMultivectorTerm CreateBasisBlade(int gaSpaceDim, int id)
        {
            return new GaNumMultivectorTerm(gaSpaceDim, id);
        }


        public int TermId { get; }

        public double TermCoef { get; set; }


        public int VSpaceDimension 
            => GaSpaceDimension.ToVSpaceDimension();

        public int GaSpaceDimension { get; }

        public double this[int id] 
            => (id == TermId) ? TermCoef : 0.0d;

        public double this[int grade, int index]
        {
            get
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                return (id == TermId) ? TermCoef : 0.0d;
            }
        }

        public IEnumerable<int> BasisBladeIds
        {
            get { yield return TermId; }
        }

        public IEnumerable<int> NonZeroBasisBladeIds
        {
            get { if (!TermCoef.IsNearZero()) yield return TermId; }
        }

        public IEnumerable<double> BasisBladeScalars
        {
            get { yield return TermCoef; }
        }

        public IEnumerable<double> NonZeroBasisBladeScalars
        {
            get { if (!TermCoef.IsNearZero()) yield return TermCoef; }
        }

        public IEnumerable<KeyValuePair<int, double>> Terms
        {
            get
            {
                yield return new KeyValuePair<int, double>(TermId, TermCoef);
            }
        }

        public IEnumerable<KeyValuePair<int, double>> NonZeroTerms
        {
            get
            {
                if (!TermCoef.IsNearZero())
                    yield return new KeyValuePair<int, double>(TermId, TermCoef);
            }
        }

        public bool IsTemp => false;

        public int TermsCount => 1;


        private GaNumMultivectorTerm(int gaSpaceDim, int id)
        {
            GaSpaceDimension = gaSpaceDim;
            TermId = id;
            TermCoef = 1.0d;
        }

        private GaNumMultivectorTerm(int gaSpaceDim, int id, double coef)
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
            return TermId == 0 || TermCoef.IsNearZero();
        }

        public bool IsZero()
        {
            return TermCoef.IsNearZero();
        }

        public bool IsNearZero(double epsilon)
        {
            return TermCoef.IsNearZero(epsilon);
        }

        public bool ContainsBasisBlade(int id)
        {
            return id == TermId;
        }

        public void Simplify()
        {
            if (TermCoef.IsNearZero()) TermCoef = 0.0d;
        }

        public double[] TermsToArray()
        {
            var termsArray = new double[GaSpaceDimension];

            termsArray[TermId] = TermCoef;

            return termsArray;
        }

        public GaNumMultivector ToMultivector()
        {
            return TermCoef.IsNearZero()
                ? GaNumMultivector.CreateZero(GaSpaceDimension)
                : GaNumMultivector.CreateTerm(GaSpaceDimension, TermId, TermCoef);
        }

        public GaNumMultivector GetVectorPart()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            if (!TermCoef.IsNearZero())
                mv.SetTermCoef(TermId, TermCoef);

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
