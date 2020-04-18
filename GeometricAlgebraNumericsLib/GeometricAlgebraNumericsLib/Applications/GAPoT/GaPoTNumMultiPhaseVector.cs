using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CodeComposerLib.Irony;
using CodeComposerLib.LaTeX;
using Irony.Parsing;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumMultiPhaseVector
    {
        private static readonly string _phaseVectorNames 
            = "abcdefghijklmnopqrstuvwxyz";

        private static string GetLatexBasisName(int id)
        {
            return GaPoTNumUtils.GetLaTeXBasisName(_phaseVectorNames[id].ToString());
        }


        public static GaPoTNumMultiPhaseVector operator -(GaPoTNumMultiPhaseVector v)
        {
            var result = new GaPoTNumMultiPhaseVector();

            foreach (var pair in v._phaseVectorsDictionary)
                result.SetPhaseVector(pair.Key, -pair.Value);

            return result;
        }

        public static GaPoTNumMultiPhaseVector operator +(GaPoTNumMultiPhaseVector v1, GaPoTNumMultiPhaseVector v2)
        {
            var result = new GaPoTNumMultiPhaseVector();

            foreach (var pair in v1._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, pair.Value);

            foreach (var pair in v2._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, pair.Value);

            return result;
        }

        public static GaPoTNumMultiPhaseVector operator -(GaPoTNumMultiPhaseVector v1, GaPoTNumMultiPhaseVector v2)
        {
            var result = new GaPoTNumMultiPhaseVector();

            foreach (var pair in v1._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, pair.Value);

            foreach (var pair in v2._phaseVectorsDictionary)
                result.AddPhaseVector(pair.Key, -pair.Value);

            return result;
        }

        public static GaPoTNumMultiPhaseBivector operator *(GaPoTNumMultiPhaseVector v1, GaPoTNumMultiPhaseVector v2)
        {
            var mpBivector = new GaPoTNumMultiPhaseBivector();
            
            if (v1.IsLinearlyDependentOn(v2, out var innerProduct))
            {
                var spBivector = new GaPoTNumSinglePhaseBivector();
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


        private readonly Dictionary<int, GaPoTNumSinglePhaseVector> _phaseVectorsDictionary
            = new Dictionary<int, GaPoTNumSinglePhaseVector>();

        public int Count 
            => _phaseVectorsDictionary.Count;


        public bool IsZero()
        {
            return Norm2().IsNearZero();
        }

        public bool IsLinearlyDependentOn(GaPoTNumMultiPhaseVector v2, out double innerProduct)
        {
            innerProduct = 0;

            var termsList1 = GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.PhaseId)
                .ThenBy(t => t.TermId)
                .ToArray();

            var termsList2 = v2.GetTerms()
                .Where(t => !t.Value.IsNearZero())
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
                    var crossValue =
                        termsList1[i1].Value * termsList2[i2].Value - 
                        termsList1[i2].Value * termsList2[i1].Value;

                    if (!crossValue.IsNearZero())
                        return false;
                }

                innerProduct += termsList1[i1].Value * termsList2[i1].Value;
            }

            return true;
        }


        public GaPoTNumSinglePhaseVector GetPhaseVector(int id)
        {
            return _phaseVectorsDictionary.TryGetValue(id, out var vector)
                ? vector
                : new GaPoTNumSinglePhaseVector();
        }

        public GaPoTNumMultiPhaseVector AddPhaseVector(int id, GaPoTNumSinglePhaseVector vector)
        {
            if (_phaseVectorsDictionary.TryGetValue(id, out var oldVector))
                _phaseVectorsDictionary[id] = oldVector + vector;
            else
                _phaseVectorsDictionary.Add(id, vector);

            return this;
        }

        public GaPoTNumMultiPhaseVector SetPhaseVector(int id, GaPoTNumSinglePhaseVector vector)
        {
            if (_phaseVectorsDictionary.ContainsKey(id))
                _phaseVectorsDictionary[id] = vector;
            else
                _phaseVectorsDictionary.Add(id, vector);

            return this;
        }


        public Dictionary<int, GaPoTNumSinglePhaseVector> GetPhaseVectors()
        {
            return _phaseVectorsDictionary
                .Where(p => p.Value.Count > 0)
                .ToDictionary(
                    p => p.Key, 
                    p => p.Value
                );
        }

        public IEnumerable<GaPoTNumMultiPhaseVectorTerm> GetTerms()
        {
            return _phaseVectorsDictionary
                .SelectMany(p =>
                    p.Value.GetTerms().Select(t => 
                        new GaPoTNumMultiPhaseVectorTerm(p.Key, t.TermId, t.Value)
                    )
                );
        }

        public Dictionary<int, GaPoTNumSinglePhaseVectorTerm[]> GetGroupedTerms()
        {
            var result = new Dictionary<int, GaPoTNumSinglePhaseVectorTerm[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var termsArray = 
                    pair.Value.GetTerms().ToArray();

                if (termsArray.Length > 0)
                    result.Add(pair.Key, termsArray);
            }

            return result;
        }

        public Dictionary<int, GaPoTNumPolarPhasor[]> GetPolarPhasors()
        {
            var result = new Dictionary<int, GaPoTNumPolarPhasor[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var termsArray = 
                    pair.Value.GeTPolarPhasors().ToArray();

                if (termsArray.Length > 0)
                    result.Add(pair.Key, termsArray);
            }

            return result;
        }

        public Dictionary<int, GaPoTNumRectPhasor[]> GetRectPhasors()
        {
            var result = new Dictionary<int, GaPoTNumRectPhasor[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var termsArray = 
                    pair.Value.GeTRectPhasors().ToArray();

                if (termsArray.Length > 0)
                    result.Add(pair.Key, termsArray);
            }

            return result;
        }


        public GaPoTNumMultiPhaseBivector Gp(GaPoTNumMultiPhaseVector v)
        {
            return this * v;
        }

        public GaPoTNumMultiPhaseVector Add(GaPoTNumMultiPhaseVector v)
        {
            return this + v;
        }

        public GaPoTNumMultiPhaseVector Subtract(GaPoTNumMultiPhaseVector v)
        {
            return this - v;
        }

        public GaPoTNumMultiPhaseVector Negative()
        {
            return -this;
        }

        public GaPoTNumMultiPhaseVector Reverse()
        {
            return this;
        }

        public double Norm2()
        {
            return _phaseVectorsDictionary
                .Values
                .Select(v => v.Norm2())
                .Sum();
        }

        public GaPoTNumMultiPhaseVector Inverse()
        {
            var norm2 = Norm2();

            var result = new GaPoTNumMultiPhaseVector();

            if (norm2 == 0)
                throw new DivideByZeroException();

            var value = 1.0d / norm2;

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
                    $"[{p.Value.TermsToText()}] <{phaseVectorNames[p.Key]}>"
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
                    $"[{p.Value.PolarPhasorsToText()}] <{phaseVectorNames[p.Key]}>"
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
                    $"[{p.Value.RectPhasorsToText()}] <{phaseVectorNames[p.Key]}>"
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