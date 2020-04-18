using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GeometricAlgebraNumericsLib
{
    public class GaRandomGenerator
    {
        private Random _random;

        public double MinScalarLimit { get; private set; } = 0.0d;

        public double MaxScalarLimit { get; private set; } = 1.0d;

        public int MinIntegerLimit { get; private set; } = 0;

        public int MaxIntegerLimit { get; private set; } = int.MaxValue - 1;


        public GaRandomGenerator()
        {
            _random = new Random();
        }

        public GaRandomGenerator(int seed)
        {
            _random = new Random(seed);
        }


        public GaRandomGenerator ResetGenerator()
        {
            _random = new Random();

            return this;
        }

        public GaRandomGenerator ResetGenerator(int seed)
        {
            _random = new Random(seed);

            return this;
        }

        public GaRandomGenerator SetScalarLimits()
        {
            MinScalarLimit = 0.0d;
            MaxScalarLimit = 1.0d;

            return this;
        }

        public GaRandomGenerator SetScalarLimits(double maxLimit)
        {
            if (maxLimit < 0)
            {
                MinScalarLimit = maxLimit;
                MaxScalarLimit = 0.0d;
            }
            else
            {
                MinScalarLimit = 0.0d;
                MaxScalarLimit = maxLimit;
            }

            return this;
        }

        public GaRandomGenerator SetScalarLimits(double minLimit, double maxLimit)
        {
            if (minLimit > maxLimit)
            {
                MinScalarLimit = maxLimit;
                MaxScalarLimit = minLimit;
            }
            else
            {
                MinScalarLimit = minLimit;
                MaxScalarLimit = maxLimit;
            }

            return this;
        }

        public GaRandomGenerator SetIntegerLimits()
        {
            MinIntegerLimit = 0;
            MaxIntegerLimit = Int32.MaxValue - 1;

            return this;
        }

        public GaRandomGenerator SetIntegerLimits(int maxLimit)
        {
            if (maxLimit >= 0)
            {
                MinIntegerLimit = 0;
                MaxIntegerLimit = maxLimit;
            }
            else
            {
                MinIntegerLimit = maxLimit;
                MaxIntegerLimit = 0;
            }

            return this;
        }

        public GaRandomGenerator SetIntegerLimits(int minLimit, int maxLimit)
        {
            if (maxLimit >= minLimit)
            {
                MinIntegerLimit = minLimit;
                MaxIntegerLimit = maxLimit;
            }
            else
            {
                MinIntegerLimit = maxLimit;
                MaxIntegerLimit = minLimit;
            }

            return this;
        }


        public double GetScalar()
        {
            return _random.NextDouble() * (MaxScalarLimit - MinScalarLimit) + MinScalarLimit;
        }

        public double GetScalar(double maxLimit)
        {
            return _random.NextDouble() * maxLimit;
        }

        public double GetScalar(double minLimit, double maxLimit)
        {
            return
                minLimit < maxLimit
                ? _random.NextDouble() * (maxLimit - minLimit) + minLimit
                : _random.NextDouble() * (minLimit - maxLimit) + maxLimit;
        }

        
        public int GetInteger()
        {
            return _random.Next(MinIntegerLimit, MaxIntegerLimit + 1);
        }

        /// <summary>
        /// Returns a random integer between zero and maxValue inclusive. If maxValue is negative
        /// his also works properly.
        /// </summary>
        /// <param name="maxLimit"></param>
        /// <returns></returns>
        public int GetInteger(int maxLimit)
        {
            return maxLimit > 0 
                ? _random.Next(maxLimit + 1)
                : -_random.Next(-maxLimit + 1);
        }

        public int GetInteger(int minLimit, int maxLimit)
        {
            return maxLimit < minLimit
                ? _random.Next(maxLimit, minLimit + 1)
                : _random.Next(minLimit, maxLimit + 1);
        }


        public IEnumerable<double> GetScalars(int scalarsCount)
        {
            while (scalarsCount > 0)
            {
                yield return GetScalar();

                scalarsCount--;
            }
        }

        public IEnumerable<double> GetScalars(int scalarsCount, double maxLimit)
        {
            while (scalarsCount > 0)
            {
                yield return GetScalar(maxLimit);

                scalarsCount--;
            }
        }

        public IEnumerable<double> GetScalars(int scalarsCount, double minLimit, double maxLimit)
        {
            while (scalarsCount > 0)
            {
                yield return GetScalar(minLimit, maxLimit);

                scalarsCount--;
            }
        }


        public IEnumerable<double> GetUniqueScalars(int scalarsCount)
        {
            var dict = new Dictionary<double, double>();

            while (scalarsCount > 0)
            {
                double value;
                do
                {
                    value = GetScalar();
                }
                while (dict.ContainsKey(value));

                dict.Add(value, 0);

                yield return value;

                scalarsCount--;
            }
        }

        public IEnumerable<double> GetUniqueScalars(int scalarsCount, double maxLimit)
        {
            var dict = new Dictionary<double, double>();

            while (scalarsCount > 0)
            {
                double value;
                do
                {
                    value = GetScalar(maxLimit);
                }
                while (dict.ContainsKey(value));

                dict.Add(value, 0);

                yield return value;

                scalarsCount--;
            }
        }

        public IEnumerable<double> GetUniqueScalars(int scalarsCount, double minLimit, double maxLimit)
        {
            var dict = new Dictionary<double, double>();

            while (scalarsCount > 0)
            {
                double value;
                do
                {
                    value = GetScalar(minLimit, maxLimit);
                }
                while (dict.ContainsKey(value));

                dict.Add(value, 0);

                yield return value;

                scalarsCount--;
            }
        }


        public IEnumerable<int> GetIntegers(int integersCount)
        {
            while (integersCount > 0)
            {
                yield return GetInteger();

                integersCount--;
            }
        }

        public IEnumerable<int> GetIntegers(int integersCount, int maxLimit)
        {
            while (integersCount > 0)
            {
                yield return GetInteger(maxLimit);

                integersCount--;
            }
        }

        public IEnumerable<int> GetIntegers(int integersCount, int minLimit, int maxLimit)
        {
            while (integersCount > 0)
            {
                yield return GetInteger(minLimit, maxLimit);

                integersCount--;
            }
        }


        public IEnumerable<int> GetUniqueIntegers(int integersCount)
        {
            var dict = new Dictionary<int, int>();

            while (integersCount > 0)
            {
                int value;
                do
                {
                    value = GetInteger();
                }
                while (dict.ContainsKey(value));

                dict.Add(value, 0);

                yield return value;

                integersCount--;
            }
        }

        public IEnumerable<int> GetUniqueIntegers(int integersCount, int maxLimit)
        {
            var dict = new Dictionary<int, int>();

            while (integersCount > 0)
            {
                int value;
                do
                {
                    value = GetInteger(maxLimit);
                }
                while (dict.ContainsKey(value));

                dict.Add(value, 0);

                yield return value;

                integersCount--;
            }
        }

        public IEnumerable<int> GetUniqueIntegers(int integersCount, int minLimit, int maxLimit)
        {
            var dict = new Dictionary<int, int>();

            while (integersCount > 0)
            {
                int value;
                do
                {
                    value = GetInteger(minLimit, maxLimit);
                }
                while (dict.ContainsKey(value));

                dict.Add(value, 0);

                yield return value;

                integersCount--;
            }
        }


        public IEnumerable<T> GetPermutation<T>(IEnumerable<T> valuesList)
        {
            var inputsList = valuesList.ToList();
            var count = inputsList.Count;

            while (count > 1)
            {
                var i = _random.Next(count);
                var value = inputsList[i];

                inputsList.RemoveAt(i);
                count--;

                yield return value;
            }

            if (inputsList.Count == 1)
                yield return inputsList[0];
        }

        public IEnumerable<int> GetRangePermutation(int maxLimit)
        {
            return GetPermutation(
                maxLimit >= 0
                ? Enumerable.Range(0, maxLimit + 1)
                : Enumerable.Range(maxLimit, -maxLimit + 1)
                );
        }

        public IEnumerable<int> GetRangePermutation(int minLimit, int maxLimit)
        {
            return GetPermutation(
                maxLimit >= minLimit
                ? Enumerable.Range(minLimit, maxLimit - minLimit + 1)
                : Enumerable.Range(maxLimit, minLimit - maxLimit + 1)
                );
        }


        public double[,] GetSymmetricArray(int n)
        {
            var array = new double[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    var value = GetScalar();

                    array[i, j] = value;

                    if (i != j)
                        array[i, j] = value;
                }
            }

            return array;
        }


        #region Random Numeric Multivectors

        public GaTerm<double> GetNumTerm(int basisBladeId)
        {
            return new GaTerm<double>(
                basisBladeId,
                GetScalar()
            );
        }

        public GaTerm<double> GetNumTerm(int basisBladeId, double maxValue)
        {
            return new GaTerm<double>(
                basisBladeId,
                GetScalar(maxValue)
            );
        }

        public GaTerm<double> GetNumTerm(int basisBladeId, double minValue, double maxValue)
        {
            return new GaTerm<double>(
                basisBladeId,
                GetScalar(minValue, maxValue)
            );
        }

        public GaTerm<double> GetNumTerm(int grade, int index)
        {
            return new GaTerm<double>(
                GaNumFrameUtils.BasisBladeId(grade, index),
                GetScalar()
            );
        }

        public GaTerm<double> GetNumTerm(int grade, int index, double maxValue)
        {
            return new GaTerm<double>(
                GaNumFrameUtils.BasisBladeId(grade, index),
                GetScalar(maxValue)
            );
        }

        public GaTerm<double> GetNumTerm(int grade, int index, double minValue, double maxValue)
        {
            return new GaTerm<double>(
                GaNumFrameUtils.BasisBladeId(grade, index),
                GetScalar(minValue, maxValue)
            );
        }


        public IEnumerable<GaTerm<double>> GetNumTerms(int vSpaceDim, int count)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

            while (count > 0)
            {
                yield return new GaTerm<double>(
                    GetInteger(gaSpaceDim - 1),
                    GetScalar()
                );

                count--;
            }
        }

        public IEnumerable<GaTerm<double>> GetNumTerms(int vSpaceDim, int count, double maxValue)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

            while (count > 0)
            {
                yield return new GaTerm<double>(
                    GetInteger(gaSpaceDim - 1),
                    GetScalar(maxValue)
                );

                count--;
            }
        }

        public IEnumerable<GaTerm<double>> GetNumTerms(int vSpaceDim, int count, double minValue, double maxValue)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

            while (count > 0)
            {
                yield return new GaTerm<double>(
                    GetInteger(gaSpaceDim - 1),
                    GetScalar(minValue, maxValue)
                );

                count--;
            }
        }


        public IEnumerable<GaTerm<double>> GetNumFullMultivectorTerms(int vSpaceDim)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                yield return new GaTerm<double>(basisBladeId, GetScalar());
        }

        public IEnumerable<GaTerm<double>> GetNumFullMultivectorTerms(int vSpaceDim, double maxValue)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                yield return new GaTerm<double>(basisBladeId, GetScalar(maxValue));
        }

        public IEnumerable<GaTerm<double>> GetNumFullMultivectorTerms(int vSpaceDim, double minValue, double maxValue)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                yield return new GaTerm<double>(basisBladeId, GetScalar(minValue, maxValue));
        }


        public IEnumerable<GaTerm<double>> GetNumSparseMultivectorTerms(int vSpaceDim)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();
            var termsCount = GetInteger(gaSpaceDim);
            var termIDs = GetUniqueIntegers(termsCount, gaSpaceDim - 1);

            foreach (var id in termIDs)
                yield return new GaTerm<double>(id, GetScalar());
        }

        public IEnumerable<GaTerm<double>> GetNumSparseMultivectorTerms(int vSpaceDim, double maxValue)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();
            var termsCount = GetInteger(gaSpaceDim);
            var termIDs = GetUniqueIntegers(termsCount, gaSpaceDim - 1);

            foreach (var id in termIDs)
                yield return new GaTerm<double>(id, GetScalar(maxValue));
        }

        public IEnumerable<GaTerm<double>> GetNumSparseMultivectorTerms(int vSpaceDim, double minValue, double maxValue)
        {
            var gaSpaceDim = vSpaceDim.ToGaSpaceDimension();
            var termsCount = GetInteger(gaSpaceDim);
            var termIDs = GetUniqueIntegers(termsCount, gaSpaceDim - 1);

            foreach (var id in termIDs)
                yield return new GaTerm<double>(id, GetScalar(minValue, maxValue));
        }


        public IEnumerable<GaTerm<double>> GetNumMultivectorTerms(params int[] basisBladeIDs)
        {
            return basisBladeIDs.Select(basisBladeId =>
                new GaTerm<double>(basisBladeId, GetScalar())
            );
        }

        public IEnumerable<GaTerm<double>> GetNumMultivectorTerms(double maxValue, params int[] basisBladeIDs)
        {
            return basisBladeIDs.Select(basisBladeId =>
                new GaTerm<double>(basisBladeId, GetScalar(maxValue))
            );
        }

        public IEnumerable<GaTerm<double>> GetNumMultivectorTerms(double minValue, double maxValue, params int[] basisBladeIDs)
        {
            return basisBladeIDs.Select(basisBladeId => 
                new GaTerm<double>(basisBladeId, GetScalar(minValue, maxValue))
            );
        }


        public IEnumerable<GaTerm<double>> GetNumMultivectorTerms(IEnumerable<int> basisBladeIDs)
        {
            return basisBladeIDs.Select(basisBladeId =>
                new GaTerm<double>(basisBladeId, GetScalar())
            );
        }

        public IEnumerable<GaTerm<double>> GetNumMultivectorTerms(double maxValue, IEnumerable<int> basisBladeIDs)
        {
            return basisBladeIDs.Select(basisBladeId =>
                new GaTerm<double>(basisBladeId, GetScalar(maxValue))
            );
        }

        public IEnumerable<GaTerm<double>> GetNumMultivectorTerms(double minValue, double maxValue, IEnumerable<int> basisBladeIDs)
        {
            return basisBladeIDs.Select(basisBladeId =>
                new GaTerm<double>(basisBladeId, GetScalar(minValue, maxValue))
            );
        }


        public IEnumerable<GaTerm<double>> GetNumFullVectorTerms(int vSpaceDim)
        {
            for (var index = 0; index < vSpaceDim; index++)
                yield return new GaTerm<double>(1 << index, GetScalar());
        }

        public IEnumerable<GaTerm<double>> GetNumFullVectorTerms(int vSpaceDim, double maxValue)
        {
            for (var index = 0; index < vSpaceDim; index++)
                yield return new GaTerm<double>(1 << index, GetScalar(maxValue));
        }

        public IEnumerable<GaTerm<double>> GetNumFullVectorTerms(int vSpaceDim, double minValue, double maxValue)
        {
            for (var index = 0; index < vSpaceDim; index++)
                yield return new GaTerm<double>(1 << index, GetScalar(minValue, maxValue));
        }


        public IEnumerable<GaTerm<double>> GetNumFullKVectorTerms(int vSpaceDim, int grade)
        {
            var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            for (var index = 0; index < kvSpaceDim; index++)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, index), 
                    GetScalar()
                );
        }

        public IEnumerable<GaTerm<double>> GetNumFullKVectorTerms(int vSpaceDim, int grade, double maxValue)
        {
            var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            for (var index = 0; index < kvSpaceDim; index++)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, index), 
                    GetScalar(maxValue)
                );
        }

        public IEnumerable<GaTerm<double>> GetNumFullKVectorTerms(int vSpaceDim, int grade, double minValue, double maxValue)
        {
            var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            for (var index = 0; index < kvSpaceDim; index++)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, index), 
                    GetScalar(minValue, maxValue)
                );
        }


        public IEnumerable<GaTerm<double>> GetNumSparseVectorTerms(int vSpaceDim)
        {
            var termsCount = GetInteger(vSpaceDim);
            var indexList = GetUniqueIntegers(termsCount, vSpaceDim - 1);

            foreach (var index in indexList)
                yield return new GaTerm<double>(
                    1 << index, 
                    GetScalar()
                );
        }

        public IEnumerable<GaTerm<double>> GetNumSparseVectorTerms(int vSpaceDim, double maxValue)
        {
            var termsCount = GetInteger(vSpaceDim);
            var indexList = GetUniqueIntegers(termsCount, vSpaceDim - 1);

            foreach (var index in indexList)
                yield return new GaTerm<double>(
                    1 << index,
                    GetScalar(maxValue)
                );
        }

        public IEnumerable<GaTerm<double>> GetNumSparseVectorTerms(int vSpaceDim, double minValue, double maxValue)
        {
            var termsCount = GetInteger(vSpaceDim);
            var indexList = GetUniqueIntegers(termsCount, vSpaceDim - 1);

            foreach (var index in indexList)
                yield return new GaTerm<double>(
                    1 << index,
                    GetScalar(minValue, maxValue)
                );
        }


        public IEnumerable<GaTerm<double>> GetNumSparseKVectorTerms(int vSpaceDim, int grade)
        {
            var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            var termsCount = GetInteger(kvSpaceDim);
            var indexList = GetUniqueIntegers(termsCount, kvSpaceDim - 1);

            foreach (var index in indexList)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, index),
                    GetScalar()
                );
        }

        public IEnumerable<GaTerm<double>> GetNumSparseKVectorTerms(int vSpaceDim, int grade, double maxValue)
        {
            var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            var termsCount = GetInteger(kvSpaceDim);
            var indexList = GetUniqueIntegers(termsCount, kvSpaceDim - 1);

            foreach (var index in indexList)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, index),
                    GetScalar(maxValue)
                );
        }

        public IEnumerable<GaTerm<double>> GetNumSparseKVectorTerms(int vSpaceDim, int grade, double minValue, double maxValue)
        {
            var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            var termsCount = GetInteger(kvSpaceDim);
            var indexList = GetUniqueIntegers(termsCount, kvSpaceDim - 1);

            foreach (var index in indexList)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, index),
                    GetScalar(minValue, maxValue)
                );
        }

        
        public GaNumSarMultivector GetNumBlade(int vSpaceDim, int grade)
        {
            if (grade < 0 || grade > vSpaceDim)
                return GaNumSarMultivector.CreateZero(vSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetNumFullKVectorTerms(vSpaceDim, grade).CreateSarMultivector(vSpaceDim);

            var mv = GetNumFullVectorTerms(vSpaceDim).CreateSarMultivector(vSpaceDim);
            grade--;

            while (grade > 0)
            {
                var v = GetNumFullVectorTerms(vSpaceDim).CreateSarMultivector(vSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) 
                    continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public GaNumSarMultivector GetNumNonNullVector(GaNumFrame frame)
        {
            GaNumSarMultivector mv;

            do
                mv = GetNumFullVectorTerms(frame.VSpaceDimension).CreateSarMultivector(frame.VSpaceDimension);
            while (!frame.Norm2(mv).IsNearZero());

            return mv;
        }

        public GaNumSarMultivector GetNumVersor(GaNumFrame frame, int vectorsCount)
        {
            var mv = GetNumNonNullVector(frame);
            vectorsCount--;

            while (vectorsCount > 0)
            {
                mv = frame.Gp[mv, GetNumNonNullVector(frame)];
                vectorsCount--;
            }

            return mv;
        }
        #endregion
    }
}
