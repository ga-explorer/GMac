﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.Multivectors;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Trees;

namespace GeometricAlgebraStructuresLib.Storage
{
    /// <summary>
    /// This multivector storage structure can hold a single term with fixed ID
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaMvsTerm<T> : IGaKVectorStorage<T>, IGaTerm<T>
    {
        public IGaScalarDomain<T> ScalarDomain { get; }
        
        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << VSpaceDimension; 

        public int StoredTermsCount 
            => 1;

        public ulong Id { get; }
        
        public int Grade { get; }

        public ulong KvSpaceDimension 
            => GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade);

        public ulong Index { get; }

        public T Scalar { get; set; }

        public bool IsUniform 
            => true;

        public bool IsGraded 
            => true;


        public GaMvsTerm(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            VSpaceDimension = vSpaceDimension;
            ScalarDomain = scalarDomain;
            Id = id;
            Grade = grade;
            Index = index;
            Scalar = ScalarDomain.GetZero();
        }

        public GaMvsTerm(int vSpaceDimension, IGaScalarDomain<T> scalarDomain, int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);
            
            VSpaceDimension = vSpaceDimension;
            ScalarDomain = scalarDomain;
            Id = id;
            Grade = grade;
            Index = index;
            Scalar = ScalarDomain.GetZero();
        }

        private GaMvsTerm(GaMvsTerm<T> term)
        {
            VSpaceDimension = term.VSpaceDimension;
            ScalarDomain = term.ScalarDomain;
            Id = term.Id;
            Grade = term.Grade;
            Index = term.Index;
            Scalar = term.Scalar;
        }
        
        private GaMvsTerm(GaMvsTerm<T> term, T newScalar)
        {
            VSpaceDimension = term.VSpaceDimension;
            ScalarDomain = term.ScalarDomain;
            Id = term.Id;
            Grade = term.Grade;
            Index = term.Index;
            Scalar = newScalar;
        }

        
        public IGaTerm<T> GetCopy(T newScalar)
        {
            return new GaMvsTerm<T>(this, newScalar);
        }

        public T GetTermScalar(ulong id)
        {
            return Id == id
                ? Scalar
                : ScalarDomain.GetZero();
        }

        public T GetTermScalar(int grade, ulong index)
        {
            return Grade == grade && Index == index
                ? Scalar
                : ScalarDomain.GetZero();
        }

        public IGaTerm<T> GetTerm(ulong id)
        {
            return Id == id
                ? (IGaTerm<T>)this
                : new GaUniformTerm<T>(id, ScalarDomain.GetZero());
        }

        public IGaTerm<T> GetTerm(int grade, ulong index)
        {
            return Grade == grade && Index == index
                ? (IGaTerm<T>)this
                : new GaGradedTerm<T>(grade, index, ScalarDomain.GetZero());
        }

        public bool TryGetTermScalar(ulong id, out T value)
        {
            if (id == Id)
            {
                value = Scalar;
                return true;
            }

            value = ScalarDomain.GetZero();
            return false;
        }

        public bool TryGetTermScalar(int grade, ulong index, out T value)
        {
            if (Grade == grade && Index == index)
            {
                value = Scalar;
                return true;
            }

            value = ScalarDomain.GetZero();
            return false;
        }

        public bool TryGetTerm(ulong id, out IGaTerm<T> term)
        {
            if (id == Id)
            {
                term = new GaUniformTerm<T>(id, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public bool TryGetTerm(int grade, ulong index, out IGaTerm<T> term)
        {
            if (Grade == grade && Index == index)
            {
                term = new GaGradedTerm<T>(grade, index, Scalar);
                return true;
            }

            term = null;
            return false;
        }

        public IGaMultivectorStorage<T> SetTermScalar(ulong id, T value)
        {
            if (Id != id)
                throw new IndexOutOfRangeException();

            Scalar = value;

            return this;
        }

        public IGaMultivectorStorage<T> SetTermScalar(int grade, ulong index, T value)
        {
            if (Grade != grade || Index != index)
                throw new IndexOutOfRangeException();

            Scalar = value;

            return this;
        }

        public IGaMultivectorStorage<T> SetTerms(IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                if (term.IsUniform)
                    SetTermScalar(term.Id, term.Scalar);
                else
                    SetTermScalar(term.Grade, term.Index, term.Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> SetTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
            {
                var scalarValue = 
                    ScalarDomain.Times(scalingFactor, term.Scalar);
                
                if (term.IsUniform)
                    SetTermScalar(term.Id, scalarValue);
                else
                    SetTermScalar(term.Grade, term.Index, scalarValue);
            }

            return this;
        }

        public bool TrySetTermScalar(ulong id, T value)
        {
            if (Id != id) 
                return false;
            
            Scalar = value;
            
            return true;
        }

        public bool TrySetTermScalar(int grade, ulong index, T value)
        {
            if (Grade != grade || Index != index) 
                return false;
            
            Scalar = value;
            
            return true;
        }

        public IGaMultivectorStorage<T> SetKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            if (scalarValuesList.Count > 1 || Index != 0)
                throw new IndexOutOfRangeException();

            Scalar = scalarValuesList[0];
            
            return this;
        }

        public IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            if (scalarValuesList.Count > 1 || Index != 0)
                throw new IndexOutOfRangeException();

            Scalar = ScalarDomain.Times(scalingFactor, scalarValuesList[0]);
            
            return this;
        }

        public IGaMultivectorStorage<T> SetKVector(int grade, IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTermScalar(
                    grade, 
                    pair.Key, 
                    pair.Value
                );

            return this;
        }

        public IGaMultivectorStorage<T> SetKVector(int grade, T scalingFactor,
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

        public IGaMultivectorStorage<T> SetKVector(IGaKVectorStorage<T> kvector)
        {
            foreach (var term in kvector.GetStoredTerms())
                SetTermScalar(
                    term.Grade,
                    term.Index,
                    term.Scalar
                );

            return this;
        }

        public IGaMultivectorStorage<T> SetKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            foreach (var term in kvector.GetStoredTerms())
                SetTermScalar(
                    term.Grade,
                    term.Index,
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );

            return this;
        }

        public IGaMultivectorStorage<T> SetKVectors(IEnumerable<IGaKVectorStorage<T>> kVectorsList)
        {
            foreach (var term in kVectorsList.SelectMany(v => v.GetStoredTerms()))
                SetTermScalar(
                    term.Grade,
                    term.Index,
                    term.Scalar
                );

            return this;
        }

        public IGaMultivectorStorage<T> SetKVectors(IEnumerable<Tuple<T, IGaKVectorStorage<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                SetKVector(scalingFactor, kVector);

            return this;
        }

        public IGaMultivectorStorage<T> AddTerm(ulong id, T value)
        {
            if (Id != id)
                throw new IndexOutOfRangeException();

            Scalar = ScalarDomain.Add(Scalar, value);

            return this;
        }

        public IGaMultivectorStorage<T> AddTerm(int grade, ulong index, T value)
        {
            if (Grade != grade || Index != index)
                throw new IndexOutOfRangeException();

            Scalar = ScalarDomain.Add(Scalar, value);

            return this;
        }

        public IGaMultivectorStorage<T> AddTerm(IGaTerm<T> term)
        {
            if (term.IsUniform)
            {
                if (Id != term.Id)
                    throw new IndexOutOfRangeException();
            }
            else
            {
                if (Grade != term.Grade || Index != term.Index)
                    throw new IndexOutOfRangeException();
            }
            
            Scalar = ScalarDomain.Add(Scalar, term.Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> AddTerm(T scalingFactor, IGaTerm<T> term)
        {
            if (term.IsUniform)
            {
                if (Id != term.Id)
                    throw new IndexOutOfRangeException();
            }
            else
            {
                if (Grade != term.Grade || Index != term.Index)
                    throw new IndexOutOfRangeException();
            }
            
            Scalar = ScalarDomain.Add(
                Scalar,
                ScalarDomain.Times(scalingFactor, term.Scalar)
            );

            return this;
        }

        public IGaMultivectorStorage<T> AddTerms(IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }

        public IGaMultivectorStorage<T> AddTerms(T scalingFactor, IEnumerable<IGaTerm<T>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(scalingFactor, term);

            return this;
        }

        public bool TryAddTerm(ulong id, T value)
        {
            if (Id != id)
                return false;
            
            Scalar = ScalarDomain.Add(
                Scalar,
                value
            );

            return true;
        }

        public bool TryAddTerm(int grade, ulong index, T value)
        {
            if (Grade != grade || Index != index)
                return false;
            
            Scalar = ScalarDomain.Add(
                Scalar,
                value
            );

            return true;
        }

        public IGaMultivectorStorage<T> AddKVector(int grade, IReadOnlyList<T> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                AddTerm(grade, (ulong)index, scalarValuesList[index]);

            return this;
        }

        public IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor, IReadOnlyList<T> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                AddTerm(
                    grade, 
                    (ulong)index, 
                    ScalarDomain.Times(scalingFactor, scalarValuesList[index])
                );

            return this;
        }

        public IGaMultivectorStorage<T> AddKVector(int grade, IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(grade, pair.Key, pair.Value);

            return this;
        }

        public IGaMultivectorStorage<T> AddKVector(int grade, T scalingFactor,
            IEnumerable<KeyValuePair<ulong, T>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(
                    grade, 
                    pair.Key,
                    ScalarDomain.Times(scalingFactor, pair.Value)
                );

            return this;
        }

        public IGaMultivectorStorage<T> AddKVector(IGaKVectorStorage<T> kvector)
        {
            foreach (var term in kvector.GetStoredTerms())
                AddTerm(
                    term.Grade,
                    term.Index,
                    term.Scalar
                );

            return this;
        }

        public IGaMultivectorStorage<T> AddKVector(T scalingFactor, IGaKVectorStorage<T> kvector)
        {
            foreach (var term in kvector.GetStoredTerms())
                AddTerm(
                    term.Grade,
                    term.Index,
                    ScalarDomain.Times(scalingFactor, term.Scalar)
                );

            return this;
        }

        public IGaMultivectorStorage<T> AddKVectors(IEnumerable<IGaKVectorStorage<T>> kVectorsList)
        {
            var termsList = 
                kVectorsList.SelectMany(v => v.GetStoredTerms());
            
            foreach (var term in termsList)
                AddTerm(
                    term.Grade,
                    term.Index,
                    ScalarDomain.Times(term.Scalar)
                );

            return this;
        }

        public IGaMultivectorStorage<T> AddKVectors(IEnumerable<Tuple<T, IGaKVectorStorage<T>>> scaledKVectorsList)
        {
            foreach (var (scalingFactor, kVector) in scaledKVectorsList)
                AddKVector(scalingFactor, kVector);

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerm(ulong id)
        {
            if (Id == id)
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerm(int grade, ulong index)
        {
            if (Grade == grade && Index == index)
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerms(IEnumerable<ulong> idsList)
        {
            if (idsList.Any(id => Id == id))
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerms(int grade, IEnumerable<ulong> indexList)
        {
            if (Grade == grade && indexList.Any(index => Index == index))
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerms(Func<T, bool> selectionFilter)
        {
            if (selectionFilter(Scalar))
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerms(Func<ulong, bool> selectionFilter)
        {
            if (selectionFilter(Id))
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerms(Func<int, ulong, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index))
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTerms(Func<ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Id, Scalar))
                Scalar = ScalarDomain.GetZero();

            return this;

        }

        public IGaMultivectorStorage<T> RemoveTerms(Func<int, ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index, Scalar))
                Scalar = ScalarDomain.GetZero();

            return this;

        }

        public IGaMultivectorStorage<T> RemoveTermsOfGrade(int grade)
        {
            if (Grade == grade)
                Scalar = ScalarDomain.GetZero();

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTermIfZero(ulong id, bool nearZeroFlag = false)
        {
            if (Id != id)
                return this;

            if (nearZeroFlag)
            {
                if (ScalarDomain.IsNearZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }
            else
            {
                if (ScalarDomain.IsZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }

            return this;
        }

        public IGaMultivectorStorage<T> RemoveTermIfZero(int grade, ulong index, bool nearZeroFlag = false)
        {
            if (Grade != grade || Index != index)
                return this;

            if (nearZeroFlag)
            {
                if (ScalarDomain.IsNearZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }
            else
            {
                if (ScalarDomain.IsZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }

            return this;
        }

        public IGaMultivectorStorage<T> RemoveZeroTerms(bool nearZeroFlag = false)
        {
            if (nearZeroFlag)
            {
                if (ScalarDomain.IsNearZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }
            else
            {
                if (ScalarDomain.IsZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }

            return this;
        }

        public IGaMultivectorStorage<T> RemoveZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (Grade != grade)
                return this;
            
            if (nearZeroFlag)
            {
                if (ScalarDomain.IsNearZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }
            else
            {
                if (ScalarDomain.IsZero(Scalar))
                    Scalar = ScalarDomain.GetZero();
            }

            return this;
        }

        public IGaMultivectorStorage<T> ResetToZero()
        {
            Scalar = ScalarDomain.GetZero();

            return this;
        }

        public bool IsEmpty()
        {
            return false;
        }

        public bool IsZero(bool nearZeroFlag = false)
        {
            return nearZeroFlag
                ? ScalarDomain.IsNearZero(Scalar)
                : ScalarDomain.IsZero(Scalar);
        }

        public bool ContainsStoredTerm(ulong id)
        {
            return Id == id;
        }

        public bool ContainsStoredTerm(int grade, ulong index)
        {
            return Grade == grade && Index == index;
        }

        public bool ContainsStoredTermOfGrade(int grade)
        {
            return Grade == grade;
        }

        public bool CanStoreTerm(ulong id)
        {
            return Id == id;
        }

        public bool CanStoreTerm(int grade, ulong index)
        {
            return Grade == grade && Index == index;
        }

        public bool CanStoreSomeTermsOfGrade(int grade)
        {
            return Grade == grade;
        }

        public bool CanStoreAllTermsOfGrade(int grade)
        {
            return Grade == grade && (Grade == 0 || Grade == VSpaceDimension);
        }

        public IEnumerable<ulong> GetStoredTermIds()
        {
            yield return Id;
        }

        public IEnumerable<ulong> GetStoredTermIds(Func<T, bool> selectionFilter)
        {
            if (selectionFilter(Scalar))
                yield return Id;
        }

        public IEnumerable<ulong> GetStoredTermIds(Func<ulong, bool> selectionFilter)
        {
            if (selectionFilter(Id))
                yield return Id;
        }

        public IEnumerable<ulong> GetStoredTermIds(Func<int, ulong, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index))
                yield return Id;
        }

        public IEnumerable<ulong> GetStoredTermIds(Func<ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Id, Scalar))
                yield return Id;
        }

        public IEnumerable<ulong> GetStoredTermIds(Func<int, ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index, Scalar))
                yield return Id;
        }

        public IEnumerable<ulong> GetStoredTermIdsOfGrade(int grade)
        {
            if (Grade == grade)
                yield return Id;
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices()
        {
            yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<T, bool> selectionFilter)
        {
            if (selectionFilter(Scalar))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, bool> selectionFilter)
        {
            if (selectionFilter(Id))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<int, ulong, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Id, Scalar))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredTermGradeIndices(Func<int, ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index, Scalar))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<ulong> GetStoredTermIndicesOfGrade(int grade)
        {
            if (Grade == grade)
                yield return Index;
        }

        public IEnumerable<T> GetStoredTermScalars()
        {
            yield return Scalar;
        }

        public IEnumerable<T> GetStoredTermScalars(Func<T, bool> selectionFilter)
        {
            if (selectionFilter(Scalar))
                yield return Scalar;
        }

        public IEnumerable<T> GetStoredTermScalars(Func<ulong, bool> selectionFilter)
        {
            if (selectionFilter(Id))
                yield return Scalar;
        }

        public IEnumerable<T> GetStoredTermScalars(Func<ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Id, Scalar))
                yield return Scalar;
        }

        public IEnumerable<T> GetStoredTermScalars(Func<int, ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index, Scalar))
                yield return Scalar;
        }

        public IEnumerable<T> GetStoredTermScalarsOfGrade(int grade)
        {
            if (Grade == grade)
                yield return Scalar;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTerms()
        {
            yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTerms(Func<T, bool> selectionFilter)
        {
            if (selectionFilter(Scalar))
                yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTerms(Func<ulong, bool> selectionFilter)
        {
            if (selectionFilter(Id))
                yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, ulong, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index))
                yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTerms(Func<ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Id, Scalar))
                yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTerms(Func<int, ulong, T, bool> selectionFilter)
        {
            if (selectionFilter(Grade, Index, Scalar))
                yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetStoredTermsOfGrade(int grade)
        {
            if (Grade == grade)
                yield return this;
        }

        public int GetStoredZeroTermsCount(bool nearZeroFlag = false)
        {
            return ScalarDomain.IsZero(Scalar, nearZeroFlag) ? 1 : 0;
        }

        public IEnumerable<ulong> GetStoredZeroTermIds(bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsZero(Scalar, nearZeroFlag))
                yield return Id;
        }

        public IEnumerable<Tuple<int, ulong>> GetStoredZeroTermGradeIndices(bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsZero(Scalar, nearZeroFlag))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<ulong> GetStoredZeroTermIdsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (Grade == grade && ScalarDomain.IsZero(Scalar, nearZeroFlag))
                yield return Id;
        }

        public IEnumerable<ulong> GetStoredZeroTermIndicesOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (Grade == grade && ScalarDomain.IsZero(Scalar, nearZeroFlag))
                yield return Index;
        }

        public int GetNonZeroTermsCount(bool nearZeroFlag = false)
        {
            return ScalarDomain.IsNotZero(Scalar, nearZeroFlag) ? 1 : 0;
        }

        public IReadOnlyDictionary<int, int> GetNonZeroTermsCountPerGrade(bool nearZeroFlag = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ulong> GetNonZeroTermIds(bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsNotZero(Scalar, nearZeroFlag))
                yield return Id;
        }

        public IEnumerable<Tuple<int, ulong>> GetNonZeroTermGradeIndices(bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsNotZero(Scalar, nearZeroFlag))
                yield return new Tuple<int, ulong>(Grade, Index);
        }

        public IEnumerable<T> GetNonZeroTermScalars(bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsNotZero(Scalar, nearZeroFlag))
                yield return Scalar;
        }

        public IEnumerable<IGaTerm<T>> GetNonZeroTerms(bool nearZeroFlag = false)
        {
            if (ScalarDomain.IsNotZero(Scalar, nearZeroFlag))
                yield return this;
        }

        public IEnumerable<IGaTerm<T>> GetNonZeroTermsOfGrade(int grade, bool nearZeroFlag = false)
        {
            if (Grade == grade && ScalarDomain.IsNotZero(Scalar, nearZeroFlag))
                yield return this;
        }

        public IEnumerable<int> GetStoredGrades()
        {
            yield return Grade;
        }

        public ulong GetStoredGradesBitPattern()
        {
            return 1UL << Grade;
        }

        public GaBinaryTree<T> GetBinaryTree()
        {
            return new GaBinaryTree<T>(
                VSpaceDimension,
                new [] { Id },
                new [] { Scalar }
            );
        }

        public GaMvsTerm<T> GetTermStorage(ulong id, bool getCopy = false)
        {
            if (Id != id)
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, id); 
            
            return getCopy 
                ? new GaMvsTerm<T>(this) 
                : this;
        }

        public GaMvsTerm<T> GetTermStorage(int grade, ulong index, bool getCopy = false)
        {
            if (Grade != grade || Index != index)
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, index); 
            
            return getCopy 
                ? new GaMvsTerm<T>(this) 
                : this;
        }

        public GaMvsVector<T> GetVectorStorage(bool getCopy = false)
        {
            var storage = new GaMvsVector<T>(VSpaceDimension, ScalarDomain);
            
            if (Grade != 1)
                return storage;

            storage.ScalarsArray[Index] = Scalar;

            return storage;
        }

        public IGaKVectorStorage<T> GetKVectorStorage(int grade, bool getCopy = false)
        {
            if (Grade != grade)
                return new GaMvsTerm<T>(VSpaceDimension, ScalarDomain, grade, 0);

            return getCopy 
                ? new GaMvsTerm<T>(this) 
                : this;
        }

        public GaMvsDenseKVector<T> GetDenseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsDenseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            if (Grade != grade)
                return storage;

            storage.ScalarsArray[Index] = Scalar;
            
            return storage;
        }

        public GaMvsSparseKVector<T> GetSparseKVectorStorage(int grade, bool getCopy = false)
        {
            var storage = new GaMvsSparseKVector<T>(VSpaceDimension, ScalarDomain, grade);

            if (Grade != grade)
                return storage;

            if (ScalarDomain.IsNotZero(Scalar))
                storage.ScalarsDictionary.AddOrSet(Index, Scalar);
            
            return storage;
        }

        public IEnumerable<IGaKVectorStorage<T>> GetStoredKVectors(bool getCopy = false)
        {
            yield return getCopy 
                ? new GaMvsTerm<T>(this) 
                : this;
        }

        public GaMvsBinaryTree<T> GetBinaryTreeStorage(bool getCopy = false)
        {
            var storage = new GaMvsBinaryTree<T>(
                VSpaceDimension, 
                ScalarDomain, 
                new []{ Id },
                new []{ Scalar }
            );

            return storage;
        }

        public GaMvsDenseArray<T> GetDenseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseArray<T>(VSpaceDimension, ScalarDomain);

            storage.ScalarsArray[Id] = Scalar;

            return storage;
        }

        public GaMvsSparseArray<T> GetSparseArrayStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseArray<T>(VSpaceDimension, ScalarDomain);

            if (ScalarDomain.IsNotZero(Scalar))
                storage.ScalarsDictionary.AddOrSet(Id, Scalar);

            return storage;
        }

        public GaMvsDenseGraded<T> GetDenseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsDenseGraded<T>(VSpaceDimension, ScalarDomain);

            storage.SetTermScalar(Grade, Index, Scalar);

            return storage;
        }

        public GaMvsSparseGraded<T> GetSparseGradedStorage(bool getCopy = false)
        {
            var storage = new GaMvsSparseGraded<T>(VSpaceDimension, ScalarDomain);

            if (ScalarDomain.IsNotZero(Scalar))
                storage.SetTermScalar(Grade, Index, Scalar);

            return storage;
        }

        public IGaMultivectorStorage<T> GetMinimalStorage(bool getCopy = false, bool nearZeroFlag = false)
        {
            return GetTermStorage(Id, getCopy);
        }

        public IGaMultivectorStorage<T> ApplyReverse()
        {
            if (Grade.GradeHasNegativeReverse())
                Scalar = ScalarDomain.Negative(Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyGradeInv()
        {
            if (Grade.GradeHasNegativeGradeInv())
                Scalar = ScalarDomain.Negative(Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyCliffConj()
        {
            if (Grade.GradeHasNegativeCliffConj())
                Scalar = ScalarDomain.Negative(Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyNegative()
        {
            Scalar = ScalarDomain.Negative(Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyScaling(T scalingFactor)
        {
            Scalar = ScalarDomain.Times(scalingFactor, Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyMapping(Func<T, T> mappingFunc)
        {
            Scalar = mappingFunc(Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyMapping(Func<ulong, T, T> mappingFunc)
        {
            Scalar = mappingFunc(Id, Scalar);

            return this;
        }

        public IGaMultivectorStorage<T> ApplyMapping(Func<int, ulong, T, T> mappingFunc)
        {
            Scalar = mappingFunc(Grade, Index, Scalar);

            return this;
        }


        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            yield return new KeyValuePair<ulong, T>(Id, Scalar); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
   }
}