namespace GMacBlade.cga5d
{
    /// <summary>
    /// Represents a factored blade in Euclidean space
    /// </summary>
    public sealed class cga5dFactoredBlade
    {
        public double Norm { get; private set; }

        public cga5dVector[] Vectors { get; private set; }


        public int Grade { get { return Vectors.Length; } }


        internal cga5dFactoredBlade(double norm)
        {
            Norm = norm;
            Vectors = new cga5dVector[0];
        }

        internal cga5dFactoredBlade(double norm, cga5dVector vector)
        {
            Norm = norm;
            Vectors = new [] { vector };
        }

        internal cga5dFactoredBlade(double norm, cga5dVector[] vectors)
        {
            Norm = norm;
            Vectors = vectors;
        }

        /// <summary>
        /// Convert each vector to a normal vector (assuming Euclidean space) and factor 
        /// the squared norms to the NormSquared member
        /// </summary>
        /// <returns></returns>
        public cga5dFactoredBlade Normalize()
        {
            for (var idx = 0; idx < Vectors.Length; idx++)
                Norm *= Vectors[idx].Normalize();

            return this;
        }

        public cga5dBlade ToBlade()
        {
            return cga5dBlade.OP(Vectors).Times(Norm);
        }
    }
}
