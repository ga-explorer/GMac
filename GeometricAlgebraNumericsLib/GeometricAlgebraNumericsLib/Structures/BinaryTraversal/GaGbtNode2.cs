using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtNode2 : IGaGbtNode
    {
        internal static Stack<GaGbtNode2> CreateStack(IGaGbtNode1 gbtNode1, IGaGbtNode1 gbtNode2)
        {
            Debug.Assert(
                gbtNode1.TreeDepth == gbtNode2.TreeDepth &&
                gbtNode1.Id == 0 &&
                gbtNode2.Id == 0
            );

            var stack = new Stack<GaGbtNode2>();

            var rootNode = new GaGbtNode2(
                gbtNode1,
                gbtNode2
            );

            stack.Push(rootNode);

            return stack;
        }


        public int TreeDepth
            => GbtNode1.TreeDepth;


        public IGaGbtNode1 GbtNode1 { get; }

        public IGaGbtNode1 GbtNode2 { get; }


        public ulong Id1
            => GbtNode1.Id;

        public ulong Id2
            => GbtNode2.Id;


        public bool IsNonZeroOp
            => GaNumFrameUtils.IsNonZeroOp(Id1, Id2);

        public bool IsNonZeroESp
            => GaNumFrameUtils.IsNonZeroESp(Id1, Id2);

        public bool IsNonZeroELcp
            => GaNumFrameUtils.IsNonZeroELcp(Id1, Id2);

        public bool IsNonZeroERcp
            => GaNumFrameUtils.IsNonZeroERcp(Id1, Id2);

        public bool IsNonZeroEFdp
            => GaNumFrameUtils.IsNonZeroEFdp(Id1, Id2);

        public bool IsNonZeroEHip
            => GaNumFrameUtils.IsNonZeroEHip(Id1, Id2);

        public bool IsNonZeroEAcp
            => GaNumFrameUtils.IsNonZeroEAcp((int)Id1, (int)Id2);

        public bool IsNonZeroECp
            => GaNumFrameUtils.IsNonZeroECp((int)Id1, (int)Id2);


        public ulong ChildIdXor00
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId0;

        public ulong ChildIdXor10
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId0;

        public ulong ChildIdXor01
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId1;

        public ulong ChildIdXor11
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId1;


        public int ChildIdXorGrade00
            => ChildIdXor00.CountOnes();

        public int ChildIdXorGrade10
            => ChildIdXor10.CountOnes();

        public int ChildIdXorGrade01
            => ChildIdXor01.CountOnes();

        public int ChildIdXorGrade11
            => ChildIdXor11.CountOnes();


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


        private GaGbtNode2(IGaGbtNode1 gbtNode1, IGaGbtNode1 gbtNode2)
        {
            GbtNode1 = gbtNode1;
            GbtNode2 = gbtNode2;
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


        public GaGbtNode2 GetChild00()
        {
            return new GaGbtNode2(
                GbtNode1.GetChild0(),
                GbtNode2.GetChild0()
            );
        }

        public GaGbtNode2 GetChild10()
        {
            return new GaGbtNode2(
                GbtNode1.GetChild1(),
                GbtNode2.GetChild0()
            );
        }

        public GaGbtNode2 GetChild01()
        {
            return new GaGbtNode2(
                GbtNode1.GetChild0(),
                GbtNode2.GetChild1()
            );
        }

        public GaGbtNode2 GetChild11()
        {
            return new GaGbtNode2(
                GbtNode1.GetChild1(),
                GbtNode2.GetChild1()
            );
        }
    }

    public sealed class GaGbtNode2<T> : IGaGbtNode
    {
        internal static Stack<GaGbtNode2<T>> CreateStack(IGaGbtNode1<T> gbtNode1, IGaGbtNode1<T> gbtNode2)
        {
            Debug.Assert(
                gbtNode1.TreeDepth == gbtNode2.TreeDepth &&
                gbtNode1.Id == 0 &&
                gbtNode2.Id == 0
            );
            
            var stack = new Stack<GaGbtNode2<T>>();

            var rootNode = new GaGbtNode2<T>(
                gbtNode1,
                gbtNode2
            );

            stack.Push(rootNode);

            return stack;
        }


        public int TreeDepth 
            => GbtNode1.TreeDepth;


        public IGaGbtNode1<T> GbtNode1 { get; }

        public IGaGbtNode1<T> GbtNode2 { get; }


        public ulong Id1 
            => GbtNode1.Id;

        public ulong Id2
            => GbtNode2.Id;


        public bool IsNonZeroOp
            => GaNumFrameUtils.IsNonZeroOp(Id1, Id2);

        public bool IsNonZeroESp
            => GaNumFrameUtils.IsNonZeroESp(Id1, Id2);

        public bool IsNonZeroELcp
            => GaNumFrameUtils.IsNonZeroELcp(Id1, Id2);

        public bool IsNonZeroERcp
            => GaNumFrameUtils.IsNonZeroERcp(Id1, Id2);

        public bool IsNonZeroEFdp
            => GaNumFrameUtils.IsNonZeroEFdp(Id1, Id2);

        public bool IsNonZeroEHip
            => GaNumFrameUtils.IsNonZeroEHip(Id1, Id2);

        public bool IsNonZeroEAcp
            => GaNumFrameUtils.IsNonZeroEAcp((int)Id1, (int)Id2);

        public bool IsNonZeroECp
            => GaNumFrameUtils.IsNonZeroECp((int)Id1, (int)Id2);


        public ulong ChildIdXor00
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId0;

        public ulong ChildIdXor10
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId0;

        public ulong ChildIdXor01
            => GbtNode1.ChildId0 ^ GbtNode2.ChildId1;

        public ulong ChildIdXor11
            => GbtNode1.ChildId1 ^ GbtNode2.ChildId1;


        public int ChildIdXorGrade00
            => ChildIdXor00.CountOnes();

        public int ChildIdXorGrade10
            => ChildIdXor10.CountOnes();

        public int ChildIdXorGrade01
            => ChildIdXor01.CountOnes();

        public int ChildIdXorGrade11
            => ChildIdXor11.CountOnes();


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


        private GaGbtNode2(IGaGbtNode1<T> gbtNode1, IGaGbtNode1<T> gbtNode2)
        {
            GbtNode1 = gbtNode1;
            GbtNode2 = gbtNode2;
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


        public GaGbtNode2<T> GetChild00()
        {
            return new GaGbtNode2<T>(
                GbtNode1.GetValueChild0(),
                GbtNode2.GetValueChild0()
            );
        }

        public GaGbtNode2<T> GetChild10()
        {
            return new GaGbtNode2<T>(
                GbtNode1.GetValueChild1(),
                GbtNode2.GetValueChild0()
            );
        }

        public GaGbtNode2<T> GetChild01()
        {
            return new GaGbtNode2<T>(
                GbtNode1.GetValueChild0(),
                GbtNode2.GetValueChild1()
            );
        }

        public GaGbtNode2<T> GetChild11()
        {
            return new GaGbtNode2<T>(
                GbtNode1.GetValueChild1(),
                GbtNode2.GetValueChild1()
            );
        }
    }
}