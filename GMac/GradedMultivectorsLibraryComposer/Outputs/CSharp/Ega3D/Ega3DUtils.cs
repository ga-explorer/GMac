namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    public static partial class Ega3DUtils
    {
        private static int[][] ChooseList { get; } 
            =
            {
                new[] {1},
                new[] {1, 1},
                new[] {1, 2, 1},
                new[] {1, 3, 3, 1},
                new[] {1, 4, 6, 4, 1},
                new[] {1, 5, 10, 10, 5, 1},
                new[] {1, 6, 15, 20, 15, 6, 1},
                new[] {1, 7, 21, 35, 35, 21, 7, 1},
                new[] {1, 8, 28, 56, 70, 56, 28, 8, 1},
                new[] {1, 9, 36, 84, 126, 126, 84, 36, 9, 1},
                new[] {1, 10, 45, 120, 210, 252, 210, 120, 45, 10, 1},
                new[] {1, 11, 55, 165, 330, 462, 462, 330, 165, 55, 11, 1},
                new[] {1, 12, 66, 220, 495, 792, 924, 792, 495, 220, 66, 12, 1},
                new[] {1, 13, 78, 286, 715, 1287, 1716, 1716, 1287, 715, 286, 78, 13, 1},
                new[] {1, 14, 91, 364, 1001, 2002, 3003, 3432, 3003, 2002, 1001, 364, 91, 14, 1},
                new[] {1, 15, 105, 455, 1365, 3003, 5005, 6435, 6435, 5005, 3003, 1365, 455, 105, 15, 1},
                new[] {1, 16, 120, 560, 1820, 4368, 8008, 11440, 12870, 11440, 8008, 4368, 1820, 560, 120, 16, 1},
                new[] {1, 17, 136, 680, 2380, 6188, 12376, 19448, 24310, 24310, 19448, 12376, 6188, 2380, 680, 136, 17, 1},
                new[] {1, 18, 153, 816, 3060, 8568, 18564, 31824, 43758, 48620, 43758, 31824, 18564, 8568, 3060, 816, 153, 18, 1},
                new[] {1, 19, 171, 969, 3876, 11628, 27132, 50388, 75582, 92378, 92378, 75582, 50388, 27132, 11628, 3876, 969, 171, 19, 1},
                new[] {1, 20, 190, 1140, 4845, 15504, 38760, 77520, 125970, 167960, 184756, 167960, 125970, 77520, 38760, 15504, 4845, 1140, 190, 20, 1},
                new[] {1, 21, 210, 1330, 5985, 20349, 54264, 116280, 203490, 293930, 352716, 352716, 293930, 203490, 116280, 54264, 20349, 5985, 1330, 210, 21, 1},
                new[] {1, 22, 231, 1540, 7315, 26334, 74613, 170544, 319770, 497420, 646646, 705432, 646646, 497420, 319770, 170544, 74613, 26334, 7315, 1540, 231, 22, 1},
                new[] {1, 23, 253, 1771, 8855, 33649, 100947, 245157, 490314, 817190, 1144066, 1352078, 1352078, 1144066, 817190, 490314, 245157, 100947, 33649, 8855, 1771, 253, 23, 1},
                new[] {1, 24, 276, 2024, 10626, 42504, 134596, 346104, 735471, 1307504, 1961256, 2496144, 2704156, 2496144, 1961256, 1307504, 735471, 346104, 134596, 42504, 10626, 2024, 276, 24, 1},
                new[] {1, 25, 300, 2300, 12650, 53130, 177100, 480700, 1081575, 2042975, 3268760, 4457400, 5200300, 5200300, 4457400, 3268760, 2042975, 1081575, 480700, 177100, 53130, 12650, 2300, 300, 25, 1}
            };

        public static double ScalarEpsilon { get; set; } = 1e-12;

        public static int VectorSpaceDimensions { get; }

        public static int GaSpaceDimensions { get; } 

        public static int MaxGradesPerMultivector { get; }

        internal static int[][] IdLookupTable { get; }

        internal static int[] GradeLookupTable { get; }
        
        internal static int[] IndexLookupTable { get; }
        
        internal static int[] KVectorSizesLookupTable { get; }


        static Ega3DUtils()
        {
            VectorSpaceDimensions = 3;
            MaxGradesPerMultivector = 1 + VectorSpaceDimensions;
            GaSpaceDimensions = 1 << VectorSpaceDimensions;

            IdLookupTable = new int[MaxGradesPerMultivector][];
            GradeLookupTable = new int[GaSpaceDimensions];
            IndexLookupTable = new int[GaSpaceDimensions];
            KVectorSizesLookupTable = new int[MaxGradesPerMultivector];

            var gradeCount = new int[MaxGradesPerMultivector];
            for (var id = 0; id < GaSpaceDimensions; id++)
            {
                var grade = id.CountOnes();
                var index = gradeCount[grade];

                GradeLookupTable[id] = grade;
                IndexLookupTable[id] = index;

                gradeCount[grade] = index + 1;
            }

            gradeCount = new int[MaxGradesPerMultivector];
            for (var id = 0; id < GaSpaceDimensions; id++)
            {
                var grade = GradeLookupTable[id];
                var index = gradeCount[grade];

                if (index == 0)
                {
                    var kVectorSize = Choose(VectorSpaceDimensions, grade);
                    
                    IdLookupTable[grade] = new int[kVectorSize];
                    KVectorSizesLookupTable[grade] = kVectorSize;
                }

                IdLookupTable[grade][index] = id;

                gradeCount[grade] = index + 1;
            }
        }


        public static int Choose(this int n, int r)
        {
            return ChooseList[n][r];
        }

        /// <summary>
        /// Count the number of ones in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int CountOnes(this int bitPattern)
        {
            var onesCount = 0;

            while (bitPattern > 0)
            {
                // clear the least significant bit set
                bitPattern &= bitPattern - 1;

                onesCount++;
            }

            return onesCount;
        }

        public static bool IsNearZero(this double scalar)
        {
            return scalar >= -ScalarEpsilon && scalar <= ScalarEpsilon;
        }
    }
}