using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;
using TextComposerLib.Text.Tabular;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public sealed class GaNumMapBilinearArray : GaNumMapBilinear
    {
        public static GaNumMapBilinearArray Create(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
        {
            return new GaNumMapBilinearArray(
                targetVSpaceDim,
                associativity
            );
        }

        public static GaNumMapBilinearArray Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearArray(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearArray CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaNumOp(vSpaceDimension).ToArrayMap();
        }

        public static GaNumMapBilinearArray CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToArrayMap();
        }


        private readonly GaNumMapUnilinearSparseColumns[] _basisBladesMaps;


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        //public int FactorsCount
        //{
        //    get
        //    {
        //        var count = 0;

        //        for (var id1 = 0; id1 < DomainGaSpaceDimension1; id1++)
        //        for (var id2 = 0; id2 < DomainGaSpaceDimension2; id2++)
        //        {
        //            var mv = _basisBladesMaps[id1, id2];

        //            if (!mv.IsNullOrZero())
        //                count += mv.NonZeroTerms.Count();
        //        }

        //        return count;
        //    }
        //}

        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                var map = _basisBladesMaps[id1];

                if (ReferenceEquals(map, null))
                    return GaNumTerm.CreateZero(TargetGaSpaceDimension);

                return map[id2]
                       ?? GaNumTerm.CreateZero(TargetGaSpaceDimension);
            }
        }
        

        //public override GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
        //{
        //    get
        //    {
        //        if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
        //            throw new GaNumericsException("Multivector size mismatch");

        //        var tempMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

        //        var mvNodeStack1 = mv1.TermsTree.CreateNodesStack();
        //        var mvNodeStack2 = mv2.TermsTree.CreateNodesStack();

        //        while (mvNodeStack1.Count > 0)
        //        {
        //            var mvNode1 = mvNodeStack1.Pop();
        //            var mvNode2 = mvNodeStack2.Pop();

        //            var id1 = mvNode1.Id;
        //            var id2 = mvNode2.Id;

        //            if (mvNode1.IsLeafNode)
        //            {
        //                var basisBladeMv = _basisBladesMaps[id1, id2];

        //                if (!ReferenceEquals(basisBladeMv, null))
        //                    tempMv.AddFactors(mvNode1.Value * mvNode2.Value, basisBladeMv);

        //                continue;
        //            }

        //            if (mvNode1.HasChildNode0)
        //            {
        //                if (mvNode2.HasChildNode0)
        //                {
        //                    mvNodeStack1.Push(mvNode1.ChildNode0);
        //                    mvNodeStack2.Push(mvNode2.ChildNode0);
        //                }

        //                if (mvNode2.HasChildNode1)
        //                {
        //                    mvNodeStack1.Push(mvNode1.ChildNode0);
        //                    mvNodeStack2.Push(mvNode2.ChildNode1);
        //                }
        //            }

        //            if (mvNode1.HasChildNode1)
        //            {
        //                if (mvNode2.HasChildNode0)
        //                {
        //                    mvNodeStack1.Push(mvNode1.ChildNode1);
        //                    mvNodeStack2.Push(mvNode2.ChildNode0);
        //                }

        //                if (mvNode2.HasChildNode1)
        //                {
        //                    mvNodeStack1.Push(mvNode1.ChildNode1);
        //                    mvNodeStack2.Push(mvNode2.ChildNode1);
        //                }
        //            }
        //        }

        //        //foreach (var biTerm in mv1.GetBiTermsForEGp(mv2))
        //        //{
        //        //    var basisBladeMv = _basisBladesMaps[biTerm.Id1, biTerm.Id2];
        //        //    if (ReferenceEquals(basisBladeMv, null))
        //        //        continue;

        //        //    tempMv.AddFactors(biTerm.ValuesProduct, basisBladeMv);
        //        //}

        //        return tempMv;
        //    }
        //}

        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                foreach (var term1 in mv1.GetStoredTerms())
                {
                    var map = _basisBladesMaps[term1.BasisBladeId];

                    if (ReferenceEquals(map, null))
                        continue;

                    foreach (var term2 in mv2.GetStoredTerms())
                    {
                        var basisBladeMv = map[term2.BasisBladeId];

                        if (!ReferenceEquals(basisBladeMv, null))
                            tempMv.AddTerms(term1.ScalarValue * term2.ScalarValue, basisBladeMv);
                    }
                }

                return tempMv.GetSarMultivector();
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);

                foreach (var term1 in mv1.GetStoredTerms())
                {
                    var map = _basisBladesMaps[term1.BasisBladeId];

                    if (ReferenceEquals(map, null))
                        continue;

                    foreach (var term2 in mv2.GetStoredTerms())
                    {
                        var basisBladeMv = map[term2.BasisBladeId];

                        if (!ReferenceEquals(basisBladeMv, null))
                            tempMv.AddTerms(term1.ScalarValue * term2.ScalarValue, basisBladeMv);
                    }
                }

                return tempMv.GetDgrMultivector();
            }
        }


        private GaNumMapBilinearArray(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            _basisBladesMaps = new GaNumMapUnilinearSparseColumns[DomainGaSpaceDimension];
        }

        private GaNumMapBilinearArray(int domainVSpaceDim, int targetVSpaceDim)
            : base(GaNumMapBilinearAssociativity.NoneAssociative)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            _basisBladesMaps = new GaNumMapUnilinearSparseColumns[DomainGaSpaceDimension];
        }


        public GaNumMapBilinearArray SetBasisBladesMap(int basisBladeId1, int basisBladeId2, IGaNumMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            var map = _basisBladesMaps[basisBladeId1];

            if (!ReferenceEquals(_basisBladesMaps[basisBladeId1], null))
            {
                map.SetColumn(basisBladeId2, targetMv.Compactify());

                return this;
            }

            map = GaNumMapUnilinearSparseColumns.Create(
                DomainVSpaceDimension, 
                TargetVSpaceDimension
            );

            map.SetColumn(basisBladeId2, targetMv.Compactify());

            _basisBladesMaps[basisBladeId1] = map;

            return this;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
                for (var id2 = 0; id2 < DomainGaSpaceDimension; id2++)
                {
                    var mv = this[id1, id2];

                    if (!mv.IsNullOrEmpty())
                        yield return new Tuple<int, int, IGaNumMultivector>(id1, id2, mv);
                }
        }


        public override string ToString()
        {
            var tableText = new TableComposer(TargetGaSpaceDimension, TargetGaSpaceDimension);
            var basisBladeIds = GaFrameUtils.BasisBladeIDs(TargetVSpaceDimension).ToArray();

            foreach (var basisBladeId in basisBladeIds)
            {
                tableText.ColumnsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
                tableText.RowsInfo[basisBladeId].Header = basisBladeId.BasisBladeName();
            }

            for (var basisBladeId1 = 0; basisBladeId1 < TargetGaSpaceDimension; basisBladeId1++)
                for (var basisBladeId2 = 0; basisBladeId2 < TargetGaSpaceDimension; basisBladeId2++)
                {
                    var mv = this[basisBladeId1, basisBladeId2];
                    if (!mv.IsNullOrEmpty())
                        tableText.Items[basisBladeId1, basisBladeId2] = mv.ToString();
                }

            var text = tableText.ToString();

            return text;
        }
    }
}
