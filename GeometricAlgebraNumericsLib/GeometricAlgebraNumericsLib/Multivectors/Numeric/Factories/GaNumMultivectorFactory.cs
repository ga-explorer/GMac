using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    /// <summary>
    /// The base class of all factory classes used to construct multivectors
    /// </summary>
    public abstract class GaNumMultivectorFactory : IGaNumMultivectorSource
    {
        public int VSpaceDimension { get; private set; }

        public int GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public abstract int StoredTermsCount { get; }


        public abstract double this[int id] { get; set; }

        public abstract double this[int grade, int index] { get; set; }


        public abstract bool TryGetValue(int id, out double value);

        public abstract bool TryGetValue(int grade, int index, out double value);

        public abstract bool TryGetTerm(int id, out GaTerm<double> term);

        public abstract bool TryGetTerm(int grade, int index, out GaTerm<double> term);


        public abstract bool IsEmpty();


        protected GaNumMultivectorFactory(int vSpaceDim)
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
        public abstract GaNumMultivectorFactory Reset();

        /// <summary>
        /// Clear all internal data for this factory
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public GaNumMultivectorFactory Reset(int vSpaceDim)
        {
            Debug.Assert(vSpaceDim.IsValidVSpaceDimension());

            VSpaceDimension = vSpaceDim;

            return Reset();
        }

        /// <summary>
        /// Remove all terms with near zero scalar values
        /// </summary>
        /// <returns></returns>
        public abstract GaNumMultivectorFactory RemoveNearZeroTerms();


        public abstract IEnumerable<GaTerm<double>> GetStoredTerms();

        public abstract IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade);

        public abstract IEnumerable<GaTerm<double>> GetNonZeroTerms();

        public abstract IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade);


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
        public abstract GaNumVector GetStoredVector();

        /// <summary>
        /// Get the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract IGaNumKVector GetStoredKVector(int grade);

        /// <summary>
        /// Get the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract GaNumDarKVector GetStoredDarKVector(int grade);

        /// <summary>
        /// Get the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract GaNumSarKVector GetStoredSarKVector(int grade);


        /// <summary>
        /// Get a copy of the vector part stored inside this factory. If no vector terms are stored this returns null
        /// </summary>
        /// <returns></returns>
        public abstract GaNumVector GetStoredVectorCopy();

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract IGaNumKVector GetStoredKVectorCopy(int grade);

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract GaNumDarKVector GetStoredDarKVectorCopy(int grade);

        /// <summary>
        /// Get a copy of the k-vector part stored inside this factory. If no k-vector terms are stored this returns null
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public abstract GaNumSarKVector GetStoredSarKVectorCopy(int grade);


        /// <summary>
        /// Get all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGaNumKVector> GetStoredKVectors()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredKVector)
                .Where(item => item != null);
        }

        /// <summary>
        /// Get all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaNumDarKVector> GetStoredDarKVectors()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredDarKVector)
                .Where(item => item != null);
        }

        /// <summary>
        /// Get all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaNumSarKVector> GetStoredSarKVectors()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredSarKVector)
                .Where(item => item != null);
        }


        /// <summary>
        /// Get a copy of all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGaNumKVector> GetStoredKVectorsCopy()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredKVectorCopy)
                .Where(item => item != null);
        }

        /// <summary>
        /// Get a copy of all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaNumDarKVector> GetStoredDarKVectorsCopy()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredDarKVectorCopy)
                .Where(item => item != null);
        }

        /// <summary>
        /// Get a copy of all stored k-vectors in this factory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaNumSarKVector> GetStoredSarKVectorsCopy()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(GetStoredSarKVectorCopy)
                .Where(item => item != null);
        }


        public abstract GaNumDarMultivector GetDarMultivector();

        public abstract GaNumDgrMultivector GetDgrMultivector();

        public abstract GaNumSarMultivector GetSarMultivector();

        public abstract GaNumSgrMultivector GetSgrMultivector();


        public abstract GaNumDarMultivector GetDarMultivectorCopy();

        public abstract GaNumDgrMultivector GetDgrMultivectorCopy();

        public abstract GaNumSarMultivector GetSarMultivectorCopy();

        public abstract GaNumSgrMultivector GetSgrMultivectorCopy();


        public abstract GaNumMultivectorFactory CopyToFactory();


        public abstract GaNumMultivectorFactory SetTerm(int id, double value);

        public abstract GaNumMultivectorFactory SetTerm(int grade, int index, double value);

        public abstract GaNumMultivectorFactory SetTerm(GaTerm<double> term);

        public abstract GaNumMultivectorFactory SetTerm(double scalingFactor, GaTerm<double> term);


        public GaNumMultivectorFactory SetTerms(IEnumerable<GaTerm<double>> termsList)
        {
            foreach (var term in termsList)
                SetTerm(term);

            return this;
        }

        public GaNumMultivectorFactory SetTerms(double scalingFactor, IEnumerable<GaTerm<double>> termsList)
        {
            foreach (var term in termsList)
                SetTerm(scalingFactor, term);

            return this;
        }


        public abstract GaNumMultivectorFactory SetKVector(int grade, IReadOnlyList<double> scalarValuesList);

        public abstract GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList);

        public abstract GaNumMultivectorFactory SetKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList);

        public abstract GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList);


        public GaNumMultivectorFactory SetKVector(GaNumDarKVector kvector)
        {
            return SetKVector(kvector.Grade, kvector.ScalarValuesArray);
        }

        public GaNumMultivectorFactory SetKVector(double scalingFactor, GaNumDarKVector kvector)
        {
            return SetKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesArray);
        }

        public GaNumMultivectorFactory SetKVector(GaNumSarKVector kvector)
        {
            return SetKVector(kvector.Grade, kvector.ScalarValuesDictionary);
        }

        public GaNumMultivectorFactory SetKVector(double scalingFactor, GaNumSarKVector kvector)
        {
            return SetKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesDictionary);
        }


        public GaNumMultivectorFactory SetKVectors(IEnumerable<GaNumDarKVector> kVectorsList)
        {
            foreach (var kvector in kVectorsList)
                SetKVector(kvector.Grade, kvector.ScalarValuesArray);

            return this;
        }

        public GaNumMultivectorFactory SetKVectors(IEnumerable<KeyValuePair<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var pair in scaledKVectorsList)
                SetKVector(pair.Value.Grade, pair.Key, pair.Value.ScalarValuesArray);

            return this;
        }

        public GaNumMultivectorFactory SetKVectors(IEnumerable<Tuple<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                SetKVector(kVector.Grade, scalingFactor, kVector.ScalarValuesArray);

            return this;
        }


        public abstract GaNumMultivectorFactory AddTerm(int id, double value);

        public abstract GaNumMultivectorFactory AddTerm(int grade, int index, double value);

        public abstract GaNumMultivectorFactory AddTerm(GaTerm<double> term);

        public abstract GaNumMultivectorFactory AddTerm(double scalingFactor, GaTerm<double> term);


        public abstract GaNumMultivectorFactory AddTerms(IEnumerable<GaTerm<double>> termsList);

        public abstract GaNumMultivectorFactory AddTerms(double scalingFactor, IEnumerable<GaTerm<double>> termsList);


        public abstract GaNumMultivectorFactory AddKVector(int grade, IReadOnlyList<double> scalarValuesList);

        public abstract GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList);

        public abstract GaNumMultivectorFactory AddKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList);

        public abstract GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList);


        public GaNumMultivectorFactory AddKVector(GaNumDarKVector kvector)
        {
            return AddKVector(kvector.Grade, kvector.ScalarValuesArray);
        }

        public GaNumMultivectorFactory AddKVector(double scalingFactor, GaNumDarKVector kvector)
        {
            return AddKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesArray);
        }

        public GaNumMultivectorFactory AddKVector(GaNumSarKVector kvector)
        {
            return AddKVector(kvector.Grade, kvector.ScalarValuesDictionary);
        }

        public GaNumMultivectorFactory AddKVector(double scalingFactor, GaNumSarKVector kvector)
        {
            return AddKVector(kvector.Grade, scalingFactor, kvector.ScalarValuesDictionary);
        }


        public GaNumMultivectorFactory AddKVectors(IEnumerable<GaNumDarKVector> kVectorsList)
        {
            foreach (var kvector in kVectorsList)
                AddKVector(kvector.Grade, kvector.ScalarValuesArray);

            return this;
        }

        public GaNumMultivectorFactory AddKVectors(IEnumerable<KeyValuePair<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var pair in scaledKVectorsList)
                AddKVector(pair.Value.Grade, pair.Key, pair.Value.ScalarValuesArray);

            return this;
        }

        public GaNumMultivectorFactory AddKVectors(IEnumerable<Tuple<double, GaNumDarKVector>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                AddKVector(kVector.Grade, scalingFactor, kVector.ScalarValuesArray);

            return this;
        }


        public abstract GaNumMultivectorFactory ApplyReverse();

        public abstract GaNumMultivectorFactory ApplyGradeInv();

        public abstract GaNumMultivectorFactory ApplyCliffConj();

        public abstract GaNumMultivectorFactory ApplyNegative();


        public abstract GaNumMultivectorFactory ApplyScaling(double scalingFactor);

        public abstract GaNumMultivectorFactory ApplyMapping(Func<double, double> mappingFunc);

        public abstract GaNumMultivectorFactory ApplyMapping(Func<int, double, double> mappingFunc);

        public abstract GaNumMultivectorFactory ApplyMapping(Func<int, int, double, double> mappingFunc);
    }
}
