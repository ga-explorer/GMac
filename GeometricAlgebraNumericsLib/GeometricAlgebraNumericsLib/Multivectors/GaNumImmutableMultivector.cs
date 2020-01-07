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
    public sealed class GaNumImmutableMultivector : IGaNumMultivector
    {
        //public static int AddFactorsCallCount { get; private set; }

        //public static void ResetAddFactorsCallCount()
        //{
        //    AddFactorsCallCount = 0;
        //}

        
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
        //public static IGaNumMultivectorTemp CreateCopyTemp(GaNumImmutableMultivector mv)
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
        public static GaNumImmutableMultivector CreateZero(int gaSpaceDim)
        {
            return new GaNumImmutableMultivector(gaSpaceDim);
        }

        /// <summary>
        /// Create a multivector from the given list of terms.
        /// All basis blade IDs in the list must be unique.
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="termsList"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateFromTerms(int gaSpaceDim, IEnumerable<KeyValuePair<int, double>> termsList)
        {
            //Debug.Assert(termsList.All(t => t.Key >= 0 && t.Key < gaSpaceDim));

            return new GaNumImmutableMultivector(gaSpaceDim, termsList);
        }

        /// <summary>
        /// Create a single term multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="id"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateTerm(int gaSpaceDim, int id, double coef)
        {
            return new GaNumImmutableMultivector(gaSpaceDim, id, coef);
        }

        /// <summary>
        /// Create a basis blade multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateBasisBlade(int gaSpaceDim, int id)
        {
            return new GaNumImmutableMultivector(gaSpaceDim, id, 1.0d);
        }

        /// <summary>
        /// Create a scalar multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateScalar(int gaSpaceDim, double coef)
        {
            return new GaNumImmutableMultivector(gaSpaceDim, 0, coef);
        }

        /// <summary>
        /// Create a unit scalar multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateUnitScalar(int gaSpaceDim)
        {
            return new GaNumImmutableMultivector(gaSpaceDim, 0, 1.0d);
        }

        /// <summary>
        /// Create a pseudoscalar multivector
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <param name="coef"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreatePseudoscalar(int gaSpaceDim, double coef)
        {
            return new GaNumImmutableMultivector(gaSpaceDim, gaSpaceDim - 1, coef);
        }

        /// <summary>
        /// Create a copy of the given multivector
        /// </summary>
        /// <param name="mv"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateCopy(GaNumImmutableMultivector mv)
        {
            var resultMv = new GaNumImmutableMultivector(mv.GaSpaceDimension);

            resultMv.TermsTree.FillFromTree(mv.TermsTree);

            return resultMv;
        }

        /// <summary>
        /// Create a copy of the given multivector's internal binary tree
        /// </summary>
        /// <param name="termsTree"></param>
        /// <returns></returns>
        internal static GaNumImmutableMultivector CreateCopy(GaCompactBinaryTree<double> termsTree)
        {
            var resultMv = new GaNumImmutableMultivector(termsTree.TreeDepth.ToGaSpaceDimension());

            resultMv.TermsTree.FillFromTree(termsTree);

            return resultMv;
        }

        /// <summary>
        /// Create a mapped copy of the scalar coefficients of the given multivector
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="scalarMap"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateMapped(GaNumImmutableMultivector mv, Func<double, double> scalarMap)
        {
            var termsList =
                mv.NonZeroTerms.Select(term => 
                    new KeyValuePair<int, double>(
                        term.Key, 
                        scalarMap(term.Value)
                    )
                );

            return new GaNumImmutableMultivector(mv.GaSpaceDimension, termsList);
        }

        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateFromColumn(Matrix matrix, int col)
        {
            Debug.Assert(matrix.RowCount.IsValidGaSpaceDimension());

            var termsList =
                Enumerable
                    .Range(0, matrix.RowCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(i, matrix[i, col])
                    );

            return new GaNumImmutableMultivector(matrix.RowCount, termsList);
        }

        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateFromColumn(double[,] exprArray, int col)
        {
            var rowsCount = exprArray.GetLength(0);

            Debug.Assert(rowsCount.IsValidGaSpaceDimension());

            var termsList =
                Enumerable
                    .Range(0, rowsCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(i, exprArray[i, col])
                    );

            return new GaNumImmutableMultivector(rowsCount, termsList);
        }

        /// <summary>
        /// Create a multivector from a row vector. The row vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateFromRow(double[,] exprArray, int row)
        {
            var colsCount = exprArray.GetLength(1);

            Debug.Assert(colsCount.IsValidGaSpaceDimension());

            var termsList =
                Enumerable
                    .Range(0, colsCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(i, exprArray[row, i])
                    );

            return new GaNumImmutableMultivector(colsCount, termsList);
        }

        /// <summary>
        /// Create a vector multivector from a column vector. The column vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateVectorFromColumn(Matrix matrix, int col)
        {
            var gaSpaceDim = matrix.RowCount.ToGaSpaceDimension();

            var termsList =
                Enumerable
                    .Range(0, matrix.RowCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(1, i), 
                            matrix[i, col]
                        )
                    );

            return new GaNumImmutableMultivector(gaSpaceDim, termsList);
        }

        /// <summary>
        /// Create a vector multivector from a row vector. The row vector length
        /// defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateVectorFromRow(Matrix matrix, int row)
        {
            var gaSpaceDim = matrix.ColumnCount.ToGaSpaceDimension();

            var termsList =
                Enumerable
                    .Range(0, matrix.RowCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(1, i), 
                            matrix[row, i]
                        )
                    );

            return new GaNumImmutableMultivector(gaSpaceDim, termsList);
        }

        /// <summary>
        /// Create a vector multivector from a column vector. The column vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateVectorFromColumn(double[,] exprArray, int col)
        {
            var rowsCount = exprArray.GetLength(0);
            var gaSpaceDim = rowsCount.ToGaSpaceDimension();

            var termsList =
                Enumerable
                    .Range(0, rowsCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(1, i), 
                            exprArray[i, col]
                        )
                    );

            return new GaNumImmutableMultivector(gaSpaceDim, termsList);
        }

        /// <summary>
        /// Create a vector multivector from a row vector. The row vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateVectorFromRow(double[,] exprArray, int row)
        {
            var colsCount = exprArray.GetLength(1);
            var gaSpaceDim = colsCount.ToGaSpaceDimension();

            var termsList =
                Enumerable
                    .Range(0, colsCount)
                    .Select(i =>
                        new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(1, i), 
                            exprArray[row, i]
                        )
                    );

            return new GaNumImmutableMultivector(gaSpaceDim, termsList);
        }

        /// <summary>
        /// Create a vector multivector from an array of scalars. The array
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="scalars"></param>
        /// <returns></returns>
        public static GaNumImmutableMultivector CreateVectorFromScalars(double[] scalars)
        {
            var gaSpaceDim = scalars.Length.ToGaSpaceDimension();

            var termsList =
                Enumerable
                    .Range(0, scalars.Length)
                    .Select(i =>
                        new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(1, i), 
                            scalars[i]
                        )
                    );

            return new GaNumImmutableMultivector(gaSpaceDim, termsList);
        }


        public static GaNumImmutableMultivector operator -(GaNumImmutableMultivector mv)
        {
            var termsList = mv
                .NonZeroTerms
                .Select(term =>
                    new KeyValuePair<int, double>(term.Key, -term.Value)
                );

            return new GaNumImmutableMultivector(mv.GaSpaceDimension, termsList);
        }

        public static GaNumImmutableMultivector operator +(GaNumImmutableMultivector mv1, GaNumImmutableMultivector mv2)
        {
            var resultMv = 
                GaNumMultivectorHash.Create(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTerm(term.Key, term.Value);

            foreach (var term in mv2.NonZeroTerms)
                resultMv.UpdateTerm(term.Key, term.Value);

            return new GaNumImmutableMultivector(
                mv1.GaSpaceDimension, 
                resultMv.NonZeroTerms
            );
        }

        public static GaNumImmutableMultivector operator -(GaNumImmutableMultivector mv1, GaNumImmutableMultivector mv2)
        {
            var resultMv = 
                GaNumMultivectorHash.Create(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTerm(term.Key, term.Value);

            foreach (var term in mv2.NonZeroTerms)
                resultMv.UpdateTerm(term.Key, -term.Value);

            return new GaNumImmutableMultivector(
                mv1.GaSpaceDimension, 
                resultMv.NonZeroTerms
            );
        }

        public static GaNumImmutableMultivector operator *(GaNumImmutableMultivector mv1, double s)
        {
            var resultMv = 
                GaNumMultivectorHash.Create(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTerm(term.Key, s * term.Value);

            return new GaNumImmutableMultivector(
                mv1.GaSpaceDimension, 
                resultMv.NonZeroTerms
            );
        }

        public static GaNumImmutableMultivector operator *(double s, GaNumImmutableMultivector mv1)
        {
            var resultMv = 
                GaNumMultivectorHash.Create(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTerm(term.Key, s * term.Value);

            return new GaNumImmutableMultivector(
                mv1.GaSpaceDimension, 
                resultMv.NonZeroTerms
            );
        }

        public static GaNumImmutableMultivector operator /(GaNumImmutableMultivector mv1, double s)
        {
            var resultMv = 
                GaNumMultivectorHash.Create(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTerm(term.Key, term.Value / s);

            return new GaNumImmutableMultivector(
                mv1.GaSpaceDimension, 
                resultMv.NonZeroTerms
            );
        }


        internal GaCompactBinaryTree<double> TermsTree { get; private set; }


        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public IEnumerable<int> BasisBladeIds
            => TermsTree.LeafNodeIDs.Select(k => (int)k);

        public IEnumerable<int> NonZeroBasisBladeIds
            => TermsTree
                .LeafValuePairs
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => (int)pair.Key);

        public IEnumerable<double> BasisBladeScalars
            => TermsTree.LeafValues;

        public IEnumerable<double> NonZeroBasisBladeScalars 
            => TermsTree.LeafValues.Where(v => !v.IsNearZero());

        public int VSpaceDimension { get; }

        public double this[int grade, int index] 
            => this[GaNumFrameUtils.BasisBladeId(grade, index)];

        public double this[int id] 
            => TermsTree.GetLeafValue((ulong)id);

        public IEnumerable<KeyValuePair<int, double>> Terms
            => TermsTree
                .LeafNodePairs
                .Select(pair => new KeyValuePair<int, double>((int)pair.Key, pair.Value.Value));

        public IEnumerable<KeyValuePair<int, double>> NonZeroTerms
            => TermsTree
                .LeafNodePairs
                .Where(pair => !pair.Value.Value.IsNearZero())
                .Select(pair => new KeyValuePair<int, double>((int)pair.Key, pair.Value.Value));

        public bool IsTemp 
            => false;

        public int TermsCount 
            => TermsTree.LeafNodes.Count();


        private GaNumImmutableMultivector(int gaSpaceDim)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            TermsTree = new GaCompactBinaryTree<double>(VSpaceDimension);
        }

        private GaNumImmutableMultivector(int gaSpaceDim, int termId, double termValue)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            TermsTree = new GaCompactBinaryTree<double>(VSpaceDimension, termId, termValue);
        }

        private GaNumImmutableMultivector(int gaSpaceDim, ulong termId, double termValue)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            TermsTree = new GaCompactBinaryTree<double>(VSpaceDimension, termId, termValue);
        }

        private GaNumImmutableMultivector(int gaSpaceDim, IEnumerable<KeyValuePair<int, double>> termsList)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            TermsTree = new GaCompactBinaryTree<double>(VSpaceDimension, termsList);
        }

        private GaNumImmutableMultivector(int gaSpaceDim, IEnumerable<KeyValuePair<ulong, double>> termsList)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            TermsTree = new GaCompactBinaryTree<double>(VSpaceDimension, termsList);
        }


        public bool IsTerm()
        {
            return TermsTree.LeafNodes.Count() <= 1;
        }

        public bool IsScalar()
        {
            return TermsTree.RootNode.HasNoChildNodes ||
                   TermsTree.LeafValuePairs.All(p => p.Key == 0 || p.Value.IsNearZero());
        }

        public bool IsZero()
        {
            return TermsTree.RootNode.HasNoChildNodes ||
                   TermsTree.LeafValues.All(v => v.IsNearZero());
        }

        public bool IsEmpty()
        {
            return TermsTree.RootNode.HasNoChildNodes;
        }

        public bool IsNearZero(double epsilon)
        {
            return TermsTree.RootNode.HasNoChildNodes ||
                   TermsTree.LeafValues.All(v => v.IsNearZero(epsilon));
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
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                mv.SetTerm(term.Key, term.Value);

            return mv;
        }

        public bool ContainsBasisBlade(int id)
        {
            return TermsTree.ContainsLeafNodeId((ulong)id);
        }

        public void Simplify()
        {
            TermsTree = new GaCompactBinaryTree<double>(
                VSpaceDimension, 
                NonZeroTerms
            );
        }

        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }


        ///// <summary>
        ///// Set this multivector to zero
        ///// </summary>
        ///// <returns></returns>
        //public GaNumImmutableMultivector ResetToZero()
        //{
        //    TermsTree.RemoveChildNodes();

        //    return this;
        //}

        ///// <summary>
        ///// Add the scalar 'coef' to the given basis blade coefficient
        ///// </summary>
        ///// <param name="grade"></param>
        ///// <param name="index"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector UpdateTermCoef(int grade, int index, double coef)
        //{
        //    AddFactorsCallCount++;

        //    var id = GaNumFrameUtils.BasisBladeId(grade, index);

        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value += coef;
        //        else
        //            node.ResetLeafChildNode1(coef);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value += coef;
        //    else
        //        node.ResetLeafChildNode0(coef);

        //    return this;
        //}

        ///// <summary>
        ///// Add the scalar 'coef' to the given basis blade coefficient
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector UpdateTermCoef(int id, double coef)
        //{
        //    AddFactorsCallCount++;

        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value += coef;
        //        else
        //            node.ResetLeafChildNode1(coef);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value += coef;
        //    else
        //        node.ResetLeafChildNode0(coef);

        //    return this;
        //}

        ///// <summary>
        ///// Add or subtract the scalar 'coef' to the given basis blade coefficient
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="isNegative"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector UpdateTermCoef(int id, bool isNegative, double coef)
        //{
        //    AddFactorsCallCount++;

        //    var deltaValue = isNegative ? -coef : coef;

        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value += deltaValue;
        //        else
        //            node.ResetLeafChildNode1(deltaValue);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value += deltaValue;
        //    else
        //        node.ResetLeafChildNode0(deltaValue);

        //    return this;
        //}


        ///// <summary>
        ///// Set the given basis blade coefficient scalar value
        ///// </summary>
        ///// <param name="grade"></param>
        ///// <param name="index"></param>
        ///// <param name="isNegative"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector SetTermCoef(int grade, int index, bool isNegative, double coef)
        //{
        //    var id = GaNumFrameUtils.BasisBladeId(grade, index);

        //    var value = isNegative ? -coef : coef;

        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value = value;
        //        else
        //            node.ResetLeafChildNode1(value);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value = value;
        //    else
        //        node.ResetLeafChildNode0(value);

        //    return this;
        //}

        ///// <summary>
        ///// Set the given basis blade coefficient scalar value
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="isNegative"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector SetTermCoef(int id, bool isNegative, double coef)
        //{
        //    var value = isNegative ? -coef : coef;

        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value = value;
        //        else
        //            node.ResetLeafChildNode1(value);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value = value;
        //    else
        //        node.ResetLeafChildNode0(value);

        //    return this;
        //}

        ///// <summary>
        ///// Set the given basis blade coefficient scalar value
        ///// </summary>
        ///// <param name="grade"></param>
        ///// <param name="index"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector SetTermCoef(int grade, int index, double coef)
        //{
        //    var id = GaNumFrameUtils.BasisBladeId(grade, index);

        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value = coef;
        //        else
        //            node.ResetLeafChildNode1(coef);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value = coef;
        //    else
        //        node.ResetLeafChildNode0(coef);

        //    return this;
        //}

        ///// <summary>
        ///// Set the given basis blade coefficient scalar value
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="coef"></param>
        ///// <returns></returns>
        //public GaNumImmutableMultivector SetTermCoef(int id, double coef)
        //{
        //    var node = TermsTree;
        //    for (var i = VSpaceDimension - 1; i > 0; i--)
        //    {
        //        var bitPattern = (1 << i) & id;
        //        node = node.GetOrAddInternalChildNode(bitPattern != 0);
        //    }

        //    if ((1 & id) != 0)
        //    {
        //        if (node.HasChildNode1)
        //            node.LeafChildNode1.Value = coef;
        //        else
        //            node.ResetLeafChildNode1(coef);

        //        return this;
        //    }

        //    if (node.HasChildNode0)
        //        node.LeafChildNode0.Value = coef;
        //    else
        //        node.ResetLeafChildNode0(coef);

        //    return this;
        //}


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
        public GaNumImmutableMultivector GetPart(Func<int, bool> idSelectionFunc)
        {
            var termsList =
                TermsTree
                    .LeafNodePairs
                    .Where(pair => 
                        !pair.Value.Value.IsNearZero() && 
                        idSelectionFunc((int)pair.Key)
                    ).Select(pair => 
                        new KeyValuePair<ulong, double>(
                            pair.Key, 
                            pair.Value.Value
                        )
                    );

            return new GaNumImmutableMultivector(
                GaSpaceDimension, 
                termsList
            );
        }

        /// <summary>
        /// Extract part of this multivector as a new multivector
        /// </summary>
        /// <param name="idSelectionFunc"></param>
        /// <returns></returns>
        public GaNumImmutableMultivector GetPart(Func<ulong, bool> idSelectionFunc)
        {
            var termsList =
                TermsTree
                    .LeafNodePairs
                    .Where(pair => 
                        !pair.Value.Value.IsNearZero() && 
                        idSelectionFunc(pair.Key)
                    ).Select(pair => 
                        new KeyValuePair<ulong, double>(
                            pair.Key, 
                            pair.Value.Value
                        )
                    );

            return new GaNumImmutableMultivector(
                GaSpaceDimension, 
                termsList
            );
        }

        public GaNumMultivector GetVectorPart()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

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
        public GaNumImmutableMultivector GetKVectorPart(int grade)
        {
            var termsList =
                TermsTree
                    .LeafNodePairs
                    .Where(pair => 
                        !pair.Value.Value.IsNearZero() && 
                        ((int)pair.Key).BasisBladeGrade() == grade
                    ).Select(pair => 
                        new KeyValuePair<ulong, double>(
                            pair.Key, 
                            pair.Value.Value
                        )
                    );

            return new GaNumImmutableMultivector(
                GaSpaceDimension, 
                termsList
            );
        }

        /// <summary>
        /// Extract the even part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumImmutableMultivector GetEvenPart()
        {
            var termsList =
                TermsTree
                    .LeafNodePairs
                    .Where(pair => 
                        !pair.Value.Value.IsNearZero() && 
                        (((int)pair.Key).BasisBladeGrade() & 1) == 0
                    ).Select(pair => 
                        new KeyValuePair<ulong, double>(
                            pair.Key, 
                            pair.Value.Value
                        )
                    );

            return new GaNumImmutableMultivector(
                GaSpaceDimension, 
                termsList
            );
        }

        /// <summary>
        /// Extract the odd part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumImmutableMultivector GetOddPart()
        {
            var termsList =
                TermsTree
                    .LeafNodePairs
                    .Where(pair => 
                        !pair.Value.Value.IsNearZero() && 
                        (((int)pair.Key).BasisBladeGrade() & 1) == 1
                    ).Select(pair => 
                        new KeyValuePair<ulong, double>(
                            pair.Key, 
                            pair.Value.Value
                        )
                    );

            return new GaNumImmutableMultivector(
                GaSpaceDimension, 
                termsList
            );
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
        public Dictionary<int, GaNumImmutableMultivector> GetKVectorParts()
        {
            var kVectorsList = new Dictionary<int, GaNumMultivectorHash>();

            foreach (var term in NonZeroTerms)
            {
                var grade = term.Key.BasisBladeGrade();

                if (kVectorsList.TryGetValue(grade, out var mv) == false)
                {
                    mv = GaNumMultivectorHash.Create(GaSpaceDimension);

                    kVectorsList.Add(grade, mv);
                }

                mv.UpdateTerm(term.Key, term.Value);
            }

            return kVectorsList.ToDictionary(
                pair => pair.Key, 
                pair => new GaNumImmutableMultivector(GaSpaceDimension, pair.Value.Terms)
            );
        }


        /// <summary>
        /// Compute the reverse of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumImmutableMultivector Reverse()
        {
            var termsList = 
                NonZeroTerms.Select(pair => 
                    new KeyValuePair<int, double>(
                        pair.Key, 
                        pair.Key.BasisBladeIdHasNegativeReverse()
                            ? -pair.Value : pair.Value
                    )
                );

            return new GaNumImmutableMultivector(GaSpaceDimension, termsList);
        }

        /// <summary>
        /// Compute the grade involution of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumImmutableMultivector GradeInv()
        {
            var termsList = 
                NonZeroTerms.Select(pair => 
                    new KeyValuePair<int, double>(
                        pair.Key, 
                        pair.Key.BasisBladeIdHasNegativeGradeInv()
                            ? -pair.Value : pair.Value
                    )
                );

            return new GaNumImmutableMultivector(GaSpaceDimension, termsList);
        }

        /// <summary>
        /// Compute the Clifford conjugate of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumImmutableMultivector CliffConj()
        {
            var termsList = 
                NonZeroTerms.Select(pair => 
                    new KeyValuePair<int, double>(
                        pair.Key, 
                        pair.Key.BasisBladeIdHasNegativeClifConj()
                            ? -pair.Value : pair.Value
                    )
                );

            return new GaNumImmutableMultivector(GaSpaceDimension, termsList);
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