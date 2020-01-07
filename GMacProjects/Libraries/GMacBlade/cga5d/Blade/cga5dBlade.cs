using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        /// <summary>
        /// Grade of blade.
        /// </summary>
        public int Grade { get; private set; }
        
        /// <summary>
        /// The k-vector space dimension of this blade (equals the length of the Coef array)
        /// </summary>
        public int KvSpaceDim { get { return GradeToKvSpaceDim[Grade]; } }
        
        /// <summary>
        /// Ordered coefficients of blade in the additive representation. 
        /// </summary>
        internal double[] Coefs { get; set; }
        
        
        /// <summary>
        /// This blade is a zero blade: it has no internal coefficients and its grade is any legal grade
        /// This kind of blade should be treated separately in operations on blades
        /// </summary>
        public bool IsZeroBlade { get { return Coefs.Length == 0; } }
        
        /// <summary>
        /// True if this blade is a null blade
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (IsZeroBlade)
                    return true;
        
                var c = Norm2;
                return !(c <= -Epsilon || c >= Epsilon);
            }
        }
        
        public bool IsScalar { get { return IsZeroBlade || Grade == 0; } }
        
        public bool IsVector { get { return IsZeroBlade || Grade == 1; } }
        
        public bool IsPseudoVector { get { return IsZeroBlade || Grade == MaxGrade - 1; } }
        
        public bool IsPseudoScalar { get { return IsZeroBlade || Grade == MaxGrade; } }
        
        public double this[int index] { get { return Coefs[index]; } }
        
        public string[] BasisBladesNames { get { return BasisBladesNamesArray[Grade]; } }
        
        /// <summary>
        /// True if the coefficients represent a blade; not a general non-simple k-vector.
        /// </summary>
        public bool IsBlade { get { return SelfDPGrade() == 0; } }
        
        /// <summary>
        /// True if the coefficients represent a general non-simple k-vector; not a blade.
        /// </summary>
        public bool IsNonBlade { get { return SelfDPGrade() != 0; } }
        
        /// <summary>
        /// Create a blade and initialize its coefficients by the given array. Use this vary
        /// carefully as the blade type is supposed to be immutable
        /// </summary>
        internal cga5dBlade(int grade, double[] coefs)
        {
            if (coefs.Length != GradeToKvSpaceDim[grade])
                throw new ArgumentException("The given array has the wrong number of items for this grade", "coefs");
        
            Grade = grade;
            Coefs = coefs;
        }
        
        /// <summary>
        /// Create a scalar blade (a grade-0 blade)
        /// </summary>
        public cga5dBlade(double scalar)
        {
            Grade = 0;
            Coefs = new [] { scalar };
        }
        
        /// <summary>
        /// Create the zero blade
        /// </summary>
        private cga5dBlade()
        {
            Grade = 0;
            Coefs = new double[0];
        }
        
        
        /// <summary>
        /// Test if this blade is of a given grade. A zero blade is assumed to have any grade
        /// </summary>
        public bool IsOfGrade(int grade)
        {
            return Grade == grade || (grade >= 0 && grade <= MaxGrade && IsZero);
        }
        
        /// <summary>
        /// If this blade is of grade 1 convert it to a vector
        /// </summary>
        /// <returns></returns>
        public cga5dVector ToVector()
        {
            if (Grade == 1)
                return new cga5dVector(Coefs);
        
            if (IsZero)
                return new cga5dVector();
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
        
        public static cga5dBlade Meet(cga5dBlade bladeA, cga5dBlade bladeB)
        {
            //blade A1 is the part of A not in B
            var bladeA1 = bladeA.DPDual(bladeB).DP(bladeA);
        
            return bladeA1.ELCP(bladeB);
        }
        
        public static cga5dBlade Join(cga5dBlade bladeA, cga5dBlade bladeB)
        {
            //blade A1 is the part of A not in B
            var bladeA1 = bladeA.DPDual(bladeB).DP(bladeA);
        
            return bladeA1.OP(bladeB);
        }
        
        public static void MeetJoin(cga5dBlade bladeA, cga5dBlade bladeB, out cga5dBlade bladeMeet, out cga5dBlade bladeJoin)
        {
            //blade A1 is the part of A not in B
            var bladeA1 = bladeA.DPDual(bladeB).DP(bladeA);
        
            bladeMeet = bladeA1.ELCP(bladeB);
            bladeJoin = bladeA1.OP(bladeB);
        }
        
        public static void MeetJoin(cga5dBlade bladeA, cga5dBlade bladeB, out cga5dBlade bladeA1, out cga5dBlade bladeB1, out cga5dBlade bladeMeet)
        {
            //blade A1 is the part of A not in B
            bladeA1 = bladeA.DPDual(bladeB).DP(bladeA);
        
            bladeMeet = bladeA1.ELCP(bladeB);
        
            bladeB1 = bladeMeet.ELCP(bladeB);
        }
        
        public static void MeetJoin(cga5dBlade bladeA, cga5dBlade bladeB, out cga5dBlade bladeA1, out cga5dBlade bladeB1, out cga5dBlade bladeMeet, out cga5dBlade bladeJoin)
        {
            //blade A1 is the part of A not in B
            bladeA1 = bladeA.DPDual(bladeB).DP(bladeA);
        
            bladeMeet = bladeA1.ELCP(bladeB);
            bladeJoin = bladeA1.OP(bladeB);
        
            bladeB1 = bladeMeet.ELCP(bladeB);
        }
        
        
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null) && Equals(obj as cga5dBlade);
        }
        
        public override int GetHashCode()
        {
            return Grade.GetHashCode() ^ Coefs.GetHashCode();
        }
        
        public override string ToString()
        {
            if (IsZeroBlade)
                return default(double).ToString(CultureInfo.InvariantCulture);
        
            if (IsScalar)
                return Coefs[0].ToString(CultureInfo.InvariantCulture);
        
            var s = new StringBuilder();
        
            for (var i = 0; i < KvSpaceDim; i++)
            {
                s.Append("(")
                    .Append(Coefs[i].ToString(CultureInfo.InvariantCulture))
                    .Append(" ")
                    .Append(BasisBladesNames[i])
                    .Append(") + ");
            }
        
            s.Length -= 3;
        
            return s.ToString();
        }
    }
}
