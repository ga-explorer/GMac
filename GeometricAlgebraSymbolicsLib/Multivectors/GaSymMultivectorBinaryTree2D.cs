using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree2D
    {
        internal GaBinaryTree<GaSymMultivectorBinaryTree1D> RootNode { get; }

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

        public GaSymMultivectorBinaryTree1D this[int id1]
        {
            get
            {
                GaSymMultivectorBinaryTree1D mvTree1D;
                RootNode.TryGetLeafValue((ulong) id1, out mvTree1D);

                return mvTree1D;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1, 
                    value ?? new GaSymMultivectorBinaryTree1D(
                        DomainVSpaceDimension2, 
                        TargetVSpaceDimension
                        )
                    );
            }
        }

        public IGaSymMultivector this[int id1, int id2]
        {
            get
            {
                GaSymMultivectorBinaryTree1D mvTree1D;
                RootNode.TryGetLeafValue((ulong) id1, out mvTree1D);

                return mvTree1D[id2];
            }
            set
            {
                GaSymMultivectorBinaryTree1D mvTree1D;
                if (!RootNode.TryGetLeafValue((ulong)id1, out mvTree1D) || ReferenceEquals(mvTree1D, null))
                {
                    mvTree1D = new GaSymMultivectorBinaryTree1D(
                        DomainVSpaceDimension2, 
                        TargetVSpaceDimension
                        );

                    RootNode.SetLeafValue((ulong)id1, mvTree1D);
                }

                mvTree1D[id2] = value;
            }
        }

        public MathematicaScalar this[int id1, int id2, int id3]
        {
            get
            {
                GaSymMultivectorBinaryTree1D mvTree1D;
                if (RootNode.TryGetLeafValue((ulong)id1, out mvTree1D))
                    return mvTree1D[id2, id3] ?? GaSymbolicsUtils.Constants.Zero;

                return GaSymbolicsUtils.Constants.Zero;
            }
        }


        public GaSymMultivectorBinaryTree2D(int domainVSpaceDimension1, int domainVSpaceDimension2, int targetVSpaceDimension)
        {
            RootNode = new GaBinaryTree<GaSymMultivectorBinaryTree1D>(domainVSpaceDimension1);
            DomainVSpaceDimension2 = domainVSpaceDimension2;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}