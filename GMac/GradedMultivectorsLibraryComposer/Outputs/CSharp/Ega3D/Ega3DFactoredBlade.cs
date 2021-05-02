namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// Represents a factored blade in Euclidean space
    /// </summary>
    public sealed class Ega3DFactoredBlade
    {
        public double Norm { get; private set; }

        public Ega3DVector[] Vectors { get; private set; }


        public int Grade { get { return Vectors.Length; } }


        internal Ega3DFactoredBlade(double norm)
        {
            Norm = norm;
            Vectors = new Ega3DVector[0];
        }

        internal Ega3DFactoredBlade(double norm, Ega3DVector vector)
        {
            Norm = norm;
            Vectors = new [] { vector };
        }

        internal Ega3DFactoredBlade(double norm, Ega3DVector[] vectors)
        {
            Norm = norm;
            Vectors = vectors;
        }

        /// <summary>
        /// Convert each vector to a normal vector (assuming Euclidean space) and factor 
        /// the squared norms to the NormSquared member
        /// </summary>
        /// <returns></returns>
        public Ega3DFactoredBlade Normalize()
        {
            for (var idx = 0; idx < Vectors.Length; idx++)
                Norm *= Vectors[idx].Normalize();

            return this;
        }

        public Ega3DkVector ToBlade()
        {
            return Ega3DkVector.OP(Vectors).Times(Norm);
        }
    }
}
