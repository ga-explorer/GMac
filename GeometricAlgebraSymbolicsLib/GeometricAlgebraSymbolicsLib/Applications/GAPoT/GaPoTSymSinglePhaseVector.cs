using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymSinglePhaseVector
    {
        public static GaPoTSymSinglePhaseVector operator -(GaPoTSymSinglePhaseVector v)
        {
            var result = new GaPoTSymSinglePhaseVector();

            foreach (var phasor in v._phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    Mfs.Minus[phasor.Magnitude].GaPoTSymSimplify(),
                    phasor.Phase
                );

            return result;
        }

        public static GaPoTSymSinglePhaseVector operator +(GaPoTSymSinglePhaseVector v1, GaPoTSymSinglePhaseVector v2)
        {
            var result = new GaPoTSymSinglePhaseVector();

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

        public static GaPoTSymSinglePhaseVector operator -(GaPoTSymSinglePhaseVector v1, GaPoTSymSinglePhaseVector v2)
        {
            var result = new GaPoTSymSinglePhaseVector();

            foreach (var phasor in v1._phasorsDictionary.Values)
                result.AddPolarPhasor(
                    phasor.Id,
                    phasor.Magnitude,
                    phasor.Phase
                );

            foreach (var phasor in v2._phasorsDictionary.Values)
                result.AddPolarPhasor(
                    phasor.Id,
                    Mfs.Minus[phasor.Magnitude].GaPoTSymSimplify(),
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
        public static GaPoTSymSinglePhaseBivector operator *(GaPoTSymSinglePhaseVector v1, GaPoTSymSinglePhaseVector v2)
        {
            var bivector = new GaPoTSymSinglePhaseBivector();

            //if (v1.TryGetCommonScalingFactor(v2, out var scalingFactor))
            //{
            //    var value = 
            //        Mfs.Times[scalingFactor, v1.Norm2()].GaPoTSymSimplify();

            //    bivector.AddTerm(0, 0, value);

            //    return bivector;
            //}

            foreach (var term1 in v1.GetTerms())
            {
                foreach (var term2 in v2.GetTerms())
                {
                    var scalarValue = 
                        Mfs.Times[term1.Value, term2.Value].GaPoTSymSimplify();

                    bivector.AddTerm(
                        term1.TermId, 
                        term2.TermId, 
                        scalarValue
                    );
                }
            }

            return bivector;
        }

        public static GaPoTSymSinglePhaseVector operator *(GaPoTSymSinglePhaseVector v, Expr s)
        {
            var result = new GaPoTSymSinglePhaseVector();

            foreach (var phasor in v._phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    Mfs.Times[s, phasor.Magnitude].GaPoTSymSimplify(),
                    phasor.Phase
                );

            return result;
        }

        public static GaPoTSymSinglePhaseVector operator *(Expr s, GaPoTSymSinglePhaseVector v)
        {
            var result = new GaPoTSymSinglePhaseVector();

            foreach (var phasor in v._phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    Mfs.Times[s, phasor.Magnitude].GaPoTSymSimplify(),
                    phasor.Phase
                );

            return result;
        }


        private readonly Dictionary<int, GaPoTSymPolarPhasor> _phasorsDictionary
            = new Dictionary<int, GaPoTSymPolarPhasor>();


        public int Count 
            => _phasorsDictionary.Count;


        public GaPoTSymSinglePhaseVector SetToZero()
        {
            _phasorsDictionary.Clear();

            return this;
        }


        public bool IsZero()
        {
            return Norm2().IsZero();
        }

        //public bool TryGetCommonScalingFactor(GaPoTSymSinglePhaseVector v2, out Expr scalingFactor)
        //{
        //    scalingFactor = 0.0d;

        //    var termsList1 = GetTerms()
        //        .Where(t => !GaNumericsUtils.IsNearZero(t.Value))
        //        .OrderBy(t => t.TermId)
        //        .ToArray();

        //    var termsList2 = v2.GetTerms()
        //        .Where(t => !t.Value.IsNearZero())
        //        .OrderBy(t => t.TermId)
        //        .ToArray();

        //    if (termsList1.Length != termsList2.Length)
        //        return false;

        //    if (termsList1.Length == 0)
        //        return false;

        //    var scalingFactorsList = new Expr[termsList1.Length];
        //    for (var i = 0; i < termsList1.Length; i++)
        //    {
        //        var term1 = termsList1[i];
        //        var term2 = termsList2[i];

        //        if (term1.TermId != term2.TermId)
        //            return false;

        //        scalingFactorsList[i] = term2.Value / term1.Value;
        //    }

        //    var avgValue = 
        //        scalingFactorsList.Sum() / scalingFactorsList.Length;

        //    var stdValue = 
        //        Math.Sqrt(
        //            scalingFactorsList.Select(v => (v - avgValue) * (v - avgValue)).Sum() / scalingFactorsList.Length
        //        );

        //    if (stdValue > 1e-7)
        //        return false;

        //    scalingFactor = avgValue;
        //    return true;
        //}


        public GaPoTSymSinglePhaseVector AddTerm(int id, Expr value)
        {
            if (id % 2 == 0)
                AddRectPhasor(id, value, 0.ToExpr());
            else
                AddRectPhasor(id - 1, 0.ToExpr(), Mfs.Minus[value].GaPoTSymSimplify());

            return this;
        }

        public GaPoTSymSinglePhaseVector AddPolarPhasor(int id, Expr magnitude, Expr phase)
        {
            var newPhasor = new GaPoTSymPolarPhasor(id, magnitude, phase);

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = oldPhasor + newPhasor;
            else
                _phasorsDictionary.Add(id, newPhasor);

            return this;
        }

        public GaPoTSymSinglePhaseVector AddRectPhasor(int id, Expr x, Expr y)
        {
            var newPhasor = new GaPoTSymRectPhasor(id, x, y);

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = (oldPhasor.ToRectPhasor() + newPhasor).ToPolarPhasor();
            else
                _phasorsDictionary.Add(id, newPhasor.ToPolarPhasor());

            return this;
        }

        public GaPoTSymSinglePhaseVector AddPolarPhasor(GaPoTSymPolarPhasor phasor)
        {
            var id = phasor.Id;

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = oldPhasor + phasor;
            else
                _phasorsDictionary.Add(id, phasor);

            return this;
        }

        public GaPoTSymSinglePhaseVector AddRectPhasor(GaPoTSymRectPhasor phasor)
        {
            var id = phasor.Id;

            if (_phasorsDictionary.TryGetValue(id, out var oldPhasor))
                _phasorsDictionary[id] = (oldPhasor.ToRectPhasor() + phasor).ToPolarPhasor();
            else
                _phasorsDictionary.Add(id, phasor.ToPolarPhasor());

            return this;
        }


        public GaPoTSymSinglePhaseVector SetPolarPhasor(int id, Expr magnitude, Expr phase)
        {
            var phasor = new GaPoTSymPolarPhasor(id, magnitude, phase);

            if (_phasorsDictionary.ContainsKey(id))
                _phasorsDictionary[id] = phasor;
            else
                _phasorsDictionary.Add(id, phasor);

            return this;
        }

        public GaPoTSymSinglePhaseVector SetRectPhasor(int id, Expr x, Expr y)
        {
            var phasor = new GaPoTSymRectPhasor(id, x, y).ToPolarPhasor();

            if (_phasorsDictionary.ContainsKey(id))
                _phasorsDictionary[id] = phasor;
            else
                _phasorsDictionary.Add(id, phasor);

            return this;
        }


        public GaPoTSymPolarPhasor GetPolarPhasor(int id)
        {
            return _phasorsDictionary.TryGetValue(id, out var phasor) 
                ? phasor 
                : new GaPoTSymPolarPhasor(id, 0.ToExpr(), 0.ToExpr());
        }

        public GaPoTSymRectPhasor GetRectPhasor(int id)
        {
            return _phasorsDictionary.TryGetValue(id, out var phasor) 
                ? phasor.ToRectPhasor() 
                : new GaPoTSymRectPhasor(id, 0.ToExpr(), 0.ToExpr());
        }


        public IEnumerable<GaPoTSymSinglePhaseVectorTerm> GetTerms()
        {
            return _phasorsDictionary
                .Values
                .SelectMany(v => v.GetTerms());
        }

        public IEnumerable<GaPoTSymPolarPhasor> GeTPolarPhasors()
        {
            return _phasorsDictionary.Values;
        }

        public IEnumerable<GaPoTSymRectPhasor> GeTRectPhasors()
        {
            return _phasorsDictionary.Values.Select(
                p => p.ToRectPhasor()
            );
        }


        public GaPoTSymSinglePhaseBivector Gp(GaPoTSymSinglePhaseVector v)
        {
            return this * v;
        }

        public GaPoTSymSinglePhaseVector Add(GaPoTSymSinglePhaseVector v)
        {
            return this + v;
        }

        public GaPoTSymSinglePhaseVector Subtract(GaPoTSymSinglePhaseVector v)
        {
            return this - v;
        }

        public GaPoTSymSinglePhaseVector Negative()
        {
            return -this;
        }

        public GaPoTSymSinglePhaseVector Reverse()
        {
            return this;
        }

        public Expr Norm2()
        {
            return Mfs.SumExpr(
                _phasorsDictionary
                .Values
                .Select(p => p.Norm2())
                .ToArray()
            ).GaPoTSymSimplify();
        }

        public GaPoTSymSinglePhaseVector Inverse()
        {
            var norm2 = Norm2();

            var result = new GaPoTSymSinglePhaseVector();

            if (norm2.IsZero())
                throw new DivideByZeroException();

            foreach (var phasor in _phasorsDictionary.Values)
                result.SetPolarPhasor(
                    phasor.Id,
                    Mfs.Divide[phasor.Magnitude, norm2].GaPoTSymSimplify(),
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