using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo
{
    public sealed class GaBinaryTreeGradedNodeInfo2<T>
    {
        public static Stack<GaBinaryTreeGradedNodeInfo2<T>> CreateStack(int treeDepth, ulong activeGradesBitPattern1,
            ulong activeGradesBitPattern2)
        {
            var activeGradesBitMask = (1ul << (treeDepth + 2)) - 1;

            var stack = new Stack<GaBinaryTreeGradedNodeInfo2<T>>();

            var rootNodeInfo = new GaBinaryTreeGradedNodeInfo2<T>(
                treeDepth,
                0,
                activeGradesBitPattern1,
                activeGradesBitMask,
                activeGradesBitMask,
                0,
                activeGradesBitPattern2,
                activeGradesBitMask,
                activeGradesBitMask
            );

            stack.Push(rootNodeInfo);

            return stack;
        }


        public ulong Id1 { get; }

        public ulong Id2 { get; }

        //public T Value1 { get; }

        //public T Value2 { get; }

        public int TreeDepth { get; }

        public ulong ActiveGradesBitPattern1 { get; }

        public ulong ActiveGradesBitPattern2 { get; }

        public ulong ActiveGradesBitMask10 { get; }

        public ulong ActiveGradesBitMask11 { get; }

        public ulong ActiveGradesBitMask20 { get; }

        public ulong ActiveGradesBitMask21 { get; }

        public ulong ChildActiveGradesBitPattern10
            => ActiveGradesBitPattern1 & (ActiveGradesBitMask10 >> 1) & ActiveGradesBitMask11;

        public ulong ChildActiveGradesBitPattern11
            => ActiveGradesBitPattern1 & ActiveGradesBitMask10 & (ActiveGradesBitMask11 << 1);

        public ulong ChildActiveGradesBitPattern20
            => ActiveGradesBitPattern2 & (ActiveGradesBitMask20 >> 1) & ActiveGradesBitMask21;

        public ulong ChildActiveGradesBitPattern21
            => ActiveGradesBitPattern2 & ActiveGradesBitMask20 & (ActiveGradesBitMask21 << 1);

        public ulong ChildId10
            => Id1;

        public ulong ChildId11
            => Id1 | (1ul << (TreeDepth - 1));

        public ulong ChildId20
            => Id2;

        public ulong ChildId21
            => Id2 | (1ul << (TreeDepth - 1));

        public ulong IdBitMask
            => 1ul << TreeDepth;

        public bool IsLeafNode
            => TreeDepth == 0;

        public bool IsLeafParentNode
            => TreeDepth == 1;

        public bool IsInternalNode
            => TreeDepth > 0;

        public bool HasChildNode10
            => TreeDepth > 0 && ChildActiveGradesBitPattern10 != 0;

        public bool HasChildNode11
            => TreeDepth > 0 && ChildActiveGradesBitPattern11 != 0;

        public bool HasChildNode20
            => TreeDepth > 0 && ChildActiveGradesBitPattern20 != 0;

        public bool HasChildNode21
            => TreeDepth > 0 && ChildActiveGradesBitPattern21 != 0;


        /// <summary>
        /// Create an internal node info
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="id1"></param>
        /// <param name="pattern1"></param>
        /// <param name="mask10"></param>
        /// <param name="mask11"></param>
        /// <param name="id2"></param>
        /// <param name="pattern2"></param>
        /// <param name="mask20"></param>
        /// <param name="mask21"></param>
        private GaBinaryTreeGradedNodeInfo2(int treeDepth, ulong id1, ulong pattern1, ulong mask10, ulong mask11,
            ulong id2, ulong pattern2, ulong mask20, ulong mask21)
        {
            TreeDepth = treeDepth;

            Id1 = id1;
            Id2 = id2;

            //Value1 = default;
            //Value2 = default;

            ActiveGradesBitPattern1 = pattern1;
            ActiveGradesBitMask10 = mask10;
            ActiveGradesBitMask11 = mask11;

            ActiveGradesBitPattern2 = pattern2;
            ActiveGradesBitMask20 = mask20;
            ActiveGradesBitMask21 = mask21;
        }


        /// <summary>
        /// Create node info for child 00
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeGradedNodeInfo2<T> GetChildNodeInfo00()
        {
            return new GaBinaryTreeGradedNodeInfo2<T>(
                TreeDepth - 1,
                ChildId10,
                ChildActiveGradesBitPattern10,
                ActiveGradesBitMask10 >> 1,
                ActiveGradesBitMask11,
                ChildId20,
                ChildActiveGradesBitPattern20,
                ActiveGradesBitMask20 >> 1,
                ActiveGradesBitMask21
            );
        }

        /// <summary>
        /// Create node info for child 10
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeGradedNodeInfo2<T> GetChildNodeInfo10()
        {
            return new GaBinaryTreeGradedNodeInfo2<T>(
                TreeDepth - 1,
                ChildId11,
                ChildActiveGradesBitPattern11,
                ActiveGradesBitMask10,
                ActiveGradesBitMask11 << 1,
                ChildId20,
                ChildActiveGradesBitPattern20,
                ActiveGradesBitMask20 >> 1,
                ActiveGradesBitMask21
            );
        }

        /// <summary>
        /// Create node info for child 01
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeGradedNodeInfo2<T> GetChildNodeInfo01()
        {
            return new GaBinaryTreeGradedNodeInfo2<T>(
                TreeDepth - 1,
                ChildId10,
                ChildActiveGradesBitPattern10,
                ActiveGradesBitMask10 >> 1,
                ActiveGradesBitMask11,
                ChildId21,
                ChildActiveGradesBitPattern21,
                ActiveGradesBitMask20,
                ActiveGradesBitMask21 << 1
            );
        }

        /// <summary>
        /// Create node info for child 11
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeGradedNodeInfo2<T> GetChildNodeInfo11()
        {
            return new GaBinaryTreeGradedNodeInfo2<T>(
                TreeDepth - 1,
                ChildId11,
                ChildActiveGradesBitPattern11,
                ActiveGradesBitMask10,
                ActiveGradesBitMask11 << 1,
                ChildId21,
                ChildActiveGradesBitPattern21,
                ActiveGradesBitMask20,
                ActiveGradesBitMask21 << 1
            );
        }
    }
}