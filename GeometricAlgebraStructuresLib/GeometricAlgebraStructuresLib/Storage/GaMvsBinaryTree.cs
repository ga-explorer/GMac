using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    public class GaMvsBinaryTree<T> 
        : GaUniformMultivectorStorage<T>
    {
        public GaBinaryTree<T> ScalarsTree { get; }


        public override int StoredTermsCount 
            => ScalarsTree.Count;


        public GaMvsBinaryTree(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, IReadOnlyList<int> idsList)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsTree = new GaBinaryTree<T>(vSpaceDimension, idsList);
        }

        public GaMvsBinaryTree(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, IReadOnlyDictionary<int, T> leafNodes)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsTree = new GaBinaryTree<T>(vSpaceDimension, leafNodes);
        }

        public GaMvsBinaryTree(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, IDictionary<int, T> leafNodes)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsTree = new GaBinaryTree<T>(vSpaceDimension, leafNodes);
        }

        public GaMvsBinaryTree(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, IReadOnlyList<int> idsList, IReadOnlyCollection<T> leafNodeValuesList)
            : base(vSpaceDimension, scalarDomain)
        {
            ScalarsTree = new GaBinaryTree<T>(vSpaceDimension, idsList, leafNodeValuesList);
        }

        public GaMvsBinaryTree(IGaScalarDomain<T> scalarDomain, GaBinaryTree<T> scalarsTree)
            : base(scalarsTree.TreeDepth, scalarDomain)
        {
            ScalarsTree = new GaBinaryTree<T>(scalarsTree);
        }
        
        
        public override T GetTermScalar(int id)
        {
            return ScalarsTree.TryGetValue(id, out var value) 
                ? value 
                : ScalarDomain.GetZero();
        }

        public override bool TryGetTermScalar(int id, out T value)
        {
            return ScalarsTree.TryGetValue(id, out value);
        }


        public override IGaMultivectorStorage<T> SetTermScalar(int basisBladeId, T value)
        {
            ScalarsTree[basisBladeId] = value;

            return this;
        }

        public override IGaMultivectorStorage<T> AddTerm(int id, T value)
        {
            if (!ScalarsTree.TryGetLeafNodeIndexValue(id, out var index, out var scalar))
                throw new IndexOutOfRangeException();

            ScalarsTree.SetLeafNodeValueByIndex(
                index,
                ScalarDomain.Add(scalar, value)
            );
            
            return this;
        }


        public override IGaMultivectorStorage<T> RemoveTerm(int id)
        {
            if (!ScalarsTree.TryGetLeafNodeIndex(id, out var index))
                return this;
            
            ScalarsTree.SetLeafNodeValueByIndex(index, ScalarDomain.GetZero());
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            var idsList = ScalarsTree
                .GetLeafNodeIDs()
                .Where(id => id.BasisBladeGrade() == grade);

            foreach (var id in idsList)
                ScalarsTree[id] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveTermIfZero(int id, bool nearZeroFlag = false)
        {
            if (!nearZeroFlag)
                return this;
            
            if (!ScalarsTree.TryGetLeafNodeIndexValue(id, out var index, out var scalar))
                return this;

            if (ScalarDomain.IsNearZero(scalar))
                ScalarsTree.SetLeafNodeValueByIndex(index, ScalarDomain.GetZero());
            
            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            if (!nearZeroFlag)
                return this;
            
            for (var index = 0; index < ScalarsTree.Count; index++)
            {
                var scalar = ScalarsTree.GetLeafNodeValueByIndex(index);

                if (ScalarDomain.IsNearZero(scalar))
                    ScalarsTree.SetLeafNodeValueByIndex(index, ScalarDomain.GetZero());
            }

            return this;
        }

        public override IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (!nearZeroFlag)
                return this;

            var idsList = ScalarsTree
                .GetLeafNodes()
                .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(term.Value))
                .Select(term => term.Key);

            foreach (var id in idsList)
                ScalarsTree[id] = ScalarDomain.GetZero();
            
            return this;
        }

        public override IGaMultivectorStorage<T> ResetToZero()
        {
            ScalarsTree.ClearValues();

            return this;
        }

        public override bool IsEmpty()
        {
            return ScalarsTree.Count == 0;
        }

        public override bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsTree.All(scalar => ScalarDomain.IsNearZero(scalar))
                : ScalarsTree.All(scalar => ScalarDomain.IsZero(scalar));
        }

        public override bool ContainsStoredTerm(int id)
        {
            return ScalarsTree.ContainsId(id);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return ScalarsTree
                .GetLeafNodeIDs()
                .Any(id => id.BasisBladeGrade() == grade);
        }

        public override bool CanStoreTerm(int id)
        {
            return ScalarsTree.ContainsId(id);
        }

        public override bool CanStoreSomeTermsOfGrade(int grade)
        {
            return ScalarsTree
                .GetLeafNodeIDs()
                .Any(id => id.BasisBladeGrade() == grade);
        }

        public override bool CanStoreAllTermsOfGrade(int grade)
        {
            var count = ScalarsTree
                .GetLeafNodeIDs()
                .Count(id => id.BasisBladeGrade() == grade);

            return count == GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
        }

        public override IEnumerable<int> GetStoredTermIds()
        {
            return ScalarsTree.GetLeafNodeIDs();
        }

        public override IEnumerable<int> GetStoredTermIds(Func<T, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term => selectionFilter(term.Value))
                .Select(term => term.Key);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodeIDs()
                .Where(id =>
                {
                    id.BasisBladeGradeIndex(out var grade, out var index);

                    return selectionFilter(grade, index);
                });
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, T, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term => selectionFilter(term.Key, term.Value))
                .Select(term => term.Key);
        }

        public override IEnumerable<int> GetStoredTermIds(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term =>
                {
                    term.Key.BasisBladeGradeIndex(out var grade, out var index);

                    return selectionFilter(grade, index, term.Value);
                })
                .Select(term => term.Key);
        }

        public override IEnumerable<int> GetStoredTermIdsOfGrade(int grade)
        {
            return ScalarsTree
                .GetLeafNodeIDs()
                .Where(id => id.BasisBladeGrade() == grade);
        }

        public override IEnumerable<T> GetStoredTermScalars()
        {
            return ScalarsTree;
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term => selectionFilter(term.Key))
                .Select(term => term.Value);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, T, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term => selectionFilter(term.Key, term.Value))
                .Select(term => term.Value);
        }

        public override IEnumerable<T> GetStoredTermScalars(Func<int, int, T, bool> selectionFilter)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term =>
                {
                    term.Key.BasisBladeGradeIndex(out var grade, out var index);
                    
                    return selectionFilter(grade, index, term.Value);
                })
                .Select(term => term.Value);
        }

        public override IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term => term.Key.BasisBladeGrade() == grade)
                .Select(term => term.Value);
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            return ScalarsTree
                .GetLeafNodes()
                .Select(node => new GaUniformTerm<T>(node.Key, node.Value));
        }

        public override IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            return ScalarsTree
                .GetLeafNodes()
                .Where(term => term.Key.BasisBladeGrade() == grade)
                .Select(node => new GaUniformTerm<T>(node.Key, node.Value));
        }

        public override int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsTree.Count(scalar => ScalarDomain.IsNearZero(scalar))
                : ScalarsTree.Count(scalar => ScalarDomain.IsZero(scalar));
        }

        public override IEnumerable<int> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsTree
                    .GetLeafNodes()
                    .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(term.Value))
                    .Select(term => term.Key)
                : ScalarsTree
                    .GetLeafNodes()
                    .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsZero(term.Value))
                    .Select(term => term.Key);
        }

        public override IEnumerable<int> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsTree
                    .GetLeafNodes()
                    .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(term.Value))
                    .Select(term => term.Key.BasisBladeIndex())
                : ScalarsTree
                    .GetLeafNodes()
                    .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsZero(term.Value))
                    .Select(term => term.Key.BasisBladeIndex());
        }

        public override int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsTree.Count(scalar => ScalarDomain.IsNotNearZero(scalar))
                : ScalarsTree.Count(scalar => ScalarDomain.IsNotZero(scalar));
        }

        public override IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            var countDictionary = new Dictionary<int, int>();
            
            for (var i = 0; i < ScalarsTree.Count; i++)
            {
                var (id, scalar) = ScalarsTree.GetLeafNodeByIndex(i);

                if (ScalarDomain.IsZero(scalar, nearZeroFlag)) 
                    continue;

                var grade = id.BasisBladeGrade();

                if (countDictionary.TryGetValue(grade, out var oldCount))
                    countDictionary[grade] = oldCount + 1;
                else
                    countDictionary.Add(grade, 1);
            }

            return countDictionary;
        }

        public override IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarsTree
                    .GetLeafNodes()
                    .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsNearZero(term.Value))
                    .Select(term => new GaUniformTerm<T>(term))
                : ScalarsTree
                    .GetLeafNodes()
                    .Where(term => term.Key.BasisBladeGrade() == grade && ScalarDomain.IsZero(term.Value))
                    .Select(term => new GaUniformTerm<T>(term));
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            return ScalarsTree
                .GetLeafNodeIDs()
                .Select(id => id.BasisBladeGrade())
                .Distinct();
        }

        public override GaBinaryTree<T> GetBinaryTree()
        {
            return ScalarsTree;
        }

        public override GaMvsTerm<T> GetTermStorage(int id, bool getCopy = false)
        {
            var storage = new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, id);

            if (ScalarsTree.TryGetValue(id, out var scalar))
                storage.Scalar = scalar;
            
            return storage;
        }

        public override GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);

            var termsList = ScalarsTree
                .GetLeafNodes()
                .Where(term => term.Key.BasisBladeGrade() == 1);

            foreach (var term in termsList)
                storage.ScalarsArray[term.Key.BasisBladeIndex()] = term.Value;
            
            return storage;
        }

        public override IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            return GetSparseKVectorStorage(grade, getCopy);
        }

        public override GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var termsList = ScalarsTree
                .GetLeafNodes()
                .Where(term => term.Key.BasisBladeGrade() == grade);

            foreach (var term in termsList)
                storage.ScalarsArray[term.Key.BasisBladeIndex()] = term.Value;
            
            return storage;
        }

        public override GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var termsList = ScalarsTree
                .GetLeafNodes()
                .Where(term => term.Key.BasisBladeGrade() == grade);

            foreach (var term in termsList)
                storage.ScalarsDictionary.Add(term.Key.BasisBladeIndex(), term.Value);
            
            return storage;
        }

        public override IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            var gradesList = GetStoredGrades();
            
            var kVectorsArray = new GaMvsSparseKVector<T>[VSpaceDimension + 1];
            
            foreach (var grade in gradesList)
                kVectorsArray[grade] = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            var termsList = ScalarsTree.GetLeafNodes();

            foreach (var term in termsList)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);
                
                kVectorsArray[grade].ScalarsDictionary.Add(index, term.Value);
            }
            
            return kVectorsArray.Where(v => !ReferenceEquals(v, null));
        }

        public override GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            if (!getCopy)
                return this;
            
            return new GaMvsBinaryTree<T>(
                ScalarDomain,
                ScalarsTree
            );
        }

        public override GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            var termsList = ScalarsTree.GetLeafNodes();

            foreach (var term in termsList)
                storage.ScalarsArray[term.Key] = term.Value;
            
            return storage;
        }

        public override GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            var termsList = ScalarsTree.GetLeafNodes();

            foreach (var term in termsList)
                storage.ScalarsDictionary.Add(term.Key, term.Value);
            
            return storage;
        }

        public override GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);
            
            var gradesList = GetStoredGrades();

            foreach (var grade in gradesList)
                storage.GradedScalarsArrays[grade] = new T[GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade)];

            var termsList = ScalarsTree.GetLeafNodes();

            foreach (var term in termsList)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);
                
                storage.GradedScalarsArrays[grade][index] = term.Value;
            }
            
            return storage;
        }

        public override GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            var termsList = ScalarsTree.GetLeafNodes();

            foreach (var term in termsList)
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
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyGradeInv()
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyCliffConj()
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyNegative()
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, T, T> mappingFunc)
        {
            throw new NotImplementedException();
        }

        public override IGaMultivectorStorage<T> ApplyMapping(Func<int, int, T, T> mappingFunc)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}