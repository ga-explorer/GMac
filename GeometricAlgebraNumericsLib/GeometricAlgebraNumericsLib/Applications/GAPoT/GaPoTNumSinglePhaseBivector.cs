using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Dictionary;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumSinglePhaseBivector
    {
        public static GaPoTNumSinglePhaseBivector operator -(GaPoTNumSinglePhaseBivector v)
        {
            return new GaPoTNumSinglePhaseBivector(
                v._termsDictionary.Values.Select(t => -t)
            );
        }

        public static GaPoTNumSinglePhaseBivector operator +(GaPoTNumSinglePhaseBivector v1, GaPoTNumSinglePhaseBivector v2)
        {
            var biVector = new GaPoTNumSinglePhaseBivector();

            biVector.AddTerms(v1._termsDictionary.Values);
            biVector.AddTerms(v2._termsDictionary.Values);

            return biVector;
        }

        public static GaPoTNumSinglePhaseBivector operator -(GaPoTNumSinglePhaseBivector v1, GaPoTNumSinglePhaseBivector v2)
        {
            var biVector = new GaPoTNumSinglePhaseBivector();

            biVector.AddTerms(v1._termsDictionary.Values);
            biVector.AddTerms(v2._termsDictionary.Values.Select(t => -t));

            return biVector;
        }


        private readonly Dictionary2Keys<int, GaPoTNumSinglePhaseBivectorTerm> _termsDictionary
            = new Dictionary2Keys<int, GaPoTNumSinglePhaseBivectorTerm>();


        public GaPoTNumSinglePhaseBivector()
        {
        }

        public GaPoTNumSinglePhaseBivector(IEnumerable<GaPoTNumSinglePhaseBivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GaPoTNumSinglePhaseBivector AddTerm(GaPoTNumSinglePhaseBivectorTerm term)
        {
            if (_termsDictionary.TryGetValue(term.TermId1, term.TermId2, out var oldTerm))
                _termsDictionary[term.TermId1, term.TermId2] = new GaPoTNumSinglePhaseBivectorTerm(
                    term.TermId1,
                    term.TermId2,
                    oldTerm.Value + term.Value
                );
            else
                _termsDictionary.Add(term.TermId1, term.TermId2, term);

            return this;
        }

        public GaPoTNumSinglePhaseBivector AddTerm(int id1, int id2, double value)
        {
            return AddTerm(
                new GaPoTNumSinglePhaseBivectorTerm(id1, id2, value)
            );
        }

        public GaPoTNumSinglePhaseBivector AddTerms(IEnumerable<GaPoTNumSinglePhaseBivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public IEnumerable<GaPoTNumSinglePhaseBivectorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsNearZero());
        }


        public GaPoTNumSinglePhaseBivector Reverse()
        {
            var result = new GaPoTNumSinglePhaseBivector();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(pair.Value);
                else
                    result.AddTerm(-pair.Value);
            }

            return result;
        }

        public GaPoTNumSinglePhaseBivector NegativeReverse()
        {
            var result = new GaPoTNumSinglePhaseBivector();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(-pair.Value);
                else
                    result.AddTerm(pair.Value);
            }

            return result;
        }

        public double Norm2()
        {
            return _termsDictionary
                .Values
                .Select(t => t.Norm2())
                .Sum();
        }

        public GaPoTNumSinglePhaseBivector Inverse()
        {
            var norm2 = Norm2();

            if (norm2 == 0)
                throw new DivideByZeroException();

            var value = 1.0d / norm2;

            return new GaPoTNumSinglePhaseBivector(
                _termsDictionary
                    .Values
                    .Select(t => t.ScaledReverse(value))
            );
        }
        

        public string TermsToText()
        {
            var termsArray = 
                GetTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ");
        }
        
        public string TermsToLaTeX()
        {
            var termsArray = 
                GetTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }
 

        public override string ToString()
        {
            return TermsToText();
        }
    }
}