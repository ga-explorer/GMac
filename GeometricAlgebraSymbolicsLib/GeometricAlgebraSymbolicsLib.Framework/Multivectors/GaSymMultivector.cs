using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivector : IGaSymMultivector, ISymbolicVector
    {
        public static IGaSymMultivectorTemp CreateZeroTemp(int vSpaceDim)
        {
            switch (GaSymMultivectorUtils.DefaultTempMultivectorKind)
            {
                case GaMultivectorMutableImplementation.Hash:
                    return GaSymMultivectorTempHash.Create(vSpaceDim);

                case GaMultivectorMutableImplementation.SparseArrayRepresentation:
                    return GaSymMultivectorTempTree.Create(vSpaceDim);
            }

            return GaSymMultivectorTempArray.Create(vSpaceDim);
        }

        public static IGaSymMultivectorTemp CreateCopyTemp(GaSymMultivector mv)
        {
            return CreateZeroTemp(mv.VSpaceDimension).SetTerms(mv);
        }

        public static IGaSymMultivectorTemp CreateCopyTemp(int vSpaceDim, IEnumerable<KeyValuePair<ulong, Expr>> termsList)
        {
            return CreateZeroTemp(vSpaceDim).SetTerms(termsList);
        }

        public static IGaSymMultivectorTemp CreateBasisBladeTemp(int vSpaceDim, ulong id)
        {
            return CreateZeroTemp(vSpaceDim).SetTermCoef(id, Expr.INT_ONE);
        }

        public static IGaSymMultivectorTemp CreateCopyTemp(ISymbolicVector v)
        {
            return CreateZeroTemp(v.Size.ToVSpaceDimension()).SetTerms(v);
        }


        public static GaSymMultivector CreateZero(int vSpaceDim)
        {
            return new GaSymMultivector(vSpaceDim);
        }

        public static GaSymMultivector CreateTerm(int vSpaceDim, ulong id, MathematicaScalar coef)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(id, coef.Expression);
        }

        public static GaSymMultivector CreateTerm(int vSpaceDim, ulong id, Expr coef)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(id, coef);
        }

        public static GaSymMultivector CreateBasisBlade(int vSpaceDim, ulong id)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(id, Expr.INT_ONE);
        }

        public static GaSymMultivector CreateBasisVector(int vSpaceDim, ulong index)
        {
            var id = 1UL << (int)index;
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(id, Expr.INT_ONE);
        }

        public static GaSymMultivector CreateScalar(int vSpaceDim, MathematicaScalar coef)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(0, coef.Expression);
        }

        public static GaSymMultivector CreateUnitScalar(int vSpaceDim)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(0, Expr.INT_ONE);
        }

        public static GaSymMultivector CreateScalar(int vSpaceDim, Expr coef)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef(0, coef);
        }

        public static GaSymMultivector CreatePseudoscalar(int vSpaceDim, MathematicaScalar coef)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            return resultMv.SetTermCoef((1UL << vSpaceDim) - 1, coef.Expression);
        }

        public static GaSymMultivector CreateCopy(GaSymMultivector mv)
        {
            var resultMv = new GaSymMultivector(mv.VSpaceDimension);

            resultMv.TermsTree.FillFromTree(mv.TermsTree);

            return resultMv;
        }

        public static GaSymMultivector CreateCopy(int vSpaceDim, IEnumerable<KeyValuePair<ulong, Expr>> termsList)
        {
            return CreateZero(vSpaceDim).SetTerms(termsList);
        }

        public static GaSymMultivector CreateCopy(ISymbolicVector v)
        {
            return CreateZero(v.Size.ToVSpaceDimension()).SetTerms(v);
        }

        public static GaSymMultivector CreateCopy(GaBtrInternalNode<Expr> termsTree)
        {
            var vSpaceDimension = termsTree.GetTreeDepth();
            var resultMv = new GaSymMultivector(vSpaceDimension);

            resultMv.TermsTree.FillFromTree(termsTree);

            return resultMv;
        }

        public static GaSymMultivector CreateMapped(GaSymMultivector mv, Func<MathematicaScalar, MathematicaScalar> scalarMap)
        {
            var resultMv = CreateZero(mv.VSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.SetTermCoef(term.Key, scalarMap(term.Value));

            return resultMv;
        }

        public static GaSymMultivector CreateMapped(GaSymMultivector mv, Func<Expr, Expr> scalarMap)
        {
            var resultMv = CreateZero(mv.VSpaceDimension);

            foreach (var term in mv.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, scalarMap(term.Value));

            return resultMv;
        }

        public static GaSymMultivector CreateSymbolic(int vSpaceDim, string baseCoefName)
        {
            return CreateSymbolic(
                vSpaceDim,
                baseCoefName,
                Enumerable.Range(0, (int)vSpaceDim.ToGaSpaceDimension()).Select(i => (ulong)i)
            );
        }

        public static GaSymMultivector CreateFromColumn(ISymbolicMatrix matrix, int col)
        {
            var vSpaceDim = matrix.RowCount.ToVSpaceDimension();
            var gaSpaceDim = (ulong)matrix.RowCount;

            var mv = new GaSymMultivector(vSpaceDim);

            for (var id = 0UL; id < gaSpaceDim; id++)
                mv.SetTermCoef(id, matrix[(int)id, col]);

            return mv;
        }

        public static GaSymMultivector CreateFromColumn(Expr[,] exprArray, int col)
        {
            var rows = (ulong)exprArray.GetLength(0);

            Debug.Assert(rows.IsValidGaSpaceDimension());

            var mv = new GaSymMultivector(rows.ToVSpaceDimension());

            for (var row = 0UL; row < rows; row++)
            {
                var expr = exprArray[row, col];

                if (!expr.IsNullOrZero())
                    mv.SetTermCoef(row, expr);
            }

            return mv;
        }

        public static GaSymMultivector CreateFromRow(Expr[,] exprArray, ulong row)
        {
            var cols = (ulong)exprArray.GetLength(1);

            Debug.Assert(cols.IsValidGaSpaceDimension());

            var mv = new GaSymMultivector(cols.ToVSpaceDimension());

            for (var col = 0UL; col < cols; col++)
            {
                var expr = exprArray[row, col];

                if (!expr.IsNullOrZero())
                    mv.SetTermCoef(col, expr);
            }

            return mv;
        }

        public static GaSymMultivector CreateVectorFromColumn(ISymbolicMatrix matrix, ulong col)
        {
            var vSpaceDim = matrix.RowCount;

            var mv = new GaSymMultivector(vSpaceDim);

            for (var row = 0; row < matrix.RowCount; row++)
                mv.SetTermCoef(1, (ulong)row, matrix[row, (int)col]);

            return mv;
        }

        public static GaSymMultivector CreateVectorFromColumn(Expr[,] matrix, ulong col)
        {
            var rowsCount = matrix.GetLength(0);

            var mv = new GaSymMultivector(rowsCount);

            for (var row = 0; row < rowsCount; row++)
                mv.SetTermCoef(1, (ulong)row, matrix[row, col]);

            return mv;
        }

        public static GaSymMultivector CreateVectorFromScalars(params Expr[] exprScalars)
        {
            var vSpaceDim = exprScalars.Length;

            var mv = new GaSymMultivector(vSpaceDim);

            for (var index = 0; index < exprScalars.Length; index++)
                mv.SetTermCoef(1, (ulong)index, exprScalars[index]);

            return mv;
        }

        public static GaSymMultivector CreateVectorFromScalars(params string[] exprTextScalars)
        {
            var vSpaceDim = exprTextScalars.Length;

            var mv = new GaSymMultivector(vSpaceDim);

            for (var index = 0; index < exprTextScalars.Length; index++)
                mv.SetTermCoef(1, (ulong)index, exprTextScalars[index].ToExpr(GaSymbolicsUtils.Cas));

            return mv;
        }

        public static GaSymMultivector CreateVectorFromRow(ISymbolicMatrix matrix, int row)
        {
            var vSpaceDim = matrix.ColumnCount;

            var mv = new GaSymMultivector(vSpaceDim);

            for (var col = 0; col < matrix.ColumnCount; col++)
                mv.SetTermCoef(1, (ulong)col, matrix[row, col]);

            return mv;
        }

        public static GaSymMultivector CreateSymbolicVector(int vSpaceDim, string baseCoefName)
        {
            return CreateSymbolic(
                vSpaceDim,
                baseCoefName,
                GaFrameUtils.BasisBladeIDsOfGrade(vSpaceDim.ToVSpaceDimension(), 1)
            );
        }

        public static GaSymMultivector CreateSymbolicKVector(int vSpaceDim, string baseCoefName, int grade)
        {
            return CreateSymbolic(
                vSpaceDim,
                baseCoefName,
                GaFrameUtils.BasisBladeIDsOfGrade(vSpaceDim, grade)
            );
        }

        public static GaSymMultivector CreateSymbolicTerm(int vSpaceDim, string baseCoefName, ulong id)
        {
            return new GaSymMultivector(vSpaceDim)
                .SetTermCoef(
                    id,
                    MathematicaScalar.CreateSymbol(
                        GaSymbolicsUtils.Cas,
                        baseCoefName + id.PatternToString(vSpaceDim)
                    ));
        }

        public static GaSymMultivector CreateSymbolicScalar(int vSpaceDim, string baseCoefName)
        {
            return CreateSymbolicTerm(vSpaceDim, baseCoefName, 0);
        }

        public static GaSymMultivector CreateSymbolicPseudoscalar(int vSpaceDim, string baseCoefName)
        {
            return CreateSymbolicTerm(vSpaceDim, baseCoefName, (1UL << vSpaceDim) - 1);
        }

        public static GaSymMultivector CreateSymbolic(int vSpaceDim, string baseCoefName, IEnumerable<ulong> idsList)
        {
            var resultMv = new GaSymMultivector(vSpaceDim);

            foreach (var id in idsList)
                resultMv.SetTermCoef(
                    id,
                    MathematicaScalar.CreateSymbol(
                        GaSymbolicsUtils.Cas,
                        baseCoefName + id.PatternToString(vSpaceDim)
                    ));

            return resultMv;
        }


        public static GaSymMultivector operator -(GaSymMultivector mv)
        {
            var resultMv = CreateZero(mv.VSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv.SetTermCoef(term.Key, -term.Value);

            return resultMv;
        }

        public static GaSymMultivector operator +(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            var resultMv = CreateCopyTemp(mv1);

            foreach (var term in mv2.NonZeroExprTerms)
                resultMv.AddFactor(term.Key, term.Value);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator -(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            var resultMv = CreateCopyTemp(mv1);

            foreach (var term in mv2.NonZeroExprTerms)
                resultMv.AddFactor(term.Key, Mfs.Minus[term.Value]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(GaSymMultivector mv1, MathematicaScalar s)
        {
            if (s.IsNullOrZero()) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s.Expression]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(GaSymMultivector mv1, Expr s)
        {
            if (s.IsNullOrZero()) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(GaSymMultivector mv1, double s)
        {
            if (Math.Abs(s) <= 0.0d) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s.ToExpr()]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(GaSymMultivector mv1, int s)
        {
            if (s == 0) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s.ToExpr()]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(Expr s, GaSymMultivector mv1)
        {
            if (s.IsNullOrZero()) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(double s, GaSymMultivector mv1)
        {
            if (Math.Abs(s) <= 0.0d) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s.ToExpr()]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(int s, GaSymMultivector mv1)
        {
            if (s == 0) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s.ToExpr()]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator *(MathematicaScalar s, GaSymMultivector mv1)
        {
            if (s.IsNullOrZero()) return CreateZero(mv1.VSpaceDimension);

            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Times[term.Value, s.Expression]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator /(GaSymMultivector mv1, MathematicaScalar s)
        {
            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Divide[term.Value, s.Expression]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator /(GaSymMultivector mv1, Expr s)
        {
            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Divide[term.Value, s]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator /(GaSymMultivector mv1, double s)
        {
            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Divide[term.Value, s.ToExpr()]);

            return resultMv.ToMultivector();
        }

        public static GaSymMultivector operator /(GaSymMultivector mv1, int s)
        {
            var resultMv = CreateZeroTemp(mv1.VSpaceDimension);

            foreach (var term in mv1.NonZeroExprTerms)
                resultMv.SetTermCoef(term.Key, Mfs.Divide[term.Value, s.ToExpr()]);

            return resultMv.ToMultivector();
        }


        internal GaBtrInternalNode<Expr> TermsTree { get; }


        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public IEnumerable<ulong> BasisBladeIds
            => TermsTree
                .GetNodeInfo(VSpaceDimension, 0)
                .GetTreeLeafNodeIDs()
                .Select(k => k);

        public IEnumerable<ulong> NonZeroBasisBladeIds
            => TermsTree
                .GetNodeInfo(VSpaceDimension, 0)
                .GetTreeLeafNodesInfo()
                .Where(nodeInfo => !nodeInfo.Value.IsNullOrZero())
                .Select(nodeInfo => nodeInfo.Id);

        public int VSpaceDimension { get; }

        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;

        public int Size 
            => (int)GaSpaceDimension;

        MathematicaScalar ISymbolicVector.this[int id] 
            => TermsTree.GetLeafValue(VSpaceDimension, (ulong)id)?.ToMathematicaScalar() 
               ?? GaSymbolicsUtils.Constants.Zero;

        public bool IsTemp
            => false;

        public ulong TermsCount
            => (ulong)TermsTree.GetTreeLeafNodes().LongCount();

        public Expr this[int grade, ulong index] 
            => this[GaFrameUtils.BasisBladeId(grade, index)];

        public Expr this[ulong id] 
            => TermsTree.GetLeafValue(VSpaceDimension, id)
               ?? Expr.INT_ZERO;

        public IEnumerable<MathematicaScalar> BasisBladeScalars 
            => TermsTree
                .GetTreeLeafValues()
                .Select(value => value?.ToMathematicaScalar() ?? GaSymbolicsUtils.Constants.Zero);

        public IEnumerable<Expr> BasisBladeExprScalars 
            => TermsTree
                .GetTreeLeafValues()
                .Select(value => value ?? Expr.INT_ZERO);

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars 
            => TermsTree
            .GetTreeLeafValues()
            .Where(value => !value.IsNullOrZero())
            .Select(value => value.ToMathematicaScalar());

        public IEnumerable<Expr> NonZeroBasisBladeExprScalars
            => TermsTree
            .GetTreeLeafValues()
            .Where(value => !value.IsNullOrZero())
            .Select(value => value);

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> Terms
            => TermsTree
                .GetNodeInfo(VSpaceDimension, 0)
                .GetTreeNodesInfo()
                .Select(
                    pair => new KeyValuePair<ulong, MathematicaScalar>(
                        pair.Id, 
                        pair.Value?.ToMathematicaScalar() ?? GaSymbolicsUtils.Constants.Zero
                        )
                    );

        public IEnumerable<KeyValuePair<ulong, Expr>> ExprTerms
            => TermsTree
                .GetNodeInfo(VSpaceDimension, 0)
                .GetTreeNodesInfo()
                .Select(
                    pair => new KeyValuePair<ulong, Expr>(
                        pair.Id,
                        pair.Value ?? Expr.INT_ZERO
                    )
                );

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> NonZeroTerms
            => TermsTree
                .GetNodeInfo(VSpaceDimension, 0)
                .GetTreeNodesInfo()
                .Where(pair => !pair.Value.IsNullOrZero())
                .Select(
                    pair => new KeyValuePair<ulong, MathematicaScalar>(
                        pair.Id, 
                        pair.Value.ToMathematicaScalar()
                        )
                    );

        public IEnumerable<KeyValuePair<ulong, Expr>> NonZeroExprTerms
            => TermsTree
                .GetNodeInfo(VSpaceDimension, 0)
                .GetTreeNodesInfo()
                .Where(pair => !pair.Value.IsNullOrZero())
                .Select(
                    pair => new KeyValuePair<ulong, Expr>(
                        pair.Id,
                        pair.Value ?? Expr.INT_ZERO
                    )
                );

        private GaSymMultivector(int vSpaceDim)
        {
            CasInterface = GaSymbolicsUtils.Cas;
            VSpaceDimension = vSpaceDim;
            TermsTree = new GaBtrInternalNode<Expr>();
        }


        public bool IsTerm()
        {
            return TermsTree.GetTreeLeafNodes().Count() <= 1;
        }

        public bool IsScalar()
        {
            return TermsTree.HasNoChildNodes ||
                   TermsTree
                       .GetNodeInfo(VSpaceDimension, 0)
                       .GetTreeNodesInfo()
                       .All(p => p.Id == 0 || p.Value.IsNullOrZero());
        }

        public bool IsZero()
        {
            return TermsTree.HasNoChildNodes || 
                   TermsTree
                       .GetTreeLeafValues()
                       .All(v => v.IsNullOrZero());
        }

        public bool IsNearNumericZero(double epsilon)
        {
            return TermsTree.HasNoChildNodes ||
                   TermsTree
                       .GetTreeLeafValues()
                       .All(v => v.IsNullOrNearNumericZero(epsilon));
        }

        public bool IsEqualZero()
        {
            return TermsTree.HasNoChildNodes ||
                   BasisBladeScalars.All(s => s.IsNullOrEqualZero());
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
            return this;
        }

        public bool ContainsBasisBlade(ulong id)
        {
            return TermsTree.ContainsLeafNodeId(VSpaceDimension, id);
        }

        public void Simplify()
        {
            var terms = NonZeroTerms.ToArray();
            ResetToZero();

            foreach (var term in terms)
            {
                term.Value.Simplify();
                SetTermCoef(term.Key, term.Value.Expression);
            }
        }

        public bool IsFullVector()
        {
            return false;
        }

        public bool IsSparseVector()
        {
            return true;
        }

        public ISymbolicVector Times(ISymbolicMatrix m)
        {
            throw new NotImplementedException();
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

        public IEnumerator<KeyValuePair<ulong, Expr>> GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }

        public GaSymMultivector ResetToZero()
        {
            TermsTree.ResetInternalChildNode0();
            TermsTree.ResetInternalChildNode1();

            return this;
        }

        public GaSymMultivector AddFactor(int grade, ulong index, MathematicaScalar coef)
        {
            return AddFactor(
                GaFrameUtils.BasisBladeId(grade, index),
                coef.Expression
                );
        }

        public GaSymMultivector AddFactor(ulong id, MathematicaScalar coef)
        {
            return AddFactor(id, coef.Expression);
        }

        public GaSymMultivector AddFactor(int grade, ulong index, Expr coef)
        {
            return AddFactor(
                GaFrameUtils.BasisBladeId(grade, index),
                coef
            );
        }

        public GaSymMultivector AddFactor(ulong id, Expr coef)
        {
            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1UL << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1 && !node.ChildNode1.Value.IsNullOrZero())
                    node.LeafChildNode1.Value = Mfs.Plus[node.ChildNode1.Value, coef];
                else
                    node.ResetLeafChildNode1(coef);

                return this;
            }

            if (node.HasChildNode0 && !node.ChildNode0.Value.IsNullOrZero())
                node.LeafChildNode0.Value = Mfs.Plus[node.ChildNode0.Value, coef];
            else
                node.ResetLeafChildNode0(coef);

            return this;
        }

        public GaSymMultivector AddFactor(ulong id, bool isNegative, Expr coef)
        {
            var deltaValue = isNegative ? Mfs.Minus[coef] : coef;

            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1UL << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                if (node.HasChildNode1 && !node.ChildNode1.Value.IsNullOrZero())
                    node.LeafChildNode1.Value = Mfs.Plus[node.ChildNode1.Value, deltaValue];
                else
                    node.ResetLeafChildNode1(deltaValue);

                return this;
            }

            if (node.HasChildNode0 && !node.ChildNode0.Value.IsNullOrZero())
                node.LeafChildNode0.Value = Mfs.Plus[node.ChildNode0.Value, deltaValue];
            else
                node.ResetLeafChildNode0(deltaValue);

            return this;
        }

        public GaSymMultivector SetTermCoef(int grade, ulong index, MathematicaScalar coef)
        {
            return SetTermCoef(
                GaFrameUtils.BasisBladeId(grade, index),
                coef.Expression
                );
        }

        public GaSymMultivector SetTermCoef(ulong id, MathematicaScalar coef)
        {
            return SetTermCoef(id, coef.Expression);
        }

        public GaSymMultivector SetTermCoef(int grade, ulong index, Expr coef)
        {
            return SetTermCoef(
                GaFrameUtils.BasisBladeId(grade, index),
                coef
            );
        }

        public GaSymMultivector SetTermCoef(ulong id, bool isNegative, Expr coef)
        {
            return SetTermCoef(
                id,
                isNegative ? Mfs.Minus[coef] : coef
            );
        }

        public GaSymMultivector SetTermCoef(ulong id, Expr coef)
        {
            var node = TermsTree;
            for (var i = VSpaceDimension - 1; i > 0; i--)
            {
                var bitPattern = (1UL << i) & id;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            if ((1 & id) != 0)
            {
                node.ResetLeafChildNode1(coef);

                return this;
            }

            node.ResetLeafChildNode0(coef);

            return this;
        }


        public KeyValuePair<ulong, MathematicaScalar> GetTerm(ulong id)
        {
            return new KeyValuePair<ulong, MathematicaScalar>(id, this[id].ToMathematicaScalar());
        }

        public KeyValuePair<ulong, Expr> GetExprTerm(ulong id)
        {
            return new KeyValuePair<ulong, Expr>(id, this[id]);
        }

        public GaSymMultivector GetPartById(Func<ulong, bool> idSelectionFunc)
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => idSelectionFunc(t.Key)))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaSymMultivector GetPartByGrade(Func<int, bool> gradeSelectionFunc)
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => gradeSelectionFunc(t.Key.BasisBladeGrade())))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = CreateZero(VSpaceDimension);

            foreach (var id in GaFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public GaSymMultivector GetKVectorPart(int grade)
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            if (grade < 0 || grade > VSpaceDimension)
                return resultMv;

            foreach (var term in NonZeroTerms.Where(t => t.Key.BasisBladeGrade() == grade))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaSymMultivector GetEvenPart()
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => (t.Key.BasisBladeGrade() & 1) == 0))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public GaSymMultivector GetOddPart()
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroTerms.Where(t => (t.Key.BasisBladeGrade() & 1) == 1))
                resultMv.SetTermCoef(term.Key, term.Value);

            return resultMv;
        }

        public Expr[] VectorPartToColumnVector()
        {
            var columnVector = new Expr[VSpaceDimension];

            for (var index = 0; index < VSpaceDimension; index++)
                columnVector[index] = this[1, (ulong)index];

            return columnVector;
        }

        public Expr[] KVectorPartToColumnVector(int grade)
        {
            var columnVectorLength = (int)GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            var columnVector = new Expr[columnVectorLength];

            for (var index = 0; index < columnVectorLength; index++)
                columnVector[index] = this[grade, (ulong)index];

            return columnVector;
        }

        public Expr[] ToColumnVector()
        {
            var columnVector = new Expr[GaSpaceDimension];

            foreach (var term in NonZeroExprTerms)
                columnVector[term.Key] = term.Value;

            return columnVector;
        }

        public Dictionary<int, GaSymMultivector> GetKVectorParts()
        {
            var kVectorsList = new Dictionary<int, GaSymMultivector>();

            foreach (var term in NonZeroTerms)
            {
                var grade = term.Key.BasisBladeGrade();

                if (kVectorsList.TryGetValue(grade, out var mv) == false)
                {
                    mv = new GaSymMultivector(VSpaceDimension);

                    kVectorsList.Add(grade, mv);
                }

                mv.AddFactor(term.Key, term.Value);
            }

            return kVectorsList;
        }



        public GaSymMultivector Reverse()
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroExprTerms)
                resultMv.SetTermCoef(
                    term.Key, 
                    term.Key.BasisBladeIdHasNegativeReverse(),
                    term.Value
                );

            return resultMv;
        }

        public GaSymMultivector GradeInv()
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroExprTerms)
                resultMv.SetTermCoef(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeGradeInv(),
                    term.Value
                );

            return resultMv;
        }

        public GaSymMultivector CliffConj()
        {
            var resultMv = new GaSymMultivector(VSpaceDimension);

            foreach (var term in NonZeroExprTerms)
                resultMv.SetTermCoef(
                    term.Key,
                    term.Key.BasisBladeIdHasNegativeCliffConj(),
                    term.Value
                );

            return resultMv;
        }


        IEnumerator<MathematicaScalar> IEnumerable<MathematicaScalar>.GetEnumerator()
        {
            return NonZeroBasisBladeScalars.GetEnumerator();
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
