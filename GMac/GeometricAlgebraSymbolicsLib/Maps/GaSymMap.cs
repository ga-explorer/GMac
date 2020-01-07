using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;

namespace GeometricAlgebraSymbolicsLib.Maps
{
    public abstract class GaSymMap : IGaSymMap
    {
        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;


        public abstract int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();


        protected GaSymMap()
        {
            CasInterface = GaSymbolicsUtils.Cas;
        }
    }
}