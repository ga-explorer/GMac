﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.Structures.Collections;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    /// <summary>
    /// A multivector factory where the internal storage is an array of graded dense k-vectors
    /// </summary>
    public sealed class GaNumDgrMultivectorFactory : GaNumMultivectorFactory
    {
        private double[][] _gradedScalarValuesArray;

        private double[] GetOrCreateScalarValuesArray(int grade)
        {
            return _gradedScalarValuesArray[grade] ??
                   (_gradedScalarValuesArray[grade] = new double[GetKvSpaceDimension(grade)]);
        }


        public override int StoredTermsCount
            => _gradedScalarValuesArray
                .Where(a => !a.IsNullOrEmpty())
                .Sum(a => a.Length);

        public override double this[ulong id]
        {
            get
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var scalarValues = _gradedScalarValuesArray[grade];

                return scalarValues.IsNullOrEmpty() 
                    ? 0.0d : scalarValues[index];
            }
            set 
            {
                id.BasisBladeGradeIndex(out var grade, out var index);

                var scalarValuesArray = GetOrCreateScalarValuesArray(grade);

                scalarValuesArray[index] = value;
            }
        }

        public override double this[int grade, ulong index]
        {
            get
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                return scalarValues.IsNullOrEmpty()
                    ? 0.0d : scalarValues[index];
            }
            set
            {
                var scalarValuesArray = GetOrCreateScalarValuesArray(grade);

                scalarValuesArray[index] = value;
            }
        }


        public GaNumDgrMultivectorFactory(int vSpaceDim)
            : base(vSpaceDim)
        {
            _gradedScalarValuesArray = new double[VSpaceDimension + 1][];
        }

        public GaNumDgrMultivectorFactory(GaNumVector mv)
            : base(mv.VSpaceDimension)
        {
            _gradedScalarValuesArray = new double[VSpaceDimension + 1][];

            var srcScalarValues =
                mv.ScalarValuesArray;

            if (srcScalarValues.IsNullOrEmpty())
                return;

            _gradedScalarValuesArray[1] = new double[VSpaceDimension];

            var dstScalarValues = _gradedScalarValuesArray[1];
            for (var index = 0; index < VSpaceDimension; index++)
                dstScalarValues[index] = srcScalarValues[index];
        }

        public GaNumDgrMultivectorFactory(GaNumDarKVector mv)
            : base(mv.VSpaceDimension)
        {
            _gradedScalarValuesArray = new double[VSpaceDimension + 1][];

            var srcScalarValues =
                mv.ScalarValuesArray;

            if (srcScalarValues.IsNullOrEmpty())
                return;

            var grade = mv.Grade;
            var kvSpaceDim = GetKvSpaceDimension(grade);
            _gradedScalarValuesArray[grade] = new double[kvSpaceDim];

            var dstScalarValues = _gradedScalarValuesArray[grade];
            for (var index = 0UL; index < kvSpaceDim; index++)
                dstScalarValues[index] = srcScalarValues[(int)index];
        }

        public GaNumDgrMultivectorFactory(GaNumDgrMultivector mv) 
            : base(mv.VSpaceDimension)
        {
            _gradedScalarValuesArray = new double[VSpaceDimension + 1][];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues = 
                    mv.GradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                var kvSpaceDim = GetKvSpaceDimension(grade);
                _gradedScalarValuesArray[grade] = new double[kvSpaceDim];

                var dstScalarValues = _gradedScalarValuesArray[grade];
                for (var index = 0UL; index < kvSpaceDim; index++)
                    dstScalarValues[index] = srcScalarValues[(int)index];
            }
        }

        public GaNumDgrMultivectorFactory(GaNumDgrMultivectorFactory factory)
            : base(factory.VSpaceDimension)
        {
            _gradedScalarValuesArray = new double[VSpaceDimension + 1][];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues =
                    factory._gradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                var kvSpaceDim = GetKvSpaceDimension(grade);
                _gradedScalarValuesArray[grade] = new double[kvSpaceDim];

                var dstScalarValues = _gradedScalarValuesArray[grade];
                for (var index = 0UL; index < kvSpaceDim; index++)
                    dstScalarValues[index] = srcScalarValues[index];
            }
        }


        public override GaNumMultivectorFactory Reset()
        {
            _gradedScalarValuesArray = new double[VSpaceDimension + 1][];

            return this;
        }

        public override GaNumMultivectorFactory RemoveNearZeroTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];

                if (scalarValues == null)
                    continue;

                if (scalarValues.Length == 0)
                {
                    _gradedScalarValuesArray[grade] = null;
                    continue;
                }

                var clearKVectorFlag = true;
                for (var index = 0; index < scalarValues.Length; index++)
                {
                    if (scalarValues[index].IsNearZero())
                        scalarValues[index] = 0.0d;
                    else
                        clearKVectorFlag = false;
                }

                if (clearKVectorFlag)
                    _gradedScalarValuesArray[grade] = null;
            }

            return this;
        }


        public override bool TryGetValue(ulong id, out double value)
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

        public override bool TryGetValue(int grade, ulong index, out double value)
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

        public override bool TryGetTerm(ulong id, out GaTerm<double> term)
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

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<double> term)
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
                var scalarValuesArray = 
                    _gradedScalarValuesArray[grade];

                if (scalarValuesArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(grade, index),
                        scalarValuesArray[index]
                    );
            }
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            var scalarValuesArray = 
                _gradedScalarValuesArray[grade];

            if (scalarValuesArray.IsNullOrEmpty())
                yield break;

            for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
                yield return new GaTerm<double>(
                    GaFrameUtils.BasisBladeId(grade, index),
                    scalarValuesArray[index]
                );
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesArray = 
                    _gradedScalarValuesArray[grade];

                if (scalarValuesArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
                {
                    var value = scalarValuesArray[index];

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
            var scalarValuesArray = _gradedScalarValuesArray[grade];

            if (scalarValuesArray.IsNullOrEmpty())
                yield break;

            for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
            {
                var value = scalarValuesArray[index];

                if (!value.IsNearZero())
                    yield return new GaTerm<double>(
                        GaFrameUtils.BasisBladeId(grade, index),
                        value
                    );
            }
        }


        public override IEnumerable<ulong> GetStoredTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _gradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)kVectorArray.Length; index++)
                    yield return GaFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<ulong> GetNonZeroTermIds()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _gradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)kVectorArray.Length; index++)
                    if (!kVectorArray[index].IsNearZero())
                        yield return GaFrameUtils.BasisBladeId(grade, index);
            }
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _gradedScalarValuesArray[grade];

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
                var kVectorArray = _gradedScalarValuesArray[grade];

                if (kVectorArray.IsNullOrEmpty())
                    continue;

                foreach (var value in kVectorArray)
                    if (!value.IsNearZero())
                        yield return value;
            }
        }


        public override bool ContainsStoredTerm(ulong id)
        {
            var grade = id.BasisBladeGrade();

            return id < GaSpaceDimension &&
                !_gradedScalarValuesArray[grade].IsNullOrEmpty();
        }

        public override bool ContainsStoredTerm(int grade, ulong index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return id < GaSpaceDimension &&
                !_gradedScalarValuesArray[grade].IsNullOrEmpty();
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension &&
                !_gradedScalarValuesArray[grade].IsNullOrEmpty();
        }


        public override GaNumVector GetStoredVector()
        {
            var vectorScalarValues = 
                _gradedScalarValuesArray[1];

            return vectorScalarValues.IsNullOrEmpty() 
                ? null 
                : new GaNumVector(vectorScalarValues);
        }

        public override GaNumDarKVector GetStoredDarKVector(int grade)
        {
            var kVectorScalarValues = 
                _gradedScalarValuesArray[grade];

            return kVectorScalarValues.IsNullOrEmpty()
                ? null
                : new GaNumDarKVector(VSpaceDimension, grade, kVectorScalarValues);
        }

        public override GaNumSarKVector GetStoredSarKVector(int grade)
        {
            var kVectorScalarValues = 
                _gradedScalarValuesArray[grade];

            if (kVectorScalarValues.IsNullOrEmpty())
                return null;

            var scalarValues = new Dictionary<ulong, double>();
            for (var index = 0UL; index < (ulong)kVectorScalarValues.Length; index++)
            {
                var value = kVectorScalarValues[index];

                if (!value.IsNearZero())
                    scalarValues.Add(index, value);
            }

            return scalarValues.Count == 0
                ? null
                : new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override IGaNumKVector GetStoredKVector(int grade)
        {
            return GetStoredDarKVector(grade);
        }


        public override GaNumVector GetStoredVectorCopy()
        {
            var kVector = _gradedScalarValuesArray[1];

            if (kVector.IsNullOrEmpty())
                return null;

            var scalarValues = new double[VSpaceDimension];
            kVector.CopyTo(scalarValues, 0);

            return new GaNumVector(scalarValues);
        }

        public override IGaNumKVector GetStoredKVectorCopy(int grade)
        {
            return GetStoredDarKVector(grade);
        }

        public override GaNumDarKVector GetStoredDarKVectorCopy(int grade)
        {
            var kVector = _gradedScalarValuesArray[grade];
            if (kVector.IsNullOrEmpty())
                return null;

            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            var scalarValues = new double[kvSpaceDim];
            kVector.CopyTo(scalarValues, 0);

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public override GaNumSarKVector GetStoredSarKVectorCopy(int grade)
        {
            var kVector = _gradedScalarValuesArray[grade];

            if (kVector.IsNullOrEmpty())
                return null;

            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            var scalarValues = new Dictionary<ulong, double>();

            for (var index = 0UL; index < kvSpaceDim; index++)
            {
                var value = kVector[index];

                if (!value.IsNearZero())
                    scalarValues.Add(index, value);
            }

            return scalarValues.Count == 0
                ? null
                : new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues = new GaDgrMultivectorAsDarMultivectorReadOnlyList<double>(
                _gradedScalarValuesArray.Cast<IReadOnlyList<double>>().ToArray()
            );

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var scalarValues =
                _gradedScalarValuesArray.Cast<IReadOnlyList<double>>().ToArray();

            return new GaNumDgrMultivector(scalarValues);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            var scalarValues = new Dictionary<ulong, double>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesArray =
                    _gradedScalarValuesArray[grade];

                if (scalarValuesArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
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
            var kVectorsArray = new Dictionary<ulong, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues = _gradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                var dstScalarValues = new Dictionary<ulong, double>();

                for (var index = 0UL; index < (ulong)srcScalarValues.Length; index++)
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


        public override GaNumDarMultivector GetDarMultivectorCopy()
        {
            var scalarValues = new double[GaSpaceDimension];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesArray = _gradedScalarValuesArray[grade];

                if (scalarValuesArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
                {
                    var value = scalarValuesArray[index];

                    if (value.IsNearZero())
                        continue;

                    var id = GaFrameUtils.BasisBladeId(grade, index);

                    scalarValues[id] = value;
                }
            }

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivectorCopy()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVector = _gradedScalarValuesArray[grade];

                if (kVector.IsNullOrEmpty())
                    continue;

                var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
                var scalarValues = new double[kvSpaceDim];
                kVector.CopyTo(scalarValues, 0);

                kVectorsArray[grade] = scalarValues;
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivectorCopy()
        {
            var scalarValues = new Dictionary<ulong, double>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValuesArray =
                    _gradedScalarValuesArray[grade];

                if (scalarValuesArray.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)scalarValuesArray.Length; index++)
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

        public override GaNumSgrMultivector GetSgrMultivectorCopy()
        {
            var kVectorsArray = new Dictionary<ulong, double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var srcScalarValues = _gradedScalarValuesArray[grade];

                if (srcScalarValues.IsNullOrEmpty())
                    continue;

                var dstScalarValues = new Dictionary<ulong, double>();

                for (var index = 0UL; index < (ulong)srcScalarValues.Length; index++)
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


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumDgrMultivectorFactory(this);
        }


        public override GaNumMultivectorFactory SetTerm(ulong id, double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] = value;

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(int grade, ulong index, double value)
        {
            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] = value;

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] = term.ScalarValue;

            return this;
        }

        public override GaNumMultivectorFactory SetTerm(double scalingFactor, GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] = scalingFactor * term.ScalarValue;

            return this;
        }


        public override GaNumMultivectorFactory SetKVector(int grade, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != (int)kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            for (var index = 0UL; index < kvSpaceDimension; index++)
                scalarValuesArray[index] = scalarValuesList[(int)index];

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != (int)kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            for (var index = 0UL; index < kvSpaceDimension; index++)
                scalarValuesArray[index] = scalingFactor * scalarValuesList[(int)index];

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, IEnumerable<KeyValuePair<ulong, double>> scalarValuesList)
        {
            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            foreach (var pair in scalarValuesList)
                scalarValuesArray[pair.Key] = pair.Value;

            return this;
        }

        public override GaNumMultivectorFactory SetKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<ulong, double>> scalarValuesList)
        {
            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            foreach (var pair in scalarValuesList)
                scalarValuesArray[pair.Key] = scalingFactor * pair.Value;

            return this;
        }


        public override GaNumMultivectorFactory AddTerm(ulong id, double value)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] += value;

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(int grade, ulong index, double value)
        {
            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] += value;

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] += term.ScalarValue;

            return this;
        }

        public override GaNumMultivectorFactory AddTerm(double scalingFactor, GaTerm<double> term)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            scalarValuesArray[index] += scalingFactor * term.ScalarValue;

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

            if (scalarValuesList.Count != (int)kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            for (var index = 0UL; index < kvSpaceDimension; index++)
                scalarValuesArray[index] += scalarValuesList[(int)index];

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IReadOnlyList<double> scalarValuesList)
        {
            var kvSpaceDimension = GetKvSpaceDimension(grade);

            if (scalarValuesList.Count != (int)kvSpaceDimension)
                throw new InvalidOperationException();

            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            for (var index = 0UL; index < kvSpaceDimension; index++)
                scalarValuesArray[index] += scalingFactor * scalarValuesList[(int)index];

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, IEnumerable<KeyValuePair<ulong, double>> scalarValuesList)
        {
            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            foreach (var pair in scalarValuesList)
                scalarValuesArray[pair.Key] += pair.Value;

            return this;
        }

        public override GaNumMultivectorFactory AddKVector(int grade, double scalingFactor, IEnumerable<KeyValuePair<ulong, double>> scalarValuesList)
        {
            var scalarValuesArray = 
                GetOrCreateScalarValuesArray(grade);

            foreach (var pair in scalarValuesList)
                scalarValuesArray[pair.Key] += scalingFactor * pair.Value;

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
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                for (var index = 0; index < scalarValues.Length; index++)
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
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                for (var index = 0; index < scalarValues.Length; index++)
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
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                for (var index = 0; index < scalarValues.Length; index++)
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
                var scalarValues = 
                    _gradedScalarValuesArray[grade];

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = -scalarValues[index];
            }

            return this;
        }


        public override GaNumMultivectorFactory ApplyScaling(double scalingFactor)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] *= scalingFactor;
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<double, double> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0; index < scalarValues.Length; index++)
                    scalarValues[index] = mappingFunc(scalarValues[index]);
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<ulong, double, double> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                if (scalarValues.IsNullOrEmpty()) 
                    continue;

                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                {
                    var id = GaFrameUtils.BasisBladeId(grade, index);

                    scalarValues[index] = mappingFunc(id, scalarValues[index]);
                }
            }

            return this;
        }

        public override GaNumMultivectorFactory ApplyMapping(Func<int, ulong, double, double> mappingFunc)
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var scalarValues = _gradedScalarValuesArray[grade];
                if (scalarValues.IsNullOrEmpty())
                    continue;

                for (var index = 0UL; index < (ulong)scalarValues.Length; index++)
                    scalarValues[index] = mappingFunc(grade, index, scalarValues[index]);
            }

            return this;
        }
    }
}