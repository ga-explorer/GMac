using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtJarMultivectorOutermorphismNode : IGaGbtNode1<double>
    {
        public static Stack<IGaGbtNode1<double>> CreateStack(IReadOnlyList<GaNumVector> basisVectorMappingsList, IGaNumMultivector multivector)
        {
            var treeDepth = multivector.VSpaceDimension;
            var targetGaSpaceDimension = basisVectorMappingsList[0].GaSpaceDimension;
            var activeGradesBitPattern = (ulong)multivector.GetStoredGradesBitPattern();
            var activeGradesBitMask = (1ul << (treeDepth + 2)) - 1;

            var rootNode = new GaGbtJarMultivectorOutermorphismNode(
                treeDepth,
                basisVectorMappingsList,
                GaNumDarKVector.CreateScalar(targetGaSpaceDimension, 1),
                multivector,
                0,
                activeGradesBitPattern,
                activeGradesBitMask,
                activeGradesBitMask
            );

            var stack = new Stack<IGaGbtNode1<double>>();
            stack.Push(rootNode);

            return stack;
        }


        public int TreeDepth { get; }

        public ulong Id { get; }

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        public double Value
            => TreeDepth == 0 ? Multivector[(int)Id] : 0;

        public IReadOnlyList<GaNumVector> BasisVectorMappingsList { get; }

        public GaNumDarKVector KVector { get; }

        public IGaNumMultivector Multivector { get; }

        public ulong ActiveGradesBitPattern { get; }

        public ulong ActiveGradesBitMask0 { get; }

        public ulong ActiveGradesBitMask1 { get; }

        public ulong ChildActiveGradesBitPattern0
            => ActiveGradesBitPattern & (ActiveGradesBitMask0 >> 1) & ActiveGradesBitMask1;

        public ulong ChildActiveGradesBitPattern1
            => ActiveGradesBitPattern & ActiveGradesBitMask0 & (ActiveGradesBitMask1 << 1);


        private GaGbtJarMultivectorOutermorphismNode(int treeDepth, IReadOnlyList<GaNumVector> MappingsList, GaNumDarKVector kVector, IGaNumMultivector multivector, ulong id, ulong pattern, ulong mask0, ulong mask1)
        {
            Debug.Assert((pattern & mask0 & mask1) > 0);

            BasisVectorMappingsList = MappingsList;
            KVector = kVector;
            Multivector = multivector;

            TreeDepth = treeDepth;

            Id = id;

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

        /// <summary>
        /// Create node info for child 0
        /// </summary>
        /// <returns></returns>
        public IGaGbtNode1<double> GetValueChild0()
        {
            return new GaGbtJarMultivectorOutermorphismNode(
                TreeDepth - 1,
                BasisVectorMappingsList,
                KVector,
                Multivector,
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
        public IGaGbtNode1<double> GetValueChild1()
        {
            var basisVector = BasisVectorMappingsList[TreeDepth - 1];

            return new GaGbtJarMultivectorOutermorphismNode(
                TreeDepth - 1,
                BasisVectorMappingsList,
                KVector.Grade == 0
                    ? basisVector.ToDarKVector()
                    : basisVector.Op(KVector),
                Multivector,
                ChildId1,
                ChildActiveGradesBitPattern1,
                ActiveGradesBitMask0,
                ActiveGradesBitMask1 << 1
            );
        }
    }
}