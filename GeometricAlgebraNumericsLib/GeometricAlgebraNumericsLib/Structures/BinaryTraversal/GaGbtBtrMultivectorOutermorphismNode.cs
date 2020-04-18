using System.Collections.Generic;
using DataStructuresLib.Stacks;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtBtrMultivectorOutermorphismNode : IGaGbtNode1<double>
    {
        public static FixedStack<IGaGbtNode1<double>> CreateFixedStack(IReadOnlyList<GaNumVector> basisVectorMappingsList, GaNumSarMultivector multivector)
        {
            var targetGaSpaceDimension = basisVectorMappingsList[0].GaSpaceDimension;
            var stack = new FixedStack<IGaGbtNode1<double>>(multivector.VSpaceDimension + 1);

            stack.Push(
                new GaGbtBtrMultivectorOutermorphismNode(
                    multivector.VSpaceDimension,
                    0,
                    basisVectorMappingsList,
                    GaNumDarKVector.CreateScalar(targetGaSpaceDimension, 1),
                    multivector.BtrRootNode
                )
            );

            return stack;
        }

        public static Stack<IGaGbtNode1<double>> CreateStack(IReadOnlyList<GaNumVector> basisVectorMappingsList, GaNumSarMultivector multivector)
        {
            var targetGaSpaceDimension = basisVectorMappingsList[0].GaSpaceDimension;
            var stack = new Stack<IGaGbtNode1<double>>();

            stack.Push(
                new GaGbtBtrMultivectorOutermorphismNode(
                    multivector.VSpaceDimension,
                    0,
                    basisVectorMappingsList,
                    GaNumDarKVector.CreateScalar(targetGaSpaceDimension, 1),
                    multivector.BtrRootNode
                )
            );

            return stack;
        }


        public int TreeDepth { get; }

        public ulong Id { get; }

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        public double Value
            => BtrNode.Value;

        public IReadOnlyList<GaNumVector> BasisVectorMappingsList { get; }

        public GaNumDarKVector KVector { get; }

        public IGaBtrNode<double> BtrNode { get; }


        private GaGbtBtrMultivectorOutermorphismNode(int treeDepth, ulong id, IReadOnlyList<GaNumVector> basisVectorMappingsList, GaNumDarKVector kVector, IGaBtrNode<double> btrNode)
        {
            TreeDepth = treeDepth;
            Id = id;
            BasisVectorMappingsList = basisVectorMappingsList;
            KVector = kVector;
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

        public IGaGbtNode1<double> GetValueChild0()
        {
            return new GaGbtBtrMultivectorOutermorphismNode(
                TreeDepth - 1,
                ChildId0,
                BasisVectorMappingsList,
                KVector,
                BtrNode.ChildNode0
            );
        }

        public IGaGbtNode1<double> GetValueChild1()
        {
            var basisVector = BasisVectorMappingsList[TreeDepth - 1];

            return new GaGbtBtrMultivectorOutermorphismNode(
                TreeDepth - 1,
                ChildId1,
                BasisVectorMappingsList,
                KVector.Grade == 0
                    ? basisVector.ToDarKVector()
                    : basisVector.Op(KVector),
                BtrNode.ChildNode1
            );
        }
    }
}