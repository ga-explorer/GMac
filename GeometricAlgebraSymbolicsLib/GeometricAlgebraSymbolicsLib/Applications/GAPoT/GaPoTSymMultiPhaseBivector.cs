using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Dictionary;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymMultiPhaseBivector
    {
        private static readonly string _phaseVectorNames 
            = "abcdefghijklmnopqrstuvwxyz";


        private static string GetTextBasisName(Tuple<int, int> idTuple)
        {
            return $"{_phaseVectorNames[idTuple.Item1]},{_phaseVectorNames[idTuple.Item2]}";
        }

        private static string TermsToText(KeyValuePair<Tuple<int, int>, GaPoTSymSinglePhaseBivector> phaseData)
        {
            var idTuple = phaseData.Key;
            var spBivector = phaseData.Value;

            var spBivectorText = spBivector.TermsToText();
            var basisText = GetTextBasisName(idTuple);

            if (idTuple.Item1 == idTuple.Item2)
                return $@"[{spBivectorText}]";

            return $@"[{spBivectorText}] {basisText}";
        }


        private static string GetLaTeXBasisName(Tuple<int, int> idTuple)
        {
            return (_phaseVectorNames[idTuple.Item1] + "," + _phaseVectorNames[idTuple.Item2]).GetLaTeXBasisName();
        }

        private static string TermsToLaTeX(KeyValuePair<Tuple<int, int>, GaPoTSymSinglePhaseBivector> phaseData)
        {
            var idTuple = phaseData.Key;
            var spBivector = phaseData.Value;

            var spBivectorText = spBivector.TermsToLaTeX();
            var basisText = GetLaTeXBasisName(idTuple);

            if (idTuple.Item1 == idTuple.Item2)
                return $@"\left[ {spBivectorText} \right]";

            return $@"\left[ {spBivectorText} \right] {basisText}";
        }

        
        private readonly Dictionary2Keys<int, GaPoTSymSinglePhaseBivector> _phaseVectorsDictionary
            = new Dictionary2Keys<int, GaPoTSymSinglePhaseBivector>();


        public int Count 
            => _phaseVectorsDictionary.Count;


        public GaPoTSymSinglePhaseBivector GetPhaseBivector(int id1, int id2)
        {
            return _phaseVectorsDictionary.TryGetValue(id1, id2, out var vector)
                ? vector
                : new GaPoTSymSinglePhaseBivector();
        }


        public GaPoTSymMultiPhaseBivector AddPhaseBivector(int id1, int id2, GaPoTSymSinglePhaseBivector vector)
        {
            if (id1 == id2)
            {
                id1 = 0;
                id2 = 0;
            }
            else if (id1 > id2)
            {
                var s = id1;
                id1 = id2;
                id2 = s;
                vector = -vector;
            }

            if (_phaseVectorsDictionary.TryGetValue(id1, id2, out var oldVector))
                _phaseVectorsDictionary[id1, id2] = oldVector + vector;
            else
                _phaseVectorsDictionary.Add(id1, id2, vector);

            return this;
        }

        public GaPoTSymMultiPhaseBivector SetPhaseBivector(int id1, int id2, GaPoTSymSinglePhaseBivector vector)
        {
            if (id1 == id2)
            {
                id1 = 0;
                id2 = 0;
            }
            else if (id1 > id2)
            {
                var s = id1;
                id1 = id2;
                id2 = s;
                vector = -vector;
            }

            if (_phaseVectorsDictionary.ContainsKey(id1, id2))
                _phaseVectorsDictionary[id1, id2] = vector;
            else
                _phaseVectorsDictionary.Add(id1, id2, vector);

            return this;
        }


        public Dictionary2Keys<int, GaPoTSymSinglePhaseBivectorTerm[]> GetTerms()
        {
            var result = new Dictionary2Keys<int, GaPoTSymSinglePhaseBivectorTerm[]>();

            foreach (var pair in _phaseVectorsDictionary)
            {
                var id1 = pair.Key.Item1;
                var id2 = pair.Key.Item2;
                var termsArray = pair.Value.GetTerms().ToArray();

                if (termsArray.Length > 0)
                    result.Add(id1, id2, termsArray);
            }

            return result;
        }


        public GaPoTSymMultiPhaseBivector Reverse()
        {
            var result = new GaPoTSymMultiPhaseBivector();

            foreach (var pair in _phaseVectorsDictionary)
            {
                if (pair.Key.Item1 == pair.Key.Item2)
                    result.AddPhaseBivector(0, 0, pair.Value);
                else
                    result.AddPhaseBivector(
                        pair.Key.Item1, 
                        pair.Key.Item2, 
                        pair.Value.NegativeReverse()
                    );
            }

            return result;
        }

        public Expr Norm2()
        {
            return Mfs.SumExpr(
                _phaseVectorsDictionary
                .Values
                .Select(v => v.Norm2())
                .ToArray()
            );
        }

        public GaPoTSymMultiPhaseBivector PerPhaseInverse()
        {
            var result = new GaPoTSymMultiPhaseBivector();

            foreach (var pair in _phaseVectorsDictionary)
            {
                result.AddPhaseBivector(
                    pair.Key.Item1, 
                    pair.Key.Item2, 
                    pair.Value.Inverse()
                );
            }

            return result;
        }


        public string ToText()
        {
            return TermsToText();
        }

        public string TermsToText()
        {
            return _phaseVectorsDictionary
                .Select(TermsToText)
                .Concatenate("; ");
        }


        public string ToLaTeX()
        {
            return TermsToLaTeX();
        }

        public string TermsToLaTeX()
        {
            return _phaseVectorsDictionary
                .Select(TermsToLaTeX)
                .Concatenate(" + ");
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}