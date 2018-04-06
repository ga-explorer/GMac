using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacMath.Numeric;
using GMac.GMacMath.Numeric.Frames;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Products;
using GMac.GMacMath.Symbolic;
using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Products;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;
using Wolfram.NETLink;

namespace GMac.GMacMath
{
    public sealed class GMacRandomGenerator
    {
        private Random _random;

        public double MinScalarLimit { get; private set; } = 0.0d;

        public double MaxScalarLimit { get; private set; } = 1.0d;

        public int MinIntegerLimit { get; private set; } = 0;

        public int MaxIntegerLimit { get; private set; } = int.MaxValue - 1;


        public GMacRandomGenerator()
        {
            _random = new Random();
        }

        public GMacRandomGenerator(int seed)
        {
            _random = new Random(seed);
        }


        public GMacRandomGenerator ResetGenerator()
        {
            _random = new Random();

            return this;
        }

        public GMacRandomGenerator ResetGenerator(int seed)
        {
            _random = new Random(seed);

            return this;
        }

        public GMacRandomGenerator SetScalarLimits()
        {
            MinScalarLimit = 0.0d;
            MaxScalarLimit = 1.0d;

            return this;
        }

        public GMacRandomGenerator SetScalarLimits(double maxLimit)
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

        public GMacRandomGenerator SetScalarLimits(double minLimit, double maxLimit)
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

        public GMacRandomGenerator SetIntegerLimits()
        {
            MinIntegerLimit = 0;
            MaxIntegerLimit = Int32.MaxValue - 1;

            return this;
        }

        public GMacRandomGenerator SetIntegerLimits(int maxLimit)
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

        public GMacRandomGenerator SetIntegerLimits(int minLimit, int maxLimit)
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

        public Expr GetScalarExpr()
        {
            return GetScalar().ToExpr();
        }

        public Expr GetScalarExpr(double maxLimit)
        {
            return (_random.NextDouble() * maxLimit).ToExpr();
        }

        public Expr GetScalarExpr(double minLimit, double maxLimit)
        {
            return GetScalar(minLimit, maxLimit).ToExpr();
        }

        public MathematicaScalar GetSymbolicScalar()
        {
            return MathematicaScalar.Create(SymbolicUtils.Cas, GetScalar());
        }

        public MathematicaScalar GetSymbolicScalar(double maxLimit)
        {
            return MathematicaScalar.Create(SymbolicUtils.Cas, _random.NextDouble() * maxLimit);
        }

        public MathematicaScalar GetSymbolicScalar(double minLimit, double maxLimit)
        {
            return MathematicaScalar.Create(SymbolicUtils.Cas, GetScalar(minLimit, maxLimit));
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

        public Expr GetIntegerExpr()
        {
            return GetInteger().ToExpr();
        }

        public Expr GetIntegerExpr(int maxLimit)
        {
            return GetInteger(maxLimit).ToExpr();
        }

        public Expr GetIntegerExpr(int minLimit, int maxLimit)
        {
            return GetInteger(minLimit, maxLimit).ToExpr();
        }

        public MathematicaScalar GetSymbolicInteger()
        {
            return MathematicaScalar.Create(SymbolicUtils.Cas, GetInteger());
        }

        public MathematicaScalar GetSymbolicInteger(int maxLimit)
        {
            return MathematicaScalar.Create(SymbolicUtils.Cas, GetInteger(maxLimit));
        }

        public MathematicaScalar GetSymbolicInteger(int minLimit, int maxLimit)
        {
            return MathematicaScalar.Create(SymbolicUtils.Cas, GetInteger(minLimit, maxLimit));
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


        #region Random Symbolic Multivectors
        public GaSymMultivector GetSymMultivectorFull(int gaSpaceDim)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymMultivectorFull(int gaSpaceDim, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar(maxValue));

            return mv;
        }

        public GaSymMultivector GetSymMultivectorFull(int gaSpaceDim, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar(minValue, maxValue));

            return mv;
        }

