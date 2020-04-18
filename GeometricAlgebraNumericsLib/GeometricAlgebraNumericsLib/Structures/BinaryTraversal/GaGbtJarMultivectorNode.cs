using System.Diagnostics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtJarMultivectorNode : IGaGbtNode1<double>
    {
        public static GaGbtJarMultivectorNode CreateRootNode(IGaNumMultivector multivector)
        {
            var treeDepth = multivector.VSpaceDimension;
            var activeGradesBitPattern = (ulong)multivector.GetStoredGradesBitPattern();
            var activeGradesBitMask = (1ul << (treeDepth + 2)) - 1;

            return new GaGbtJarMultivectorNode(
                treeDepth,
                0,
                multivector,
                activeGradesBitPattern,
                activeGradesBitMask,
                activeGradesBitMask
            );
        }


        public int TreeDepth { get; }

        public ulong Id { get; }

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        public double Value 
            => TreeDepth == 0 ? Multivector[(int)Id] : 0;

        public IGaNumMultivector Multivector { get; }

        public ulong ActiveGradesBitPattern { get; }

        public ulong ActiveGradesBitMask0 { get; }

        public ulong ActiveGradesBitMask1 { get; }

        public ulong ChildActiveGradesBitPattern0
            => ActiveGradesBitPattern & (ActiveGradesBitMask0 >> 1) & ActiveGradesBitMask1;

        public ulong ChildActiveGradesBitPattern1
            => ActiveGradesBitPattern & ActiveGradesBitMask0 & (ActiveGradesBitMask1 << 1);


        private GaGbtJarMultivectorNode(int treeDepth, ulong id, IGaNumMultivector multivector, ulong pattern, ulong mask0, ulong mask1)
        {
            Debug.Assert((pattern & mask0 & mask1) != 0);

            TreeDepth = treeDepth;

            Id = id;

            Multivector = multivector;

            ActiveGradesBitPattern = pattern;
            ActiveGradesBitMask0 = mask0;
            ActiveGradesBitMask1 = mask1;
        }


        public bool HasChild0()
        {
            return TreeDepth > 0 && ChildActiveGradesBitPattern0 != 0;
        }

        public bool HasChild1()
        {
            return TreeDepth > 0 && ChildActiveGradesBitPattern1 != 0;
        }

        public IGaGbtNode1 GetChild0()
        {
            return GetValueChild0();
        }

        public IGaGbtNode1 GetChild1()
        {
            return GetValueChild1();
        }

        public IGaGbtNode1<double> GetValueChild0()
        {
            return new GaGbtJarMultivectorNode(
                TreeDepth - 1,
                ChildId0,
                Multivector,
                ChildActiveGradesBitPattern0,
                ActiveGradesBitMask0 >> 1,
                ActiveGradesBitMask1
            );
        }

        public IGaGbtNode1<double> GetValueChild1()
        {
            return new GaGbtJarMultivectorNode(
                TreeDepth - 1,
                ChildId1,
                Multivector,
                ChildActiveGradesBitPattern1,
                ActiveGradesBitMask0,
                ActiveGradesBitMask1 << 1
            );
        }
    }
}