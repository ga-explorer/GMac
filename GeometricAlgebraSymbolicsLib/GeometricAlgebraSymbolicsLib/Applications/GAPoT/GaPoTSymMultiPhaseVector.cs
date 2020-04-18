using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.LaTeX;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymMultiPhaseVector
    {
        private static readonly string _phaseVectorNames 
            = "abcdefghijklmnopqrstuvwxyz";

        private static string GetLatexBasisName(int id)
        {
            return _phaseVectorNames[id].ToString().GetLaTeXBasisName();
        }


        public static GaPoTSymMultiPhaseVector operator -(GaPoTSymMultiPhaseVector v)
        {
            var result = new GaPoTSymMultiPhaseVector();

            foreach (var pair in v._phaseVectorsDictionary)
                result.SetPhaseVector(
                    pair.Key, 
                    -pair.Value
                );

            return result;
        }

        public static GaPoTSymMultiPhaseVector operator +(GaPoTSymMultiPhaseVector v1, GaPoTSymMultiPhaseVector v2)
        {
            var result = new GaPoTSymMultiPhaseVector();

            foreach (var pair in v1._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, pair.Value);

            foreach (var pair in v2._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, pair.Value);

            return result;
        }

        public static GaPoTSymMultiPhaseVector operator -(GaPoTSymMultiPhaseVector v1, GaPoTSymMultiPhaseVector v2)
        {
            var result = new GaPoTSymMultiPhaseVector();

            foreach (var pair in v1._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, pair.Value);

            foreach (var pair in v2._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, -pair.Value);

            return result;
        }

        public static GaPoTSymMultiPhaseBivector operator *(GaPoTSymMultiPhaseVector v1, GaPoTSymMultiPhaseVector v2)
        {
            var mpBivector = new GaPoTSymMultiPhaseBivector();

            if (v1.IsLinearlyDependentOn(v2, out var innerProduct))
            {
                var spBivector = new GaPoTSymSinglePhaseBivector();
                spBivector.AddTerm(0, 0, innerProduct);

                mpBivector.AddPhaseBivector(0, 0, spBivector);

                return mpBivector;
            }

            foreach (var spVector1 in v1.GetPhaseVectors())
            {
                foreach (var spVector2 in v2.GetPhaseVectors())
                {
                    var spVectorsProduct = spVector1.Value * spVector2.Value;

                    mpBivector.AddPhaseBivector(
                        spVector1.Key, 
                        spVector2.Key, 
                        spVectorsProduct
                    );
                }
            }

            return mpBivector;
        }


        private readonly Dictionary<int, GaPoTSymSinglePhaseVector> _phaseVectorsDictionary
            = new Dictionary<int, GaPoTSymSinglePhaseVector>();

        public int Count 
            => _phaseVectorsDictionary.Count;


        public bool IsZero()
        {
            return Norm2().IsZero();
        }

        public bool IsLinearlyDependentOn(GaPoTSymMultiPhaseVector v2, out Expr innerProduct)
        {
            innerProduct = Expr.INT_ZERO;

            var termsList1 = GetTerms()
                .Where(t => !t.Value.IsZero())
                .OrderBy(t => t.PhaseId)
                .ThenBy(t => t.TermId)
                .ToArray();

            var termsList2 = v2.GetTerms()
                .Where(t => !t.Value.IsZero())
                .OrderBy(t => t.PhaseId)
                .ThenBy(t => t.TermId)
                .ToArray();

            if (termsList1.Length != termsList2.Length)
                return false;

            if (termsList1.Length == 0)
                return false;

            for (var i = 0; i < termsList1.Length; i++)
            {
                var term1 = termsList1[i];
                var term2 = termsList2[i];

                if (!term1.HasSameId(term2))
                    return false;
            }

            for (var i1 = 0; i1 < termsList1.Length; i1++)
            {
                for (var i2 = i1 + 1; i2 < termsList2.Length; i2++)
                {
                    var crossValue = Mfs.Subtract[
                        Mfs.Times[termsList1[i1].Value, termsList2[i2].Value], 
                        Mfs.Times[termsList1[i2].Value, termsList2[i1].Value]
                    ].GaPoTSymSimplify();

                    if (!crossValue.IsZero())
                        return false;
                }
            }

            var innerProductTerms = new Expr[termsList1.Length];
            for (var i = 0; i < termsList1.Length; i++)
                innerProductTerms[i] = Mfs.Times[termsList1[i].Value, termsList2[i].Value];

            innerProduct = Mfs.SumExpr(innerProductTerms).GaPoTSymSimplify();

            return true;
        }


        public GaPoTSymSinglePhaseVector GetPhaseVector(int id)
        {
            return _phaseVectorsDictionary.TryGetValue(id, out var vector)
                ? vector
                : new GaPoTSymSinglePhaseVector();
        }

        public GaPoTSymMultiPhaseVector AddPhaseVector(int id, GaPoTSymSinglePhaseVector vector)
        {
            if (_phaseVectorsDictionary.TryGetValue(id, out var oldVector))
                _phaseVectorsDictionary[id] = oldVector + vector;
            else
                _phaseVectorsDictionary.Add(id, vector);

            return this;
        }

        public GaPoTSymMultiPhaseVector SetPhaseVector(int id, GaPoTSymSinglePhaseVector vector)
        {
            if (_phaseVectorsDictionary.ContainsKey(id))
                _phaseVectorsDictionary[id] = vector;
            else
                _phaseVectorsDictionary.Add(id, vector);

            return this;
        }


        public Dictionary<int, GaPoTSymSinglePhaseVector> GetPhaseVectors()
        {
            return _phaseVectorsDictionary
                .Where(p => p.Value.Count > 0)
                .ToDictionary(
                    p => p.Key, 
                    p => p.Value
                );
        }

        public IEnumerable<GaPoTSymMultiPhaseVectorTerm> GetTerms()
        {
            return _phaseVectorsDictionary
                .SelectMany(p =>
                    p.Value.GetTerms().Select(t => 
                        new GaPoTSymMultiPhaseVectorTerm(p.Key, t.TermId, t.Value)
                    )
                );
        }

        public Dictionary<int, GaPoTSymSinglePhaseVectorTerm[]> GetGroupedTerms()
        {
            var result = new Dictionary<int, GaPoTSymSinglePhaseVectorTerm[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var termsArray = 
                    pair.Value.GetTerms().ToArray();

                if (termsArray.Length > 0)
                    result.Add(pair.Key, termsArray);
            }

            return result;
        }

        public Dictionary<int, GaPoTSymPolarPhasor[]> GetPolarPhasors()
        {
            var result = new Dictionary<int, GaPoTSymPolarPhasor[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var termsArray = 
                    pair.Value.GeTPolarPhasors().ToArray();

                if (termsArray.Length > 0)
                    result.Add(pair.Key, termsArray);
            }

            return result;
        }

        public Dictionary<int, GaPoTSymRectPhasor[]> GetRectPhasors()
        {
            var result = new Dictionary<int, GaPoTSymRectPhasor[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var termsArray = 
                    pair.Value.GeTRectPhasors().ToArray();

                if (termsArray.Length > 0)
                    result.Add(pair.Key, termsArray);
            }

            return result;
        }


        public GaPoTSymMultiPhaseBivector Gp(GaPoTSymMultiPhaseVector v)
        {
            return this * v;
        }

        public GaPoTSymMultiPhaseVector Add(GaPoTSymMultiPhaseVector v)
        {
            return this + v;
        }

        public GaPoTSymMultiPhaseVector Subtract(GaPoTSymMultiPhaseVector v)
        {
            return this - v;
        }

        public GaPoTSymMultiPhaseVector Negative()
        {
            return -this;
        }

        public GaPoTSymMultiPhaseVector Reverse()
        {
            return this;
        }

        public Expr Norm2()
        {
            return Mfs.SumExpr(
                _phaseVectorsDictionary
                .Values
                .Select(v => v.Norm2())
                .ToArray()
            );
        }

        public GaPoTSymMultiPhaseVector Inverse()
        {
            var norm2 = Norm2();

            var result = new GaPoTSymMultiPhaseVector();

            if (norm2.IsZero())
                throw new DivideByZeroException();

            var value = Mfs.Divide[1, norm2];

            foreach (var pair in _phaseVectorsDictionary)
                result.SetPhaseVector(
                    pair.Key,
                    pair.Value * value
                );

            return result;
        }


        public string ToText()
        {
            return PolarPhasorsToText();
        }

        public string TermsToText()
        {
            var phaseVectorsDictionary = GetPhaseVectors();

            if (phaseVectorsDictionary.Count == 0)
                return "0";

            const string phaseVectorNames = "abcdefghijklmnopqrstuvwxyz";

            return phaseVectorsDictionary
                .Select(p => 
                    $"({p.Value.TermsToText()})<{phaseVectorNames[p.Key]}>"
                )
                .Concatenate("; ");
        }

        public string PolarPhasorsToText()
        {
            var phaseVectorsDictionary = GetPhaseVectors();

            if (phaseVectorsDictionary.Count == 0)
                return "0";

            const string phaseVectorNames = "abcdefghijklmnopqrstuvwxyz";

            return phaseVectorsDictionary
                .Select(p => 
                    $"({p.Value.PolarPhasorsToText()})<{phaseVectorNames[p.Key]}>"
                )
                .Concatenate("; ");
        }

        public string RectPhasorsToText()
        {
            var phaseVectorsDictionary = GetPhaseVectors();

            if (phaseVectorsDictionary.Count == 0)
                return "0";

            const string phaseVectorNames = "abcdefghijklmnopqrstuvwxyz";

            return phaseVectorsDictionary
                .Select(p => 
                    $"({p.Value.RectPhasorsToText()})<{phaseVectorNames[p.Key]}>"
                )
                .Concatenate("; ");
        }


        public string ToLaTeX()
        {
            return PolarPhasorsToLaTeX();
        }

        public string TermsToLaTeX()
        {
            var phaseVectorsDictionary = GetPhaseVectors();

            if (phaseVectorsDictionary.Count == 0)
                return "0";

            return phaseVectorsDictionary
                .Select(p => 
                    $@"{p.Value.TermsToLaTeX().LaTeXMathAddSquareBrackets()} {GetLatexBasisName(p.Key)}"
                )
                .Concatenate(" + ");
        }

        public string PolarPhasorsToLaTeX()
        {
            var phaseVectorsDictionary = GetPhaseVectors();

            if (phaseVectorsDictionary.Count == 0)
                return "0";

            return phaseVectorsDictionary
                .Select(p => 
                    $@"\left[ {p.Value.PolarPhasorsToLaTeX()} \right] {GetLatexBasisName(p.Key)}"
                )
                .Concatenate(" + ");
        }

        public string RectPhasorsToLaTeX()
        {
            var phaseVectorsDictionary = GetPhaseVectors();

            if (phaseVectorsDictionary.Count == 0)
                return "0";

            return phaseVectorsDictionary
                .Select(p => 
                    $@"\left[ {p.Value.RectPhasorsToLaTeX()} \right] {GetLatexBasisName(p.Key)}"
                )
                .Concatenate(" + ");
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}