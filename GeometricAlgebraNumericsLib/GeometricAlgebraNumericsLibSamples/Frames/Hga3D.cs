using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLibSamples.Frames
{
    /// <summary>
    /// 3D homogeneous GA frame (ex, ey, eo)
    /// </summary>
    public static class Hga3D
    {
        public static GaNumFrameEuclidean Frame { get; }
            = GaNumFrame.CreateEuclidean(3);

        
        


    }
}
