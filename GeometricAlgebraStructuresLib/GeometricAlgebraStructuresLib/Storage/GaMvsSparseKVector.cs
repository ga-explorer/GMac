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
    public sealed class GaMvsSparseKVector<T> 
        : GaGradedMultivectorStorage<T>, IGaKVectorStorage<T>
    {
        public IDictionary<int, T> ScalarsDictionary { get; private set; }

        public override int StoredTermsCount 
            => ScalarsDictionary.Count;
        
        public int Grade { get; }

        public int KvSpaceDimension { get; }
        
        
        public GaMvsSparseKVector(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, int grade) 
            : base(vSpaceDimension, scalarDomain)
        {
            Debug.Assert(grade >= 0 && grade <= VSpaceDimension);
            
            Grade = grade;
            KvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade);
            ScalarsDictionary = new Dictionary<int, T>();
        }
        
        public GaMvsSparseKVector(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, int grade, IDictionary<int, T> scalarsDictionary) 
            : base(vSpaceDimension, scalarDomain)
        {
            Debug.Assert(grade >= 0 && grade <= VSpaceDimension);
            
            Grade = grade;
            KvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade);
            
            Debug.Assert(scalarsDictionary.Keys.All(index => index >= 0 && index < KvSpaceDimension));
            
            ScalarsDictionary = scalarsDictionary;
        }

        
        public override T GetTermScalar(int grade, int index)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            return ScalarsDictionary.TryGetValue(index, out var scalar) 
                ? scalar : ScalarDomain.GetZero();
        }

        public override IGaTerm<T> GetTerm(int grade, int index)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            return ScalarsDictionary.TryGetValue(index, out var scalar) 
                ? new GaGradedTerm<T>(grade, index, scalar) 
                : new GaGradedTerm<T>(grade, index, ScalarDomain.GetZero());
        }

        public override bool TryGetTermScalar(int grade, int index, out T value)
        {
            if (grade == Grade) 
                return ScalarsDictionary.TryGetValue(index, out value);
            
            value = ScalarDomain.GetZero();
            return false;

        }

        public override bool TryGetTerm(int grade, int index, out IGaTerm<T> term)
        {
            if (grade == Grade && ScalarsDictionary.TryGetValue(index, out var scalar))
            {
                term = new GaGradedTerm<T>(grade, index, scalar);
                return true;
            }

            term = null;
            return false;
        }

        public override IGaMultivectorStorage<T> SetTermScalar(int grade, int index, T value)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            ScalarsDictionary.AddOrSet(index, value);
            
            return this;
        }

        public override bool TrySetTermScalar(int grade, int index, T value)
        {
            if (grade != Grade)
                return false;

            ScalarsDictionary.AddOrSet(index, value);
            
            return true;
        }

        
        public override IGaMultivectorStorage<T> SetKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();
            
            for (var index = 0; index < scalarValuesList.Count; index++)
            {
                var scalar = scalarValuesList[index];
                
                if (ScalarDomain.IsNotZero(scalar))
                    ScalarsDictionary.AddOrSet(index, scalar);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();
            
            for (var index = 0; index < scalarValuesList.Count; index++)
            {
                var scalar = scalarValuesList[index];
                
                if (ScalarDomain.IsNotZero(scalar))
                    ScalarsDictionary.AddOrSet(index, ScalarDomain.Times(scalingFactor, scalar));
            }

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(int grade, IEnumerable<KeyValuePair<int, T>> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();
            
            foreach (var pair in scalarValuesList)
                if (ScalarDomain.IsNotZero(pair.Value))
                    ScalarsDictionary.AddOrSet(pair.Key, pair.Value);

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IEnumerable<KeyValuePair<int, T>> scalarValuesList)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();
            
            foreach (var pair in scalarValuesList)
                if (ScalarDomain.IsNotZero(pair.Value))
                    ScalarsDictionary.AddOrSet(pair.Key, ScalarDomain.Times(scalingFactor, pair.Value));

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(IGaKVectorStorage<T> kvector)
        {
            if (kvector.Grade != Grade)
                throw new IndexOutOfRangeException();
            
            var termsList = kvector.GetNonZeroTerms();

            foreach (var term in termsList)
                ScalarsDictionary.AddOrSet(term.Index, term.Scalar);

            return this;
        }

        public override IGaMultivectorStorage<T> SetKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            if (kvector.Grade != Grade)
                throw new IndexOutOfRangeException();
            
            var termsList = kvector.GetNonZeroTerms();

            foreach (var term in termsList)
                ScalarsDictionary.AddOrSet(term.Index, ScalarDomain.Times(scalingFactor, term.Scalar));

            return this;
        }
        
        public override IGaMultivectorStorage<T> AddTerm(int grade, int index, T value)
        {
            if (grade != Grade)
                throw new IndexOutOfRangeException();

            if (ScalarsDictionary.TryGetValue(index, out var scalar))
                ScalarsDictionary[index] = ScalarDomain.Add(scalar, value);
            else
                ScalarsDictionary.Add(index, value);

            return this;
        }

        public override bool TryAddTerm(int grade, int index, T value)
        {
            if (grade != Grade)
                return false;
            
            if (ScalarsDictionary.TryGetValue(index, out var scalar))
                ScalarsDictionary[index] = ScalarDomain.Add(scalar, value);
            else
                ScalarsDictionary.Add(index, value);

            return true;
        }

        public override IGaMultivectorStorage<T> RemoveTerm(int grade, int index)
        {
            if (grade != Grade)
                return this;

            ScalarsDictionary.Remove(index);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            if (grade != Grade)
                return this;

            ScalarsDictionary.Clear();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int grade, int index, bool nearZeroFlag = false)
        {
            if (TryGetTermScalar(grade, index, out var scalar) && ScalarDomain.IsZero(scalar, nearZeroFlag))
                ScalarsDictionary.Remove(index);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            var indicesArray = 
                GetStoredZeroTermIndicesOfGrade(Grade, nearZeroFlag).ToArray();

            foreach (var index in indicesArray)
                ScalarsDictionary.Remove(index);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                return this;
            
            var indicesArray = 
                GetStoredZeroTermIndicesOfGrade(Grade, nearZeroFlag).ToArray();

            foreach (var index in indicesArray)
                ScalarsDictionary.Remove(index);

            return this;
        }

        public override IGaMultivectorStorage<T> ResetToZero()
        {
            ScalarsDictionary.Clear();

            return this;
        }

        public override bool IsEmpty()
        {
            return ScalarsDictionary.Count == 0;
        }

        public override bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsDictionary.All(p => ScalarDomain.IsNearZero(p.Value))
                : ScalarsDictionary.All(p => ScalarDomain.IsZero(p.Value));
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            return grade == Grade && ScalarsDictionary.ContainsKey(index);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade == Grade && ScalarsDictionary.Count > 0;
        }

        public override bool CanStoreTerm(int grade, int index)
        {
            return grade == Grade;
        }

        public override bool CanStoreSomeTermsOfGrade(int grade)
        {
            return grade == Grade;
        }

        public override bool CanStoreAllTermsOfGrade(int grade)
        {
            return grade == Grade;
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices()
        {
            return ScalarsDictionary
                .Select(p => new Tuple<int, int>(Grade, p.Key));
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(p.Value))
                .Select(p => new Tuple<int, int>(Grade, p.Key));
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(GaFrameUtils.BasisBladeId(Grade, p.Key)))
                .Select(p => new Tuple<int, int>(Grade, p.Key));
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(Grade, p.Key))
                .Select(p => new Tuple<int, int>(Grade, p.Key));
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(GaFrameUtils.BasisBladeId(Grade, p.Key), p.Value))
                .Select(p => new Tuple<int, int>(Grade, p.Key));
        }

        public override IEnumerable<Tuple<int, int>> GetStoredTermGradeIndices(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(Grade, p.Key, p.Value))
                .Select(p => new Tuple<int, int>(Grade, p.Key));
        }

        public override IEnumerable<int> GetStoredTermIndicesOfGrade(int grade)
        {
            return grade == Grade
                ? ScalarsDictionary.Keys
                : Enumerable.Empty<int>();
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return ScalarsDictionary.Values;
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<T, bool> selectionFilter)
        {
            return ScalarsDictionary.Values.Where(selectionFilter);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(GaFrameUtils.BasisBladeId(Grade, p.Key)))
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(GaFrameUtils.BasisBladeId(Grade, p.Key), p.Value))
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(Grade, p.Key, p.Value))
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            return grade == Grade
                ? ScalarsDictionary.Values
                : Enumerable.Empty<T>();
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            return ScalarsDictionary
                .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(p.Value))
                .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(GaFrameUtils.BasisBladeId(Grade, p.Key)))
                .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(Grade, p.Key))
                .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(GaFrameUtils.BasisBladeId(Grade, p.Key), p.Value))
                .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(Grade, p.Key, p.Value))
                .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            return grade == Grade
                ? ScalarsDictionary.Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value))
                : Enumerable.Empty<GaGradedTerm<T>>();
        }

        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? ScalarsDictionary.Count(p => ScalarDomain.IsNearZero(p.Value))
                : ScalarsDictionary.Count(p => ScalarDomain.IsZero(p.Value));
        }

        public override IEnumerable<int> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                return Enumerable.Empty<int>();

            return nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => ScalarDomain.IsNearZero(p.Value))
                    .Select(p => GaFrameUtils.BasisBladeId(Grade, p.Key))
                : ScalarsDictionary
                    .Where(p => ScalarDomain.IsZero(p.Value))
                    .Select(p => GaFrameUtils.BasisBladeId(Grade, p.Key));
        }

        public override IEnumerable<int> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                return Enumerable.Empty<int>();
            
            return nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key)
                : ScalarsDictionary
                    .Where(p => ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key);
        }

        public override int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsDictionary.Count(p => ScalarDomain.IsNearZero(p.Value))
                : ScalarsDictionary.Count(p => ScalarDomain.IsZero(p.Value));
        }

        public override IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (grade != Grade)
                return Enumerable.Empty<IGaTerm<T>>();
            
            return nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => ScalarDomain.IsNotNearZero(p.Value))
                    .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value))
                : ScalarsDictionary
                    .Where(p => ScalarDomain.IsNotZero(p.Value))
                    .Select(p => new GaGradedTerm<T>(Grade, p.Key, p.Value));
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            if (ScalarsDictionary.Count > 0)
                yield return Grade;
        }

        public override GaBinaryTree<T> GetBinaryTree()
        {
            return new GaBinaryTree<T>(
                VSpaceDimension,
                ScalarsDictionary
            );
        }

        public override GaMvsTerm<T> GetTermStorage(int grade, int index, bool getCopy = false)
        {
            var storage = new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, index);

            if (grade == Grade && ScalarsDictionary.TryGetValue(index, out var scalar))
                storage.Scalar = scalar;

            return storage;
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            if (Grade != 1)
                return storage;

            foreach (var pair in ScalarsDictionary)
                storage.ScalarsArray[pair.Key] = pair.Value;
            
            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            if (grade != Grade)
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, 0);

            if (!getCopy)
                return this;

            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            foreach (var pair in ScalarsDictionary)
                storage.ScalarsDictionary.Add(pair.Key, pair.Value);
            
            return storage;
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            if (Grade != grade)
                return storage;

            foreach (var pair in ScalarsDictionary)
                storage.ScalarsArray[pair.Key] = pair.Value;
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            if (grade != Grade)
                return storage;

            if (!getCopy)
                return this;

            foreach (var pair in ScalarsDictionary)
                storage.ScalarsDictionary.Add(pair.Key, pair.Value);
            
            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            yield return GetKVectorStorage(Grade, getCopy);
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            return new GaMvsBinaryTree<T>(VSpaceDimension, ScalarDomain, ScalarsDictionary);
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            foreach (var pair in ScalarsDictionary)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, pair.Key);

                storage.ScalarsArray[id] = pair.Value;
            }
            
            return storage;
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            foreach (var pair in ScalarsDictionary)
            {
                var id = GaFrameUtils.BasisBladeId(Grade, pair.Key);

                storage.ScalarsDictionary.Add(id, pair.Value);
            }
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            var scalarValues = new T[KvSpaceDimension];

            foreach (var pair in ScalarsDictionary)
                scalarValues[pair.Key] = pair.Value;
            
            storage.GradedScalarsArrays[Grade] = scalarValues;
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);
            
            storage.GradedScalarsDictionaries[Grade] = getCopy
                ? ScalarsDictionary.ToDictionary(p => p.Key, p => p.Value)
                : ScalarsDictionary;
            
            return storage;
        }

        public override IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            if (ScalarsDictionary.Count == 0)
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, 0, 0);

            if (ScalarsDictionary.Count > 1) 
                return this;
            
            var pair = ScalarsDictionary.First();
                
            return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, Grade, pair.Key)
            {
                Scalar = pair.Value
            };
        }

        public override IGaMultivectorStorage<T> ApplyReverse()
        {
            return Grade.GradeHasNegativeReverse() 
                ? ApplyNegative() 
                : this;
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            return Grade.GradeHasNegativeGradeInv() 
                ? ApplyNegative() 
                : this;
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            return Grade.GradeHasNegativeCliffConj() 
                ? ApplyNegative() 
                : this;
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            foreach (var pair in ScalarsDictionary)
                ScalarsDictionary[pair.Key] = ScalarDomain.Negative(pair.Value);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            foreach (var pair in ScalarsDictionary)
                ScalarsDictionary[pair.Key] = ScalarDomain.Times(scalingFactor, pair.Value);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            foreach (var pair in ScalarsDictionary)
                ScalarsDictionary[pair.Key] = mappingFunc(pair.Value);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, T, T> mappingFunc)
        {
            foreach (var pair in ScalarsDictionary)
                ScalarsDictionary[pair.Key] = mappingFunc(
                    GaFrameUtils.BasisBladeId(Grade, pair.Key), 
                    pair.Value
                );

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, int, T, T> mappingFunc)
        {
            foreach (var pair in ScalarsDictionary)
                ScalarsDictionary[pair.Key] = mappingFunc(Grade, pair.Key, pair.Value);

            return this;
        }

        public override IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<int, T>>)ScalarsDictionary).GetEnumerator();
        }
   }
}