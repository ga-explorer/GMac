using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public sealed class GaMvsDenseKVector<T> 
        : GaGradedMultivectorStorage<T>, IGaKVectorStorage<T>
    {
        public T[] ScalarsArray { get; private set; }
        
        public int Grade { get; }

        public int KvSpaceDimension 
            => ScalarsArray.Length;

        public override int StoredTermsCount 
            => ScalarsArray.Length;


        public GaMvsDenseKVector(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, int grade) 
            : base(vSpaceDimension, scalarDomain)
        {
            Debug.Assert(grade >= 0 && grade <= VSpaceDimension);

            Grade = grade;
            
            ScalarsArray = Enumerable.Repeat(
                ScalarDomain.GetZero(), 
                GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade)
            ).ToArray();
        }

        
        public override T GetTermScalar(int grade, int index)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            return ScalarsArray[index];
        }

        public override bool TryGetTermScalar(int grade, int index, out T value)
        {
            if (grade != Grade)
            {
                value = ScalarDomain.GetZero();
                return false;
            }

            value = ScalarsArray[index];
            return true;
        }

        public override IGaMultivectorStorage<T> SetTermScalar(int grade, int index, T value)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            ScalarsArray[index] = value;

            return this;
        }

        public override bool TrySetTermScalar(int grade, int index, T value)
        {
            if (grade != Grade)
                return false;

            ScalarsArray[index] = value;

            return true;
        }
        
        public override IGaMultivectorStorage<T> SetKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            for (var index = 0; index < scalarValuesList.Count; index++)
                ScalarsArray[index] = scalarValuesList[index];

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            for (var index = 0; index < scalarValuesList.Count; index++)
                ScalarsArray[index] = 
                    ScalarDomain.Times(scalingFactor, scalarValuesList[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(int grade, IEnumerable<KeyValuePair<int, T>> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            foreach (var pair in scalarValuesList)
                ScalarsArray[pair.Key] = pair.Value;

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IEnumerable<KeyValuePair<int, T>> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            foreach (var pair in scalarValuesList)
                ScalarsArray[pair.Key] = ScalarDomain.Times(scalingFactor, pair.Value);

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(IGaKVectorStorage<T> kvector)
        {
            if (kvector.Grade != Grade)
                throw new IndexOutOfRangeException();

            var termsList = 
                kvector.GetStoredTerms();

            foreach (var term in termsList)
                ScalarsArray[term.Index] = term.Scalar;

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            if (kvector.Grade != Grade)
                throw new IndexOutOfRangeException();

            var termsList = 
                kvector.GetStoredTerms();

            foreach (var term in termsList)
                ScalarsArray[term.Index] = 
                    ScalarDomain.Times(scalingFactor, term.Scalar);

            return this;
        }
        
        public override IGaMultivectorStorage<T> AddTerm(int grade, int index, T value)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            ScalarsArray[index] = ScalarDomain.Add(ScalarsArray[index], value);

            return this;
        }

        public override bool TryAddTerm(int grade, int index, T value)
        {
            if (grade != Grade)
                return false;
            
            ScalarsArray[index] = 
                ScalarDomain.Add(ScalarsArray[index], value);
            
            return true;
        }
        
        public override IGaMultivectorStorage<T> AddKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            for (var index = 0; index < scalarValuesList.Count; index++)
                ScalarsArray[index] = scalarValuesList[index];

            return this;
        }

        public override IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            for (var index = 0; index < scalarValuesList.Count; index++)
                ScalarsArray[index] = ScalarDomain.Times(scalingFactor, scalarValuesList[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> AddKVector(int grade, IEnumerable<KeyValuePair<int, T>> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            foreach (var pair in scalarValuesList)
                ScalarsArray[pair.Key] = pair.Value; 
            
            return this;
        }

        public override IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IEnumerable<KeyValuePair<int, T>> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            foreach (var pair in scalarValuesList)
                ScalarsArray[pair.Key] = ScalarDomain.Times(scalingFactor, pair.Value); 
            
            return this;
        }
        
        public override IGaMultivectorStorage<T> RemoveTerm(int grade, int index)
        {
            if (grade != Grade)
                return this;

            ScalarsArray[index] = ScalarDomain.GetZero();
            
            return this;
        }
        
        public override IGaMultivectorStorage<T> RemoveTerms(int grade, IEnumerable<int> indexList)
        {
            if (grade != Grade)
                return this;

            foreach (var index in indexList)
                ScalarsArray[index] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerms(Func<T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(scalar))
                    ScalarsArray[index] = ScalarDomain.GetZero();
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerms(Func<int, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);
                
                if (selectionFilter(id))
                    ScalarsArray[index] = ScalarDomain.GetZero();
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerms(Func<int, int, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                if (selectionFilter(Grade, index))
                    ScalarsArray[index] = ScalarDomain.GetZero();
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerms(Func<int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(id, scalar))
                    ScalarsArray[index] = ScalarDomain.GetZero();
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerms(Func<int, int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(Grade, index, scalar))
                    ScalarsArray[index] = ScalarDomain.GetZero();
            }

            return this;
        }
        
        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            if (grade != Grade)
                return this;
            
            ScalarsArray = Enumerable.Repeat(ScalarDomain.GetZero(), VSpaceDimension).ToArray();

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int grade, int index, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                return this;
            
            if (ScalarDomain.IsZero(ScalarsArray[index], nearZeroFlag))
                ScalarsArray[index] = ScalarDomain.GetZero();

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            for (var i = 0; i < ScalarsArray.Length; i++)
                if (ScalarDomain.IsZero(ScalarsArray[i], nearZeroFlag))
                    ScalarsArray[i] = ScalarDomain.GetZero();

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                return this;
            
            for (var i = 0; i < ScalarsArray.Length; i++)
                if (ScalarDomain.IsZero(ScalarsArray[i], nearZeroFlag))
                    ScalarsArray[i] = ScalarDomain.GetZero();

            return this;
        }

        public override IGaMultivectorStorage<T> ResetToZero()
        {
            ScalarsArray = Enumerable.Repeat(
                ScalarDomain.GetZero(), 
                GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade)
            ).ToArray();
            
            return this;
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsArray.All(s => ScalarDomain.IsNearZero(s))
                : ScalarsArray.All(s => ScalarDomain.IsZero(s));
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            return (grade == Grade);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return (grade == Grade);
        }

        public override bool CanStoreTerm(int grade, int index)
        {
            return (grade == Grade);
        }

        public override bool CanStoreSomeTermsOfGrade(int grade)
        {
            return (grade == Grade);
        }

        public override bool CanStoreAllTermsOfGrade(int grade)
        {
            return (grade == Grade);
        }
        
        public override IEnumerable<int> GetStoredTermIds()
        {
            return Enumerable
                .Range(0, ScalarsArray.Length)
                .Select(index => GaFrameUtils.BasisBladeId(Grade, index));
        }

        public override IEnumerable<int> GetStoredTermIds(Func<T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (selectionFilter(ScalarsArray[index]))
                    yield return GaFrameUtils.BasisBladeId(Grade, index);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, bool> selectionFilter)
        {
            return Enumerable
                .Range(0, ScalarsArray.Length)
                .Select(index => GaFrameUtils.BasisBladeId(Grade, index))
                .Where(selectionFilter);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, bool> selectionFilter)
        {
            return Enumerable
                .Range(0, ScalarsArray.Length)
                .Where(index => selectionFilter(Grade, index));
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(id, scalar))
                    yield return id;
            }
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(Grade, index, scalar))
                    yield return GaFrameUtils.BasisBladeId(Grade, index);
            }
        }

        public override IEnumerable<int> GetStoredTermIdsOfGrade(int grade)
        {
            return grade == Grade
                ? Enumerable.Range(0, ScalarsArray.Length).Select(index => GaFrameUtils.BasisBladeId(Grade, index)) 
                : Enumerable.Empty<int>();
        }
        
        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices()
        {
            return Enumerable
                .Range(0, ScalarsArray.Length)
                .Select(index => new Tuple<int, int>(Grade, index));
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (selectionFilter(ScalarsArray[index]))
                    yield return new Tuple<int, int>(Grade, index);
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (selectionFilter(GaFrameUtils.BasisBladeId(Grade, index)))
                    yield return new Tuple<int, int>(Grade, index);
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (selectionFilter(GaFrameUtils.BasisBladeId(Grade, index), ScalarsArray[index]))
                    yield return new Tuple<int, int>(Grade, index);
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (selectionFilter(Grade, index, ScalarsArray[index]))
                    yield return new Tuple<int, int>(Grade, index);
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, int, bool> selectionFilter)
        {
            return Enumerable
                .Range(0, ScalarsArray.Length)
                .Where(index => selectionFilter(Grade, index))
                .Select(index => new Tuple<int, int>(Grade, index));
        }
        
        public override IEnumerable<int> GetStoredTermIndicesOfGrade(int grade)
        {
            return (grade == Grade)
                ? Enumerable.Range(0, ScalarsArray.Length)
                : Enumerable.Empty<int>();
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return ScalarsArray;
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (selectionFilter(GaFrameUtils.BasisBladeId(Grade, index)))
                    yield return ScalarsArray[index];
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(GaFrameUtils.BasisBladeId(Grade, index), scalar))
                    yield return scalar;
            }
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(Grade, index, scalar))
                    yield return scalar;
            }
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            return (grade == Grade)
                ? ScalarsArray
                : Enumerable.Empty<T>();
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            return ScalarsArray.Select((scalar, index) => 
                new GaGradedTerm<T>(Grade, index, scalar)
            );
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(scalar))
                    yield return new GaGradedTerm<T>(Grade, index, scalar);
            }
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);
                
                if (selectionFilter(id))
                    yield return new GaGradedTerm<T>(Grade, index, ScalarsArray[index]);
            }
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, int, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                if (selectionFilter(Grade, index))
                    yield return new GaGradedTerm<T>(Grade, index, ScalarsArray[index]);
            }
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(id, scalar))
                    yield return new GaGradedTerm<T>(Grade, index, scalar);
            }
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, int, T, bool> selectionFilter)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (selectionFilter(Grade, index, scalar))
                    yield return new GaGradedTerm<T>(Grade, index, scalar);
            }
        }
        
        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsArray.Count(s => ScalarDomain.IsNearZero(s))
                : ScalarsArray.Count(s => ScalarDomain.IsZero(s));
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            yield return Grade;
        }

        public override int GetStoredGradesBitPattern()
        {
            return 1 << Grade;
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            return grade == Grade 
                ? ScalarsArray.Select((scalar, index) => 
                    new GaGradedTerm<T>(Grade, index, scalar)
                )
                : Enumerable.Empty<IGaTerm<T>>();
        }

        public override IEnumerable<int> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                yield break;
            
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (ScalarDomain.IsZero(ScalarsArray[index], nearZeroFlag))
                    yield return GaFrameUtils.BasisBladeId(Grade, index);
        }

        public override IEnumerable<int> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                yield break;
            
            for (var index = 0; index < ScalarsArray.Length; index++)
                if (ScalarDomain.IsZero(ScalarsArray[index], nearZeroFlag))
                    yield return index;
        }
        
        public override int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsArray.Count(s => ScalarDomain.IsNotNearZero(s))
                : ScalarsArray.Count(s => ScalarDomain.IsNotZero(s));
        }

        public override IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                yield break;
            
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (ScalarDomain.IsNotZero(scalar, nearZeroFlag))
                    yield return new GaGradedTerm<T>(Grade, index, scalar);
            }
        }

        public override GaBinaryTree<T> GetBinaryTree()
        {
            var idsList = Enumerable
                .Range(0, ScalarsArray.Length)
                .Select(index => GaFrameUtils.BasisBladeId(Grade, index))
                .ToArray();
            
            return new GaBinaryTree<T>(
                VSpaceDimension, 
                idsList,
                ScalarsArray
            );
        }

        public override GaMvsTerm<T> GetTermStorage(int grade, int index, bool getCopy = false)
        {
            var storage = new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, index);

            if (grade == Grade)
                storage.Scalar = ScalarsArray[index];

            return storage;
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            if (Grade != 1) 
                return new GaMvsVector<T>(VSpaceDimension, ScalarDomain);
            
            if (!getCopy)
                return new GaMvsVector<T>(ScalarDomain, ScalarsArray);
            
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            ScalarsArray.CopyTo(storage.ScalarsArray, 0);

            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            if (grade != Grade) 
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, 0);
            
            if (!getCopy)
                return this;
            
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);
            
            ScalarsArray.CopyTo(storage.ScalarsArray, 0);
            
            return storage;
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            if (grade != Grade) 
                return new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);
            
            if (!getCopy)
                return this;
            
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);
            
            ScalarsArray.CopyTo(storage.ScalarsArray, 0);
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            if (grade != Grade)
                return storage;
            
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (ScalarDomain.IsNotZero(scalar))
                    storage.ScalarsDictionary.AddOrSet(index, scalar);
            }
            
            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            yield return GetKVectorStorage(Grade, getCopy);
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            var idsList = Enumerable
                .Range(0, ScalarsArray.Length)
                .Select(index => GaFrameUtils.BasisBladeId(Grade, index))
                .ToArray();
            
            return new GaMvsBinaryTree<T>(
                VSpaceDimension, 
                ScalarDomain,
                idsList,
                ScalarsArray
            );
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);

                storage.ScalarsArray[id] = ScalarsArray[index];
            }
            
            return storage;
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];

                if (ScalarDomain.IsZero(scalar))
                    continue;
                
                var id = GaFrameUtils.BasisBladeId(Grade, index);

                storage.ScalarsDictionary.AddOrSet(id, scalar);
            }
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            storage.GradedScalarsArrays[Grade] = getCopy
                ? ScalarsArray.GetCopyOfArray() 
                : ScalarsArray;
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            var scalarValuesDictionary = storage.GradedScalarsDictionaries[Grade];
            
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];

                if (ScalarDomain.IsZero(scalar))
                    continue;
                
                scalarValuesDictionary.AddOrSet(index, scalar);
            }
            
            return storage;
        }

        public override IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            var nonZeroTermsCount = GetNonZeroTermsCount(nearZeroFlag);

            if (StoredTermsCount == nonZeroTermsCount)
                return GetDenseKVectorStorage(Grade, getCopy);

            if (nonZeroTermsCount != 1) 
                return GetSparseKVectorStorage(Grade);

            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var scalar = ScalarsArray[index];
                
                if (ScalarDomain.IsNotZero(scalar, nearZeroFlag))
                    return GetTermStorage(Grade, index);
            }
            
            throw new InvalidOperationException();
        }

        public override IGaMultivectorStorage<T> ApplyReverse()
        {
            return this;
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                ScalarsArray[index] = ScalarDomain.Negative(ScalarsArray[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                ScalarsArray[index] = ScalarDomain.Negative(ScalarsArray[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                ScalarsArray[index] = ScalarDomain.Negative(ScalarsArray[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                ScalarsArray[index] = ScalarDomain.Times(scalingFactor, ScalarsArray[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                ScalarsArray[index] = mappingFunc(ScalarsArray[index]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, T, T> mappingFunc)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, index);
                
                ScalarsArray[index] = mappingFunc(id, ScalarsArray[index]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, int, T, T> mappingFunc)
        {
            for (var index = 0; index < ScalarsArray.Length; index++)
                ScalarsArray[index] = mappingFunc(Grade, index, ScalarsArray[index]);

            return this;
        }

        public override IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return ScalarsArray
                .Select((scalar, index) => 
                    new KeyValuePair<int, T>(GaFrameUtils.BasisBladeId(Grade, index), scalar)
                )
                .GetEnumerator();
        }
    }
}