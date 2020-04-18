using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Metrics
{
    public interface IGaNumMetricOrthogonal : IGaNumMetric
    {
        /// <summary>
        /// A list containing basis vectors signatures for this metric
        /// </summary>
        IReadOnlyList<double> BasisVectorsSignatures { get; }

        /// <summary>
        /// Get the signature of a basis vector; its geometric product with itself
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        double GetBasisVectorSignature(int index);

        /// <summary>
        /// Get the signature of a basis blade; its geometric product with itself
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        double GetBasisBladeSignature(int id);

        /// <summary>
        /// Compute the geometric product of two basis blades
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        GaTerm<double> Gp(int id1, int id2);

        /// <summary>
        /// Compute the geometric product of two basis blades
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        GaTerm<double> ScaledGp(int id1, int id2, double scalingFactor);

        /// <summary>
        /// Compute the geometric product of three basis blades
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        GaTerm<double> Gp(int id1, int id2, int id3);

        /// <summary>
        /// Compute the geometric product of three basis blades
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        GaTerm<double> ScaledGp(int id1, int id2, int id3, double scalingFactor);
    }
}