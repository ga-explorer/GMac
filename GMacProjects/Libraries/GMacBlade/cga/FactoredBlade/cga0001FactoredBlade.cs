namespace GMacBlade.cga0001
{
    /// <summary>
    /// Represents a factored blade in Euclidean space
    /// </summary>
    public sealed class cga0001FactoredBlade
    {
        public double Norm { get; private set; }

        public cga0001Vector[] Vectors { get; private set; }


        public int Grade { get { return Vectors.Length; } }


        internal cga0001FactoredBlade(double norm)
        {
            Norm = norm;
            Vectors = new cga0001Vector[0];
        }

        internal cga0001FactoredBlade(double norm, cga0001Vector vector)
        {
            Norm = norm;
            Vectors = new [] { vector };
        }

        internal cga0001FactoredBlade(double norm, cga0001Vector[] vectors)
        {
            Norm = norm;
            Vectors = vectors;
        }

        /// <summary>
        /// Convert each vector to a normal vector (assuming Euclidean space) and factor 
        /// the squared norms to the NormSquared member
        /// </summary>
        /// <returns></returns>
        public cga0001FactoredBlade Normalize()
        {
            for (var idx = 0; idx < Vectors.Length; idx++)
                Norm *= Vectors[idx].Normalize();

            return this;
        }

        public cga0001Blade ToBlade()
        {
            return cga0001Blade.OP(Vectors).Times(Norm);
        }
    }
}
