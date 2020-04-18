using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumPolarPhasor
    {
        public static GaPoTNumPolarPhasor operator -(GaPoTNumPolarPhasor p)
        {
            return new GaPoTNumPolarPhasor(p.Id, -p.Magnitude, p.Phase);
        }

        public static GaPoTNumPolarPhasor operator +(GaPoTNumPolarPhasor p1, GaPoTNumPolarPhasor p2)
        {
            var rp = 
                p1.ToRectPhasor() + p2.ToRectPhasor();

            return rp.ToPolarPhasor();
        }

        public static GaPoTNumPolarPhasor operator -(GaPoTNumPolarPhasor p1, GaPoTNumPolarPhasor p2)
        {
            var rp = 
                p1.ToRectPhasor() - p2.ToRectPhasor();

            return rp.ToPolarPhasor();
        }


        public int Id { get; }

        public double Magnitude { get; }

        public double Phase { get; }


        public GaPoTNumPolarPhasor(int id, double magnitude, double phase)
        {
            Debug.Assert(id % 2 == 0);

            Id = id;
            Magnitude = magnitude;
            Phase = phase;
        }


        public IEnumerable<GaPoTNumSinglePhaseVectorTerm> GetTerms()
        {
            return Magnitude == 0
                ? Enumerable.Empty<GaPoTNumSinglePhaseVectorTerm>()
                : ToRectPhasor().GetTerms();
        }

        public double Norm2()
        {
            return Magnitude * Magnitude;
        }

        public GaPoTNumRectPhasor ToRectPhasor()
        {
            return new GaPoTNumRectPhasor(
                Id,
                Magnitude * Math.Cos(Phase),
                Magnitude * Math.Sin(Phase)
            );
        }

        public string ToText()
        {
            if (Magnitude == 0)
                return "0";

            var i1 = Id + 1;
            var i2 = Id + 2;

            return $"p({Magnitude:G}, {Phase:G}) <{i1},{i2}>";
        }

        public string ToLaTeX()
        {
            if (Magnitude == 0)
                return "0";

            var i1 = Id + 1;
            var i2 = Id + 2;

            var magnitudeText = GaPoTNumUtils.GetLaTeXNumber(Magnitude);
            var phaseText = GaPoTNumUtils.GetLaTeXNumber(Phase);
            var basisText1 = GaPoTNumUtils.GetLaTeXBasisName($"{i1},{i2}");
            var basisText2 = GaPoTNumUtils.GetLaTeXBasisName($"{i1}");

            if (Phase == 0)
                return $@"{magnitudeText} {basisText2}";

            return $@"{magnitudeText} e^{{ {phaseText} {basisText1} }} {basisText2}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}