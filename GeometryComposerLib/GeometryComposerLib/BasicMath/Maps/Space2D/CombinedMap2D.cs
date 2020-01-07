using System;
using System.Collections.Generic;
using GeometryComposerLib.BasicMath.Matrices;
using GeometryComposerLib.BasicMath.Tuples;

namespace GeometryComposerLib.BasicMath.Maps.Space2D
{
    public sealed class CombinedMap2D : IAffineMap2D
    {
        private readonly List<Tuple<double, IAffineMap2D>> _affineMapsList 
            = new List<Tuple<double, IAffineMap2D>>();


        public Matrix3X3 ToMatrix()
        {
            throw new NotImplementedException();
        }

        public ITuple2D MapPoint(ITuple2D point)
        {
            throw new NotImplementedException();
        }

        public ITuple2D MapVector(ITuple2D vector)
        {
            throw new NotImplementedException();
        }

        public ITuple2D MapNormal(ITuple2D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap2D InverseMap()
        {
            throw new NotImplementedException();
        }
    }
}
