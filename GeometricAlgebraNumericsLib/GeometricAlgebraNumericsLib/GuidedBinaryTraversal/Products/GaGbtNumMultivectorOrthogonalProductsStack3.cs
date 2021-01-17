using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.GuidedBinaryTraversal;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Products
{
    public sealed class GaGbtNumMultivectorOrthogonalProductsStack3 : GaGbtStack3
    {
        public static GaGbtNumMultivectorOrthogonalProductsStack3 Create(IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMultivector mv3)
        {
            Debug.Assert(
                mv1.GaSpaceDimension == mv2.GaSpaceDimension &&
                mv2.GaSpaceDimension == mv3.GaSpaceDimension
            );

            var capacity = 
                (mv1.VSpaceDimension + 1) * 
                (mv1.VSpaceDimension + 1) * 
                (mv1.VSpaceDimension + 1);
            
            var stack1 = mv1.CreateGbtStack(capacity);
            var stack2 = mv2.CreateGbtStack(capacity);
            var stack3 = mv3.CreateGbtStack(capacity);

            return new GaGbtNumMultivectorOrthogonalProductsStack3(stack1, stack2, stack3);
        }


        private IGaGbtNumMultivectorStack1 MultivectorStack1 { get; }

        private IGaGbtNumMultivectorStack1 MultivectorStack2 { get; }

        private IGaGbtNumMultivectorStack1 MultivectorStack3 { get; }


        public IGaNumMultivector Multivector1 
            => MultivectorStack1.Multivector;

        public IGaNumMultivector Multivector2 
            => MultivectorStack2.Multivector;

        public IGaNumMultivector Multivector3 
            => MultivectorStack3.Multivector;

        public double TosValue1 
            => MultivectorStack1.TosValue;

        public double TosValue2 
            => MultivectorStack2.TosValue;

        public double TosValue3 
            => MultivectorStack3.TosValue;

        public bool TosIsNonZeroOp
            => GaFrameUtils.IsNonZeroOp(TosId1, TosId2);

        public bool TosIsNonZeroESp
            => GaFrameUtils.IsNonZeroESp(TosId1, TosId2);

        public bool TosIsNonZeroELcp
            => GaFrameUtils.IsNonZeroELcp(TosId1, TosId2);

        public bool TosIsNonZeroERcp
            => GaFrameUtils.IsNonZeroERcp(TosId1, TosId2);

        public bool TosIsNonZeroEFdp
            => GaFrameUtils.IsNonZeroEFdp(TosId1, TosId2);

        public bool TosIsNonZeroEHip
            => GaFrameUtils.IsNonZeroEHip(TosId1, TosId2);

        public bool TosIsNonZeroEAcp
            => GaFrameUtils.IsNonZeroEAcp((int)TosId1, (int)TosId2);

        public bool TosIsNonZeroECp
            => GaFrameUtils.IsNonZeroECp((int)TosId1, (int)TosId2);

        public ulong TosChildIdXor000
            => Stack1.TosChildId0 ^ Stack2.TosChildId0 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor100
            => Stack1.TosChildId1 ^ Stack2.TosChildId0 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor010
            => Stack1.TosChildId0 ^ Stack2.TosChildId1 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor110
            => Stack1.TosChildId1 ^ Stack2.TosChildId1 ^ Stack3.TosChildId0;

        public ulong TosChildIdXor001
            => Stack1.TosChildId0 ^ Stack2.TosChildId0 ^ Stack3.TosChildId1;

        public ulong TosChildIdXor101
            => Stack1.TosChildId1 ^ Stack2.TosChildId0 ^ Stack3.TosChildId1;

        public ulong TosChildIdXor011
            => Stack1.TosChildId0 ^ Stack2.TosChildId1 ^ Stack3.TosChildId1;

        public ulong TosChildIdXor111
            => Stack1.TosChildId1 ^ Stack2.TosChildId1 ^ Stack3.TosChildId1;

        public int TosChildIdXorGrade000
            => TosChildIdXor000.CountOnes();

        public int TosChildIdXorGrade100
            => TosChildIdXor100.CountOnes();

        public int TosChildIdXorGrade010
            => TosChildIdXor010.CountOnes();

        public int TosChildIdXorGrade110
            => TosChildIdXor110.CountOnes();

        public int TosChildIdXorGrade001
            => TosChildIdXor001.CountOnes();

        public int TosChildIdXorGrade101
            => TosChildIdXor101.CountOnes();

        public int TosChildIdXorGrade011
            => TosChildIdXor011.CountOnes();

        public int TosChildIdXorGrade111
            => TosChildIdXor111.CountOnes();


        private GaGbtNumMultivectorOrthogonalProductsStack3(IGaGbtNumMultivectorStack1 stack1, IGaGbtNumMultivectorStack1 stack2, IGaGbtNumMultivectorStack1 stack3) 
            : base(stack1, stack2, stack3)
        {
            MultivectorStack1 = stack1;
            MultivectorStack2 = stack2;
            MultivectorStack3 = stack3;
        }

        
        public bool TosChildMayContainGrade1(int childGrade)
        {
            return
                (TosTreeDepth > 1 && childGrade <= 1) ||
                (TosTreeDepth == 1 && childGrade == 1);
        }

        public bool TosChildMayContainGrade(int childGrade, int grade)
        {
            return
                (TosTreeDepth > 1 && childGrade <= grade) ||
                (TosTreeDepth == 1 && childGrade == grade);
        }


        public GaTerm<double> TosGetTermsEGp()
        {
            var id1 = (int)TosId1;
            var id2 = (int)TosId2;
            var id3 = (int)TosId3;

            var value = TosValue1 * TosValue2 * TosValue3;

            if (GaFrameUtils.IsNegativeEGp(id1, id2, id3))
                value = -value;

            //Console.Out.WriteLine($"id: ({id1}, {id2}, {id3}), value: {value}");

            return new GaTerm<double>(id1 ^ id2 ^ id3, value);
        }

        //public GaTerm<double> TosGetTermsGp(IGaNumMetricOrthogonal metric)
        //{
        //    var id1 = (int)TosId1;
        //    var id2 = (int)TosId2;

        //    var term = metric.Gp(id1, id2);
        //    term.ScalarValue *= TosValue1 * TosValue2;

        //    return term;
        //}

        public GaTerm<double> TosGetTermsGp(double basisBladeSignature)
        {
            var id1 = (int)TosId1;
            var id2 = (int)TosId2;
            var id3 = (int)TosId3;

            var value = 
                TosValue1 * TosValue2 * TosValue3 * basisBladeSignature;

            if (GaFrameUtils.IsNegativeEGp(id1, id2, id3))
                value = -value;

            return new GaTerm<double>(id1 ^ id2 ^ id3, value);
        }


        public IEnumerable<GaTerm<double>> TraverseForEGpGpTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = Stack1.TosHasChild(0);
                var hasChild11 = Stack1.TosHasChild(1);

                var hasChild20 = Stack2.TosHasChild(0);
                var hasChild21 = Stack2.TosHasChild(1);

                var hasChild30 = Stack3.TosHasChild(0);
                var hasChild31 = Stack3.TosHasChild(1);

                if (hasChild31)
                {
                    if (hasChild21)
                    {
                        if (hasChild11) PushDataOfChild(7); //111
                        if (hasChild10) PushDataOfChild(6); //110
                    }

                    if (hasChild20)
                    {
                        if (hasChild11) PushDataOfChild(5); //101
                        if (hasChild10) PushDataOfChild(4); //100
                    }
                }

                if (hasChild30)
                {
                    if (hasChild21)
                    {
                        if (hasChild11) PushDataOfChild(3); //011
                        if (hasChild10) PushDataOfChild(2); //010
                    }

                    if (hasChild20)
                    {
                        if (hasChild11) PushDataOfChild(1); //001
                        if (hasChild10) PushDataOfChild(0); //000
                    }
                }
            }
        }
        
        /// <summary>
        /// (X op Y) lcp Z
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GaTerm<double>> TraverseForEOpLcpLaTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = Stack1.TosHasChild(0);
                var hasChild11 = Stack1.TosHasChild(1);

                var hasChild20 = Stack2.TosHasChild(0);
                var hasChild21 = Stack2.TosHasChild(1);

                var hasChild30 = Stack3.TosHasChild(0);
                var hasChild31 = Stack3.TosHasChild(1);

                if (hasChild31)
                {
                    if (hasChild21)
                    {
                        //if (hasChild11) PushDataOfChild(7); //111
                        if (hasChild10) PushDataOfChild(6); //110
                    }

                    if (hasChild20)
                    {
                        if (hasChild11) PushDataOfChild(5); //101
                        if (hasChild10) PushDataOfChild(4); //100
                    }
                }

                if (hasChild30)
                {
                    //if (hasChild21)
                    //{
                    //    if (hasChild11) PushDataOfChild(3); //011
                    //    if (hasChild10) PushDataOfChild(2); //010
                    //}

                    if (hasChild20)
                    {
                        //if (hasChild11) PushDataOfChild(1); //001
                        if (hasChild10) PushDataOfChild(0); //000
                    }
                }
            }
        }
    }
}