using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal
{
    public sealed class GaGbtBtrNodeStack1<T> 
        : GaGbtStack1, IGaGbtStack1<T>
    {
        public static GaGbtBtrNodeStack1<double> Create(GaNumSarMultivector mv)
        {
            var stack = new GaGbtBtrNodeStack1<double>(
                mv.VSpaceDimension + 1,
                mv.VSpaceDimension,
                mv.BtrRootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBtrNodeStack1<double> Create(int capacity, GaNumSarMultivector mv)
        {
            var stack = new GaGbtBtrNodeStack1<double>(
                capacity,
                mv.VSpaceDimension,
                mv.BtrRootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBtrNodeStack1<T> Create(IGaBtrNode<T> rootNode)
        {
            var treeDepth = rootNode.GetTreeDepth();

            var stack = new GaGbtBtrNodeStack1<T>(
                treeDepth + 1,
                treeDepth,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBtrNodeStack1<T> Create(int treeDepth, IGaBtrNode<T> rootNode)
        {
            var stack = new GaGbtBtrNodeStack1<T>(
                treeDepth + 1,
                treeDepth,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBtrNodeStack1<T> Create(int capacity, int treeDepth, IGaBtrNode<T> rootNode)
        {
            var stack = new GaGbtBtrNodeStack1<T>(
                capacity,
                treeDepth,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }


        private IGaBtrNode<T>[] BtrNodeArray { get; }


        public IGaBtrNode<T> TosBtrNode { get; private set; }

        public T TosValue { get; private set; }


        public IGaBtrNode<T> RootBtrNode { get; }


        private GaGbtBtrNodeStack1(int capacity, int rootTreeDepth, IGaBtrNode<T> rootNode)
            : base(capacity, rootTreeDepth, 0ul)
        {
            BtrNodeArray = new IGaBtrNode<T>[capacity];

            RootBtrNode = rootNode;
        }


        public override bool TosHasChild0()
        {
            return TosBtrNode.HasChildNode0;
        }

        public override bool TosHasChild1()
        {
            return TosBtrNode.HasChildNode1;
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            BtrNodeArray[TosIndex] = RootBtrNode;
        }

        public override void PopNodeData()
        {
            //Pop data for both cases of leaf and internal node
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                //Pop data for the case of internal node
                TosBtrNode = BtrNodeArray[TosIndex];
            }
            else
            {
                //Pop data for the case of leaf node
                TosValue = BtrNodeArray[TosIndex].Value;
            }

            //Remove top of stack
            TosIndex--;
        }

        public override void PushDataOfChild0()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId0;
            BtrNodeArray[TosIndex] = TosBtrNode.ChildNode0;
        }

        public override void PushDataOfChild1()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId1;
            BtrNodeArray[TosIndex] = TosBtrNode.ChildNode1;
        }


        public IEnumerable<KeyValuePair<ulong, T>> TraverseForLeafIdValue()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new KeyValuePair<ulong, T>(TosId, TosValue);

                    continue;
                }

                if (TosHasChild1())
                    PushDataOfChild1();

                if (TosHasChild0())
                    PushDataOfChild0();
            }
        }
    }
}