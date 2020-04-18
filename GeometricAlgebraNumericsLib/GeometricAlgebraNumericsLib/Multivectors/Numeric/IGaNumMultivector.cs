using System.Collections.Generic;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// This interface represents a multivector with floating point scalar coefficients
    /// </summary>
    public interface IGaNumMultivector : IGaNumMultivectorSource, IEnumerable<GaTerm<double>>
    {
        /// <summary>
        /// True if this multivector is a single term
        /// </summary>
        /// <returns></returns>
        bool IsTerm();

        /// <summary>
        /// True if this multivector is a single scalar
        /// </summary>
        /// <returns></returns>
        bool IsScalar();

        /// <summary>
        /// True if this multivector is zero
        /// </summary>
        /// <returns></returns>
        bool IsZero();

        /// <summary>
        /// True if this multivector is almost zero
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        bool IsNearZero(double epsilon);

        /// <summary>
        /// Returns the active grades stored in this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> GetStoredGrades();

        /// <summary>
        /// Create a bit pattern where each active grades is a 1
        /// </summary>
        /// <returns></returns>
        int GetStoredGradesBitPattern();
        

        /// <summary>
        /// Extract the vector part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        IGaNumVector GetVectorPart();

        /// <summary>
        /// Extract the vector part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        IGaNumKVector GetKVectorPart(int grade);

        /// <summary>
        /// Get all k-vectors inside this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGaNumKVector> GetKVectorParts();


        /// <summary>
        /// Create and return a Guided Binary Traversal root node from this multivector
        /// </summary>
        /// <returns></returns>
        IGaGbtNode1<double> GetGbtRootNode();

        /// <summary>
        /// Create a Binary Tree Representation of this multivector and return its root node
        /// </summary>
        /// <returns></returns>
        GaBtrInternalNode<double> GetBtrRootNode();

        /// <summary>
        /// Create a Guided Binary Traversal stack suitable for this multivector
        /// </summary>
        /// <returns></returns>
        IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity);


        IGaNumMultivector GetNegative();

        IGaNumMultivector GetReverse();

        IGaNumMultivector GetGradeInv();

        IGaNumMultivector GetCliffConj();
    }
}