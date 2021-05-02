using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public class GaMvsDenseGraded<T> 
        : GaGradedMultivectorStorage<T>
    {
        public T[][] GradedScalarsArrays { get; }


        public override int StoredTermsCount
            => GradedScalarsArrays
                .Where(a => !a.IsNullOrEmpty())
                .Select(a => a.Length)
                .Sum();

        
        public GaMvsDenseGraded(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
            : base(vSpaceDimension, scalarDomain)
        {
            GradedScalarsArrays = new T[vSpaceDimension + 1][];
        }


        private T[] GetKVectorScalarsArray(int grade)
        {
            var scalarsArray = GradedScalarsArrays[grade];

            if (!scalarsArray.IsNullOrEmpty()) 
                return scalarsArray;
            
            scalarsArray = new T[GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade)];
            GradedScalarsArrays[grade] = scalarsArray;

            return scalarsArray;
        }

        public override T GetTermScalar(int grade, ulong index)
        {
            var scalarValues = GradedScalarsArrays[grade];
            
            if (scalarValues.IsNullOrEmpty())
                throw new IndexOutOfRangeException();
            
            return scalarValues[index];
        }

        public override bool TryGetTermScalar(int grade, ulong index, out T value)
        {
            var scalarValues = GradedScalarsArrays[grade];

            if (scalarValues.IsNullOrEmpty())
            {
                value = ScalarDomain.GetZero();
                return false;
            }

            value = scalarValues[index];
            return true;
        }

        public override IGaMultivectorStorage<T> SetTermScalar(int grade, ulong index, T value)
        {
            var scalarValues = GetKVectorScalarsArray(grade);
                
            scalarValues[index] = value;
            
            return this;
        }

        public override IGaMultivectorStorage<T> AddTerm(int grade, ulong index, T value)
        {
            var scalarValues = GetKVectorScalarsArray(grade);
            
            scalarValues[index] = ScalarDomain.Add(scalarValues[index], value);
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerm(int grade, ulong index)
        {
            var scalarValues = GradedScalarsArrays[grade];

            if (scalarValues.IsNullOrEmpty())
                return this;

            scalarValues[index] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            GradedScalarsArrays[grade] = null;

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int grade, ulong index, bool nearZeroFlag = false)
        {
            var scalarValuesArray = GradedScalarsArrays[grade];
            
            if (scalarValuesArray.IsNullOrEmpty())
                return this;

            if (ScalarDomain.IsZero(scalarValuesArray[index], nearZeroFlag))
                scalarValuesArray[index] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                var allZero = true;
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    if (ScalarDomain.IsZero(scalarValues[index], nearZeroFlag))
                        scalarValues[index] = ScalarDomain.GetZero();
                    else
                        allZero = false;
                }

                if (allZero)
                    GradedScalarsArrays[grade] = null;
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            var scalarValues = GradedScalarsArrays[grade];
                
            if (scalarValues.IsNullOrEmpty())
                return this;

            var allZero = true;
            for (var index = 0; index < scalarValues.Length; index++)
            {
                if (ScalarDomain.IsZero(scalarValues[index], nearZeroFlag))
                    scalarValues[index] = ScalarDomain.GetZero();
                else
                    allZero = false;
            }

            if (allZero)
                GradedScalarsArrays[grade] = null;
            
            return this;
        }

        public override IGaMultivectorStorage<T> ResetToZero()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                GradedScalarsArrays[grade] = null;

            return this;
        }

        public override bool IsEmpty()
        {
            return GradedScalarsArrays.All(a => a.IsNullOrEmpty());
        }

        public override bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsArrays
                    .Where(a => !a.IsNullOrEmpty())
                    .SelectMany(a => a)
                    .All(s => ScalarDomain.IsNearZero(s))
                : GradedScalarsArrays
                    .Where(a => !a.IsNullOrEmpty())
                    .SelectMany(a => a)
                    .All(s => ScalarDomain.IsZero(s));
        }

        public override bool ContainsStoredTerm(int grade, ulong index)
        {
            var scalarValues = GradedScalarsArrays[grade];

            if (scalarValues.IsNullOrEmpty())
                return false;

            return index < (ulong)scalarValues.Length;
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return !GradedScalarsArrays[grade].IsNullOrEmpty();
        }

        public override bool CanStoreTerm(int grade, ulong index)
        {
            return 
                grade >= 0 && grade <= VSpaceDimension &&
                index < GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
        }

        public override bool CanStoreSomeTermsOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension;
        }

        public override bool CanStoreAllTermsOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension;
        }

        public override IEnumerable<ulong> GetStoredTermIds()
        {
            var gradesList = GetStoredGrades();

            foreach (var grade in gradesList)
            {
                var count = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                for (var index = 0UL; index < count; index++)
                    yield return GaFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                    yield return new Tuple<int, ulong>(grade, index);
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                    if (selectionFilter(scalarValues[index]))
                        yield return new Tuple<int, ulong>(grade, index);
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    
                    if (selectionFilter(id))
                        yield return new Tuple<int, ulong>(grade, index);
                }
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    
                    if (selectionFilter(id, scalarValues[index]))
                        yield return new Tuple<int, ulong>(grade, index);
                }
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(
            Func<int, ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                    if (selectionFilter(grade, index, scalarValues[index]))
                        yield return new Tuple<int, ulong>(grade, index);
            }
        }

        public override IEnumerable<ulong> GetStoredTermIndicesOfGrade(int grade)
        {
            var scalarValues = GradedScalarsArrays[grade];

            return scalarValues.IsNullOrEmpty()
                ? Enumerable.Empty<ulong>()
                : Enumerable.Range(0, scalarValues.Length).Select(id => (ulong)id);
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return GradedScalarsArrays
                .Where(a => !a.IsNullOrEmpty())
                .SelectMany(a => a);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<ulong, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, (ulong)index);
                    
                    if (selectionFilter(id))
                        yield return scalarValues[index];
                }
            }
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, (ulong)index);
                    var scalar = scalarValues[index];
                    
                    if (selectionFilter(id, scalar))
                        yield return scalar;
                }
            }
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var scalar = scalarValues[index];
                    
                    if (selectionFilter(grade, (ulong)index, scalar))
                        yield return scalar;
                }
            }
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            var scalarValues = GradedScalarsArrays[grade];

            return scalarValues.IsNullOrEmpty()
                ? Enumerable.Empty<T>() 
                : scalarValues;
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;
                
                for (var index = 0; index < scalarValues.Length; index++)
                    yield return new GaGradedTerm<T>(grade, (ulong)index, scalarValues[index]);
            }
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            var scalarValues = GradedScalarsArrays[grade];
                
            if (scalarValues.IsNullOrEmpty())
                yield break;
                
            for (var index = 0; index < scalarValues.Length; index++)
                yield return new GaGradedTerm<T>(grade, (ulong)index, scalarValues[index]);
        }

        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsArrays
                    .Where(a => !a.IsNullOrEmpty())
                    .SelectMany(a => a)
                    .Count(s => ScalarDomain.IsNearZero(s))
                : GradedScalarsArrays
                    .Where(a => !a.IsNullOrEmpty())
                    .SelectMany(a => a)
                    .Count(s => ScalarDomain.IsZero(s));
        }

        public override IEnumerable<ulong> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            var scalarValues = GradedScalarsArrays[grade];
            
            if (scalarValues.IsNullOrEmpty())
                yield break;

            if (nearZeroFlag)
            {
                for (var index = 0; index < scalarValues.Length; index++)
                    if (ScalarDomain.IsNearZero(scalarValues[index]))
                        yield return GaFrameUtils.BasisBladeId(grade, (ulong)index);
            }
            else
            {
                for (var index = 0; index < scalarValues.Length; index++)
                    if (ScalarDomain.IsZero(scalarValues[index]))
                        yield return GaFrameUtils.BasisBladeId(grade, (ulong)index);
            }
        }

        public override IEnumerable<ulong> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            var scalarValues = GradedScalarsArrays[grade];
            
            if (scalarValues.IsNullOrEmpty())
                yield break;

            if (nearZeroFlag)
            {
                for (var index = 0; index < scalarValues.Length; index++)
                    if (ScalarDomain.IsNearZero(scalarValues[index]))
                        yield return (ulong)index;
            }
            else
            {
                for (var index = 0; index < scalarValues.Length; index++)
                    if (ScalarDomain.IsZero(scalarValues[index]))
                        yield return (ulong)index;
            }
        }

        public override int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsArrays
                    .Where(a => !a.IsNullOrEmpty())
                    .SelectMany(a => a)
                    .Count(s => ScalarDomain.IsNotNearZero(s))
                : GradedScalarsArrays
                    .Where(a => !a.IsNullOrEmpty())
                    .SelectMany(a => a)
                    .Count(s => ScalarDomain.IsNotZero(s));
        }

        public override IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            var scalarValues = GradedScalarsArrays[grade];
            
            if (scalarValues.IsNullOrEmpty())
                yield break;
            
            if (nearZeroFlag)
            {
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var scalar = scalarValues[index];
                    
                    if (ScalarDomain.IsNearZero(scalar))
                        yield return new GaGradedTerm<T>(grade, (ulong)index, scalar);
                }
            }
            else
            {
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var scalar = scalarValues[index];
                    
                    if (ScalarDomain.IsZero(scalar))
                        yield return new GaGradedTerm<T>(grade, (ulong)index, scalar);
                }
            }
        }

        public override GaBinaryTree<T> GetBinaryTree()
        {
            var termsList = GetStoredTerms().ToArray();
            var idsList = termsList.Select(t => t.Id).ToArray();
            var scalarsList = termsList.Select(t => t.Scalar).ToArray();
            
            return new GaBinaryTree<T>(
                VSpaceDimension,
                idsList,
                scalarsList
            );
        }

        public override GaMvsTerm<T> GetTermStorage(int grade, ulong index, bool getCopy = false)
        {
            var scalarValues = GradedScalarsArrays[grade];

            var storage = new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, index);
            
            if (!scalarValues.IsNullOrEmpty())
                storage.Scalar = scalarValues[index];
            
            return storage;
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            var scalarValues = GradedScalarsArrays[1];
            
            if (!scalarValues.IsNullOrEmpty())
                scalarValues.CopyTo(storage.ScalarsArray, 0);
            
            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            return GetDenseKVectorStorage(grade, getCopy);
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var scalarValues = GradedScalarsArrays[grade];
            
            if (!scalarValues.IsNullOrEmpty())
                scalarValues.CopyTo(storage.ScalarsArray, 0);
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var scalarValues = GradedScalarsArrays[grade];

            if (scalarValues.IsNullOrEmpty()) 
                return storage;
            
            for (var index = 0; index < scalarValues.Length; index++)
            {
                var scalar = scalarValues[index];
                    
                if (!ScalarDomain.IsZero(scalar))
                    storage.ScalarsDictionary.Add((ulong)index, scalar);
            }

            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
            
                if (scalarValues.IsNullOrEmpty())
                    continue;

                var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

                scalarValues.CopyTo(storage.ScalarsArray, 0);
            
                yield return storage;
            }
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            var termsList = GetStoredTerms().ToArray();
            var idsList = termsList.Select(t => t.Id).ToArray();
            var scalarsList = termsList.Select(t => t.Scalar).ToArray();
            
            return new GaMvsBinaryTree<T>(
                VSpaceDimension,
                ScalarDomain,
                idsList,
                scalarsList
            );
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, (ulong)index);

                    storage.ScalarsArray[id] = scalarValues[index];
                }
            }
            
            return storage;
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, (ulong)index);

                    storage.ScalarsDictionary.Add(id, scalarValues[index]);
                }
            }
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            if (!getCopy)
                return this;
            
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                var newScalarValues = new T[scalarValues.Length];
                
                scalarValues.CopyTo(newScalarValues, 0);

                storage.GradedScalarsArrays[grade] = newScalarValues;
            }
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                var targetScalarValuesDictionary = 
                    storage.GradedScalarsDictionaries[grade];
                
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var scalar = scalarValues[index];
                    
                    if (ScalarDomain.IsNotZero(scalar))
                        targetScalarValuesDictionary.Add((ulong)index, scalar);
                }
            }
            
            return storage;
        }

        public override IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyReverse()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                if (!grade.GradeHasNegativeReverse())
                    continue;
                
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = ScalarDomain.Negative(scalarValues[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                if (!grade.GradeHasNegativeGradeInv())
                    continue;
                
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = ScalarDomain.Negative(scalarValues[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                if (!grade.GradeHasNegativeCliffConj())
                    continue;
                
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = ScalarDomain.Negative(scalarValues[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = ScalarDomain.Negative(scalarValues[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = ScalarDomain.Times(scalingFactor, scalarValues[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = mappingFunc(scalarValues[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<ulong, T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, (ulong)index);
                    
                    scalarValues[index] = mappingFunc(id, scalarValues[index]);
                }
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, ulong, T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = mappingFunc(grade, (ulong)index, scalarValues[index]);
            }

            return this;
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            for (var i = 0; i < GradedScalarsArrays.Length; i++)
                if (!GradedScalarsArrays[i].IsNullOrEmpty())
                    yield return i;
        }

        public override IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsArrays[grade];
                
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    yield return new KeyValuePair<ulong, T>((ulong)index, scalarValues[index]);
            }
        }
    }
}