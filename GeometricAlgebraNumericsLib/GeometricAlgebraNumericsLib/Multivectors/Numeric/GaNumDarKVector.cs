using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraNumericsLib.Structures.Collections;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// An immutable full-array k-vector
    /// </summary>
    public sealed class GaNumDarKVector : GaNumMultivector, IGaNumKVector
    {
        public static GaNumDarKVector CreateZero(int vSpaceDim, int grade)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);
            var scalarValues = new EmptyReadOnlyList<double>(kvSpaceDim);

            return new GaNumDarKVector(vSpaceDim, grade, scalarValues);
        }

        public static GaNumDarKVector Create(int vSpaceDim, int grade, IReadOnlyList<double> scalarValues)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (scalarValues.Count == kvSpaceDim)
                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector Create(int vSpaceDim, int grade, params double[] scalarValues)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (scalarValues.Length == kvSpaceDim)
                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector Create(int vSpaceDim, int grade, IEnumerable<double> scalarValues)
        {
            var scalarValuesArray = scalarValues.ToArray();

            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (scalarValuesArray.Length == kvSpaceDim)
                return new GaNumDarKVector(vSpaceDim, grade, scalarValuesArray);

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector CreateScalar(int vSpaceDim, double value)
        {
            var scalarValues = new SingleItemReadOnlyList<double>(1, 0, value);

            return new GaNumDarKVector(vSpaceDim, 0, scalarValues);
        }

        public static GaNumDarKVector CreateFromColumn(int vSpaceDim, int grade, Matrix matrix, int col)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (matrix.RowCount == kvSpaceDim)
            {
                var scalarValues = matrix.CreateReadOnlyColumn(col);

                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);
            }

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector CreateFromRow(int vSpaceDim, int grade, Matrix matrix, int row)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (matrix.ColumnCount == kvSpaceDim)
            {
                var scalarValues = matrix.CreateReadOnlyRow(row);

                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);
            }

            throw new InvalidOperationException();
        }


        public int Grade { get; }

        public int MaxStoredGrade
            => Grade;

        public IReadOnlyList<double> ScalarValuesArray { get; }

        public int KvSpaceDimension 
            => ScalarValuesArray.Count;

        public override double this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == Grade)
                    return ScalarValuesArray[index];

                return 0.0d;
            }
        }

        public override double this[int grade, int index]
        {
            get
            {
                if (grade == Grade)
                    return ScalarValuesArray[index];

                return 0.0d;
            }
        }

        public override int StoredTermsCount
            => ScalarValuesArray.Count;


        internal GaNumDarKVector(int vSpaceDim, int grade, IReadOnlyList<double> scalarValues)
            : base(vSpaceDim)
        {
            Debug.Assert(
                scalarValues.Count == GaFrameUtils.KvSpaceDimension(vSpaceDim, grade)
            );

            Grade = grade;
            ScalarValuesArray = scalarValues;
        }


        public int GetBasisBladeId(int index)
        {
            return GaFrameUtils.BasisBladeId(Grade, index);
        }

        public int GetBasisBladeIndex(int id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == Grade)
                return index;

            throw new IndexOutOfRangeException();
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            return ScalarValuesArray.Select((v, i) =>
                new GaTerm<double>(GaFrameUtils.BasisBladeId(Grade, i), v)
            );
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            return Grade == grade
                ? GetStoredTerms()
                : Enumerable.Empty<GaTerm<double>>();
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var index = 0; index < ScalarValuesArray.Count; index++)
            {
                var value = ScalarValuesArray[index];

                if (!value.IsNearZero())
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(Grade, index),
                        value
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            return Grade == grade
                ? GetNonZeroTerms()
                : Enumerable.Empty<GaTerm<double>>();
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            return Enumerable
                .Range(0, ScalarValuesArray.Count)
                .Select(index => GaFrameUtils.BasisBladeId(Grade, index));
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            for (var index = 0; index < ScalarValuesArray.Count; index++)
            {
                if (!ScalarValuesArray[index].IsNearZero())
                    yield return GaFrameUtils.BasisBladeId(Grade, index);
            }
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            return ScalarValuesArray;
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            return ScalarValuesArray.Where(v => !v.IsNearZero());
        }


        public override bool IsTerm()
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

        public override bool IsScalar()
        {
            return Grade == 0 || ScalarValuesArray.All(v => v.IsNearZero());
        }

        public override bool IsZero()
        {
            return ScalarValuesArray.All(v => v.IsNearZero());
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override bool IsNearZero(double epsilon)
        {
            return ScalarValuesArray.All(v => v.IsNearZero());
        }

        public override bool ContainsStoredTerm(int id)
        {
            return Grade == id.BasisBladeGrade();
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            return Grade == grade;
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues = 
                new GaDarKVectorAsDarMultivectorReadOnlyList<double>(VSpaceDimension, Grade, ScalarValuesArray);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            kVectorsArray[Grade] = ScalarValuesArray;

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues =
                new GaDarKVectorAsSarMultivectorReadOnlyDictionary<double>(VSpaceDimension, Grade, ScalarValuesArray);

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var kVectorsArray = new Dictionary<int, double>[VSpaceDimension + 1];

            kVectorsArray[Grade] = new Dictionary<int, double>();

            for (var index = 0; index < ScalarValuesArray.Count; index++)
            {
                var value = ScalarValuesArray[index];

                if (!value.IsNearZero())
                    kVectorsArray[Grade].Add(index, value);
            }

            return new GaNumSgrMultivector(kVectorsArray);
        }


        public GaNumDarKVector ToDarKVector()
        {
            return this;
        }

        public GaNumSarKVector ToSarKVector()
        {
            var scalarValues = 
                new ReadOnlyListAsReadOnlyDictionary<double>(ScalarValuesArray);

            return new GaNumSarKVector(VSpaceDimension, Grade, scalarValues);
        }


        public override bool TryGetValue(int id, out double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade)
            {
                value = ScalarValuesArray[index];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            if (Grade == grade)
            {
                value = ScalarValuesArray[index];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade)
            {
                term = new GaTerm<double>(id, ScalarValuesArray[index]);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            if (Grade == grade)
            {
                term = new GaTerm<double>(GaFrameUtils.BasisBladeId(grade, index), ScalarValuesArray[index]);
                return true;
            }

            term = null;
            return false;
        }


        public override IGaNumVector GetVectorPart()
        {
            return (Grade == 1)
                ? new GaNumVector(ScalarValuesArray)
                : GaNumVector.CreateZero(VSpaceDimension);
        }

        public override IGaNumKVector GetKVectorPart(int grade)
        {
            return Grade == grade
                ? this
                : GaNumDarKVector.CreateZero(VSpaceDimension, grade);
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            yield return this;
        }


        public override IEnumerable<int> GetStoredGrades()
        {
            yield return Grade;
        }

        public override int GetStoredGradesBitPattern()
        {
            return 1 << Grade;
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade == Grade;
        }


        public override IGaGbtNode1<double> GetGbtRootNode()
        {
            return GaGbtJarMultivectorNode.CreateRootNode(this);
        }

        public override IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity)
        {
            return GaGbtNumDgrMultivectorStack1.Create(capacity, this);
        }


        public override IGaNumMultivector GetNegative()
        {
            var scalarValues = 
                ScalarValuesArray.Select(v => -v).ToArray();

            return new GaNumDarKVector(VSpaceDimension, Grade, scalarValues);
        }

        public override IGaNumMultivector GetReverse()
        {
            return Grade.GradeHasNegativeReverse()
                ? GetNegative()
                : this;
        }

        public override IGaNumMultivector GetGradeInv()
        {
            return Grade.GradeHasNegativeGradeInv()
                ? GetNegative()
                : this;
        }

        public override IGaNumMultivector GetCliffConj()
        {
            return Grade.GradeHasNegativeCliffConj()
                ? GetNegative()
                : this;
        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumDgrMultivectorFactory(this);
        }
    }
}