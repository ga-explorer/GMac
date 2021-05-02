using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree1D
    {
        internal GaBtrInternalNode<IGaSymMultivector> RootNode { get; }

        public int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public ulong TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IGaSymMultivector this[ulong id1]
        {
            get
            {
                RootNode.TryGetLeafValue(DomainVSpaceDimension, id1, out var mv);

                return mv;
            }
            set =>
                RootNode.SetLeafValue(
                    DomainVSpaceDimension, 
                    id1, 
                    value ?? GaSymMultivector.CreateZero(TargetVSpaceDimension)
                );
        }

        public MathematicaScalar this[ulong id1, ulong id2] 
            => RootNode.TryGetLeafValue(DomainVSpaceDimension, id1, out var mv)
                ? (mv[id2]?.ToMathematicaScalar() ?? GaSymbolicsUtils.Constants.Zero)
                : GaSymbolicsUtils.Constants.Zero;


        public GaSymMultivectorBinaryTree1D(int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<IGaSymMultivector>();

            DomainVSpaceDimension = targetVSpaceDimension;
            TargetVSpaceDimension = targetVSpaceDimension;
        }

        public GaSymMultivectorBinaryTree1D(int domainVSpaceDimension, int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<IGaSymMultivector>();

            DomainVSpaceDimension = domainVSpaceDimension;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}