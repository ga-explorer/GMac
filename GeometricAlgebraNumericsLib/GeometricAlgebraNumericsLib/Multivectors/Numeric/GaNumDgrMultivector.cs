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
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// Immutable Dense Graded Representation (DGR) Multivector. Internally, each grade is separated into a dense array of scalar values.
    /// Either a grade is fully present or not present (null)
    /// </summary>
    public sealed class GaNumDgrMultivector
        : GaNumMultivector, IGaNumGradedMultivector
    {
        public static GaNumDgrMultivector CreateZero(int vSpaceDim)
        {
            var kVectorsArray = new IReadOnlyList<double>[vSpaceDim + 1];

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public static GaNumDgrMultivector CreateCopy(IGaNumMultivector mv)
        {
            var factory = new GaNumDgrMultivectorFactory(mv.VSpaceDimension);

            factory.SetTerms(mv.GetNonZeroTerms());

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector CreateVector(params double[] scalarValues)
        {
            var factory = new GaNumDgrMultivectorFactory(scalarValues.Length);

            factory.SetKVector(1, scalarValues);

            return factory.GetDgrMultivector();
        }


        public static GaNumDgrMultivector operator -(GaNumDgrMultivector mv)
        {
            return mv.GetNonZeroTerms().GaNegative().CreateDgrMultivector(mv.VSpaceDimension);
        }

        public static GaNumDgrMultivector operator +(GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            return mv1.GetAdditionTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector operator -(GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            return mv1.GetSubtractionTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector operator *(GaNumDgrMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(s).CreateDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector operator *(double s, GaNumDgrMultivector mv1)
        {
            return mv1.GetScaledTerms(s).CreateDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector operator /(GaNumDgrMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(1.0d / s).CreateDgrMultivector(mv1.VSpaceDimension);
        }


        public IReadOnlyList<IReadOnlyList<double>> GradedScalarValuesArray { get; }

        public override double this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var kVectorArray = 
                    GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    return 0;

                return kVectorArray[index];
            }
        }

        public override double this[int grade, int index]
        {
            get
            {
                var kVectorArray = 
                    GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    return 0;

                return kVectorArray[index];
            }
        }

        public int MaxStoredGrade
        {
            get
            {
                var grade = -1;

                for (var i = 0; i < GradedScalarValuesArray.Count; i++)
                    if (GradedScalarValuesArray[i] != null)
                        grade = i;

                return grade;
            }
        }

        public override int StoredTermsCount
            => GradedScalarValuesArray
                .Where(a => !a.IsNullOrEmpty())
                .Sum(a => a.Count);


        internal GaNumDgrMultivector(IReadOnlyList<IReadOnlyList<double>> kVectorsArray)
            : base(kVectorsArray.Count - 1)
        {
            Debug.Assert(
                kVectorsArray
                    .Select((a, i) => Tuple.Create(i, a))
                    .All(t =>
                        t.Item2.IsNullOrEmpty() ||
                        t.Item2.Count == GaFrameUtils.KvSpaceDimension(kVectorsArray.Count - 1, t.Item1)
                    )
            );

            GradedScalarValuesArray = kVectorsArray;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < kVectorArray.Count; index++)
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(grade, index),
                        kVectorArray[index]
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            var scalarValuesArray =
                GradedScalarValuesArray[grade];

            if (scalarValuesArray.IsNullOrEmpty())
                yield break;

            for (var index = 0; index < scalarValuesArray.Count; index++)
                yield return new GaTerm<double>(
                    GaFrameUtils.BasisBladeId(grade, index),
                    scalarValuesArray[index]
                );
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < kVectorArray.Count; index++)
                {
                    var value = kVectorArray[index];

                    if (!value.IsNearZero())
                        yield return new GaTerm<double>(
                            GaFrameUtils.BasisBladeId(grade, index),
                            value
                        );
                }
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            var scalarValuesArray = GradedScalarValuesArray[grade];

            if (scalarValuesArray.IsNullOrEmpty())
                yield break;

            for (var index = 0; index < scalarValuesArray.Count; index++)
            {
                var value = scalarValuesArray[index];

                if (!value.IsNearZero())
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(grade, index),
                        value
                    );
            }
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < kVectorArray.Count; index++)
                    yield return GaFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < kVectorArray.Count; index++)
                    if (!kVectorArray[index].IsNearZero())
                        yield return GaFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                foreach (var value in kVectorArray)
                    yield return value;
            }
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                foreach (var value in kVectorArray)
                    if (!value.IsNearZero())
                        yield return value;
            }
        }


        public override bool ContainsStoredTerm(int id)
        {
            var grade = id.BasisBladeGrade();

            return !GradedScalarValuesArray[grade].IsNullOrEmpty();
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            return !GradedScalarValuesArray[grade].IsNullOrEmpty();
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return !GradedScalarValuesArray[grade].IsNullOrEmpty();
        }


        public override bool TryGetValue(int id, out double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues = GradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
            {
                value = 0.0d;
                return false;
            }

            value = scalarValues[index];
            return true;
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            var scalarValues = GradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
            {
                value = 0.0d;
                return false;
            }

            value = scalarValues[index];
            return true;
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues = GradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
            {
                term = null;
                return false;
            }

            term = new GaTerm<double>(id, scalarValues[index]);
            return true;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            var scalarValues = GradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
            {
                term = null;
                return false;
            }

            term = new GaTerm<double>(grade, index, scalarValues[index]);
            return true;
        }


        public override IGaNumVector GetVectorPart()
        {
            return !GradedScalarValuesArray[1].IsNullOrEmpty()
                ? GaNumVector.Create(GradedScalarValuesArray[1])
                : GaNumVector.CreateZero(VSpaceDimension);
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            for (var i = 0; i < GradedScalarValuesArray.Count; i++)
                if (GradedScalarValuesArray[i] != null)
                    yield return i;
        }

        public override int GetStoredGradesBitPattern()
        {
            var pattern = 0;

            for (var i = 0; i < GradedScalarValuesArray.Count; i++)
                if (GradedScalarValuesArray[i] != null)
                    pattern |= 1 << i;

            return pattern;
        }

        public override IGaGbtNode1<double> GetGbtRootNode()
        {
            return GaGbtJarMultivectorNode.CreateRootNode(this);
        }

        public override IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity)
        {
            return GaGbtNumDgrMultivectorStack1.Create(capacity, this);
        }


        public override IGaNumKVector GetKVectorPart(int grade)
        {
            var kVectorArray = GradedScalarValuesArray[grade];

            return kVectorArray == null
                ? GaNumDarKVector.CreateZero(VSpaceDimension, grade)
                : new GaNumDarKVector(VSpaceDimension, grade, GradedScalarValuesArray[grade]);
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues != null)
                    yield return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
            }
        }


        public override bool IsEmpty()
        {
            return GradedScalarValuesArray.All(a => a == null);
        }

        public override bool IsNearZero(double epsilon)
        {
            return GetStoredTermScalars().All(s => s.IsNearZero(epsilon));
        }

        public override bool IsScalar()
        {
            for (var grade = 1; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = GradedScalarValuesArray[grade];

                if (kVectorArray == null)
                    continue;

                foreach (var value in kVectorArray)
                    if (!value.IsNearZero())
                        return false;
            }

            return true;
        }

        public override bool IsTerm()
        {
            var count = 0;
            foreach (var value in GetNonZeroTermScalars())
            {
                count++;

                if (count > 1)
                    return false;
            }

            return count == 1;
        }

        public override bool IsZero()
        {
            return GetStoredTermScalars().All(v => v.IsNearZero());
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues = 
                new GaDgrMultivectorAsDarMultivectorReadOnlyList<double>(
                    GradedScalarValuesArray
                );

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            return this;
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues = new Dictionary<int, double>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesArray =
                    GradedScalarValuesArray[grade];

                if (scalarValuesArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValuesArray.Count; index++)
                {
                    var value = scalarValuesArray[index];

                    if (!value.IsNearZero())
                        scalarValues.Add(
                            GaFrameUtils.BasisBladeId(grade, index),
                            value
                        );
                }
            }

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var kVectorsArray = new Dictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues = GradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                var dstScalarValues = new Dictionary<int, double>();

                for (var index = 0; index < srcScalarValues.Count; index++)
                {
                    var value = srcScalarValues[index];

                    if (!value.IsNearZero())
                        dstScalarValues.Add(index, value);
                }

                if (dstScalarValues.Count > 0)
                    kVectorsArray[grade] = dstScalarValues;
            }

            return new GaNumSgrMultivector(
                kVectorsArray.ToSgrKVectorsList()
            );
        }


        public override IGaNumMultivector GetNegative()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues == null)
                    continue;

                kVectorsArray[grade] = scalarValues.Select(
                    v => -v
                ).ToArray();
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override IGaNumMultivector GetReverse()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues == null)
                    continue;

                kVectorsArray[grade] = grade.GradeHasNegativeReverse()
                    ? scalarValues.Select(v => -v).ToArray()
                    : scalarValues;
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override IGaNumMultivector GetGradeInv()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues == null)
                    continue;

                kVectorsArray[grade] = grade.GradeHasNegativeGradeInv()
                    ? scalarValues.Select(v => -v).ToArray()
                    : scalarValues;
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override IGaNumMultivector GetCliffConj()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues == null)
                    continue;

                kVectorsArray[grade] = grade.GradeHasNegativeCliffConj()
                    ? scalarValues.Select(v => -v).ToArray()
                    : scalarValues;
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumDgrMultivectorFactory(this);
        }


        public override string ToString()
        {
            //return TermsTree.ToString();

            var composer = new LinearTextComposer();

            composer.AppendLine("Binary Tree Multivector:");

            var termsList =
                GetStoredTerms()
                    //.Where(t => t.ScalarValue != 0)
                    .OrderBy(t => t.BasisBladeId);

            foreach (var term in termsList)
                composer
                    .Append(term.BasisBladeId.ToString().PadLeft(12))
                    .Append(": ")
                    .AppendLine(term.ScalarValue.ToString("G"));

            return composer.ToString();
        }
    }
}