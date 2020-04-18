using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructuresLib;

namespace GeometricAlgebraNumericsLib.Frames
{
    public static class GaNumFrameUtils
    {
        /// <summary>
        /// The maximum allowed GA vector space dimension
        /// </summary>
        public static int MaxVSpaceDimension { get; } 
            = 25;

        /// <summary>
        /// The maximum possible basis blade ID in the maximum allowed GA vector space dimension
        /// </summary>
        public static int MaxVSpaceBasisBladeId { get; } 
            = (1 << MaxVSpaceDimension) - 1;

        public static string[] DefaultBasisVectorsNames { get; } 
            = Enumerable.Range(1, MaxVSpaceDimension)
                .Select(i => "e" + i)
                .ToArray();

        /// <summary>
        /// The number of basis blades in a GA with dimension vSpaceDim
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static int ToGaSpaceDimension(this int vSpaceDim)
        {
            return 1 << vSpaceDim;
        }

        /// <summary>
        /// The number of basis vectors in a GA with dimension gaSpaceDim
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        public static int ToVSpaceDimension(this int gaSpaceDim)
        {
            return gaSpaceDim.LastOneBitPosition();
        }

        /// <summary>
        /// The max basis blade ID in a GA space with a given dimension
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static int MaxBasisBladeId(int vSpaceDim)
        {
            return (1 << vSpaceDim) - 1;
        }

        /// <summary>
        /// The number of grades in a GA space with a given dimension
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static int GradesCount(int vSpaceDim)
        {
            return vSpaceDim + 1;
        }

        /// <summary>
        /// The dimension of k-vectors subspace of some grade of a given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static int KvSpaceDimension(this IGaFrame frame, int grade)
        {
            return GaLookupTables.Choose(frame.VSpaceDimension, grade);
        }

        /// <summary>
        /// The dimension of k-vectors subspace of some grade of a GA with a given dimension
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static int KvSpaceDimension(int vSpaceDim, int grade)
        {
            return GaLookupTables.Choose(vSpaceDim, grade);
        }



        /// <summary>
        /// The grades of k-vectors in the given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static IEnumerable<int> Grades(this IGaFrame frame)
        {
            return Enumerable.Range(0, frame.VSpaceDimension + 1);
        }

        /// <summary>
        /// The grades of k-vectors in a GA with the given dimension
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static IEnumerable<int> Grades(int vSpaceDim)
        {
            return Enumerable.Range(0, vSpaceDim + 1);
        }


        /// <summary>
        /// The Basis blade IDs of the given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDs(this IGaFrame frame)
        {
            return Enumerable.Range(0, frame.GaSpaceDimension);
        }

        /// <summary>
        /// The Basis blade IDs of a GA space with the given dimension
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDs(int vSpaceDim)
        {
            return Enumerable.Range(0, ToGaSpaceDimension(vSpaceDim));
        }

        /// <summary>
        /// The basis vectors IDs of the given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisVectorIDs(this IGaFrame frame)
        {
            return Enumerable.Range(0, frame.VSpaceDimension).Select(i => (1 << i));
        }

        /// <summary>
        /// The basis vector IDs of a GA with the given dimension
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisVectorIDs(int vSpaceDim)
        {
            return Enumerable.Range(0, vSpaceDim).Select(i => (1 << i));
        }


