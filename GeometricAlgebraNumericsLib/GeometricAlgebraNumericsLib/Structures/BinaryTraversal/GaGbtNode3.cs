using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtNode3 : IGaGbtNode
    {
        internal static Stack<GaGbtNode3> CreateStack(IGaGbtNode1 nodeInfo1, IGaGbtNode1 nodeInfo2, IGaGbtNode1 nodeInfo3)
        {
            Debug.Assert(
                nodeInfo1.TreeDepth == nodeInfo2.TreeDepth &&
                nodeInfo2.TreeDepth == nodeInfo3.TreeDepth &&
                nodeInfo1.Id == 0 &&
                nodeInfo2.Id == 0 &&
                nodeInfo3.Id == 0
            );

            var stack = new Stack<GaGbtNode3>();

            var rootNode = new GaGbtNode3(
                nodeInfo1,
                nodeInfo2,
                nodeInfo3
            );

            stack.Push(rootNode);

            return stack;
        }


        public int TreeDepth
            => GbtNode1.TreeDepth;


        public IGaGbtNode1 GbtNode1 { get; }

        public IGaGbtNode1 GbtNode2 { get; }

        public IGaGbtNode1 GbtNode3 { get; }


        public ulong Id1
            => GbtNode1.Id;

        public ulong Id2
            => GbtNode2.Id;

        public ulong Id3
            => GbtNode3.Id;


        public ulong ChildIdXor000
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId0 ^ GbtNode3.ChildId0;

        public ulong ChildIdXor100
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId0 ^ GbtNode3.ChildId0;

        public ulong ChildIdXor010
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId1 ^ GbtNode3.ChildId0;

        public ulong ChildIdXor110
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId1 ^ GbtNode3.ChildId0;

        public ulong ChildIdXor001
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId0 ^ GbtNode3.ChildId1;

        public ulong ChildIdXor101
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId0 ^ GbtNode3.ChildId1;

        public ulong ChildIdXor011
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId1 ^ GbtNode3.ChildId1;

        public ulong ChildIdXor111
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId1 ^ GbtNode3.ChildId1;


        public int ChildIdXorGrade000
            => ChildIdXor000.CountOnes();

        public int ChildIdXorGrade100
            => ChildIdXor100.CountOnes();

        public int ChildIdXorGrade010
            => ChildIdXor010.CountOnes();

        public int ChildIdXorGrade110
            => ChildIdXor110.CountOnes();

        public int ChildIdXorGrade001
            => ChildIdXor001.CountOnes();

        public int ChildIdXorGrade101
            => ChildIdXor101.CountOnes();

        public int ChildIdXorGrade011
            => ChildIdXor011.CountOnes();

        public int ChildIdXorGrade111
            => ChildIdXor111.CountOnes();


        private GaGbtNode3(IGaGbtNode1 nodeInfo1, IGaGbtNode1 nodeInfo2, IGaGbtNode1 nodeInfo3)
        {
            GbtNode1 = nodeInfo1;
            GbtNode2 = nodeInfo2;
            GbtNode3 = nodeInfo3;
        }


        public bool HasChild10()
        {
            return GbtNode1.HasChild0();
        }

        public bool HasChild11()
        {
            return GbtNode1.HasChild1();
        }

        public bool HasChild20()
        {
            return GbtNode2.HasChild0();
        }

        public bool HasChild21()
        {
            return GbtNode2.HasChild1();
        }

        public bool HasChild30()
        {
            return GbtNode3.HasChild0();
        }

        public bool HasChild31()
        {
            return GbtNode3.HasChild1();
        }


        public bool ChildMayContainGrade1(int childGrade)
        {
            return
                (TreeDepth > 1 && childGrade <= 1) ||
                (TreeDepth == 1 && childGrade == 1);
        }

        public bool ChildMayContainGrade(int childGrade, int grade)
        {
            return
                (TreeDepth > 1 && childGrade <= grade) ||
                (TreeDepth == 1 && childGrade == grade);
        }


        public GaGbtNode3 GetChild000()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild0(),
                GbtNode2.GetChild0(),
                GbtNode3.GetChild0()
            );
        }

        public GaGbtNode3 GetChild100()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild1(),
                GbtNode2.GetChild0(),
                GbtNode3.GetChild0()
            );
        }

        public GaGbtNode3 GetChild010()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild0(),
                GbtNode2.GetChild1(),
                GbtNode3.GetChild0()
            );
        }

        public GaGbtNode3 GetChild110()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild1(),
                GbtNode2.GetChild1(),
                GbtNode3.GetChild0()
            );
        }

        public GaGbtNode3 GetChild001()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild0(),
                GbtNode2.GetChild0(),
                GbtNode3.GetChild1()
            );
        }

        public GaGbtNode3 GetChild101()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild1(),
                GbtNode2.GetChild0(),
                GbtNode3.GetChild1()
            );
        }

        public GaGbtNode3 GetChild011()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild0(),
                GbtNode2.GetChild1(),
                GbtNode3.GetChild1()
            );
        }

        public GaGbtNode3 GetChild111()
        {
            return new GaGbtNode3(
                GbtNode1.GetChild1(),
                GbtNode2.GetChild1(),
                GbtNode3.GetChild1()
            );
        }
    }
}