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

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// An immutable sparse-array k-vector
    /// </summary>
    public sealed class GaNumSarKVector : GaNumMultivector, IGaNumKVector
    {
        public static GaNumSarKVector CreateZero(int vSpaceDim, int grade)
        {
            var scalarValues = new Dictionary<int, double>();

            return new GaNumSarKVector(vSpaceDim, grade, scalarValues);
        }

        public static GaNumSarKVector Create(int vSpaceDim, int grade, IReadOnlyDictionary<int, double> scalarValues)
        {
            return new GaNumSarKVector(vSpaceDim, grade, scalarValues);
        }

        public static GaNumSarKVector CreateScalar(int vSpaceDim, double value)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {0, value}
            };

            return new GaNumSarKVector(vSpaceDim, 0, scalarValues);
        }
        

        public int Grade { get; }

        public int MaxStoredGrade
            => Grade;

        public IReadOnlyDictionary<int, double> ScalarValuesDictionary { get; }

        public IReadOnlyList<double> ScalarValuesArray { get; }

        public int KvSpaceDimension
            => GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade);

        public override double this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                if (grade == Grade)
                    return ScalarValuesDictionary.TryGetValue(index, out var value) 
                        ? value : 0.0d;

                return 0.0d;
            }
        }

        public override double this[int grade, int index]
        {
            get
            {
                if (grade == Grade)
                    return ScalarValuesDictionary.TryGetValue(index, out var value)
                        ? value : 0.0d;

                return 0.0d;
            }
        }

        public override int StoredTermsCount
            => ScalarValuesDictionary.Count;


        internal GaNumSarKVector(int vSpaceDim, int grade, IReadOnlyDictionary<int, double> scalarValues)
            : base(vSpaceDim)
        {
            Debug.Assert(
                scalarValues.All(p => 
                    p.Key >= 0 && p.Key < GaFrameUtils.KvSpaceDimension(vSpaceDim, grade)
                )
            );

            Grade = grade;
            ScalarValuesDictionary = scalarValues;
            ScalarValuesArray = new SparseReadOnlyList<double>(vSpaceDim, scalarValues);
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


        public override bool IsTerm()
        {
            var count = 0;
            foreach (var value in GetNonZeroTermScalars())
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
            return Grade == 0 || GetStoredTermScalars().All(v => v.IsNearZero());
        }

        public override bool IsZero()
        {
            return GetStoredTermScalars().All(v => v.IsNearZero());
        }

        public override bool IsEmpty()
        {
            return ScalarValuesDictionary.Count == 0;
        }

        public override bool IsNearZero(double epsilon)
        {
            return GetStoredTermScalars().All(v => v.IsNearZero());
        }

        public override bool ContainsStoredTerm(int id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return Grade == grade && ScalarValuesDictionary.ContainsKey(index);
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            return Grade == grade && ScalarValuesDictionary.ContainsKey(index);
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues =
                new GaSarKVectorAsDarMultivectorReadOnlyList<double>(VSpaceDimension, Grade, ScalarValuesDictionary);

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
                new GaSarKVectorAsSarMultivectorReadOnlyDictionary<double>(VSpaceDimension, Grade, ScalarValuesDictionary);

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var kVectorsArray = new IReadOnlyDictionary<int, double>[VSpaceDimension + 1];

            kVectorsArray[Grade] = ScalarValuesDictionary;

            return new GaNumSgrMultivector(kVectorsArray);
        }


        public GaNumDarKVector ToDarKVector()
        {
            return new GaNumDarKVector(
                VSpaceDimension, 
                Grade, 
                ScalarValuesArray
            );
        }

        public GaNumSarKVector ToSarKVector()
        {
            return this;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            return ScalarValuesDictionary
                .Select(p =>
                    new GaTerm<double>(GaFrameUtils.BasisBladeId(Grade, p.Key), p.Value)
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
            return ScalarValuesDictionary
                .Where(p => !p.Value.IsNearZero())
                .Select(p =>
                    new GaTerm<double>(GaFrameUtils.BasisBladeId(Grade, p.Key), p.Value)
                );
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            return Grade == grade
                ? GetNonZeroTerms()
                : Enumerable.Empty<GaTerm<double>>();
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            return ScalarValuesDictionary.Select(p =>
                GaFrameUtils.BasisBladeId(Grade, p.Key)
            );
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            return ScalarValuesDictionary
                .Where(p => !p.Value.IsNearZero())
                .Select(p =>
                    GaFrameUtils.BasisBladeId(Grade, p.Key)
                );
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            return ScalarValuesDictionary.Select(p => p.Value);
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            return ScalarValuesDictionary
                .Where(p => !p.Value.IsNearZero())
                .Select(p => p.Value);
        }


        public override bool TryGetValue(int id, out double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade)
                return ScalarValuesDictionary.TryGetValue(index, out value);

            value = 0.0d;
            return false;
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            if (Grade == grade)
                return ScalarValuesDictionary.TryGetValue(index, out value);

            value = 0.0d;
            return false;
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            if (Grade == grade && ScalarValuesDictionary.TryGetValue(index, out var value))
            {
                term = new GaTerm<double>(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            if (Grade == grade && ScalarValuesDictionary.TryGetValue(index, out var value))
            {
                term = new GaTerm<double>(GaFrameUtils.BasisBladeId(grade, index), value);
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
                : (IGaNumKVector)GaNumDarKVector.CreateZero(VSpaceDimension, grade);
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            yield return this;
        }


        public override IEnumerable<int> GetStoredGrades()
        {
            if (ScalarValuesDictionary.Count > 0)
                yield return Grade;
        }

        public override int GetStoredGradesBitPattern()
        {
            return (ScalarValuesDictionary.Count > 0) ? (1 << Grade) : 0;
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade == Grade && ScalarValuesDictionary.Count > 0;
        }


        public override IGaGbtNode1<double> GetGbtRootNode()
        {
            return GaGbtJarMultivectorNode.CreateRootNode(this);
        }

        public override IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity)
        {
            return GaGbtNumSgrMultivectorStack1.Create(capacity, this);
        }


        public override IGaNumMultivector GetNegative()
        {
            var scalarValues =
                ScalarValuesDictionary.ToDictionary(
                    pair => pair.Key,
                    pair => -pair.Value
                );

            return new GaNumSarKVector(VSpaceDimension, Grade, scalarValues);
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
            return new GaNumSgrMultivectorFactory(this);
        }
    }
}