        /// <summary>
        /// Find all basis blade IDs with the given grade in a given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrade(this IGaFrame frame, int grade)
        {
            var kvDim = frame.KvSpaceDimension(grade);

            return
                Enumerable
                .Range(0, kvDim)
                .Select(index => BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade in a GA of dimension vSpaceDim
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrade(int vSpaceDim, int grade)
        {
            var kvDim = KvSpaceDimension(vSpaceDim, grade);

            return
                Enumerable
                .Range(0, kvDim)
                .Select(index => BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes in a given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrade(this IGaFrame frame, int grade, IEnumerable<int> indexSeq)
        {
            return indexSeq.Select(index => BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGradeIndex(int grade, IEnumerable<int> indexSeq)
        {
            return indexSeq.Select(index => BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes in a given frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGradeIndex(this IGaFrame frame, int grade, params int[] indexSeq)
        {
            return indexSeq.Select(index => BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGradeIndex(int grade, params int[] indexSeq)
        {
            return indexSeq.Select(index => BasisBladeId(grade, index));
        }

        /// <summary>
        /// The basis blade IDs of the given frame sorted by their grade and index
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsSortedByGrade(this IGaFrame frame, int startGrade = 0)
        {
            for (var grade = startGrade; grade <= frame.VSpaceDimension; grade++)
            {
                var kvSpaceDim = frame.KvSpaceDimension(grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    yield return BasisBladeId(grade, index);
            }
        }

        /// <summary>
        /// The basis blade IDs of a GA space with the given dimension sorted by their grade and index
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsSortedByGrade(int vSpaceDim, int startGrade = 0)
        {
            for (var grade = startGrade; grade <= vSpaceDim; grade++)
            {
                var kvSpaceDim = KvSpaceDimension(vSpaceDim, grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    yield return BasisBladeId(grade, index);
            }
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrades(this IGaFrame frame, IEnumerable<int> gradesSeq)
        {
            foreach (var grade in gradesSeq.OrderBy(g => g))
            {
                var kvSpaceDim = frame.KvSpaceDimension(grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    yield return BasisBladeId(grade, index);
            }
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrades(int vSpaceDim, IEnumerable<int> gradesSeq)
        {
            foreach (var grade in gradesSeq.OrderBy(g => g))
            {
                var kvSpaceDim = KvSpaceDimension(vSpaceDim, grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    yield return BasisBladeId(grade, index);
            }
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrades(this IGaFrame frame, params int[] gradesSeq)
        {
            foreach (var grade in gradesSeq.OrderBy(g => g))
            {
                var kvSpaceDim = frame.KvSpaceDimension(grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    yield return BasisBladeId(grade, index);
            }
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static IEnumerable<int> BasisBladeIDsOfGrades(int vSpaceDim, params int[] gradesSeq)
        {
            foreach (var grade in gradesSeq.OrderBy(g => g))
            {
                var kvSpaceDim = KvSpaceDimension(vSpaceDim, grade);

                for (var index = 0; index < kvSpaceDim; index++)
                    yield return BasisBladeId(grade, index);
            }
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> BasisBladeIDsGroupedByGrade(this IGaFrame frame, int startGrade = 0)
        {
            var result = new Dictionary<int, List<int>>();

            for (var grade = startGrade; grade <= frame.VSpaceDimension; grade++)
            {
                var kvSpaceDim = frame.KvSpaceDimension(grade);

                var newList = new List<int>(kvSpaceDim);

                for (var index = 0; index < kvSpaceDim; index++)
                    newList.Add(BasisBladeId(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> BasisBladeIDsGroupedByGrade(int vSpaceDim, int startGrade = 0)
        {
            var result = new Dictionary<int, List<int>>();

            for (var grade = startGrade; grade <= vSpaceDim; grade++)
            {
                var kvSpaceDim = KvSpaceDimension(vSpaceDim, grade);

                var newList = new List<int>(kvSpaceDim);

                for (var index = 0; index < kvSpaceDim; index++)
                    newList.Add(BasisBladeId(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> BasisBladeIDsGroupedByGrade(this IGaFrame frame, IEnumerable<int> gradesSeq)
        {
            var result = new Dictionary<int, List<int>>();

            foreach (var grade in gradesSeq)
            {
                var kvSpaceDim = frame.KvSpaceDimension(grade);

                var newList = new List<int>(kvSpaceDim);

                for (var index = 0; index < kvSpaceDim; index++)
                    newList.Add(BasisBladeId(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> BasisBladeIDsGroupedByGrade(int vSpaceDim, IEnumerable<int> gradesSeq)
        {
            var result = new Dictionary<int, List<int>>();

            foreach (var grade in gradesSeq)
            {
                var kvSpaceDim = KvSpaceDimension(vSpaceDim, grade);

                var newList = new List<int>(kvSpaceDim);

                for (var index = 0; index < kvSpaceDim; index++)
                    newList.Add(BasisBladeId(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> BasisBladeIDsGroupedByGrade(this IGaFrame frame, params int[] gradesSeq)
        {
            var result = new Dictionary<int, List<int>>();

            foreach (var grade in gradesSeq)
            {
                var kvSpaceDim = frame.KvSpaceDimension(grade);

                var newList = new List<int>(kvSpaceDim);

                for (var index = 0; index < kvSpaceDim; index++)
                    newList.Add(BasisBladeId(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> BasisBladeIDsGroupedByGrade(int vSpaceDim, params int[] gradesSeq)
        {
            var result = new Dictionary<int, List<int>>();

            foreach (var grade in gradesSeq)
            {
                var kvSpaceDim = KvSpaceDimension(vSpaceDim, grade);

                var newList = new List<int>(kvSpaceDim);

                for (var index = 0; index < kvSpaceDim; index++)
                    newList.Add(BasisBladeId(grade, index));

                result.Add(grade, newList);
            }

            return result;
        }


        /// <summary>
        /// Find the ID of a basis blade given its grade and index
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int BasisBladeId(this IGaFrame frame, int grade, int index)
        {
            return GaLookupTables.GradeIndexToIdTable[grade][index];
        }

        /// <summary>
        /// Find the ID of a basis blade given its grade and index
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int BasisBladeId(int grade, int index)
        {
            return GaLookupTables.GradeIndexToIdTable[grade][index];
        }

        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int BasisVectorId(this IGaFrame frame, int index)
        {
            return GaLookupTables.GradeIndexToIdTable[1][index];
        }

        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int BasisVectorId(int index)
        {
            return GaLookupTables.GradeIndexToIdTable[1][index];
        }

        /// <summary>
        /// Find the grade of a basis blade given its ID
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public static int BasisBladeGrade(this IGaFrame frame, int basisBladeId)
        {
            return GaLookupTables.IdToGradeTable[basisBladeId];
        }

        /// <summary>
        /// Find the grade of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public static int BasisBladeGrade(this int basisBladeId)
        {
            return GaLookupTables.IdToGradeTable[basisBladeId];
        }

        /// <summary>
        /// Find the index of a basis blade given its ID
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public static int BasisBladeIndex(this IGaFrame frame, int basisBladeId)
        {
            return GaLookupTables.IdToIndexTable[basisBladeId];
        }

        /// <summary>
        /// Find the index of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public static int BasisBladeIndex(this int basisBladeId)
        {
            return GaLookupTables.IdToIndexTable[basisBladeId];
        }

        /// <summary>
        /// Find the grade and index of a basis blade given its ID
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        public static void BasisBladeGradeIndex(this IGaFrame frame, int basisBladeId, out int grade, out int index)
        {
            basisBladeId.BasisBladeGradeIndex(out grade, out index);
        }

        /// <summary>
        /// Find the grade and index of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        public static void BasisBladeGradeIndex(this int basisBladeId, out int grade, out int index)
        {
            grade = GaLookupTables.IdToGradeTable[basisBladeId];
            index = GaLookupTables.IdToIndexTable[basisBladeId];
        }

        public static string BasisBladeName(this int basisBladeId)
        {
            return DefaultBasisVectorsNames.ConcatenateUsingPattern(basisBladeId, "E0", "^");
        }

        public static string BasisBladeName(int grade, int index)
        {
            return DefaultBasisVectorsNames.ConcatenateUsingPattern(BasisBladeId(grade, index), "E0", "^");
        }

        public static string BasisBladeName(this int basisBladeId, params string[] basisVactorNames)
        {
            return basisVactorNames.ConcatenateUsingPattern(basisBladeId, "E0", "^");
        }

        public static string BasisBladeName(int grade, int index, params string[] basisVactorNames)
        {
            return basisVactorNames.ConcatenateUsingPattern(BasisBladeId(grade, index), "E0", "^");
        }


        public static string BasisBladeIndexedName(this IGaFrame frame, int basisBladeId)
        {
            return "E" + basisBladeId;
        }

        public static string BasisBladeIndexedName(this int basisBladeId)
        {
            return "E" + basisBladeId;
        }

        public static string BasisBladeIndexedName(this IGaFrame frame, int grade, int index)
        {
            return "E" + BasisBladeId(grade, index);
        }

        public static string BasisBladeIndexedName(int grade, int index)
        {
            return "E" + BasisBladeId(grade, index);
        }


        public static string BasisBladeBinaryIndexedName(this IGaFrame frame, int basisBladeId)
        {
            return "B" + basisBladeId.PatternToString(frame.VSpaceDimension);
        }

        public static string BasisBladeBinaryIndexedName(int vSpaceDim, int basisBladeId)
        {
            return "B" + basisBladeId.PatternToString(vSpaceDim);
        }

        public static string BasisBladeBinaryIndexedName(this IGaFrame frame, int grade, int index)
        {
            return "B" + BasisBladeId(grade, index).PatternToString(frame.VSpaceDimension);
        }

        public static string BasisBladeBinaryIndexedName(int vSpaceDim, int grade, int index)
        {
            return "B" + BasisBladeId(grade, index).PatternToString(vSpaceDim);
        }


        public static string BasisBladeGradeIndexName(this IGaFrame frame, int basisBladeId)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(basisBladeId.BasisBladeGrade())
                .Append('I')
                .Append(basisBladeId.BasisBladeIndex())
                .ToString();
        }

        public static string BasisBladeGradeIndexName(this int basisBladeId)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(basisBladeId.BasisBladeGrade())
                .Append('I')
                .Append(basisBladeId.BasisBladeIndex())
                .ToString();
        }

        public static string BasisBladeGradeIndexName(this IGaFrame frame, int grade, int index)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(grade)
                .Append('I')
                .Append(index)
                .ToString();
        }

        public static string BasisBladeGradeIndexName(int grade, int index)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(grade)
                .Append('I')
                .Append(index)
                .ToString();
        }


        public static IEnumerable<int> BasisVectorIDsInside(this IGaFrame frame, int basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }

        public static IEnumerable<int> BasisVectorIDsInside(this int basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }

        public static IEnumerable<int> BasisVectorIDsInside(this IGaFrame frame, int grade, int index)
        {
            return BasisBladeId(grade, index).GetBasicPatterns();
        }

        public static IEnumerable<int> BasisVectorIDsInside(int grade, int index)
        {
            return BasisBladeId(grade, index).GetBasicPatterns();
        }


        public static IEnumerable<int> BasisVectorIndexesInside(this IGaFrame frame, int basisBladeId)
        {
            return basisBladeId.PatternToPositions();
        }

        public static IEnumerable<int> BasisVectorIndexesInside(this int basisBladeId)
        {
            return basisBladeId.PatternToPositions();
        }

        public static IEnumerable<int> BasisVectorIndexesInside(this IGaFrame frame, int grade, int index)
        {
            return BasisBladeId(grade, index).PatternToPositions();
        }

        public static IEnumerable<int> BasisVectorIndexesInside(int grade, int index)
        {
            return BasisBladeId(grade, index).PatternToPositions();
        }


        public static IEnumerable<int> BasisBladeIDsInside(this IGaFrame frame, int basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }

        public static IEnumerable<int> BasisBladeIDsInside(this int basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }

        public static IEnumerable<int> BasisBladeIDsInside(this IGaFrame frame, int grade, int index)
        {
            return BasisBladeId(grade, index).GetSubPatterns();
        }

        public static IEnumerable<int> BasisBladeIDsInside(int grade, int index)
        {
            return BasisBladeId(grade, index).GetSubPatterns();
        }


        public static IEnumerable<int> BasisBladeIDsContaining(this IGaFrame frame, int basisBladeId)
        {
            return basisBladeId.GetSuperPatterns(frame.VSpaceDimension);
        }

        public static IEnumerable<int> BasisBladeIDsContaining(int vSpaceDim, int basisBladeId)
        {
            return basisBladeId.GetSuperPatterns(vSpaceDim);
        }

        public static IEnumerable<int> BasisBladeIDsContaining(this IGaFrame frame, int grade, int index)
        {
            return BasisBladeId(grade, index).GetSuperPatterns(frame.VSpaceDimension);
        }

        public static IEnumerable<int> BasisBladeIDsContaining(int vSpaceDim, int grade, int index)
        {
            return BasisBladeId(grade, index).GetSuperPatterns(vSpaceDim);
        }


        public static IEnumerable<int> SortBasisBladeIDsByGrade(this IEnumerable<int> idsSeq)
        {
            return idsSeq.OrderBy(BasisBladeGrade).ThenBy(BasisBladeIndex);
        }

        public static IEnumerable<IGrouping<int, int>> GroupBasisBladeIDsByGrade(this IEnumerable<int> idsSeq)
        {
            return idsSeq.GroupBy(BasisBladeGrade);
        }


        /// <summary>
        /// Returns true if the given id contains subId as a binary pattern 
        /// (i.e. whenever subId has a 1 we find id has 1 at the same bit position)
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public static bool BasisBladeIdContains(this int basisBladeId, int subId)
        {
            return (basisBladeId | subId) == basisBladeId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="basisVectorId"></param>
        /// <param name="subBasisBladeId"></param>
        public static void SplitBySmallestBasisVectorId(this int basisBladeId, out int basisVectorId, out int subBasisBladeId)
        {
            basisBladeId.SplitBySmallestBasicPattern(out basisVectorId, out subBasisBladeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="basisVectorId"></param>
        /// <param name="subBasisBladeId"></param>
        public static void SplitByLargestBasisVectorId(this int basisBladeId, out int basisVectorId, out int subBasisBladeId)
        {
            basisBladeId.SplitByLargestBasicPattern(out basisVectorId, out subBasisBladeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basisVectorsIds"></param>
        /// <param name="idIndex"></param>
        /// <returns></returns>
        public static int ComposeGaSubspaceBasisBladeId(List<int> basisVectorsIds, int idIndex)
        {
            var posList = idIndex.PatternToPositions();

            return posList.Aggregate(0, (acc, pos) => acc | basisVectorsIds[pos]);

            //int id = 0;

            //foreach (var pos in pos_list)
            //    id = id | basis_vectors_ids[pos];

            //return id;
        }


        public static bool IsValidVSpaceDimension(this int vaSpaceDim)
        {
            return vaSpaceDim >= 2 && vaSpaceDim < MaxVSpaceDimension;
        }

        /// <summary>
        /// Test if the given integer is a legal GA space dimension (i.e. positive power of 2 less than or 
        /// equal to 2 ^ MaxVSpaceDim)
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        public static bool IsValidGaSpaceDimension(this int gaSpaceDim)
        {
            return
                gaSpaceDim == (MaxVSpaceBasisBladeId & gaSpaceDim) &&
                gaSpaceDim.CountOnes() == 1;
        }

        public static bool IsValidBasisVectorId(this IGaFrame frame, int basisBladeId)
        {
            return frame.IsValidBasisBladeId(basisBladeId) && basisBladeId.IsBasicPattern();
        }

        public static bool IsValidBasisVectorId(this int basisBladeId)
        {
            return basisBladeId.IsValidBasisBladeId() && basisBladeId.IsBasicPattern();
        }

        public static bool TryGetValidBasisVectorIndex(this int basisBladeId, out int basisVectorIndex)
        {
            var onesCount = 0;
            basisVectorIndex = -1;

            var i = 0;
            while (basisBladeId != 0 && onesCount <= 1)
            {
                if ((basisBladeId & 1) != 0)
                {
                    basisVectorIndex = (onesCount == 0) ? i : -1;
                    onesCount++;
                }

                i++;
                basisBladeId >>= 1;
            }

            return onesCount == 1;
        }

        public static bool IsValidBasisVectorIndex(this IGaFrame frame, int index)
        {
            return index >= 0 && index < frame.VSpaceDimension;
        }

        public static bool IsValidBasisVectorIndex(int index)
        {
            return index >= 0 && index < MaxVSpaceDimension;
        }

        public static bool IsValidBasisBladeId(this IGaFrame frame, int basisBladeId)
        {
            return (basisBladeId >= 0 && basisBladeId <= frame.MaxBasisBladeId);
        }

        public static bool IsValidBasisBladeId(this int basisBladeId)
        {
            return (basisBladeId >= 0 && basisBladeId <= MaxVSpaceBasisBladeId);
        }

        public static bool IsValidBasisBladeGradeIndex(this IGaFrame frame, int grade, int index)
        {
            if (grade < 0 || grade > frame.VSpaceDimension) return false;

            var kvDim = KvSpaceDimension(frame.VSpaceDimension, grade);

            return (index >= 0 && index <= kvDim);
        }

        public static bool IsValidBasisBladeGradeIndex(int vSpaceDim, int grade, int index)
        {
            if (grade < 0 || grade > vSpaceDim) return false;

            var kvDim = KvSpaceDimension(vSpaceDim, grade);

            return (index >= 0 && index <= kvDim);
        }

        public static bool IsValidGrade(this IGaFrame frame, int grade)
        {
            return (grade >= 0 && grade <= frame.VSpaceDimension);
        }

        public static bool IsValidGrade(int vSpaceDim, int grade)
        {
            return (grade >= 0 && grade <= vSpaceDim);
        }


        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfMeet(this IGaFrame frame, int grade1, int grade2)
        {
            if (grade1 > frame.VSpaceDimension || grade2 > frame.VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var maxGrade = Math.Min(grade1, grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = 0; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfJoin(this IGaFrame frame, int grade1, int grade2)
        {
            if (grade1 > frame.VSpaceDimension || grade2 > frame.VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var minGrade = Math.Max(grade1, grade2);
            var maxGrade = Math.Min(frame.VSpaceDimension, grade1 + grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Return a list of all possible grades in the geometric product of two k-vectors with
        /// the given grades
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfEGp(this IGaFrame frame, int grade1, int grade2)
        {
            if (grade1 > frame.VSpaceDimension || grade2 > frame.VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var minGrade = Math.Abs(grade1 - grade2);
            var maxGrade = Math.Min(frame.VSpaceDimension, grade1 + grade2);

            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }


        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeCliffConj(this IGaFrame frame, int grade)
        {
            var v = grade % 4;
            return v == 1 || v == 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeCliffConj(this int grade)
        {
            var v = grade % 4;
            return v == 1 || v == 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeGradeInv(this IGaFrame frame, int grade)
        {
            return (grade & 1) != 0;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeGradeInv(this int grade)
        {
            return (grade & 1) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeReverse(this IGaFrame frame, int grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static bool GradeHasNegativeReverse(this int grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }


        public static bool BasisBladeHasEvenGrade(this int basisBladeId)
        {
            return (GaLookupTables.IdToGradeTable[basisBladeId] & 1) == 0;
        }

        public static bool BasisBladeHasOddGrade(this int basisBladeId)
        {
            return (GaLookupTables.IdToGradeTable[basisBladeId] & 1) != 0;
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BasisBladeIdHasNegativeCliffConj(this IGaFrame frame, int id)
        {
            return GaLookupTables.IsNegativeCliffConjTable.Get(id);
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BasisBladeIdHasNegativeCliffConj(this int id)
        {
            return GaLookupTables.IsNegativeCliffConjTable.Get(id);
        }

        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BasisBladeIdHasNegativeGradeInv(this IGaFrame frame, int id)
        {
            return GaLookupTables.IsNegativeGradeInvTable.Get(id);
        }

        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BasisBladeIdHasNegativeGradeInv(this int id)
        {
            return GaLookupTables.IsNegativeGradeInvTable.Get(id);
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BasisBladeIdHasNegativeReverse(this IGaFrame frame, int id)
        {
            return GaLookupTables.IsNegativeReverseTable.Get(id);
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool BasisBladeIdHasNegativeReverse(this int id)
        {
            return GaLookupTables.IsNegativeReverseTable.Get(id);
        }


        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroOp(int id1, int id2)
        {
            return (id1 & id2) == 0;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        public static bool IsNonZeroOp(int id1, int id2, int id3)
        {
            return (id1 & id2 & id3) == 0;
        }

        /// <summary>
        /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEGp(int id1, int id2)
        {
            return true;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroESp(int id1, int id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroESp(ulong id1, ulong id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroELcp(int id1, int id2)
        {
            return (id1 & ~id2) == 0;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroERcp(int id1, int id2)
        {
            return (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEFdp(int id1, int id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEHip(int id1, int id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0);
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEHip(ulong id1, ulong id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroEAcp(int id1, int id2)
        {
            //A acp B = (AB + BA) / 2
            return IsNegativeEGp(id1, id2) == IsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNonZeroECp(int id1, int id2)
        {
            //A cp B = (AB - BA) / 2
            return IsNegativeEGp(id1, id2) != IsNegativeEGp(id2, id1);
        }

        public static bool IsNonZeroTriProductLeftAssociative(int id1, int id2, int id3, Func<int, int, bool> isNonZeroProductFunc1, Func<int, int, bool> isNonZeroProductFunc2)
        {
            return isNonZeroProductFunc1(id1, id2) && isNonZeroProductFunc2(id1 ^ id2, id3);
        }

        public static bool IsNonZeroTriProductRightAssociative(int id1, int id2, int id3, Func<int, int, bool> isNonZeroProductFunc1, Func<int, int, bool> isNonZeroProductFunc2)
        {
            return isNonZeroProductFunc1(id1, id2 ^ id3) && isNonZeroProductFunc2(id2, id3);
        }

        public static bool IsNonZeroELcpELcpLa(int id1, int id2, int id3)
        {
            return IsNonZeroTriProductLeftAssociative(id1, id2, id3, IsNonZeroELcp, IsNonZeroELcp);
        }

        public static bool IsNonZeroELcpELcpRa(int id1, int id2, int id3)
        {
            return IsNonZeroTriProductRightAssociative(id1, id2, id3, IsNonZeroELcp, IsNonZeroELcp);
        }


        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by MaxVSpaceDimension bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static ulong JoinIDs(ulong id1, ulong id2)
        {
            return id1 | (id2 << MaxVSpaceDimension);
        }

        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by VSpaceDimension bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static ulong JoinIDs(this IGaFrame frame, ulong id1, ulong id2)
        {
            return id1 | (id2 << frame.VSpaceDimension);
        }

        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by VSpaceDim bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static ulong JoinIDs(int vSpaceDim, ulong id1, ulong id2)
        {
            return id1 | (id2 << vSpaceDim);
        }

        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slow but can be used for GAs with dimension more than 16
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool ComputeIsNegativeEGp(int id1, int id2)
        {
            if (id1 == 0 || id2 == 0) return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0)
                            flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id = id ^ mask2;

                mask2 <<= 1;
            }

            return flag;
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNegativeEGp(int id1, int id2)
        {
            if (id1 < GaLookupTables.IsNegativeEgpLookupTables.Length)
            {
                var lookupTable = GaLookupTables.IsNegativeEgpLookupTables[id1];

                if (id2 < lookupTable.Count)
                    return lookupTable[id2];
            }

            return ComputeIsNegativeEGp(id1, id2);
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of the given basis blades is -1.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        public static bool IsNegativeEGp(int id1, int id2, int id3)
        {
            return IsNegativeEGp(id1, id2) != IsNegativeEGp(id1 ^ id2, id3);
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of a basis vector and a basis blade is -1.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool IsNegativeVectorEGp(int index1, int id2)
        {
            if (index1 < GaLookupTables.IsNegativeVectorEgpLookupTables.Length)
            {
                var lookupTable = GaLookupTables.IsNegativeEgpLookupTables[index1];

                if (id2 < lookupTable.Count)
                    return lookupTable[id2];
            }

            return ComputeIsNegativeEGp(1 << index1, id2);
        }
    }
}
