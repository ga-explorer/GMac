using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumRectPhasor
    {
        public static GaPoTNumRectPhasor operator -(GaPoTNumRectPhasor p)
        {
            return new GaPoTNumRectPhasor(
                p.Id,
                -p.XValue,
                -p.YValue
            );
        }

        public static GaPoTNumRectPhasor operator +(GaPoTNumRectPhasor p1, GaPoTNumRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTNumRectPhasor(
                p1.Id,
                p1.XValue + p2.XValue,
                p1.YValue + p2.YValue
            );
        }

        public static GaPoTNumRectPhasor operator -(GaPoTNumRectPhasor p1, GaPoTNumRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTNumRectPhasor(
                p1.Id,
                p1.XValue - p2.XValue,
                p1.YValue - p2.YValue
            );
        }


        public int Id { get; }

        public double XValue { get; }

        public double YValue { get; }


        public GaPoTNumRectPhasor(int id, double x, double y)
        {
            Debug.Assert(id % 2 == 0);

            Id = id;
            XValue = x;
            YValue = y;
        }

        
        public IEnumerable<GaPoTNumSinglePhaseVectorTerm> GetTerms()
        {
            if (XValue != 0)
                yield return new GaPoTNumSinglePhaseVectorTerm(
                    Id,
                    XValue
                );

            if (YValue != 0)
                yield return new GaPoTNumSinglePhaseVectorTerm(
                    Id + 1,
                    -YValue
                );
        }

        public double Norm2()
        {
            return XValue * XValue + YValue * YValue;
        }

        public GaPoTNumPolarPhasor ToPolarPhasor()
        {
            return new GaPoTNumPolarPhasor(
                Id,
                Math.Sqrt(XValue * XValue + YValue * YValue),
                Math.Atan2(YValue, XValue)
            );
        }
        
        public string ToText()
        {
            if (XValue == 0 && YValue == 0)
                return "0";

            var i1 = Id + 1;
            var i2 = Id + 2;

            return $"r({XValue:G}, {YValue:G}) <{i1},{i2}>";
        }

        public string ToLaTeX()
        {
            if (XValue == 0 && YValue == 0)
                return "0";

            var i1 = Id + 1;
            var i2 = Id + 2;

            var xValueText = GaPoTNumUtils.GetLaTeXNumber(XValue);
            var yValueText = GaPoTNumUtils.GetLaTeXNumber(YValue);
            var basisText1 = GaPoTNumUtils.GetLaTeXBasisName($"{i1},{i2}");
            var basisText2 = GaPoTNumUtils.GetLaTeXBasisName($"{i1}");

            if (XValue == 0)
                return $@"\left( {yValueText} \right) {basisText1} {basisText2}";

            if (YValue == 0)
                return $@"{xValueText} {basisText2}";

            return $@"\left[ {xValueText} + \left( {yValueText} \right) {basisText1} \right] {basisText2}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}