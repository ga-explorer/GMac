using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraNumericsLib.Structures.Collections;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// An immutable vector multivector
    /// </summary>
    public sealed class GaNumVector : GaNumMultivector, IGaNumVector
    {
        public static GaNumVector CreateZero(int vSpaceDim)
        {
            return new GaNumVector(
                new EmptyReadOnlyList<double>(vSpaceDim)
            );
        }

        public static GaNumVector Create(IReadOnlyList<double> scalarValuesList)
        {
            return new GaNumVector(scalarValuesList);
        }

        public static GaNumVector Create(params double[] scalarValuesArray)
        {
            return new GaNumVector(scalarValuesArray);
        }

        public static GaNumVector Create(IEnumerable<double> scalarValues)
        {
            return new GaNumVector(scalarValues.ToArray());
        }

        public static GaNumVector CreateFromColumn(Matrix matrix, int col)
        {
            return new GaNumVector(matrix.CreateReadOnlyColumn(col));
        }

        public static GaNumVector CreateFromRow(Matrix matrix, int row)
        {
            return new GaNumVector(matrix.CreateReadOnlyRow(row));
        }


        public static GaNumVector operator -(GaNumVector mv)
        {
            var scalarValues = new double[mv.VSpaceDimension];

            for (var index = 0; index < mv.VSpaceDimension; index++)
                scalarValues[index] = -mv.ScalarValuesArray[index];

            return new GaNumVector(scalarValues);
        }

        public static GaNumVector operator +(GaNumVector mv1, GaNumVector mv2)
        {
            var scalarValues = new double[mv1.VSpaceDimension];

            for (var index = 0; index < mv1.VSpaceDimension; index++)
                scalarValues[index] = mv1.ScalarValuesArray[index] + mv2.ScalarValuesArray[index];

            return new GaNumVector(scalarValues);
        }

        public static GaNumVector operator -(GaNumVector mv1, GaNumVector mv2)
        {
            var scalarValues = new double[mv1.VSpaceDimension];

            for (var index = 0; index < mv1.VSpaceDimension; index++)
                scalarValues[index] = mv1.ScalarValuesArray[index] - mv2.ScalarValuesArray[index];

            return new GaNumVector(scalarValues);
        }

        public static GaNumVector operator *(GaNumVector mv1, double s)
        {
            var scalarValues = new double[mv1.VSpaceDimension];

            for (var index = 0; index < mv1.VSpaceDimension; index++)
                scalarValues[index] = s * mv1.ScalarValuesArray[index];

            return new GaNumVector(scalarValues);
        }

        public static GaNumVector operator *(double s, GaNumVector mv1)
        {
            var scalarValues = new double[mv1.VSpaceDimension];

            for (var index = 0; index < mv1.VSpaceDimension; index++)
                scalarValues[index] = s * mv1.ScalarValuesArray[index];

            return new GaNumVector(scalarValues);
        }

        public static GaNumVector operator /(GaNumVector mv1, double s)
        {
            var scalarValues = new double[mv1.VSpaceDimension];
            s = 1.0d / s;

            for (var index = 0; index < mv1.VSpaceDimension; index++)
                scalarValues[index] = s * mv1.ScalarValuesArray[index];

            return new GaNumVector(scalarValues);
        }


        public int Grade
            => 1;

        public int MaxStoredGrade
            => 1;

        public IReadOnlyList<double> ScalarValuesArray { get; }

        public int KvSpaceDimension
            => VSpaceDimension;

        public override double this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == 1)
                    return ScalarValuesArray[index];

                return 0.0d;
            }
        }

        public override double this[int grade, int index]
        {
            get
            {
                if (grade == 1)
                    return ScalarValuesArray[index];

                return 0.0d;
            }
        }

        public override int StoredTermsCount
            => ScalarValuesArray.Count;


        internal GaNumVector(IReadOnlyList<double> scalarValuesList)
            : base(scalarValuesList.Count)
        {
            ScalarValuesArray = scalarValuesList;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            return ScalarValuesArray.Select((v, i) => new GaTerm<double>(1 << i, v));
        }

        public override IEnumerable<GaTerm<double>> GetStoredTerms(int grade)
        {
            return grade == 1
                ? GetStoredTerms()
                : Enumerable.Empty<GaTerm<double>>();
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var index = 0; index < ScalarValuesArray.Count; index++)
            {
                var value = ScalarValuesArray[index];

                if (!value.IsNearZero())
                    yield return new GaTerm<double>(1 << index, value);
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms(int grade)
        {
            return grade == 1
                ? GetNonZeroTerms()
                : Enumerable.Empty<GaTerm<double>>();
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            return Enumerable
                .Range(0, ScalarValuesArray.Count)
                .Select(index => GaNumFrameUtils.BasisBladeId(1, index));
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            for (var index = 0; index < ScalarValuesArray.Count; index++)
            {
                if (!ScalarValuesArray[index].IsNearZero())
                    yield return GaNumFrameUtils.BasisBladeId(1, index);
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


        public int GetBasisBladeId(int index)
        {
            return GaNumFrameUtils.BasisBladeId(1, index);
        }

        public int GetBasisBladeIndex(int id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1)
                return index;

            throw new IndexOutOfRangeException();
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
            return ScalarValuesArray.All(v => v.IsNearZero());
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
            return id.BasisBladeGrade() == 1;
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            return grade == 1;
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues =
                new GaVectorAsDarMultivectorReadOnlyList<double>(ScalarValuesArray);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var scalarValues = new IReadOnlyList<double>[VSpaceDimension + 1];

            scalarValues[1] = ScalarValuesArray;

            return new GaNumDgrMultivector(scalarValues);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues =
                new GaDarVectorAsSarMultivectorReadOnlyDictionary<double>(ScalarValuesArray);

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var scalarValues = new Dictionary<int, double>[VSpaceDimension + 1];

            scalarValues[1] = new Dictionary<int, double>();

            for (var index = 0; index < VSpaceDimension; index++)
            {
                var value = ScalarValuesArray[index];

                if (!value.IsNearZero())
                    scalarValues[1].Add(index, value);
            }

            return new GaNumSgrMultivector(scalarValues);
        }

        public GaNumDarKVector ToDarKVector()
        {
            return new GaNumDarKVector(VSpaceDimension, 1, ScalarValuesArray);
        }

        public GaNumSarKVector ToSarKVector()
        {
            var scalarValues = new ReadOnlyListAsReadOnlyDictionary<double>(ScalarValuesArray);

            return new GaNumSarKVector(VSpaceDimension, 1, scalarValues);
        }


        public override bool TryGetValue(int id, out double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (grade == 1)
            {
                value = ScalarValuesArray[index];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            if (grade == 1)
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

            if (grade == 1)
            {
                term = new GaTerm<double>(id, ScalarValuesArray[index]);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            if (grade == 1)
            {
                term = new GaTerm<double>(GaNumFrameUtils.BasisBladeId(grade, index), ScalarValuesArray[index]);
                return true;
            }

            term = null;
            return false;
        }


        public override IGaNumKVector GetKVectorPart(int grade)
        {
            return grade == 1
                ? (IGaNumKVector)this
                : GaNumDarKVector.CreateZero(VSpaceDimension, grade);
        }

        public override IGaNumVector GetVectorPart()
        {
            return this;
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            yield return 1;
        }

        public override int GetStoredGradesBitPattern()
        {
            return 2;
        }

        public override bool ContainsStoredKVector(int grade)
        {
            return grade == 1;
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            yield return this;
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

            return new GaNumVector(scalarValues);
        }

        public override IGaNumMultivector GetReverse()
        {
            return 1.GradeHasNegativeReverse()
                ? GetNegative()
                : this;
        }

        public override IGaNumMultivector GetGradeInv()
        {
            return 1.GradeHasNegativeGradeInv()
                ? GetNegative()
                : this;
        }

        public override IGaNumMultivector GetCliffConj()
        {
            return 1.GradeHasNegativeCliffConj()
                ? GetNegative()
                : this;
        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumDgrMultivectorFactory(this);
        }
    }
}