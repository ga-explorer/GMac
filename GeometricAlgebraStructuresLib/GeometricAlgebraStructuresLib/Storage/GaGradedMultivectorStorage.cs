using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public abstract class GaGradedMultivectorStorage<T> : IGaMultivectorStorage<T>
    {
        public IGaScalarDomain<T> ScalarDomain { get; }
        
        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << VSpaceDimension;
        
        public abstract int StoredTermsCount { get; }
        

        protected GaGradedMultivectorStorage(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
        {
            Debug.Assert(vSpaceDimension.IsValidVSpaceDimension());
            
            VSpaceDimension = vSpaceDimension;
            ScalarDomain = scalarDomain;
        }
        
        
        public virtual T GetTermScalar(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GetTermScalar(grade, index);
        }

        public abstract T GetTermScalar(int grade, ulong index);

        
        public virtual IGaTerm<T> GetTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return new GaGradedTerm<T>(
                grade, 
                index, 
                GetTermScalar(grade, index)
            );
        }

        public virtual IGaTerm<T> GetTerm(int grade, ulong index)
        {
            return new GaGradedTerm<T>(
                grade, 
                index, 
                GetTermScalar(grade, index)
            );
        }

        
        public virtual bool TryGetTermScalar(ulong id, out T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return TryGetTermScalar(grade, index, out value);
        }

        public abstract bool TryGetTermScalar(int grade, ulong index, out T value);

        
        public virtual bool TryGetTerm(ulong id, out IGaTerm<T> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (TryGetTermScalar(grade, index, out var scalar))
            {
                term = new GaGradedTerm<T>(grade, index, scalar);
                return true;
            }
            
            term = null;
            return false;
        }

        public virtual bool TryGetTerm(int grade, ulong index, out IGaTerm<T> term)
        {
            if (TryGetTermScalar(grade, index, out var scalar))
            {
                term = new GaGradedTerm<T>(grade, index, scalar);
                return true;
            }
            
            term = null;
            return false;
        }

        
        public virtual IGaMultivectorStorage<T> SetTermScalar(ulong id, T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return SetTermScalar(grade, index, value);
        }

        public abstract IGaMultivectorStorage<T> SetTermScalar(int grade, ulong index, T value);

        
        public virtual IGaMultivectorStorage<T> SetTerms(IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                SetTermScalar(
                    term.Grade, 
                    term.Index, 
                    term.Scalar
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                SetTermScalar(
                    term.Grade, 
                    term.Index, 
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );
            
            return this;
        }

        
        public virtual bool TrySetTermScalar(ulong id, T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return TrySetTermScalar(grade, index, value);
        }

        public virtual bool TrySetTermScalar(int grade, ulong index, T value)
        {
            if (!CanStoreTerm(grade, index)) 
                return false;
            
            SetTermScalar(grade, index, value);
            
            return true;
        }

        
        public virtual IGaMultivectorStorage<T> SetKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                SetTermScalar(
                    grade, 
                    (ulong)index, 
                    scalarValuesList[index]
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                SetTermScalar(grade, 
                    (ulong)index, 
                    ScalarDomain.Times(scalingFactor, scalarValuesList[index])
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(int grade,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTermScalar(
                    grade, 
                    pair.Key, 
                    pair.Value
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTermScalar(
                    grade, 
                    pair.Key, 
                    ScalarDomain.Times(scalingFactor, pair.Value)
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(IGaKVectorStorage<T> kvector)
        {
            var termsList = kvector.GetStoredTerms();

            foreach (var term in termsList)
                SetTermScalar(
                    term.Grade, 
                    term.Index, 
                    term.Scalar
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            var termsList = kvector.GetStoredTerms();

            foreach (var term in termsList)
                SetTermScalar(
                    term.Grade, 
                    term.Index, 
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVectors(IEnumerable<IGaKVectorStorage<T>> kVectorsList)
        {
            foreach (var kVector in kVectorsList)
                SetKVector(kVector);

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVectors(IEnumerable<Tuple<T, IGaKVectorStorage<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                SetKVector(scalingFactor, kVector);

            return this;
        }

        
        public virtual IGaMultivectorStorage<T> AddTerm(ulong id, T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return AddTerm(grade, index, value);
        }

        public abstract IGaMultivectorStorage<T> AddTerm(int grade, ulong index, T value);

        public virtual IGaMultivectorStorage<T> AddTerm(IGaTerm<T> term)
        {
            return AddTerm(
                term.Grade, 
                term.Index, 
                term.Scalar
            );
        }

        public virtual IGaMultivectorStorage<T> AddTerm(T scalingFactor, IGaTerm<T> term)
        {
            return AddTerm(
                term.Grade, 
                term.Index, 
                ScalarDomain.Times(scalingFactor, term.Scalar)
            );
        }

        public virtual IGaMultivectorStorage<T> AddTerms(IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(
                    term.Grade, 
                    term.Index, 
                    term.Scalar
                );
            
            return this;
        }

        public virtual IGaMultivectorStorage<T> AddTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(
                    term.Grade, 
                    term.Index, 
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );
            
            return this;
        }

        
        public virtual bool TryAddTerm(ulong id, T value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return TryAddTerm(grade, index, value);
        }

        public virtual bool TryAddTerm(int grade, ulong index, T value)
        {
            if (!CanStoreTerm(grade, index)) 
                return false;
            
            AddTerm(grade, index, value);
            
            return true;
        }

        
        public virtual IGaMultivectorStorage<T> AddKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                AddTerm(
                    grade,
                    (ulong)index,
                    scalarValuesList[index]
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                AddTerm(
                    grade,
                    (ulong)index,
                    ScalarDomain.Times(scalingFactor, scalarValuesList[index])
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(int grade, IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(
                    grade,
                    pair.Key,
                    pair.Value
                );
            
            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(
                    grade,
                    pair.Key,
                    ScalarDomain.Times(scalingFactor, pair.Value)
                );
            
            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(IGaKVectorStorage<T> kvector)
        {
            foreach (var term in kvector.GetStoredTerms())
                AddTerm(
                    term.Grade, 
                    term.Index, 
                    term.Scalar
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            foreach (var term in kvector.GetStoredTerms())
                AddTerm(
                    term.Grade, 
                    term.Index, 
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVectors(IEnumerable<IGaKVectorStorage<T>> kVectorsList)
        {
            foreach (var kVector in kVectorsList)
                AddKVector(kVector);

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVectors(IEnumerable<Tuple<T, IGaKVectorStorage<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                AddKVector(scalingFactor, kVector);

            return this;
        }
        
        public virtual IGaMultivectorStorage<T> RemoveTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return RemoveTerm(grade, index);
        }

        
        public abstract IGaMultivectorStorage<T> RemoveTerm(int grade, ulong index);

        public virtual IGaMultivectorStorage<T> RemoveTerms(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                RemoveTerm(grade, index);
            }

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(int grade, IEnumerable<ulong> indexList)
        {
            foreach (var index in indexList)
                RemoveTerm(grade, index);
            
            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<T, bool> selectionFilter)
        {
            var gradeIndicesList = 
                GetStoredTermGradeIndices(selectionFilter).ToArray();

            foreach (var (grade, index) in gradeIndicesList)
                RemoveTerm(grade, index);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<ulong, bool> selectionFilter)
        {
            var gradeIndicesList = 
                GetStoredTermGradeIndices(selectionFilter).ToArray();

            foreach (var (grade, index) in gradeIndicesList)
                RemoveTerm(grade, index);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<int, ulong, bool> selectionFilter)
        {
            var gradeIndicesList = 
                GetStoredTermGradeIndices(selectionFilter).ToArray();

            foreach (var (grade, index) in gradeIndicesList)
                RemoveTerm(grade, index);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<ulong, T, bool> selectionFilter)
        {
            var gradeIndicesList = 
                GetStoredTermGradeIndices(selectionFilter).ToArray();

            foreach (var (grade, index) in gradeIndicesList)
                RemoveTerm(grade, index);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<int, ulong, T, bool> selectionFilter)
        {
            var gradeIndicesList = 
                GetStoredTermGradeIndices(selectionFilter).ToArray();

            foreach (var (grade, index) in gradeIndicesList)
                RemoveTerm(grade, index);

            return this;
        }

        public abstract IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade);

        
        public virtual IGaMultivectorStorage<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return RemoveTermIfZero(grade, index, nearZeroFlag);
        }

        public abstract IGaMultivectorStorage<T> RemoveTermIfZero(int grade, ulong index, bool nearZeroFlag = false);

        public abstract IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false);

        public abstract IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false);

        public abstract IGaMultivectorStorage<T> ResetToZero();

        
        public abstract bool IsEmpty();

        public abstract bool IsZero(bool nearZeroFlag = false);

        
        public virtual bool ContainsStoredTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return ContainsStoredTerm(grade, index);
        }

        public abstract bool ContainsStoredTerm(int grade, ulong index);

        public abstract bool ContainsStoredTermOfGrade(int grade);

        
        public virtual bool CanStoreTerm(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return CanStoreTerm(grade, index);
        }

        public abstract bool CanStoreTerm(int grade, ulong index);

        public abstract bool CanStoreSomeTermsOfGrade(int grade);

        public abstract bool CanStoreAllTermsOfGrade(int grade);

        
        public virtual IEnumerable<ulong> GetStoredTermIds()
        {
            return GetStoredTermGradeIndices()
                .Select(t =>
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public virtual IEnumerable<ulong> GetStoredTermIds(Func<T, bool> selectionFilter)
        {
            return GetStoredTermGradeIndices(selectionFilter)
                .Select(t =>
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public virtual IEnumerable<ulong> GetStoredTermIds(Func<ulong, bool> selectionFilter)
        {
            return GetStoredTermGradeIndices(selectionFilter)
                .Select(t =>
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public virtual IEnumerable<ulong> GetStoredTermIds(Func<int, ulong, bool> selectionFilter)
        {
            return GetStoredTermGradeIndices(selectionFilter)
                .Select(t =>
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public virtual IEnumerable<ulong> GetStoredTermIds(Func<ulong, T, bool> selectionFilter)
        {
            return GetStoredTermGradeIndices(selectionFilter)
                .Select(t =>
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public virtual IEnumerable<ulong> GetStoredTermIds(Func<int, ulong, T, bool> selectionFilter)
        {
            return GetStoredTermGradeIndices(selectionFilter)
                .Select(t =>
                    GaFrameUtils.BasisBladeId(t.Item1, t.Item2)
                );
        }

        public virtual IEnumerable<ulong> GetStoredTermIdsOfGrade(int grade)
        {
            return GetStoredTermIndicesOfGrade(grade)
                .Select(index =>
                    GaFrameUtils.BasisBladeId(grade, index)
                );
        }

        
        public abstract IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices();

        public abstract IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter);

        public abstract IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, bool> selectionFilter);

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<int, ulong, bool> selectionFilter)
        {
            return GetStoredTermGradeIndices()
                .Where(t => selectionFilter(t.Item1, t.Item2));
        }

        public abstract IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, T, bool> selectionFilter);

        public abstract IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(
            Func<int, ulong, T, bool> selectionFilter);

        public abstract IEnumerable<ulong> GetStoredTermIndicesOfGrade(int grade);


        public abstract IEnumerable<T> GetStoredTermScalars();

        public virtual IEnumerable<T> GetStoredTermScalars(Func<T, bool> selectionFilter)
        {
            return GetStoredTermScalars().Where(selectionFilter);
        }

        public abstract IEnumerable<T> GetStoredTermScalars(Func<ulong, bool> selectionFilter);

        public abstract IEnumerable<T> GetStoredTermScalars(Func<ulong, T, bool> selectionFilter);

        public abstract IEnumerable<T> GetStoredTermScalars(Func<int, ulong, T, bool> selectionFilter);

        public abstract IEnumerable<T> GetStoredTermScalarsOfGrade(int grade);


        public abstract IEnumerable<IGaTerm<T>> GetStoredTerms();

        public virtual IEnumerable<IGaTerm<T>> GetStoredTerms(Func<T, bool> selectionFilter)
        {
            return GetStoredTerms().Where(t => 
                selectionFilter(t.Scalar)
            );
        }

        public virtual IEnumerable<IGaTerm<T>> GetStoredTerms(Func<ulong, bool> selectionFilter)
        {
            return GetStoredTerms().Where(t => 
                selectionFilter(t.Id)
            );
        }

        public virtual IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, ulong, bool> selectionFilter)
        {
            return GetStoredTerms().Where(t => 
                selectionFilter(t.Grade, t.Index)
            );
        }

        public virtual IEnumerable<IGaTerm<T>> GetStoredTerms(Func<ulong, T, bool> selectionFilter)
        {
            return GetStoredTerms().Where(t => 
                selectionFilter(t.Id, t.Scalar)
            );
        }

        public virtual IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, ulong, T, bool> selectionFilter)
        {
            return GetStoredTerms().Where(t => 
                selectionFilter(t.Grade, t.Index, t.Scalar)
            );
        }

        public abstract IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade);

        public abstract int GetStoredZeroTermsCount(bool nearZeroFlag = false);


        public abstract IEnumerable<int> GetStoredGrades();

        public virtual ulong GetStoredGradesBitPattern()
        {
            var gradesList = GetStoredGrades();

            return gradesList.Aggregate(
                0UL, 
                (current, grade) => current | (1UL << grade)
            );
        }

        
        public virtual IEnumerable<ulong> GetStoredZeroTermIds(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GetStoredTermIds(ScalarDomain.IsNearZero)
                : GetStoredTermIds(ScalarDomain.IsZero);
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredZeroTermGradeIndices(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GetStoredTermGradeIndices(ScalarDomain.IsNearZero)
                : GetStoredTermGradeIndices(ScalarDomain.IsZero);
        }

        public abstract IEnumerable<ulong> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false);

        public abstract IEnumerable<ulong> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false);

        public abstract int GetNonZeroTermsCount(bool nearZeroFlag = false);

        public abstract IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false);


        public virtual IEnumerable<ulong> GetNonZeroTermIds(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GetStoredTermIds(ScalarDomain.IsNotNearZero)
                : GetStoredTermIds(ScalarDomain.IsNotZero);
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetNonZeroTermGradeIndices(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GetStoredTermGradeIndices(ScalarDomain.IsNotNearZero)
                : GetStoredTermGradeIndices(ScalarDomain.IsNotZero);
        }

        public virtual IEnumerable<T> GetNonZeroTermScalars(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GetStoredTermScalars(ScalarDomain.IsNotNearZero)
                : GetStoredTermScalars(ScalarDomain.IsNotZero);
        }

        public virtual IEnumerable<IGaTerm<T>> GetNonZeroTerms(bool nearZeroFlag = false)
        {
            return nearZeroFlag 
                ? GetStoredTerms(ScalarDomain.IsNotNearZero)
                : GetStoredTerms(ScalarDomain.IsNotZero);
        }

        public abstract IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false);

        
        public abstract GaBinaryTree<T> GetBinaryTree();

        public virtual GaMvsTerm<T> GetTermStorage(ulong id, bool getCopy = false)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return GetTermStorage(grade, index);
        }

        public abstract GaMvsTerm<T> GetTermStorage(int grade, ulong index, bool getCopy = false);

        public abstract GaMvsVector<T> GetVectorStorage(bool getCopy = false);

        public abstract IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false);

        public abstract GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false);

        public abstract GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false);

        public abstract IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false);

        public abstract GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false);

        public abstract GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false);

        public abstract GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false);

        public abstract GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false);

        public abstract GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false);

        public abstract IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false);

        
        public abstract IGaMultivectorStorage<T> ApplyReverse();

        public abstract IGaMultivectorStorage<T> ApplyGradeInv();

        public abstract IGaMultivectorStorage<T> ApplyCliffConj();

        public abstract IGaMultivectorStorage<T> ApplyNegative();

        public abstract IGaMultivectorStorage<T> ApplyScaling(T scalingFactor);

        public abstract IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc);

        public abstract IGaMultivectorStorage<T> ApplyMapping(Func<ulong, T, T> mappingFunc);

        public abstract IGaMultivectorStorage<T> ApplyMapping(Func<int, ulong, T, T> mappingFunc);


        public abstract IEnumerator<KeyValuePair<ulong, T>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}