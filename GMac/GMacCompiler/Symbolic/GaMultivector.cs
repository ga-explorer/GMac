using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic
{
    public sealed class GaMultivector : ISymbolicVector, IDictionary<int, MathematicaScalar>
    {
        public static GaMultivector CreateTerm(int gaspacedim, int id, MathematicaScalar coef)
        {
            var resultMv = new GaMultivector(gaspacedim) {[id] = coef};


            return resultMv;
        }

        public static GaMultivector CreateBasisBlade(int gaspacedim, int id)
        {
            var resultMv = new GaMultivector(gaspacedim) {[id] = SymbolicUtils.Constants.One};


            return resultMv;
        }

        public static GaMultivector CreateScalar(int gaspacedim, MathematicaScalar coef)
        {
            var resultMv = new GaMultivector(gaspacedim) {[0] = coef};


            return resultMv;
        }

        public static GaMultivector CreatePseudoScalar(int gaspacedim, MathematicaScalar coef)
        {
            var resultMv = new GaMultivector(gaspacedim) {[gaspacedim - 1] = coef};


            return resultMv;
        }

        public static GaMultivector CreateZero(int gaspacedim)
        {
            return new GaMultivector(gaspacedim);
        }

        public static GaMultivector CreateCopy(GaMultivector mv)
        {
            var resultMv = new GaMultivector(mv.GaSpaceDim);

            foreach (var term in mv)
                resultMv.Add(term.Key, term.Value);

            return resultMv;
        }


        public static GaMultivector operator -(GaMultivector mv)
        {
            var resultMv = CreateZero(mv.GaSpaceDim);

            foreach (var term in mv)
                resultMv.Add(term.Key, -term.Value);

            return resultMv;
        }

        public static GaMultivector operator +(GaMultivector mv1, GaMultivector mv2)
        {
            var resultMv = CreateCopy(mv1);

            foreach (var term in mv2)
                resultMv[term.Key] += term.Value;

            return resultMv;
        }

        public static GaMultivector operator -(GaMultivector mv1, GaMultivector mv2)
        {
            var resultMv = CreateCopy(mv1);

            foreach (var term in mv2)
                resultMv[term.Key] -= term.Value;

            return resultMv;
        }

        public static GaMultivector operator *(GaMultivector mv1, MathematicaScalar s)
        {
            var resultMv = CreateZero(mv1.GaSpaceDim);

            foreach (var term in mv1)
                resultMv[term.Key] += term.Value * s;

            return resultMv;
        }

        public static GaMultivector operator *(MathematicaScalar s, GaMultivector mv1)
        {
            var resultMv = CreateZero(mv1.GaSpaceDim);

            foreach (var term in mv1)
                resultMv[term.Key] += term.Value * s;

            return resultMv;
        }

        public static GaMultivector operator /(GaMultivector mv1, MathematicaScalar s)
        {
            var resultMv = CreateZero(mv1.GaSpaceDim);

            foreach (var term in mv1)
                resultMv[term.Key] += term.Value / s;

            return resultMv;
        }


        private readonly Dictionary<int, MathematicaScalar> _internalDictionary =
            new Dictionary<int, MathematicaScalar>();

        public int GaSpaceDim { get; }

        public int VSpaceDim => FrameUtils.VSpaceDimension(GaSpaceDim);

        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;

        public int Size => GaSpaceDim;

        public MathematicaScalar this[int grade, int index]
        {
            get
            {
                return this[FrameUtils.BasisBladeId(grade, index)];
            }
            set
            {
                this[FrameUtils.BasisBladeId(grade, index)] = value;
            }
        }

        public MathematicaScalar this[int index]
        {
            get
            {
                if (!(index >= 0 && index < GaSpaceDim))
                    throw new IndexOutOfRangeException();

                MathematicaScalar value;

                return 
                    _internalDictionary.TryGetValue(index, out value) 
                    ? value 
                    : CasConstants.Zero;
            }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentNullException();

                if (!(index >= 0 && index < GaSpaceDim))
                    throw new IndexOutOfRangeException();
                
                if (value.IsZero())
                {
                    if (_internalDictionary.ContainsKey(index))
                        _internalDictionary.Remove(index);
                }
                else
                {
                    if (_internalDictionary.ContainsKey(index))
                        _internalDictionary[index] = value;
                    else
                        _internalDictionary.Add(index, value);
                }
            }
        }


        private GaMultivector(int gaspacedim)
        {
            CasInterface = SymbolicUtils.Cas;
            GaSpaceDim = gaspacedim;
        }


        //public IEnumerable<KeyValuePair<int, MathematicaScalar>> FilterTermsForOp(int id1)
        //{
        //    return _internalDictionary.Where(pair => (id1 & pair.Key) == 0);
        //}

        //public IEnumerable<KeyValuePair<int, MathematicaScalar>> FilterTermsUsing(int id1, Func<int, int, bool> termDiscardFunction)
        //{
        //    return _internalDictionary.Where(pair => termDiscardFunction(id1, pair.Key) == false);
        //}

        public KeyValuePair<int, MathematicaScalar> GetTerm(int id)
        {
            return new KeyValuePair<int, MathematicaScalar>(id, this[id]);
        }

        public bool IsScalar()
        {
            return _internalDictionary.Count == 0 || (_internalDictionary.Count == 1 && _internalDictionary.ContainsKey(0));
        }

        public Dictionary<int, GaMultivector> ToKVectors()
        {
            var kvectorsList = new Dictionary<int, GaMultivector>();

            foreach (var pair in _internalDictionary)
            {
                GaMultivector mv;
                
                var grade = pair.Key.BasisBladeGrade();

                if (kvectorsList.TryGetValue(grade, out mv) == false)
                {
                    mv = new GaMultivector(GaSpaceDim);

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
            if (!(key >= 0 && key < GaSpaceDim))
                throw new IndexOutOfRangeException();

            if (ReferenceEquals(value, null))
                throw new ArgumentNullException();

            if (value.IsZero() == false)
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
            return _internalDictionary.TryGetValue(key, out value);
        }

        public ICollection<MathematicaScalar> Values => _internalDictionary.Values;

        public void Add(KeyValuePair<int, MathematicaScalar> item)
        {
            if (!(item.Key >= 0 && item.Key < GaSpaceDim))
                throw new IndexOutOfRangeException();

            if (ReferenceEquals(item.Value, null))
                throw new ArgumentNullException();

            if (item.Value.IsZero() == false)
                _internalDictionary.Add(item.Key, item.Value);
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

        public bool Remove(KeyValuePair<int, MathematicaScalar> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, MathematicaScalar>> GetEnumerator()
        {
            return _internalDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalDictionary.GetEnumerator();
        }


        public GaMultivector Reverse()
        {
            var resultMv = new GaMultivector(GaSpaceDim);

            foreach (var term in _internalDictionary)
                if (term.Key.BasisBladeIdHasNegativeReverse())
                    resultMv._internalDictionary.Add(term.Key, -term.Value);
                else
                    resultMv._internalDictionary.Add(term.Key, term.Value);

            return resultMv;
        }

        public GaMultivector GradeInv()
        {
            var resultMv = new GaMultivector(GaSpaceDim);

            foreach (var term in _internalDictionary)
                if (term.Key.BasisBladeIdHasNegativeGradeInv())
                    resultMv._internalDictionary.Add(term.Key, -term.Value);
                else
                    resultMv._internalDictionary.Add(term.Key, term.Value);

            return resultMv;
        }

        public GaMultivector CliffConj()
        {
            var resultMv = new GaMultivector(GaSpaceDim);

            foreach (var term in _internalDictionary)
                if (term.Key.BasisBladeIdHasNegativeClifConj())
                    resultMv._internalDictionary.Add(term.Key, -term.Value);
                else
                    resultMv._internalDictionary.Add(term.Key, term.Value);

            return resultMv;
        }

        public void Simplify()
        {
            foreach (var term in _internalDictionary)
                term.Value.Simplify();

            var zeroCoefsIDs = 
                _internalDictionary
                .Where(t => t.Value.IsZero())
                .Select(t => t.Key)
                .ToArray();

            foreach (var id in zeroCoefsIDs)
                _internalDictionary.Remove(id);

            //var resultMv = new GaMultivectorCoefficients(GaSpaceDim);

            //foreach (var term in _internalDictionary)
            //{
            //    var mathExpr = CasEvaluator.Simplify(term.Value.MathExpr);
            //    var coef = MathematicaScalar.Create(CasInterface, mathExpr);

            //    if (coef.IsZero() == false)
            //        resultMv._internalDictionary.Add(term.Key, coef);
            //}

            //return resultMv;
        }

        public Dictionary<int, string> ToStringsDictionary()
        {
            return
                _internalDictionary
                .ToDictionary(
                    pair => pair.Key, 
                    pair => pair.Value.MathExpr.ToString()
                    );
        }

        public ISymbolicVector Times(ISymbolicMatrix m)
        {
            throw new NotImplementedException();
        }

        IEnumerator<MathematicaScalar> IEnumerable<MathematicaScalar>.GetEnumerator()
        {
            return _internalDictionary.Values.GetEnumerator();
        }
    }
}
