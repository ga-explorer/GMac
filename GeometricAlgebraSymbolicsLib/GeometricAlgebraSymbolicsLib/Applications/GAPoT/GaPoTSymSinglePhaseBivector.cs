using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Dictionary;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymSinglePhaseBivector
    {
        public static GaPoTSymSinglePhaseBivector operator -(GaPoTSymSinglePhaseBivector v)
        {
            return new GaPoTSymSinglePhaseBivector(
                v._termsDictionary.Values.Select(t => -t)
            );
        }

        public static GaPoTSymSinglePhaseBivector operator +(GaPoTSymSinglePhaseBivector v1, GaPoTSymSinglePhaseBivector v2)
        {
            var biVector = new GaPoTSymSinglePhaseBivector();

            biVector.AddTerms(v1._termsDictionary.Values);
            biVector.AddTerms(v2._termsDictionary.Values);

            return biVector;
        }

        public static GaPoTSymSinglePhaseBivector operator -(GaPoTSymSinglePhaseBivector v1, GaPoTSymSinglePhaseBivector v2)
        {
            var biVector = new GaPoTSymSinglePhaseBivector();

            biVector.AddTerms(v1._termsDictionary.Values);
            biVector.AddTerms(v2._termsDictionary.Values.Select(t => -t));

            return biVector;
        }


        private readonly Dictionary2Keys<int, GaPoTSymSinglePhaseBivectorTerm> _termsDictionary
            = new Dictionary2Keys<int, GaPoTSymSinglePhaseBivectorTerm>();


        public GaPoTSymSinglePhaseBivector()
        {
        }

        public GaPoTSymSinglePhaseBivector(IEnumerable<GaPoTSymSinglePhaseBivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GaPoTSymSinglePhaseBivector AddTerm(GaPoTSymSinglePhaseBivectorTerm term)
        {
            if (_termsDictionary.TryGetValue(term.TermId1, term.TermId2, out var oldTerm))
                _termsDictionary[term.TermId1, term.TermId2] = new GaPoTSymSinglePhaseBivectorTerm(
                    term.TermId1,
                    term.TermId2,
                    Mfs.Plus[oldTerm.Value, term.Value].GaPoTSymSimplify()
                );
            else
                _termsDictionary.Add(term.TermId1, term.TermId2, term);

            return this;
        }

        public GaPoTSymSinglePhaseBivector AddTerm(int id1, int id2, Expr value)
        {
            return AddTerm(
                new GaPoTSymSinglePhaseBivectorTerm(id1, id2, value)
            );
        }

        public GaPoTSymSinglePhaseBivector AddTerms(IEnumerable<GaPoTSymSinglePhaseBivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public IEnumerable<GaPoTSymSinglePhaseBivectorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsZero());
        }


        public GaPoTSymSinglePhaseBivector Reverse()
        {
            var result = new GaPoTSymSinglePhaseBivector();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(pair.Value);
                else
                    result.AddTerm(-pair.Value);
            }

            return result;
        }

        public GaPoTSymSinglePhaseBivector NegativeReverse()
        {
            var result = new GaPoTSymSinglePhaseBivector();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(-pair.Value);
                else
                    result.AddTerm(pair.Value);
            }

            return result;
        }

        public Expr Norm2()
        {
            return Mfs.SumExpr(_termsDictionary
                .Values
                .Select(t => t.Norm2())
                .ToArray()
            );
        }

        public GaPoTSymSinglePhaseBivector Inverse()
        {
            var norm2 = Norm2();

            if (norm2.IsZero())
                throw new DivideByZeroException();

            var value = Mfs.Divide[1, norm2];

            return new GaPoTSymSinglePhaseBivector(
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


        public string ToLaTeX()
        {
            return TermsToLaTeX();
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