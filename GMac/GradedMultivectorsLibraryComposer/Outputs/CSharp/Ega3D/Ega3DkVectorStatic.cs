namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        /// <summary>
        /// An array of arrays containing basis blades names for this frame grouped by grade
        /// </summary>
        private static string[][] BasisBladesNamesArray { get; } 
            = 
            {
                new [] { "E0" },
                new [] { "E1", "E2", "E4" },
                new [] { "E3", "E5", "E6" },
                new [] { "E7" }
            };
        
        public static Ega3DkVector E0 { get; } = new Ega3DkVector(0, new[] { 1.0D });
        
        public static Ega3DkVector E1 { get; } = new Ega3DkVector(1, new[] { 1.0D, 0.0D, 0.0D });
        public static Ega3DkVector E2 { get; } = new Ega3DkVector(1, new[] { 0.0D, 1.0D, 0.0D });
        public static Ega3DkVector E4 { get; } = new Ega3DkVector(1, new[] { 0.0D, 0.0D, 1.0D });
        
        public static Ega3DkVector E3 { get; } = new Ega3DkVector(2, new[] { 1.0D, 0.0D, 0.0D });
        public static Ega3DkVector E5 { get; } = new Ega3DkVector(2, new[] { 0.0D, 1.0D, 0.0D });
        public static Ega3DkVector E6 { get; } = new Ega3DkVector(2, new[] { 0.0D, 0.0D, 1.0D });
        
        public static Ega3DkVector E7 { get; } = new Ega3DkVector(3, new[] { 1.0D });
        
        
    }
}
