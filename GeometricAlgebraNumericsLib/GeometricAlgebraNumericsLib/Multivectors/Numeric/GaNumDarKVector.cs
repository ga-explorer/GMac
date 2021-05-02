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
            var scalarValues = new EmptyReadOnlyList<double>((int)kvSpaceDim);

            return new GaNumDarKVector(vSpaceDim, grade, scalarValues);
        }

        public static GaNumDarKVector Create(int vSpaceDim, int grade, IReadOnlyList<double> scalarValues)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (scalarValues.Count == (int)kvSpaceDim)
                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector Create(int vSpaceDim, int grade, params double[] scalarValues)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (scalarValues.Length == (int)kvSpaceDim)
                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector Create(int vSpaceDim, int grade, IEnumerable<double> scalarValues)
        {
            var scalarValuesArray = scalarValues.ToArray();

            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (scalarValuesArray.Length == (int)kvSpaceDim)
                return new GaNumDarKVector(vSpaceDim, grade, scalarValuesArray);

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector CreateScalar(int vSpaceDim, double value)
        {
            var scalarValues = new SingleItemReadOnlyList<double>(1, 0, value);

            return new GaNumDarKVector(vSpaceDim, 0, scalarValues);
        }

        public static GaNumDarKVector CreateFromColumn(int vSpaceDim, int grade, Matrix matrix, ulong col)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if ((ulong)matrix.RowCount == kvSpaceDim)
            {
                var scalarValues = matrix.CreateReadOnlyColumn((int)col);

                return new GaNumDarKVector(vSpaceDim, grade, scalarValues);
            }

            throw new InvalidOperationException();
        }

        public static GaNumDarKVector CreateFromRow(int vSpaceDim, int grade, Matrix matrix, int row)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(vSpaceDim, grade);

            if (matrix.ColumnCount == (int)kvSpaceDim)
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

        public ulong KvSpaceDimension 
            => (ulong)ScalarValuesArray.Count;

        public override double this[ulong id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == Grade)
                    return ScalarValuesArray[(int)index];

                return 0.0d;
            }
        }

        public override double this[int grade, ulong index]
        {
            get
            {
                if (grade == Grade)
                    return ScalarValuesArray[(int)index];

                return 0.0d;
            }
        }

        public override int StoredTermsCount
            => ScalarValuesArray.Count;


        internal GaNumDarKVector(int vSpaceDim, int grade, IReadOnlyList<double> scalarValues)
            : base(vSpaceDim)
        {
            Debug.Assert(
                scalarValues.Count == (int)GaFrameUtils.KvSpaceDimension(vSpaceDim, grade)
            );

            Grade = grade;
            ScalarValuesArray = scalarValues;
        }


        public ulong GetBasisBladeId(ulong index)
        {
            return GaFrameUtils.BasisBladeId(Grade, index);
        }

        public ulong GetBasisBladeIndex(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == Grade)
                return index;

            throw new IndexOutOfRangeException();
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            return ScalarValuesArray.Select((v, i) =>
                new GaTerm<double>(GaFrameUtils.BasisBladeId(Grade, (ulong)i), v)
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
            for (var index = 0UL; index < (ulong)ScalarValuesArray.Count; index++)
            {
                var value = ScalarValuesArray[(int)index];

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


        public override IEnumerable<ulong> GetStoredTermIds()
        {
            return Enumerable
                .Range(0, ScalarValuesArray.Count)
                .Select(index => GaFrameUtils.BasisBladeId(Grade, (ulong)index));
        }

        public override IEnumerable<ulong> GetNonZeroTermIds()
        {
            for (var index = 0; index < ScalarValuesArray.Count; index++)
            {
                if (!ScalarValuesArray[index].IsNearZero())
                    yield return GaFrameUtils.BasisBladeId(Grade, (ulong)index);
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

        public override bool ContainsStoredTerm(ulong id)
        {
            return Grade == id.BasisBladeGrade();
        }

        public override bool ContainsStoredTerm(int grade, ulong index)
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
            var kVectorsArray = new Dictionary<ulong, double>[VSpaceDimension + 1];

            kVectorsArray[Grade] = new Dictionary<ulong, double>();

            for (var index = 0UL; index < (ulong)ScalarValuesArray.Count; index++)
            {
                var value = ScalarValuesArray[(int)index];

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
                new ReadOnlyListAsULongReadOnlyDictionary<double>(ScalarValuesArray);

            return new GaNumSarKVector(VSpaceDimension, Grade, scalarValues);
        }


        public override bool TryGetValue(ulong id, out double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade)
            {
                value = ScalarValuesArray[(int)index];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetValue(int grade, ulong index, out double value)
        {
            if (Grade == grade)
            {
                value = ScalarValuesArray[(int)index];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<double> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade)
            {
                term = new GaTerm<double>(id, ScalarValuesArray[(int)index]);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<double> term)
        {
            if (Grade == grade)
            {
                term = new GaTerm<double>(GaFrameUtils.BasisBladeId(grade, index), ScalarValuesArray[(int)index]);
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