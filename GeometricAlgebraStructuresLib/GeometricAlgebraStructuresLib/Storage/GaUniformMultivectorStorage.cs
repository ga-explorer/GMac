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
    public abstract class GaUniformMultivectorStorage<T> : IGaMultivectorStorage<T>
    {
        public IGaScalarDomain<T> ScalarDomain { get; }
        
        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << VSpaceDimension;
        
        public abstract int StoredTermsCount { get; }


        protected GaUniformMultivectorStorage(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
        {
            Debug.Assert(vSpaceDimension.IsValidVSpaceDimension());
            
            VSpaceDimension = vSpaceDimension;
            ScalarDomain = scalarDomain;
        }


        public abstract T GetTermScalar(ulong id);

        public virtual T GetTermScalar(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return GetTermScalar(id);
        }

        
        public virtual IGaTerm<T> GetTerm(ulong id)
        {
            return new GaUniformTerm<T>(id, GetTermScalar(id));
        }

        public virtual IGaTerm<T> GetTerm(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return new GaUniformTerm<T>(id, GetTermScalar(id));
        }


        public abstract bool TryGetTermScalar(ulong id, out T value);

        public virtual bool TryGetTermScalar(int grade, ulong index, out T value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return TryGetTermScalar(id, out value);
        }
        
        
        public virtual bool TryGetTerm(ulong id, out IGaTerm<T> term)
        {
            if (TryGetTermScalar(id, out var value))
            {
                term = new GaUniformTerm<T>(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public virtual bool TryGetTerm(int grade, ulong index, out IGaTerm<T> term)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);
            
            if (TryGetTermScalar(id, out var value))
            {
                term = new GaUniformTerm<T>(id, value);
                return true;
            }

            term = null;
            return false;
        }

        
        public abstract IGaMultivectorStorage<T> SetTermScalar(ulong id, T value);
        
        public virtual IGaMultivectorStorage<T> SetTermScalar(int grade, ulong index, T value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return SetTermScalar(id, value);
        }

        
        public virtual IGaMultivectorStorage<T> SetTerms(IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                SetTermScalar(term.Id, term.Scalar);

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                SetTermScalar(
                    term.Id, 
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );

            return this;
        }

        
        public virtual bool TrySetTermScalar(ulong id, T value)
        {
            if (!CanStoreTerm(id)) 
                return false;
            
            SetTermScalar(id, value);
            
            return true;
        }

        public virtual bool TrySetTermScalar(int grade, ulong index, T value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (!CanStoreTerm(id)) 
                return false;
            
            SetTermScalar(id, value);
            
            return true;
        }

        
        public virtual IGaMultivectorStorage<T> SetKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            var index = 0UL;
            foreach (var scalar in scalarValuesList)
            {
                SetTermScalar(grade, index, scalar);
                
                index++;
            }

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            var index = 0UL;
            foreach (var scalar in scalarValuesList)
            {
                SetTermScalar(grade, index, ScalarDomain.Times(scalingFactor, scalar));
                
                index++;
            }

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(int grade,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTermScalar(grade, pair.Key, pair.Value);

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTermScalar(grade, pair.Key, ScalarDomain.Times(scalingFactor, pair.Value));

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(IGaKVectorStorage<T> kvector)
        {
            foreach (var pair in kvector)
                SetTermScalar(pair.Key, pair.Value);

            return this;
        }

        public virtual IGaMultivectorStorage<T> SetKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            foreach (var pair in kvector)
                SetTermScalar(pair.Key, ScalarDomain.Times(scalingFactor, pair.Value));

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

        
        public abstract IGaMultivectorStorage<T> AddTerm(ulong id, T value);
        
        public virtual IGaMultivectorStorage<T> AddTerm(int grade, ulong index, T value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return AddTerm(id, value);
        }

        public virtual IGaMultivectorStorage<T> AddTerm(IGaTerm<T> term)
        {
            return AddTerm(term.Id, term.Scalar);
        }

        public virtual IGaMultivectorStorage<T> AddTerm(T scalingFactor, IGaTerm<T> term)
        {
            AddTerm(
                term.Id, 
                ScalarDomain.Times(scalingFactor, term.Scalar)
            );

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddTerms(IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term.Id, term.Scalar);

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(
                    term.Id, 
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );

            return this;
        }

        
        public virtual bool TryAddTerm(ulong id, T value)
        {
            if (!CanStoreTerm(id)) 
                return false;
            
            AddTerm(id, value);
            
            return true;
        }

        public virtual bool TryAddTerm(int grade, ulong index, T value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (!CanStoreTerm(id)) 
                return false;
            
            AddTerm(id, value);
            
            return true;
        }

        
        public virtual IGaMultivectorStorage<T> AddKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            var index = 0UL;
            foreach (var scalar in scalarValuesList)
            {
                AddTerm(grade, index, scalar);
                
                index++;
            }

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            var index = 0UL;
            foreach (var scalar in scalarValuesList)
            {
                AddTerm(grade, index, ScalarDomain.Times(scalingFactor, scalar));
                
                index++;
            }

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(int grade,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(grade, pair.Key, pair.Value);

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(grade, pair.Key, ScalarDomain.Times(scalingFactor, pair.Value));

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(IGaKVectorStorage<T> kvector)
        {
            foreach (var pair in kvector)
                AddTerm(pair.Key, pair.Value);

            return this;
        }

        public virtual IGaMultivectorStorage<T> AddKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            foreach (var pair in kvector)
                AddTerm(pair.Key, ScalarDomain.Times(scalingFactor, pair.Value));

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

        
        public abstract IGaMultivectorStorage<T> RemoveTerm(ulong id);

        public virtual IGaMultivectorStorage<T> RemoveTerm(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return RemoveTerm(id);
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(IEnumerable<ulong> idsList)
        {
            foreach (var id in idsList)
                RemoveTerm(id);

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
            var idsList = 
                GetStoredTermIds(selectionFilter).ToArray();

            foreach (var id in idsList)
                RemoveTerm(id);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<ulong, bool> selectionFilter)
        {
            var idsList = 
                GetStoredTermIds(selectionFilter).ToArray();

            foreach (var id in idsList)
                RemoveTerm(id);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<int, ulong, bool> selectionFilter)
        {
            var idsList = 
                GetStoredTermIds(selectionFilter).ToArray();

            foreach (var id in idsList)
                RemoveTerm(id);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<ulong, T, bool> selectionFilter)
        {
            var idsList = 
                GetStoredTermIds(selectionFilter).ToArray();

            foreach (var id in idsList)
                RemoveTerm(id);

            return this;
        }

        public virtual IGaMultivectorStorage<T> RemoveTerms(Func<int, ulong, T, bool> selectionFilter)
        {
            var idsList = 
                GetStoredTermIds(selectionFilter).ToArray();

            foreach (var id in idsList)
                RemoveTerm(id);

            return this;
        }
        
        public abstract IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade);

        
        public abstract IGaMultivectorStorage<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false);

        public virtual IGaMultivectorStorage<T> RemoveTermIfZero(int grade, ulong index, bool nearZeroFlag = false)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return RemoveTermIfZero(id);
        }
        
        public abstract IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false);
        
        public abstract IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false);

        public abstract IGaMultivectorStorage<T> ResetToZero();

        
        public abstract bool IsEmpty();

        public abstract bool IsZero(bool nearZeroFlag = false);

        
        public abstract bool ContainsStoredTerm(ulong id);

        public virtual bool ContainsStoredTerm(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return ContainsStoredTerm(id);
        }

        public abstract bool ContainsStoredTermOfGrade(int grade);

        
        public abstract bool CanStoreTerm(ulong id);

        public virtual bool CanStoreTerm(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return CanStoreTerm(id);
        }

        public abstract bool CanStoreSomeTermsOfGrade(int grade);

        public abstract bool CanStoreAllTermsOfGrade(int grade);

        
        public abstract IEnumerable<ulong> GetStoredTermIds();

        public abstract IEnumerable<ulong> GetStoredTermIds(Func<T, bool> selectionFilter);

        public virtual IEnumerable<ulong> GetStoredTermIds(Func<ulong, bool> selectionFilter)
        {
            return GetStoredTermIds().Where(selectionFilter);
        }

        public abstract IEnumerable<ulong> GetStoredTermIds(Func<int, ulong, bool> selectionFilter);

        public abstract IEnumerable<ulong> GetStoredTermIds(Func<ulong, T, bool> selectionFilter);
        
        public abstract IEnumerable<ulong> GetStoredTermIds(Func<int, ulong, T, bool> selectionFilter);

        public abstract IEnumerable<ulong> GetStoredTermIdsOfGrade(int grade);

        
        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices()
        {
            return GetStoredTermIds().Select(id =>
            {
                id.BasisBladeGradeIndex(out var grade, out var index);
                
                return new Tuple<int, ulong>(grade, index);
            });
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter)
        {
            return GetStoredTermIds(selectionFilter).Select(
                id =>
                {
                    id.BasisBladeGradeIndex(out var grade, out var index);

                    return new Tuple<int, ulong>(grade, index);
                }
            );
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, bool> selectionFilter)
        {
            return GetStoredTermIds(selectionFilter).Select(
                id =>
                {
                    id.BasisBladeGradeIndex(out var grade, out var index);

                    return new Tuple<int, ulong>(grade, index);
                }
            );
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<int, ulong, bool> selectionFilter)
        {
            return GetStoredTermIds(selectionFilter).Select(
                id =>
                {
                    id.BasisBladeGradeIndex(out var grade, out var index);

                    return new Tuple<int, ulong>(grade, index);
                }
            );
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, T, bool> selectionFilter)
        {
            return GetStoredTermIds(selectionFilter).Select(
                id =>
                {
                    id.BasisBladeGradeIndex(out var grade, out var index);

                    return new Tuple<int, ulong>(grade, index);
                }
            );
        }

        public virtual IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(
            Func<int, ulong, T, bool> selectionFilter)
        {
            return GetStoredTermIds(selectionFilter).Select(
                id =>
                {
                    id.BasisBladeGradeIndex(out var grade, out var index);

                    return new Tuple<int, ulong>(grade, index);
                }
            );
        }

        public virtual IEnumerable<ulong> GetStoredTermIndicesOfGrade(int grade)
        {
            return GetStoredTermIdsOfGrade(grade)
                .Select(id => id.BasisBladeIndex());
        }

        
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

        public abstract GaMvsTerm<T> GetTermStorage(ulong id, bool getCopy = false);

        public virtual GaMvsTerm<T> GetTermStorage(int grade, ulong index, bool getCopy = false)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return GetTermStorage(id, getCopy);
        }

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

        public virtual IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            var termsCountDictionary = GetNonZeroTermsCountPerGrade();
            var termsCount = termsCountDictionary.Values.Sum();
            var gradesCount = termsCountDictionary.Count;
            
            if (termsCount == 0)
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, 0);

            if (termsCount == 1)
            {
                var term = GetNonZeroTerms().First();
                
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, term.Id)
                {
                    Scalar = term.Scalar
                };
            }

            if ((ulong)termsCount == GaSpaceDimension)
                return GetDenseArrayStorage(getCopy);

            if (gradesCount == 1)
            {
                
            }

            return GetSparseGradedStorage(getCopy);
        }


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