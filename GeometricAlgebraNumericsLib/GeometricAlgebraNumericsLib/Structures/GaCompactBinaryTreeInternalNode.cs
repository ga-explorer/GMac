using System.Diagnostics;

namespace GeometricAlgebraNumericsLib.Structures
{
    public struct GaCompactBinaryTreeInternalNode<T> : IGaCompactBinaryTreeNode<T>
    {
        public int TreeDepth { get; }

        public int ChildNodeIndex0 { get; }

        public int ChildNodeIndex1 { get; }

        public ulong Id { get; }

        public T Value 
            => default(T);

        public ulong BitMask
            => 1ul << TreeDepth;

        public bool HasChildNode0 
            => ChildNodeIndex0 >= 0;

        public bool HasChildNode1 
            => ChildNodeIndex1 >= 0;

        public bool HasNoChildNodes 
            => ChildNodeIndex0 < 0 && ChildNodeIndex1 < 0;

        public bool IsInternalNode 
            => true;

        public bool IsLeafNode 
            => false;

        public bool IsEmptyNode 
            => false;


        internal GaCompactBinaryTreeInternalNode(int id, int treeDepth, int childIndex0, int childIndex1)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            TreeDepth = treeDepth;
            Id = (ulong)id;
            ChildNodeIndex0 = childIndex0;
            ChildNodeIndex1 = childIndex1;
        }

        internal GaCompactBinaryTreeInternalNode(ulong id, int treeDepth, int childIndex0, int childIndex1)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            Id = id;
            TreeDepth = treeDepth;
            
            ChildNodeIndex0 = childIndex0;
            ChildNodeIndex1 = childIndex1;
        }

        internal GaCompactBinaryTreeInternalNode(GaCompactBinaryTreeInternalNode<T> node)
        {
            Id = node.Id;
            TreeDepth = node.TreeDepth;
            
            ChildNodeIndex0 = node.ChildNodeIndex0;
            ChildNodeIndex1 = node.ChildNodeIndex1;
        }


        //internal GaCompactBinaryTreeInternalNode<T> GetUpdatedCopyChild0(int childIndex)
        //{
        //    return new GaCompactBinaryTreeInternalNode<T>(
        //        Id,
        //        TreeDepth,
        //        childIndex,
        //        ChildNodeIndex1
        //    );
        //}

        //internal GaCompactBinaryTreeInternalNode<T> GetUpdatedCopyChild1(int childIndex)
        //{
        //    return new GaCompactBinaryTreeInternalNode<T>(
        //        Id,
        //        TreeDepth,
        //        ChildNodeIndex0,
        //        childIndex
        //    );
        //}

        internal GaCompactBinaryTreeInternalNode<T> GetUpdatedCopy(int rightChild, int childIndex)
        {
            return rightChild == 0
                ? new GaCompactBinaryTreeInternalNode<T>(
                    Id,
                    TreeDepth,
                    childIndex,
                    ChildNodeIndex1
                )
                : new GaCompactBinaryTreeInternalNode<T>(
                    Id,
                    TreeDepth,
                    ChildNodeIndex0,
                    childIndex
                );
        }

        internal GaCompactBinaryTreeInternalNode<T> GetUpdatedCopy(ulong rightChild, int childIndex)
        {
            return rightChild == 0ul
                ? new GaCompactBinaryTreeInternalNode<T>(
                    Id,
                    TreeDepth,
                    childIndex,
                    ChildNodeIndex1
                )
                : new GaCompactBinaryTreeInternalNode<T>(
                    Id,
                    TreeDepth,
                    ChildNodeIndex0,
                    childIndex
                );
        }
    }
}