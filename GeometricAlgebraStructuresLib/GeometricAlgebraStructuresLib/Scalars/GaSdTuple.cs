using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeometricAlgebraStructuresLib.Scalars
{
    public sealed class GaSdTuple<T> : IGaScalarDomain<IReadOnlyList<T>>
    {
        public int Size { get; }
        
        public IGaScalarDomain<T> ElementScalarDomain { get; }


        public GaSdTuple(int size, IGaScalarDomain<T> elementScalarDomain)
        {
            Size = size;
            ElementScalarDomain = elementScalarDomain;
        }


        public IReadOnlyList<T> GetZero()
        {
            var result = new T[Size];
            
            for (var i = 0; i < Size; i++)
                result[i] = ElementScalarDomain.GetZero();

            return result;
        }

        public IReadOnlyList<T> GetOne()
        {
            var result = new T[Size];
            
            for (var i = 0; i < Size; i++)
                result[i] = ElementScalarDomain.GetOne();

            return result;
        }

        public IReadOnlyList<T> Add(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            Debug.Assert(scalar1.Count == Size && scalar2.Count == Size);

            return scalar1
                .Zip(scalar2, (s1, s2) => ElementScalarDomain.Add(s1, s2))
                .ToArray();
        }

        public IReadOnlyList<T> Add(params IReadOnlyList<T>[] scalarsList)
        {
            Debug.Assert(
                scalarsList.All(s => s.Count == Size)
            );
            
            var result = new T[Size];

            var scalar0 = scalarsList[0];
            for (var i = 0; i < Size; i++)
                result[i] = scalar0[i];
            
            foreach (var scalar in scalarsList.Skip(1))
                for (var i = 0; i < Size; i++)
                    result[i] = ElementScalarDomain.Add(result[i], scalar[i]);
            
            return result;
        }

        public IReadOnlyList<T> Add(IEnumerable<IReadOnlyList<T>> scalarsList)
        {
            Debug.Assert(
                scalarsList.All(s => s.Count == Size)
            );
            
            var result = new T[Size];

            for (var i = 0; i < Size; i++)
                result[i] = ElementScalarDomain.GetZero();
            
            foreach (var scalar in scalarsList)
                for (var i = 0; i < Size; i++)
                    result[i] = ElementScalarDomain.Add(result[i], scalar[i]);
            
            return result;
        }

        public IReadOnlyList<T> Subtract(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            Debug.Assert(scalar1.Count == Size && scalar2.Count == Size);

            return scalar1
                .Zip(scalar2, (s1, s2) => ElementScalarDomain.Subtract(s1, s2))
                .ToArray();
        }

        public IReadOnlyList<T> Times(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            Debug.Assert(scalar1.Count == Size && scalar2.Count == Size);

            return scalar1
                .Zip(scalar2, (s1, s2) => ElementScalarDomain.Times(s1, s2))
                .ToArray();
        }

        public IReadOnlyList<T> Times(params IReadOnlyList<T>[] scalarsList)
        {
            Debug.Assert(
                scalarsList.All(s => s.Count == Size)
            );
            
            var result = new T[Size];

            var scalar0 = scalarsList[0];
            for (var i = 0; i < Size; i++)
                result[i] = scalar0[i];
            
            foreach (var scalar in scalarsList.Skip(1))
                for (var i = 0; i < Size; i++)
                    result[i] = ElementScalarDomain.Times(result[i], scalar[i]);
            
            return result;
        }

        public IReadOnlyList<T> Times(IEnumerable<IReadOnlyList<T>> scalarsList)
        {
            Debug.Assert(
                scalarsList.All(s => s.Count == Size)
            );
            
            var result = new T[Size];

            for (var i = 0; i < Size; i++)
                result[i] = ElementScalarDomain.GetZero();
            
            foreach (var scalar in scalarsList)
                for (var i = 0; i < Size; i++)
                    result[i] = ElementScalarDomain.Times(result[i], scalar[i]);
            
            return result;
        }

        public IReadOnlyList<T> Divide(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
        {
            Debug.Assert(scalar1.Count == Size && scalar2.Count == Size);

            return scalar1
                .Zip(scalar2, (s1, s2) => ElementScalarDomain.Divide(s1, s2))
                .ToArray();
        }

        public IReadOnlyList<T> Positive(IReadOnlyList<T> scalar)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<T> Negative(IReadOnlyList<T> scalar)
        {
            Debug.Assert(scalar.Count == Size);
            
            return scalar.Select(ElementScalarDomain.Negative).ToArray();
        }

        public IReadOnlyList<T> Inverse(IReadOnlyList<T> scalar)
        {
            Debug.Assert(scalar.Count == Size);
            
            return scalar.Select(ElementScalarDomain.Inverse).ToArray();
        }

        public bool IsZero(IReadOnlyList<T> scalar)
        {
            Debug.Assert(scalar.Count == Size);
            
            return scalar.All(s => ElementScalarDomain.IsZero(s));
        }

        public bool IsZero(IReadOnlyList<T> scalar, bool nearZeroFlag)
        {
            Debug.Assert(scalar.Count == Size);
            
            return nearZeroFlag
                ? scalar.All(s => ElementScalarDomain.IsNearZero(s))
                : scalar.All(s => ElementScalarDomain.IsZero(s));
        }

        public bool IsNearZero(IReadOnlyList<T> scalar)
        {
            Debug.Assert(scalar.Count == Size);
            
            return scalar.All(s => ElementScalarDomain.IsNearZero(s));
        }

        public bool IsNotZero(IReadOnlyList<T> scalar)
        {
            Debug.Assert(scalar.Count == Size);
            
            return scalar.Any(s => ElementScalarDomain.IsNotZero(s));
        }

        public bool IsNotZero(IReadOnlyList<T> scalar, bool nearZeroFlag)
        {
            Debug.Assert(scalar.Count == Size);
            
            return nearZeroFlag
                ? scalar.Any(s => ElementScalarDomain.IsNotNearZero(s))
                : scalar.Any(s => ElementScalarDomain.IsNotZero(s));
        }

        public bool IsNotNearZero(IReadOnlyList<T> scalar)
        {
            Debug.Assert(scalar.Count == Size);
            
            return scalar.Any(s => ElementScalarDomain.IsNotNearZero(s));
        }
    }
}