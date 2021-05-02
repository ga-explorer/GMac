using System;
using System.Collections.Generic;
using System.Linq;

namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This interface represents a multivector in the Ega3D frame.
    /// </summary>
    public interface IEga3DMultivector
    {
        /// <summary>
        /// The number of current terms stored in this multivectors
        /// </summary>
        int StoredTermsCount { get; }

        /// <summary>
        /// Get or set the scalar coefficient of a basis blade of a given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        double this[int id] { get; }

        /// <summary>
        /// Get or set the scalar coefficient of a basis blade of a given
        /// grade and index
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        double this[int grade, int index] { get; }

        /// <summary>
        /// Get the number of k-vectors stored in this multivector 
        /// </summary>
        double KVectorsCount { get; }

        /// <summary>
        /// Get the grades of the k-vectors stored in this multivector 
        /// </summary>
        IEnumerable<int> KVectorGrades { get; }

        /// <summary>
        /// Get the k-vectors stored in this multivector 
        /// </summary>
        IEnumerable<Ega3DkVector> KVectors { get; }

        /// <summary>
        /// Get all (id, scalar) terms stored in this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, double>> GetStoredTermsById();

        /// <summary>
        /// Get all (grade, index, scalar) terms stored in this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, int, double>> GetStoredTermsByGradeIndex();

        /// <summary>
        /// Get all (id, scalar) terms with non zero scalar value
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, double>> GetNonZeroTermsById();

        /// <summary>
        /// Get all (grade, index, scalar) terms with non zero scalar value
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, int, double>> GetNonZeroTermsByGradeIndex();

        /// <summary>
        /// Get a k-vector of a given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        Ega3DkVector GetKVector(int grade);
    }

    /// <summary>
    /// This class represents a multivector in the Ega3D frame. A multivector
    /// contains an array of n+1 k-vectors of grades 0 to n. If a k-vector is not
    /// needed, a null is stored in the internal KVectorsArray instead to save
    /// memory.
    /// </summary>
    public sealed partial class Ega3DMultivector 
        : IEga3DMultivector
    {
        public static Ega3DkVector Zero { get; }
            = new Ega3DkVector();

        


        /// <summary>
        /// The internal array holding k-vectors of this multivector
        /// </summary>
        internal Ega3DkVector[] KVectorsArray { get; }
	        = new Ega3DkVector[6];

        public int StoredTermsCount 
            => KVectors.Sum(v => v.StoredTermsCount);

        public double this[int grade, int index]
        {
	        get
	        {
		        var kVector = KVectorsArray[grade];

		        if (ReferenceEquals(kVector, null)) 
			        return 0;

		        return kVector.Scalars[index];
	        }
	        set
	        {
		        var kVector = KVectorsArray[grade];

		        if (ReferenceEquals(kVector, null))
		        {
			        if (value == 0) 
				        return;

			        kVector = new Ega3DkVector(grade);

			        KVectorsArray[grade] = kVector;
		        }

		        kVector.Scalars[index] = value;
	        }
        }

        public double this[int id]
        {
	        get
	        {
		        var grade = Ega3DUtils.GradeLookupTable[id];
		        var index = Ega3DUtils.IndexLookupTable[id];

		        return this[grade, index];
	        }
	        set
	        {
		        var grade = Ega3DUtils.GradeLookupTable[id];
		        var index = Ega3DUtils.IndexLookupTable[id];

		        this[grade, index] = value;
	        }
        }

        public double KVectorsCount 
	        => KVectorsArray
		        .Count(v => !ReferenceEquals(v, null));

        public IEnumerable<int> KVectorGrades
	        => KVectorsArray
		        .Where(v => !ReferenceEquals(v, null))
		        .Select(v => v.Grade);

        public IEnumerable<Ega3DkVector> KVectors
	        => KVectorsArray
		        .Where(v => !ReferenceEquals(v, null));


        /// <summary>
        /// Create a zero multivector
        /// </summary>
        public Ega3DMultivector()
        {
        }

        /// <summary>
        /// Create a scalar multivector
        /// </summary>
        /// <param name="scalar"></param>
        public Ega3DMultivector(double scalar)
        {
	        KVectorsArray[0] = new Ega3DkVector(scalar);
        }

        /// <summary>
        /// Create a multivector containing a single k-vector
        /// </summary>
        /// <param name="kVector"></param>
        public Ega3DMultivector(Ega3DkVector kVector)
        {
	        KVectorsArray[kVector.Grade] = kVector;
        }

        /// <summary>
        /// Create a multivector and fill its terms using a list of
        /// (grade, index, scalar) tuples
        /// </summary>
        /// <param name="terms"></param>
        public Ega3DMultivector(IEnumerable<Tuple<int, int, double>> terms)
        {
            foreach (var (grade, index, scalar) in terms)
                this[grade, index] = scalar;
        }

        /// <summary>
        /// Create a multivector and fill its terms using a list of
        /// (id, scalar) tuples
        /// </summary>
        /// <param name="terms"></param>
        public Ega3DMultivector(IEnumerable<Tuple<int, double>> terms)
        {
            foreach (var (id, scalar) in terms)
                this[id] = scalar;
        }


        public IEnumerable<Tuple<int, double>> GetStoredTermsById()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                var idTable = Ega3DUtils.IdLookupTable[grade];
                for (var index = 0; index < kVector.StoredTermsCount; index++)
                    yield return new Tuple<int, double>(
                        idTable[index], kVector[index]
                    );
            }
        }

        public IEnumerable<Tuple<int, int, double>> GetStoredTermsByGradeIndex()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                for (var index = 0; index < kVector.StoredTermsCount; index++)
                    yield return new Tuple<int, int, double>(
                        grade, index, kVector[index]
                    );
            }
        }

        public IEnumerable<Tuple<int, double>> GetNonZeroTermsById()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                var idTable = Ega3DUtils.IdLookupTable[grade];
                for (var index = 0; index < kVector.StoredTermsCount; index++)
                {
                    var scalar = kVector[index];
                    if (scalar.IsNearZero())
                        continue;

                    yield return new Tuple<int, double>(
                        idTable[index], scalar
                    );
                }
            }
        }

        public IEnumerable<Tuple<int, int, double>> GetNonZeroTermsByGradeIndex()
        {
            for (var grade = 0; grade <= KVectorsArray.Length; grade++)
            {
                var kVector = KVectorsArray[grade];
                if (ReferenceEquals(kVector, null)) 
                    continue;

                for (var index = 0; index < kVector.StoredTermsCount; index++)
                {
                    var scalar = kVector[index];
                    if (scalar.IsNearZero())
                        continue;

                    yield return new Tuple<int, int, double>(
                        grade, index, scalar
                    );
                }
            }
        }

        public Ega3DkVector GetKVector(int grade)
        {
	        return KVectorsArray[grade] 
		           ?? new Ega3DkVector(grade);
        }

        /// <summary>
        /// Set a k-vector of a given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="kVector"></param>
        /// <returns></returns>
        public Ega3DMultivector SetKVector(int grade, Ega3DkVector kVector)
        {
	        KVectorsArray[grade] = kVector;
	        return this;
        }

        /// <summary>
        /// Remove a k-vector of a given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public Ega3DMultivector RemoveKVector(int grade)
        {
	        KVectorsArray[grade] = null;
	        return this;
        }

        /// <summary>
        /// Set a k-vector of a given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="scalars"></param>
        /// <returns></returns>
        internal Ega3DkVector SetKVector(int grade, double[] scalars)
        {
	        var kVector = new Ega3DkVector(grade, scalars);

	        KVectorsArray[grade] = kVector;

	        return kVector;
        }
    }
}