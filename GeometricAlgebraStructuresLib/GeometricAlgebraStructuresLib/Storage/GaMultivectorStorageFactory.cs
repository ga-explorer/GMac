using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;

namespace GeometricAlgebraStructuresLib.Storage
{
    /// <summary>
    /// The base class of all factory classes used to construct multivector storage structures
    /// </summary>
    public abstract class GaMultivectorStorageFactory<T>
    {
        private readonly Dictionary<int, T> _scalarValuesDictionary
             = new Dictionary<int, T>();

        private Dictionary<int, T>[] _gradedScalarValuesArray;


        public int VSpaceDimension { get; private set; }

        public int GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public abstract int StoredTermsCount { get; }

        public bool HasGradedStorage 
            => !ReferenceEquals(_gradedScalarValuesArray, null);


        public abstract double this[int id] { get; set; }

        public abstract double this[int grade, int index] { get; set; }


        public abstract bool TryGetValue(int id, out double value);

        public abstract bool TryGetValue(int grade, int index, out double value);

        public abstract bool TryGetTerm(int id, out GaUniformTerm<double> term);

        public abstract bool TryGetTerm(int grade, int index, out GaUniformTerm<double> term);


        public abstract bool IsEmpty();


        protected GaMultivectorStorageFactory(int vSpaceDim)
        {
            Debug.Assert(vSpaceDim.IsValidVSpaceDimension());

            VSpaceDimension = vSpaceDim;
        }


        public int GetKvSpaceDimension(int grade)
        {
            return GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
        }

        /// <summary>
        /// Clear all internal data for this factory without changing
        /// the base vector space dimension
        /// </summary>
        /// <returns></returns>
        public abstract GaMultivectorStorageFactory<T> Reset();

        /// <summary>
        /// Clear all internal data for this factory
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public GaMultivectorStorageFactory<T> Reset(int vSpaceDim)
        {
            Debug.Assert(vSpaceDim.IsValidVSpaceDimension());

            VSpaceDimension = vSpaceDim;

            return Reset();
        }

        /// <summary>
        /// Remove all terms with near zero scalar values
        /// </summary>
        /// <returns></returns>
        public abstract GaMultivectorStorageFactory<T> RemoveNearZeroTerms();


        public abstract IEnumerable<GaUniformTerm<double>> GetStoredTerms();

        public abstract IEnumerable<GaUniformTerm<double>> GetStoredTermsOfGrade(int grade);

        public abstract IEnumerable<GaUniformTerm<double>> GetNonZeroTerms();

        public abstract IEnumerable<GaUniformTerm<double>> GetNonZeroTermsOfGrade(int grade);


        public abstract IEnumerable<int> GetStoredTermIds();

        public abstract IEnumerable<int> GetNonZeroTermIds();

        public abstract IEnumerable<double> GetStoredTermScalars();

        public abstract IEnumerable<double> GetNonZeroTermScalars();


        public abstract bool ContainsStoredTerm(int id);

        public abstract bool ContainsStoredTerm(int grade, int index);

        public abstract bool ContainsStoredTermOfGrade(int grade);


        /// <summary>
        /// Get the vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        public abstract GaMvsVector<T> GetStoredVector();

        /// <summary>
        /// Get the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract GaMvsDenseKVector<T> GetStoredKVector(int grade);


        /// <summary>
        /// Get a copy of the vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        public abstract GaMvsVector<T> GetStoredVectorCopy();

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract GaMvsDenseKVector<T> GetStoredKVectorCopy(int grade);