        public GaSymMultivector GetSymMultivectorByTerms(int gaSpaceDim, params int[] basisBladeIDs)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymMultivectorByTerms(int gaSpaceDim, IEnumerable<int> basisBladeIDs)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymMultivectorByGrades(int gaSpaceDim, params int[] grades)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeIDs = 
                GMacMathUtils.BasisBladeIDsOfGrades(
                    mv.VSpaceDimension, 
                    grades
                    );

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymMultivector(int gaSpaceDim)
        {
            //Randomly select the number of terms in the multivector
            var termsCount = GetInteger(gaSpaceDim - 1);

            //Randomly select the terms basis blades in the multivectors
            var basisBladeIDs = GetRangePermutation(gaSpaceDim - 1).Take(termsCount);

            //Randomly generate the multivector's coefficients
            return GetSymMultivectorByTerms(gaSpaceDim, basisBladeIDs);
        }

        public GaSymMultivector GetSymMultivector(int gaSpaceDim, string baseCoefName)
        {
            //Randomly select the number of terms in the multivector
            var termsCount = GetInteger(gaSpaceDim - 1);

            //Randomly select the terms basis blades in the multivectors
            var basisBladeIDs = GetRangePermutation(gaSpaceDim - 1).Take(termsCount);

            //Generate the multivector's symbolic coefficients
            return GaSymMultivector.CreateSymbolic(gaSpaceDim, baseCoefName, basisBladeIDs);
        }


        public GaSymMultivector GetSymTerm(int gaSpaceDim, string baseCoefName)
        {
            //Randomly select the number of terms in the multivector
            var basisBladeId = GetInteger(gaSpaceDim - 1);

            //Generate the multivector's symbolic coefficients
            return GaSymMultivector.CreateSymbolicTerm(gaSpaceDim, baseCoefName, basisBladeId);
        }


        public GaSymMultivector GetSymVector(int gaSpaceDim)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymVector(int gaSpaceDim, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar(maxValue));

            return mv;
        }

        public GaSymMultivector GetSymVector(int gaSpaceDim, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar(minValue, maxValue));

