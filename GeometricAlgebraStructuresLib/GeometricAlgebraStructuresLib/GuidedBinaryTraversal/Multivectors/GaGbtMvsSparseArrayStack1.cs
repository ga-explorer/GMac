using System;
using GeometricAlgebraStructuresLib.Storage;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtMvsSparseArrayStack1<T>
        : GaGbtStack1, IGaGbtMultivectorStorageStack1<T>
    {
        public static GaGbtMvsSparseArrayStack1<T> Create(int capacity, IGaMultivectorStorage<T> multivectorStorage)
        {
            return new GaGbtMvsSparseArrayStack1<T>(capacity, multivectorStorage);
        }


        private int[] BinaryTreeNodeIndexArray { get; }


        public IGaMultivectorStorage<T> MultivectorStorage { get; }

        public Tuple<int, int> TosBinaryTreeNode { get; private set; }

        public T TosValue { get; private set; }

        public GaBinaryTree<T> BinaryTree { get; }


        private GaGbtMvsSparseArrayStack1(int capacity, IGaMultivectorStorage<T> multivectorStorage)
            : base(capacity, multivectorStorage.VSpaceDimension, 0ul)
        {
            BinaryTreeNodeIndexArray = new int[capacity];
            MultivectorStorage = multivectorStorage;

            BinaryTree = multivectorStorage.GetBinaryTree();
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            BinaryTreeNodeIndexArray[TosIndex] = BinaryTree.RootNodeIndex;
        }

        public override void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                TosBinaryTreeNode = BinaryTree.GetInternalNodeByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }
            else
            {
                TosValue = BinaryTree.GetLeafNodeValueByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }

            TosIndex--;
        }

        public override bool TosHasChild(int childIndex)
        {
            return (childIndex & 1) == 0
                ? TosBinaryTreeNode.Item1 >= 0
                : TosBinaryTreeNode.Item2 >= 0;
        }

        public override void PushDataOfChild(int childIndex)
        {
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            if ((childIndex & 1) == 0)
            {
                IdArray[TosIndex] = TosChildId0;
                BinaryTreeNodeIndexArray[TosIndex] = TosBinaryTreeNode.Item1;
            }
            else
            {
                IdArray[TosIndex] = TosChildId1;
                BinaryTreeNodeIndexArray[TosIndex] = TosBinaryTreeNode.Item2;
            }
        }
    }
}