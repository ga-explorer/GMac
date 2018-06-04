using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;

namespace GeometricAlgebraSymbolicsLib.Multivectors
{
    public sealed class GaSymMultivectorBinaryTree1D
    {
        internal GaBinaryTree<IGaSymMultivector> RootNode { get; }

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
                    ? (mv[id2]?.ToMathematicaScalar() ?? GaSymbolicsUtils.Constants.Zero)
                    : GaSymbolicsUtils.Constants.Zero;
            }
        }


        public GaSymMultivectorBinaryTree1D(int domainVSpaceDimension, int targetVSpaceDimension)
        {
            RootNode = new GaBinaryTree<IGaSymMultivector>(domainVSpaceDimension);
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}