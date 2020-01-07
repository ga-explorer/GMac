namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
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
        public static readonly cga0001Blade ZeroBlade = new cga0001Blade();
        
        public static readonly cga0001Blade E0 = new cga0001Blade(0, new[] { 1.0D });
        
        public static readonly cga0001Blade E1 = new cga0001Blade(1, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E2 = new cga0001Blade(1, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E4 = new cga0001Blade(1, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E8 = new cga0001Blade(1, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga0001Blade E16 = new cga0001Blade(1, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga0001Blade E3 = new cga0001Blade(2, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E5 = new cga0001Blade(2, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E6 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E9 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E10 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E12 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E17 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E18 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E20 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga0001Blade E24 = new cga0001Blade(2, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga0001Blade E7 = new cga0001Blade(3, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E11 = new cga0001Blade(3, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E13 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E14 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E19 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E21 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E22 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E25 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E26 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga0001Blade E28 = new cga0001Blade(3, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga0001Blade E15 = new cga0001Blade(4, new[] { 1.0D, 0.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E23 = new cga0001Blade(4, new[] { 0.0D, 1.0D, 0.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E27 = new cga0001Blade(4, new[] { 0.0D, 0.0D, 1.0D, 0.0D, 0.0D });
        public static readonly cga0001Blade E29 = new cga0001Blade(4, new[] { 0.0D, 0.0D, 0.0D, 1.0D, 0.0D });
        public static readonly cga0001Blade E30 = new cga0001Blade(4, new[] { 0.0D, 0.0D, 0.0D, 0.0D, 1.0D });
        
        public static readonly cga0001Blade E31 = new cga0001Blade(5, new[] { 1.0D });
        
        
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
