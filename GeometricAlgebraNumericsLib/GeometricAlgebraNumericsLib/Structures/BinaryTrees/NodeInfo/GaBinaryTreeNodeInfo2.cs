using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo
{
    public sealed class GaBinaryTreeNodeInfo2<T>
    {
        public static Stack<GaBinaryTreeNodeInfo2<T>> CreateStack(int treeDepth, IGaBtrNode<T> node1, IGaBtrNode<T> node2)
        {
            var stack = new Stack<GaBinaryTreeNodeInfo2<T>>();

            var rootNodeInfo = new GaBinaryTreeNodeInfo2<T>(
                treeDepth, 
                0, 
                0, 
                node1, 
                node2
            );

            stack.Push(rootNodeInfo);

            return stack;
        }

        
        public IGaBtrNode<T> Node1 { get; }

        public IGaBtrNode<T> Node2 { get; }

        public int TreeDepth { get; }

        public ulong Id1 { get; }

        public ulong Id2 { get; }

        public ulong ChildId10 
            => Id1;

        public ulong ChildId11 
            => Id1 | ChildBitMask;

        public ulong ChildId20 
            => Id2;

        public ulong ChildId21 
            => Id2 | ChildBitMask;

        public ulong BitMask
            => 1ul << TreeDepth;

        public ulong ChildBitMask
            => 1ul << (TreeDepth - 1);

        public bool IsLeafNode 
            => TreeDepth == 0;

        public bool IsInternalNode
            => TreeDepth > 0;

        public T Value1
            => Node1.Value;

        public T Value2
            => Node2.Value;

        public IGaBtrNode<T> ChildNode10 
            => Node1.ChildNode0;

        public IGaBtrNode<T> ChildNode11 
            => Node1.ChildNode1;

        public IGaBtrNode<T> ChildNode20 
            => Node2.ChildNode0;

        public IGaBtrNode<T> ChildNode21 
            => Node2.ChildNode1;

        public GaBtrLeafNode<T> LeafChildNode10 
            => Node1.LeafChildNode0;

        public GaBtrLeafNode<T> LeafChildNode11 
            => Node1.LeafChildNode1;

        public GaBtrLeafNode<T> LeafChildNode20 
            => Node2.LeafChildNode0;

        public GaBtrLeafNode<T> LeafChildNode21 
            => Node2.LeafChildNode1;

        public bool HasChildNode10 
            => Node1.HasChildNode0;

        public bool HasChildNode11 
            => Node1.HasChildNode1;

        public bool HasChildNode20 
            => Node2.HasChildNode0;

        public bool HasChildNode21 
            => Node2.HasChildNode1;

        public bool HasNoChildNodes1 
            => Node1.HasNoChildNodes;

        public bool HasNoChildNodes2 
            => Node2.HasNoChildNodes;


        public GaBinaryTreeNodeInfo2(int treeDepth, ulong id1, ulong id2, IGaBtrNode<T> node1, IGaBtrNode<T> node2)
        {
            TreeDepth = treeDepth;
            Id1 = id1;
            Id2 = id2;
            Node1 = node1;
            Node2 = node2;
        }

        
        public GaBinaryTreeNodeInfo2<T> GetChildNodeInfo00()
        { 
            return new GaBinaryTreeNodeInfo2<T>(
                TreeDepth - 1, 
                ChildId10,
                ChildId20,
                Node1.ChildNode0,
                Node2.ChildNode0
            ); 
        }

        public GaBinaryTreeNodeInfo2<T> GetChildNodeInfo01()
        { 
            return new GaBinaryTreeNodeInfo2<T>(
                TreeDepth - 1, 
                ChildId10,
                ChildId21,
                Node1.ChildNode0,
                Node2.ChildNode1
            ); 
        }

        public GaBinaryTreeNodeInfo2<T> GetChildNodeInfo10()
        { 
            return new GaBinaryTreeNodeInfo2<T>(
                TreeDepth - 1, 
                ChildId11,
                ChildId20,
                Node1.ChildNode1,
                Node2.ChildNode0
            ); 
        }

        public GaBinaryTreeNodeInfo2<T> GetChildNodeInfo11()
        { 
            return new GaBinaryTreeNodeInfo2<T>(
                TreeDepth - 1, 
                ChildId11,
                ChildId21,
                Node1.ChildNode1,
                Node2.ChildNode1
            ); 
        }
    }
}