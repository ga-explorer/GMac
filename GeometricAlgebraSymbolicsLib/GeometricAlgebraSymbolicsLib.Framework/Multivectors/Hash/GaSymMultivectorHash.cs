using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using TextComposerLib.Text.Structured;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors.Hash
{
    public sealed class GaSymMultivectorHash : ISymbolicVector, IGaSymMultivector
    {
        public static GaSymMultivectorHash CreateTerm(int gaSpaceDim, int id, MathematicaScalar coef)
        {
            return new GaSymMultivectorHash(gaSpaceDim) {[id] = coef.Expression};
        }

        public static GaSymMultivectorHash CreateBasisBlade(int gaSpaceDim, int id)
        {
            return new GaSymMultivectorHash(gaSpaceDim) {[id] = Expr.INT_ONE};
        }

        public static GaSymMultivectorHash CreateScalar(int gaSpaceDim, MathematicaScalar coef)
        {
            return new GaSymMultivectorHash(gaSpaceDim) {[0] = coef.Expression};
        }

        public static GaSymMultivectorHash CreatePseudoscalar(int gaSpaceDim, MathematicaScalar coef)
        {
            return new GaSymMultivectorHash(gaSpaceDim) {[gaSpaceDim - 1] = coef.Expression};
        }

        public static GaSymMultivectorHash CreateZero(int gaSpaceDim)
        {
            return new GaSymMultivectorHash(gaSpaceDim);
        }

        public static GaSymMultivectorHash CreateCopy(GaSymMultivectorHash mv)
        {
            var resultMv = new GaSymMultivectorHash(mv.GaSpaceDimension);

            foreach (var term in mv)
                resultMv.Add(term.Key, term.Value);

            return resultMv;
        }

        public static GaSymMultivectorHash CreateMapped(GaSymMultivectorHash mv, Func<MathematicaScalar, MathematicaScalar> scalarMap)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv[term.Key] = scalarMap(term.Value).Expression;

            return resultMv;
        }

        public static GaSymMultivectorHash CreateSymbolic(int gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolic(
                gaSpaceDim, 
                baseCoefName, 
                Enumerable.Range(0, gaSpaceDim)
                );
        }

        public static GaSymMultivectorHash CreateSymbolicVector(int gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolic(
                gaSpaceDim,
                baseCoefName,
                GaFrameUtils.BasisBladeIDsOfGrade(gaSpaceDim.ToVSpaceDimension(), 1)
            );
        }

        public static GaSymMultivectorHash CreateSymbolicKVector(int gaSpaceDim, string baseCoefName, int grade)
        {
            return CreateSymbolic(
                gaSpaceDim,
                baseCoefName,
                GaFrameUtils.BasisBladeIDsOfGrade(gaSpaceDim.ToVSpaceDimension(), grade)
            );
        }

        public static GaSymMultivectorHash CreateSymbolicTerm(int gaSpaceDim, string baseCoefName, int id)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            return new GaSymMultivectorHash(gaSpaceDim)
            {
                [id] = MathematicaScalar.CreateSymbol(
                    GaSymbolicsUtils.Cas,
                    baseCoefName + id.PatternToString(vSpaceDim)
                ).Expression
            };
        }

        public static GaSymMultivectorHash CreateSymbolicScalar(int gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolicTerm(gaSpaceDim, baseCoefName, 0);
        }

        public static GaSymMultivectorHash CreateSymbolicPseudoscalar(int gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolicTerm(gaSpaceDim, baseCoefName, gaSpaceDim - 1);
        }

        public static GaSymMultivectorHash CreateSymbolic(int gaSpaceDim, string baseCoefName, IEnumerable<int> idsList)
        {
            var resultMv = new GaSymMultivectorHash(gaSpaceDim);
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            foreach (var id in idsList)
                resultMv.Add(
                    id,
                    MathematicaScalar.CreateSymbol(
                        GaSymbolicsUtils.Cas,
                        baseCoefName + id.PatternToString(vSpaceDim)
                    )
                );

            return resultMv;
        }


        public static GaSymMultivectorHash operator -(GaSymMultivectorHash mv)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.Add(term.Key, -term.Value);

            return resultMv;
        }

        public static GaSymMultivectorHash operator +(GaSymMultivectorHash mv1, GaSymMultivectorHash mv2)
        {
            var resultMv = GaSymMultivector.CreateCopyTemp(
                mv1.GaSpaceDimension, 
                mv1.NonZeroExprTerms
            );

            foreach (var term in mv2.NonZeroExprTerms)
                resultMv.AddFactor(term.Key, term.Value);

            return resultMv.ToHashMultivector();
        }

        public static GaSymMultivectorHash operator -(GaSymMultivectorHash mv1, GaSymMultivectorHash mv2)
        {
            var resultMv = GaSymMultivector.CreateCopyTemp(
                mv1.GaSpaceDimension, 
                mv1.NonZeroExprTerms
            );

            foreach (var term in mv2.NonZeroExprTerms)
                resultMv.AddFactor(term.Key, true, term.Value);

            return resultMv.ToHashMultivector();
        }

        public static GaSymMultivectorHash operator *(GaSymMultivectorHash mv1, MathematicaScalar s)
        {
            return s.IsNullOrZero()
                ? CreateZero(mv1.GaSpaceDimension)
                : GaSymMultivector
                    .CreateZeroTemp(mv1.GaSpaceDimension)
                    .AddFactors(s.Expression, mv1)
                    .ToHashMultivector();
        }

        public static GaSymMultivectorHash operator *(MathematicaScalar s, GaSymMultivectorHash mv1)
        {
            return s.IsNullOrZero()
                ? CreateZero(mv1.GaSpaceDimension)
                : GaSymMultivector
                    .CreateZeroTemp(mv1.GaSpaceDimension)
                    .AddFactors(s.Expression, mv1)
                    .ToHashMultivector();
        }

        public static GaSymMultivectorHash operator /(GaSymMultivectorHash mv1, MathematicaScalar s)
        {
            var sInv = GaSymbolicsUtils.Constants.One / s;

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(sInv.Expression, mv1)
                .ToHashMultivector();
        }


        private Dictionary<int, Expr> _internalDictionary =
            new Dictionary<int, Expr>();


        public IEnumerable<int> BasisBladeIds 
            => _internalDictionary.Keys;

        public IEnumerable<int> NonZeroBasisBladeIds
            => _internalDictionary
                .Where(p => !p.Value.IsNullOrZero())
                .Select(p => p.Key);

        public IEnumerable<MathematicaScalar> BasisBladeScalars
            => _internalDictionary.Select(p => p.Value.ToMathematicaScalar());

        public IEnumerable<Expr> BasisBladeExprScalars
            => _internalDictionary.Values;

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars
            => _internalDictionary
                .Where(p => !p.Value.IsNullOrZero())
                .Select(p => p.Value.ToMathematicaScalar());

        public IEnumerable<Expr> NonZeroBasisBladeExprScalars
            => _internalDictionary
                .Where(p => !p.Value.IsNullOrZero())
                .Select(p => p.Value);

        public int VSpaceDimension { get; }

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection 
            => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator 
            => CasInterface.Evaluator;

        public MathematicaConstants CasConstants 
            => CasInterface.Constants;

        public int Size 
            => GaSpaceDimension;

        MathematicaScalar ISymbolicVector.this[int index]
        {
            get
            {
                _internalDictionary.TryGetValue(index, out var value);

                return ReferenceEquals(value, null)
                    ? GaSymbolicsUtils.Constants.Zero
                    : value.ToMathematicaScalar();
            }
        }

        public Expr this[int grade, int index]
        {
            get
            {
                return this[GaFrameUtils.BasisBladeId(grade, index)];
            }
            set
            {
                this[GaFrameUtils.BasisBladeId(grade, index)] = value;
            }
        }

        public Expr this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index < GaSpaceDimension);

                return 
                    _internalDictionary.TryGetValue(index, out var value) 
                    ? value
                    : Expr.INT_ZERO;
            }

            set
            {
                Debug.Assert(index >= 0 && index < GaSpaceDimension);

                if (value.IsNullOrZero())
                {
                    _internalDictionary.Remove(index);

                    return;
                }

                if (_internalDictionary.ContainsKey(index))
                    _internalDictionary[index] = value;

                else
                    _internalDictionary.Add(index, value);
            }
        }

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> Terms
            => _internalDictionary
                .Select(
                    p => new KeyValuePair<int, MathematicaScalar>(
                        p.Key, 
                        p.Value.ToMathematicaScalar()
                    )
                );

        public IEnumerable<KeyValuePair<int, Expr>> ExprTerms
            => _internalDictionary;

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> NonZeroTerms
            => _internalDictionary
                .Where(p => !p.Value.IsNullOrZero())
                .Select(
                    p => new KeyValuePair<int, MathematicaScalar>(
                        p.Key,
                        p.Value.ToMathematicaScalar()
                    )
                );

        public IEnumerable<KeyValuePair<int, Expr>> NonZeroExprTerms 
            => _internalDictionary
                .Where(p => !p.Value.IsNullOrZero());


        private GaSymMultivectorHash(int gaSpaceDim)
        {
            CasInterface = GaSymbolicsUtils.Cas;
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
        }


        public bool IsTerm()
        {
            return NonZeroExprTerms.Count() <= 1;
        }

        public bool IsScalar()
        {
            return _internalDictionary.Count == 0 ||
                   _internalDictionary.All(pair => pair.Key != 0 && pair.Value.IsNullOrZero());
        }

        public bool IsZero()
        {
            return _internalDictionary.Count == 0 ||
                   _internalDictionary.All(pair => pair.Value.IsNullOrZero());
        }

        public bool IsEqualZero()
        {
            return _internalDictionary.Count == 0 ||
                   BasisBladeScalars.All(s => s.IsNullOrEqualZero());
        }

        public Dictionary<int, GaSymMultivectorHash> ToKVectors()
        {
            var kvectorsList = new Dictionary<int, GaSymMultivectorHash>();

            foreach (var pair in _internalDictionary)
            {
                var grade = pair.Key.BasisBladeGrade();

                if (kvectorsList.TryGetValue(grade, out var mv) == false)
                {
                    mv = new GaSymMultivectorHash(GaSpaceDimension);

                    kvectorsList.Add(grade, mv);
                }

                mv.Add(pair.Key, pair.Value);
            }

            return kvectorsList;
        }


        public bool IsFullVector()
        {
            return false;
        }

        public bool IsSparseVector()
        {
            return true;
        }

        public MathematicaVector ToMathematicaVector()
        {
            throw new NotImplementedException();
        }

        public MathematicaVector ToMathematicaFullVector()
        {
            throw new NotImplementedException();
        }

        public MathematicaVector ToMathematicaSparseVector()
        {
            throw new NotImplementedException();
        }


        public void Add(int key, MathematicaScalar value)
        {
            if (!(key >= 0 && key < GaSpaceDimension))
                throw new IndexOutOfRangeException();

            if (ReferenceEquals(value, null))
                throw new ArgumentNullException();

            if (value.IsZero() == false)
                _internalDictionary.Add(key, value.Expression);
        }

        private void Add(int key, Expr value)
        {
            if (!(key >= 0 && key < GaSpaceDimension))
                throw new IndexOutOfRangeException();

            if (ReferenceEquals(value, null))
                throw new ArgumentNullException();

            if (value.IsNullOrZero() == false)
                _internalDictionary.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return _internalDictionary.ContainsKey(key);
        }

        public ICollection<int> Keys => _internalDictionary.Keys;

        public bool Remove(int key)
        {
            return _internalDictionary.Remove(key);
        }

        public bool TryGetValue(int key, out MathematicaScalar value)
        {
            if (_internalDictionary.TryGetValue(key, out var expr))
            {
                value = expr.ToMathematicaScalar();
                return true;
            }

            value = null;
            return false;
        }

        public void Add(KeyValuePair<int, MathematicaScalar> item)
        {
            if (!(item.Key >= 0 && item.Key < GaSpaceDimension))
                throw new IndexOutOfRangeException();

            if (ReferenceEquals(item.Value, null))
                throw new ArgumentNullException();

            if (item.Value.IsZero() == false)
                _internalDictionary.Add(item.Key, item.Value.Expression);
        }

        public void Clear()
        {
            _internalDictionary.Clear();
        }

        public bool Contains(KeyValuePair<int, MathematicaScalar> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<int, MathematicaScalar>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count => _internalDictionary.Count;

        public bool IsReadOnly => false;

        public IEnumerator<KeyValuePair<int, Expr>> GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }


        public GaSymMultivectorHash Reverse()
        {
            var resultMv = new GaSymMultivectorHash(GaSpaceDimension);

            foreach (var term in _internalDictionary)
                resultMv._internalDictionary.Add(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeReverse() 
                        ? Mfs.Minus[term.Value] 
                        : term.Value
                    );

            return resultMv;
        }

        public GaSymMultivectorHash GradeInv()
        {
            var resultMv = new GaSymMultivectorHash(GaSpaceDimension);

            foreach (var term in _internalDictionary)
                resultMv._internalDictionary.Add(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeGradeInv() 
                        ? Mfs.Minus[term.Value] 
                        : term.Value
                    );

            return resultMv;
        }

        public GaSymMultivectorHash CliffConj()
        {
            var resultMv = new GaSymMultivectorHash(GaSpaceDimension);

            foreach (var term in _internalDictionary)
                resultMv._internalDictionary.Add(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeCliffConj() 
                        ? Mfs.Minus[term.Value] 
                        : term.Value
                    );

            return resultMv;
        }

        public bool ContainsBasisBlade(int id)
        {
            return _internalDictionary.ContainsKey(id);
        }

        public bool IsTemp 
            => false;

        public int TermsCount 
            => _internalDictionary.Count;

        public void Simplify()
        {
            var newDict = new Dictionary<int, Expr>();

            foreach (var term in _internalDictionary)
            {
                var expr = term.Value.Simplify(CasInterface);

                if (!expr.IsZero())
                    newDict.Add(term.Key, expr);
            }

            _internalDictionary = newDict;
        }

        public MathematicaScalar[] TermsToArray()
        {
            var scalarsArray = new MathematicaScalar[GaSpaceDimension];

            foreach (var term in NonZeroTerms)
                scalarsArray[term.Key] = term.Value;

            return scalarsArray;
        }

        public Expr[] TermsToExprArray()
        {
            var scalarsArray = new Expr[GaSpaceDimension];

            foreach (var term in NonZeroExprTerms)
                scalarsArray[term.Key] = term.Value;

            return scalarsArray;
        }

        public GaSymMultivector ToMultivector()
        {
            return GaSymMultivector.CreateCopy(GaSpaceDimension, NonZeroExprTerms);
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            foreach (var id in GaFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public ISymbolicVector Times(ISymbolicMatrix m)
        {
            throw new NotImplementedException();
        }

        IEnumerator<MathematicaScalar> IEnumerable<MathematicaScalar>.GetEnumerator()
        {
            return BasisBladeScalars.GetEnumerator();
        }

        public override string ToString()
        {
            var composer = new ListTextComposer(" + ");

            foreach (var pair in _internalDictionary.OrderBy(p => p.Key))
            {
                composer.Add(
                    pair.Value + " " + pair.Key.BasisBladeName()
                    );
            }

            return composer.ToString();
        }
    }
}
