using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Symbolic.Maps.Unilinear;
using SymbolicInterface.Mathematica;

namespace GMac.GMacMath.Symbolic.Metrics
{
    /// <summary>
    /// This class holds main information of a Change-of-Basis Automorphism Frame System
    /// </summary>
    public sealed class GaSymMetricNonOrthogonal : IGaSymMetric
    {
        public GaSymFrame BaseFrame { get; }

        public GaSymFrame DerivedFrame { get; }

        public GaSymOutermorphism DerivedToBaseCba { get; }

        public GaSymOutermorphism BaseToDerivedCba { get; }

        public IGaSymMetricOrthogonal BaseMetric => BaseFrame.BaseOrthogonalMetric;

        public int VSpaceDimension => BaseFrame.VSpaceDimension;

        public int GaSpaceDimension => BaseFrame.GaSpaceDimension;

        public ISymbolicMatrix BaseFrameIpm => BaseFrame.Ipm;

        public ISymbolicMatrix DerivedFrameIpm => DerivedFrame.Ipm;


        internal GaSymMetricNonOrthogonal(GaSymFrame baseFrame, GaSymFrame derivedFrame, GaSymOutermorphism derivedToBaseCba, GaSymOutermorphism baseToDerivedCba)
        {
            BaseFrame = baseFrame;
            DerivedFrame = derivedFrame;
            DerivedToBaseCba = derivedToBaseCba;
            BaseToDerivedCba = baseToDerivedCba;
        }
    }
}
