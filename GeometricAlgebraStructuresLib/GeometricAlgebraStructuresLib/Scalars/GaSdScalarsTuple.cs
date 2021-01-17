using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraStructuresLib.Tuples;

namespace GeometricAlgebraStructuresLib.Scalars
{
    public sealed class GaSdScalarsTuple<T> : IGaScalarDomain<IGaScalarsTuple<T>>
    {
        public int ItemsCount { get; }
        
        public IGaScalarDomain<T> ItemsScalarDomain { get; }


        public GaSdScalarsTuple(int maxListSize)
        {
            Debug.Assert(maxListSize > 0);
            
            ItemsCount = maxListSize;
        }
        
        
        public IGaScalarsTuple<T> GetZero()
        {
            return new GaZeroScalarsTuple<T>(ItemsCount, ItemsScalarDomain);
        }

        public IGaScalarsTuple<T> GetOne()
        {
            return new GaConstantScalarsTuple<T>(
                ItemsScalarDomain.GetOne(),
                ItemsCount, 
                ItemsScalarDomain
            );
        }

        public IGaScalarsTuple<T> Add(IGaScalarsTuple<T> scalar1, IGaScalarsTuple<T> scalar2)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Add(params IGaScalarsTuple<T>[] scalarsList)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Add(IEnumerable<IGaScalarsTuple<T>> scalarsList)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Subtract(IGaScalarsTuple<T> scalar1, IGaScalarsTuple<T> scalar2)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Times(IGaScalarsTuple<T> scalar1, IGaScalarsTuple<T> scalar2)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Times(params IGaScalarsTuple<T>[] scalarsList)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Times(IEnumerable<IGaScalarsTuple<T>> scalarsList)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Divide(IGaScalarsTuple<T> scalar1, IGaScalarsTuple<T> scalar2)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Positive(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Negative(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarsTuple<T> Inverse(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsZero(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsZero(IGaScalarsTuple<T> scalar, bool nearZeroFlag)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNearZero(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotZero(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotZero(IGaScalarsTuple<T> scalar, bool nearZeroFlag)
        {
            throw new System.NotImplementedException();
        }

        public bool IsNotNearZero(IGaScalarsTuple<T> scalar)
        {
            throw new System.NotImplementedException();
        }
    }
}