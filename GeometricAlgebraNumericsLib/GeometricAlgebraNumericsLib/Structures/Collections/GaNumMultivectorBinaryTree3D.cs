using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaNumMultivectorBinaryTree3D
    {
        internal GaBtrInternalNode<GaNumMultivectorBinaryTree2D> RootNode { get; }

        public int DomainVSpaceDimension1 { get; }

        public int DomainGaSpaceDimension1
            => DomainVSpaceDimension1.ToGaSpaceDimension();

        public int DomainVSpaceDimension2 { get; }

        public int DomainGaSpaceDimension2
            => DomainVSpaceDimension2.ToGaSpaceDimension();

        public int DomainVSpaceDimension3 { get; }

        public int DomainGaSpaceDimension3
            => DomainVSpaceDimension3.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public GaNumMultivectorBinaryTree2D this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainGaSpaceDimension1, (ulong)id1, out var mvTree1D);

                return mvTree1D;
            }
            set =>
                RootNode.SetLeafValue(
                    DomainGaSpaceDimension1, 
                    (ulong)id1,
                    value ?? new GaNumMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    )
                );
        }

        public GaNumMultivectorBinaryTree1D this[int id1, int id2]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainGaSpaceDimension1, (ulong)id1, out var mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(DomainGaSpaceDimension1, (ulong)id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaNumMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue(DomainGaSpaceDimension1, (ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public IGaNumMultivector this[int id1, int id2, int id3]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainGaSpaceDimension1, (ulong)id1, out var mvTree1D);

                return mvTree1D[id2, id3];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(DomainGaSpaceDimension1, (ulong)id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaNumMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue(DomainGaSpaceDimension1, (ulong)id1, mvTree1D);
                }

                mvTree1D[id2, id3] = value;
            }
        }

        public double this[int id1, int id2, int id3, int id4]
        {
            get
            {
                if (RootNode.TryGetLeafValue(DomainGaSpaceDimension1, (ulong)id1, out var mvTree1D) && ! ReferenceEquals(mvTree1D, null))
                    return mvTree1D[id2, id3, id4];

                return 0.0d;
            }
            //set
            //{
            //    GaNumMultivectorBinaryTree2D mvTree1D;
            //    if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
            //    {
            //        mvTree1D = new GaNumMultivectorBinaryTree2D(
            //            DomainVSpaceDimension2,
            //            DomainVSpaceDimension3,
            //            TargetVSpaceDimension
            //        );

            //        RootNode.SetLeafValue((ulong)id1, mvTree1D);
            //    }

            //    mvTree1D[id2, id3, id4] = value;
            //}
        }


        public GaNumMultivectorBinaryTree3D(int domainVSpaceDimension1, int domainVSpaceDimension2, int domainVSpaceDimension3, int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<GaNumMultivectorBinaryTree2D>();

            DomainVSpaceDimension1 = domainVSpaceDimension1;
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            DomainVSpaceDimension3 = domainVSpaceDimension3;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}