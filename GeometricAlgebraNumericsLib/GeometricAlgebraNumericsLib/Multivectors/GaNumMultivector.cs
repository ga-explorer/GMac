using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    /// <summary>
    /// This class represents a multivector with floating point coefficients.
    /// The coefficients are stored internally in a binary tree structure to
    /// exploit sparsity of multivectors during computations.
    /// </summary>
    public sealed class GaNumMultivector : IGaNumMultivectorMutable
    {
        public static int AddFactorsCallCount { get; private set; }

        public static void ResetAddFactorsCallCount()
        {
            AddFactorsCallCount = 0;
        }


        ///// <summary>
        ///// Create a temporary multivector
        ///// </summary>
        ///// <param name="gaSpaceDim"></param>
        ///// <returns></returns>
        //public static IGaNumMultivectorTemp CreateZeroTemp(int gaSpaceDim)
        //{
        //    switch (GaNumMultivectorUtils.DefaultTempMultivectorKind)
        //    {
        //        case GaTempMultivectorImplementation.Hash:
        //            return GaNumMultivectorTempHash.Create(gaSpaceDim);
        //    }

        //    return GaNumMultivectorTempArray.Create(gaSpaceDim);
        //}

        ///// <summary>
        ///// Create a temporary multivector from the given multivector
        ///// </summary>
        ///// <param name="mv"></param>
        ///// <returns></returns>
        //public static IGaNumMultivectorTemp CreateCopyTemp(GaNumMultivector mv)
        //{
        //    return CreateZeroTemp(mv.GaSpaceDimension).SetTerms(mv);
        //}

        ///// <summary>
        ///// Create a temporary basis blade multivector
        ///// </summary>
        ///// <param name="gaSpaceDim"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static IGaNumMultivectorTemp CreateBasisBladeTemp(int gaSpaceDim, int id)
        //{
        //    return CreateZeroTemp(gaSpaceDim).SetTermCoef(id, 1.0d);
        //}


        /// <summary>
        /// Create a zero multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateZero(int gaSpaceDim)
        {
            return new GaNumMultivector(gaSpaceDim);
        }

        /// <summary>
        /// Create a multivector from the given list of terms.
        /// All basis blade IDs in the list must be unique.
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="termsList"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateFromTerms(int gaSpaceDim, IEnumerable<KeyValuePair<int, double>> termsList)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            foreach (var term in termsList)
                resultMv.AddTerm(term.Key, term.Value);

            return resultMv;
        }

        /// <summary>
        /// Create a single term multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="id"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateTerm(int gaSpaceDim, int id, double coef)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(id, coef);

            return resultMv;
        }

        /// <summary>
        /// Create a basis vector multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateBasisVector(int gaSpaceDim, int index)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(1, index, 1.0d);

            return resultMv;
        }

        /// <summary>
        /// Create a basis blade multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateBasisBlade(int gaSpaceDim, int id)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(id, 1.0d);

            return resultMv;
        }

        public static GaNumMultivector CreateBasisBlade(int gaSpaceDim, int grade, int index)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(grade, index, 1.0d);

            return resultMv;
        }

        /// <summary>
        /// Create a scalar multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateScalar(int gaSpaceDim, double coef)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(0, coef);

            return resultMv;
        }

        /// <summary>
        /// Create a unit scalar multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateUnitScalar(int gaSpaceDim)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(0, 1.0d);

            return resultMv;
        }

        /// <summary>
        /// Create a pseudoscalar multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public static GaNumMultivector CreatePseudoscalar(int gaSpaceDim, double coef)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            resultMv.AddTerm(gaSpaceDim - 1, coef);

            return resultMv;
        }

        /// <summary>
        /// Create a copy of the given multivector
        /// </summary>
        /// <param name="mv"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateCopy(GaNumMultivector mv)
        {
            var resultMv = new GaNumMultivector(mv.GaSpaceDimension);

            resultMv.AddTerms(mv.Terms);

            return resultMv;
        }

        /// <summary>
        /// Create a copy of the given multivector's internal binary tree
        /// </summary>
        /// <param name="termsTree"></param>
        /// <returns></returns>
        internal static GaNumMultivector CreateCopy(GaBinaryTreeInternalNode<double> termsTree)
        {
            var resultMv = new GaNumMultivector(termsTree.TreeDepth.ToGaSpaceDimension());

            foreach (var node in termsTree.LeafNodes)
                resultMv.AddTerm((int) node.Id, node.Value);

            return resultMv;
        }

        /// <summary>
        /// Create a mapped copy of the scalar coefficients of the given multivector
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="scalarMap"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateMapped(GaNumMultivector mv, Func<double, double> scalarMap)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.AddTerm(term.Key, scalarMap(term.Value));

            return resultMv;
        }

        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateFromColumn(Matrix matrix, int col)
        {
            Debug.Assert(matrix.RowCount.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(matrix.RowCount);

            for (var row = 0; row < matrix.RowCount; row++)
                mv.AddTerm(row, matrix[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateFromRow(Matrix matrix, int row)
        {
            Debug.Assert(matrix.ColumnCount.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(matrix.ColumnCount);

            for (var col = 0; col < matrix.ColumnCount; col++)
                mv.AddTerm(col, matrix[col, row]);

            return mv;
        }
        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateFromColumn(double[,] exprArray, int col)
        {
            var rows = exprArray.GetLength(0);

            Debug.Assert(rows.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(rows);

            for (var row = 0; row < rows; row++)
                mv.AddTerm(row, exprArray[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a multivector from a row vector. The row vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateFromRow(double[,] exprArray, int row)
        {
            var cols = exprArray.GetLength(1);

            Debug.Assert(cols.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(cols);

            for (var col = 0; col < cols; col++)
                mv.AddTerm(col, exprArray[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a vector multivector from a column vector. The column vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateVectorFromColumn(Matrix matrix, int col)
        {
            var gaSpaceDim = matrix.RowCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var row = 0; row < matrix.RowCount; row++)
                mv.AddTerm(1, row, matrix[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a vector multivector from a row vector. The row vector length
        /// defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateVectorFromRow(Matrix matrix, int row)
        {
            var gaSpaceDim = matrix.ColumnCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var col = 0; col < matrix.ColumnCount; col++)
                mv.AddTerm(1, col, matrix[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a vector multivector from a column vector. The column vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateVectorFromColumn(double[,] exprArray, int col)
        {
            var rowsCount = exprArray.GetLength(0);
            var gaSpaceDim = rowsCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var row = 0; row < rowsCount; row++)
                mv.AddTerm(1, row, exprArray[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a vector multivector from a row vector. The row vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateVectorFromRow(double[,] exprArray, int row)
        {
            var colsCount = exprArray.GetLength(1);
            var gaSpaceDim = colsCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var col = 0; col < colsCount; col++)
                mv.AddTerm(1, col, exprArray[row, col]);

            return mv;
        }

        /// <summary>
        /// Create a vector multivector from an array of scalars. The array
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="scalars"></param>
        /// <returns></returns>
        public static GaNumMultivector CreateVectorFromScalars(params double[] scalars)
        {
            var gaSpaceDim = scalars.Length.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var index = 0; index < scalars.Length; index++)
                mv.AddTerm(1, index, scalars[index]);

            return mv;
        }


        public static GaNumMultivector operator -(GaNumMultivector mv)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.AddTerm(term.Key, -term.Value);

            return resultMv;
        }

        public static GaNumMultivector operator +(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var resultMv = CreateCopy(mv1);

            foreach (var term in mv2.NonZeroTerms)
                resultMv.UpdateTerm(term.Key, term.Value);

            return resultMv;
        }

        public static GaNumMultivector operator -(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var resultMv = CreateCopy(mv1);

            foreach (var term in mv2.NonZeroTerms)
                resultMv.UpdateTerm(term.Key, -term.Value);

            return resultMv;
        }

        public static GaNumMultivector operator *(GaNumMultivector mv1, double s)
        {
            var resultMv = CreateZero(mv1.GaSpaceDimension);

            if (s.IsNearZero()) return resultMv;

            foreach (var term in mv1.NonZeroTerms)
                resultMv.AddTerm(term.Key, term.Value * s);

            return resultMv;
        }

        public static GaNumMultivector operator *(double s, GaNumMultivector mv1)
        {
            var resultMv = CreateZero(mv1.GaSpaceDimension);

            if (s.IsNearZero()) return resultMv;

            foreach (var term in mv1.NonZeroTerms)
                resultMv.AddTerm(term.Key, term.Value * s);

            return resultMv;
        }

        public static GaNumMultivector operator /(GaNumMultivector mv1, double s)
        {
            var resultMv = CreateZero(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.AddTerm(term.Key, term.Value / s);

            return resultMv;
        }


        
        private readonly Dictionary<int, double> _termsDictionary
            = new Dictionary<int, double>();

        private GaBinaryTreeInternalNode<double> _termsTree;

        internal GaBinaryTreeInternalNode<double> TermsTree
        {
            get
            {
                if (ReferenceEquals(_termsTree, null))
                    UpdateTermsTree();

                return _termsTree;
            }
        }


        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public IEnumerable<int> BasisBladeIds
            => TermsTree.LeafNodeIDs.Select(k => (int)k);

        public IEnumerable<int> NonZeroBasisBladeIds
            => _termsDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => pair.Key);

        public IEnumerable<double> BasisBladeScalars
            => _termsDictionary.Select(pair => pair.Value);

        public IEnumerable<double> NonZeroBasisBladeScalars 
            => _termsDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => pair.Value);

        public int VSpaceDimension { get; }

        public double this[int grade, int index] 
            => _termsDictionary.TryGetValue(GaNumFrameUtils.BasisBladeId(grade, index), out var coef) 
                ? coef : 0.0d;

        public double this[int id] =>
            _termsDictionary.TryGetValue(id, out var coef) 
                ? coef : 0.0d;

        public IEnumerable<KeyValuePair<int, double>> Terms
            => _termsDictionary
                .Select(pair => 
                    new KeyValuePair<int, double>(pair.Key, pair.Value)
                );

        public IEnumerable<KeyValuePair<int, double>> NonZeroTerms
            => _termsDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => 
                    new KeyValuePair<int, double>(pair.Key, pair.Value)
                );

        public bool IsTemp 
            => false;

        public int TermsCount 
            => _termsDictionary.Count;


        private GaNumMultivector(int gaSpaceDim)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
        }


        private void UpdateTermsTree()
        {
            _termsTree = new GaBinaryTreeInternalNode<double>(0, VSpaceDimension);

            foreach (var leafNode in _termsDictionary)
            {
                var id = leafNode.Key;
                var coef = leafNode.Value;

                var node = _termsTree;
                for (var i = VSpaceDimension - 1; i > 0; i--)
                {
                    var bitPattern = (1 << i) & id;
                    node = node.GetOrAddInternalChildNode(bitPattern != 0);
                }

                if ((1 & id) == 0)
                    node.ResetLeafChildNode0(coef);
                else
                    node.ResetLeafChildNode1(coef);
            }
        }

        public GaNumMultivector ClearInternalTermsTree()
        {
            _termsTree = null;

            return this;
        }

        public GaBinaryTreeInternalNode<double> GetInternalTermsTree()
        {
            if (ReferenceEquals(_termsTree, null))
                UpdateTermsTree();

            return _termsTree;
        }


        public bool IsTerm()
        {
            return _termsDictionary.Count <= 1;
        }

        public bool IsScalar()
        {
            return 
                _termsDictionary.Count == 0 ||
                _termsDictionary.All(
                    p => p.Key == 0 || p.Value.IsNearZero()
                );
        }

        public bool IsZero()
        {
            return 
                _termsDictionary.Count == 0 ||
                _termsDictionary.Values.All(
                    p => p.IsNearZero()
                );
        }

        public bool IsEmpty()
        {
            return _termsDictionary.Count == 0;
        }

        public bool IsNearZero(double epsilon)
        {
            return 
                _termsDictionary.Count == 0 ||
                _termsDictionary.Values.All(
                    p => p.IsNearZero()
                );
        }

        public double[] TermsToArray()
        {
            var termsArray = new double[GaSpaceDimension];

            foreach (var term in NonZeroTerms)
                termsArray[term.Key] = term.Value;

            return termsArray;
        }

        public GaNumMultivector ToMultivector()
        {
            return this;
        }

        public bool ContainsBasisBlade(int id)
        {
            return _termsDictionary.ContainsKey(id);
        }

        public bool TryGetValue(int id, out double value)
        {
            return _termsDictionary.TryGetValue(id, out value);
        }

        public void Simplify()
        {
            _termsTree = null;

            var idsList = _termsDictionary
                .Where(p => p.Value.IsNearZero())
                .Select(p => p.Key)
                .ToArray();

            foreach (var id in idsList)
                _termsDictionary.Remove(id);
        }

        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }


        /// <summary>
        /// Set this multivector to zero
        /// </summary>
        /// <returns></returns>
        public GaNumMultivector ResetToZero()
        {
            _termsTree = null;
            _termsDictionary.Clear();

            return this;
        }

        /// <summary>
        /// Add the scalar 'coef' to the given basis blade coefficient
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public GaNumMultivector UpdateTerm(int grade, int index, double coef)
        {
            _termsTree = null;
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            if (_termsDictionary.TryGetValue(id, out var oldCoef))
                _termsDictionary[id] = oldCoef + coef;
            else
                _termsDictionary.Add(id, coef);

            return this;
        }

        /// <summary>
        /// Add the scalar 'coef' to the given basis blade coefficient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public IGaNumMultivectorMutable UpdateTerm(int id, double coef)
        {
            _termsTree = null;

            if (_termsDictionary.TryGetValue(id, out var oldCoef))
                _termsDictionary[id] = oldCoef + coef;
            else
                _termsDictionary.Add(id, coef);

            return this;
        }

        /// <summary>
        /// Add or subtract the scalar 'coef' to the given basis blade coefficient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isNegative"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public IGaNumMultivectorMutable UpdateTerm(int id, bool isNegative, double coef)
        {
            _termsTree = null;
            var deltaValue = isNegative ? -coef : coef;

            if (_termsDictionary.TryGetValue(id, out var oldCoef))
                _termsDictionary[id] = oldCoef + deltaValue;
            else
                _termsDictionary.Add(id, deltaValue);

            return this;
        }


        /// <summary>
        /// Set the given basis blade coefficient scalar value
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="isNegative"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public GaNumMultivector SetTerm(int grade, int index, bool isNegative, double coef)
        {
            _termsTree = null;
            var id = GaNumFrameUtils.BasisBladeId(grade, index);
            var value = isNegative ? -coef : coef;

            if (_termsDictionary.ContainsKey(id))
                _termsDictionary[id] = value;
            else
                _termsDictionary.Add(id, value);

            return this;
        }

        /// <summary>
        /// Set the given basis blade coefficient scalar value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isNegative"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public IGaNumMultivectorMutable SetTerm(int id, bool isNegative, double coef)
        {
            _termsTree = null;
            var value = isNegative ? -coef : coef;

            if (_termsDictionary.ContainsKey(id))
                _termsDictionary[id] = value;
            else
                _termsDictionary.Add(id, value);

            return this;
        }

        /// <summary>
        /// Set the given basis blade coefficient scalar value
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public GaNumMultivector SetTerm(int grade, int index, double coef)
        {
            _termsTree = null;
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            if (_termsDictionary.ContainsKey(id))
                _termsDictionary[id] = coef;
            else
                _termsDictionary.Add(id, coef);

            return this;
        }

        /// <summary>
        /// Set the given basis blade coefficient scalar value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public IGaNumMultivectorMutable SetTerm(int id, double coef)
        {
            _termsTree = null;

            if (_termsDictionary.ContainsKey(id))
                _termsDictionary[id] = coef;
            else
                _termsDictionary.Add(id, coef);

            return this;
        }

        public void AddTerm(int id, double value)
        {
            _termsDictionary.Add(id, value);
        }

        public void AddTerm(int id, bool isNegative, double value)
        {
            _termsDictionary.Add(id, isNegative ? -value : value);
        }

        public void AddTerm(int grade, int index, double value)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            _termsDictionary.Add(id, value);
        }

        public void AddTerm(int grade, int index, bool isNegative, double value)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            _termsDictionary.Add(id, isNegative ? -value : value);
        }


        /// <summary>
        /// Get the term of the given basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<int, double> GetTerm(int id)
        {
            return new KeyValuePair<int, double>(id, this[id]);
        }

        /// <summary>
        /// Extract part of this multivector as a new multivector
        /// </summary>
        /// <param name="idSelectionFunc"></param>
        /// <returns></returns>
        public GaNumMultivector GetPart(Func<int, bool> idSelectionFunc)
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => idSelectionFunc(t.Key)))
                resultMv.SetTerm(term.Key, term.Value);

            return resultMv;
        }

        public GaNumMultivector GetVectorPart()
        {
            var mv = CreateZero(GaSpaceDimension);

            foreach (var id in GaNumFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNearZero())
                    mv.SetTerm(id, coef);
            }

            return mv;
        }

        /// <summary>
        /// Extract the k-vector part of this multivector as a new multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public GaNumMultivector GetKVectorPart(int grade)
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => t.Key.BasisBladeGrade() == grade))
                resultMv.SetTerm(term.Key, term.Value);

            return resultMv;
        }

        /// <summary>
        /// Extract the even part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumMultivector GetEvenPart()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => (t.Key.BasisBladeGrade() & 1) == 0))
                resultMv.SetTerm(term.Key, term.Value);

            return resultMv;
        }

        /// <summary>
        /// Extract the odd part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumMultivector GetOddPart()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => (t.Key.BasisBladeGrade() & 1) == 1))
                resultMv.SetTerm(term.Key, term.Value);

            return resultMv;
        }

        /// <summary>
        /// Extract the vector part of this multivector as a scalar array
        /// </summary>
        /// <returns></returns>
        public double[] VectorPartToColumnVector()
        {
            var columnVector = new double[VSpaceDimension];

            for (var index = 0; index < VSpaceDimension; index++)
                columnVector[index] = this[1, index];

            return columnVector;
        }

        /// <summary>
        /// Extract the k-vector part of this multivector as a scalar array
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public double[] KVectorPartToColumnVector(int grade)
        {
            var columnVectorLength = GaNumFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            var columnVector = new double[columnVectorLength];

            for (var index = 0; index < columnVectorLength; index++)
                columnVector[index] = this[grade, index];

            return columnVector;
        }

        /// <summary>
        /// Extract all k-vector parts of this multivector as a dictionary of
        /// multivectors
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, GaNumMultivector> GetKVectorParts()
        {
            var kVectorsList = new Dictionary<int, GaNumMultivector>();

            foreach (var term in NonZeroTerms)
            {
                var grade = term.Key.BasisBladeGrade();

                if (kVectorsList.TryGetValue(grade, out var mv) == false)
                {
                    mv = new GaNumMultivector(GaSpaceDimension);

                    kVectorsList.Add(grade, mv);
                }

                mv.UpdateTerm(term.Key, term.Value);
            }

            return kVectorsList;
        }


        /// <summary>
        /// Compute the reverse of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumMultivector Reverse()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                resultMv.SetTerm(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeReverse(),
                    term.Value
                );

            return resultMv;
        }

        /// <summary>
        /// Compute the grade involution of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumMultivector GradeInv()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                resultMv.SetTerm(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeGradeInv(),
                    term.Value
                );

            return resultMv;
        }

        /// <summary>
        /// Compute the Clifford conjugate of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumMultivector CliffConj()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                resultMv.SetTerm(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeClifConj(),
                    term.Value
                );

            return resultMv;
        }


        public override string ToString()
        {
            return TermsTree.ToString();

            //var composer = new ListTextComposer(" + ");

            //foreach (var pair in Terms)
            //    composer.Add(
            //        pair.Value + " " + pair.Key.BasisBladeName()
            //    );

            //return composer.ToString();
        }
    }
}
