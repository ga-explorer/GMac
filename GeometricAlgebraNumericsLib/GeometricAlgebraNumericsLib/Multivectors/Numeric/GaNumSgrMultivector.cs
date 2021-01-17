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
    /// Immutable Sparse Graded Representation (DGR) Multivector. Internally, each grade is separated into a sparse array of scalar values.
    /// </summary>
    public sealed class GaNumSgrMultivector 
        : GaNumMultivector, IGaNumGradedMultivector
    {
        internal Dictionary<int, double>[] CreateKVectorsArray(int vSpaceDim)
        {
            var kVectorsArray = new Dictionary<int, double>[vSpaceDim + 1];

            for (var grade = 0; grade <= vSpaceDim; grade++)
                kVectorsArray[grade] = new Dictionary<int, double>();

            return kVectorsArray;
        }


        public static GaNumSgrMultivector CreateZero(int vSpaceDim)
        {
            var kVectorsArray = new IReadOnlyDictionary<int, double>[vSpaceDim + 1];

            return new GaNumSgrMultivector(kVectorsArray);
        }

        public static GaNumSgrMultivector CreateCopy(IGaNumMultivector mv)
        {
            var factory = new GaNumSgrMultivectorFactory(mv.VSpaceDimension);

            factory.SetTerms(mv.GetNonZeroTerms());

            return factory.GetSgrMultivector();
        }

        public static GaNumSgrMultivector CreateTerm(int vSpaceDim, int id, double value)
        {
            var kVectorsArray = new IReadOnlyDictionary<int, double>[vSpaceDim + 1];

            id.BasisBladeGradeIndex(out var grade, out var index);

            kVectorsArray[grade] = new Dictionary<int, double> {{index, value}};

            return new GaNumSgrMultivector(kVectorsArray);
        }


        public static GaNumSgrMultivector operator -(GaNumSgrMultivector mv)
        {
            return mv.GetNonZeroTerms().GaNegative().CreateSgrMultivector(mv.VSpaceDimension);
        }

        public static GaNumSgrMultivector operator +(GaNumSgrMultivector mv1, GaNumSgrMultivector mv2)
        {
            return mv1.GetAdditionTerms(mv2).SumAsSgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSgrMultivector operator -(GaNumSgrMultivector mv1, GaNumSgrMultivector mv2)
        {
            return mv1.GetSubtractionTerms(mv2).SumAsSgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSgrMultivector operator *(GaNumSgrMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(s).CreateSgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSgrMultivector operator *(double s, GaNumSgrMultivector mv1)
        {
            return mv1.GetScaledTerms(s).CreateSgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSgrMultivector operator /(GaNumSgrMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(1.0d / s).CreateSgrMultivector(mv1.VSpaceDimension);
        }


        public IReadOnlyList<IReadOnlyDictionary<int, double>> GradedScalarValuesArray { get; }

        public override double this[int id] 
        { 
            get
            { 
                id.BasisBladeGradeIndex(out var grade, out var index);

                var kVectorArray =
                    GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    return 0.0d;

                return kVectorArray.TryGetValue(index, out var value) 
                    ? value : 0.0d;
            }
        }

        public override double this[int grade, int index] 
        { 
            get 
            { 
                var kVectorArray = 
                    GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty()) 
                    return 0.0d;

                return kVectorArray.TryGetValue(index, out var value) 
                    ? value : 0.0d;
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

        
        internal GaNumSgrMultivector(IReadOnlyList<IReadOnlyDictionary<int, double>> kVectorsArray)
            : base(kVectorsArray.Count - 1)
        {
            Debug.Assert(
                kVectorsArray
                    .Select((a, i) => Tuple.Create(i, a))
                    .All(a => 
                        a.Item2.IsNullOrEmpty() ||
                        a.Item2.All(p => 
                            p.Key >= 0 && p.Key < GaFrameUtils.KvSpaceDimension(VSpaceDimension, a.Item1)
                        )
                )
            );

            GradedScalarValuesArray = kVectorsArray;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues =
                    GradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValues)
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(grade, pair.Key),
                        scalarValues[pair.Key]
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            var scalarValues =
                GradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
                yield break;

            foreach (var pair in scalarValues)
                yield return new GaTerm<double>(
                    GaFrameUtils.BasisBladeId(grade, pair.Key),
                    pair.Value
                );
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues =
                    GradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                var pairsList =
                    scalarValues.Where(p => !p.Value.IsNearZero());

                foreach (var pair in pairsList)
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(grade, pair.Key),
                        pair.Value
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            var scalarValues =
                GradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
                yield break;

            var pairsList =
                scalarValues.Where(p => !p.Value.IsNearZero());

            foreach (var pair in pairsList)
                yield return new GaTerm<double>(
                    GaFrameUtils.BasisBladeId(grade, pair.Key),
                    pair.Value
                );
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = 
                    GradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValues)
                    yield return GaFrameUtils.BasisBladeId(grade, pair.Key);
            }
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = 
                    GradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                var pairsList =
                    scalarValues.Where(p => !p.Value.IsNearZero());

                foreach (var pair in pairsList)
                    if (!scalarValues[pair.Key].IsNearZero())
                        yield return GaFrameUtils.BasisBladeId(grade, pair.Key);
            }
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = 
                    GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                foreach (var value in kVectorArray.Values)
                    yield return value;
            }
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = 
                    GradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                foreach (var value in kVectorArray.Values)
                    if (!value.IsNearZero())
                        yield return value;
            }
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


        public override bool IsEmpty()
        {
            return GradedScalarValuesArray.All(a => a == null);
        }

        public override bool ContainsStoredTerm(int id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return id >= 0 && id < GaSpaceDimension &&
                   !GradedScalarValuesArray[grade].IsNullOrEmpty() &&
                   GradedScalarValuesArray[grade].ContainsKey(index);
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return id >= 0 && id < GaSpaceDimension &&
                   !GradedScalarValuesArray[grade].IsNullOrEmpty() &&
                   GradedScalarValuesArray[grade].ContainsKey(index);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension &&
                   !GradedScalarValuesArray[grade].IsNullOrEmpty();
        }


        public override IGaNumVector GetVectorPart()
        {
            return GradedScalarValuesArray[1] != null 
                ? GaNumVector.Create(new SparseReadOnlyList<double>(VSpaceDimension, GradedScalarValuesArray[1]))
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
            return GaGbtNumSgrMultivectorStack1.Create(capacity, this);
        }


        public override IGaNumKVector GetKVectorPart(int grade)
        {
            var kVectorArray = GradedScalarValuesArray[grade];

            return kVectorArray == null
                ? GaNumSarKVector.CreateZero(VSpaceDimension, grade)
                : new GaNumSarKVector(VSpaceDimension, grade, GradedScalarValuesArray[grade]);
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues.Count > 0)
                    yield return new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
            }
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

                if (kVectorArray.Values.Any(value => !value.IsNearZero()))
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
                new GaSgrMultivectorAsDarMultivectorReadOnlyList<double>(GradedScalarValuesArray);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectors = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = GradedScalarValuesArray[grade];

                if (scalarValues.Count == 0)
                    continue;

                kVectors[grade] = new SparseReadOnlyList<double>(
                    GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade),
                    scalarValues
                );
            }

            return new GaNumDgrMultivector(kVectors);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues =
                new GaSgrMultivectorAsSarMultivectorReadOnlyDictionary<double>(
                    GradedScalarValuesArray
                );

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            return this;
        }


        public override IGaNumMultivector GetNegative()
        {
            var kVectorsArray = 
                new IReadOnlyDictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = 
                    GradedScalarValuesArray[grade];

                kVectorsArray[grade] = 
                    (scalarValues.Count == 0)
                        ? new EmptyReadOnlyDictionary<int, double>()
                        : scalarValues.GetNegative();
            }

            return new GaNumSgrMultivector(kVectorsArray);
        }

        public override IGaNumMultivector GetReverse()
        {
            var kVectorsArray =
                new IReadOnlyDictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues =
                    GradedScalarValuesArray[grade];

                kVectorsArray[grade] = 
                    (scalarValues.Count == 0)
                        ? new EmptyReadOnlyDictionary<int, double>()
                        : (grade.GradeHasNegativeReverse() ? scalarValues.GetNegative() : scalarValues);
            }

            return new GaNumSgrMultivector(kVectorsArray);
        }

        public override IGaNumMultivector GetGradeInv()
        {
            var kVectorsArray =
                new IReadOnlyDictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues =
                    GradedScalarValuesArray[grade];

                kVectorsArray[grade] = 
                    (scalarValues.Count == 0)
                        ? new EmptyReadOnlyDictionary<int, double>()
                        : (grade.GradeHasNegativeGradeInv() ? scalarValues.GetNegative() : scalarValues);
            }

            return new GaNumSgrMultivector(kVectorsArray);
        }

        public override IGaNumMultivector GetCliffConj()
        {
            var kVectorsArray =
                new IReadOnlyDictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues =
                    GradedScalarValuesArray[grade];

                kVectorsArray[grade] = 
                    (scalarValues.Count == 0)
                        ? new EmptyReadOnlyDictionary<int, double>()
                        : (grade.GradeHasNegativeCliffConj() ? scalarValues.GetNegative() : scalarValues);
            }

            return new GaNumSgrMultivector(kVectorsArray);
        }

        
        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumSgrMultivectorFactory(this);
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