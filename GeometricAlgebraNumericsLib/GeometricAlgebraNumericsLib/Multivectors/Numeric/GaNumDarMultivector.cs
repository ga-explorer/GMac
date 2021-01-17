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
    /// Mutable Dense Array Representation Multivector. Internally, a dense array is used to store all scalar values.
    /// </summary>
    public sealed class GaNumDarMultivector : GaNumMultivector
    {
        public static GaNumDarMultivector CreateZero(int vSpaceDim)
        {
            return new GaNumDarMultivector(
                new EmptyReadOnlyList<double>(vSpaceDim.ToGaSpaceDimension())
            );
        }

        public static GaNumDarMultivector CreateCopy(IGaNumMultivector mv)
        {
            var factory = new GaNumDarMultivectorFactory(mv.VSpaceDimension);

            factory.AddTerms(mv.GetNonZeroTerms());

            return factory.GetDarMultivector();
        }


        public static GaNumDarMultivector operator -(GaNumDarMultivector mv)
        {
            return mv.GetNonZeroTerms().GaNegative().CreateDarMultivector(mv.VSpaceDimension);
        }

        public static GaNumDarMultivector operator +(GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            return mv1.GetAdditionTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector operator -(GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            return mv1.GetSubtractionTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector operator *(GaNumDarMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(s).CreateDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector operator *(double s, GaNumDarMultivector mv1)
        {
            return mv1.GetScaledTerms(s).CreateDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector operator /(GaNumDarMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(1.0d / s).CreateDarMultivector(mv1.VSpaceDimension);
        }


        public IReadOnlyList<double> ScalarValuesArray { get; }

        public override double this[int id]
            => ScalarValuesArray[id];

        public override double this[int grade, int index]
            => ScalarValuesArray[GaFrameUtils.BasisBladeId(grade, index)];

        public override int StoredTermsCount 
            => ScalarValuesArray.Count;


        internal GaNumDarMultivector(IReadOnlyList<double> scalarValues)
            : base(scalarValues.Count.ToVSpaceDimension())
        {
            Debug.Assert(scalarValues.Count.IsValidGaSpaceDimension());

            ScalarValuesArray = scalarValues;
        }


        public override bool IsEmpty()
        {
            return false;
        }

        public override bool ContainsStoredTerm(int id)
        {
            return id >= 0 && id <= GaSpaceDimension;
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return id >= 0 && id <= GaSpaceDimension;
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            var id = 0;
            foreach (var value in ScalarValuesArray)
            {
                yield return new GaTerm<double>(id, value);

                id++;
            }
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

            for (var index = 0; index < kvSpaceDim; index++)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                yield return new GaTerm<double>(id, ScalarValuesArray[id]);
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            var id = 0;
            foreach (var value in ScalarValuesArray)
            {
                if (!value.IsNearZero())
                    yield return new GaTerm<double>(id, value);

                id++;
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

            for (var index = 0; index < kvSpaceDim; index++)
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                var value = ScalarValuesArray[id];

                if (!value.IsNearZero())
                    yield return new GaTerm<double>(id, value);
            }
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            return Enumerable.Range(0, GaSpaceDimension);
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            var id = 0;
            foreach (var value in ScalarValuesArray)
            {
                if (!value.IsNearZero())
                    yield return id;

                id++;
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


        public override bool TryGetValue(int id, out double value)
        {
            if (id >= 0 && id < GaSpaceDimension)
            {
                value = ScalarValuesArray[id];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (id >= 0 && id < GaSpaceDimension)
            {
                value = ScalarValuesArray[id];
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            if (id >= 0 && id < GaSpaceDimension)
            {
                term = new GaTerm<double>(id, ScalarValuesArray[id]);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (id >= 0 && id < GaSpaceDimension)
            {
                term = new GaTerm<double>(id, ScalarValuesArray[id]);
                return true;
            }

            term = null;
            return false;
        }


        public override bool IsTerm()
        {
            return ScalarValuesArray.Count(v => !v.IsNearZero()) <= 1;
        }

        public override bool IsScalar()
        {
            return ScalarValuesArray.Skip(1).All(v => v.IsNearZero());
        }

        public override bool IsZero()
        {
            return ScalarValuesArray.All(v => v.IsNearZero());
        }

        public override bool IsNearZero(double epsilon)
        {
            return ScalarValuesArray.All(v => v.IsNearZero(epsilon));
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            return this;
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                kVectorsArray[grade] =
                    new GaDarMultivectorAsDarKVectorReadOnlyList<double>(grade, ScalarValuesArray);
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues = new Dictionary<int, double>();

            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var value = ScalarValuesArray[id];

                if (!value.IsNearZero())
                    scalarValues.Add(id, value);
            }

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var kVectorsArray = new Dictionary<int, double>[VSpaceDimension + 1];

            foreach (var term in GetNonZeroTerms())
            {
                term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

                if (kVectorsArray[grade] == null)
                    kVectorsArray[grade] = new Dictionary<int, double>();

                kVectorsArray[grade].Add(index, term.ScalarValue);
            }

            return new GaNumSgrMultivector(
                kVectorsArray.ToSgrKVectorsList()
            );
        }


        public override IGaNumVector GetVectorPart()
        {
            var scalarValues = 
                new GaDarMultivectorAsVectorReadOnlyList<double>(ScalarValuesArray);

            return new GaNumVector(scalarValues);
        }

        public override IGaNumKVector GetKVectorPart(int grade)
        {
            var scalarValues = 
                new GaDarMultivectorAsDarKVectorReadOnlyList<double>(grade, ScalarValuesArray);

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                yield return GetKVectorPart(grade);
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            return Enumerable.Range(0, VSpaceDimension + 1);
        }

        public override int GetStoredGradesBitPattern()
        {
            return (1 << (VSpaceDimension + 2)) - 1;
        }


        public override IGaGbtNode1<double> GetGbtRootNode()
        {
            return GaGbtFarMultivectorNode.CreateRootNode(this);
        }

        public override IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity)
        {
            return GaGbtNumDarMultivectorStack1.Create(capacity, this);
        }


        public override IGaNumMultivector GetNegative()
        {
            var scalarValues = 
                ScalarValuesArray.Select(v => -v).ToArray();

            return new GaNumDarMultivector(scalarValues);
        }

        public override IGaNumMultivector GetReverse()
        {
            var scalarValues =
                ScalarValuesArray.Select(
                    (v, id) => id.GradeHasNegativeReverse() ? -v : v
                ).ToArray();

            return new GaNumDarMultivector(scalarValues);
        }

        public override IGaNumMultivector GetGradeInv()
        {
            var scalarValues =
                ScalarValuesArray.Select(
                    (v, id) => id.GradeHasNegativeGradeInv() ? -v : v
                ).ToArray();

            return new GaNumDarMultivector(scalarValues);
        }

        public override IGaNumMultivector GetCliffConj()
        {
            var scalarValues =
                ScalarValuesArray.Select(
                    (v, id) => id.GradeHasNegativeCliffConj() ? -v : v
                ).ToArray();

            return new GaNumDarMultivector(scalarValues);
        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumDarMultivectorFactory(this);
        }
    }
}