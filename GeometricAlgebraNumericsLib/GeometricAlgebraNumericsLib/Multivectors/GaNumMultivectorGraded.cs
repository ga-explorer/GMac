using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public sealed class GaNumMultivectorGraded : IGaNumMultivectorMutable
    {
        public static GaNumMultivectorGraded CreateZero(int gaSpaceDim)
        {
            return new GaNumMultivectorGraded(gaSpaceDim);
        }

        public static GaNumMultivectorGraded CreateCopy(IGaNumMultivector mv)
        {
            var resultMv = new GaNumMultivectorGraded(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                resultMv[term.Key] = term.Value;

            return resultMv;
        }


        private readonly double[][] _kVectorsArray;


        public double this[int id] 
        { 
            get
            { 
                GaNumFrameUtils.BasisBladeGradeIndex(id, out var grade, out var index);
                return this[grade, index];
            }
            set
            {
                GaNumFrameUtils.BasisBladeGradeIndex(id, out var grade, out var index);
                this[grade, index] = value;
            }
        }

        public double this[int grade, int index] 
        { 
            get 
            { 
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray == null) 
                    return 0;

                return kVectorArray[index]; 
            }
            set 
            { 
                var kVectorArray = _kVectorsArray[grade];
                    
                if (value == 0)
                {  
                    if (kVectorArray == null)
                        return;

                    kVectorArray[index] = value;
                }
                else
                {
                    if (kVectorArray == null)
                    {
                        var n = GaNumFrameUtils.KvSpaceDimension(
                            VSpaceDimension, 
                            grade
                        );

                        kVectorArray = new double[n];

                        _kVectorsArray[grade] = kVectorArray;
                    }

                    kVectorArray[index] = value;
                }
            }
        }

        public int VSpaceDimension { get; }

        public int GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public IEnumerable<int> BasisBladeIds 
        {
            get
            {
                for (var grade = 0; grade <= VSpaceDimension; grade++)
                {
                    var kVectorArray = _kVectorsArray[grade];

                    if (kVectorArray == null)
                        continue;

                    for (var index = 0; index < kVectorArray.Length; index++)
                        yield return GaNumFrameUtils.BasisBladeId(grade, index);
                }
            }
        }

        public IEnumerable<int> NonZeroBasisBladeIds 
        {
            get
            {
                for (var grade = 0; grade <= VSpaceDimension; grade++)
                {
                    var kVectorArray = _kVectorsArray[grade];

                    if (kVectorArray == null)
                        continue;

                    for (var index = 0; index < kVectorArray.Length; index++)
                        if (!kVectorArray[index].IsNearZero())
                            yield return GaNumFrameUtils.BasisBladeId(grade, index);
                }
            }
        }

        public IEnumerable<double> BasisBladeScalars 
        {
            get
            {
                for (var grade = 0; grade <= VSpaceDimension; grade++)
                {
                    var kVectorArray = _kVectorsArray[grade];

                    if (kVectorArray == null)
                        continue;

                    foreach (var value in kVectorArray)
                        yield return value;
                }
            }
        }

        public IEnumerable<double> NonZeroBasisBladeScalars 
        {
            get
            {
                for (var grade = 0; grade <= VSpaceDimension; grade++)
                {
                    var kVectorArray = _kVectorsArray[grade];

                    if (kVectorArray == null)
                        continue;

                    foreach (var value in kVectorArray)
                        if (!value.IsNearZero())
                            yield return value;
                }
            }
        }

        public IEnumerable<KeyValuePair<int, double>> Terms 
        {
            get
            {
                for (var grade = 0; grade <= VSpaceDimension; grade++)
                {
                    var kVectorArray = _kVectorsArray[grade];

                    if (kVectorArray == null)
                        continue;

                    for (var index = 0; index < kVectorArray.Length; index++)
                        yield return new KeyValuePair<int, double>(
                            GaNumFrameUtils.BasisBladeId(grade, index),
                            kVectorArray[index]
                        );
                }
            }
        }

        public IEnumerable<KeyValuePair<int, double>> NonZeroTerms 
        {
            get
            {
                for (var grade = 0; grade <= VSpaceDimension; grade++)
                {
                    var kVectorArray = _kVectorsArray[grade];

                    if (kVectorArray == null)
                        continue;

                    for (var index = 0; index < kVectorArray.Length; index++)
                    {   
                        var value = kVectorArray[index];

                        if (!value.IsNearZero())
                            yield return new KeyValuePair<int, double>(
                                GaNumFrameUtils.BasisBladeId(grade, index),
                                value
                            ); 
                    }
                }
            }
        }

        public bool IsTemp 
            => false;

        public int TermsCount 
            => _kVectorsArray
                .Where(a => a != null)
                .Sum(a => a.Length);

        
        private GaNumMultivectorGraded(int gaSpaceDim)
        {
            VSpaceDimension = gaSpaceDim.ToVSpaceDimension();
            _kVectorsArray = new double[VSpaceDimension + 1][];
        }


        public bool ContainsBasisBlade(int id)
        {
            var grade = GaNumFrameUtils.BasisBladeGrade(id);

            return _kVectorsArray[grade] != null;
        }

        public GaNumMultivector GetVectorPart()
        {
            throw new NotImplementedException();
        }

        public GaNumKVector GetKVector(int grade)
        {
            var kVectorArray = _kVectorsArray[grade];

            return kVectorArray == null
                ? GaNumKVector.Create(GaSpaceDimension, grade)
                : GaNumKVector.Create(GaSpaceDimension, grade, _kVectorsArray[grade]);
        }

        public IEnumerable<GaNumKVector> GetKVectors()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray != null)
                    yield return GaNumKVector.Create(GaSpaceDimension, grade, kVectorArray);
            }
        }

        public bool IsEmpty()
        {
            return _kVectorsArray.All(a => a == null);
        }

        public bool IsNearZero(double epsilon)
        {
            return BasisBladeScalars.All(s => s.IsNearZero(epsilon));
        }

        public bool IsScalar()
        {
            for (var grade = 1; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray == null)
                    continue;

                foreach (var value in kVectorArray)
                    if (!value.IsNearZero())
                        return false;
            }

            return true;
        }

        public bool IsTerm()
        {
            throw new NotImplementedException();
        }

        public bool IsZero()
        {
            return BasisBladeScalars.All(v => v.IsNearZero());
        }

        public IGaNumMultivectorMutable SetTerm(int id, double scalarValue)
        {
            this[id] = scalarValue;

            return this;
        }

        public IGaNumMultivectorMutable SetTerm(int id, bool isNegative, double scalarValue)
        {
            this[id] = isNegative ? -scalarValue : scalarValue;

            return this;
        }

        public void Simplify()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            { 
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray == null)
                    continue;

                if (kVectorArray.All(v => v.IsNearZero()))
                    _kVectorsArray[grade] = null;
            }
        }

        public double[] TermsToArray()
        {
            var termsArray = new double[GaSpaceDimension];

            foreach (var term in NonZeroTerms)
                termsArray[term.Key] = term.Value;

            return termsArray;
        }

        public GaNumMultivector ToMultivector()
        {
            var mv = GaNumMultivector.CreateZero(GaSpaceDimension);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray == null)
                    continue;

                for (var index = 0; index < kVectorArray.Length; index++)
                {
                    var scalarValue = kVectorArray[index];

                    if (!scalarValue.IsNearZero())
                        mv.SetTerm(grade, index, scalarValue);
                }
            }

            return mv;
        }

        public IGaNumMultivectorMutable UpdateTerm(int id, double scalarValue)
        {
            this[id] = this[id] + scalarValue;

            return this;
        }

        public IGaNumMultivectorMutable UpdateTerm(int id, bool isNegative, double scalarValue)
        {
            var oldValue = this[id];

            this[id] = isNegative
                ? oldValue - scalarValue 
                : oldValue + scalarValue;

            return this;
        }

        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray == null)
                    continue;

                for (var index = 0; index < kVectorArray.Length; index++)
                    yield return new KeyValuePair<int, double>(
                        GaNumFrameUtils.BasisBladeId(grade, index),
                        kVectorArray[index]
                    );
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kVectorArray = _kVectorsArray[grade];

                if (kVectorArray == null)
                    continue;

                for (var index = 0; index < kVectorArray.Length; index++)
                    yield return new KeyValuePair<int, double>(
                        GaNumFrameUtils.BasisBladeId(grade, index),
                        kVectorArray[index]
                    );
            }
        }
    }
}
