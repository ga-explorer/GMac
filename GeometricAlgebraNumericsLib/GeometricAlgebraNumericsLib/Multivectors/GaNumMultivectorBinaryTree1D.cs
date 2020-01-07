using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public sealed class GaNumMultivectorBinaryTree1D
    {
        internal GaBinaryTreeInternalNode<IGaNumMultivector> RootNode { get; }

        public int DomainVSpaceDimension
            => RootNode.TreeDepth;

        public int DomainGaSpaceDimension
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IGaNumMultivector this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue((ulong)id1, out var mv);

                return mv;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1,
                    value ?? GaNumMultivector.CreateZero(TargetGaSpaceDimension)
                    );
            }
        }

        public double this[int id1, int id2]
        {
            get
            {
                return
                    RootNode.TryGetLeafValue((ulong)id1, out var mv) && !ReferenceEquals(mv, null)
                    ? mv[id2]
                    : 0.0d;
            }
        }


        public GaNumMultivectorBinaryTree1D(int domainVSpaceDimension, int targetVSpaceDimension)
        {
            RootNode = new GaBinaryTreeInternalNode<IGaNumMultivector>(
                0, 
                domainVSpaceDimension
            );

            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}