        /// <summary>
        /// Get all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaMvsDenseKVector<T>> GetStoredKVectors()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredKVector)
                .Where(item => item != null);
        }

        
        /// <summary>
        /// Get a copy of all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaMvsDenseKVector<T>> GetStoredKVectorsCopy()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredKVectorCopy)
                .Where(item => item != null);
        }


        public abstract GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false);

        public abstract GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false);

        public abstract GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false);

        public abstract GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false);

        public abstract GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false);


        public abstract GaMultivectorStorageFactory<T> CopyToFactory();


        public abstract GaMultivectorStorageFactory<T> SetTerm(int id, double value);

        public abstract GaMultivectorStorageFactory<T> SetTerm(int grade, int index, double value);

        public abstract GaMultivectorStorageFactory<T> SetTerm(GaUniformTerm<double> term);

        public abstract GaMultivectorStorageFactory<T> SetTerm(double scalingFactor, GaUniformTerm<double> term);


        public GaMultivectorStorageFactory<T> SetTerms(IEnumerable<GaUniformTerm<double>> termsList)
        {
            foreach (var term in termsList)
                SetTerm(term);

            return this;
        }

        public GaMultivectorStorageFactory<T> SetTerms(double scalingFactor, IEnumerable<GaUniformTerm<double>> termsList)
        {
            foreach (var term in termsList)
                SetTerm(scalingFactor, term);

            return this;
        }


        public abstract GaMultivectorStorageFactory<T> SetKVector(int grade, IReadOnlyList<double> scalarValuesList);

        public abstract GaMultivectorStorageFactory<T> SetKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList);

        public abstract GaMultivectorStorageFactory<T> SetKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList);

        public abstract GaMultivectorStorageFactory<T> SetKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList);


        public GaMultivectorStorageFactory<T> SetKVector(GaNumDarKVector kvector)
        {
            return SetKVector(kvector.Grade, kvector.ScalarValuesArray);
        }

        public GaMultivectorStorageFactory<T> SetKVector(double scalingFactor, GaNumDarKVector kvector)
        {
            return SetKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesArray);
        }

        public GaMultivectorStorageFactory<T> SetKVector(GaNumSarKVector kvector)
        {
            return SetKVector(kvector.Grade, kvector.ScalarValuesDictionary);
        }

        public GaMultivectorStorageFactory<T> SetKVector(double scalingFactor, GaNumSarKVector kvector)
        {
            return SetKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesDictionary);
        }


        public GaMultivectorStorageFactory<T> SetKVectors(IEnumerable<GaNumDarKVector> kVectorsList)
        {
            foreach (var kvector in kVectorsList)
                SetKVector(kvector.Grade, kvector.ScalarValuesArray);

            return this;
        }

        public GaMultivectorStorageFactory<T> SetKVectors(IEnumerable<KeyValuePair<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var pair in scaledKVectorsList)
                SetKVector(pair.Value.Grade, pair.Key, pair.Value.ScalarValuesArray);

            return this;
        }

        public GaMultivectorStorageFactory<T> SetKVectors(IEnumerable<Tuple<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                SetKVector(kVector.Grade, scalingFactor, kVector.ScalarValuesArray);

            return this;
        }


        public abstract GaMultivectorStorageFactory<T> AddTerm(int id, double value);

        public abstract GaMultivectorStorageFactory<T> AddTerm(int grade, int index, double value);

        public abstract GaMultivectorStorageFactory<T> AddTerm(GaUniformTerm<double> term);

        public abstract GaMultivectorStorageFactory<T> AddTerm(double scalingFactor, GaUniformTerm<double> term);


        public abstract GaMultivectorStorageFactory<T> AddTerms(IEnumerable<GaUniformTerm<double>> termsList);

        public abstract GaMultivectorStorageFactory<T> AddTerms(double scalingFactor, IEnumerable<GaUniformTerm<double>> termsList);


        public abstract GaMultivectorStorageFactory<T> AddKVector(int grade, IReadOnlyList<double> scalarValuesList);

        public abstract GaMultivectorStorageFactory<T> AddKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList);

        public abstract GaMultivectorStorageFactory<T> AddKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList);

        public abstract GaMultivectorStorageFactory<T> AddKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList);


        public GaMultivectorStorageFactory<T> AddKVector(GaNumDarKVector kvector)
        {
            return AddKVector(kvector.Grade, kvector.ScalarValuesArray);
        }

        public GaMultivectorStorageFactory<T> AddKVector(double scalingFactor, GaNumDarKVector kvector)
        {
            return AddKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesArray);
        }

        public GaMultivectorStorageFactory<T> AddKVector(GaNumSarKVector kvector)
        {
            return AddKVector(kvector.Grade, kvector.ScalarValuesDictionary);
        }

        public GaMultivectorStorageFactory<T> AddKVector(double scalingFactor, GaNumSarKVector kvector)
        {
            return AddKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesDictionary);
        }


        public GaMultivectorStorageFactory<T> AddKVectors(IEnumerable<GaNumDarKVector> kVectorsList)
        {
            foreach (var kvector in kVectorsList)
                AddKVector(kvector.Grade, kvector.ScalarValuesArray);

            return this;
        }

        public GaMultivectorStorageFactory<T> AddKVectors(IEnumerable<KeyValuePair<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var pair in scaledKVectorsList)
                AddKVector(pair.Value.Grade, pair.Key, pair.Value.ScalarValuesArray);

            return this;
        }

        public GaMultivectorStorageFactory<T> AddKVectors(IEnumerable<Tuple<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                AddKVector(kVector.Grade, scalingFactor, kVector.ScalarValuesArray);

            return this;
        }


        public abstract GaMultivectorStorageFactory<T> ApplyReverse();

        public abstract GaMultivectorStorageFactory<T> ApplyGradeInv();

        public abstract GaMultivectorStorageFactory<T> ApplyCliffConj();

        public abstract GaMultivectorStorageFactory<T> ApplyNegative();


        public abstract GaMultivectorStorageFactory<T> ApplyScaling(double scalingFactor);

        public abstract GaMultivectorStorageFactory<T> ApplyMapping(Func<double, double> mappingFunc);

        public abstract GaMultivectorStorageFactory<T> ApplyMapping(Func<int, double, double> mappingFunc);

        public abstract GaMultivectorStorageFactory<T> ApplyMapping(Func<int, int, double, double> mappingFunc);
    }
}
