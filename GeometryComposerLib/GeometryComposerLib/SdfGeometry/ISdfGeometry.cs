using GeometryComposerLib.BasicMath.Tuples.Immutable;
using GeometryComposerLib.BasicShapes.Lines.Immutable;

namespace GeometryComposerLib.SdfGeometry
{
    /// <summary>
    /// This interface represents 3D geometry defined using a
    /// Scalar Distance Function (SDF)
    /// </summary>
    public interface  ISdfGeometry3D
    {
        double SdfDistanceDelta { get; }

        double SdfDistanceDeltaInv { get; }

        double SdfAlpha { get; }

        double SdfDelta { get; }

        double ComputeSdf(Tuple3D point);

        double ComputeSdfRayStep(Line3D ray, double t0);

        Tuple3D ComputeSdfNormal(Tuple3D point);
    }
}
