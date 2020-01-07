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
        /// The maximum grade for any blade of this class
        /// </summary>
        public const int MaxGrade = 5;
        
        /// <summary>
        /// The accuracy factor for computations. Any value below this is considered zero
        /// </summary>
        public static double Epsilon = 1e-12;
        
        /// <summary>
        /// A lookup table for finding the k-vctor space dimension of a blade using its grade 
        /// as index to this array
        /// </summary>
        internal static readonly int[] GradeToKvSpaceDim = { 1, 5, 10, 10, 5, 1 };
        
        /// <summary>
        /// An array of arrays containing basis blades names for this frame grouped by grade
        /// </summary>
        private static readonly string[][] BasisBladesNamesArray =
            {
                new [] { "E0" },
                new [] { "E1", "E2", "E4", "E8", "E16" },
                new [] { "E3", "E5", "E6", "E9", "E10", "E12", "E17", "E18", "E20", "E24" },
                new [] { "E7", "E11", "E13", "E14", "E19", "E21", "E22", "E25", "E26", "E28" },
                new [] { "E15", "E23", "E27", "E29", "E30" },
                new [] { "E31" }
            };
        
        /// <summary>
        /// The zero blade
        /// </summary>
        public static readonly cga5dBlade ZeroBlade = new cga5dBlade();
        
        public static readonly cga5dBlade E0 = new cga5dBlade(0, new[] { 1.0D });
        
        public static readonly cga5dBlade E1 = new cga5dBlade(1, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E2 = new cga5dBlade(1, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E4 = new cga5dBlade(1, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E8 = new cga5dBlade(1, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga5dBlade E16 = new cga5dBlade(1, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga5dBlade E3 = new cga5dBlade(2, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E5 = new cga5dBlade(2, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E6 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E9 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E10 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E12 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E17 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E18 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E20 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga5dBlade E24 = new cga5dBlade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga5dBlade E7 = new cga5dBlade(3, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E11 = new cga5dBlade(3, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E13 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E14 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E19 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E21 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E22 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E25 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E26 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga5dBlade E28 = new cga5dBlade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga5dBlade E15 = new cga5dBlade(4, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E23 = new cga5dBlade(4, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E27 = new cga5dBlade(4, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga5dBlade E29 = new cga5dBlade(4, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga5dBlade E30 = new cga5dBlade(4, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga5dBlade E31 = new cga5dBlade(5, new[] { 1.0D });
        
        
        /// <summary>
        /// Create a new coefficients array to be used for later creation of a blade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static double[] CreateCoefsArray(int grade)
        {
            return new double[GradeToKvSpaceDim[grade]];
        }
    }
}
