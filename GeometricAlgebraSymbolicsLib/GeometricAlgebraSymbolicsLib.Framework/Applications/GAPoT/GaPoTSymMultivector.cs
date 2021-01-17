using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymMultivector
    {
        public static GaPoTSymMultivector CreateZero()
        {
            return new GaPoTSymMultivector();
        }
        
        
        public static GaPoTSymMultivector operator -(GaPoTSymMultivector v)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern, 
                    Mfs.Minus[term.Value].GaPoTSymSimplify()
                );

            return result;
        }

        public static GaPoTSymMultivector operator +(GaPoTSymMultivector v1, GaPoTSymMultivector v2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            return result;
        }

        public static GaPoTSymMultivector operator +(GaPoTSymMultivector v1, Expr v2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            result.AddTerm(0, v2);

            return result;
        }

        public static GaPoTSymMultivector operator +(Expr v2, GaPoTSymMultivector v1)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            result.AddTerm(0, v2);

            return result;
        }

        public static GaPoTSymMultivector operator -(GaPoTSymMultivector v1, GaPoTSymMultivector v2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    Mfs.Minus[term.Value].GaPoTSymSimplify()
                );

            return result;
        }

        public static GaPoTSymMultivector operator -(GaPoTSymMultivector v1, Expr v2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v1._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    term.Value
                );

            result.AddTerm(0, Mfs.Minus[v2]);

            return result;
        }

        public static GaPoTSymMultivector operator -(Expr v1, GaPoTSymMultivector v2)
        {
            var result = new GaPoTSymMultivector();

            result.AddTerm(0, v1);

            foreach (var term in v2._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    Mfs.Minus[term.Value].GaPoTSymSimplify()
                );

            return result;
        }
        
        public static GaPoTSymMultivector operator *(GaPoTSymMultivector v1, GaPoTSymMultivector v2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term1 in v1._termsDictionary.Values)
            foreach (var term2 in v2._termsDictionary.Values)
            {
                var term = term1.Gp(term2);

                if (!term.Value.IsZero())
                    result.AddTerm(term);
            }

            return result;
        }
        
        public static GaPoTSymMultivector operator *(GaPoTSymMultivector v, Expr s)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    Mfs.Times[term.Value, s]
                );

            return result;
        }

        public static GaPoTSymMultivector operator *(Expr s, GaPoTSymMultivector v)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    Mfs.Times[s, term.Value]
                );

            return result;
        }

        public static GaPoTSymMultivector operator /(GaPoTSymMultivector v, Expr s)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern,
                    Mfs.Divide[term.Value, s]
                );

            return result;
        }

        
        
        private readonly Dictionary<int, GaPoTSymMultivectorTerm> _termsDictionary
            = new Dictionary<int, GaPoTSymMultivectorTerm>();


        public int Count 
            => _termsDictionary.Count;

        
        internal GaPoTSymMultivector()
        {
        }

        internal GaPoTSymMultivector(IEnumerable<GaPoTSymMultivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GaPoTSymMultivector SetToZero()
        {
            _termsDictionary.Clear();

            return this;
        }
        
        public bool IsZero()
        {
            return _termsDictionary.Values.All(t => t.Value.IsZero());
        }

        public bool IsRotor()
        {
            if (GetNonZeroTerms().Select(term => term.GetGrade()).Any(grade => grade % 2 != 0))
                return false;

            var s = Gp(Reverse()) - Expr.INT_ONE;

            return s.IsZero();
        }

        public bool IsSimpleRotor()
        {
            if (GetNonZeroTerms().Select(term => term.GetGrade()).Any(grade => grade != 0 && grade != 2))
                return false;

            var s = Gp(Reverse()) - Expr.INT_ONE;

            return s.IsZero();
        }

        public GaPoTSymMultivector SetTerm(int idsPattern, Expr value)
        {
            Debug.Assert(idsPattern >= 0);

            if (_termsDictionary.ContainsKey(idsPattern))
                _termsDictionary[idsPattern].Value = value;
            else
                _termsDictionary.Add(idsPattern, new GaPoTSymMultivectorTerm(idsPattern, value));

            return this;
        }

        public GaPoTSymMultivector AddTerm(GaPoTSymMultivectorTerm term)
        {
            var idsPattern = term.IDsPattern;

            if (_termsDictionary.TryGetValue(idsPattern, out var oldTerm))
                _termsDictionary[idsPattern] = 
                    new GaPoTSymMultivectorTerm(
                        idsPattern, 
                        Mfs.Plus[oldTerm.Value, term.Value].GaPoTSymSimplify()
                    );
            else
                _termsDictionary.Add(
                    idsPattern, 
                    new GaPoTSymMultivectorTerm(idsPattern, term.Value)
                );

            return this;
        }

        public GaPoTSymMultivector AddTerm(int idsPattern, Expr value)
        {
            Debug.Assert(idsPattern >= 0);

            if (_termsDictionary.TryGetValue(idsPattern, out var oldTerm))
                _termsDictionary[idsPattern] = 
                    new GaPoTSymMultivectorTerm(
                        idsPattern, 
                        Mfs.Plus[oldTerm.Value, value].GaPoTSymSimplify()
                    );
            else
                _termsDictionary.Add(idsPattern, new GaPoTSymMultivectorTerm(idsPattern, value));

            return this;
        }

        public GaPoTSymMultivector AddTerms(IEnumerable<GaPoTSymMultivectorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }

        
        public IEnumerable<GaPoTSymMultivectorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsZero());
        }

        public IEnumerable<GaPoTSymMultivectorTerm> GetTermsOfGrade(int grade)
        {
            Debug.Assert(grade >= 0);
            
            return GetTerms().Where(t => t.GetGrade() == grade);
        }

        public Expr GetTermValue(int idsPattern)
        {
            Debug.Assert(idsPattern >= 0);
            
            return _termsDictionary.TryGetValue(idsPattern, out var term) 
                ? term.Value 
                : Expr.INT_ZERO;
        }

        public GaPoTSymMultivectorTerm GetTerm(int idsPattern)
        {
            var value = GetTermValue(idsPattern);

            return new GaPoTSymMultivectorTerm(idsPattern, value);
        }

        public IEnumerable<GaPoTSymMultivectorTerm> GetNonZeroTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsZero());
        }

        public GaPoTSymVector GetVectorPart()
        {
            return new GaPoTSymVector(
                GetTermsOfGrade(1).Select(t => t.ToVectorTerm())
            );
        }

        public GaPoTSymBiversor GetBiversorPart()
        {
            var biversor = new GaPoTSymBiversor();

            var scalarValue = GetTermValue(0);

            if (!scalarValue.IsZero())
                biversor.AddTerm(new GaPoTSymBiversorTerm(scalarValue));

            biversor.AddTerms(
                GetTermsOfGrade(2).Select(t => t.ToBiversorTerm())
            );

            return biversor;
        }


        public GaPoTSymMultivector Op(GaPoTSymMultivector mv2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Op(term2);

                if (!term.Value.IsZero())
                    result.AddTerm(term);
            }

            return result;
        }
        
        public GaPoTSymMultivector Op(GaPoTSymVector v)
        {
            return Op(v.ToMultivector());
        }

        public GaPoTSymMultivector Sp(GaPoTSymMultivector mv2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term1 in _termsDictionary.Values)
            {
                if (!mv2._termsDictionary.TryGetValue(term1.IDsPattern, out var term2)) 
                    continue;
                
                var value = Mfs.Plus[term1.Value, term2.Value].GaPoTSymSimplify();

                if (value.IsZero())
                    continue;
                    
                if (GaFrameUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                    value = Mfs.Minus[value];

                result.AddTerm(new GaPoTSymMultivectorTerm(0, value));
            }

            return result;
        }

        public GaPoTSymMultivector Lcp(GaPoTSymMultivector mv2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Lcp(term2);

                if (!term.Value.IsZero())
                    result.AddTerm(term);
            }

            return result;
        }

        public GaPoTSymMultivector Gp(GaPoTSymMultivector mv2)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term1 in _termsDictionary.Values)
            foreach (var term2 in mv2._termsDictionary.Values)
            {
                var term = term1.Gp(term2);

                if (!term.Value.IsZero())
                    result.AddTerm(term);
            }

            return result;
        }

        public GaPoTSymMultivector Add(GaPoTSymMultivector v)
        {
            return this + v;
        }

        public GaPoTSymMultivector Subtract(GaPoTSymMultivector v)
        {
            return this - v;
        }

        public GaPoTSymMultivector Negative()
        {
            return -this;
        }

        public GaPoTSymMultivector ScaleBy(Expr s)
        {
            return s * this;
        }

        public GaPoTSymMultivector MapScalars(Func<Expr, Expr> mappingFunc)
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Select(
                    t => new GaPoTSymMultivectorTerm(t.IDsPattern, mappingFunc(t.Value))
                )
            );
        }

        public GaPoTSymMultivector Reverse()
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Select(t => t.Reverse())
            );
        }

        public GaPoTSymMultivector GradeInvolution()
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Select(t => t.GradeInvolution())
            );
        }

        public GaPoTSymMultivector CliffordConjugate()
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Select(t => t.CliffordConjugate())
            );
        }

        public GaPoTSymMultivector ScaledReverse(Expr s)
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Select(t => t.ScaledReverse(s))
            );
        }

        public Expr Norm()
        {
            return Mfs.Sqrt[Norm2()].GaPoTSymSimplify();
        }

        public Expr Norm2()
        {
            return Mfs.SumExpr(_termsDictionary.Values.Select(term => 
                term.IDsPattern.BasisBladeIdHasNegativeReverse()
                    ? Mfs.Times[Expr.INT_MINUSONE, term.Value, term.Value]
                    : Mfs.Times[term.Value, term.Value]
            ).ToArray());
        }

        public GaPoTSymMultivector Inverse()
        {
            var norm2Array = new Expr[_termsDictionary.Count];
            var termsArray = new GaPoTSymMultivectorTerm[_termsDictionary.Count];
            var i = 0;
            foreach (var term in _termsDictionary.Values)
            {
                if (term.IDsPattern.BasisBladeIdHasNegativeReverse())
                {
                    termsArray[i] =  new GaPoTSymMultivectorTerm(
                        term.IDsPattern,
                        Mfs.Minus[term.Value]
                    );
                    
                    norm2Array[i] = Mfs.Times[Expr.INT_MINUSONE, term.Value, term.Value];
                }
                else
                {
                    termsArray[i] =  new GaPoTSymMultivectorTerm(
                        term.IDsPattern, 
                        term.Value
                    );
                    
                    norm2Array[i] = Mfs.Times[term.Value, term.Value];
                }
                
                i++;
            }

            var norm2 = Mfs.Divide[Expr.INT_ONE, Mfs.SumExpr(norm2Array)].GaPoTSymSimplify();

            foreach (var term in termsArray)
                term.Value = Mfs.Times[term.Value, norm2].GaPoTSymSimplify();
            
            return new GaPoTSymMultivector(termsArray);
        }
        
        public GaPoTSymMultivector ApplyRotor(GaPoTSymMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();

            return r1.Gp(this).Gp(r2);
        }

        
        public string TermsToText()
        {
            var termsArray = GetTerms()
                .OrderBy(t => t.GetGrade())
                .ThenBy(t => t.IDsPattern)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
        }

        public string TermsToLaTeX()
        {
            var termsArray = GetTerms()
                .OrderBy(t => t.GetGrade())
                .ThenBy(t => t.IDsPattern)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : string.Join(" + ", termsArray.Select(t => t.ToLaTeX()));
        }

        public string ToLaTeXEquationsArray(string multivectorName, string basisName)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\begin{eqnarray*}");

            var termsArray = 
                GetNonZeroTerms()
                    .OrderBy(t => t.GetGrade())
                    .ThenBy(t => t.IDsPattern)
                    .ToArray();

            var j = 0;
            foreach (var term in termsArray)
            {
                var termLaTeX = 
                    new GaPoTSymMultivectorTerm(
                            term.IDsPattern,
                            Mfs.ToRadicals[term.Value]
                        )
                        .ToLaTeX()
                        .Replace(@"\sigma_", $"{basisName}_");

                var line = j == 0
                    ? $@"{multivectorName} & = & {termLaTeX}"
                    : $@" & + & {termLaTeX}";

                if (j < termsArray.Length - 1)
                    line += @"\\";

                textComposer.AppendLine(line);

                j++;
            }

            textComposer.AppendLine(@"\end{eqnarray*}");

            return textComposer.ToString();
        }


        public override string ToString()
        {
            return TermsToText();
        }
    }
}