using GMac.GMacMath.Structures;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree3D
    {
        internal GMacBinaryTree<GaSymMultivectorBinaryTree2D> RootNode { get; }

        public int DomainVSpaceDimension1
            => RootNode.TreeDepth;

        public int DomainGaSpaceDimension1
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public int DomainVSpaceDimension2 { get; }

        public int DomainGaSpaceDimension2
            => DomainVSpaceDimension2.ToGaSpaceDimension();

        public int DomainVSpaceDimension3 { get; }

        public int DomainGaSpaceDimension3
            => DomainVSpaceDimension3.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public GaSymMultivectorBinaryTree2D this[int id1]
        {
            get
            {
                GaSymMultivectorBinaryTree2D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

                return mvTree1D;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1,
                    value ?? new GaSymMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    )
                );
            }
        }

        public GaSymMultivectorBinaryTree1D this[int id1, int id2]
        {
            get
            {
                GaSymMultivectorBinaryTree2D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                GaSymMultivectorBinaryTree2D mvTree1D;
                if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue((ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public IGaSymMultivector this[int id1, int id2, int id3]
        {
            get
            {
                GaSymMultivectorBinaryTree2D mvTree1D;
                RootNode.TryGetLeafValue((ulong)id1, out mvTree1D);

                return mvTree1D[id2, id3];
            }
            set
            {
                GaSymMultivectorBinaryTree2D mvTree1D;
                if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue((ulong)id1, mvTree1D);
                }

                mvTree1D[id2, id3] = value;
            }
        }

        public MathematicaScalar this[int id1, int id2, int id3, int id4]
        {
            get
            {
                GaSymMultivectorBinaryTree2D mvTree1D;
                if (RootNode.TryGetLeafValue((ulong)id1, out mvTree1D))
                    return mvTree1D[id2, id3, id4] ?? SymbolicUtils.Constants.Zero;

                return SymbolicUtils.Constants.Zero;
            }
        }


        public GaSymMultivectorBinaryTree3D(int domainVSpaceDimension1, int domainVSpaceDimension2, int domainVSpaceDimension3, int targetVSpaceDimension)
        {
            RootNode = new GMacBinaryTree<GaSymMultivectorBinaryTree2D>(domainVSpaceDimension1);
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            DomainVSpaceDimension3 = domainVSpaceDimension3;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}