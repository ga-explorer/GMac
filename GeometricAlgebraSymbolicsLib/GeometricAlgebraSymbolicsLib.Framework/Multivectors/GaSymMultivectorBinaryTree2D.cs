using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree2D
    {
        internal GaBtrInternalNode<GaSymMultivectorBinaryTree1D> RootNode { get; }

        public int DomainVSpaceDimension1 { get; }

        public int DomainGaSpaceDimension1
            => DomainVSpaceDimension1.ToGaSpaceDimension();

        public int DomainVSpaceDimension2 { get; }

        public int DomainGaSpaceDimension2
            => DomainVSpaceDimension2.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public GaSymMultivectorBinaryTree1D this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainVSpaceDimension1, (ulong) id1, out var mvTree1D);

                return mvTree1D;
            }
            set =>
                RootNode.SetLeafValue(
                    DomainVSpaceDimension1, 
                    (ulong)id1, 
                    value ?? new GaSymMultivectorBinaryTree1D(
                        DomainVSpaceDimension2, 
                        TargetVSpaceDimension
                    )
                );
        }

        public IGaSymMultivector this[int id1, int id2]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainVSpaceDimension1, (ulong) id1, out var mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                if (!RootNode.TryGetLeafValue(DomainVSpaceDimension1, (ulong)id1, out var mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymMultivectorBinaryTree1D(
                        DomainVSpaceDimension2, 
                        TargetVSpaceDimension
                        );

                    RootNode.SetLeafValue(DomainVSpaceDimension1, (ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public MathematicaScalar this[int id1, int id2, int id3]
        {
            get
            {
                if (RootNode.TryGetLeafValue(DomainVSpaceDimension1, (ulong)id1, out var mvTree1D))
                    return mvTree1D[id2, id3] ?? GaSymbolicsUtils.Constants.Zero;

                return GaSymbolicsUtils.Constants.Zero;
            }
        }


        public GaSymMultivectorBinaryTree2D(int domainVSpaceDimension1, int domainVSpaceDimension2, int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<GaSymMultivectorBinaryTree1D>();

            DomainVSpaceDimension1 = domainVSpaceDimension1;
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}