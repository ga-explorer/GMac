using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CodeComposerLib.Irony;
using Irony.Parsing;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumSinglePhaseVector
    {
        public static GaPoTNumSinglePhaseVector operator -(GaPoTNumSinglePhaseVector v)
        {
            var result = new GaPoTNumSinglePhaseVector();

            foreach (var phasor in v._phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    -phasor.Magnitude,
                    phasor.Phase
                );

            return result;
        }

        public static GaPoTNumSinglePhaseVector operator +(GaPoTNumSinglePhaseVector v1, GaPoTNumSinglePhaseVector v2)
        {
            var result = new GaPoTNumSinglePhaseVector();

            foreach (var phasor in v1._phasorsDictionary.Values)
                result.AddPolarPhasor(
                    phasor.Id,
                    phasor.Magnitude,
                    phasor.Phase
                );

            foreach (var phasor in v2._phasorsDictionary.Values)
                result.AddPolarPhasor(
                    phasor.Id,
                    phasor.Magnitude,
                    phasor.Phase
                );

            return result;
        }

        public static GaPoTNumSinglePhaseVector operator -(GaPoTNumSinglePhaseVector v1, GaPoTNumSinglePhaseVector v2)
        {
            var result = new GaPoTNumSinglePhaseVector();

            foreach (var phasor in v1._phasorsDictionary.Values)
                result.AddPolarPhasor(
                    phasor.Id,
                    phasor.Magnitude,
                    phasor.Phase
                );

            foreach (var phasor in v2._phasorsDictionary.Values)
                result.AddPolarPhasor(
                    phasor.Id,
                    -phasor.Magnitude,
                    phasor.Phase
                );

            return result;
        }

        /// <summary>
        /// The geometric product of two single phase GAPoT vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static GaPoTNumSinglePhaseBivector operator *(GaPoTNumSinglePhaseVector v1, GaPoTNumSinglePhaseVector v2)
        {
            var bivector = new GaPoTNumSinglePhaseBivector();

            //if (v1.TryGetCommonScalingFactor(v2, out var scalingFactor))
            //{
            //    bivector.AddTerm(0, 0, scalingFactor * v1.Norm2());

            //    return bivector;
            //}

            foreach (var term1 in v1.GetTerms())
            {
                foreach (var term2 in v2.GetTerms())
                {
                    var scalarValue = term1.Value * term2.Value;

                    bivector.AddTerm(
                        term1.TermId, 
                        term2.TermId, 
                        scalarValue
                    );
                }
            }

            return bivector;
        }

        public static GaPoTNumSinglePhaseVector operator *(GaPoTNumSinglePhaseVector v, double s)
        {
            var result = new GaPoTNumSinglePhaseVector();

            foreach (var phasor in v._phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    s * phasor.Magnitude,
                    phasor.Phase
                );

            return result;
        }

        public static GaPoTNumSinglePhaseVector operator *(double s, GaPoTNumSinglePhaseVector v)
        {
            var result = new GaPoTNumSinglePhaseVector();

            foreach (var phasor in v._phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    s * phasor.Magnitude,
                    phasor.Phase
                );

            return result;
        }


        private readonly Dictionary<int, GaPoTNumPolarPhasor> _phasorsDictionary
            = new Dictionary<int, GaPoTNumPolarPhasor>();


        public int Count 
            => _phasorsDictionary.Count;


        public GaPoTNumSinglePhaseVector SetToZero()
        {
            _phasorsDictionary.Clear();

            return this;
        }


        public bool IsZero()
        {
            return Norm2().IsNearZero();
        }

        public bool TryGetCommonScalingFactor(GaPoTNumSinglePhaseVector v2, out double scalingFactor)
        {
            scalingFactor = 0.0d;

            var termsList1 = GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.TermId)
                .ToArray();

            var termsList2 = v2.GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.TermId)
                .ToArray();

            if (termsList1.Length != termsList2.Length)
                return false;

            if (termsList1.Length == 0)
                return false;

            var scalingFactorsList = new double[termsList1.Length];
            for (var i = 0; i < termsList1.Length; i++)
            {
                var term1 = termsList1[i];
                var term2 = termsList2[i];

                if (term1.TermId != term2.TermId)
                    return false;

                scalingFactorsList[i] = term2.Value / term1.Value;
            }

            var avgValue = 
                scalingFactorsList.Sum() / scalingFactorsList.Length;

            var stdValue = 
                Math.Sqrt(
                    scalingFactorsList.Select(v => (v - avgValue) * (v - avgValue)).Sum() / scalingFactorsList.Length
                );

            if (stdValue > 1e-7)
                return false;

            scalingFactor = avgValue;
            return true;
        }


        public GaPoTNumSinglePhaseVector AddTerm(int id, double value)
        {
            if (id % 2 == 0)
                AddRectPhasor(id, value, 0);
            else
                AddRectPhasor(id - 1, 0, -value);

            return this;
        }

        public GaPoTNumSinglePhaseVector AddPolarPhasor(int id, double magnitude, double phase)
        {
            var newPhasor = new GaPoTNumPolarPhasor(id, magnitude, phase);

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = oldPhasor + newPhasor;
            else
                _phasorsDictionary.Add(id, newPhasor);

            return this;
        }

        public GaPoTNumSinglePhaseVector AddRectPhasor(int id, double x, double y)
        {
            var newPhasor = new GaPoTNumRectPhasor(id, x, y);

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = (oldPhasor.ToRectPhasor() + newPhasor).ToPolarPhasor();
            else
                _phasorsDictionary.Add(id, newPhasor.ToPolarPhasor());

            return this;
        }

        public GaPoTNumSinglePhaseVector AddPolarPhasor(GaPoTNumPolarPhasor phasor)
        {
            var id = phasor.Id;

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = oldPhasor + phasor;
            else
                _phasorsDictionary.Add(id, phasor);

            return this;
        }

        public GaPoTNumSinglePhaseVector AddRectPhasor(GaPoTNumRectPhasor phasor)
        {
            var id = phasor.Id;

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = (oldPhasor.ToRectPhasor() + phasor).ToPolarPhasor();
            else
                _phasorsDictionary.Add(id, phasor.ToPolarPhasor());

            return this;
        }


        public GaPoTNumSinglePhaseVector SetPolarPhasor(int id, double magnitude, double phase)
        {
            var phasor = new GaPoTNumPolarPhasor(id, magnitude, phase);

            if (_phasorsDictionary.ContainsKey(id))
                _phasorsDictionary[id] = phasor;
            else
                _phasorsDictionary.Add(id, phasor);

            return this;
        }

        public GaPoTNumSinglePhaseVector SetRectPhasor(int id, double x, double y)
        {
            var phasor = new GaPoTNumRectPhasor(id, x, y).ToPolarPhasor();

            if (_phasorsDictionary.ContainsKey(id))
                _phasorsDictionary[id] = phasor;
            else
                _phasorsDictionary.Add(id, phasor);

            return this;
        }


        public GaPoTNumPolarPhasor GetPolarPhasor(int id)
        {
            return _phasorsDictionary.TryGetValue(id, out var phasor) 
                ? phasor 
                : new GaPoTNumPolarPhasor(id, 0, 0);
        }

        public GaPoTNumRectPhasor GetRectPhasor(int id)
        {
            return _phasorsDictionary.TryGetValue(id, out var phasor) 
                ? phasor.ToRectPhasor() 
                : new GaPoTNumRectPhasor(id, 0, 0);
        }


        public IEnumerable<GaPoTNumSinglePhaseVectorTerm> GetTerms()
        {
            return _phasorsDictionary
                .Values
                .SelectMany(v => v.GetTerms());
        }

        public IEnumerable<GaPoTNumPolarPhasor> GeTPolarPhasors()
        {
            return _phasorsDictionary.Values;
        }

        public IEnumerable<GaPoTNumRectPhasor> GeTRectPhasors()
        {
            return _phasorsDictionary.Values.Select(
                p => p.ToRectPhasor()
            );
        }


        public GaPoTNumSinglePhaseBivector Gp(GaPoTNumSinglePhaseVector v)
        {
            return this * v;
        }

        public GaPoTNumSinglePhaseVector Add(GaPoTNumSinglePhaseVector v)
        {
            return this + v;
        }

        public GaPoTNumSinglePhaseVector Subtract(GaPoTNumSinglePhaseVector v)
        {
            return this - v;
        }

        public GaPoTNumSinglePhaseVector Negative()
        {
            return -this;
        }

        public GaPoTNumSinglePhaseVector Reverse()
        {
            return this;
        }

        public double Norm2()
        {
            return _phasorsDictionary
                .Values
                .Select(p => p.Norm2())
                .Sum();
        }

        public GaPoTNumSinglePhaseVector Inverse()
        {
            var norm2 = Norm2();

            var result = new GaPoTNumSinglePhaseVector();

            if (norm2 == 0)
                throw new DivideByZeroException();

            var value = 1.0d / norm2;

            foreach (var phasor in _phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    phasor.Magnitude * value,
                    phasor.Phase
                );

            return result;
        }


        public string ToText()
        {
            return PolarPhasorsToText();
        }

        public string TermsToText()
        {
            var termsArray = 
                GetTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ");
        }

        public string PolarPhasorsToText()
        {
            var termsArray = 
                GeTPolarPhasors().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ");
        }

        public string RectPhasorsToText()
        {
            var termsArray = 
                GeTRectPhasors().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ");
        }


        public string ToLaTeX()
        {
            return PolarPhasorsToLaTeX();
        }

        public string TermsToLaTeX()
        {
            var termsArray = 
                GetTerms().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }

        public string PolarPhasorsToLaTeX()
        {
            var termsArray = 
                GeTPolarPhasors().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }

        public string RectPhasorsToLaTeX()
        {
            var termsArray = 
                GeTRectPhasors().ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToLaTeX()).Concatenate(" + ");
        }
 

        public override string ToString()
        {
            return ToText();
        }
    }
}
