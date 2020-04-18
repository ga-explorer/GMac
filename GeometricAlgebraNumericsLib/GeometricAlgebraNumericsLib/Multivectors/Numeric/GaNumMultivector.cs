using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    public abstract class GaNumMultivector : IGaNumMultivector
    {
        private GaBtrInternalNode<double> _cachedBtrRootNode;
        internal GaBtrInternalNode<double> BtrRootNode 
            => _cachedBtrRootNode 
               ?? (_cachedBtrRootNode = GetNonZeroTerms().CreateBtr(VSpaceDimension));


        public int VSpaceDimension { get; }

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public abstract double this[int grade, int index] { get; }

        public abstract double this[int id] { get; }

        public abstract int StoredTermsCount { get; }


        protected GaNumMultivector(int vSpaceDim)
        {
            Debug.Assert(vSpaceDim.IsValidVSpaceDimension());

            VSpaceDimension = vSpaceDim;
        }


        public void ClearBtr()
        {
            _cachedBtrRootNode = null;
        }

        public GaBtrInternalNode<double> GetBtrRootNode()
        {
            return _cachedBtrRootNode ?? 
                   (_cachedBtrRootNode = GetNonZeroTerms().CreateBtr(VSpaceDimension));
        }
        

        public abstract bool IsTerm();

        public abstract bool IsScalar();

        public abstract bool IsZero();

        public abstract bool IsNearZero(double epsilon);


        public abstract bool IsEmpty();

        public abstract bool ContainsStoredTerm(int id);

        public abstract bool ContainsStoredTerm(int grade, int index);

        public abstract bool ContainsStoredKVector(int grade);

        public abstract IEnumerable<int> GetStoredGrades();

        public abstract int GetStoredGradesBitPattern();


        public abstract IEnumerable<GaTerm<double>> GetStoredTerms();

        public abstract IEnumerable<GaTerm<double>> GetStoredTerms(int grade);

        public abstract IEnumerable<GaTerm<double>> GetNonZeroTerms();

        public abstract IEnumerable<GaTerm<double>> GetNonZeroTerms(int grade);

        public abstract IEnumerable<int> GetStoredTermIds();

        public abstract IEnumerable<int> GetNonZeroTermIds();

        public abstract IEnumerable<double> GetStoredTermScalars();

        public abstract IEnumerable<double> GetNonZeroTermScalars();


        public abstract bool TryGetValue(int id, out double value);

        public abstract bool TryGetValue(int grade, int index, out double value);

        public abstract bool TryGetTerm(int id, out GaTerm<double> term);

        public abstract bool TryGetTerm(int grade, int index, out GaTerm<double> term);


        public abstract IGaGbtNode1<double> GetGbtRootNode();

        public abstract IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity);


        public abstract IGaNumMultivector GetNegative();
        
        public abstract IGaNumMultivector GetReverse();

        public abstract IGaNumMultivector GetGradeInv();

        public abstract IGaNumMultivector GetCliffConj();


        public abstract GaNumMultivectorFactory CopyToFactory();


        public abstract IGaNumVector GetVectorPart();

        public abstract IGaNumKVector GetKVectorPart(int grade);

        public abstract IEnumerable<IGaNumKVector> GetKVectorParts();


        public abstract GaNumSarMultivector GetSarMultivector();

        public abstract GaNumSgrMultivector GetSgrMultivector();

        public abstract GaNumDgrMultivector GetDgrMultivector();

        public abstract GaNumDarMultivector GetDarMultivector();


        public GaNumDarMultivector GetDarMultivectorCopy()
        {
            return CopyToFactory().GetDarMultivector();
        }

        public GaNumDgrMultivector GetDgrMultivectorCopy()
        {
            return CopyToFactory().GetDgrMultivector();
        }

        public GaNumSarMultivector GetSarMultivectorCopy()
        {
            return CopyToFactory().GetSarMultivector();
        }

        public GaNumSgrMultivector GetSgrMultivectorCopy()
        {
            return CopyToFactory().GetSgrMultivector();
        }


        public IEnumerator<GaTerm<double>> GetEnumerator()
        {
            return GetNonZeroTerms().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetNonZeroTerms().GetEnumerator();
        }
    }
}