using GMac.GMacMath.Structures;

namespace GMac.GMacMath.Numeric.Multivectors
{
    public sealed class GaNumMultivectorBinaryTree2D
    {
        internal GMacBinaryTree<GaNumMultivectorBinaryTree1D> RootNode { get; }

        public int DomainVSpaceDimension1
            => RootNode.TreeDepth;

        public int DomainGaSpaceDimension1
            => RootNode.TreeDepth.ToGaSpaceDimension();

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
                GaNumMultivectorBinaryTree1D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

                return mvTree1D;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1,
                    value ?? new GaNumMultivectorBinaryTree1D(
                        DomainVSpaceDimension2,
                        TargetVSpaceDimension
                    )
                );
            }
        }

        public IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                GaNumMultivectorBinaryTree1D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

                return ReferenceEquals(mvTree1D, null) 
                    ? GaNumMultivectorTerm.CreateZero(TargetGaSpaceDimension)
                    : mvTree1D[id2];
            }
            set
            {
                GaNumMultivectorBinaryTree1D mvTree1D;
                if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaNumMultivectorBinaryTree1D(
                        DomainVSpaceDimension2,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue((ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public double this[int id1, int id2, int id3]
        {
            get
            {
                GaNumMultivectorBinaryTree1D mvTree1D;
                if (RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) && !ReferenceEquals(mvTree1D, null))
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
            RootNode = new GMacBinaryTree<GaNumMultivectorBinaryTree1D>(domainVSpaceDimension1);
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}