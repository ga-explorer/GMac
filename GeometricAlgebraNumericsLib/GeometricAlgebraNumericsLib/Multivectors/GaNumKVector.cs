using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public sealed class GaNumKVector : IGaNumMultivector
    {
        public static GaNumKVector Create(int gaSpaceDim, int grade)
        {
            return new GaNumKVector(gaSpaceDim, grade);
        }

        public static GaNumKVector Create(int gaSpaceDim, int grade, params double[] scalarValues)
        {
            var mv = new GaNumKVector(gaSpaceDim, grade);

            var n = Math.Min(mv.TermsCount, scalarValues.Length);
            for (var i = 0; i < n; i++)
                mv.ScalarValuesArray[i] = scalarValues[i];

            return mv;
        }

        public static GaNumKVector Create(int gaSpaceDim, int grade, IEnumerable<double> scalarValues)
        {
            var mv = new GaNumKVector(gaSpaceDim, grade);

            var i = 0;
            foreach (var scalarValue in scalarValues)
            {
                mv.ScalarValuesArray[i] = scalarValue;

                i++;
                if (i >= mv.ScalarValuesArray.Length)
                    break;
            }

            return mv;
        }

        public static GaNumKVector CreateScalar(int gaSpaceDim)
        {
            return new GaNumKVector(gaSpaceDim, 0);
        }

        public static GaNumKVector CreateScalar(int gaSpaceDim, double value)
        {
            var mv = new GaNumKVector(gaSpaceDim, 0);

            mv.ScalarValuesArray[0] = value;

            return mv;
        }

        public static GaNumKVector CreateVectorFromColumn(Matrix matrix, int col)
        {
            var gaSpaceDim = matrix.RowCount.ToGaSpaceDimension();

            var mv = new GaNumKVector(gaSpaceDim, 1);

            for (var row = 0; row < matrix.RowCount; row++)
                mv.ScalarValuesArray[row] = matrix[row, col];

            return mv;
        }


        public double[] ScalarValuesArray { get; }


        public int Grade { get; }

        public int VSpaceDimension
            => GaSpaceDimension.ToVSpaceDimension();

        public int GaSpaceDimension { get; }

        public double this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == Grade)
                    return ScalarValuesArray[index];

                return 0.0d;
            }
        }

        public double this[int grade, int index]
        {
            get
            {
                if (grade == Grade)
                    return ScalarValuesArray[index];

                return 0.0d;
            }
        }

        public IEnumerable<int> BasisBladeIds 
            => Enumerable
                .Range(0, ScalarValuesArray.Length)
                .Select(index => GaNumFrameUtils.BasisBladeId(Grade, index));

        public IEnumerable<int> NonZeroBasisBladeIds
        {
            get
            {
                for (var index = 0; index < ScalarValuesArray.Length; index++)
                {
                    if (!ScalarValuesArray[index].IsNearZero())
                        yield return GaNumFrameUtils.BasisBladeId(Grade, index);
                }
            }
        }

        public IEnumerable<double> BasisBladeScalars
            => ScalarValuesArray;

        public IEnumerable<double> NonZeroBasisBladeScalars
            => ScalarValuesArray.Where(v => !v.IsNearZero());

        public IEnumerable<KeyValuePair<int, double>> Terms
            => ScalarValuesArray.Select((v, i) => 
                new KeyValuePair<int, double>(GaNumFrameUtils.BasisBladeId(Grade, i), v)
            );

        public IEnumerable<KeyValuePair<int, double>> NonZeroTerms
        {
            get
            {
                for (var index = 0; index < ScalarValuesArray.Length; index++)
                {
                    var value = ScalarValuesArray[index];

                    if (!value.IsNearZero())
                        yield return new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(Grade, index), 
                            value
                        );
                }
            }
        }

        public bool IsTemp 
            => false;

        public int TermsCount 
            => ScalarValuesArray.Length;


        private GaNumKVector(int gaSpaceDim, int grade)
        {
            GaSpaceDimension = gaSpaceDim;
            Grade = grade;

            ScalarValuesArray = new double[
                GaNumFrameUtils.KvSpaceDimension(gaSpaceDim.ToVSpaceDimension(), grade)
            ];
        }


        public double GetTermValue(int index)
        {
            return ScalarValuesArray[index];
        }

        public void SetTermValue(int index, double value)
        {
            ScalarValuesArray[index] = value;
        }

        public void UpdateTermValue(int index, double deltaValue)
        {
            ScalarValuesArray[index] += deltaValue;
        }

        public int GetBasisBladeId(int index)
        {
            return GaNumFrameUtils.BasisBladeId(Grade, index);
        }

        public int GetBasisBladeIndex(int id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == Grade)
                return index;

            throw new IndexOutOfRangeException();
        }

        public bool IsTerm()
        {
            var count = 0;
            foreach (var value in ScalarValuesArray)
            {
                if (value.IsNearZero())
                    continue;

                count++;

                if (count > 1)
                    return false;
            }

            return true;
        }

        public bool IsScalar()
        {
            return Grade == 0 || ScalarValuesArray.All(v => v.IsNearZero());
        }

        public bool IsZero()
        {
            return ScalarValuesArray.All(v => v.IsNearZero());
        }

        public bool IsEmpty()
        {
            return ScalarValuesArray.All(v => v == 0.0d);
        }

        public bool IsNearZero(double epsilon)
        {
            return ScalarValuesArray.All(v => v.IsNearZero());
        }

        public bool ContainsBasisBlade(int id)
        {
            return Grade == id.BasisBladeGrade();
        }

        public void Simplify()
        {
            for (var i = 0; i < ScalarValuesArray.Length; i++)
                if (ScalarValuesArray[i].IsNearZero())
                    ScalarValuesArray[i] = 0.0d;
        }

        public double[] TermsToArray()
        {
            var termsArray = new double[GaSpaceDimension];

            for (var index = 0; index < ScalarValuesArray.Length; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(Grade, index);

                termsArray[id] = ScalarValuesArray[index];
            }

            return termsArray;
        }

        public GaNumMultivector ToMultivector()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            for (var index = 0; index < ScalarValuesArray.Length; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(Grade, index);

                mv.AddTerm(id, ScalarValuesArray[index]);
            }

            return mv;
        }

        public GaNumMultivector GetVectorPart()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            if (Grade == 1)
            {
                for (var index = 0; index < ScalarValuesArray.Length; index++)
                {
                    var id = GaNumFrameUtils.BasisBladeId(1, index);

                    mv.AddTerm(id, ScalarValuesArray[index]);
                }
            }

            return mv;
        }

        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroTerms.GetEnumerator();
        }
    }
}