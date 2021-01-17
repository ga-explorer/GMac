using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;
using DataStructuresLib.Stacks;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraStructuresLib.GuidedBinaryTraversal;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Products
{
    public sealed class GaGbtNumMultivectorOrthogonalProductsStack2
        : GaGbtStack2
    {
        public static GaGbtNumMultivectorOrthogonalProductsStack2 Create(IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            Debug.Assert(mv1.GaSpaceDimension == mv2.GaSpaceDimension);

            var capacity = (mv1.VSpaceDimension + 1) * (mv1.VSpaceDimension + 1);
            
            var stack1 = mv1.CreateGbtStack(capacity);
            var stack2 = mv2.CreateGbtStack(capacity);

            return new GaGbtNumMultivectorOrthogonalProductsStack2(stack1, stack2);
        }


        private IGaGbtNumMultivectorStack1 MultivectorStack1 { get; }

        private IGaGbtNumMultivectorStack1 MultivectorStack2 { get; }


        public IGaNumMultivector Multivector1 
            => MultivectorStack1.Multivector;

        public IGaNumMultivector Multivector2 
            => MultivectorStack2.Multivector;

        public double TosValue1 
            => MultivectorStack1.TosValue;

        public double TosValue2 
            => MultivectorStack2.TosValue;

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

        public ulong TosChildIdXor00
            => Stack1.TosChildId0 ^ Stack2.TosChildId0;

        public ulong TosChildIdXor10
            => Stack1.TosChildId1 ^ Stack2.TosChildId0;

        public ulong TosChildIdXor01
            => Stack1.TosChildId0 ^ Stack2.TosChildId1;

        public ulong TosChildIdXor11
            => Stack1.TosChildId1 ^ Stack2.TosChildId1;

        public int TosChildIdXorGrade00
            => TosChildIdXor00.CountOnes();

        public int TosChildIdXorGrade10
            => TosChildIdXor10.CountOnes();

        public int TosChildIdXorGrade01
            => TosChildIdXor01.CountOnes();

        public int TosChildIdXorGrade11
            => TosChildIdXor11.CountOnes();


        private GaGbtNumMultivectorOrthogonalProductsStack2(IGaGbtNumMultivectorStack1 stack1, IGaGbtNumMultivectorStack1 stack2) 
            : base(stack1, stack2)
        {
            MultivectorStack1 = stack1;
            MultivectorStack2 = stack2;
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

            var value = TosValue1 * TosValue2;

            if (GaFrameUtils.IsNegativeEGp(id1, id2))
                value = -value;

            //Console.Out.WriteLine($"id1: {id1}, id2: {id2}, value: {value}");

            return new GaTerm<double>(id1 ^ id2, value);
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

            var value = TosValue1 * TosValue2 * basisBladeSignature;

            if (GaFrameUtils.IsNegativeEGp(id1, id2))
                value = -value;

            return new GaTerm<double>(id1 ^ id2, value);
        }


        public IEnumerable<GaTerm<double>> TraverseForEGpTerms()
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

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForOpTerms()
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

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForELcpTerms()
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

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForERcpTerms()
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

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForESpTerms()
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

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                }

                if (hasChild11)
                {
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForEFdpTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEFdp)
                        yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForEHipTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEHip)
                        yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForEAcpTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEAcp)
                        yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForECpTerms()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroECp)
                        yield return TosGetTermsEGp();

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20) PushDataOfChild(0);
                    if (hasChild21) PushDataOfChild(2);
                }

                if (hasChild11)
                {
                    if (hasChild20) PushDataOfChild(1);
                    if (hasChild21) PushDataOfChild(3);
                }
            }
        }


        public IEnumerable<GaTerm<double>> TraverseForGpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature = 
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForLcpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForRcpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForSpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForFdpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEFdp)
                        yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForHipTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEHip)
                        yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForAcpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroEAcp)
                        yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }

        public IEnumerable<GaTerm<double>> TraverseForCpTerms(IGaNumMetricOrthogonal metric)
        {
            PushRootData();

            var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;
            var metricStack = new FixedStack<double>(Capacity);
            metricStack.Push(1);

            while (!IsEmpty)
            {
                var basisBladeSignature = metricStack.Pop();

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosIsNonZeroECp)
                        yield return TosGetTermsGp(basisBladeSignature);

                    continue;
                }

                var hasChild10 = TosHasChild10();
                var hasChild11 = TosHasChild11();
                var hasChild20 = TosHasChild20();
                var hasChild21 = TosHasChild21();

                if (hasChild10)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(0);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        PushDataOfChild(2);
                        metricStack.Push(basisBladeSignature);
                    }
                }

                if (hasChild11)
                {
                    if (hasChild20)
                    {
                        PushDataOfChild(1);
                        metricStack.Push(basisBladeSignature);
                    }

                    if (hasChild21)
                    {
                        var basisVectorSignature =
                            metricBasisVectorsSignaturesList[TosTreeDepth - 1];

                        if (basisVectorSignature != 0)
                        {
                            PushDataOfChild(3);
                            metricStack.Push(basisBladeSignature * basisVectorSignature);
                        }
                    }
                }
            }
        }
    }
}