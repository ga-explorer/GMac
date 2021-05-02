using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public sealed class GaMvsSparseGraded<T> 
        : GaGradedMultivectorStorage<T>
    {
        public IDictionary<ulong, T>[] GradedScalarsDictionaries { get; }

        public override int StoredTermsCount
            => GradedScalarsDictionaries.Sum(a => a.Count);
        
        
        public GaMvsSparseGraded(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
            : base(vSpaceDimension, scalarDomain)
        {
            GradedScalarsDictionaries = 
                new IDictionary<ulong, T>[vSpaceDimension + 1];

            for (var i = 0; i <= vSpaceDimension; i++)
                GradedScalarsDictionaries[i] = new Dictionary<ulong, T>();
        }

        
        public override T GetTermScalar(int grade, ulong index)
        {
            return GradedScalarsDictionaries[grade][index];
        }

        public override bool TryGetTermScalar(int grade, ulong index, out T value)
        {
            return GradedScalarsDictionaries[grade].TryGetValue(index, out value);
        }

        public override IGaMultivectorStorage<T> SetTermScalar(int grade, ulong index, T value)
        {
            GradedScalarsDictionaries[grade].AddOrSet(index, value);

            return this;
        }

        public override IGaMultivectorStorage<T> AddTerm(int grade, ulong index, T value)
        {
            var scalarValues = GradedScalarsDictionaries[grade];

            if (scalarValues.TryGetValue(index, out var oldValue))
                scalarValues[index] = ScalarDomain.Add(oldValue, value);
            else
                scalarValues.Add(index, value);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerm(int grade, ulong index)
        {
            GradedScalarsDictionaries[grade].Remove(index);
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            GradedScalarsDictionaries[grade].Clear();

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int grade, ulong index, bool nearZeroFlag = false)
        {
            var scalarValues = GradedScalarsDictionaries[grade];

            if (scalarValues.TryGetValue(index, out var scalar) && ScalarDomain.IsZero(scalar, nearZeroFlag))
                scalarValues.Remove(index);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                if (scalarValues.Count == 0)
                    continue;
                
                var indicesList = nearZeroFlag 
                    ? scalarValues
                        .Where(p => ScalarDomain.IsNearZero(p.Value))
                        .Select(p => p.Key)
                        .ToArray()
                    : scalarValues
                        .Where(p => ScalarDomain.IsZero(p.Value))
                        .Select(p => p.Key)
                        .ToArray();

                scalarValues.Remove(indicesList);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            var scalarValues = GradedScalarsDictionaries[grade];

            if (scalarValues.Count == 0)
                return this;
                
            var indicesList = nearZeroFlag 
                ? scalarValues
                    .Where(p => ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key)
                    .ToArray()
                : scalarValues
                    .Where(p => ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key)
                    .ToArray();

            scalarValues.Remove(indicesList);
            
            return this;
        }

        public override IGaMultivectorStorage<T> ResetToZero()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                GradedScalarsDictionaries[grade].Clear();
            
            return this;
        }

        public override bool IsEmpty()
        {
            return GradedScalarsDictionaries.All(d => d.Count == 0);
        }

        public override bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GradedScalarsDictionaries.SelectMany(d => d.Values).All(s => ScalarDomain.IsNearZero(s))
                : GradedScalarsDictionaries.SelectMany(d => d.Values).All(s => ScalarDomain.IsZero(s));
        }

        public override bool ContainsStoredTerm(int grade, ulong index)
        {
            return GradedScalarsDictionaries[grade].ContainsKey(index);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return GradedScalarsDictionaries[grade].Count > 0;
        }

        public override bool CanStoreTerm(int grade, ulong index)
        {
            return grade >= 0 && grade <= VSpaceDimension && 
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

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                foreach (var index in GradedScalarsDictionaries[grade].Keys)
                    yield return new Tuple<int, ulong>(grade, index);
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                    if (selectionFilter(term.Value))
                        yield return new Tuple<int, ulong>(grade, term.Key);
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var index in scalarValues.Keys)
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
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, term.Key);
                    
                    if (selectionFilter(id, term.Value))
                        yield return new Tuple<int, ulong>(grade, term.Key);
                }
            }
        }

        public override IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(
            Func<int, ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                    if (selectionFilter(grade, term.Key, term.Value))
                        yield return new Tuple<int, ulong>(grade, term.Key);
            }
        }

        public override IEnumerable<ulong> GetStoredTermIndicesOfGrade(int grade)
        {
            return GradedScalarsDictionaries[grade].Keys;
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return GradedScalarsDictionaries.SelectMany(d => d.Values);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<ulong, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, term.Key);
                    
                    if (selectionFilter(id))
                        yield return term.Value;
                }
            }
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, term.Key);
                    
                    if (selectionFilter(id, term.Value))
                        yield return term.Value;
                }
            }
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, ulong, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                    if (selectionFilter(grade, term.Key, term.Value))
                        yield return term.Value;
            }
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            return GradedScalarsDictionaries[grade].Values;
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarsDictionaries[grade];
                
                foreach (var term in scalarValues)
                    yield return new GaGradedTerm<T>(grade, term.Key, term.Value);
            }
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            return GradedScalarsDictionaries[grade]
                .Select(term => 
                    new GaGradedTerm<T>(grade, term.Key, term.Value)
                );
        }

        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsDictionaries
                    .SelectMany(p => p.Values)
                    .Count(s => ScalarDomain.IsNearZero(s))
                : GradedScalarsDictionaries
                    .SelectMany(p => p.Values)
                    .Count(s => ScalarDomain.IsZero(s));
        }

        public override IEnumerable<ulong> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsDictionaries[grade]
                    .Where(p => ScalarDomain.IsNearZero(p.Value))
                    .Select(p => GaFrameUtils.BasisBladeId(grade, p.Key))
                : GradedScalarsDictionaries[grade]
                    .Where(p => ScalarDomain.IsZero(p.Value))
                    .Select(p => GaFrameUtils.BasisBladeId(grade, p.Key));
        }

        public override IEnumerable<ulong> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsDictionaries[grade]
                    .Where(p => ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key)
                : GradedScalarsDictionaries[grade]
                    .Where(p => ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key);
        }

        public override int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsDictionaries
                    .SelectMany(d => d.Values)
                    .Count(ScalarDomain.IsNotNearZero)
                : GradedScalarsDictionaries
                    .SelectMany(d => d.Values)
                    .Count(ScalarDomain.IsNotZero);
        }

        public override IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? GradedScalarsDictionaries[grade]
                    .Where(p => ScalarDomain.IsNotNearZero(p.Value))
                    .Select(p => new GaGradedTerm<T>(grade, p.Key, p.Value))
                : GradedScalarsDictionaries[grade]
                    .Where(p => ScalarDomain.IsNotZero(p.Value))
                    .Select(p => new GaGradedTerm<T>(grade, p.Key, p.Value));
        }

        public override GaBinaryTree<T> GetBinaryTree()
        {
            var termsDictionary = (IDictionary<ulong, T>)GetStoredTerms().ToDictionary(
                term => term.Id,
                term => term.Scalar
            );
            
            return new GaBinaryTree<T>(
                VSpaceDimension,
                termsDictionary
            );
        }

        public override GaMvsTerm<T> GetTermStorage(int grade, ulong index, bool getCopy = false)
        {
            var scalarValues = GradedScalarsDictionaries[grade];

            var storage = new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, index);

            if (scalarValues.TryGetValue(index, out var scalar))
                storage.Scalar = scalar;

            return storage;
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var scalarValues = GradedScalarsDictionaries[1];
            
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            foreach (var term in scalarValues)
                storage.ScalarsArray[term.Key] = term.Value; 
            
            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            return GetSparseKVectorStorage(grade, getCopy);
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var scalarValues = GradedScalarsDictionaries[grade];
            
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            foreach (var term in scalarValues)
                storage.ScalarsArray[term.Key] = term.Value; 
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var scalarValues = GradedScalarsDictionaries[grade];

            if (!getCopy)
                return new GaMvsSparseKVector<T>(
                    VSpaceDimension,
                    ScalarDomain,
                    grade,
                    scalarValues
                );

            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);
            
            foreach (var term in scalarValues)
                storage.ScalarsDictionary.Add(term.Key, term.Value); 
            
            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Select(grade => GetSparseKVectorStorage(grade, getCopy));
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            var termsDictionary = (IDictionary<ulong, T>)GetStoredTerms().ToDictionary(
                term => term.Id,
                term => term.Scalar
            );
            
            return new GaMvsBinaryTree<T>(
                VSpaceDimension,
                ScalarDomain,
                termsDictionary
            );
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, term.Key);

                    storage.ScalarsArray[id] = term.Value;
                }
            }
            
            return storage;
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, term.Key);

                    storage.ScalarsDictionary.Add(id, term.Value);
                }
            }
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];
                
                if (scalarsDictionary.Count == 0)
                    continue;

                var scalarsArray = new T[GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade)];
                
                foreach (var term in scalarsDictionary)
                    scalarsArray[term.Key] = term.Value;

                storage.GradedScalarsArrays[grade] = scalarsArray;
            }
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];
                
                if (scalarsDictionary.Count == 0)
                    continue;

                if (!getCopy)
                {
                    storage.GradedScalarsDictionaries[grade] = scalarsDictionary;
                    continue;
                }
                
                var newScalarsDictionary = new Dictionary<ulong, T>();
                
                foreach (var term in scalarsDictionary)
                    newScalarsDictionary[term.Key] = term.Value;

                storage.GradedScalarsDictionaries[grade] = newScalarsDictionary;
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

                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = ScalarDomain.Negative(term.Value);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                if (!grade.GradeHasNegativeGradeInv())
                    continue;

                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = ScalarDomain.Negative(term.Value);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                if (!grade.GradeHasNegativeCliffConj())
                    continue;

                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = ScalarDomain.Negative(term.Value);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = ScalarDomain.Negative(term.Value);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = ScalarDomain.Times(scalingFactor, term.Value);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = mappingFunc(term.Value);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<ulong, T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, term.Key);
                    
                    scalarsDictionary[term.Key] = mappingFunc(id, term.Value);
                }
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, ulong, T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    scalarsDictionary[term.Key] = mappingFunc(grade, term.Key, term.Value);
            }

            return this;
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            return Enumerable
                .Range(0, VSpaceDimension + 1)
                .Where(grade => GradedScalarsDictionaries[grade].Count > 0);
        }

        public override IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarsDictionary = GradedScalarsDictionaries[grade];

                foreach (var term in scalarsDictionary)
                    yield return new KeyValuePair<ulong, T>(
                        GaFrameUtils.BasisBladeId(grade, term.Key),
                        term.Value
                    );
            }
        }
    }
}