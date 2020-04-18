using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Structures.Collections;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    /// <summary>
    /// A multivector factory where the internal storage is a full array for storing scalar values of terms.
    /// </summary>
    public sealed class GaNumDarMultivectorFactory : GaNumMultivectorFactory
    {
        private double[] _scalarValuesArray;


        public override int StoredTermsCount 
            => GaSpaceDimension;

        public override double this[int id]
        {
            get => _scalarValuesArray[id];
            set => _scalarValuesArray[id] = value;
        }

        public override double this[int grade, int index]
        {
            get => _scalarValuesArray[GaNumFrameUtils.BasisBladeId(grade, index)];
            set => _scalarValuesArray[GaNumFrameUtils.BasisBladeId(grade, index)] = value;
        }


        public GaNumDarMultivectorFactory(int vSpaceDim) 
            : base(vSpaceDim)
        {
            _scalarValuesArray = new double[GaSpaceDimension];
        }

        public GaNumDarMultivectorFactory(GaNumDarMultivector mv)
            : base(mv.VSpaceDimension)
        {
            _scalarValuesArray = new double[GaSpaceDimension];

            for (var id = 0; id < GaSpaceDimension; id++)
                _scalarValuesArray[id] = mv.ScalarValuesArray[id];
        }

        public GaNumDarMultivectorFactory(GaNumDarMultivectorFactory factory)
            : base(factory.VSpaceDimension)
        {
            _scalarValuesArray = new double[GaSpaceDimension];

            for (var id = 0; id < GaSpaceDimension; id++)
                _scalarValuesArray[id] = factory._scalarValuesArray[id];
        }


        public override GaNumMultivectorFactory Reset()
        {
            _scalarValuesArray = new double[GaSpaceDimension];

            return this;
        }

        public override GaNumMultivectorFactory RemoveNearZeroTerms()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                if (_scalarValuesArray[id].IsNearZero())
                    _scalarValuesArray[id] = 0.0d;

            return this;
        }


        public override bool TryGetValue(int id, out double value)
        {
            if (id < 0 || id >= GaSpaceDimension)
            {
                value = 0.0d;
                return false;
            }

            value = _scalarValuesArray[id];
            return true;
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            if (id < 0 || id >= GaSpaceDimension)
            {
                value = 0.0d;
                return false;
            }

            value = _scalarValuesArray[id];
            return true;
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            if (id < 0 || id >= GaSpaceDimension)
            {
                term = null;
                return false;
            }

            term = new GaTerm<double>(id, _scalarValuesArray[id]);
            return true;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            if (id < 0 || id >= GaSpaceDimension)
            {
                term = null;
                return false;
            }

            term = new GaTerm<double>(id, _scalarValuesArray[id]);
            return true;
        }


        public override bool IsEmpty()
        {
            return false;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
            => _scalarValuesArray.Select(
                (value, id) => new GaTerm<double>(id, value)
            );

        public override IEnumerable<GaTerm<double>> GetStoredTerms(int grade)
        {
            var kvSpaceDim = GetKvSpaceDimension(grade);

            for (var index = 0; index < kvSpaceDim; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                yield return new GaTerm<double>(id, _scalarValuesArray[id]);
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var value = _scalarValuesArray[id];

                if (!value.IsNearZero())
                    yield return new GaTerm<double>(id, value);
            }
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms(int grade)
        {
            var kvSpaceDim = GetKvSpaceDimension(grade);

            for (var index = 0; index < kvSpaceDim; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                var value = _scalarValuesArray[id];

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
            foreach (var value in _scalarValuesArray)
            {
                if (!value.IsNearZero())
                    yield return id;

                id++;
            }
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            return _scalarValuesArray;
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            return _scalarValuesArray.Where(v => !v.IsNearZero());
        }


        public override bool ContainsStoredTerm(int id)
        {
            return (id >= 0 && id < GaSpaceDimension);
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            return (id >= 0 && id < GaSpaceDimension);
        }

        public override bool ContainsStoredKVector(int grade)
        {
            return (grade >= 0 && grade <= VSpaceDimension);
        }


        public override GaNumVector GetStoredVector()
        {
            var scalarValues =
                new GaDarMultivectorAsVectorReadOnlyList<double>(_scalarValuesArray);

            return new GaNumVector(scalarValues);
        }

        public override IGaNumKVector GetStoredKVector(int grade)
        {
            return GetStoredDarKVector(grade);
        }

        public override GaNumDarKVector GetStoredDarKVector(int grade)
        {
            var scalarValues =
                new GaDarMultivectorAsDarKVectorReadOnlyList<double>(grade, _scalarValuesArray);

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override GaNumSarKVector GetStoredSarKVector(int grade)
        {
            var kvSpaceDim = GetKvSpaceDimension(grade);
            var scalarValuesDict = new Dictionary<int, double>();

            for (var index = 0; index < kvSpaceDim; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                var value = _scalarValuesArray[id];

                if (!value.IsNearZero())
                    scalarValuesDict.Add(id, value);
            }

            return scalarValuesDict.Count == 0 
                ? null 
                : new GaNumSarKVector(VSpaceDimension, grade, scalarValuesDict);
        }


        public override GaNumVector GetStoredVectorCopy()
        {
            var scalarValues = new double[VSpaceDimension];

            for (var index = 0; index < VSpaceDimension; index++)
                scalarValues[index] = _scalarValuesArray[1 << index];

            return new GaNumVector(scalarValues);
        }

        public override IGaNumKVector GetStoredKVectorCopy(int grade)
        {
            return GetStoredDarKVectorCopy(grade);
        }

        public override GaNumDarKVector GetStoredDarKVectorCopy(int grade)
        {
            var kvSpaceDim = GetKvSpaceDimension(grade);
            var scalarValues = new double[kvSpaceDim];

            for (var index = 0; index < kvSpaceDim; index++)
                scalarValues[index] = _scalarValuesArray[GaNumFrameUtils.BasisBladeId(grade, index)];

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override GaNumSarKVector GetStoredSarKVectorCopy(int grade)
        {
            var kvSpaceDim = GetKvSpaceDimension(grade);
            var scalarValues = new Dictionary<int, double>();

            for (var index = 0; index < kvSpaceDim; index++)
            {
                var value = 
                    _scalarValuesArray[GaNumFrameUtils.BasisBladeId(grade, index)];

                if (!value.IsNearZero())
                    scalarValues.Add(index, value);
            }

            return
                scalarValues.Count == 0
                ? null
                : new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            return new GaNumDarMultivector(_scalarValuesArray);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                kVectorsArray[grade] =
                    new GaDarMultivectorAsDarKVectorReadOnlyList<double>(grade, _scalarValuesArray);
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues = new Dictionary<int, double>();

            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var value = _scalarValuesArray[id];

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


        public override GaNumDarMultivector GetDarMultivectorCopy()
        {
            var scalarValues = new double[GaSpaceDimension];
            _scalarValuesArray.CopyTo(scalarValues, 0);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivectorCopy()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDim = GetKvSpaceDimension(grade);
                var scalarValues = new double[kvSpaceDim];

                for (var index = 0; index < kvSpaceDim; index++)
                    scalarValues[index] = _scalarValuesArray[GaNumFrameUtils.BasisBladeId(grade, index)];

                kVectorsArray[grade] = scalarValues;
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivectorCopy()
        {
            var scalarValues = new Dictionary<int, double>();

            for (var id = 0; id < GaSpaceDimension; id++)
            {
                var value = _scalarValuesArray[id];

                if (!value.IsNearZero())
                    scalarValues.Add(id, value);
            }

            return new GaNumSarMultivector(VSpaceDimension, scalarValues);
        }

        public override GaNumSgrMultivector GetSgrMultivectorCopy()
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


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumDarMultivectorFactory(this);
        }


        public override GaNumMultivectorFactory SetTerm(int id, double value)
        {
            _scalarValuesArray[id] = value;

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(int grade, int index, double value)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            _scalarValuesArray[id] = value;

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(GaTerm<double> term)
        {
            _scalarValuesArray[term.BasisBladeId] = term.ScalarValue;

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(double scalingFactor, GaTerm<double> term)
        {
            _scalarValuesArray[term.BasisBladeId] = scalingFactor * term.ScalarValue;

            return this;
        }


        public override GaNumMultivectorFactory SetKVector(int grade, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension) 
                throw new InvalidOperationException();

            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                _scalarValuesArray[id] = scalarValuesList[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                _scalarValuesArray[id] = scalingFactor * scalarValuesList[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, pair.Key);

                _scalarValuesArray[id] = pair.Value;
            }

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, pair.Key);

                _scalarValuesArray[id] = scalingFactor * pair.Value;
            }

            return this;
        }


        public override GaNumMultivectorFactory AddTerm(int id, double value)
        {
            _scalarValuesArray[id] += value;

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(int grade, int index, double value)
        {
            var id = GaNumFrameUtils.BasisBladeId(grade, index);

            _scalarValuesArray[id] += value;

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(GaTerm<double> term)
        {
            _scalarValuesArray[term.BasisBladeId] += term.ScalarValue;

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(double scalingFactor, GaTerm<double> term)
        {
            _scalarValuesArray[term.BasisBladeId] += scalingFactor * term.ScalarValue;

            return this;
        }

        public override GaNumMultivectorFactory AddTerms(IEnumerable<GaTerm<double>> termsList)
        {
            foreach (var term in termsList)
                _scalarValuesArray[term.BasisBladeId] += term.ScalarValue;

            return this;
        }

        public override GaNumMultivectorFactory AddTerms(double scalingFactor, IEnumerable<GaTerm<double>> termsList)
        {
            foreach (var term in termsList)
                _scalarValuesArray[term.BasisBladeId] += scalingFactor * term.ScalarValue;

            return this;
        }


        public override GaNumMultivectorFactory AddKVector(int grade, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                _scalarValuesArray[id] += scalarValuesList[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != kvSpaceDimension)
                throw new InvalidOperationException();

            for (var index = 0; index < kvSpaceDimension; index++)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, index);

                _scalarValuesArray[id] += scalingFactor * scalarValuesList[index];
            }

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, pair.Key);

                _scalarValuesArray[id] += pair.Value;
            }

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<int, double>> scalarValuesList)
        {
            foreach (var pair in scalarValuesList)
            {
                var id = GaNumFrameUtils.BasisBladeId(grade, pair.Key);

                _scalarValuesArray[id] += scalingFactor * pair.Value;
            }

            return this;
        }


        public override GaNumMultivectorFactory ApplyReverse()
        {
            var idsList = 
                Enumerable
                    .Range(0, GaSpaceDimension)
                    .Where(id => id.BasisBladeIdHasNegativeReverse());

            foreach (var id in idsList)
                _scalarValuesArray[id] = -_scalarValuesArray[id];

            return this;
        }

        public override GaNumMultivectorFactory ApplyGradeInv()
        {
            var idsList =
                Enumerable
                    .Range(0, GaSpaceDimension)
                    .Where(id => id.BasisBladeIdHasNegativeGradeInv());

            foreach (var id in idsList)
                _scalarValuesArray[id] = -_scalarValuesArray[id];

            return this;
        }

        public override GaNumMultivectorFactory ApplyCliffConj()
        {
            var idsList =
                Enumerable
                    .Range(0, GaSpaceDimension)
                    .Where(id => id.BasisBladeIdHasNegativeCliffConj());

            foreach (var id in idsList)
                _scalarValuesArray[id] = -_scalarValuesArray[id];

            return this;
        }

        public override GaNumMultivectorFactory ApplyNegative()
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                _scalarValuesArray[id] = -_scalarValuesArray[id];

            return this;
        }


        public override GaNumMultivectorFactory ApplyScaling(double scalingFactor)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                _scalarValuesArray[id] *= scalingFactor;

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<double, double> mappingFunc)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                _scalarValuesArray[id] = mappingFunc(_scalarValuesArray[id]);

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, double, double> mappingFunc)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
                _scalarValuesArray[id] = mappingFunc(id, _scalarValuesArray[id]);

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, int, double, double> mappingFunc)
        {
            for (var id = 0; id < GaSpaceDimension; id++)
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                _scalarValuesArray[id] = mappingFunc(grade, index, _scalarValuesArray[id]);
            }

            return this;
        }
    }
}