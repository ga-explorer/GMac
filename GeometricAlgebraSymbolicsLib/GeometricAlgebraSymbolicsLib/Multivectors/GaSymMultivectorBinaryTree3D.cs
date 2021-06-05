using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree3D
    {
        internal GaBtrInternalNode<GaSymMultivectorBinaryTree2D> RootNode { get; }

        public int DomainVSpaceDimension1 { get; }

        public ulong DomainGaSpaceDimension1
            => DomainVSpaceDimension1.ToGaSpaceDimension();

        public int DomainVSpaceDimension2 { get; }

        public ulong DomainGaSpaceDimension2
            => DomainVSpaceDimension2.ToGaSpaceDimension();

        public int DomainVSpaceDimension3 { get; }

        public ulong DomainGaSpaceDimension3
            => DomainVSpaceDimension3.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public ulong TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public GaSymMultivectorBinaryTree2D this[ulong id1]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainVSpaceDimension1, id1, out var mvTree1D);

                return mvTree1D;
            }
            set =>
                RootNode.SetLeafValue(
                    DomainVSpaceDimension1,
                    id1,
                    value ?? new GaSymMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    )
                );
        }

        public GaSymMultivectorBinaryTree1D this[ulong id1, ulong id2]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainVSpaceDimension1, id1, out var mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(DomainVSpaceDimension1, id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue(DomainVSpaceDimension1, id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public IGaSymMultivector this[ulong id1, ulong id2, ulong id3]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainVSpaceDimension1, id1, out var mvTree1D);

                return mvTree1D[id2, id3];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(DomainVSpaceDimension1, id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymMultivectorBinaryTree2D(
                        DomainVSpaceDimension2,
                        DomainVSpaceDimension3,
                        TargetVSpaceDimension
                    );

                    RootNode.SetLeafValue(DomainVSpaceDimension1, id1, mvTree1D);
                }

                mvTree1D[id2, id3] = value;
            }
        }

        public MathematicaScalar this[ulong id1, ulong id2, ulong id3, ulong id4]
        {
            get
            {
                if (RootNode.TryGetLeafValue(DomainVSpaceDimension1, id1, out var mvTree1D))
                    return mvTree1D[id2, id3, id4] ?? GaSymbolicsUtils.Constants.Zero;

                return GaSymbolicsUtils.Constants.Zero;
            }
        }


        public GaSymMultivectorBinaryTree3D(int domainVSpaceDimension1, int domainVSpaceDimension2, int domainVSpaceDimension3, int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<GaSymMultivectorBinaryTree2D>();

            DomainVSpaceDimension1 = domainVSpaceDimension1;
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            DomainVSpaceDimension3 = domainVSpaceDimension3;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}