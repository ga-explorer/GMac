using System;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public interface IGaMultivectorStorage<T> : IEnumerable<KeyValuePair<int, T>>
    {
        /// <summary>
        /// This is used for low-level computations on scalars like adding,
        /// subtracting, multiplying, and dividing scalars
        /// </summary>
        IGaScalarDomain<T> ScalarDomain { get; }
        
        /// <summary>
        /// The dimension of the base Vector Space of the multivector
        /// </summary>
        int VSpaceDimension { get; }

        /// <summary>
        /// The dimension of the Geometric Algebra space of the multivector
        /// </summary>
        int GaSpaceDimension { get; }
        
        /// <summary>
        /// The number of terms stored in this multivector
        /// </summary>
        int StoredTermsCount { get; }

        
        /// <summary>
        /// Read the scalar coefficient associated with the given basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetTermScalar(int id);

        /// <summary>
        /// Read the scalar coefficient associated with the given basis blade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        T GetTermScalar(int grade, int index);

        IGaTerm<T> GetTerm(int id);
        
        IGaTerm<T> GetTerm(int grade, int index);

        
        /// <summary>
        /// Try to get the given term scalar value if stored in this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetTermScalar(int id, out T value);

        /// <summary>
        /// Try to get the given term scalar value if stored in this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetTermScalar(int grade, int index, out T value);

        /// <summary>
        /// Try to get the given term if stored in this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        bool TryGetTerm(int id, out IGaTerm<T> term);

        /// <summary>
        /// Try to get the given term if stored in this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        bool TryGetTerm(int grade, int index, out IGaTerm<T> term);

        
        /// <summary>
        /// Set the scalar coefficient associated with the given basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        IGaMultivectorStorage<T> SetTermScalar(int id, T value);

        /// <summary>
        /// Set the scalar coefficient associated with the given basis blade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        IGaMultivectorStorage<T> SetTermScalar(int grade, int index, T value);

        /// <summary>
        /// Set the given terms
        /// </summary>
        /// <param name="termsList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetTerms(IEnumerable<IGaTerm<T>> termsList);
        
        /// <summary>
        /// Set the given terms by a scaling factor 
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="termsList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList);

        
        /// <summary>
        /// Try to set the given term
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TrySetTermScalar(int id, T value);

        /// <summary>
        /// Try to set the given term
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TrySetTermScalar(int grade, int index, T value);
        
        
        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVector(int grade, IReadOnlyList<T> scalarValuesList);
        
        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVector(int grade, IEnumerable<KeyValuePair<int, T>> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IEnumerable<KeyValuePair<int, T>> scalarValuesList);

        /// <summary>
        /// Set some terms using the given k-vector data
        /// </summary>
        /// <param name="kvector"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVector(IGaKVectorStorage<T> kvector);

        /// <summary>
        /// Set some terms using the given k-vector data scaled by a scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="kvector"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVector(T scalingFactor, IGaKVectorStorage<T> kvector);

        /// <summary>
        /// Set some terms using the given k-vectors data
        /// </summary>
        /// <param name="kVectorsList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVectors(IEnumerable<IGaKVectorStorage<T>> kVectorsList);

        /// <summary>
        /// Set some terms using the given k-vectors data scaled by a scaling factor
        /// </summary>
        /// <param name="scaledKVectorsList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> SetKVectors(IEnumerable<Tuple<T, IGaKVectorStorage<T>>> scaledKVectorsList);

        
        /// <summary>
        /// Add the given term to this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddTerm(int id, T value);

        /// <summary>
        /// Add the given term to this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddTerm(int grade, int index, T value);

        /// <summary>
        /// Add the given term to this multivector
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddTerm(IGaTerm<T> term);

        /// <summary>
        /// Add the given scaled term to this multivector
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddTerm(T scalingFactor, IGaTerm<T> term);

        /// <summary>
        /// Add the given terms to this multivector
        /// </summary>
        /// <param name="termsList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddTerms(IEnumerable<IGaTerm<T>> termsList);

        /// <summary>
        /// Add the given scaled terms to this multivector
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <param name="termsList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList);

        
        /// <summary>
        /// Try adding the given term to this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryAddTerm(int id, T value);

        /// <summary>
        /// Try adding the given term to this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryAddTerm(int grade, int index, T value);

        
        /// <summary>
        /// Add the terms of the given k-vector to this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddKVector(int grade, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Add the scaled terms of the given k-vector to this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList);

        /// <summary>
        /// Add the terms of the given k-vector to this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddKVector(int grade, IEnumerable<KeyValuePair<int, T>> scalarValuesList);

        /// <summary>
        /// Add the scaled terms of the given k-vector to this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalingFactor"></param>
        /// <param name="scalarValuesList"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IEnumerable<KeyValuePair<int, T>> scalarValuesList);

        IGaMultivectorStorage<T> AddKVector(IGaKVectorStorage<T> kvector);

        IGaMultivectorStorage<T> AddKVector(T scalingFactor, IGaKVectorStorage<T> kvector);

        IGaMultivectorStorage<T> AddKVectors(IEnumerable<IGaKVectorStorage<T>> kVectorsList);

        IGaMultivectorStorage<T> AddKVectors(IEnumerable<Tuple<T, IGaKVectorStorage<T>>> scaledKVectorsList);
        
        
        /// <summary>
        /// Remove the given term if possible, else set to zero
        /// </summary>
        /// <param name="id"></param>
        IGaMultivectorStorage<T> RemoveTerm(int id);
        
        /// <summary>
        /// Remove the given term if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        IGaMultivectorStorage<T> RemoveTerm(int grade, int index);

        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="idsList"></param>
        IGaMultivectorStorage<T> RemoveTerms(IEnumerable<int> idsList);
        
        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        IGaMultivectorStorage<T> RemoveTerms(int grade, IEnumerable<int> indexList);

        IGaMultivectorStorage<T> RemoveTerms(Func<T, bool> selectionFilter);

        IGaMultivectorStorage<T> RemoveTerms(Func<int, bool> selectionFilter);

        IGaMultivectorStorage<T> RemoveTerms(Func<int, int, bool> selectionFilter);
        
        IGaMultivectorStorage<T> RemoveTerms(Func<int, T, bool> selectionFilter);
        
        IGaMultivectorStorage<T> RemoveTerms(Func<int, int, T, bool> selectionFilter);
        
        /// <summary>
        /// Remove the given terms if possible, else set to zero
        /// </summary>
        /// <param name="grade"></param>
        IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade);


        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> RemoveTermIfZero(int id, bool nearZeroFlag = false);

        /// <summary>
        /// Remove the given term if zero
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IGaMultivectorStorage<T> RemoveTermIfZero(int grade, int index, bool nearZeroFlag = false);
        
        /// <summary>
        /// Remove all terms from storage where their scalar values is zero
        /// </summary>
        IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false);

        /// <summary>
        /// Remove all terms from storage where their scalar values is near zero
        /// </summary>
        IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false);

        /// <summary>
        /// Reset the storage to represent a zero multivector
        /// </summary>
        IGaMultivectorStorage<T> ResetToZero();

        
        /// <summary>
        /// True if this multivector stores no terms internally
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// True if all terms are zero 
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        bool IsZero(bool nearZeroFlag = false);
        
        /// <summary>
        /// Test if this storage contains the given term
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ContainsStoredTerm(int id);

        /// <summary>
        /// Test if this storage contains the given term
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool ContainsStoredTerm(int grade, int index);
        
        /// <summary>
        /// Test if this storage contains one or more terms of the given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        bool ContainsStoredTermOfGrade(int grade);

        
        /// <summary>
        /// True if this storage can store the given term
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CanStoreTerm(int id);

        /// <summary>
        /// True if this storage can store the given term
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool CanStoreTerm(int grade, int index);

        /// <summary>
        /// True if this storage can store one or more terms of the given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        bool CanStoreSomeTermsOfGrade(int grade);

        /// <summary>
        /// True if this storage can store all terms of the given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        bool CanStoreAllTermsOfGrade(int grade);
        
        
        /// <summary>
        /// The basis blade ids with coefficients stored in this multivector
        /// </summary>
        IEnumerable<int> GetStoredTermIds();

        /// <summary>
        /// Select stored term IDs by a filter that takes the term's scalar value as input
        /// </summary>
        /// <param name="selectionFilter"></param>
        /// <returns></returns>
        IEnumerable<int> GetStoredTermIds(Func<T, bool> selectionFilter);
        
        /// <summary>
        /// Select stored term IDs by a filter that takes the term's ID value as input
        /// </summary>
        /// <param name="selectionFilter"></param>
        /// <returns></returns>
        IEnumerable<int> GetStoredTermIds(Func<int, bool> selectionFilter);
        
        /// <summary>
        /// Select stored term IDs by a filter that takes the term's grade and index as input
        /// </summary>
        /// <param name="selectionFilter"></param>
        /// <returns></returns>
        IEnumerable<int> GetStoredTermIds(Func<int, int, bool> selectionFilter);
        
        /// <summary>
        /// Select stored term IDs by a filter that takes the term's ID and scalar value as input
        /// </summary>
        /// <param name="selectionFilter"></param>
        /// <returns></returns>
        IEnumerable<int> GetStoredTermIds(Func<int, T, bool> selectionFilter);
        
        /// <summary>
        /// Select stored term IDs by a filter that takes the term's grade, index, and scalar value as input
        /// </summary>
        /// <param name="selectionFilter"></param>
        /// <returns></returns>
        IEnumerable<int> GetStoredTermIds(Func<int, int, T, bool> selectionFilter);

        IEnumerable<int> GetStoredTermIdsOfGrade(int grade);
        
        
        IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices();

        IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter);

        IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, bool> selectionFilter);

        IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, int, bool> selectionFilter);

        IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, T, bool> selectionFilter);

        IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, int, T, bool> selectionFilter);
        
        IEnumerable<int> GetStoredTermIndicesOfGrade(int grade);
        

        IEnumerable<T> GetStoredTermScalars();

        IEnumerable<T> GetStoredTermScalars(Func<T, bool> selectionFilter);

        IEnumerable<T> GetStoredTermScalars(Func<int, bool> selectionFilter);

        IEnumerable<T> GetStoredTermScalars(Func<int, T, bool> selectionFilter);

        IEnumerable<T> GetStoredTermScalars(Func<int, int, T, bool> selectionFilter);

        IEnumerable<T> GetStoredTermScalarsOfGrade(int grade);
        
        
        /// <summary>
        /// Get all terms currently stored inside this factory
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGaTerm<T>> GetStoredTerms();
        
        IEnumerable<IGaTerm<T>> GetStoredTerms(Func<T, bool> selectionFilter);
        
        IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, bool> selectionFilter);
        
        IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, int, bool> selectionFilter);
        
        IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, T, bool> selectionFilter);
        
        IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, int, T, bool> selectionFilter);

        /// <summary>
        /// Get all terms of given grade currently stored inside this factory
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade);
        

        /// <summary>
        /// Return the number of zero terms stored inside this multivector storage
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        int GetStoredZeroTermsCount(bool nearZeroFlag = false);
        
        IEnumerable<int> GetStoredZeroTermIds(bool nearZeroFlag = false);
        
        IEnumerable<Tuple<int, int>> GetStoredZeroTermGradeIndices(bool nearZeroFlag = false);
        
        IEnumerable<int> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false);
        
        IEnumerable<int> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false);


        /// <summary>
        /// Return the number of non-zero terms inside this multivector storage
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        int GetNonZeroTermsCount(bool nearZeroFlag = false);

        /// <summary>
        /// Return the number of non-zero terms inside this multivector storage grouped by grade
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false);
        
        /// <summary>
        /// The basis blade ids with non-zero coefficients stored in this multivector storage
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        IEnumerable<int> GetNonZeroTermIds(bool nearZeroFlag = false);
        
        /// <summary>
        /// The basis blade ids with non-zero coefficients stored in this multivector
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        IEnumerable<Tuple<int, int>> GetNonZeroTermGradeIndices(bool nearZeroFlag = false);

        /// <summary>
        /// The basis blade non-zero coefficients stored in this multivector
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        IEnumerable<T> GetNonZeroTermScalars(bool nearZeroFlag = false);

        /// <summary>
        /// Get all non-zero terms currently stored inside this factory
        /// </summary>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IEnumerable<IGaTerm<T>> GetNonZeroTerms(bool nearZeroFlag = false);

        /// <summary>
        /// Get all non-zero terms of given grade currently stored inside this factory
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="nearZeroFlag"></param>
        /// <returns></returns>
        IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false);

        
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
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        GaBinaryTree<T> GetBinaryTree();


        /// <summary>
        /// Create a copy of a term stored inside this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="getCopy"></param>
        /// <returns></returns>
        GaMvsTerm<T> GetTermStorage(int id, bool getCopy = false);
        
        /// <summary>
        /// Create a copy of a term stored inside this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="getCopy"></param>
        /// <returns></returns>
        GaMvsTerm<T> GetTermStorage(int grade, int index, bool getCopy = false);

        /// <summary>
        /// Get a copy of the vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        GaMvsVector<T> GetVectorStorage(bool getCopy = false);

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false);

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false);

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false);
        
        /// <summary>
        /// Get all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false);
        
        
        GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false);

        GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false);

        GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false);

        GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false);

        GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false);

        IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false);
        
        
        IGaMultivectorStorage<T> ApplyReverse();

        IGaMultivectorStorage<T> ApplyGradeInv();

        IGaMultivectorStorage<T> ApplyCliffConj();

        IGaMultivectorStorage<T> ApplyNegative();

        IGaMultivectorStorage<T> ApplyScaling(T scalingFactor);

        IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc);

        IGaMultivectorStorage<T> ApplyMapping(Func<int, T, T> mappingFunc);

        IGaMultivectorStorage<T> ApplyMapping(Func<int, int, T, T> mappingFunc);
    }
}
