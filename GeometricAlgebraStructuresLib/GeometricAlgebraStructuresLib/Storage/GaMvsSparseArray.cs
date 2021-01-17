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
    public class GaMvsSparseArray<T> 
        : GaUniformMultivectorStorage<T>
    {
        public IDictionary<int, T> ScalarsDictionary { get; private set; }

        public override int StoredTermsCount
            => ScalarsDictionary.Count;


        public GaMvsSparseArray(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsDictionary = new Dictionary<int, T>();
        }

        public GaMvsSparseArray(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, IDictionary<int, T> scalarsDictionary)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsDictionary = scalarsDictionary;
        }


        public override T GetTermScalar(int basisBladeId)
        {
            return ScalarsDictionary.TryGetValue(basisBladeId, out var value) 
                ? value : ScalarDomain.GetZero();
        }

        public override bool TryGetTermScalar(int id, out T value)
        {
            if (ScalarsDictionary.TryGetValue(id, out value))
                return true;

            value = ScalarDomain.GetZero();
            return false;
        }


        public override IGaMultivectorStorage<T> SetTermScalar(int id, T value)
        {
            if (ScalarsDictionary.ContainsKey(id))
                ScalarsDictionary[id] = value;
            else
                ScalarsDictionary.Add(id, value);

            return this;
        }

        public override IGaMultivectorStorage<T> AddTerm(int id, T value)
        {
            Debug.Assert(!ReferenceEquals(value, null));
            
            if (ScalarsDictionary.TryGetValue(id, out var oldValue))
                ScalarsDictionary[id] = ScalarDomain.Add(oldValue, value);
            else
                ScalarsDictionary.Add(id, value);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerm(int basisBladeId)
        {
            ScalarsDictionary.Remove(basisBladeId);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            var idsList = ScalarsDictionary
                .Keys
                .Where(id => id.BasisBladeGrade() == grade)
                .ToArray();

            foreach (var id in idsList)
                ScalarsDictionary.Remove(id);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int id, bool nearZeroFlag = false)
        {
            if (!ScalarsDictionary.TryGetValue(id, out var scalar))
                return this;

            if (ScalarDomain.IsNotZero(scalar, nearZeroFlag))
                return this;

            ScalarsDictionary.Remove(id);
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            var idsList =
                nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key)
                : ScalarsDictionary
                    .Where(p => ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key);

            ScalarsDictionary.Remove(idsList);
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            var idsList = nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => p.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key)
                : ScalarsDictionary
                    .Where(p => p.Key.BasisBladeGrade() == grade && ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key);

            ScalarsDictionary.Remove(idsList);
            
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
                ? ScalarsDictionary.Values.All(scalar => ScalarDomain.IsNearZero(scalar))
                : ScalarsDictionary.Values.All(scalar => ScalarDomain.IsZero(scalar));
        }

        public override bool ContainsStoredTerm(int id)
        {
            return ScalarsDictionary.ContainsKey(id);
        }


        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return ScalarsDictionary.Keys.Any(id => id.BasisBladeGrade() == grade);
        }

        public override bool CanStoreTerm(int id)
        {
            return id >= 0 && id < GaSpaceDimension;
        }

        public override bool CanStoreSomeTermsOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension;
        }

        public override bool CanStoreAllTermsOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension;
        }


        public override GaBinaryTree<T> GetBinaryTree()
        {
            return new GaBinaryTree<T>(VSpaceDimension, ScalarsDictionary);
        }

        public override GaMvsTerm<T> GetTermStorage(int id, bool getCopy = false)
        {
            var storage = new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, id);

            if (ScalarsDictionary.TryGetValue(id, out var scalar))
                storage.Scalar = scalar;

            return storage;
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            var termsList = 
                ScalarsDictionary.Where(p => p.Key.BasisBladeGrade() == 1);

            foreach (var term in termsList)
                storage.ScalarsArray[term.Key.BasisBladeIndex()] = term.Value;
            
            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            return GetSparseKVectorStorage(grade);
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var termsList = 
                ScalarsDictionary.Where(p => p.Key.BasisBladeGrade() == grade);

            foreach (var term in termsList)
                storage.ScalarsArray[term.Key.BasisBladeIndex()] = term.Value;
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var termsList = 
                ScalarsDictionary.Where(p => p.Key.BasisBladeGrade() == grade);

            foreach (var term in termsList)
                storage.ScalarsDictionary.Add(term.Key.BasisBladeIndex(), term.Value);
            
            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            var gradesList = GetStoredGrades().ToArray();
            
            var kVectorsArray = new GaMvsSparseKVector<T>[VSpaceDimension];
            
            foreach (var grade in gradesList)
                kVectorsArray[grade] = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            foreach (var term in ScalarsDictionary)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);
                
                kVectorsArray[grade].ScalarsDictionary.Add(index, term.Value);
            }
            
            return kVectorsArray.Where(v => !ReferenceEquals(v, null));
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            return new GaMvsBinaryTree<T>(
                VSpaceDimension,
                ScalarDomain,
                ScalarsDictionary
            );
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            foreach (var term in ScalarsDictionary)
                storage.ScalarsArray[term.Key] = term.Value;
            
            return storage;
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            if (!getCopy)
                return this;

            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            var termsList = 
                ScalarsDictionary.Where(term => ScalarDomain.IsNotZero(term.Value));
            
            foreach (var term in termsList)
                storage.ScalarsDictionary.Add(term.Key, term.Value);
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            var gradesList = GetStoredGrades().ToArray();
            
            foreach (var grade in gradesList)
            {
                var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
                
                storage.GradedScalarsArrays[grade] = new T[kvSpaceDimension];
            }

            foreach (var term in ScalarsDictionary)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);

                storage.GradedScalarsArrays[grade][index] = term.Value;
            }
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            foreach (var term in ScalarsDictionary)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);

                storage.GradedScalarsDictionaries[grade].Add(index, term.Value);
            }
            
            return storage;
        }

        public override IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyReverse()
        {
            var idsList = ScalarsDictionary
                .Keys
                .Where(id => id.GradeHasNegativeReverse());

            foreach (var id in idsList)
                ScalarsDictionary[id] = ScalarDomain.Negative(ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            var idsList = ScalarsDictionary
                .Keys
                .Where(id => id.GradeHasNegativeGradeInv());

            foreach (var id in idsList)
                ScalarsDictionary[id] = ScalarDomain.Negative(ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            var idsList = ScalarsDictionary
                .Keys
                .Where(id => id.GradeHasNegativeCliffConj());

            foreach (var id in idsList)
                ScalarsDictionary[id] = ScalarDomain.Negative(ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            var idsList = ScalarsDictionary.Keys;

            foreach (var id in idsList)
                ScalarsDictionary[id] = ScalarDomain.Negative(ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            var idsList = ScalarsDictionary.Keys;

            foreach (var id in idsList)
                ScalarsDictionary[id] = ScalarDomain.Times(scalingFactor, ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            var idsList = ScalarsDictionary.Keys;

            foreach (var id in idsList)
                ScalarsDictionary[id] = mappingFunc(ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, T, T> mappingFunc)
        {
            var idsList = ScalarsDictionary.Keys;

            foreach (var id in idsList)
                ScalarsDictionary[id] = mappingFunc(id, ScalarsDictionary[id]);

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, int, T, T> mappingFunc)
        {
            var idsList = ScalarsDictionary.Keys;

            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                
                ScalarsDictionary[id] = mappingFunc(grade, index, ScalarsDictionary[id]);
            }

            return this;
        }

        public override IEnumerable<int> GetStoredTermIds()
        {
            return ScalarsDictionary.Keys;
        }

        public override IEnumerable<int> GetStoredTermIds(Func<T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(p.Value))
                .Select(p => p.Key);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p =>
                {
                    p.Key.BasisBladeGradeIndex(out var grade, out var index);

                    return selectionFilter(grade, index);
                })
                .Select(p => p.Key);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(p.Key, p.Value))
                .Select(p => p.Key);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p =>
                {
                    p.Key.BasisBladeGradeIndex(out var grade, out var index);

                    return selectionFilter(grade, index, p.Value);
                })
                .Select(p => p.Key);
        }

        public override IEnumerable<int> GetStoredTermIdsOfGrade(int grade)
        {
            return ScalarsDictionary
                .Keys
                .Where(id => id.BasisBladeGrade() == grade);
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return ScalarsDictionary.Values;
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(p.Key))
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p => selectionFilter(p.Key, p.Value))
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsDictionary
                .Where(p =>
                {
                    p.Key.BasisBladeGradeIndex(out var grade, out var index);
                    
                    return selectionFilter(grade, index, p.Value);
                })
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            return ScalarsDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade)
                .Select(p => p.Value);
        }

        public override IEnumerable<T> GetNonZeroTermScalars(bool nearZeroFlag = false)
        {
            return ScalarsDictionary.Values.Where(v => !ScalarDomain.IsZero(v));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            return ScalarsDictionary.Select(
                p => new GaUniformTerm<T>(p.Key, p.Value)
            );
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            return ScalarsDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade)
                .Select(
                    p => new GaUniformTerm<T>(p.Key, p.Value)
                );
        }

        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsDictionary
                    .Values
                    .Count(scalar => ScalarDomain.IsNearZero(scalar))
                : ScalarsDictionary
                    .Values
                    .Count(scalar => ScalarDomain.IsZero(scalar));
        }

        public override IEnumerable<int> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => p.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key)
                : ScalarsDictionary
                    .Where(p => p.Key.BasisBladeGrade() == grade && ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key);
        }

        public override IEnumerable<int> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsDictionary
                    .Where(p => p.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(p.Value))
                    .Select(p => p.Key.BasisBladeIndex())
                : ScalarsDictionary
                    .Where(p => p.Key.BasisBladeGrade() == grade && ScalarDomain.IsZero(p.Value))
                    .Select(p => p.Key.BasisBladeIndex());
        }

        public override int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsDictionary
                    .Values
                    .Count(scalar => ScalarDomain.IsNotNearZero(scalar))
                : ScalarsDictionary
                    .Values
                    .Count(scalar => ScalarDomain.IsNotZero(scalar));
        }

        public override IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTerms(bool nearZeroFlag = false)
        {
            return ScalarsDictionary
                .Where(p => !ScalarDomain.IsZero(p.Value))
                .Select(
                    p => new GaUniformTerm<T>(p.Key, p.Value)
                );
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return ScalarsDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade && !ScalarDomain.IsZero(p.Value))
                .Select(
                    p => new GaUniformTerm<T>(p.Key, p.Value)
                );
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            return ScalarsDictionary
                .Keys
                .Select(id => id.BasisBladeGrade())
                .Distinct();
        }

        public override IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<int, T>>)ScalarsDictionary).GetEnumerator();
        }
    }
}