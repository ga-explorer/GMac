using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using GMac.GMacMath.Structures;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Numeric.Multivectors
{
    public sealed class GaNumMultivector : IGaNumMultivector
    {
        public static int AddFactorsCallCount { get; private set; }

        public static void ResetAddFactorsCallCount()
        {
            AddFactorsCallCount = 0;
        }

        
        public static IGaNumMultivectorTemp CreateZeroTemp(int gaSpaceDim)
        {
            switch (GaNumMultivectorUtils.DefaultTempMultivectorKind)
            {
                case GaTempMultivectorImplementation.Hash:
                    return GaNumMultivectorTempHash.Create(gaSpaceDim);
            }

            return GaNumMultivectorTempArray.Create(gaSpaceDim);
        }

        public static IGaNumMultivectorTemp CreateCopyTemp(GaNumMultivector mv)
        {
            return CreateZeroTemp(mv.GaSpaceDimension).SetTerms(mv);
        }

        public static IGaNumMultivectorTemp CreateBasisBladeTemp(int gaSpaceDim, int id)
        {
            return CreateZeroTemp(gaSpaceDim).SetTermCoef(id, 1.0d);
        }


        public static GaNumMultivector CreateZero(int gaSpaceDim)
        {
            return new GaNumMultivector(gaSpaceDim);
        }

        public static GaNumMultivector CreateTerm(int gaSpaceDim, int id, double coef)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            return resultMv.AddFactor(id, coef);
        }

        public static GaNumMultivector CreateBasisBlade(int gaSpaceDim, int id)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            return resultMv.AddFactor(id, 1.0d);
        }

        public static GaNumMultivector CreateScalar(int gaSpaceDim, double coef)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            return resultMv.AddFactor(0, coef);
        }

        public static GaNumMultivector CreateUnitScalar(int gaSpaceDim)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            return resultMv.AddFactor(0, 1.0d);
        }

        public static GaNumMultivector CreatePseudoscalar(int gaSpaceDim, double coef)
        {
            var resultMv = new GaNumMultivector(gaSpaceDim);

            return resultMv.AddFactor(gaSpaceDim - 1, coef);
        }

        public static GaNumMultivector CreateCopy(GaNumMultivector mv)
        {
            var resultMv = new GaNumMultivector(mv.GaSpaceDimension);

            resultMv.TermsTree.FillFromTree(mv.TermsTree);

            return resultMv;
        }

        internal static GaNumMultivector CreateCopy(GMacBinaryTree<double> termsTree)
        {
            var resultMv = new GaNumMultivector(termsTree.TreeDepth.ToGaSpaceDimension());

            resultMv.TermsTree.FillFromTree(termsTree);

            return resultMv;
        }

        public static GaNumMultivector CreateMapped(GaNumMultivector mv, Func<double, double> scalarMap)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.AddFactor(term.Key, scalarMap(term.Value));

            return resultMv;
        }

        public static GaNumMultivector CreateFromColumn(Matrix matrix, int col)
        {
            Debug.Assert(matrix.RowCount.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(matrix.RowCount);

            for (var index = 0; index < matrix.RowCount; index++)
                mv.AddFactor(index, matrix[index, col]);

            return mv;
        }

        public static GaNumMultivector CreateFromColumn(double[,] exprArray, int col)
        {
            var rows = exprArray.GetLength(0);

            Debug.Assert(rows.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(rows);

            for (var row = 0; row < rows; row++)
                mv.AddFactor(row, exprArray[row, col]);

            return mv;
        }

        public static GaNumMultivector CreateFromRow(double[,] exprArray, int row)
        {
            var cols = exprArray.GetLength(1);

            Debug.Assert(cols.IsValidGaSpaceDimension());

            var mv = new GaNumMultivector(cols);

            for (var col = 0; col < cols; col++)
                mv.AddFactor(col, exprArray[row, col]);

            return mv;
        }

        public static GaNumMultivector CreateVectorFromColumn(Matrix matrix, int col)
        {
            var gaSpaceDim = matrix.RowCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var row = 0; row < matrix.RowCount; row++)
                mv.AddFactor(1, row, matrix[row, col]);

            return mv;
        }

        public static GaNumMultivector CreateVectorFromRow(Matrix matrix, int row)
        {
            var gaSpaceDim = matrix.ColumnCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var col = 0; col < matrix.ColumnCount; col++)
                mv.AddFactor(1, col, matrix[row, col]);

            return mv;
        }

        public static GaNumMultivector CreateVectorFromColumn(double[,] exprArray, int col)
        {
            var rowsCount = exprArray.GetLength(0);
            var gaSpaceDim = rowsCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var row = 0; row < rowsCount; row++)
                mv.AddFactor(1, row, exprArray[row, col]);

            return mv;
        }

        public static GaNumMultivector CreateVectorFromRow(double[,] exprArray, int row)
        {
            var colsCount = exprArray.GetLength(1);
            var gaSpaceDim = colsCount.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var col = 0; col < colsCount; col++)
                mv.AddFactor(1, col, exprArray[row, col]);

            return mv;
        }

        public static GaNumMultivector CreateVectorFromScalars(double[] scalars)
        {
            var gaSpaceDim = scalars.Length.ToGaSpaceDimension();

            var mv = new GaNumMultivector(gaSpaceDim);

            for (var index = 0; index < scalars.Length; index++)
                mv.SetTermCoef(1, index, scalars[index]);

            return mv;
        }


        public static GaNumMultivector operator -(GaNumMultivector mv)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.SetTermCoef(term.Key, -term.Value);

            return resultMv;
        }

        public static GaNumMultivector operator +(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var resultMv = CreateCopy(mv1);

            foreach (var term in mv2.NonZeroTerms)
                resultMv.AddFactor(term.Key, term.Value);

            return resultMv;
        }

        public static GaNumMultivector operator -(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var resultMv = CreateCopy(mv1);

            foreach (var term in mv2.NonZeroTerms)
                resultMv.AddFactor(term.Key, -term.Value);

            return resultMv;
        }

        public static GaNumMultivector operator *(GaNumMultivector mv1, double s)
        {
            var resultMv = CreateZero(mv1.GaSpaceDimension);

            if (s.IsNearZero()) return resultMv;

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTermCoef(term.Key, term.Value * s);

            return resultMv;
        }

        public static GaNumMultivector operator *(double s, GaNumMultivector mv1)
        {
            var resultMv = CreateZero(mv1.GaSpaceDimension);

            if (s.IsNearZero()) return resultMv;

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTermCoef(term.Key, term.Value * s);

            return resultMv;
        }

        public static GaNumMultivector operator /(GaNumMultivector mv1, double s)
        {
            var resultMv = CreateZero(mv1.GaSpaceDimension);

            foreach (var term in mv1.NonZeroTerms)
                resultMv.SetTermCoef(term.Key, term.Value / s);

            return resultMv;
        }


        internal GMacBinaryTree<double> TermsTree { get; }

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
        {
            get
            {
                return this[GMacMathUtils.BasisBladeId(grade, index)];
            }
        }

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


        private GaNumMultivector(int gaSpaceDim)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            TermsTree = new GMacBinaryTree<double>(VSpaceDimension);
        }


        public bool IsTerm()
        {
            return TermsTree.LeafNodes.Count() <= 1;
        }

        public bool IsScalar()
        {
            return TermsTree.HasNoChildNodes ||
                   TermsTree.LeafValuePairs.All(p => p.Key == 0 || p.Value.IsNearZero());
        }

        public bool IsZero()
        {
            return TermsTree.HasNoChildNodes ||
                   TermsTree.LeafValues.All(v => v.IsNearZero());
        }

        public bool IsNearZero(double epsilon)
        {
            return TermsTree.HasNoChildNodes ||
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
            return this;
        }

        public bool ContainsBasisBlade(int id)
        {
            return TermsTree.ContainsLeafNodeId((ulong)id);
        }

        public void Simplify()
        {
            var terms = NonZeroTerms.ToArray();
            ResetToZero();

            foreach (var term in terms)
                SetTermCoef(term.Key, term.Value);
        }

        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public GaNumMultivector ResetToZero()
        {
            TermsTree.RemoveChildNodes();

            return this;
        }


        public GaNumMultivector AddFactor(int grade, int index, double coef)
        {
            AddFactorsCallCount++;

            var id = GMacMathUtils.BasisBladeId(grade, index);

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value += coef;
                else
                    node.ResetLeafChildNode1(coef);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value += coef;
            else
                node.ResetLeafChildNode0(coef);

            return this;
        }

        public GaNumMultivector AddFactor(int id, double coef)
        {
            AddFactorsCallCount++;

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value += coef;
                else
                    node.ResetLeafChildNode1(coef);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value += coef;
            else
                node.ResetLeafChildNode0(coef);

            return this;
        }

        public GaNumMultivector AddFactor(int id, bool isNegative, double coef)
        {
            AddFactorsCallCount++;

            var deltaValue = isNegative ? -coef : coef;

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value += deltaValue;
                else
                    node.ResetLeafChildNode1(deltaValue);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value += deltaValue;
            else
                node.ResetLeafChildNode0(deltaValue);

            return this;
        }


        public GaNumMultivector SetTermCoef(int grade, int index, bool isNegative, double coef)
        {
            var id = GMacMathUtils.BasisBladeId(grade, index);

            var value = isNegative ? -coef : coef;

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value = value;
                else
                    node.ResetLeafChildNode1(value);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value = value;
            else
                node.ResetLeafChildNode0(value);

            return this;
        }

        public GaNumMultivector SetTermCoef(int id, bool isNegative, double coef)
        {
            var value = isNegative ? -coef : coef;

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value = value;
                else
                    node.ResetLeafChildNode1(value);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value = value;
            else
                node.ResetLeafChildNode0(value);

            return this;
        }

        public GaNumMultivector SetTermCoef(int grade, int index, double coef)
        {
            var id = GMacMathUtils.BasisBladeId(grade, index);

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value = coef;
                else
                    node.ResetLeafChildNode1(coef);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value = coef;
            else
                node.ResetLeafChildNode0(coef);

            return this;
        }

        public GaNumMultivector SetTermCoef(int id, double coef)
        {
            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1 << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1)
                    node.LeafChildNode1.Value = coef;
                else
                    node.ResetLeafChildNode1(coef);

                return this;
            }

            if (node.HasChildNode0)
                node.LeafChildNode0.Value = coef;
            else
                node.ResetLeafChildNode0(coef);

            return this;
        }


        public KeyValuePair<int, double> GetTerm(int id)
        {
            return new KeyValuePair<int, double>(id, this[id]);
        }

        public GaNumMultivector GetPart(Func<int, bool> idSelectionFunc)
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => idSelectionFunc(t.Key)))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaNumMultivector GetVectorPart()
        {
            var mv = CreateZero(GaSpaceDimension);

            foreach (var id in GMacMathUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNearZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public GaNumMultivector GetKVectorPart(int grade)
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => t.Key.BasisBladeGrade() == grade))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaNumMultivector GetEvenPart()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => (t.Key.BasisBladeGrade() & 1) == 0))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaNumMultivector GetOddPart()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => (t.Key.BasisBladeGrade() & 1) == 1))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public double[] VectorPartToColumnVector()
        {
            var columnVector = new double[VSpaceDimension];

            for (var index = 0; index < VSpaceDimension; index++)
                columnVector[index] = this[1, index];

            return columnVector;
        }

        public double[] KVectorPartToColumnVector(int grade)
        {
            var columnVectorLength = GMacMathUtils.KvSpaceDimension(VSpaceDimension, grade);
            var columnVector = new double[columnVectorLength];

            for (var index = 0; index < columnVectorLength; index++)
                columnVector[index] = this[grade, index];

            return columnVector;
        }

        public Dictionary<int, GaNumMultivector> GetKVectorParts()
        {
            var kVectorsList = new Dictionary<int, GaNumMultivector>();

            foreach (var term in NonZeroTerms)
            {
                GaNumMultivector mv;

                var grade = term.Key.BasisBladeGrade();

                if (kVectorsList.TryGetValue(grade, out mv) == false)
                {
                    mv = new GaNumMultivector(GaSpaceDimension);

                    kVectorsList.Add(grade, mv);
                }

                mv.AddFactor(term.Key, term.Value);
            }

            return kVectorsList;
        }


        public GaNumMultivector Reverse()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                resultMv.AddFactor(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeReverse(),
                    term.Value
                );

            return resultMv;
        }

        public GaNumMultivector GradeInv()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                resultMv.AddFactor(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeGradeInv(),
                    term.Value
                );

            return resultMv;
        }

        public GaNumMultivector CliffConj()
        {
            var resultMv = new GaNumMultivector(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                resultMv.AddFactor(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeClifConj(),
                    term.Value
                );

            return resultMv;
        }


        public override string ToString()
        {
            return TermsTree.ToString();

            //var composer = new ListComposer(" + ");

            //foreach (var pair in Terms)
            //    composer.Add(
            //        pair.Value + " " + pair.Key.BasisBladeName()
            //    );

            //return composer.ToString();
        }
    }
}
