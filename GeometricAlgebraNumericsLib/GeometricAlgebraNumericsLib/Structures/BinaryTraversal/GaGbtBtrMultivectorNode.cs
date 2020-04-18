using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    /// <summary>
    /// Guided Binary Traversal node of a Binary Tree Representation Multivector
    /// </summary>
    public sealed class GaGbtBtrMultivectorNode<T> : IGaGbtNode1<T>
    {
        public static GaGbtBtrMultivectorNode<T> CreateRootNode(int treeDepth, IGaBtrNode<T> btrNode)
        {
            return new GaGbtBtrMultivectorNode<T>(treeDepth, 0, btrNode);
        }


        public IGaBtrNode<T> BtrNode { get; }

        public int TreeDepth { get; }

        public ulong Id { get; }

        public T Value 
            => BtrNode.Value;

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));


        private GaGbtBtrMultivectorNode(int treeDepth, ulong id, IGaBtrNode<T> btrNode)
        {
            TreeDepth = treeDepth;
            Id = id;
            BtrNode = btrNode;
        }


        public bool HasChild0()
        {
            return BtrNode.HasChildNode0;
        }

        public bool HasChild1()
        {
            return BtrNode.HasChildNode1;
        }


        public IGaGbtNode1 GetChild0()
        {
            return GetValueChild0();
        }

        public IGaGbtNode1 GetChild1()
        {
            return GetValueChild1();
        }

        public IGaGbtNode1<T> GetValueChild0()
        {
            return new GaGbtBtrMultivectorNode<T>(
                TreeDepth - 1,
                ChildId0,
                BtrNode.ChildNode0
            );
        }

        public IGaGbtNode1<T> GetValueChild1()
        {
            return new GaGbtBtrMultivectorNode<T>(
                TreeDepth - 1,
                ChildId1,
                BtrNode.ChildNode1
            );
        }
    }
}