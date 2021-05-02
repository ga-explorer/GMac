using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DataStructuresLib;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;
using TextComposerLib.Text;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymMultivector
    {
        public static GaPoTSymMultivector CreateZero()
        {
            return new GaPoTSymMultivector();
        }

        public static GaPoTSymMultivector CreateTerm(ulong idsPattern, Expr value)
        {
            return new GaPoTSymMultivector().SetTerm(idsPattern, value);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static GaPoTSymMultivector CreateSimpleRotor(Expr rotationAngle, GaPoTSymMultivector rotationBlade)
        {
            var cosHalfAngle = MathematicaUtils.Evaluate(Mfs.Cos[Mfs.Divide[rotationAngle, 2.ToExpr()]]);
            var sinHalfAngle = MathematicaUtils.Evaluate(Mfs.Sin[Mfs.Divide[rotationAngle, 2.ToExpr()]]);

            var rotationBladeScalar =
                Mfs.Divide[
                    sinHalfAngle, 
                    Mfs.Sqrt[Mfs.Abs[rotationBlade.Gp(rotationBlade).GetTermValue(0)]]
                ];

            var rotor=  
                cosHalfAngle + rotationBladeScalar * rotationBlade;

            //rotor.IsSimpleRotor();

            return rotor;
        }

        public static GaPoTSymMultivector CreateSimpleRotor(GaPoTSymVector inputVector, GaPoTSymVector rotatedVector)
        {
            var cosAngle = Mfs.Divide[
                inputVector.DotProduct(rotatedVector), 
                Mfs.Sqrt[Mfs.Times[inputVector.Norm2(), rotatedVector.Norm2()]]
            ].FullSimplify();

            if (cosAngle.IsOne())
                return new GaPoTSymMultivector().SetTerm(0, Expr.INT_ONE);
            
            //TODO: Handle the case for cosAngle == -1
            if (cosAngle.IsMinusOne())
                throw new InvalidOperationException(
                    $"Can't find a unique rotation plane to rotate {inputVector} into {rotatedVector}"
                );
            
            var cosHalfAngle = 
                Mfs.Sqrt[Mfs.Divide[Mfs.Plus[Expr.INT_ONE, cosAngle], 2.ToExpr()]];
            
            var sinHalfAngle = 
                Mfs.Sqrt[Mfs.Divide[Mfs.Subtract[Expr.INT_ONE, cosAngle], 2.ToExpr()]];
            
            var rotationBlade = 
                inputVector.Op(rotatedVector).FullSimplifyScalars();

            var rotationBladeScalar =
                Mfs.Divide[
                    sinHalfAngle, 
                    Mfs.Sqrt[Mfs.Abs[rotationBlade.Gp(rotationBlade).GetTermValue(0)]]
                ];

            var rotor=  
                (cosHalfAngle - rotationBladeScalar * rotationBlade).FullSimplifyScalars();
            
            //var rotationAngle = Math.Acos(DotProduct(v2) * invNorm1 * invNorm2) / 2;
            //var unitBlade = rotationBlade.ScaleBy(rotationBladeInvNorm);
            //var unitBladeNorm = unitBlade.Gp(unitBlade).TermsToText();
            //var rotor= Math.Cos(rotationAngle) - (rotationBladeInvNorm * Math.Sin(rotationAngle)) * rotationBlade;

            //Normalize rotor
            //var invRotorNorm = 1.0d / Math.Sqrt(rotor.Gp(rotor.Reverse()).GetTermValue(0));
            
            return rotor;
        }

        public static GaPoTSymMultivector CreateSimpleRotor(GaPoTSymVector inputVector1, GaPoTSymVector inputVector2, GaPoTSymVector rotatedVector1, GaPoTSymVector rotatedVector2)
        {
            var inputFrame = GaPoTSymFrame.Create(inputVector1, inputVector2);
            var rotatedFrame = GaPoTSymFrame.Create(rotatedVector1, rotatedVector2);

            return GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static GaPoTSymMultivector CreateSimpleRotor(int baseSpaceDimensions, GaPoTSymVector inputVector1, GaPoTSymVector inputVector2, GaPoTSymVector rotatedVector1, GaPoTSymVector rotatedVector2)
        {
            var inputFrame = GaPoTSymFrame.Create(inputVector1, inputVector2);
            var rotatedFrame = GaPoTSymFrame.Create(rotatedVector1, rotatedVector2);

            return GaPoTSymRotorsSequence.CreateFromFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();
        }

        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static GaPoTSymMultivector CreateGivensRotor(int i, int j, Expr angle)
        {
            Debug.Assert(i > 0 && j > i);

            var cosHalfAngle = MathematicaUtils.Evaluate(Mfs.Cos[Mfs.Divide[angle, 2.ToExpr()]]);
            var sinHalfAngle = MathematicaUtils.Evaluate(Mfs.Cos[Mfs.Divide[angle, 2.ToExpr()]]);

            var bladeId = (1UL << (i - 1)) | (1UL << (j - 1));

            return new GaPoTSymMultivector()
                .AddTerm(0, cosHalfAngle)
                .AddTerm(bladeId, sinHalfAngle);
        }
        

        public static GaPoTSymMultivector operator -(GaPoTSymMultivector v)
        {
            var result = new GaPoTSymMultivector();

            foreach (var term in v._termsDictionary.Values)
                result.AddTerm(
                    term.IDsPattern, 
                    Mfs.Minus[term.Value].Evaluate()
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
                    Mfs.Minus[term.Value].Evaluate()
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
                    Mfs.Minus[term.Value].Evaluate()
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

        public static GaPoTSymMultivector operator /(GaPoTSymMultivector v1, GaPoTSymMultivector v2)
        {
            return v1 * v2.Inverse();
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

        public static GaPoTSymMultivector operator /(Expr s, GaPoTSymMultivector v)
        {
            return s * v.Inverse();
        }
        
        
        private readonly Dictionary<ulong, GaPoTSymMultivectorTerm> _termsDictionary
            = new Dictionary<ulong, GaPoTSymMultivectorTerm>();


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

        public GaPoTSymMultivector SetTerm(ulong idsPattern, Expr value)
        {
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
                        Mfs.Plus[oldTerm.Value, term.Value].Evaluate()
                    );
            else
                _termsDictionary.Add(
                    idsPattern, 
                    new GaPoTSymMultivectorTerm(idsPattern, term.Value)
                );

            return this;
        }

        public GaPoTSymMultivector AddTerm(ulong idsPattern, Expr value)
        {
            if (_termsDictionary.TryGetValue(idsPattern, out var oldTerm))
                _termsDictionary[idsPattern] = 
                    new GaPoTSymMultivectorTerm(
                        idsPattern, 
                        Mfs.Plus[oldTerm.Value, value].Evaluate()
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

        public IEnumerable<GaPoTSymMultivectorTerm> GetGradeOrderedTerms()
        {
            var bitsCount = _termsDictionary.Keys.Max().LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return _termsDictionary.Values;

            return _termsDictionary
                .Values
                .Where(t => !t.Value.IsZero())
                .OrderBy(t => t.GetGrade())
                .ThenByDescending(t => t.IDsPattern.ReverseBits(bitsCount));
        }

        public IEnumerable<GaPoTSymMultivectorTerm> GetTermsOfGrade(int grade)
        {
            Debug.Assert(grade >= 0);
            
            return GetTerms().Where(t => t.GetGrade() == grade);
        }

        public Expr GetTermValue(ulong idsPattern)
        {
            return _termsDictionary.TryGetValue(idsPattern, out var term) 
                ? term.Value 
                : Expr.INT_ZERO;
        }

        public Expr GetScalar()
        {
            return GetTermValue(0);
        }

        public GaPoTSymMultivectorTerm GetTerm(ulong idsPattern)
        {
            var value = GetTermValue(idsPattern);

            return new GaPoTSymMultivectorTerm(idsPattern, value);
        }

        public GaPoTSymMultivector GetKVectorPart(int grade)
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Where(t => t.GetGrade() == grade)
            );
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

        /// <summary>
        /// Assuming this multivector is a simple rotor, this extracts its angle and 2-blade of rotation
        /// </summary>
        /// <returns></returns>
        public Tuple<Expr, GaPoTSymMultivector> GetSimpleRotorAngleBlade()
        {
            var scalarPart = GetTermValue(0);
            var bivectorPart = new GaPoTSymMultivector(GetTermsOfGrade(2));

            var halfAngle = Mfs.ArcCos[scalarPart];
            var angle = MathematicaUtils.Evaluate(Mfs.Times[2.ToExpr(), halfAngle]);
            var blade = bivectorPart / Mfs.Sin[halfAngle];

            return new Tuple<Expr, GaPoTSymMultivector>(angle, blade);
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
                
                var value = Mfs.Plus[term1.Value, term2.Value].Evaluate();

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

        public GaPoTSymMultivector FullSimplifyScalars()
        {
            return new GaPoTSymMultivector(
                _termsDictionary.Values.Select(
                    t => new GaPoTSymMultivectorTerm(t.IDsPattern, t.Value.FullSimplify())
                )
            );
        }

        public GaPoTSymMultivector ScalarsToNumerical()
        {
            return MapScalars(e => Mfs.N[e].Evaluate());
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

        public GaPoTSymMultivector OrthogonalComplement(GaPoTSymMultivector blade)
        {
            return Gp(blade.Inverse());
        }

        public Expr Norm()
        {
            return Mfs.Sqrt[Norm2()].Evaluate();
        }

        public Expr Norm2()
        {
            return Mfs.SumExpr(
                _termsDictionary.Values.Select(
                    term => Mfs.Times[term.Value, term.Value]
                ).ToArray()
            ).Evaluate();
        }

        public GaPoTSymMultivector Inverse()
        {
            var norm2Array = new Expr[_termsDictionary.Count];
            var termsArray = new GaPoTSymMultivectorTerm[_termsDictionary.Count];

            var i = 0;
            foreach (var term in _termsDictionary.Values)
            {
                termsArray[i] = new GaPoTSymMultivectorTerm(
                    term.IDsPattern,
                    term.IDsPattern.BasisBladeIdHasNegativeReverse()
                        ? Mfs.Minus[term.Value]
                        : term.Value 
                );

                norm2Array[i] = Mfs.Times[term.Value, term.Value];

                i++;
            }

            var norm2 = Mfs.Divide[Expr.INT_ONE, Mfs.SumExpr(norm2Array)].Evaluate();

            foreach (var term in termsArray)
                term.Value = Mfs.Times[term.Value, norm2].Evaluate();
            
            return new GaPoTSymMultivector(termsArray);
        }

        public GaPoTSymMultivector DivideByNorm()
        {
            return this / Norm();
        }

        public GaPoTSymMultivector DivideByNorm2()
        {
            return this / Norm2();
        }
        
        public GaPoTSymMultivector ApplyRotor(GaPoTSymMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();

            return r1.Gp(this).Gp(r2);
        }

        public GaPoTSymMultivector Round(int places)
        {
            return new GaPoTSymMultivector(
                _termsDictionary
                    .Values
                    .Select(t => t.Round(places))
                    .Where(t => !t.Value.IsZero())
            );
        }


        public string ToText()
        {
            return TermsToText();
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

        public string ToLaTeX()
        {
            return TermsToLaTeX();
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

        public string ToLaTeXDisplayEquation(string multivectorName, string basisName)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\[");

            var termsArray = 
                GetGradeOrderedTerms()
                    .OrderBy(t => t.GetGrade())
                    .ThenBy(t => t.IDsPattern)
                    .ToArray();

            textComposer.AppendLine($@"{multivectorName} = ");

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

                var termText = j == 0
                    ? $@"{termLaTeX}"
                    : $@" + {termLaTeX}";

                textComposer.Append(termText);

                j++;
            }

            textComposer
                .AppendLine()
                .AppendLine(@"\]");

            return textComposer.ToString();
        }

        public string ToLaTeXEquationsArray(string multivectorName, string basisName)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\begin{eqnarray*}");

            var termsArray = 
                GetGradeOrderedTerms()
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