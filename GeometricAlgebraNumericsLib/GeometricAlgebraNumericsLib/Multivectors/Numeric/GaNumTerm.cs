using System.Collections.Generic;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// This class represents a multivector with a single term. Only the scalar
    /// coefficient can be updated, but not the basis blade
    /// </summary>
    public sealed class GaNumTerm : GaNumMultivector, IGaNumKVector
    {
        public static GaNumTerm Create(int vSpaceDim, ulong id, double scalarValue)
        {
            return new GaNumTerm(vSpaceDim, id, scalarValue);
        }

        public static GaNumTerm CreateScalar(int vSpaceDim, double scalarValue)
        {
            return new GaNumTerm(vSpaceDim, 0, scalarValue);
        }

        public static GaNumTerm CreateZero(int vSpaceDim)
        {
            return new GaNumTerm(vSpaceDim, 0, 0);
        }

        public static GaNumTerm CreateBasisBlade(int vSpaceDim, ulong id)
        {
            return new GaNumTerm(vSpaceDim, id);
        }


        public ulong BasisBladeId { get; }

        public double ScalarValue { get; set; }

        public int MaxStoredGrade
            => BasisBladeId.BasisBladeGrade();

        public int Grade
            => BasisBladeId.BasisBladeGrade();

        public ulong KvSpaceDimension
            => GaFrameUtils.KvSpaceDimension(VSpaceDimension, Grade);

        public IReadOnlyList<double> ScalarValuesArray
            => new SingleItemReadOnlyList<double>(
                (int)KvSpaceDimension, 
                (int)BasisBladeId.BasisBladeIndex(), 
                ScalarValue
            );

        public override double this[ulong id] 
            => (id == BasisBladeId) ? ScalarValue : 0.0d;

        public override double this[int grade, ulong index]
        {
            get
            {
                var id = GaFrameUtils.BasisBladeId(grade, index);

                return (id == BasisBladeId) ? ScalarValue : 0.0d;
            }
        }

        public override int StoredTermsCount
            => 1;


        internal GaNumTerm(int vSpaceDim, ulong id)
            : base(vSpaceDim)
        {
            BasisBladeId = id;
            ScalarValue = 1.0d;
        }

        internal GaNumTerm(int vSpaceDim, ulong id, double scalarValue)
            : base(vSpaceDim)
        {
            BasisBladeId = id;
            ScalarValue = scalarValue;
        }



        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            yield return new GaTerm<double>(BasisBladeId, ScalarValue);
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            if (BasisBladeId.BasisBladeGrade() == grade)
                yield return new GaTerm<double>(BasisBladeId, ScalarValue);
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            if (!ScalarValue.IsNearZero())
                yield return new GaTerm<double>(BasisBladeId, ScalarValue);
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            if (BasisBladeId.BasisBladeGrade() == grade && !ScalarValue.IsNearZero())
                yield return new GaTerm<double>(BasisBladeId, ScalarValue);
        }


        public override IEnumerable<ulong> GetStoredTermIds()
        {
            yield return BasisBladeId;
        }

        public override IEnumerable<ulong> GetNonZeroTermIds()
        {
            if (!ScalarValue.IsNearZero())
                yield return BasisBladeId;
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            yield return ScalarValue;
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            if (!ScalarValue.IsNearZero())
                yield return ScalarValue;
        }


        public ulong GetBasisBladeId(ulong index)
        {
            return GaFrameUtils.BasisBladeId(Grade, index);
        }

        public ulong GetBasisBladeIndex(ulong id)
        {
            return id.BasisBladeIndex();
        }

        public override bool IsTerm()
        {
            return true;
        }

        public override bool IsScalar()
        {
            return BasisBladeId == 0 || ScalarValue.IsNearZero();
        }

        public override bool IsZero()
        {
            return ScalarValue.IsNearZero();
        }

        public override bool IsEmpty()
        {
            return false;
        }

        public override bool IsNearZero(double epsilon)
        {
            return ScalarValue.IsNearZero(epsilon);
        }

        public override bool ContainsStoredTerm(ulong id)
        {
            return id == BasisBladeId;
        }

        public override bool ContainsStoredTerm(int grade, ulong index)
        {
            return BasisBladeId == GaFrameUtils.BasisBladeId(grade, index);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return BasisBladeId.BasisBladeGrade() == grade;
        }


        public override GaNumDgrMultivector GetDgrMultivector()
        {
            return GaNumDgrMultivector.CreateCopy(this);
        }

        public override GaNumDarMultivector GetDarMultivector()
        {
            return GaNumDarMultivector.CreateCopy(this);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            return ScalarValue.IsNearZero()
                ? GaNumSarMultivector.CreateZero(VSpaceDimension)
                : GaNumSarMultivector.CreateTerm(VSpaceDimension, BasisBladeId, ScalarValue);
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            return ScalarValue.IsNearZero()
                ? GaNumSgrMultivector.CreateZero(VSpaceDimension)
                : GaNumSgrMultivector.CreateTerm(VSpaceDimension, BasisBladeId, ScalarValue);
        }


        public GaNumDarKVector ToDarKVector()
        {
            BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues = new SingleItemReadOnlyList<double>(
                (int)GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade),
                (int)index,
                ScalarValue
            );

            return new GaNumDarKVector(VSpaceDimension, grade, scalarValues);
        }

        public GaNumSarKVector ToSarKVector()
        {
            BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues = new Dictionary<ulong, double>
            {
                {index, ScalarValue}
            };

            return new GaNumSarKVector(VSpaceDimension, grade, scalarValues);
        }


        public override bool TryGetValue(ulong id, out double value)
        {
            if (id == BasisBladeId)
            {
                value = ScalarValue;
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetValue(int grade, ulong index, out double value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (id == BasisBladeId)
            {
                value = ScalarValue;
                return true;
            }

            value = 0.0d;
            return false;
        }

        public override bool TryGetTerm(ulong id, out GaTerm<double> term)
        {
            if (id == BasisBladeId)
            {
                term = new GaTerm<double>(id, ScalarValue);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, ulong index, out GaTerm<double> term)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (id == BasisBladeId)
            {
                term = new GaTerm<double>(id, ScalarValue);
                return true;
            }

            term = null;
            return false;
        }

        public override IGaNumKVector GetKVectorPart(int grade)
        {
            BasisBladeId.BasisBladeGradeIndex(out var termGrade, out var termIndex);

            if (grade != termGrade)
                return GaNumDarKVector.CreateZero(VSpaceDimension, grade);

            var scalarValues = new SingleItemReadOnlyList<double>(
                (int)GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade),
                (int)termIndex,
                ScalarValue
            );

            return GaNumDarKVector.Create(VSpaceDimension, grade, scalarValues);
        }

        public override IGaNumVector GetVectorPart()
        {
            BasisBladeId.BasisBladeGradeIndex(out var termGrade, out var termIndex);

            if (termGrade != 1)
                return GaNumVector.CreateZero(VSpaceDimension);

            var scalarValues = new SingleItemReadOnlyList<double>(
                VSpaceDimension, 
                (int)termIndex, 
                ScalarValue
            );

            return GaNumVector.Create(scalarValues);
        }

        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            BasisBladeId.BasisBladeGradeIndex(out var termGrade, out var termIndex);

            var scalarValues = new SingleItemReadOnlyList<double>(
                (int)GaFrameUtils.KvSpaceDimension(VSpaceDimension, termGrade),
                (int)termIndex,
                ScalarValue
            );

            yield return GaNumDarKVector.Create(VSpaceDimension, termGrade, scalarValues);
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            yield return BasisBladeId.BasisBladeGrade();
        }

        public override int GetStoredGradesBitPattern()
        {
            return 1 << BasisBladeId.BasisBladeGrade();
        }

        public override IGaGbtNode1<double> GetGbtRootNode()
        {
            return GaGbtBasisBladeNode.CreateRootNode(VSpaceDimension, BasisBladeId);
        }
        
        public override IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity)
        {
            return GaGbtNumTermMultivectorStack1.Create(capacity, this);
        }


        public override IGaNumMultivector GetNegative()
        {
            return new GaNumTerm(
                VSpaceDimension, 
                BasisBladeId, 
                -ScalarValue
            );
        }

        public override IGaNumMultivector GetReverse()
        {
            return new GaNumTerm(
                VSpaceDimension,
                BasisBladeId,
                BasisBladeId.BasisBladeIdHasNegativeReverse() 
                    ? - ScalarValue : ScalarValue
            );
        }

        public override IGaNumMultivector GetGradeInv()
        {
            return new GaNumTerm(
                VSpaceDimension,
                BasisBladeId,
                BasisBladeId.BasisBladeIdHasNegativeGradeInv() 
                    ? -ScalarValue : ScalarValue
            );
        }

        public override IGaNumMultivector GetCliffConj()
        {
            return new GaNumTerm(
                VSpaceDimension,
                BasisBladeId,
                BasisBladeId.BasisBladeIdHasNegativeCliffConj() 
                    ? -ScalarValue : ScalarValue
            );

        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumSarMultivectorFactory(this);
        }


        public override string ToString()
        {
            return "Term <ID: " + BasisBladeId + ", Value: " + ScalarValue.ToString("G") + ">";
        }
    }
}
