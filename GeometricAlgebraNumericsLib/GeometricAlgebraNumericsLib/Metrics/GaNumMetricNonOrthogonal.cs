using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Outermorphisms;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Metrics
{
    /// <summary>
    /// This class holds main information of a Change-of-Basis Automorphism Frame System
    /// </summary>
    public sealed class GaNumMetricNonOrthogonal : IGaNumMetric
    {
        public GaNumFrame BaseFrame { get; }

        public GaNumFrame DerivedFrame { get; }

        public GaNumOutermorphism DerivedToBaseCba { get; }

        public GaNumOutermorphism BaseToDerivedCba { get; }

        public IGaNumMetricOrthogonal BaseMetric => BaseFrame.BaseOrthogonalMetric;

        public int VSpaceDimension => BaseFrame.VSpaceDimension;

        public int GaSpaceDimension => BaseFrame.GaSpaceDimension;

        public Matrix BaseFrameIpm => BaseFrame.Ipm;

        public Matrix DerivedFrameIpm => DerivedFrame.Ipm;


        internal GaNumMetricNonOrthogonal(GaNumFrame baseFrame, GaNumFrame derivedFrame, GaNumOutermorphism derivedToBaseCba, GaNumOutermorphism baseToDerivedCba)
        {
            BaseFrame = baseFrame;
            DerivedFrame = derivedFrame;
            DerivedToBaseCba = derivedToBaseCba;
            BaseToDerivedCba = baseToDerivedCba;
        }
    }
}
