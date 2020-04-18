using System.Collections.Generic;
using System.Diagnostics;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo
{
    public sealed class GaBinaryTreeGradedNodeInfo1<T>
    {
        public static Stack<GaBinaryTreeGradedNodeInfo1<T>> CreateStack(int treeDepth, ulong activeGradesBitPattern)
        {
            var stack = new Stack<GaBinaryTreeGradedNodeInfo1<T>>();

            var activeGradesBitMask = (1ul << (treeDepth + 2)) - 1;

            var rootNodeInfo = new GaBinaryTreeGradedNodeInfo1<T>(
                treeDepth,
                0,
                activeGradesBitPattern,
                activeGradesBitMask,
                activeGradesBitMask
            );

            stack.Push(rootNodeInfo);

            return stack;
        }


        public ulong Id { get; }

        public ulong IdBitMask
            => 1ul << TreeDepth;

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        //public T Value { get; }

        public int TreeDepth { get; }
        
        public ulong ActiveGradesBitPattern { get; }

        public ulong ActiveGradesBitMask0 { get; }

        public ulong ActiveGradesBitMask1 { get; }

        public ulong ChildActiveGradesBitPattern0
            => ActiveGradesBitPattern & (ActiveGradesBitMask0 >> 1) & ActiveGradesBitMask1;

        public ulong ChildActiveGradesBitPattern1
            => ActiveGradesBitPattern & ActiveGradesBitMask0 & (ActiveGradesBitMask1 << 1);

        public bool HasChildNode0
            => TreeDepth > 0 && ChildActiveGradesBitPattern0 != 0;

        public bool HasChildNode1
            => TreeDepth > 0 && ChildActiveGradesBitPattern1 != 0;

        public bool IsLeafNode 
            => TreeDepth == 0;

        public bool IsLeafParentNode
            => TreeDepth == 1;

        public bool IsInternalNode
            => TreeDepth > 0;


        /// <summary>
        /// Create an internal node info
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="id"></param>
        /// <param name="pattern"></param>
        /// <param name="mask0"></param>
        /// <param name="mask1"></param>
        private GaBinaryTreeGradedNodeInfo1(int treeDepth, ulong id, ulong pattern, ulong mask0, ulong mask1)
        {
            Debug.Assert((pattern & mask0 & mask1) > 0);

            TreeDepth = treeDepth;

            Id = id;

            //Value = default;

            ActiveGradesBitPattern = pattern;
            ActiveGradesBitMask0 = mask0;
            ActiveGradesBitMask1 = mask1;
        }


        /// <summary>
        /// Create node info for child 0
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeGradedNodeInfo1<T> GetChildNodeInfo0()
        {
            return new GaBinaryTreeGradedNodeInfo1<T>(
                TreeDepth - 1, 
                ChildId0,
                ChildActiveGradesBitPattern0,
                ActiveGradesBitMask0 >> 1,
                ActiveGradesBitMask1
            ); 
        }

        /// <summary>
        /// Create node info for child 1
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeGradedNodeInfo1<T> GetChildNodeInfo1()
        {
            return new GaBinaryTreeGradedNodeInfo1<T>(
                TreeDepth - 1,
                ChildId1,
                ChildActiveGradesBitPattern1,
                ActiveGradesBitMask0,
                ActiveGradesBitMask1 << 1
            );
        }

        //public override string ToString()
        //{
        //    var textComposer = new LinearTextComposer();

        //    var nodeStack = new Stack<GaBinaryTreeGradedNodeInfo<T>>();
        //    nodeStack.Push(this);

        //    while (nodeStack.Count > 0)
        //    {
        //        var node = nodeStack.Pop();
        //        var level = TreeDepth - node.TreeDepth;

        //        if (node.IsLeafNode)
        //        {
        //            textComposer
        //                .AppendAtNewLine("<")
        //                .Append(node.Id.PatternToString(level))
        //                .Append("> ")
        //                .Append("".PadRight(level * 2, ' '))
        //                .Append("Leaf { ")
        //                .Append(node.Value.ToString())
        //                .AppendLine(" }");

        //            continue;
        //        }

        //        if (node.TreeDepth == TreeDepth)
        //            textComposer
        //                .AppendAtNewLine("<")
        //                .Append("".PadRight(TreeDepth, '-'))
        //                .Append("> ")
        //                .AppendLine("Root");
        //        else
        //            textComposer
        //                .AppendAtNewLine("<")
        //                .Append(
        //                    node.Id
        //                        .PatternToString(TreeDepth)
        //                        .Substring(0, level)
        //                        .PadRight(TreeDepth, '-')
        //                )
        //                .Append("> ")
        //                .Append("".PadRight(level * 2, ' '))
        //                .AppendLine("Node");

        //        if (node.HasChildNode1)
        //        {
        //            nodeStack.Push(node.GetChildNodeInfo1());
        //        }

        //        if (node.HasChildNode0)
        //        {
        //            nodeStack.Push(node.GetChildNodeInfo0());
        //        }
        //    }

        //    return textComposer.ToString();
        //}
    }
}