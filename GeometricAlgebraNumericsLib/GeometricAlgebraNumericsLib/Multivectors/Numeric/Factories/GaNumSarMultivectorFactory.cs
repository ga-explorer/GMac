using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.Structures.Collections;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    /// <summary>
    /// A multivector factory where the internal storage is a sparse array of terms
    /// </summary>
    public sealed class GaNumSarMultivectorFactory : GaNumMultivectorFactory
    {
        private Dictionary<int, double> _scalarValuesDictionary;


        public override int StoredTermsCount
            => _scalarValuesDictionary.Count;

        public override double this[int id]
        {
            get => _scalarValuesDictionary.TryGetValue(id, out var value) ? value : 0.0d;
            set
            {
                if (_scalarValuesDictionary.ContainsKey(id))
                    _scalarValuesDictionary[id] = value;
                else
                    _scalarValuesDictionary.Add(id, value);
            }
        }

        public override double this[int grade, int index]
        {
            get => _scalarValuesDictionary.TryGetValue(
                GaFrameUtils.BasisBladeId(grade, index), 
                out var value
            ) ? value : 0.0d;
            set
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                if (_scalarValuesDictionary.ContainsKey(id))
                    _scalarValuesDictionary[id] = value;
                else
                    _scalarValuesDictionary.Add(id, value);
            }
        }


        public GaNumSarMultivectorFactory(int vSpaceDim) 
            : base(vSpaceDim)
        {
            _scalarValuesDictionary = new Dictionary<int, double>();
        }

        public GaNumSarMultivectorFactory(GaNumTerm mv)
            : base(mv.VSpaceDimension)
        {
            _scalarValuesDictionary = new Dictionary<int, double>
            {
                {mv.BasisBladeId, mv.ScalarValue}
            };
        }

        public GaNumSarMultivectorFactory(GaNumSarMultivector mv)
            : base(mv.VSpaceDimension)
        {
            _scalarValuesDictionary = mv
                .ScalarValuesDictionary
                .Where(p => !p.Value.IsNearZero())
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );
        }

        public GaNumSarMultivectorFactory(GaNumSarMultivectorFactory factory)
            : base(factory.VSpaceDimension)
        {
            _scalarValuesDictionary = factory
                ._scalarValuesDictionary
                .Where(p => !p.Value.IsNearZero())
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );
        }


        public override GaNumMultivectorFactory Reset()
        {
            _scalarValuesDictionary = new Dictionary<int, double>();

            return this;
        }

        public override GaNumMultivectorFactory RemoveNearZeroTerms()
        {
            var idsList =
                _scalarValuesDictionary
                    .Where(p => p.Value.IsNearZero())
                    .Select(p => p.Key)
                    .ToArray();

            foreach (var id in idsList)
                _scalarValuesDictionary.Remove(id);

            return this;
        }


        public override bool TryGetValue(int id, out double value)
        {
            return _scalarValuesDictionary.TryGetValue(id, out value);
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return _scalarValuesDictionary.TryGetValue(id, out value);
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            if (_scalarValuesDictionary.TryGetValue(id, out var value))
            {
                term = new GaTerm<double>(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (_scalarValuesDictionary.TryGetValue(id, out var value))
            {
                term = new GaTerm<double>(id, value);
                return true;
            }

            term = null;
            return false;
        }


        public override bool IsEmpty()
        {
            return _scalarValuesDictionary.Count == 0;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            return _scalarValuesDictionary
                .Select(p => new GaTerm<double>(p.Key, p.Value));
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            return _scalarValuesDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade)
                .Select(p => new GaTerm<double>(p.Key, p.Value));
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            return _scalarValuesDictionary
                .Where(p => !p.Value.IsNearZero())
                .Select(p => new GaTerm<double>(p.Key, p.Value));
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            return _scalarValuesDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade && !p.Value.IsNearZero())
                .Select(p => new GaTerm<double>(p.Key, p.Value));
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            return _scalarValuesDictionary
                .Select(pair => pair.Key);
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            return _scalarValuesDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => pair.Key);
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            return _scalarValuesDictionary
                .Select(pair => pair.Value);
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            return _scalarValuesDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => pair.Value);
        }


        public override bool ContainsStoredTerm(int id)
        {
            return id >= 0 && id < GaSpaceDimension &&
                _scalarValuesDictionary.ContainsKey(id);
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return id >= 0 && id < GaSpaceDimension &&
                _scalarValuesDictionary.ContainsKey(id);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension &&
                _scalarValuesDictionary.Any(p => p.Key.BasisBladeGrade() == grade);
        }


        public override GaNumVector GetStoredVector()
        {
            var scalarValues =
                new GaSarMultivectorAsVectorReadOnlyList<double>(
                    VSpaceDimension, 
                    _scalarValuesDictionary
                );

            return new GaNumVector(scalarValues);
        }

        public override GaNumDarKVector GetStoredDarKVector(int grade)
        {
            var scalarValues =
                new GaSarMultivectorAsDarKVectorReadOnlyList<double>(
                    VSpaceDimension, 
                    grade, 
                    _scalarValuesDictionary
                );

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override GaNumSarKVector GetStoredSarKVector(int grade)
        {
            var scalarValues =
                new GaSarMultivectorAsSarKVectorReadOnlyDictionary<double>(
                    VSpaceDimension, 
                    grade,
                    _scalarValuesDictionary
                );

            return new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override IGaNumKVector GetStoredKVector(int grade)
        {
            return GetStoredDarKVector(grade);
        }


        public override GaNumVector GetStoredVectorCopy()
        {
            var scalarValues = new double[VSpaceDimension];

            var valueAddedFlag = false;
            for (var i = 0; i < VSpaceDimension; i++)
                if (_scalarValuesDictionary.TryGetValue(i, out var value))
                {
                    valueAddedFlag = true;
                    scalarValues[i] = value;
                }

            return valueAddedFlag 
                ? new GaNumVector(scalarValues) 
                : null;
        }

        public override IGaNumKVector GetStoredKVectorCopy(int grade)
        {
            return GetStoredSarKVectorCopy(grade);
        }

        public override GaNumDarKVector GetStoredDarKVectorCopy(int grade)
        {
            var kvSpaceDim = GetKvSpaceDimension(grade);
            var scalarValues = new double[kvSpaceDim];

            var pairsList =
                _scalarValuesDictionary.Where(pair => pair.Key.BasisBladeGrade() == grade);

            var valueAddedFlag = false;
            foreach (var pair in pairsList)
            {
                scalarValues[pair.Key] = pair.Value;
                valueAddedFlag = true;
            }

            return valueAddedFlag 
                ? new GaNumDarKVector(VSpaceDimension, grade, scalarValues) 
                : null;
        }

        public override GaNumSarKVector GetStoredSarKVectorCopy(int grade)
        {
            var scalarValues = new Dictionary<int, double>();

            var pairsList =
                _scalarValuesDictionary.Where(pair => pair.Key.BasisBladeGrade() == grade);

            foreach (var pair in pairsList)
                scalarValues[pair.Key] = pair.Value;

            return scalarValues.Count > 0
                ? new GaNumSarKVector(VSpaceDimension, grade, scalarValues)
                : null;
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues =
                new SparseReadOnlyList<double>(GaSpaceDimension, _scalarValuesDictionary);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                kVectorsArray[grade] =
                    new GaSarMultivectorAsDarKVectorReadOnlyList<double>(
                        VSpaceDimension, 
                        grade, 
                        _scalarValuesDictionary
                    );
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            return new GaNumSarMultivector(VSpaceDimension, _scalarValuesDictionary);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var kVectorsArray = new Dictionary<int, double>[VSpaceDimension + 1];

            var termsList =
                _scalarValuesDictionary
                    .Where(p => !p.Value.IsNearZero());

            foreach (var term in termsList)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);

                if (kVectorsArray[grade] == null)
                    kVectorsArray[grade] = new Dictionary<int, double>();

                kVectorsArray[grade].Add(index, term.Value);
            }

            return new GaNumSgrMultivector(
                kVectorsArray.ToSgrKVectorsList()
            );
        }


        public override GaNumDarMultivector GetDarMultivectorCopy()
        {
            var scalarValues = new double[GaSpaceDimension];

            foreach (var pair in _scalarValuesDictionary)
                scalarValues[pair.Key] = pair.Value;

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivectorCopy()
        {
            var activeGrades = new bool[VSpaceDimension + 1];
            var kVectorsArray = new double[VSpaceDimension + 1][];

            foreach (var pair in _scalarValuesDictionary)
            {
                pair.Key.BasisBladeGradeIndex(out var grade, out var index);

                if (!activeGrades[grade])
                {
                    activeGrades[grade] = true;
                    kVectorsArray[grade] = new double[GetKvSpaceDimension(grade)];
                }

                kVectorsArray[grade][index] = pair.Value;
            }

            return new GaNumDgrMultivector(
                kVectorsArray.Cast<IReadOnlyList<double>>().ToArray()
            );
        }

        public override GaNumSarMultivector GetSarMultivectorCopy()
        {
            var scalarValues =
                _scalarValuesDictionary
                    .Where(pair => !pair.Value.IsNearZero())
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value
                    );

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivectorCopy()
        {
            var kVectorsArray = new Dictionary<int, double>[VSpaceDimension + 1];

            var termsList =
                _scalarValuesDictionary
                    .Where(p => !p.Value.IsNearZero());

            foreach (var term in termsList)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);

                if (kVectorsArray[grade] == null)
                    kVectorsArray[grade] = new Dictionary<int, double>();

                kVectorsArray[grade].Add(index, term.Value);
            }

            return new GaNumSgrMultivector(
                kVectorsArray.ToSgrKVectorsList()
            );
        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumSarMultivectorFactory(this);
        }


        public override GaNumMultivectorFactory ApplyScaling(double scalingFactor)
        {
            var scalarValuesDictionary = new Dictionary<int, double>(_scalarValuesDictionary.Count);

            foreach (var pair in _scalarValuesDictionary)
                scalarValuesDictionary.Add(
                    pair.Key,
                    scalingFactor * pair.Value
                );

            _scalarValuesDictionary = scalarValuesDictionary;

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<double, double> mappingFunc)
        {
            var scalarValuesDictionary = new Dictionary<int, double>(_scalarValuesDictionary.Count);

            foreach (var pair in _scalarValuesDictionary)
                scalarValuesDictionary.Add(
                    pair.Key, 
                    mappingFunc(pair.Value)
                );

            _scalarValuesDictionary = scalarValuesDictionary;

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, double, double> mappingFunc)
        {
            var scalarValuesDictionary = new Dictionary<int, double>(_scalarValuesDictionary.Count);

            foreach (var pair in _scalarValuesDictionary)
                scalarValuesDictionary.Add(
                    pair.Key,
                    mappingFunc(pair.Key, pair.Value)
                );

            _scalarValuesDictionary = scalarValuesDictionary;

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, int, double, double> mappingFunc)
        {
            var scalarValuesDictionary = new Dictionary<int, double>(_scalarValuesDictionary.Count);

            foreach (var pair in _scalarValuesDictionary)
            {
                pair.Key.BasisBladeGradeIndex(out var grade, out var index);

                scalarValuesDictionary.Add(
                    pair.Key,
                    mappingFunc(grade, index, pair.Value)
                );
            }

            _scalarValuesDictionary = scalarValuesDictionary;

            return this;
        }


        public override GaNumMultivectorFactory SetTerm(int id, double value)
        {
            if (_scalarValuesDictionary.ContainsKey(id))
                _scalarValuesDictionary[id] = value;
            else
                _scalarValuesDictionary.Add(id, value);

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(int grade, int index, double value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (_scalarValuesDictionary.ContainsKey(id))
                _scalarValuesDictionary[id] = value;
            else
                _scalarValuesDictionary.Add(id, value);

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(GaTerm<double> term)
        {
            if (_scalarValuesDictionary.ContainsKey(term.BasisBladeId))
                _scalarValuesDictionary[term.BasisBladeId] = term.ScalarValue;
            else
                _scalarValuesDictionary.Add(term.BasisBladeId, term.ScalarValue);

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(double scalingFactor, GaTerm<double> term)
        {
            if (_scalarValuesDictionary.ContainsKey(term.BasisBladeId))
                _scalarValuesDictionary[term.BasisBladeId] = scalingFactor * term.ScalarValue;
            else
                _scalarValuesDictionary.Add(term.BasisBladeId, scalingFactor * term.ScalarValue);

            return this;
        }


        public override GaNumMultivectorFactory SetKVector(int grade, IReadOnlyList<double> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                SetTerm(grade, index, scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                SetTerm(grade, index, scalingFactor * scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTerm(grade, pair.Key, pair.Value);

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                SetTerm(grade, pair.Key, scalingFactor * pair.Value);

            return this;
        }


        public override GaNumMultivectorFactory AddTerm(int id, double value)
        {
            if (_scalarValuesDictionary.ContainsKey(id))
                _scalarValuesDictionary[id] += value;
            else
                _scalarValuesDictionary.Add(id, value);

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(int grade, int index, double value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (_scalarValuesDictionary.ContainsKey(id))
                _scalarValuesDictionary[id] += value;
            else
                _scalarValuesDictionary.Add(id, value);

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(GaTerm<double> term)
        {
            if (_scalarValuesDictionary.ContainsKey(term.BasisBladeId))
                _scalarValuesDictionary[term.BasisBladeId] += term.ScalarValue;
            else
                _scalarValuesDictionary.Add(term.BasisBladeId, term.ScalarValue);

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(double scalingFactor, GaTerm<double> term)
        {
            if (_scalarValuesDictionary.ContainsKey(term.BasisBladeId))
                _scalarValuesDictionary[term.BasisBladeId] += scalingFactor * term.ScalarValue;
            else
                _scalarValuesDictionary.Add(term.BasisBladeId, scalingFactor * term.ScalarValue);

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
            for (var index = 0; index < scalarValuesList.Count; index++)
                AddTerm(grade, index, scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            for (var index = 0; index < scalarValuesList.Count; index++)
                AddTerm(grade, index, scalingFactor * scalarValuesList[index]);

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(grade, pair.Key, pair.Value);

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
                AddTerm(grade, pair.Key, scalingFactor * pair.Value);

            return this;
        }


        public override GaNumMultivectorFactory ApplyReverse()
        {
            var idsList =
                _scalarValuesDictionary
                    .Where(p => p.Key.BasisBladeIdHasNegativeReverse())
                    .Select(p => p.Key);

            foreach (var id in idsList)
                _scalarValuesDictionary[id] = -_scalarValuesDictionary[id];

            return this;
        }

        public override GaNumMultivectorFactory ApplyGradeInv()
        {
            var idsList =
                _scalarValuesDictionary
                    .Where(p => p.Key.BasisBladeIdHasNegativeGradeInv())
                    .Select(p => p.Key);

            foreach (var id in idsList)
                _scalarValuesDictionary[id] = -_scalarValuesDictionary[id];

            return this;
        }

        public override GaNumMultivectorFactory ApplyCliffConj()
        {
            var idsList =
                _scalarValuesDictionary
                    .Where(p => p.Key.BasisBladeIdHasNegativeCliffConj())
                    .Select(p => p.Key);

            foreach (var id in idsList)
                _scalarValuesDictionary[id] = -_scalarValuesDictionary[id];

            return this;
        }

        public override GaNumMultivectorFactory ApplyNegative()
        {
            var idsList =
                _scalarValuesDictionary
                    .Select(p => p.Key);

            foreach (var id in idsList)
                _scalarValuesDictionary[id] = -_scalarValuesDictionary[id];

            return this;
        }
    }
}