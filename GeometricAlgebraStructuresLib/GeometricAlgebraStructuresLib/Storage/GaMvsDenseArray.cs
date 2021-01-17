using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public class GaMvsDenseArray<T> 
        : GaUniformMultivectorStorage<T>
    {
        public T[] ScalarsArray { get; protected set; }

        public override int StoredTermsCount 
            => 1 << VSpaceDimension;


        public GaMvsDenseArray(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsArray = new T[1 << vSpaceDimension];
        }

        internal GaMvsDenseArray(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, T[] scalarValues)
            : base(vSpaceDimension, scalarDomain)
        {
            Debug.Assert(scalarValues.Length == 1 << vSpaceDimension);
            
            ScalarsArray = scalarValues;
        }


        public override T GetTermScalar(int basisBladeId)
        {
            return ScalarsArray[basisBladeId];
        }

        public override bool TryGetTermScalar(int id, out T value)
        {
            if (id >= 0 && id < GaSpaceDimension)
            {
                value = ScalarsArray[id];
                return true;
            }

            value = default;
            return false;
        }

        public override IGaMultivectorStorage<T> SetTermScalar(int id, T value)
        {
            ScalarsArray[id] = value;

            return this;
        }

        public override IGaMultivectorStorage<T> AddTerm(int id, T value)
        {
            ScalarsArray[id] = ScalarDomain.Add(ScalarsArray[id], value);

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTerm(int basisBladeId)
        {
            ScalarsArray[basisBladeId] = ScalarDomain.GetZero();

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            var idsList = 
                GaFrameUtils.BasisBladeIDsOfGrade(VSpaceDimension, grade);

            foreach (var id in idsList)
                ScalarsArray[id] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int id, bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsNearZero(ScalarsArray[id]))
                ScalarsArray[id] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                if (ScalarDomain.IsNearZero(ScalarsArray[id]))
                    ScalarsArray[id] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            var idsList = 
                GaFrameUtils.BasisBladeIDsOfGrade(VSpaceDimension, grade);

            foreach (var id in idsList)
                if (ScalarDomain.IsNearZero(ScalarsArray[id]))
                    ScalarsArray[id] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> ResetToZero()
        {
            ScalarsArray = Enumerable
                .Repeat(ScalarDomain.GetZero(), 1 << VSpaceDimension)
                .ToArray();

            return this;
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsArray.All(ScalarDomain.IsNearZero)
                : ScalarsArray.All(ScalarDomain.IsZero);
        }

        public override bool ContainsStoredTerm(int id)
        {
            return id >= 0 && id < GaSpaceDimension;
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension;
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
            return new GaBinaryTree<T>(
                VSpaceDimension, 
                Enumerable.Range(0, GaSpaceDimension).ToArray()
            );
        }

        public override GaMvsTerm<T> GetTermStorage(int id, bool getCopy = false)
        {
            return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, id)
            {
                Scalar = ScalarsArray[id]
            };
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            for (var index = 0; index < VSpaceDimension; index++)
                storage.ScalarsArray[index] = ScalarsArray[1 << index];
            
            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            return GetDenseKVectorStorage(grade);
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            
            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                
                storage.ScalarsArray[index] = ScalarsArray[id];
            }
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            
            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = ScalarsArray[id];
                
                if (ScalarDomain.IsNotZero(scalar))
                    storage.ScalarsDictionary.Add(index, scalar);
            }
            
            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                yield return GetSparseKVectorStorage(grade);
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            var termsDictionary = (IDictionary<int, T>)GetNonZeroTerms().ToDictionary(
                    t => t.Id,
                    t => t.Scalar
                );
            
            return new GaMvsBinaryTree<T>(
                VSpaceDimension,
                ScalarDomain,
                termsDictionary
            );
        }

        public override IEnumerable<int> GetStoredTermIds()
        {
            return Enumerable.Range(0, GaSpaceDimension);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<T, bool> selectionFilter)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                if (selectionFilter(scalar))
                    yield return id;
            }
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                for (var index = 0; index < kvSpaceDimension; index++)
                    if (selectionFilter(grade, index))
                        yield return GaFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, T, bool> selectionFilter)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                if (selectionFilter(id, scalar))
                    yield return id;
            }
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                for (var index = 0; index < kvSpaceDimension; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    var scalar = ScalarsArray[id];
                    
                    if (selectionFilter(grade, index, scalar))
                        yield return id;
                }
            }
        }

        public override IEnumerable<int> GetStoredTermIdsOfGrade(int grade)
        {
            var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            
            return Enumerable
                .Range(0, kvSpaceDimension)
                .Select(index => GaFrameUtils.BasisBladeId(grade, index));
        }

        public override IEnumerable<int> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                if (ScalarDomain.IsZero(scalar, nearZeroFlag))
                    yield return id;
            }
        }

        public override IEnumerable<int> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = ScalarsArray[id];
                    
                if (ScalarDomain.IsZero(scalar, nearZeroFlag))
                    yield return id;
            }
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

        public override IEnumerable<int> GetNonZeroTermIds(bool nearZeroFlag = false)
        {
            return Enumerable
                .Range(0, GaSpaceDimension)
                .Where(id => ScalarDomain.IsZero(ScalarsArray[id]));
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return ScalarsArray;
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, bool> selectionFilter)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                if (selectionFilter(id))
                    yield return ScalarsArray[id];
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, T, bool> selectionFilter)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                if (selectionFilter(id, scalar))
                    yield return scalar;
            }
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, int, T, bool> selectionFilter)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                for (var index = 0; index < kvSpaceDimension; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    var scalar = ScalarsArray[id];
                    
                    if (selectionFilter(grade, index, scalar))
                        yield return scalar;
                }
            }
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);
                var scalar = ScalarsArray[id];
                    
                yield return scalar;
            }
        }

        public override IEnumerable<T> GetNonZeroTermScalars(bool nearZeroFlag = false)
        {
            return ScalarsArray
                .Where(scalar => ScalarDomain.IsZero(scalar));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            return ScalarsArray.Select(
                (scalarValue, id) => new GaUniformTerm<T>(id, scalarValue)
            );
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            return GaFrameUtils
                .BasisBladeIDsOfGrade(VSpaceDimension, grade)
                .Select(id => new GaUniformTerm<T>(id, ScalarsArray[id]));
        }

        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsArray.Count(s => ScalarDomain.IsNearZero(s))
                : ScalarsArray.Count(s => ScalarDomain.IsZero(s));
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTerms(bool nearZeroFlag = false)
        {
            return ScalarsArray.Select(
                (scalarValue, id) => new GaUniformTerm<T>(id, scalarValue)
            ).Where(term => !ScalarDomain.IsZero(term.Scalar));
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return GaFrameUtils
                .BasisBladeIDsOfGrade(VSpaceDimension, grade)
                .Select(id => new GaUniformTerm<T>(id, ScalarsArray[id]))
                .Where(term => !ScalarDomain.IsZero(term.Scalar));
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            return Enumerable.Range(0, VSpaceDimension + 1);
        }

        public override int GetStoredGradesBitPattern()
        {
            return (1 << (VSpaceDimension + 1)) - 1;
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            if (!getCopy) 
                return this;
            
            var newScalarValues = new T[GaSpaceDimension];
                
            ScalarsArray.CopyTo(newScalarValues, 0);
                
            return new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain, newScalarValues);
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];
                
                if (ScalarDomain.IsNotZero(scalar))
                    storage.ScalarsDictionary.Add(id, scalar);
            }
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
                
                var scalarValuesArray = new T[kvSpaceDimension];

                for (var index = 0; index < kvSpaceDimension; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    
                    scalarValuesArray[index] = ScalarsArray[id];
                }

                storage.GradedScalarsArrays[grade] = scalarValuesArray;
            }
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDimension = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
                
                var scalarValuesDictionary = new Dictionary<int, T>();

                for (var index = 0; index < kvSpaceDimension; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    var scalar = ScalarsArray[id];
                    
                    if (ScalarDomain.IsNotZero(scalar))
                        scalarValuesDictionary.Add(index, scalar);
                }

                storage.GradedScalarsDictionaries[grade] = scalarValuesDictionary;
            }
            
            return storage;
        }

        public override IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyReverse()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                if (id.BasisBladeIdHasNegativeReverse())
                    ScalarsArray[id] = ScalarDomain.Negative(ScalarsArray[id]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                if (id.BasisBladeIdHasNegativeGradeInv())
                    ScalarsArray[id] = ScalarDomain.Negative(ScalarsArray[id]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                if (id.BasisBladeIdHasNegativeCliffConj())
                    ScalarsArray[id] = ScalarDomain.Negative(ScalarsArray[id]);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                ScalarsArray[id] = ScalarDomain.Negative(scalar);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                ScalarsArray[id] = ScalarDomain.Times(scalar, scalingFactor);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                ScalarsArray[id] = mappingFunc(scalar);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, T, T> mappingFunc)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var scalar = ScalarsArray[id];

                ScalarsArray[id] = mappingFunc(id, scalar);
            }

            return this;
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, int, T, T> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var count = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                for (var index = 0; index < count; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);
                    var scalar = ScalarsArray[id];
                    
                    ScalarsArray[id] = mappingFunc(grade, index, scalar);
                }
            }

            return this;
        }

        public override IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return ScalarsArray.Select(
                (t, i) => new KeyValuePair<int, T>(i, t)
            ).GetEnumerator();
        }
    }
}