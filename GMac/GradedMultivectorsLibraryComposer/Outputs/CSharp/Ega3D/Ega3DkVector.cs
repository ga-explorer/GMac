using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
    {
        /// <summary>
        /// Ordered coefficients of k-vector in the additive representation. 
        /// </summary>
        internal double[] Scalars { get; }
        
        public int StoredTermsCount
        	=> Scalars.Length;
        
        /// <summary>
        /// Grade of blade.
        /// </summary>
        public int Grade { get; }
        
        /// <summary>
        /// Get or set the scalar coefficient of a basis blade of a given
        /// grade and index. Setting the scalar of a grade != Grade raises
        /// an exception.
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int grade, int index]
        {
        	get
        	{
        		if (grade != Grade) 
        			return 0;
        
        		return Scalars[index];
        	}
        	set
        	{
        		if (grade != Grade)
        			throw new IndexOutOfRangeException();
        
        		Scalars[index] = value;
        	}
        }
        
        public double KVectorsCount 
        	=> 1;
        
        public IEnumerable<int> KVectorGrades
        {
        	get { yield return Grade; }
        }
        
        public IEnumerable<Ega3DkVector> KVectors
        {
        	get { yield return this; }
        }
        
        /// <summary>
        /// Get or set the scalar coefficient of a basis blade of a given ID
        /// Setting the scalar of a grade != Grade raises an exception.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public double this[int id]
        {
        	get
        	{
        		var grade = Ega3DUtils.GradeLookupTable[id];
        
        		if (grade != Grade) 
        			return 0;
        
        		var index = Ega3DUtils.IndexLookupTable[id];
        
        		return Scalars[index];
        	}
        	set
        	{
        		var grade = Ega3DUtils.GradeLookupTable[id];
        
        		if (grade != Grade)
        			throw new IndexOutOfRangeException();
        
        		var index = Ega3DUtils.IndexLookupTable[id];
        
        		Scalars[index] = value;
        	}
        }
        
        /// <summary>
        /// This k-vector is a zero k-vector: it has no internal coefficients and its grade is any legal grade
        /// This kind of k-vector should be treated separately in operations on k-vectors
        /// </summary>
        public bool IsZero 
        	=> Scalars.Length == 0;
        
        /// <summary>
        /// True if this k-vector is a null k-vector
        /// </summary>
        public bool IsNull
            => IsZero || Ega3DUtils.IsNearZero(Norm2);
        
        public bool IsScalar 
        	=> IsZero || Grade == 0;
        
        public bool IsVector 
        	=> IsZero || Grade == 1;
        
        public bool IsPseudoVector 
        	=> IsZero || Grade == Ega3DUtils.VectorSpaceDimensions - 1;
        
        public bool IsPseudoScalar 
        	=> IsZero || Grade == Ega3DUtils.VectorSpaceDimensions;
        
        public string[] BasisBladesNames 
        	=> BasisBladesNamesArray[Grade];
        
        /// <summary>
        /// True if the coefficients represent a blade; not a general non-simple k-vector.
        /// </summary>
        public bool IsBlade 
        	=> SelfDPGrade() == 0;
        
        /// <summary>
        /// True if the coefficients represent a general non-simple k-vector; not a blade.
        /// </summary>
        public bool IsNonBlade 
        	=> SelfDPGrade() != 0;
        
        /// <summary>
        /// Create a k-vector and initialize its coefficients to zero.
        /// </summary>
        internal Ega3DkVector(int grade)
        {
        	Grade = grade;
        	Scalars = new double[Ega3DUtils.KVectorSizesLookupTable[grade]];
        }
        
        /// <summary>
        /// Create a k-vector and initialize its coefficients by the given array. 
        /// </summary>
        internal Ega3DkVector(int grade, double[] scalars)
        {
        	if (scalars.Length != Ega3DUtils.KVectorSizesLookupTable[grade])
        		throw new ArgumentException(@"The given array has the wrong number of items for this grade", nameof(scalars));
        
        	Grade = grade;
        	Scalars = scalars;
        }
        
        /// <summary>
        /// Create a term
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="scalar"></param>
        public Ega3DkVector(int grade, int index, double scalar)
        {
        	Grade = grade;
        	Scalars = new double[Ega3DUtils.KVectorSizesLookupTable[grade]];
        	Scalars[index] = scalar;
        }
        
        /// <summary>
        /// Create a term
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scalar"></param>
        public Ega3DkVector(int id, double scalar)
        {
        	Grade = Ega3DUtils.GradeLookupTable[id];
        	Scalars = new double[Ega3DUtils.KVectorSizesLookupTable[Grade]];
        
        	var index = Ega3DUtils.IndexLookupTable[id];
        	Scalars[index] = scalar;
        }
        
        /// <summary>
        /// Create a scalar blade (a 0-blade)
        /// </summary>
        public Ega3DkVector(double scalar)
        {
        	Grade = 0;
        	Scalars = new [] { scalar };
        }
        
        /// <summary>
        /// Create a zero k-vector
        /// </summary>
        internal Ega3DkVector()
        {
        	Grade = 0;
        	Scalars = new double[0];
        }
        
        
        public IEnumerable<Tuple<int, double>> GetStoredTermsById()
        {
        	var idTable = Ega3DUtils.IdLookupTable[Grade];
        	for (var index = 0; index <= StoredTermsCount; index++)
        	{
        		var scalar = Scalars[index];
        		var id = idTable[index];
        
        		yield return new Tuple<int, double>(id, scalar);
        	}
        }
        
        public IEnumerable<Tuple<int, int, double>> GetStoredTermsByGradeIndex()
        {
        	for (var index = 0; index <= StoredTermsCount; index++)
        	{
        		var scalar = Scalars[index];
        
        		yield return new Tuple<int, int, double>(Grade, index, scalar);
        	}
        }
        
        public IEnumerable<Tuple<int, double>> GetNonZeroTermsById()
        {
        	var idTable = Ega3DUtils.IdLookupTable[Grade];
        	for (var index = 0; index <= StoredTermsCount; index++)
        	{
        		var scalar = Scalars[index];
        		if (scalar.IsNearZero())
        			continue;
        
        		var id = idTable[index];
        		yield return new Tuple<int, double>(id, scalar);
        	}
        }
        
        public IEnumerable<Tuple<int, int, double>> GetNonZeroTermsByGradeIndex()
        {
        	for (var index = 0; index <= StoredTermsCount; index++)
        	{
        		var scalar = Scalars[index];
        		if (scalar.IsNearZero())
        			continue;
        
        		yield return new Tuple<int, int, double>(Grade, index, scalar);
        	}
        }
        
        public Ega3DkVector GetKVector(int grade)
        {
        	if (grade == Grade)
        		return this;
        
        	return new Ega3DkVector(grade);
        }
        
        /// <summary>
        /// Test if this k-vector is of a given grade. A zero k-vector is assumed to have any grade
        /// </summary>
        public bool IsOfGrade(int grade)
        {
        	return Grade == grade || (grade >= 0 && grade <= Ega3DUtils.VectorSpaceDimensions && IsZero);
        }
        
        /// <summary>
        /// If this blade is of grade 1 convert it to a vector
        /// </summary>
        /// <returns></returns>
        public Ega3DVector ToVector()
        {
        	if (Grade == 1)
        		return new Ega3DVector(Scalars);
        
        	if (IsZero)
        		return new Ega3DVector();
        
        	throw new InvalidDataException("Internal error. Grade not acceptable!");
        }
        
        
        public Ega3DkVector Meet(Ega3DkVector bladeB)
        {
        	//blade A1 is the part of A not in B
        	var bladeA1 = DPDual(bladeB).DP(this);
        
        	return bladeA1.ELCP(bladeB);
        }
        
        public Ega3DkVector Join(Ega3DkVector bladeB)
        {
        	//blade A1 is the part of A not in B
        	var bladeA1 = DPDual(bladeB).DP(this);
        
        	return bladeA1.OP(bladeB);
        }
        
        public void MeetJoin(Ega3DkVector bladeB, out Ega3DkVector bladeMeet, out Ega3DkVector bladeJoin)
        {
        	//blade A1 is the part of A not in B
        	var bladeA1 = DPDual(bladeB).DP(this);
        
        	bladeMeet = bladeA1.ELCP(bladeB);
        	bladeJoin = bladeA1.OP(bladeB);
        }
        
        public void Meet(Ega3DkVector bladeB, out Ega3DkVector bladeA1, out Ega3DkVector bladeB1, out Ega3DkVector bladeMeet)
        {
        	//blade A1 is the part of A not in B
        	bladeA1 = DPDual(bladeB).DP(this);
        
        	bladeMeet = bladeA1.ELCP(bladeB);
        	bladeB1 = bladeMeet.ELCP(bladeB);
        }
        
        public void MeetJoin(Ega3DkVector bladeB, out Ega3DkVector bladeA1, out Ega3DkVector bladeB1, out Ega3DkVector bladeMeet, out Ega3DkVector bladeJoin)
        {
        	//blade A1 is the part of A not in B
        	bladeA1 = DPDual(bladeB).DP(this);
        
        	bladeMeet = bladeA1.ELCP(bladeB);
        	bladeJoin = bladeA1.OP(bladeB);
        	bladeB1 = bladeMeet.ELCP(bladeB);
        }
        
        
        public override bool Equals(object obj)
        {
        	return !ReferenceEquals(obj, null) && Equals(obj as Ega3DkVector);
        }
        
        public override int GetHashCode()
        {
        	return Grade.GetHashCode() ^ Scalars.GetHashCode();
        }
        
        public override string ToString()
        {
        	if (IsZero)
        		return default(double).ToString(CultureInfo.InvariantCulture);
        
        	if (IsScalar)
        		return Scalars[0].ToString(CultureInfo.InvariantCulture);
        
        	var s = new StringBuilder();
        
        	for (var i = 0; i < StoredTermsCount; i++)
        	{
        		s.Append("(")
        			.Append(Scalars[i].ToString(CultureInfo.InvariantCulture))
        			.Append(" ")
        			.Append(BasisBladesNames[i])
        			.Append(") + ");
        	}
        
        	s.Length -= 3;
        
        	return s.ToString();
        }
    }
}
