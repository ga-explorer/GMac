using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Scalars;
using GeometricAlgebraStructuresLib.Storage;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public class GaMultivector<T> : IGaMultivector<T>
    {
        public int VSpaceDimension 
            => Storage.VSpaceDimension;

        public int GaSpaceDimension 
            => Storage.GaSpaceDimension;
        
        public IGaMultivectorStorage<T> Storage { get; }

        public T this[int id]
        {
            get => Storage.GetTermScalar(id);
            set => Storage.SetTermScalar(id, value);
        }

        public T this[int grade, int index]
        {
            get => Storage.GetTermScalar(grade, index);
            set => Storage.SetTermScalar(grade, index, value);
        }

        public bool IsTerm()
        {
            throw new System.NotImplementedException();
        }

        public bool IsScalar()
        {
            throw new System.NotImplementedException();
        }

        public bool IsZero()
        {
            throw new System.NotImplementedException();
        }

        public bool IsNearZero(T epsilon)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivector<T> GetVectorPart()
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivector<T> GetKVectorPart(int grade)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IGaMultivector<T>> GetKVectorParts()
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivector<T> GetNegative()
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivector<T> GetReverse()
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivector<T> GetGradeInv()
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivector<T> GetCliffConj()
        {
            throw new System.NotImplementedException();
        }


        public GaMultivector(IGaMultivectorStorage<T> storage)
        {
            Storage = storage;
        }
        
        public GaMultivector(int vSpaceDimension, IGaScalarDomain<T> scalarDomain)
        {
            Storage = new GaMvsSparseArray<T>(vSpaceDimension, scalarDomain);
        }

        public IEnumerator<IGaTerm<T>> GetEnumerator()
        {
            return Storage.GetNonZeroTerms().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
