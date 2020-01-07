using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLibSamples.Frames
{
    /// <summary>
    /// 3D Euclidean GA frame (ex, ey, ez)
    /// </summary>
    public static class E3D
    {
        public static GaNumFrameEuclidean Frame { get; }
            = GaNumFrame.CreateEuclidean(3);


    }
}