using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLibSamples.Frames
{
    /// <summary>
    /// 3D Euclidean GA frame (ex, ey)
    /// </summary>
    public static class E2D
    {
        public static GaNumFrameEuclidean Frame { get; }
            = GaNumFrame.CreateEuclidean(2);


    }
}