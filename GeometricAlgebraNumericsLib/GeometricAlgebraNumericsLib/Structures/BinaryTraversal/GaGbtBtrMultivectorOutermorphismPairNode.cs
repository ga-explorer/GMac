using System.Collections.Generic;
using DataStructuresLib.Basic;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtBtrMultivectorOutermorphismPairNode : IGaGbtNode1<Pair<double>>
    {
        public static Stack<IGaGbtNode1<Pair<double>>> CreateStack(IReadOnlyList<IGaNumVector> basisVectorMappingsList, GaNumSarMultivector multivector1, GaNumSarMultivector multivector2)
        {
            var targetGaSpaceDimension = basisVectorMappingsList[0].GaSpaceDimension;
            var stack = new Stack<IGaGbtNode1<Pair<double>>>();

            stack.Push(
                new GaGbtBtrMultivectorOutermorphismPairNode(
                    multivector1.VSpaceDimension,
                    0,
                    basisVectorMappingsList,
                    GaNumDarKVector.CreateScalar(targetGaSpaceDimension, 1),
                    multivector1.BtrRootNode,
                    multivector2.BtrRootNode
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

        public Pair<double> Value
            => new Pair<double>(BtrNode1?.Value ?? 0, BtrNode2?.Value ?? 0);

        public IReadOnlyList<IGaNumVector> BasisVectorMappingsList { get; }

        public GaNumDarKVector KVector { get; }

        public IGaBtrNode<double> BtrNode1 { get; }

        public IGaBtrNode<double> BtrNode2 { get; }


        private GaGbtBtrMultivectorOutermorphismPairNode(int treeDepth, ulong id, IReadOnlyList<IGaNumVector> basisVectorMappingsList, GaNumDarKVector kVector, IGaBtrNode<double> btrNode1, IGaBtrNode<double> btrNode2)
        {
            TreeDepth = treeDepth;
            Id = id;
            BasisVectorMappingsList = basisVectorMappingsList;
            KVector = kVector;
            BtrNode1 = btrNode1;
            BtrNode2 = btrNode2;
        }
        

        public bool HasChild0()
        {
            return (BtrNode1 != null && BtrNode1.HasChildNode0) || 
                   (BtrNode2 != null && BtrNode2.HasChildNode0);
        }

        public bool HasChild1()
        {
            return (BtrNode1 != null && BtrNode1.HasChildNode1) ||
                   (BtrNode2 != null && BtrNode2.HasChildNode1);
        }

        public IGaGbtNode1 GetChild0()
        {
            return GetValueChild0();
        }

        public IGaGbtNode1 GetChild1()
        {
            return GetValueChild1();
        }

        public IGaGbtNode1<Pair<double>> GetValueChild0()
        {
            return new GaGbtBtrMultivectorOutermorphismPairNode(
                TreeDepth - 1,
                ChildId0,
                BasisVectorMappingsList,
                KVector,
                BtrNode1?.ChildNode0,
                BtrNode2?.ChildNode0
            );
        }

        public IGaGbtNode1<Pair<double>> GetValueChild1()
        {
            var basisVector = BasisVectorMappingsList[TreeDepth - 1];

            return new GaGbtBtrMultivectorOutermorphismPairNode(
                TreeDepth - 1,
                ChildId1,
                BasisVectorMappingsList,
                KVector.Grade == 0
                    ? basisVector.ToDarKVector()
                    : basisVector.Op(KVector),
                BtrNode1?.ChildNode1,
                BtrNode2?.ChildNode1
            );
        }
    }
}