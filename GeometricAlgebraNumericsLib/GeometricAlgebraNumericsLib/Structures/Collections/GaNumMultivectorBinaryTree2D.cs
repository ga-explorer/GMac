using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaNumMultivectorBinaryTree2D
    {
        internal GaBtrInternalNode<GaNumMultivectorBinaryTree1D> RootNode { get; }

        public int DomainVSpaceDimension1 { get; }

        public int DomainGaSpaceDimension1
            => DomainVSpaceDimension1.ToGaSpaceDimension();

        public int DomainVSpaceDimension2 { get; }

        public int DomainGaSpaceDimension2
            => DomainVSpaceDimension2.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public GaNumMultivectorBinaryTree1D this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue(
                    DomainVSpaceDimension1, 
                    (ulong)id1, 
                    out var mvTree1D
                );

                return mvTree1D;
            }
            set =>
                RootNode.SetLeafValue(
                    DomainVSpaceDimension1,
                    (ulong)id1,
                    value ?? new GaNumMultivectorBinaryTree1D(
                        DomainVSpaceDimension2,
                        TargetVSpaceDimension
                    )
                );
        }

        public IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                RootNode.TryGetLeafValue(
                    DomainVSpaceDimension1, 
                    (ulong)id1, 
                    out var mvTree1D
                );

                return ReferenceEquals(mvTree1D, null) 
                    ? GaNumTerm.CreateZero(TargetGaSpaceDimension)
                    : mvTree1D[id2];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(DomainVSpaceDimension1, (ulong)id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaNumMultivectorBinaryTree1D(
                        DomainVSpaceDimension2,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue(DomainVSpaceDimension1, (ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public double this[int id1, int id2, int id3]
        {
            get
            {
                if (RootNode.TryGetLeafValue(DomainVSpaceDimension1, (ulong)id1, out var mvTree1D) && !ReferenceEquals(mvTree1D, null))
                    return mvTree1D[id2, id3];

                return 0.0d;
            }
            //set
            //{
            //    GaNumMultivectorBinaryTree1D mvTree1D;
            //    if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
            //    {
            //        mvTree1D = new GaNumMultivectorBinaryTree1D(
            //            DomainVSpaceDimension2,
            //            TargetVSpaceDimension
            //        );

            //        RootNode.SetLeafValue((ulong)id1, mvTree1D);
            //    }

            //    mvTree1D[id2, id3] = value;
            //}
        }


        public GaNumMultivectorBinaryTree2D(int domainVSpaceDimension1, int domainVSpaceDimension2, int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<GaNumMultivectorBinaryTree1D>();

            DomainVSpaceDimension1 = domainVSpaceDimension1;
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}