﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using TextComposerLib.Text.Linear;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Multivectors.Tree
{
    public sealed class GaTreeMultivector 
        : IGaSymMultivector, IGaTreeMultivectorParentNode, ISymbolicVector
    {
        public static GaTreeMultivector CreateZero(ulong gaSpaceDim)
        {
            return new GaTreeMultivector(gaSpaceDim);
        }

        public static GaTreeMultivector CreateTerm(ulong gaSpaceDim, ulong id, MathematicaScalar coef)
        {
            var resultMv = new GaTreeMultivector(gaSpaceDim) { [id] = coef.Expression };

            return resultMv;
        }

        public static GaTreeMultivector CreateBasisBlade(ulong gaSpaceDim, ulong id)
        {
            var resultMv = new GaTreeMultivector(gaSpaceDim) { [id] = Expr.INT_ONE };

            return resultMv;
        }

        public static GaTreeMultivector CreateScalar(ulong gaSpaceDim, MathematicaScalar coef)
        {
            var resultMv = new GaTreeMultivector(gaSpaceDim) { [0] = coef.Expression };

            return resultMv;
        }

        public static GaTreeMultivector CreatePseudoScalar(ulong gaSpaceDim, MathematicaScalar coef)
        {
            var resultMv = new GaTreeMultivector(gaSpaceDim) { [gaSpaceDim - 1] = coef.Expression };

            return resultMv;
        }

        public static GaTreeMultivector CreateCopy(GaTreeMultivector mv)
        {
            var resultMv = new GaTreeMultivector(mv.GaSpaceDimension);
            var sourceNodeStack = new Stack<IGaTreeMultivectorNode>();
            var targetNodeStack = new Stack<IGaTreeMultivectorNode>();
            sourceNodeStack.Push(mv);
            targetNodeStack.Push(resultMv);

            while (sourceNodeStack.Count > 0)
            {
                var sourceNode = sourceNodeStack.Pop();
                var targetNode = targetNodeStack.Pop();

                var childNode = sourceNode.GetLeftChild();
                if (!ReferenceEquals(childNode, null))
                {
                    targetNodeStack.Push(
                        childNode.IsInternal
                            ? (IGaTreeMultivectorNode)targetNode.SetLeftChildToInternalNode()
                            : targetNode.SetLeftChildToLeafNode(childNode.AsLeafNode().Value)
                    );
                }

                childNode = sourceNode.GetRightChild();
                if (!ReferenceEquals(childNode, null))
                {
                    targetNodeStack.Push(
                        childNode.IsInternal 
                        ? (IGaTreeMultivectorNode)targetNode.SetRightChildToInternalNode()
                        : targetNode.SetRightChildToLeafNode(childNode.AsLeafNode().Value)
                        );
                }
            }

            return resultMv;
        }

        public static GaTreeMultivector CreateMapped(GaTreeMultivector mv, Func<MathematicaScalar, MathematicaScalar> scalarMap)
        {
            var resultMv = CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.Terms)
                resultMv[term.Key] = scalarMap(term.Value).Expression;

            return resultMv;
        }

        public static GaTreeMultivector CreateSymbolic(ulong gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolic(
                gaSpaceDim,
                baseCoefName,
                Enumerable.Range(0, (int)gaSpaceDim).Select(i => (ulong)i)
                );
        }

        public static GaTreeMultivector CreateSymbolicVector(ulong gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolic(
                gaSpaceDim,
                baseCoefName,
                GaFrameUtils.BasisBladeIDsOfGrade(gaSpaceDim.ToVSpaceDimension(), 1)
            );
        }

        public static GaTreeMultivector CreateSymbolicKVector(ulong gaSpaceDim, string baseCoefName, int grade)
        {
            return CreateSymbolic(
                gaSpaceDim,
                baseCoefName,
                GaFrameUtils.BasisBladeIDsOfGrade(gaSpaceDim.ToVSpaceDimension(), grade)
            );
        }

        public static GaTreeMultivector CreateSymbolicTerm(ulong gaSpaceDim, string baseCoefName, ulong id)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            return new GaTreeMultivector(gaSpaceDim)
            {
                [id] = MathematicaScalar.CreateSymbol(
                    GaSymbolicsUtils.Cas,
                    baseCoefName + id.PatternToString(vSpaceDim)
                ).Expression
            };
        }

        public static GaTreeMultivector CreateSymbolicScalar(ulong gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolicTerm(gaSpaceDim, baseCoefName, 0);
        }

        public static GaTreeMultivector CreateSymbolicPseudoscalar(ulong gaSpaceDim, string baseCoefName)
        {
            return CreateSymbolicTerm(gaSpaceDim, baseCoefName, gaSpaceDim - 1);
        }

        public static GaTreeMultivector CreateSymbolic(ulong gaSpaceDim, string baseCoefName, IEnumerable<ulong> idsList)
        {
            var resultMv = new GaTreeMultivector(gaSpaceDim);
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            foreach (var id in idsList)
                resultMv[id] =
                    MathematicaScalar.CreateSymbol(
                        GaSymbolicsUtils.Cas,
                        baseCoefName + id.PatternToString(vSpaceDim)
                        ).Expression;

            return resultMv;
        }


        public IEnumerable<ulong> BasisBladeIds { get; }

        public IEnumerable<ulong> NonZeroBasisBladeIds { get; }

        public IEnumerable<MathematicaScalar> BasisBladeScalars { get; }

        public IEnumerable<Expr> BasisBladeExprScalars { get; }

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars { get; }

        public IEnumerable<Expr> NonZeroBasisBladeExprScalars { get; }

        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection 
            => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator 
            => CasInterface.Evaluator;

        public MathematicaConstants CasConstants 
            => CasInterface.Constants;

        public int Size 
            => (int)GaSpaceDimension;

        MathematicaScalar ISymbolicVector.this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsRoot { get; } = true;

        public bool IsInternal { get; } = false;

        public bool IsLeaf { get; } = false;

        public IGaTreeMultivectorNode LeftChild { get; internal set; }

        public IGaTreeMultivectorNode RightChild { get; internal set; }

        public bool HasLeftChild => !ReferenceEquals(LeftChild, null);

        public bool HasRightChild => !ReferenceEquals(RightChild, null);

        public Expr this[int grade, ulong index]
        {
            get { return this[GaFrameUtils.BasisBladeId(grade, index)]; }
            set { this[GaFrameUtils.BasisBladeId(grade, index)] = value; }
        }

        public Expr this[ulong id]
        {
            get
            {
                IGaTreeMultivectorNode node = this;
                for (var i = 0; i < VSpaceDimension - 1; i++)
                {
                    node = node.GetChild(id);

                    if (ReferenceEquals(node, null))
                        return Expr.INT_ZERO;

                    id >>= 1;
                }

                var leafNode = node.GetChild(id) as GaTreeMultivectorLeaf;
                return ReferenceEquals(leafNode, null)
                    ? Expr.INT_ZERO
                    : leafNode.Value.Expression;
            }
            set
            {
                IGaTreeMultivectorNode node = this;
                for (var i = 0; i < VSpaceDimension - 1; i++)
                {
                    node = node.GetOrSetChildToInternalNode(id);

                    Debug.Assert(!ReferenceEquals(node, null));

                    id >>= 1;
                }

                var leafNode = node.GetOrSetChildToLeafNode(id, value.ToMathematicaScalar());
                Debug.Assert(!ReferenceEquals(leafNode, null));
            }
        }

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> Terms
        {
            get
            {
                var levelStack = new Stack<int>();
                var idStack = new Stack<ulong>();
                var nodeStack = new Stack<IGaTreeMultivectorNode>();

                levelStack.Push(0);
                idStack.Push(0);
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var level = levelStack.Pop();
                    var id = idStack.Pop();
                    var node = nodeStack.Pop();

                    var leafNode = node as GaTreeMultivectorLeaf;
                    if (!ReferenceEquals(leafNode, null))
                    {
                        yield return 
                            new KeyValuePair<ulong, MathematicaScalar>(id, leafNode.Value);

                        continue;
                    }

                    var leftChild = node.GetLeftChild();
                    if (!ReferenceEquals(leftChild, null))
                    {
                        levelStack.Push(level + 1);
                        idStack.Push((1UL << level) | id);
                        nodeStack.Push(leftChild);
                    }

                    var rightChild = node.GetRightChild();
                    if (!ReferenceEquals(rightChild, null))
                    {
                        levelStack.Push(level + 1);
                        idStack.Push(id);
                        nodeStack.Push(rightChild);
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, Expr>> ExprTerms
            => Terms.Select(p => new KeyValuePair<ulong, Expr>(p.Key, p.Value.Expression));

        public IEnumerable<KeyValuePair<ulong, MathematicaScalar>> NonZeroTerms
            => Terms.Where(p => !p.Value.IsNullOrZero());

        public IEnumerable<KeyValuePair<ulong, Expr>> NonZeroExprTerms
            => NonZeroTerms.Select(p => new KeyValuePair<ulong, Expr>(p.Key, p.Value.Expression));


        private GaTreeMultivector(ulong gaSpaceDim)
        {
            CasInterface = GaSymbolicsUtils.Cas;
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
        }


        public bool ContainsBasisBlade(ulong id)
        {
            throw new NotImplementedException();
        }

        public bool IsTemp 
            => false;

        public ulong TermsCount { get; }

        public void Simplify()
        {
            throw new NotImplementedException();
        }

        public bool IsTerm()
        {
            throw new NotImplementedException();
        }

        public bool IsScalar()
        {
            throw new NotImplementedException();
        }

        public bool IsZero()
        {
            throw new NotImplementedException();
        }

        public bool IsEqualZero()
        {
            throw new NotImplementedException();
        }

        public MathematicaScalar[] TermsToArray()
        {
            throw new NotImplementedException();
        }

        public Expr[] TermsToExprArray()
        {
            throw new NotImplementedException();
        }

        public GaSymMultivector ToMultivector()
        {
            throw new NotImplementedException();
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(VSpaceDimension);

            foreach (var id in GaFrameUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public bool IsFullVector()
        {
            throw new NotImplementedException();
        }

        public bool IsSparseVector()
        {
            throw new NotImplementedException();
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

        IEnumerator<KeyValuePair<ulong, Expr>> IEnumerable<KeyValuePair<ulong, Expr>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<MathematicaScalar> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string ToTreeString()
        {
            var textComposer = new LinearTextComposer();

            var levelStack = new Stack<int>();
            var idStack = new Stack<ulong>();
            var nodeStack = new Stack<IGaTreeMultivectorNode>();

            levelStack.Push(0);
            idStack.Push(0UL);
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var level = levelStack.Pop();
                var id = idStack.Pop();
                var node = nodeStack.Pop();

                var leafNode = node as GaTreeMultivectorLeaf;
                if (!ReferenceEquals(leafNode, null))
                {
                    textComposer
                        .AppendAtNewLine("".PadLeft(level * 2, ' '))
                        .Append("Leaf <")
                        .Append(id.PatternToString(level))
                        .Append("> { ")
                        .Append(id.BasisBladeName())
                        .Append(" = ")
                        .Append(leafNode.Value.ExpressionText)
                        .AppendLine(" }");

                    continue;
                }
                
                if (node.IsRoot)
                {
                    textComposer
                        .AppendAtNewLine("".PadLeft(level * 2, ' '))
                        .AppendLine("Multivector");
                }
                else
                {
                    textComposer
                        .AppendAtNewLine("".PadLeft(level * 2, ' '))
                        .Append("Node <")
                        .Append(id.PatternToString(level))
                        .AppendLine(">");
                }

                var rightChild = node.GetRightChild();
                if (!ReferenceEquals(rightChild, null))
                {
                    levelStack.Push(level + 1);
                    idStack.Push(id);
                    nodeStack.Push(rightChild);
                }

                var leftChild = node.GetLeftChild();
                if (!ReferenceEquals(leftChild, null))
                {
                    levelStack.Push(level + 1);
                    idStack.Push((1UL << level) | id);
                    nodeStack.Push(leftChild);
                }
            }

            return textComposer.ToString();
        }
    }
}
