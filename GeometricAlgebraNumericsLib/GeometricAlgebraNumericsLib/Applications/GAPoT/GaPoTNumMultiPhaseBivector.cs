using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Dictionary;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public sealed class GaPoTNumMultiPhaseBivector
    {
        private static readonly string _phaseVectorNames 
            = "abcdefghijklmnopqrstuvwxyz";


        private static string GetTextBasisName(Tuple<int, int> idTuple)
        {
            return $"<{_phaseVectorNames[idTuple.Item1]},{_phaseVectorNames[idTuple.Item2]}>";
        }

        private static string TermsToText(KeyValuePair<Tuple<int, int>, GaPoTNumSinglePhaseBivector> phaseData)
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
            return GaPoTNumUtils.GetLaTeXBasisName(
                _phaseVectorNames[idTuple.Item1] + "," + _phaseVectorNames[idTuple.Item2]
            );
        }

        private static string TermsToLaTeX(KeyValuePair<Tuple<int, int>, GaPoTNumSinglePhaseBivector> phaseData)
        {
            var idTuple = phaseData.Key;
            var spBivector = phaseData.Value;

            var spBivectorText = spBivector.TermsToLaTeX();
            var basisText = GetLaTeXBasisName(idTuple);

            if (idTuple.Item1 == idTuple.Item2)
                return $@"\left[ {spBivectorText} \right]";

            return $@"\left[ {spBivectorText} \right] {basisText}";
        }

        
        private readonly Dictionary2Keys<int, GaPoTNumSinglePhaseBivector> _phaseVectorsDictionary
            = new Dictionary2Keys<int, GaPoTNumSinglePhaseBivector>();


        public int Count 
            => _phaseVectorsDictionary.Count;


        public GaPoTNumSinglePhaseBivector GetPhaseBivector(int id1, int id2)
        {
            return _phaseVectorsDictionary.TryGetValue(id1, id2, out var vector)
                ? vector
                : new GaPoTNumSinglePhaseBivector();
        }


        public GaPoTNumMultiPhaseBivector AddPhaseBivector(int id1, int id2, GaPoTNumSinglePhaseBivector vector)
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

        public GaPoTNumMultiPhaseBivector SetPhaseBivector(int id1, int id2, GaPoTNumSinglePhaseBivector vector)
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


        public Dictionary2Keys<int, GaPoTNumSinglePhaseBivectorTerm[]> GetTerms()
        {
            var result = new Dictionary2Keys<int, GaPoTNumSinglePhaseBivectorTerm[]>();

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


        public GaPoTNumMultiPhaseBivector Reverse()
        {
            var result = new GaPoTNumMultiPhaseBivector();

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

        public double Norm2()
        {
            return _phaseVectorsDictionary
                .Values
                .Select(v => v.Norm2())
                .Sum();
        }

        public GaPoTNumMultiPhaseBivector PerPhaseInverse()
        {
            var result = new GaPoTNumMultiPhaseBivector();

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