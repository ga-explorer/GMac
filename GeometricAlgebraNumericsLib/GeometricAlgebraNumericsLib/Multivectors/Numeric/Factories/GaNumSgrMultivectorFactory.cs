using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures.Collections;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    /// <summary>
    /// A multivector factory where the internal storage is an array of graded sparse k-vectors
    /// </summary>
    public sealed class GaNumSgrMultivectorFactory : GaNumMultivectorFactory
    {
        private Dictionary<int, double>[] _gradedScalarValuesArray;

        private Dictionary<int, double> GetOrCreateScalarValuesDictionary(int grade)
        {
            return _gradedScalarValuesArray[grade] ??
                   (_gradedScalarValuesArray[grade] = new Dictionary<int, double>());
        }


        public override int StoredTermsCount
            => _gradedScalarValuesArray
                .Where(a => !a.IsNullOrEmpty())
                .Sum(a => a.Count);

        public override double this[int id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                return _gradedScalarValuesArray[grade].TryGetValue(index, out var value) 
                    ? value : 0.0d;
            }
            set
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues == null)
                {
                    _gradedScalarValuesArray[grade] = new Dictionary<int, double> {{index, value}};

                    return;
                }

                if (scalarValues.ContainsKey(index))
                    scalarValues[index] = value;
                else
                    scalarValues.Add(index, value);
            }
        }

        public override double this[int grade, int index]
        {
            get => _gradedScalarValuesArray[grade].TryGetValue(index, out var value) ? value : 0.0d;
            set
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues == null)
                {
                    _gradedScalarValuesArray[grade] = new Dictionary<int, double> { { index, value } };

                    return;
                }

                if (scalarValues.ContainsKey(index))
                    scalarValues[index] = value;
                else
                    scalarValues.Add(index, value);
            }
        }


        public GaNumSgrMultivectorFactory(int vSpaceDim)
            : base(vSpaceDim)
        {
            _gradedScalarValuesArray = new Dictionary<int, double>[VSpaceDimension + 1];
        }

        public GaNumSgrMultivectorFactory(GaNumSarKVector mv)
            : base(mv.VSpaceDimension)
        {
            _gradedScalarValuesArray = new Dictionary<int, double>[VSpaceDimension + 1];

            var srcScalarValues =
                mv.ScalarValuesDictionary;

            if (srcScalarValues.IsNullOrEmpty())
                return;

            _gradedScalarValuesArray[mv.Grade] = srcScalarValues
                .Where(p => !p.Value.IsNearZero())
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );
        }

        public GaNumSgrMultivectorFactory(GaNumSgrMultivector mv)
            : base(mv.VSpaceDimension)
        {
            _gradedScalarValuesArray = new Dictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues =
                    mv.GradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                _gradedScalarValuesArray[grade] = srcScalarValues
                    .Where(p => !p.Value.IsNearZero())
                    .ToDictionary(
                        p => p.Key,
                        p => p.Value
                    );
            }
        }

        public GaNumSgrMultivectorFactory(GaNumSgrMultivectorFactory factory)
            : base(factory.VSpaceDimension)
        {
            _gradedScalarValuesArray = new Dictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues =
                    factory._gradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                _gradedScalarValuesArray[grade] = srcScalarValues
                    .Where(p => !p.Value.IsNearZero())
                    .ToDictionary(
                        p => p.Key,
                        p => p.Value
                    );
            }
        }


        public override GaNumMultivectorFactory Reset()
        {
            _gradedScalarValuesArray = new Dictionary<int, double>[VSpaceDimension + 1];

            return this;
        }

        public override GaNumMultivectorFactory RemoveNearZeroTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                if (scalarValues == null)
                    continue;

                if (scalarValues.Count == 0)
                {
                    _gradedScalarValuesArray[grade] = null;
                    continue;
                }

                var indexList =
                    scalarValues
                        .Where(p => p.Value.IsNearZero())
                        .Select(p => p.Key)
                        .ToArray();

                foreach (var index in indexList)
                    scalarValues.Remove(index);

                if (scalarValues.Count == 0)
                    _gradedScalarValuesArray[grade] = null;
            }

            return this;
        }


        public override bool TryGetValue(int id, out double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues = _gradedScalarValuesArray[grade];

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
            var scalarValues = _gradedScalarValuesArray[grade];

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

            var scalarValues = _gradedScalarValuesArray[grade];

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
            var scalarValues = _gradedScalarValuesArray[grade];

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
            return _gradedScalarValuesArray.Any(a => !a.IsNullOrEmpty());
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValues)
                    yield return new GaTerm<double>(
                        GaNumFrameUtils.BasisBladeId(grade, pair.Key),
                        pair.Value
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetStoredTerms(int grade)
        {
            var scalarValues = 
                _gradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
                yield break;

            foreach (var pair in scalarValues)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, pair.Key),
                    pair.Value
                );
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    yield break;

                var pairsList = 
                    scalarValues.Where(p => !p.Value.IsNearZero());

                foreach (var pair in pairsList)
                    yield return new GaTerm<double>(
                        GaNumFrameUtils.BasisBladeId(grade, pair.Key),
                        pair.Value
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms(int grade)
        {
            var scalarValues = 
                _gradedScalarValuesArray[grade];

            if (scalarValues.IsNullOrEmpty())
                yield break;

            var pairsList =
                scalarValues.Where(p => !p.Value.IsNearZero());

            foreach (var pair in pairsList)
                yield return new GaTerm<double>(
                    GaNumFrameUtils.BasisBladeId(grade, pair.Key),
                    pair.Value
                );
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = 
                    _gradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < kVectorArray.Count; index++)
                    yield return GaNumFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = 
                    _gradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < kVectorArray.Count; index++)
                    if (!kVectorArray[index].IsNearZero())
                        yield return GaNumFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = 
                    _gradedScalarValuesArray[grade];

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
                    _gradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                foreach (var value in kVectorArray.Values)
                    if (!value.IsNearZero())
                        yield return value;
            }
        }


        public override bool ContainsStoredTerm(int id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            return id >= 0 && id < GaSpaceDimension &&
               !_gradedScalarValuesArray[grade].IsNullOrEmpty() &&
               _gradedScalarValuesArray[grade].ContainsKey(index);
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            return id >= 0 && id < GaSpaceDimension &&
               !_gradedScalarValuesArray[grade].IsNullOrEmpty() &&
               _gradedScalarValuesArray[grade].ContainsKey(index);
        }

        public override bool ContainsStoredKVector(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension &&
               !_gradedScalarValuesArray[grade].IsNullOrEmpty();
        }


        public override GaNumVector GetStoredVector()
        {
            var scalarValues = 
                _gradedScalarValuesArray[1];

            if (scalarValues.IsNullOrEmpty())
                return null;

            return new GaNumVector(
                new SparseReadOnlyList<double>(VSpaceDimension, scalarValues)
            );
        }

        public override GaNumDarKVector GetStoredDarKVector(int grade)
        {
            var scalarValues =
                _gradedScalarValuesArray[1];

            if (scalarValues.IsNullOrEmpty())
                return null;

            var kvSpaceDim = GetKvSpaceDimension(grade);

            return new GaNumDarKVector(
                VSpaceDimension, 
                grade,
                new SparseReadOnlyList<double>(kvSpaceDim, scalarValues)
            );
        }

        public override GaNumSarKVector GetStoredSarKVector(int grade)
        {
            var scalarValues = 
                _gradedScalarValuesArray[grade];

            return scalarValues.IsNullOrEmpty()
                ? null
                : new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override IGaNumKVector GetStoredKVector(int grade)
        {
            return GetStoredSarKVector(grade);
        }


        public override GaNumVector GetStoredVectorCopy()
        {
            var scalarValues = new double[VSpaceDimension];

            var scalarValuesDictionary = _gradedScalarValuesArray[1];

            if (scalarValuesDictionary.IsNullOrEmpty())
                return null;

            foreach (var pair in scalarValuesDictionary)
                scalarValues[pair.Key] = pair.Value;

            return new GaNumVector(scalarValues);
        }

        public override IGaNumKVector GetStoredKVectorCopy(int grade)
        {
            return GetStoredSarKVector(grade);
        }

        public override GaNumDarKVector GetStoredDarKVectorCopy(int grade)
        {
            var kVectorSpaceDim = GetKvSpaceDimension(grade);
            var scalarValues = new double[kVectorSpaceDim];

            var scalarValuesDictionary = _gradedScalarValuesArray[grade];

            if (scalarValuesDictionary.IsNullOrEmpty())
                return null;

            foreach (var pair in scalarValuesDictionary)
                scalarValues[pair.Key] = pair.Value;

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override GaNumSarKVector GetStoredSarKVectorCopy(int grade)
        {
            var scalarValuesDictionary = _gradedScalarValuesArray[grade];

            if (scalarValuesDictionary.IsNullOrEmpty())
                return null;

            var scalarValues = 
                scalarValuesDictionary.ToDictionary(
                    p => p.Key, 
                    p => p.Value
                );

            return new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues = 
                new GaSgrMultivectorAsDarMultivectorReadOnlyList<double>(_gradedScalarValuesArray);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectors = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                kVectors[grade] = new SparseReadOnlyList<double>(
                    GetKvSpaceDimension(grade),
                    scalarValues
                );
            }

            return new GaNumDgrMultivector(kVectors);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues = 
                new GaSgrMultivectorAsSarMultivectorReadOnlyDictionary<double>(_gradedScalarValuesArray);

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            return new GaNumSgrMultivector(_gradedScalarValuesArray);
        }


        public override GaNumDarMultivector GetDarMultivectorCopy()
        {
            var scalarValues = new double[GaSpaceDimension];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesDictionary = _gradedScalarValuesArray[grade];

                if (scalarValuesDictionary.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValuesDictionary)
                {
                    var id = GaNumFrameUtils.BasisBladeId(grade, pair.Key);
                    scalarValues[id] = pair.Value;
                }
            }

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivectorCopy()
        {
            var kVectors = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesDictionary = _gradedScalarValuesArray[grade];

                if (scalarValuesDictionary.IsNullOrEmpty())
                    continue;

                var kvSpaceDim = GetKvSpaceDimension(grade);
                var scalarValues = new double[kvSpaceDim];

                foreach (var pair in scalarValuesDictionary)
                    scalarValues[pair.Key] = pair.Value;

                kVectors[grade] = scalarValues;
            }

            return new GaNumDgrMultivector(kVectors);
        }

        public override GaNumSarMultivector GetSarMultivectorCopy()
        {
            var scalarValues = new Dictionary<int, double>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesDictionary = _gradedScalarValuesArray[grade];

                if (scalarValuesDictionary.IsNullOrEmpty())
                    continue;

                foreach (var pair in scalarValuesDictionary.Where(p => !p.Value.IsNearZero()))
                {
                    var id = GaNumFrameUtils.BasisBladeId(grade, pair.Key);
                    scalarValues.Add(id, pair.Value);
                }
            }

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivectorCopy()
        {
            var kVectors = new Dictionary<int, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesDictionary = 
                    _gradedScalarValuesArray[grade];

                if (scalarValuesDictionary.IsNullOrEmpty())
                    continue;

                kVectors[grade] =
                    scalarValuesDictionary
                        .Where(p => !p.Value.IsNearZero())
                        .ToDictionary(
                            pair => pair.Key, 
                            pair => pair.Value
                        );
            }

            return new GaNumSgrMultivector(kVectors);
        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumSgrMultivectorFactory(this);
        }


        public override GaNumMultivectorFactory SetTerm(int id, double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] = value;
            else
                scalarValues.Add(index, value);

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(int grade, int index, double value)
        {
            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] = value;
            else
                scalarValues.Add(index, value);

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] = term.ScalarValue;
            else
                scalarValues.Add(index, term.ScalarValue);

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(double scalingFactor, GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] = scalingFactor * term.ScalarValue;
            else
                scalarValues.Add(index, scalingFactor * term.ScalarValue);

            return this;
        }


        public override GaNumMultivectorFactory SetKVector(int grade, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            for (var index = 0; index < kvSpaceDimension; index++)
                if (scalarValues.ContainsKey(index))
                    scalarValues[index] = scalarValuesList[index];
                else
                    scalarValues.Add(index, scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            for (var index = 0; index < kvSpaceDimension; index++)
                if (scalarValues.ContainsKey(index))
                    scalarValues[index] = scalingFactor * scalarValuesList[index];
                else
                    scalarValues.Add(index, scalingFactor * scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            foreach (var pair in scalarValuesList)
                if (scalarValues.ContainsKey(pair.Key))
                    scalarValues[pair.Key] = pair.Value;
                else
                    scalarValues.Add(pair.Key, pair.Value);

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            foreach (var pair in scalarValuesList)
                if (scalarValues.ContainsKey(pair.Key))
                    scalarValues[pair.Key] = scalingFactor * pair.Value;
                else
                    scalarValues.Add(pair.Key, scalingFactor * pair.Value);

            return this;
        }


        public override GaNumMultivectorFactory AddTerm(int id, double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] += value;
            else
                scalarValues.Add(index, value);

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(int grade, int index, double value)
        {
            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] += value;
            else
                scalarValues.Add(index, value);

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] += term.ScalarValue;
            else
                scalarValues.Add(index, term.ScalarValue);

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(double scalingFactor, GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            if (scalarValues.ContainsKey(index))
                scalarValues[index] += scalingFactor * term.ScalarValue;
            else
                scalarValues.Add(index, scalingFactor * term.ScalarValue);

            return this;
        }

        public override GaNumMultivectorFactory AddTerms(IEnumerable<GaTerm<double>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }

        public override GaNumMultivectorFactory AddTerms(double scalingFactor, IEnumerable<GaTerm<double>> termsList)
        {
            foreach (var term in termsList)
                AddTerm(scalingFactor, term);

            return this;
        }


        public override GaNumMultivectorFactory AddKVector(int grade, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            for (var index = 0; index < kvSpaceDimension; index++)
                if (scalarValues.ContainsKey(index))
                    scalarValues[index] += scalarValuesList[index];
                else
                    scalarValues.Add(index, scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            for (var index = 0; index < kvSpaceDimension; index++)
                if (scalarValues.ContainsKey(index))
                    scalarValues[index] += scalingFactor * scalarValuesList[index];
                else
                    scalarValues.Add(index, scalingFactor * scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            foreach (var pair in scalarValuesList)
                if (scalarValues.ContainsKey(pair.Key))
                    scalarValues[pair.Key] += pair.Value;
                else
                    scalarValues.Add(pair.Key, pair.Value);

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            var scalarValues =
                GetOrCreateScalarValuesDictionary(grade);

            foreach (var pair in scalarValuesList)
                if (scalarValues.ContainsKey(pair.Key))
                    scalarValues[pair.Key] += scalingFactor * pair.Value;
                else
                    scalarValues.Add(pair.Key, scalingFactor * pair.Value);

            return this;
        }


        public override GaNumMultivectorFactory ApplyReverse()
        {
            var gradesList =
                Enumerable
                    .Range(0, VSpaceDimension + 1)
                    .Where(g => g.GradeHasNegativeReverse() && !_gradedScalarValuesArray[g].IsNullOrEmpty());

            foreach (var grade in gradesList)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                var indexList = scalarValues.Select(p => p.Key);

                foreach (var index in indexList)
                    scalarValues[index] = -scalarValues[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyGradeInv()
        {
            var gradesList =
                Enumerable
                    .Range(0, VSpaceDimension + 1)
                    .Where(g => g.GradeHasNegativeGradeInv() && !_gradedScalarValuesArray[g].IsNullOrEmpty());

            foreach (var grade in gradesList)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                var indexList = scalarValues.Select(p => p.Key);

                foreach (var index in indexList)
                    scalarValues[index] = -scalarValues[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyCliffConj()
        {
            var gradesList =
                Enumerable
                    .Range(0, VSpaceDimension + 1)
                    .Where(g => g.GradeHasNegativeCliffConj() && !_gradedScalarValuesArray[g].IsNullOrEmpty());

            foreach (var grade in gradesList)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                var indexList = scalarValues.Select(p => p.Key);

                foreach (var index in indexList)
                    scalarValues[index] = -scalarValues[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyNegative()
        {
            var gradesList =
                Enumerable
                    .Range(0, VSpaceDimension + 1)
                    .Where(g => !_gradedScalarValuesArray[g].IsNullOrEmpty());

            foreach (var grade in gradesList)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                var indexList = scalarValues.Select(p => p.Key);

                foreach (var index in indexList)
                    scalarValues[index] = -scalarValues[index];
            }

            return this;
        }


        public override GaNumMultivectorFactory ApplyScaling(double scalingFactor)
        {
            foreach (var scalarValues in _gradedScalarValuesArray.Where(a => !a.IsNullOrEmpty()))
                foreach (var key in scalarValues.Keys)
                    scalarValues[key] *= scalingFactor;

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<double, double> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                _gradedScalarValuesArray[grade] = scalarValues.ToDictionary(
                    p => p.Key,
                    p => mappingFunc(p.Value)
                );
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, double, double> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                _gradedScalarValuesArray[grade] = scalarValues.ToDictionary(
                    p => p.Key,
                    p => mappingFunc(p.Key, p.Value)
                );
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, int, double, double> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues.IsNullOrEmpty())
                    continue;

                _gradedScalarValuesArray[grade] = scalarValues.ToDictionary(
                    p => p.Key,
                    p => mappingFunc(grade, p.Key.BasisBladeIndex(), p.Value));
            }

            return this;
        }
    }
}