            return mv;
        }


        public GaSymMultivector GetSymIntegerVector(int gaSpaceDim)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetIntegerExpr());

            return mv;
        }

        public GaSymMultivector GetSymIntegerVector(int gaSpaceDim, int maxLimit)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetIntegerExpr(maxLimit));

            return mv;
        }

        public GaSymMultivector GetSymIntegerVector(int gaSpaceDim, int minLimit, int maxLimit)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetIntegerExpr(minLimit, maxLimit));

            return mv;
        }


        public IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(int gaSpaceDim, int count)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (count < 1 || count > vSpaceDim)
                yield break;

            var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
            while (count > 0)
            {
                var v = GetSymIntegerVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                count--;

                yield return v;
            }
        }

        public IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(int gaSpaceDim, int count, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (count < 1 || count > vSpaceDim)
                yield break;

            var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
            while (count > 0)
            {
                var v = GetSymIntegerVector(gaSpaceDim, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                count--;

                yield return v;
            }
        }

        public IEnumerable<GaSymMultivector> GetSymIntegerLidVectors(int gaSpaceDim, int count, int minValue, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (count < 1 || count > vSpaceDim)
                yield break;

            var mv = GaSymMultivector.CreateScalar(gaSpaceDim, Expr.INT_ONE);
            while (count > 0)
            {
                var v = GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                count--;

                yield return v;
            }
        }


        public GaSymMultivector GetSymKVector(int gaSpaceDim, int grade)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymKVector(int gaSpaceDim, int grade, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar(maxValue));

            return mv;
        }

        public GaSymMultivector GetSymKVector(int gaSpaceDim, int grade, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetSymbolicScalar(minValue, maxValue));

            return mv;
        }


        public GaSymMultivector GetSymIntegerKVector(int gaSpaceDim, int grade)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetIntegerExpr());

            return mv;
        }

        public GaSymMultivector GetSymIntegerKVector(int gaSpaceDim, int grade, int maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetIntegerExpr(maxValue));

            return mv;
        }

        public GaSymMultivector GetSymIntegerKVector(int gaSpaceDim, int grade, int minValue, int maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetIntegerExpr(minValue, maxValue));

            return mv;
        }


        public GaSymMultivector GetSymTerm(int gaSpaceDim, int basisBladeId)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymTerm(int gaSpaceDim, int basisBladeId, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, GetSymbolicScalar(maxValue));

            return mv;
        }

        public GaSymMultivector GetSymTerm(int gaSpaceDim, int basisBladeId, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, GetSymbolicScalar(minValue, maxValue));

            return mv;
        }

        public GaSymMultivector GetSymTerm(int gaSpaceDim, int grade, int index)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GMacMathUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, GetSymbolicScalar());

            return mv;
        }

        public GaSymMultivector GetSymTerm(int gaSpaceDim, int grade, int index, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GMacMathUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, GetSymbolicScalar(maxValue));

            return mv;
        }

        public GaSymMultivector GetSymTerm(int gaSpaceDim, int grade, int index, double minValue, double maxValue)
        {
            var mv = GaSymMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GMacMathUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, GetSymbolicScalar(minValue, maxValue));

            return mv;
        }


        public GaSymMultivector GetSymBlade(int gaSpaceDim, int grade)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetSymKVector(gaSpaceDim, grade);

            var mv = GetSymVector(gaSpaceDim);
            grade--;

            while (grade > 0)
            {
                var v = GetSymVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public GaSymMultivector GetSymBlade(int gaSpaceDim, int grade, double maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetSymKVector(gaSpaceDim, grade, maxValue);

            var mv = GetSymVector(gaSpaceDim, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = GetSymVector(gaSpaceDim, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public GaSymMultivector GetSymBlade(int gaSpaceDim, int grade, double minValue, double maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetSymKVector(gaSpaceDim, grade, minValue, maxValue);

            var mv = GetSymVector(gaSpaceDim, minValue, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = GetSymVector(gaSpaceDim, minValue, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }


        public GaSymMultivector GetSymIntegerBlade(int gaSpaceDim, int grade)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetSymIntegerKVector(gaSpaceDim, grade);

            var mv = GetSymIntegerVector(gaSpaceDim);
            grade--;

            while (grade > 0)
            {
                var v = GetSymIntegerVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public GaSymMultivector GetSymIntegerBlade(int gaSpaceDim, int grade, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetSymIntegerKVector(gaSpaceDim, grade, maxValue);

            var mv = GetSymIntegerVector(gaSpaceDim, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = GetSymIntegerVector(gaSpaceDim, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public GaSymMultivector GetSymIntegerBlade(int gaSpaceDim, int grade, int minValue, int maxValue)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaSymMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetSymIntegerKVector(gaSpaceDim, grade, minValue, maxValue);

            var mv = GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
            grade--;

            while (grade > 0)
            {
                var v = GetSymIntegerVector(gaSpaceDim, minValue, maxValue);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }


        public GaSymMultivector GetSymNonNullVector(GaSymFrame frame)
        {
            GaSymMultivector mv;

            do
                mv = GetSymVector(frame.GaSpaceDimension);
            while (!frame.Norm2(mv).IsZero());

            return mv;
        }


        public GaSymMultivector GetSymVersor(GaSymFrame frame, int vectorsCount)
        {
            var mv = GetSymNonNullVector(frame);
            vectorsCount--;

            while (vectorsCount > 0)
            {
                mv = frame.Gp[mv, GetSymNonNullVector(frame)];
                vectorsCount--;
            }

            return mv;
        }
        #endregion

        #region Random Numeric Multivectors
        public GaNumMultivector GetNumMultivectorFull(int gaSpaceDim)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumMultivectorFull(int gaSpaceDim, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, GetScalar(maxValue));

            return mv;
        }

        public GaNumMultivector GetNumMultivectorFull(int gaSpaceDim, double minValue, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            for (var basisBladeId = 0; basisBladeId < gaSpaceDim; basisBladeId++)
                mv.SetTermCoef(basisBladeId, GetScalar(minValue, maxValue));

            return mv;
        }

        public GaNumMultivector GetNumMultivectorByTerms(int gaSpaceDim, params int[] basisBladeIDs)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumMultivectorByTerms(int gaSpaceDim, IEnumerable<int> basisBladeIDs)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumMultivectorByGrades(int gaSpaceDim, params int[] grades)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            var basisBladeIDs =
                GMacMathUtils.BasisBladeIDsOfGrades(
                    mv.VSpaceDimension,
                    grades
                    );

            foreach (var basisBladeId in basisBladeIDs)
                mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumMultivector(int gaSpaceDim)
        {
            //Randomly select the number of terms in the multivector
            var termsCount = GetInteger(gaSpaceDim - 1);

            //Randomly select the terms basis blades in the multivectors
            var basisBladeIDs = GetRangePermutation(gaSpaceDim - 1).Take(termsCount);

            //Randomly generate the multivector's coefficients
            return GetNumMultivectorByTerms(gaSpaceDim, basisBladeIDs);
        }

        public GaNumMultivector GetNumVector(int gaSpaceDim)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumVector(int gaSpaceDim, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetScalar(maxValue));

            return mv;
        }

        public GaNumMultivector GetNumVector(int gaSpaceDim, double minValue, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, 1))
                mv.SetTermCoef(basisBladeId, GetScalar(minValue, maxValue));

            return mv;
        }

        public GaNumMultivector GetNumKVector(int gaSpaceDim, int grade)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumKVector(int gaSpaceDim, int grade, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetScalar(maxValue));

            return mv;
        }

        public GaNumMultivector GetNumKVector(int gaSpaceDim, int grade, double minValue, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            foreach (var basisBladeId in GMacMathUtils.BasisBladeIDsOfGrade(mv.VSpaceDimension, grade))
                mv.SetTermCoef(basisBladeId, GetScalar(minValue, maxValue));

            return mv;
        }

        public GaNumMultivector GetNumTerm(int gaSpaceDim, int basisBladeId)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumTerm(int gaSpaceDim, int basisBladeId, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, GetScalar(maxValue));

            return mv;
        }

        public GaNumMultivector GetNumTerm(int gaSpaceDim, int basisBladeId, double minValue, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            mv.SetTermCoef(basisBladeId, GetScalar(minValue, maxValue));

            return mv;
        }

        public GaNumMultivector GetNumTerm(int gaSpaceDim, int grade, int index)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GMacMathUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, GetScalar());

            return mv;
        }

        public GaNumMultivector GetNumTerm(int gaSpaceDim, int grade, int index, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GMacMathUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, GetScalar(maxValue));

            return mv;
        }

        public GaNumMultivector GetNumTerm(int gaSpaceDim, int grade, int index, double minValue, double maxValue)
        {
            var mv = GaNumMultivector.CreateZero(gaSpaceDim);

            var basisBladeId = GMacMathUtils.BasisBladeId(grade, index);

            mv.SetTermCoef(basisBladeId, GetScalar(minValue, maxValue));

            return mv;
        }

        public GaNumMultivector GetNumBlade(int gaSpaceDim, int grade)
        {
            var vSpaceDim = gaSpaceDim.ToVSpaceDimension();

            if (grade < 0 || grade > vSpaceDim)
                return GaNumMultivector.CreateZero(gaSpaceDim);

            if (grade <= 1 || grade >= vSpaceDim - 1)
                return GetNumKVector(gaSpaceDim, grade);

            var mv = GetNumVector(gaSpaceDim);
            grade--;

            while (grade > 0)
            {
                var v = GetNumVector(gaSpaceDim);
                var mv1 = mv.Op(v);

                if (mv1.IsZero()) continue;

                mv = mv1;
                grade--;
            }

            return mv;
        }

        public GaNumMultivector GetNumNonNullVector(GaNumFrame frame)
        {
            GaNumMultivector mv;

            do
                mv = GetNumVector(frame.GaSpaceDimension);
            while (!frame.Norm2(mv).IsNearZero());

            return mv;
        }

        public GaNumMultivector GetNumVersor(GaNumFrame frame, int vectorsCount)
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
