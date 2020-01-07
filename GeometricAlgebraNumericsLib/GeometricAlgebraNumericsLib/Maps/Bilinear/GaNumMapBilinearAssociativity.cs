namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public enum GaNumMapBilinearAssociativity
    {
        /// <summary>
        /// The bilinear map is non-associative
        /// </summary>
        NoneAssociative = 0,

        /// <summary>
        /// The bilinear map is left-associative A B C D = ((A B) C) D
        /// </summary>
        LeftAssociative = 1,

        /// <summary>
        /// The bilinear map is right-associative A B C D = A (B (C D))
        /// </summary>
        RightAssociative = 2,

        /// <summary>
        /// The bilinear map is both left and right-associative
        /// A B C D = ((A B) C) D = ((A B) C) D
        /// </summary>
        LeftRightAssociative = 3
    }
}