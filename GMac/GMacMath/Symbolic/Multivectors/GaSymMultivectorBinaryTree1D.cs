using GMac.GMacMath.Structures;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree1D
    {
        internal GMacBinaryTree<IGaSymMultivector> RootNode { get; }

        public int DomainVSpaceDimension
            => RootNode.TreeDepth;

        public int DomainGaSpaceDimension
            => RootNode.TreeDepth.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IGaSymMultivector this[int id1]
        {
            get
            {
                IGaSymMultivector mv;
                RootNode.TryGetLeafValue((ulong) id1, out mv);

                return mv;
            }
            set
            {
                RootNode.SetLeafValue(
                    (ulong)id1, 
                    value ?? GaSymMultivector.CreateZero(TargetGaSpaceDimension)
                    );
            }
        }

        public MathematicaScalar this[int id1, int id2]
        {
            get
            {
                IGaSymMultivector mv;
                return 
                    RootNode.TryGetLeafValue((ulong) id1, out mv)
                    ? (mv[id2]?.ToMathematicaScalar() ?? SymbolicUtils.Constants.Zero)
                    : SymbolicUtils.Constants.Zero;
            }
        }


        public GaSymMultivectorBinaryTree1D(int domainVSpaceDimension, int targetVSpaceDimension)
        {
            RootNode = new GMacBinaryTree<IGaSymMultivector>(domainVSpaceDimension);
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}