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
    public sealed class GaPoTSymBiversor
    {
        public static GaPoTSymBiversor operator -(GaPoTSymBiversor v)
        {
            return new GaPoTSymBiversor(
                v._termsDictionary.Values.Select(t => -t)
            );
        }

        public static GaPoTSymBiversor operator +(GaPoTSymBiversor v1, GaPoTSymBiversor v2)
        {
            var biVector = new GaPoTSymBiversor();

            biVector.AddTerms(v1._termsDictionary.Values);
            biVector.AddTerms(v2._termsDictionary.Values);

            return biVector;
        }

        public static GaPoTSymBiversor operator -(GaPoTSymBiversor v1, GaPoTSymBiversor v2)
        {
            var biVector = new GaPoTSymBiversor();

            biVector.AddTerms(v1._termsDictionary.Values);
            biVector.AddTerms(v2._termsDictionary.Values.Select(t => -t));

            return biVector;
        }

        public static GaPoTSymBiversor operator *(GaPoTSymBiversor v, Expr s)
        {
            return new GaPoTSymBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GaPoTSymBiversor operator *(Expr s, GaPoTSymBiversor v)
        {
            return new GaPoTSymBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GaPoTSymBiversor operator /(GaPoTSymBiversor v, Expr s)
        {
            return new GaPoTSymBiversor(
                v._termsDictionary.Values.Select(t => t / s)
            );
        }


        private readonly Dictionary2Keys<int, GaPoTSymBiversorTerm> _termsDictionary
            = new Dictionary2Keys<int, GaPoTSymBiversorTerm>();

        
        public Expr this[int id1, int id2]
        {
            get
            {
                (id1, id2) = GaPoTSymUtils.ValidateBiversorTermIDs(id1, id2);

                return Mfs.SumExpr(
                _termsDictionary
                    .Values
                    .Where(t => t.TermId1 == id1 && t.TermId2 == id2)
                    .Select(v => v.Value)
                    .ToArray()
                ).GaPoTSymSimplify();
            }
        }

        
        public GaPoTSymBiversor()
        {
        }

        public GaPoTSymBiversor(IEnumerable<GaPoTSymBiversorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GaPoTSymBiversor AddTerm(GaPoTSymBiversorTerm term)
        {
            //The input term IDs is already validation by construction, no validation is needed here
            var id1 = term.TermId1;
            var id2 = term.TermId2;

            if (_termsDictionary.TryGetValue(term.TermId1, term.TermId2, out var oldTerm))
                _termsDictionary[id1, id2] = new GaPoTSymBiversorTerm(
                    id1,
                    id2,
                    Mfs.Plus[oldTerm.Value, term.Value].GaPoTSymSimplify()
                );
            else
                _termsDictionary.Add(id1, id2, term);

            return this;
        }

        public GaPoTSymBiversor AddTerm(int id1, int id2, Expr value)
        {
            return AddTerm(
                new GaPoTSymBiversorTerm(id1, id2, value)
            );
        }

        public GaPoTSymBiversor AddTerms(IEnumerable<GaPoTSymBiversorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public IEnumerable<GaPoTSymBiversorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsZero());
        }

        public Expr GetTermValuesSum()
        {
            return Mfs.SumExpr(
                _termsDictionary
                .Values
                .Select(v => v.Value)
                .ToArray()
            ).GaPoTSymSimplify();
        }

        public Expr GetTermValue(int id1, int id2)
        {
            (id1, id2) = GaPoTSymUtils.ValidateBiversorTermIDs(id1, id2);

            return Mfs.SumExpr(
                _termsDictionary
                .Values
                .Where(t => t.TermId1 == id1 && t.TermId2 == id2)
                .Select(v => v.Value)
                .ToArray()
            ).GaPoTSymSimplify();
        }

        public GaPoTSymBiversorTerm GetTerm(int id1, int id2)
        {
            var value = GetTermValue(id1, id2);

            return new GaPoTSymBiversorTerm(id1, id2, value);
        }
        
        public Expr GetActiveTotal()
        {
            return Mfs.SumExpr(
                _termsDictionary
                .Values
                .Where(t => t.IsScalar)
                .Select(v => v.Value)
                .ToArray()
            ).GaPoTSymSimplify();
        }

        public Expr GetNonActiveTotal()
        {
            return Mfs.SumExpr(
                _termsDictionary
                    .Values
                    .Where(t => t.IsNonScalar)
                    .Select(v => v.Value)
                    .ToArray()
            ).GaPoTSymSimplify();
        }

        public Expr GetReactiveTotal()
        {
            return Mfs.SumExpr(
                _termsDictionary
                    .Values
                    .Where(t => t.IsPhasor)
                    .Select(v => v.Value)
                    .ToArray()
            ).GaPoTSymSimplify();
        }

        public Expr GetReactiveFundamentalTotal()
        {
            return Mfs.SumExpr(
                _termsDictionary
                .Values
                .Where(t => t.TermId1 == 1 && t.TermId2 == 2)
                .Select(v => v.Value)
                .ToArray()
            ).GaPoTSymSimplify();
        }

        public Expr GetHarmTotal()
        {
            return Mfs.SumExpr(
                _termsDictionary
                .Values
                .Where(t => t.IsNonScalar && (t.TermId1 != 1 || t.TermId2 != 2))
                .Select(v => v.Value)
                .ToArray()
            ).GaPoTSymSimplify();
        }


        public GaPoTSymBiversor GetTermPart(int id1, int id2)
        {
            (id1, id2) = GaPoTSymUtils.ValidateBiversorTermIDs(id1, id2);

            return new GaPoTSymBiversor(
                _termsDictionary.Values.Where(t => t.TermId1 == id1 && t.TermId2 == id2)
            );
        }

        public GaPoTSymBiversor GetActivePart()
        {
            return new GaPoTSymBiversor(
                _termsDictionary.Values.Where(t => t.IsScalar)
            );
        }

        public GaPoTSymBiversor GetReactivePart()
        {
            return new GaPoTSymBiversor(
                _termsDictionary.Values.Where(t => t.IsPhasor)
            );
        }

        public GaPoTSymBiversor GetNonActivePart()
        {
            return new GaPoTSymBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar)
            );
        }

        public GaPoTSymBiversor GetReactiveFundamentalPart()
        {
            return new GaPoTSymBiversor(
                _termsDictionary.Values.Where(t => t.TermId1 == 1 && t.TermId2 == 2)
            );
        }

        public GaPoTSymBiversor GetHarmPart()
        {
            return new GaPoTSymBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar && (t.TermId1 != 1 || t.TermId2 != 2))
            );
        }


        public GaPoTSymBiversor Reverse()
        {
            var result = new GaPoTSymBiversor();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(pair.Value);
                else
                    result.AddTerm(-pair.Value);
            }

            return result;
        }

        public GaPoTSymBiversor NegativeReverse()
        {
            var result = new GaPoTSymBiversor();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(-pair.Value);
                else
                    result.AddTerm(pair.Value);
            }

            return result;
        }

        public Expr Norm()
        {
            var termsArray = 
                _termsDictionary
                    .Values
                    .Select(t => Mfs.Times[t.Value, t.Value])
                    .ToArray();

            return Mfs.Sqrt[Mfs.SumExpr(termsArray)].GaPoTSymSimplify();
        }

        public Expr Norm2()
        {
            var termsArray = 
                _termsDictionary
                    .Values
                    .Select(t => Mfs.Times[t.Value, t.Value])
                    .ToArray();

            return Mfs.SumExpr(termsArray).GaPoTSymSimplify();
        }

        public GaPoTSymBiversor Inverse()
        {
            var norm2 = Norm2();

            if (norm2.IsZero())
                throw new DivideByZeroException();

            var value = Mfs.Divide[1, norm2];

            return new GaPoTSymBiversor(
                _termsDictionary
                    .Values
                    .Select(t => t.ScaledReverse(value))
            );
        }

        public GaPoTSymVector Gp(GaPoTSymVector v)
        {
            return this * v;
        }

        public GaPoTSymMultivector ToMultivector()
        {
            return new GaPoTSymMultivector(
                GetTerms().Select(t => t.ToMultivectorTerm())
            );
        }


        public string ToText()
        {
            return TermsToText();
        }

        public string TermsToText()
        {
            var termsArray = 
                GetTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
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