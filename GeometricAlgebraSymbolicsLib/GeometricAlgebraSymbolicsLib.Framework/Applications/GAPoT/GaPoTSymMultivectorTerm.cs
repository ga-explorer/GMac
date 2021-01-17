using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymMultivectorTerm
    {
        public static GaPoTSymVectorTerm operator -(GaPoTSymMultivectorTerm term)
        {
            return new GaPoTSymVectorTerm(
                term.IDsPattern, 
                Mfs.Minus[term.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultivectorTerm operator +(GaPoTSymMultivectorTerm t1, GaPoTSymMultivectorTerm t2)
        {
            if (t1.IDsPattern != t2.IDsPattern)
                throw new InvalidOperationException();

            return new GaPoTSymMultivectorTerm(
                t1.IDsPattern, 
                Mfs.Plus[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultivectorTerm operator -(GaPoTSymMultivectorTerm t1, GaPoTSymMultivectorTerm t2)
        {
            if (t1.IDsPattern != t2.IDsPattern)
                throw new InvalidOperationException();

            return new GaPoTSymMultivectorTerm(
                t1.IDsPattern, 
                Mfs.Subtract[t1.Value, t2.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultivectorTerm operator *(GaPoTSymMultivectorTerm t, Expr s)
        {
            return new GaPoTSymMultivectorTerm(
                t.IDsPattern,
                Mfs.Times[t.Value, s].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultivectorTerm operator *(Expr s, GaPoTSymMultivectorTerm t)
        {
            return new GaPoTSymMultivectorTerm(
                t.IDsPattern,
                Mfs.Times[s, t.Value].GaPoTSymSimplify()
            );
        }

        public static GaPoTSymMultivectorTerm operator /(GaPoTSymMultivectorTerm t, Expr s)
        {
            return new GaPoTSymMultivectorTerm(
                t.IDsPattern,
                Mfs.Divide[t.Value, s].GaPoTSymSimplify()
            );
        }

        
        
        public int IDsPattern { get; }

        public Expr Value { get; set; }


        public GaPoTSymMultivectorTerm(int idsPattern)
        {
            Debug.Assert(idsPattern >= 0);

            IDsPattern = idsPattern;
            Value = Expr.INT_ZERO;
        }

        public GaPoTSymMultivectorTerm(int idsPattern, Expr value)
        {
            Debug.Assert(idsPattern >= 0);

            IDsPattern = idsPattern;
            Value = value;
        }


        public IEnumerable<int> GetTermIDs()
        {
            var idsPattern = IDsPattern;
            var i = 1;
            while (idsPattern > 0)
            {
                if ((idsPattern & 1) != 0)
                    yield return i;

                i++;
                idsPattern >>= 1;
            }
        }

        public int GetGrade()
        {
            return IDsPattern.BasisBladeGrade();
        }


        public GaPoTSymMultivectorTerm Op(GaPoTSymMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = Mfs.Times[term1.Value, term2.Value].GaPoTSymSimplify();

            if (!GaFrameUtils.IsNonZeroOp(term1.IDsPattern, term2.IDsPattern) || value.IsZero())
                return new GaPoTSymMultivectorTerm(0, Expr.INT_ZERO);

            if (GaFrameUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = Mfs.Minus[value].GaPoTSymSimplify();

            return new GaPoTSymMultivectorTerm(idsPattern, value);
        }

        public GaPoTSymMultivectorTerm Sp(GaPoTSymMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = Mfs.Times[term1.Value, term2.Value].GaPoTSymSimplify();

            if (!GaFrameUtils.IsNonZeroELcp(term1.IDsPattern, term2.IDsPattern) || value.IsZero())
                return new GaPoTSymMultivectorTerm(idsPattern, Expr.INT_ZERO);

            if (GaFrameUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = Mfs.Minus[value].GaPoTSymSimplify();

            return new GaPoTSymMultivectorTerm(idsPattern, value);
        }

        public GaPoTSymMultivectorTerm Lcp(GaPoTSymMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = Mfs.Times[term1.Value, term2.Value].GaPoTSymSimplify();

            if (!GaFrameUtils.IsNonZeroELcp(term1.IDsPattern, term2.IDsPattern) || value.IsZero())
                return new GaPoTSymMultivectorTerm(idsPattern, Expr.INT_ZERO);

            if (GaFrameUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = Mfs.Minus[value].GaPoTSymSimplify();

            return new GaPoTSymMultivectorTerm(idsPattern, value);
        }

        public GaPoTSymMultivectorTerm Gp(GaPoTSymMultivectorTerm term2)
        {
            var term1 = this;

            var idsPattern = term1.IDsPattern ^ term2.IDsPattern;
            var value = Mfs.Times[term1.Value, term2.Value].GaPoTSymSimplify();

            if (value.IsZero())
                return new GaPoTSymMultivectorTerm(idsPattern, Expr.INT_ZERO);

            if (GaFrameUtils.ComputeIsNegativeEGp(term1.IDsPattern, term2.IDsPattern))
                value = Mfs.Minus[value].GaPoTSymSimplify();

            return new GaPoTSymMultivectorTerm(idsPattern, value);
        }

        public GaPoTSymMultivectorTerm Reverse()
        {
            return new GaPoTSymMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeIdHasNegativeReverse()
                    ? Mfs.Minus[Value] 
                    : Value
            );
        }

        public GaPoTSymMultivectorTerm GradeInvolution()
        {
            return new GaPoTSymMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeIdHasNegativeGradeInv()
                    ? Mfs.Minus[Value] 
                    : Value
            );
        }

        public GaPoTSymMultivectorTerm CliffordConjugate()
        {
            return new GaPoTSymMultivectorTerm(
                IDsPattern, 
                IDsPattern.BasisBladeIdHasNegativeCliffConj()
                    ? Mfs.Minus[Value] 
                    : Value
            );
        }

        public GaPoTSymMultivectorTerm ScaledReverse(Expr s)
        {
            var value = 
                IDsPattern.BasisBladeIdHasNegativeReverse() 
                    ? Mfs.Times[Mfs.Minus[Value], s].GaPoTSymSimplify() 
                    : Mfs.Times[Value, s].GaPoTSymSimplify();
            
            return new GaPoTSymMultivectorTerm(IDsPattern, value);
        }
        

        public GaPoTSymVectorTerm ToVectorTerm()
        {
            var termIDsArray = GetTermIDs().ToArray();

            if (termIDsArray.Length != 1)
                throw new InvalidOperationException($"Can't convert multivector term <{termIDsArray.Concatenate(",")}> to vector term");

            return new GaPoTSymVectorTerm(termIDsArray[0], Value);
        }

        public GaPoTSymBiversorTerm ToBiversorTerm()
        {
            if (IDsPattern == 0)
                return new GaPoTSymBiversorTerm(Value);
            
            var termIDsArray = GetTermIDs().ToArray();

            if (termIDsArray.Length != 2)
                throw new InvalidOperationException($"Can't convert multivector term <{termIDsArray.Concatenate(",")}> to biversor term");

            return new GaPoTSymBiversorTerm(termIDsArray[0], termIDsArray[1], Value);
        }


        public string ToText()
        {
            return $"{Value:G} <{GetTermIDs().Concatenate(",")}>";
        }
        
        public string ToLaTeX()
        {
            var valueText = 
                Value.GetLaTeX();

            var basisText = 
                IDsPattern == 0
                ? ""
                : $"{GetTermIDs().Concatenate(",")}".GetLaTeXBasisName();

            return $@"\left( {valueText} \right) {basisText}";
        }

        public override string ToString()
        {
            return ToText();
        }
    }
}