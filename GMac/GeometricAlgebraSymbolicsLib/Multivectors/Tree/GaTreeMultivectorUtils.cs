using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;

namespace GeometricAlgebraSymbolicsLib.Multivectors.Tree
{
    public static class GaSymTreeMultivectorUtils
    {
        #region Tree Organization Methods

        public static GaTreeMultivector AsRootNode(this IGaTreeMultivectorNode mv)
        {
            return mv as GaTreeMultivector;
        }

        public static GaTreeMultivectorNode AsInternalNode(this IGaTreeMultivectorNode mv)
        {
            return mv as GaTreeMultivectorNode;
        }

        public static GaTreeMultivectorLeaf AsLeafNode(this IGaTreeMultivectorNode mv)
        {
            return mv as GaTreeMultivectorLeaf;
        }


        public static IGaTreeMultivectorNode GetLeftChild(this IGaTreeMultivectorNode mv)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null)) return rootNode.LeftChild;

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null)) return internalNode.LeftChild;

            return null;
        }

        public static IGaTreeMultivectorNode GetRightChild(this IGaTreeMultivectorNode mv)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null)) return rootNode.RightChild;

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null)) return internalNode.RightChild;

            return null;
        }

        public static IGaTreeMultivectorNode GetChild(this IGaTreeMultivectorNode mv, int index)
        {
            return (index & 1) == 0
                ? mv.GetRightChild()
                : mv.GetLeftChild();
        }


        public static GaTreeMultivectorNode SetLeftChildToInternalNode(this IGaTreeMultivectorNode mv)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = new GaTreeMultivectorNode();
                rootNode.LeftChild = childNode;
                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = new GaTreeMultivectorNode();
                internalNode.LeftChild = childNode;
                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorNode SetRightChildToInternalNode(this IGaTreeMultivectorNode mv)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = new GaTreeMultivectorNode();
                rootNode.RightChild = childNode;
                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = new GaTreeMultivectorNode();
                internalNode.RightChild = childNode;
                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorNode SetChildToInternalNode(this IGaTreeMultivectorNode mv, int index)
        {
            return (index & 1) == 0
                ? mv.SetRightChildToInternalNode()
                : mv.SetLeftChildToInternalNode();
        }


        public static GaTreeMultivectorLeaf SetLeftChildToLeafNode(this IGaTreeMultivectorNode mv, MathematicaScalar value)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = new GaTreeMultivectorLeaf(value);
                rootNode.LeftChild = childNode;
                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = new GaTreeMultivectorLeaf(value);
                internalNode.LeftChild = childNode;
                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorLeaf SetRightChildToLeafNode(this IGaTreeMultivectorNode mv, MathematicaScalar value)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = new GaTreeMultivectorLeaf(value);
                rootNode.RightChild = childNode;
                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = new GaTreeMultivectorLeaf(value);
                internalNode.RightChild = childNode;
                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorLeaf SetChildToLeafNode(this IGaTreeMultivectorNode mv, int index, MathematicaScalar value)
        {
            return (index & 1) == 0
                ? mv.SetRightChildToLeafNode(value)
                : mv.SetLeftChildToLeafNode(value);
        }


        public static GaTreeMultivectorNode GetOrSetLeftChildToInternalNode(this IGaTreeMultivectorNode mv)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = rootNode.LeftChild as GaTreeMultivectorNode;
                if (!ReferenceEquals(childNode, null)) return childNode;

                childNode = new GaTreeMultivectorNode();
                rootNode.LeftChild = childNode;

                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = internalNode.LeftChild as GaTreeMultivectorNode;
                if (!ReferenceEquals(childNode, null)) return childNode;

                childNode = new GaTreeMultivectorNode();
                internalNode.LeftChild = childNode;

                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorNode GetOrSetRightChildToInternalNode(this IGaTreeMultivectorNode mv)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = rootNode.RightChild as GaTreeMultivectorNode;
                if (!ReferenceEquals(childNode, null)) return childNode;

                childNode = new GaTreeMultivectorNode();
                rootNode.RightChild = childNode;

                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = internalNode.RightChild as GaTreeMultivectorNode;
                if (!ReferenceEquals(childNode, null)) return childNode;

                childNode = new GaTreeMultivectorNode();
                internalNode.RightChild = childNode;

                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorNode GetOrSetChildToInternalNode(this IGaTreeMultivectorNode mv, int index)
        {
            return (index & 1) == 0
                ? mv.GetOrSetRightChildToInternalNode()
                : mv.GetOrSetLeftChildToInternalNode();
        }


        public static GaTreeMultivectorLeaf GetOrSetLeftChildToLeafNode(this IGaTreeMultivectorNode mv, MathematicaScalar value)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = rootNode.LeftChild as GaTreeMultivectorLeaf;
                if (!ReferenceEquals(childNode, null))
                {
                    childNode.Value = value;
                    return childNode;
                }

                childNode = new GaTreeMultivectorLeaf(value);
                rootNode.LeftChild = childNode;

                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = internalNode.LeftChild as GaTreeMultivectorLeaf;
                if (!ReferenceEquals(childNode, null))
                {
                    childNode.Value = value;
                    return childNode;
                }

                childNode = new GaTreeMultivectorLeaf(value);
                internalNode.LeftChild = childNode;

                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorLeaf GetOrSetRightChildToLeafNode(this IGaTreeMultivectorNode mv, MathematicaScalar value)
        {
            if (ReferenceEquals(mv, null)) return null;

            var rootNode = mv as GaTreeMultivector;
            if (!ReferenceEquals(rootNode, null))
            {
                var childNode = rootNode.RightChild as GaTreeMultivectorLeaf;
                if (!ReferenceEquals(childNode, null))
                {
                    childNode.Value = value;
                    return childNode;
                }

                childNode = new GaTreeMultivectorLeaf(value);
                rootNode.RightChild = childNode;

                return childNode;
            }

            var internalNode = mv as GaTreeMultivectorNode;
            if (!ReferenceEquals(internalNode, null))
            {
                var childNode = internalNode.RightChild as GaTreeMultivectorLeaf;
                if (!ReferenceEquals(childNode, null))
                {
                    childNode.Value = value;
                    return childNode;
                }

                childNode = new GaTreeMultivectorLeaf(value);
                internalNode.RightChild = childNode;

                return childNode;
            }

            return null;
        }

        public static GaTreeMultivectorLeaf GetOrSetChildToLeafNode(this IGaTreeMultivectorNode mv, int index, MathematicaScalar value)
        {
            return (index & 1) == 0
                ? mv.GetOrSetRightChildToLeafNode(value)
                : mv.GetOrSetLeftChildToLeafNode(value);
        }

        #endregion

        #region Outer Product Tree Algorithm
        private static IEnumerable<int> OpOneLevel(List<int> inputList, bool bitFlag)
        {
            if (inputList.Count == 0)
                return bitFlag
                    ? new List<int> { 1, 0 }
                    : new List<int> { 0 };

            return bitFlag
                ? OpAddBitList(inputList, true, false)
                : OpAddBitList(inputList, false);
        }

        private static IEnumerable<int> OpAddBitList(IEnumerable<int> inputList, bool bitFlagA)
        {
            return inputList.Select(
                i => (i << 1) | (bitFlagA ? 1 : 0)
                );
        }

        private static IEnumerable<int> OpAddBitList(IEnumerable<int> inputList, bool bitFlagA, bool bitFlagB)
        {
            foreach (var i in inputList)
            {
                yield return (i << 1) | (bitFlagA ? 1 : 0);
                yield return (i << 1) | (bitFlagB ? 1 : 0);
            }
        }

        private static void OpSign(List<bool> signList, bool currentSign, bool comp, int level, int maxLevel)
        {
            if (level == maxLevel)
                signList.Add(currentSign);
            else
            {
                OpSign(signList, comp ^ currentSign, comp, level + 1, maxLevel);
                OpSign(signList, currentSign, !comp, level + 1, maxLevel);
            }
        }

        private static List<bool> OpSign(int vSpaceDim)
        {
            var signList = new List<bool>();

            OpSign(signList, false, false, 0, vSpaceDim);

            signList.Reverse();

            return signList;
        }

        public static GaTreeMultivector Op(this GaTreeMultivector mv1, GaTreeMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(mv1.GaSpaceDimension);

            var signList = OpSign(mv1.VSpaceDimension);

            for (var basisBladeId = 0; basisBladeId < mv1.GaSpaceDimension; basisBladeId++)
            {
                var indexList = new List<int>();

                indexList =
                    basisBladeId
                    .PatternToBooleans(mv1.VSpaceDimension)
                    .Aggregate(
                        indexList,
                        (currentList, bitFlag) =>
                        OpOneLevel(currentList, bitFlag).ToList()
                        )
                    .Select(idx => idx.ReverseBits(mv1.VSpaceDimension))
                    .ToList();

                var i = 0;
                foreach (var index in indexList)
                {
                    resultMv.AddFactor(
                        basisBladeId, 
                        signList[i], 
                        Mfs.Times[mv1[index], mv2[basisBladeId ^ index]]
                    );

                    i++;
                }
            }

            return resultMv.ToTreeMultivector();
        }
        #endregion

        #region Geometric Product Tree Algorithm
        private static void GpSign(List<bool> signList, int coefId, bool currentSign, bool comp, int level, int maxLevel)
        {
            if (level == maxLevel)
                signList.Add(currentSign);
            else
            {
                var b = (coefId >> level) & 1;

                if (b == 1)
                {
                    GpSign(signList, coefId, comp ^ currentSign, comp, level + 1, maxLevel);
                    GpSign(signList, coefId, currentSign, !comp, level + 1, maxLevel);
                }
                else
                {
                    GpSign(signList, coefId, comp ^ currentSign, !comp, level + 1, maxLevel);
                    GpSign(signList, coefId, currentSign, comp, level + 1, maxLevel);
                }
            }
        }

        private static List<bool> GpSign(int vSpaceDim, int coefId)
        {
            var signList = new List<bool>();

            GpSign(signList, coefId, false, false, 0, vSpaceDim);

            return signList;
        }

        public static GaTreeMultivector EGp(this GaTreeMultivector mv1, GaTreeMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(mv1.GaSpaceDimension);

            var indexList = Enumerable.Range(0, mv1.GaSpaceDimension).Reverse().ToList();

            for (var k = 0; k < mv1.GaSpaceDimension; k++)
            {
                var coefId = k.ReverseBits(mv1.VSpaceDimension);
                var signList = GpSign(mv1.VSpaceDimension, coefId);

                foreach (var index in indexList)
                {
                    var id1 = index.ReverseBits(mv1.VSpaceDimension);
                    var id2 = id1 ^ coefId;

                    resultMv.AddFactor(
                        coefId,
                        signList[mv1.GaSpaceDimension - index - 1],
                        Mfs.Times[mv1[id1], mv2[id2]]
                    );
                }
            }

            return resultMv.ToTreeMultivector();
        }

        internal static GaTreeMultivector Gp(this IReadOnlyList<int> metricScalarsList, GaTreeMultivector mv1, GaTreeMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(mv1.GaSpaceDimension);

            var indexList = Enumerable.Range(0, mv1.GaSpaceDimension).Reverse().ToList();

            for (var k = 0; k < mv1.GaSpaceDimension; k++)
            {
                var coefId = k.ReverseBits(mv1.VSpaceDimension);
                var signList = GpSign(mv1.VSpaceDimension, coefId);

                foreach (var index in indexList)
                {
                    var id1 = index.ReverseBits(mv1.VSpaceDimension);
                    var id2 = id1 ^ coefId;

                    resultMv.AddFactor(
                        coefId,
                        signList[mv1.GaSpaceDimension - index - 1],
                        Mfs.Times[metricScalarsList[id1 & id2].ToExpr(), mv1[id1], mv2[id2]]
                    );
                }
            }

            return resultMv.ToTreeMultivector();
        }

        internal static GaTreeMultivector Gp(this IReadOnlyList<MathematicaScalar> metricScalarsList, GaTreeMultivector mv1, GaTreeMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(mv1.GaSpaceDimension);

            var indexList = Enumerable.Range(0, mv1.GaSpaceDimension).Reverse().ToList();

            for (var k = 0; k < mv1.GaSpaceDimension; k++)
            {
                var coefId = k.ReverseBits(mv1.VSpaceDimension);
                var signList = GpSign(mv1.VSpaceDimension, coefId);

                foreach (var index in indexList)
                {
                    var id1 = index.ReverseBits(mv1.VSpaceDimension);
                    var id2 = id1 ^ coefId;

                    resultMv.AddFactor(
                        coefId, 
                        signList[mv1.GaSpaceDimension - index - 1],
                        Mfs.Times[metricScalarsList[id1 & id2].Expression, mv1[id1], mv2[id2]]
                    );
                }
            }

            return resultMv.ToTreeMultivector();
        }
        #endregion
    }
}
